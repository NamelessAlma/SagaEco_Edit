﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaDB.Skill;

namespace SagaMap.Skill.Additions.Global
{
    public class 鈍足 : DefaultBuff 
    {
        public 鈍足(SagaDB.Skill.Skill skill, Actor actor, int lifetime)
            : base(skill, actor, "鈍足", (int)(lifetime * (1f - actor.AbnormalStatus[SagaLib.AbnormalStatus.鈍足] / 100 - actor.Status.Tenacity)))
        {
            this.OnAdditionStart += this.StartEvent;
            this.OnAdditionEnd += this.EndEvent;
        }

        void StartEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.SpeedDown = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            if (skill.Variable.ContainsKey("SpeedDown"))
                skill.Variable.Remove("SpeedDown");
            int value = actor.Speed / 2;
            skill.Variable.Add("SpeedDown", value);
            actor.Speed -= (ushort)value;
            if (actor.type == ActorType.MOB)
            {
                ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)actor.e;
                //设置AI活动性可以更新AI的移动速度
                SagaMap.Mob.Activity act = eh.AI.AIActivity;
                eh.AI.AIActivity = act;
            }
        }

        void EndEvent(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            actor.Buff.SpeedDown = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            int value = skill.Variable["SpeedDown"];
            actor.Speed += (ushort)value;
            if (actor.type == ActorType.MOB)
            {
                ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)actor.e;
                //设置AI活动性可以更新AI的移动速度
                SagaMap.Mob.Activity act = eh.AI.AIActivity;
                eh.AI.AIActivity = act;
            }
        }
    }
}
