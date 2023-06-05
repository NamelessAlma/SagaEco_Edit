﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;


namespace SagaMap.Skill.SkillDefinations.Monster
{
    public class MagPoison : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0.8f;
            int lifetime = 30000;
            int rate = 20;

            if (SagaLib.Global.Random.Next(0, 99) < rate)
            {
                SkillHandler.Instance.MagicAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
                Additions.Global.Poison skill = new SagaMap.Skill.Additions.Global.Poison(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
    }
}
