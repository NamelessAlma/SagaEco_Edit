﻿
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S910000057 : Event
    {
        public S910000057()
        {
            this.EventID = 910000057;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000057) >= 1)
            {
                TakeItem(pc, 910000057, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            GiveItem(pc, 910000040, 8);//CP
        }
    }
}

