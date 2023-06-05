﻿using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30020002
{
    public class S11000218 : Event
    {
        public S11000218()
        {
            this.EventID = 11000218;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "欢迎光临！", "", "买东西", "卖东西", "什么也不做"))
            {
                case 1:
                    OpenShopBuy(pc, 77);
                    break;
                case 2:
                    OpenShopSell(pc, 77);
                    break;
            }
            Say(pc, 131, "欢迎再来$R;");
        }
    }
}
