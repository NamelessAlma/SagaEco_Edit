﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12001: ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            bool active = false;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                {
                    if ((pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE) && pc.TInt["斥候远程模式"] == 1)
                        active = true;
                }

                DefaultPassiveSkill skill = new DefaultPassiveSkill(args.skill, sActor, "连射节奏", active);
                skill.OnAdditionStart += (s, e) =>
                {
                    pc.TInt["连射节奏最大次数"] = 1 + level;
                };
                skill.OnAdditionEnd += (s, e) =>
                {
                    pc.TInt["连射节奏最大次数"] = 0;
                };
                SkillHandler.ApplyAddition(sActor, skill);
            }
        }
    }
}
