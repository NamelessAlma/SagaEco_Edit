﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:迪基凱勒(11000977) X:41 Y:196
namespace SagaScript.M10071000
{
    public class S11000977 : Event
    {
        public S11000977()
        {
            this.EventID = 11000977;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000977, 131, "雛菊啊?$R;" +
                                   "$P現在都不能賣喔~$R;" +
                                   "只能隨便看看罷了~$R;" +
                                   "啦啦啦啦~~!$R;", "迪基凱勒");
        }
    }
}




