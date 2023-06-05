﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;


namespace SagaMap.Skill.SkillDefinations.Monster
{
    public class SlowLogic : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            args.type = ATTACK_TYPE.BLOW;
            float factor = 0.8f;
            int lifetime = 5000;
            int rate = 50;
            //判定上目前有些问题,预判定没有MoveSpeedDown
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.CannotMove, rate))
            {
                Additions.Global.MoveSpeedDown skill = new SagaMap.Skill.Additions.Global.MoveSpeedDown(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, skill);
            }
            //if (SagaLib.Global.Random.Next(0, 99) < rate)
            //{
                
            //    Additions.Global.MoveSpeedDown skill = new SagaMap.Skill.Additions.Global.MoveSpeedDown(args.skill, dActor, lifetime);
            //    SkillHandler.ApplyAddition(dActor, skill);
            //}
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
        }
    }
}
