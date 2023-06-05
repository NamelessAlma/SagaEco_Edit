﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations
{
    class S20008 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("神谕CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int cdtime = 0;
            int lifetime = 15000;
            short hpuprate = (short)(15 * level);

            //技能CD
            OtherAddition cd = new OtherAddition(null, sActor, "神谕CD", cdtime);
            SkillHandler.ApplyBuffAutoRenew(sActor, cd);

            //赋予BUFF
            Map map = SkillHandler.GetActorMap(sActor);
            List<ActorPC> pcs = SkillHandler.Instance.GetPartyMembersAround(sActor, 500);
            foreach (var item in pcs)
            {
                SkillHandler.Instance.ShowEffectOnActor(sActor, 4483);
                OtherAddition buff = new OtherAddition(null, item, "神谕：丰收", lifetime);
                buff.OnAdditionStart += (s, e) =>
                {
                    item.Buff.最大HP上昇 = true;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                    item.Status.hp_rate_skill += hpuprate;
                    SkillHandler.SendSystemMessage(item, "受到来自 " + sActor.Name + " 的神諭：丰收效果，最大生命值上限提升了。");
                };
                buff.OnAdditionEnd += (s, e) =>
                {
                    item.Buff.最大HP上昇 = false;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                    item.Status.hp_rate_skill -= hpuprate;
                    SkillHandler.SendSystemMessage(item, "神諭：丰收效果消失了。");
                };
                SkillHandler.ApplyBuffAutoRenew(item, buff);
            }
        }
    }
}