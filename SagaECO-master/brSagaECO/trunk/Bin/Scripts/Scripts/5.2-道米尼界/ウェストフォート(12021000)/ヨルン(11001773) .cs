﻿using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M12021000
{
    public class S11001773 : Event
    {
        public S11001773()
        {
            this.EventID = 11001773;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "你為什麼 $R;" +
            "來到了這邊的世界？$R;", "ヨルン");

            Say(pc, 11001774, 131, "那為什麼你也來了?$R;" +
            "我是聽說到，現在這個世界$R;" +
            "正面臨困難，便過來幫忙一下...。$R;", "トール");

            Say(pc, 131, "雖然我也有聽聞過、但似乎跟我們$R;" +
            "也沒有直接關係吧？$R;", "ヨルン");

            Say(pc, 11001774, 131, "什麼！？$R;" +
            "果然，塔妮亞族什麼的$R;" +
            "除了關心自已種族之外，$R;" +
            "就不會理會其他種族了。$R;", "トール");

            Say(pc, 131, "嗯？$R;" +
            "這樣的說話可真惹人生氣呢。$R;" +
            "$R我可不是他們的僕人！$R;" +
            "但如果我不擔心他們的話$R;" +
            "我才不會來呢這鬼地方……。$R;", "ヨルン");

            Say(pc, 11001774, 131, "……真是的，你這個傲嬌的僕人。$R;", "トール");

            Say(pc, 112, "吵死了!。$R;", "ヨルン");
           }

    }
               //
               /*"Say(pc, 131, "お前はなんでこっちの$R;" +
            "世界に来たんだ？$R;", "ヨルン");

            Say(pc, 11001774, 131, "なんでって……お前も$R;" +
            "聞いただろ、今この世界が$R;" +
            "大変なことになってるって。$R;", "トール");

            Say(pc, 131, "聞いたけどよ、俺達にとっちゃ$R;" +
            "正直関係のない話だろ？$R;", "ヨルン");

            Say(pc, 11001774, 131, "なっ！？$R;" +
            "……っふ、やっぱタイタニア$R;" +
            "種族なんて自分達のことしか$R;" +
            "考えていないんだな。$R;", "トール");

            Say(pc, 131, "あぁん？$R;" +
            "癇に障るねぇその言い方。$R;" +
            "$R俺は他の奴とは違うんだよ！$R;" +
            "気になんなきゃこんなとこまで$R;" +
            "来ないっつーんだよ……。$R;", "ヨルン");

            Say(pc, 11001774, 131, "……素直じゃない奴だな、お前。$R;", "トール");

            Say(pc, 112, "うるせぇよ。$R;", "ヨルン");*/

}
            
            
        
     
    