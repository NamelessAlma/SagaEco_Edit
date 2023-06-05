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
    public class S31025 : ISkill
    {

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            byte x, y;
            short[] pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 1000);
            x = SagaLib.Global.PosX16to8(pos[0], map.Width);
            y = SagaLib.Global.PosY16to8(pos[1], map.Height);

            SkillHandler.Instance.ShowEffect(map, sActor, x, y, 4386);
            foreach (var item in sActor.Slave)
            {
                item.ClearTaskAddition();
                map.DeleteActor(item);
            }
            sActor.Slave.Clear();
            Activator timer = new Activator(sActor,x,y);
            timer.Activate();

        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            Map map;
            byte x, y;
            public Activator(Actor caster,byte x,byte y)
            {
                this.caster = caster;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 3000;
                this.x = x;
                this.y = y;
            }
            public override void CallBack()
            {
                try
                {
                    SkillHandler.Instance.ActorSpeak(caster, "孤魂野鬼，何须他人作伴。");
                    ActorMob mob = map.SpawnCustomMob(10560000, map.ID,18610000,0,0,x,y,0,1,0, 孤魂info((uint)(caster.MaxHP * 0.3f)), 孤魂ai(),null,0)[0];
                    ((ActorEventHandlers.MobEventHandler)mob.e).AI.Master = caster;
                    if (caster.type == ActorType.MOB)
                        ((ActorEventHandlers.MobEventHandler)mob.e).AI.Hate = ((ActorEventHandlers.MobEventHandler)caster.e).AI.Hate;
                    mob.Owner = caster;
                    caster.Slave.Add(mob);
                    SkillHandler.Instance.ShowEffectByActor(mob, 4111);
                    Deactivate();
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                }
                Deactivate();
            }
            ActorMob.MobInfo 孤魂info(uint hp)
            {
                ActorMob.MobInfo info = new ActorMob.MobInfo();
                info.name = "鬼之影";
                info.maxhp = 350000;
                info.speed = 500;
                info.atk_min = 1000;
                info.atk_max = 2000;
                info.matk_min = 1;
                info.matk_max = 1;
                info.def = 10;
                info.def_add = 10;
                info.mdef = 10;
                info.mdef_add = 10;
                info.hit_critical = 23;
                info.hit_magic = 118;
                info.hit_melee = 118;
                info.hit_ranged = 120;
                info.avoid_critical = 24;
                info.avoid_magic = 59;
                info.avoid_melee = 60;
                info.avoid_ranged = 60;
                info.Aspd = 540;
                info.Cspd = 540;
                info.elements[SagaLib.Elements.Neutral] = 0;
                info.elements[SagaLib.Elements.Fire] = 0;
                info.elements[SagaLib.Elements.Water] = 0;
                info.elements[SagaLib.Elements.Wind] = 0;
                info.elements[SagaLib.Elements.Earth] = 0;
                info.elements[SagaLib.Elements.Holy] = 0;
                info.elements[SagaLib.Elements.Dark] = 30;
                info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 30;
                info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 30;
                info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 30;
                info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 30;
                info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 30;
                info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 30;
                info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 30;
                info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 30;
                info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 30;
                info.baseExp = 100;
                info.jobExp = 100;

                return info;
            }

            AIMode 孤魂ai()
            {
                AIMode ai = new AIMode(1);
                ai.MobID = 10560000;//怪物ID
                ai.isNewAI = true;//使用的是TT AI
                ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
                ai.ShortCD = 20;//進程技能表最短釋放間隔，3秒一次
                ai.LongCD = 20;//遠程技能表最短釋放間隔，3秒一次
                AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

                /*---------鬼之影---------*/
                skillinfo = new AIMode.SkilInfo();
                skillinfo.CD = 3;//技能CD
                skillinfo.Rate = 20;//釋放概率
                skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
                skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
                ai.SkillOfShort.Add(31020, skillinfo);//將這個技能加進進程技能表
                ai.SkillOfLong.Add(31020, skillinfo);//將這個技能加進進程技能表
                return ai;
            }
        }
    }
}
