﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Event
{
    /// <summary>
    /// 點火
    /// </summary>
    public class MoveUp2 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 90000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MoveUp2", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);

            //移動速度
            int Speed_add = 90;
            if (skill.Variable.ContainsKey("MoveUp2_Speed"))
                skill.Variable.Remove("MoveUp2_Speed");
            skill.Variable.Add("MoveUp2_Speed", Speed_add);

            actor.Speed += (ushort)Speed_add;
            actor.Buff.点火 = true;
            actor.Buff.移動力上昇 = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);

            //移動速度
            actor.Speed -= (ushort)skill.Variable["MoveUp2_Speed"];
            actor.Buff.点火 = false;
            actor.Buff.移動力上昇 = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        #endregion
    }
}
