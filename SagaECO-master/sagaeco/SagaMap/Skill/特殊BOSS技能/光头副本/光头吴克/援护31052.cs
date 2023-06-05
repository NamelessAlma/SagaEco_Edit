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
    public class S31052 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (sActor.type ==  ActorType.MOB)
            {
                ActorMob mob = (ActorMob)sActor;
                dActor = map.GetActor((uint)mob.TInt["光头疾风ID"]);
                if (dActor == null) return;
                Activator timer = new Activator(sActor, args, dActor);
                timer.Activate();
            }
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            Actor dActor;
            SkillArg skill;
            Map map;
            public Activator(Actor caster, SkillArg args, Actor dactor)
            {
                this.caster = caster;
                this.skill = args;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 30;
                this.dueTime = 0;
                this.dActor = dactor;
            }
            public override void CallBack()
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    Mob.MobAI ai = new MobAI(caster, true);
                    List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(caster.X, map.Width), SagaLib.Global.PosY16to8(caster.Y, map.Height),
                    SagaLib.Global.PosX16to8(dActor.X, map.Width), SagaLib.Global.PosY16to8(dActor.Y, map.Height));
                    if (path.Count <= 1)
                    {
                        Activator2 sc = new Activator2(caster, dActor, skill);
                        sc.Activate();
                        this.Deactivate();
                        return;

                    }
                    short[] pos = new short[2];
                    pos[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                    pos[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                    map.MoveActor(Map.MOVE_TYPE.START, caster, pos, caster.Dir, 1000, true, MoveType.BATTLE_MOTION);

                    EffectArg arg = new EffectArg();
                    arg.effectID = 5323;
                    arg.actorID = 0xFFFFFFFF;
                    arg.x = SagaLib.Global.PosX16to8(pos[0], map.Width);
                    arg.y = SagaLib.Global.PosY16to8(pos[1], map.Height);
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, caster, true);


                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    this.Deactivate();
                }
                //解开同步锁
                ClientManager.LeaveCriticalArea();
            }
        }
        private class Activator2 : MultiRunTask
        {
            Actor caster;
            Actor dActor;
            Map map;
            SkillArg arg;
            int maxcount = 1;
            List<Actor> dactors = new List<Actor>();
            int count = 0;
            public Activator2(Actor caster, Actor dactor, SkillArg args)
            {
                this.caster = caster;
                this.dActor = dactor;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 50;
                this.dueTime = 700;
                dactors = map.GetActorsArea(caster, 500, false);
                this.arg = args;
            }
            public override void CallBack()
            {
                SkillHandler.Instance.ActorSpeak(caster, "疾风！我来救你！");
                SkillHandler.Instance.ShowEffect(map, dActor, 5399);
                int damage = 2000;
                SkillHandler.Instance.CauseDamage(caster, dActor, damage);
                SkillHandler.Instance.ShowVessel(dActor, damage);
                SkillHandler.Instance.ShowEffect(map, dActor, 8059);
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    foreach (var item in dactors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item) && item != dActor)
                        {
                            SkillHandler.Instance.PushBack(caster, item, 3);
                            SkillHandler.Instance.DoDamage(true, caster, item, arg, SkillHandler.DefType.Def, Elements.Neutral, 0, 5f);
                            Stun stun = new Stun(arg.skill, item, 2000);
                            SkillHandler.ApplyAddition(item, stun);
                            item.MP = item.MP / 2;
                            item.SP = item.SP / 2;
                            ((ActorPC)item).e.OnHPMPSPUpdate(item);
                        }
                    }
                    this.Deactivate();
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                    this.Deactivate();
                }
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}

