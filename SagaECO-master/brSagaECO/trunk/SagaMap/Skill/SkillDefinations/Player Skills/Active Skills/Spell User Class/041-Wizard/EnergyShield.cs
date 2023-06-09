﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Wizard
{
    public class EnergyShield : ISkill
    {
        bool MobUse;
        public EnergyShield()
        {
            this.MobUse = false;
        }
        public EnergyShield(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {

            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (MobUse)
            {
                level = 5;
            }

            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "EnergyShield", 900000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short defadd = new short[] { 0, 5, 10, 10, 15, 15 }[skill.skill.Level];
            short def = new short[] { 0, 3, 3, 6, 6, 9 }[skill.skill.Level];

            if (skill.Variable.ContainsKey("EnergyShieldDEF"))
                skill.Variable.Remove("EnergyShieldDEF");
            skill.Variable.Add("EnergyShieldDEF", def);

            if (skill.Variable.ContainsKey("EnergyShieldDEFADD"))
                skill.Variable.Remove("EnergyShieldDEFADD");
            skill.Variable.Add("EnergyShieldDEFADD", defadd);
            actor.Status.def_add_skill  += defadd;
            actor.Status.def_skill += def;
            actor.Buff.DefUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.def_skill -= (short)skill.Variable["EnergyShieldDEF"];
            actor.Status.def_add_skill -= (short)skill.Variable["EnergyShieldDEFADD"];
            actor.Buff.DefUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}