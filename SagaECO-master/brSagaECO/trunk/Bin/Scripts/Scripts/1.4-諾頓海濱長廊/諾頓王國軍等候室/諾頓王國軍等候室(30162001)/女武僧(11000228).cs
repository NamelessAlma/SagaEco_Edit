﻿using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30162001
{
    public class S11000228 : Event
    {
        public S11000228()
        {
            this.EventID = 11000228;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Gender == PC_GENDER.MALE)
            {
                Say(pc, 131, "男士請回$R;");
                Warp(pc, 10065000, 70, 6);
                return;
            }
            Say(pc, 131, "這裡是諾頓王國軍本部$R;");
        }
    }
}
