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
    public class S910000011 : Event
    {
        public S910000011()
        {
            this.EventID = 910000011;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(CountItem(pc, 910000011) > 0)
            {
                int rate = Global.Random.Next(0, 100);
                int Coinrate = Global.Random.Next(0, 100);
                int Srate = Global.Random.Next(0, 1000);
                PlaySound(pc, 2040, false, 100, 50);

                if(Coinrate < 10)
                {
                    GiveItem(pc, (uint)Global.Random.Next(910000016, 910000019), 1);
                }
                if (rate == 23)
                {
                    GiveItem(pc, 940000000, 1);
                    PlaySound(pc, 2040, false, 100, 50);
                }
                pc.Gold += Global.Random.Next(5000, 20000);
                GiveItem(pc, 100000000, (ushort)Global.Random.Next(1,3));
                TakeItem(pc, 910000011, 1);
            }
        }
    }
}

