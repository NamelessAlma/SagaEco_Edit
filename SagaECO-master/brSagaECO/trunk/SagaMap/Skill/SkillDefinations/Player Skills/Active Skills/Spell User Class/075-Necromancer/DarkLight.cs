﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    /// <summary>
    /// 黑暗領域（ダークライト）
    /// </summary>
    public class DarkLight : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
            if (map.CheckActorSkillInRange(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 350))
            {
                return -17;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //设定技能体位置
            actor.MapID = sActor.MapID;
            actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
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
        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            SkillArg skill;
            Map map;
            int lifetime = 0;

            public Activator(Actor caster, ActorSkill actor, SkillArg args, byte level)
            {
                this.actor = actor;
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                lifetime = 15000 + 5000 * level;
                this.period = 1000;
                this.dueTime = 0;
            }

            public override void CallBack()
            {
                //同步锁，表示之后的代码是线程安全的，也就是，不允许被第二个线程同时访问
                ClientManager.EnterCriticalArea();
                try
                {
                    if (lifetime >= 0)
                    {
                        //取得设置型技能，技能体周围7x7范围的怪（范围300，300代表3格，以自己为中心的3格范围就是7x7）
                        List<Actor> actors = map.GetActorsArea(actor, 350, false);
                        List<Actor> affected = new List<Actor>();
                        //取得有效Actor（即怪物）

                        //施加火属性魔法伤害
                        skill.affectedActors.Clear();
                        foreach (Actor i in actors)
                        {
                            if ((i.type == ActorType.PC || i.type == ActorType.MOB) && i != caster)
                                affected.Add(i);
                        }

                        foreach (var item in affected)
                        {
                            if(!item.Status.Additions.ContainsKey("DarkLight"))
                            {
                                DefaultBuff hitdown = new DefaultBuff(skill.skill, item, "DarkLight", 1000);
                                hitdown.OnAdditionStart += hitdown_OnAdditionStart;
                                hitdown.OnAdditionEnd += hitdown_OnAdditionEnd;
                                SkillHandler.ApplyAddition(item, hitdown);
                            }
                        }

                        //广播技能效果
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, actor, false);
                        lifetime -= this.period;
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
                ClientManager.LeaveCriticalArea();
            }

            void hitdown_OnAdditionStart(Actor actor, DefaultBuff skill)
            {
                if (skill.Variable.ContainsKey("DarkLight"))
                    skill.Variable.Remove("DarkLight");
                skill.Variable.Add("DarkLight", skill.skill.Level * 10);
            }

            void hitdown_OnAdditionEnd(Actor actor, DefaultBuff skill)
            {
                if (skill.Variable.ContainsKey("DarkLight"))
                    skill.Variable.Remove("DarkLight");
            }
        }
        #endregion
    }
}