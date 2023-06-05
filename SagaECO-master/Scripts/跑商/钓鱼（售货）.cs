﻿using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaMap;
using SagaDB.Actor;
using SagaMap.Skill;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S60050010 : Event
    {
        public S60050010()
        {
            this.EventID = 60050010;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*if (pc.AInt["跑商次数卡死2"] == 1)
            {
                Say(pc, 131, "抱歉，$R您由于某些原因已被限制该任务。$R$R可能的原因如下：$R您多开了$R使用了黑科技$R等");
                return;
            }
            byte count = 0;
            string lastip = "";
            foreach (SagaMap.Network.Client.MapClient i in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                if (i.Character.Account.LastIP == pc.Account.LastIP && i.Character.Account.GMLevel < 20)
                    count++;
            if (count > 1)
            {
                foreach (SagaMap.Network.Client.MapClient i in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    if (i.Character.Account.LastIP == lastip && i.Character.Account.GMLevel < 20)
                        i.Character.AInt["跑商次数卡死2"] = 1;
                Say(pc, 131, "抱歉，$R您由于某些原因已被限制该任务。$R$R可能的原因如下：$R您多开了$R使用了黑科技$R等");
                return;
            }*/
            if (pc.MapID != 10005000 && pc.Account.GMLevel < 10)
            {
                SInt[pc.Name + "跑商异常"]++;
                pc.Account.Banned = true;
                return;
            }
            if (pc.AInt["跑商西部钓鱼Rate"] == 0)
                pc.AInt["跑商西部钓鱼Rate"] = 1000;
            OpenShopByList(pc, (uint)pc.AInt["跑商西部钓鱼Rate"], SagaDB.Npc.ShopType.ECoin, 953210005, 953210006, 953210007, 953210008, 953210009);
        }
    }
}

