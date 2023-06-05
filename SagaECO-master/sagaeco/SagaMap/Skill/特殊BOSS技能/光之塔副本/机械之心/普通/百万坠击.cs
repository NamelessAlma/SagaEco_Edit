﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31028 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            /*-------------------魔法阵的技能体-----------------*/
            ActorSkill actor2 = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(31113, 1), sActor);
            actor2.Name = "地AOE大魔法阵";
            actor2.MapID = sActor.MapID;
            actor2.X = sActor.X;
            actor2.Y = sActor.Y;
            actor2.e = new ActorEventHandlers.NullEventHandler();
            map.RegisterActor(actor2);
            actor2.invisble = false;
            map.OnActorVisibilityChange(actor2);
            actor2.Stackable = false;
            /*-------------------魔法阵的技能体-----------------*/


            Activator timer = new Activator(sActor, actor2);
            timer.Activate();
            SkillHandler.Instance.ShowEffectOnActor(sActor, 5266);
            硬直 y = new 硬直(args.skill, sActor, 4000);
            SkillHandler.ApplyAddition(sActor, y);
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            Map map;
            float rate;
            ActorSkill ActorSkill;
            public Activator(Actor caster, ActorSkill actorSkill)
            {
                ActorSkill = actorSkill;
                this.caster = caster;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 3000;
            }
            public override void CallBack()
            {
                try
                {
                    map.DeleteActor(ActorSkill);
                    SkillHandler.Instance.ActorSpeak(caster, "嗯，你问我的体重吗？");
                    List<Actor> actors = map.GetActorsArea(caster, 300, false);
                    SkillHandler.Instance.ShowEffectOnActor(caster, 5399);
                    foreach (var item in actors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                        {
                            int damage = (int)item.MaxHP * 2;
                            SkillHandler.Instance.CauseDamage(caster, item, damage);
                            SkillHandler.Instance.ShowVessel(item, damage);
                        }
                    }

                    List<Actor> actors2 = map.GetActorsArea(caster, 1200, false);
                    foreach (var item in actors2)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                        {
                            int damage = (int)(item.MaxHP * 0.8f);
                            SkillHandler.Instance.CauseDamage(caster, item, damage);
                            SkillHandler.Instance.ShowVessel(item, damage);
                            SkillHandler.Instance.ShowEffectOnActor(item, 5115);
                            零件回收 skill = new 零件回收(null, item, 10000);
                            SkillHandler.ApplyAddition(item, skill);
                        }
                    }
                    if (caster.type == ActorType.MOB)
                    {
                        ActorMob mob = (ActorMob)caster;
                        int count = actors.Count + actors2.Count;
                        mob.TInt["零件数"] += count;
                        SkillHandler.Instance.ShowVessel(mob, 0, -mob.TInt["零件数"], 0);
                    }     
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                }
                Deactivate();
            }
            #endregion
        }
    }
}
