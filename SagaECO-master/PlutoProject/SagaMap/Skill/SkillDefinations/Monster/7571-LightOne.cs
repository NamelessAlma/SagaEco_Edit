﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    public class LightOne : ISkill
    {
        #region
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 2.0f;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Holy, factor);
        }
        #endregion
    }
}
