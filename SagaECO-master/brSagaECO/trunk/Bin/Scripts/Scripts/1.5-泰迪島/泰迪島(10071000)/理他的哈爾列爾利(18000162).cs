﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:理他的哈爾列爾利(18000162) X:000 Y:000
namespace SagaScript.M10071000
{
    public class S18000162 : Event
    {
        public S18000162()
        {
            this.EventID = 18000162;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}