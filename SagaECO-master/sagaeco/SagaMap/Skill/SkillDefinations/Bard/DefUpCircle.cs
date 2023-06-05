﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Bard
{
    /// <summary>
    /// 森巴演奏（サンバ）
    /// </summary>
    public class DefUpCircle : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (Skill.SkillHandler.Instance.isEquipmentRight(sActor, SagaDB.Item.ItemType.STRINGS) || sActor.Inventory.GetContainer(SagaDB.Item.ContainerType.RIGHT_HAND2).Count > 0)
            {
                if (sActor.Buff.演奏中)
                {
                    return -8;
                }
                return 0;
            }
            return -5;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //设定技能体位置
            actor.MapID = sActor.MapID;
            actor.X = sActor.X;
            actor.Y = sActor.Y;
            //设定技能体的事件处理器，由于技能体不需要得到消息广播，因此创建个空处理器
            actor.e = new ActorEventHandlers.NullEventHandler();
            //在指定地图注册技能体Actor
            map.RegisterActor(actor);
            //设置Actor隐身属性为非
            actor.invisble = false;
            //广播隐身属性改变事件，以便让玩家看到技能体
            map.OnActorVisibilityChange(actor);
            //設置系
            actor.Stackable = false;
            //创建技能效果处理对象
            Activator timer = new Activator(sActor, actor, args, level);
            timer.Activate();

            //註冊施放者的Buff
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "DefUpCircle_Caster", 6000 + 2000 * level);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);

        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.演奏中 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.演奏中 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            int countMax = 3, count = 0;
            int lifeTime = 0;
            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 500;
                this.dueTime = 0;
                this.lifeTime = 6000 + 2000 * level;
                countMax = (6000 + 2000 * level) / period;
            }
            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        //取得设置型技能，技能体周围7x7范围的怪（范围300，300代表3格，以自己为中心的3格范围就是7x7）
                        List<Actor> actors = map.GetActorsArea(actor, 200, false);
                        List<Actor> affected = new List<Actor>();
                        //取得有效Actor

                        skill.affectedActors.Clear();
                        foreach (Actor act in actors)
                        {
                            if (act.type == ActorType.PC || act.type == ActorType.PET)
                            {
                                if (!act.Status.Additions.ContainsKey("DefUpCircle"))
                                {
                                    DefaultBuff skill2 = new DefaultBuff(skill.skill, act, "DefUpCircle", lifeTime - count * period);
                                    skill2.OnAdditionStart += this.StartEventHandler;
                                    skill2.OnAdditionEnd += this.EndEventHandler;
                                    skill2.OnUpdate += this.TimerEventHandler;
                                    SkillHandler.ApplyAddition(act, skill2);
                                }
                            }
                        }
                        //广播技能效果
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
                        count++;
                    }
                    else
                    {
                        this.Deactivate();
                        //在指定地图删除技能体（技能效果结束）
                        map.DeleteActor(actor);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                //解开同步锁
                //测试去除技能同步锁 ClientManager.LeaveCriticalArea();
            }
            void StartEventHandler(Actor actor, DefaultBuff skill)
            {
                int level = skill.skill.Level;
                short LDef = (short)(5 * level), RDef = LDef;
                //左防
                if (skill.Variable.ContainsKey("DefUpCircle_LDef"))
                    skill.Variable.Remove("DefUpCircle_LDef");
                skill.Variable.Add("DefUpCircle_LDef", LDef);
                //右防
                if (skill.Variable.ContainsKey("DefUpCircle_RDef"))
                    skill.Variable.Remove("DefUpCircle_RDef");
                skill.Variable.Add("DefUpCircle_RDef", RDef);

                //左防
                actor.Status.def_skill += LDef;
                //右防
                actor.Status.def_add_skill += RDef;

                //actor.Buff.防御率上昇 = true;
                //actor.Buff.防御力上昇 = true;

                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, null, actor, true);
            }
            void EndEventHandler(Actor actor, DefaultBuff skill)
            {
                //左防
                actor.Status.def_skill -= (short)skill.Variable["DefUpCircle_LDef"];
                //右防
                actor.Status.def_add_skill -= (short)skill.Variable["DefUpCircle_RDef"];

                //actor.Buff.防御率上昇 = false;
                //actor.Buff.防御力上昇 = false;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
            void TimerEventHandler(Actor actor, DefaultBuff skill)
            {
                if (!SkillHandler.Instance.isInRange(this.actor, actor, 200))
                {
                    skill.AdditionEnd();
                }
            }
        }
        #endregion
    }
}
