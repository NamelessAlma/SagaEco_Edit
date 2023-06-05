﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M21193000
{
    public class S11001785 : Event
    {
        public S11001785()
        {
            this.EventID = 11001785;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<ECOchen> ECOchen_mask = new BitMask<ECOchen>(pc.CMask["ECOchen"]);
            //int selection;
            if (ECOchen_mask.Test(ECOchen.打到放水龙后))
            {
                HandleQuest(pc, 69);
                return;
            }
            Say(pc, 0, "呼… 游泳後稍微有點累$R;" +
            "所以正在休息。$R;" +
            "$P可是、因為海$R;" +
            "沒想到喉嚨渇了。$R;" +
            "$R感覺身體正在索求水分的攝取的樣子。$R;", "斯姬");

            //
            /*
            Say(pc, 0, "ふぅ…少し泳ぎ着かれたから$R;" +
            "休憩しているところだ。$R;" +
            "$Pしかし、海というのは$R;" +
            "意外とのどが渇くものだな。$R;" +
            "$R水分の摂取を体が求めているようだ。$R;", "シキ");
            */
        }
    }
}
 