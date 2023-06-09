﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
namespace SagaScript.M10046000
{
    public class S11000429 : Event
    {
        public S11000429()
        {
            this.EventID = 11000429;
            this.questTransportSource = "哞嗚嗚嗚~~!!$R;";
            this.transport = "哞~哞~!$R;";
            this.questTransportCompleteSrc = "哞!!$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10032800) >= 1)
            {
                Say(pc, 361, "哞~哞~!$R;");
                switch (Select(pc, "要給杰利科嗎?", "", "因爲太可愛了，所以給!", "不給"))
                {
                    case 1:
                        Say(pc, 361, "哞!$R;");
                        TakeItem(pc, 10032800, 1);
                        Say(pc, 131, "吃『杰利科』吃的好香啊!$R;");
                        break;
                    case 2:
                        Say(pc, 111, "哞嗚……$R;");
                        break;
                }
                return;
            }
            Say(pc, 111, "……$R;");
        }
    }
}
