﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Knight
{
    /// <summary>
    /// 盾屏障（ホールドシールド）
    /// </summary>
    public class HoldShield : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            //只能在對方背靠牆壁時才可使用
            //使用條件：盾
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 1000 * level;
            硬直 skills = new 硬直(args.skill, sActor, lifetime);
            SkillHandler.ApplyAddition(sActor, skills);
            硬直 skilld = new 硬直(args.skill, dActor, lifetime);
            SkillHandler.ApplyAddition(dActor, skills);            
        }
        #endregion
    }
}
