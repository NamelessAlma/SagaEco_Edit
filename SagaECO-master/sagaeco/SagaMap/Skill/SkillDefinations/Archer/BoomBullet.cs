﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Archer
{
    /// <summary>
    /// 爆裂彈
    /// </summary>
    public class BoomBullet : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckSkillCanCastForWeapon(pc, args))
                return 0;
            return -5;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, 2f);
            MpDown mp = new MpDown(args.skill, sActor, dActor, 10000, (int)(dActor.MP * 0.05f));
            SkillHandler.ApplyAddition(dActor, mp);
            SkillHandler.Instance.Homicidal(sActor, 1);
        }
        #endregion
    }
}
