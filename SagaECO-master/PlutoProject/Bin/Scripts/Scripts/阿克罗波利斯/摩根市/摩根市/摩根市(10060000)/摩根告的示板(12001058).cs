﻿using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10060000
{
    public class S12001058 : Event
    {
        public S12001058()
        {
            this.EventID = 12001058;

        }

        public override void OnEvent(ActorPC pc)
        {
            OpenBBS(pc, 8, 0);
        }
    }
}