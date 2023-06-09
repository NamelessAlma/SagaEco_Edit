﻿using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30090001
{
    public class S11000767 : Event
    {
        public S11000767()
        {
            this.EventID = 11000767;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<MZTYSFlags> mask = pc.CMask["MZTYS"];
            if (pc.Account.GMLevel >= 100)
            {
                switch (Select(pc, "管理者管理模式", "", "武器合成", "裝備樣式還原", "聽取武器合成的注意要點", "什麼都不做", "普通对话"))
                {
                    case 1:
                        ItemFusion(pc, "這個價格、可以么？");
                        return;
                    case 2:
                        ItemFusion(pc, "這個價格、可以么？");
                        return;
                    case 3:
                        Say(pc, 131, "想合成武器的話$R;" +
                            "$R需要保留外貌和保留能力$R;" +
                            "的兩種道具。$R;" +
                            "$R一定需要兩種唷$R;" +
                            "$P武器合成後$R;" +
                            "兩件道具將成為一件$R;" +
                            "$R武器將「不能還原」，請注意$R;" +
                            "$P每件道具都有屬性$R;" +
                            "如果兩件道具屬性不和$R;" +
                            "就不能進行武器合成$R;" +
                            "$R屬性的條件有很多$R;" +
                            "$P而最重要的，就是必須同一種裝備$R;" +
                            "$R例如在頭盔的外形上$R;" +
                            "加上甲衣的屬性$R;" +
                            "怎麼看都是不可能的吧$R;" +
                            "$P除此之外，$R還有兩者的性別、種族、$R;" +
                            "職業、所屬團體、等級等，很多條件$R;" +
                            "你們自己找一些屬性相和的道具$R;" +
                            "試試看吧$R;" +
                            "$P武器合成成功的話$R;" +
                            "會出現一個★型圖示$R;" +
                            "$R如果想知道道具的外形$R;" +
                            "或者是更詳盡的資料$R;" +
                            "請用滑鼠右擊，在外形資料室裡查看$R;" +
                            "$P如果可以的話$R;" +
                            "先用比較便宜的道具，試試各種合成$R;" +
                            "因為武器合成可以反覆試驗$R;" +
                            "$R已合成的武器$R;" +
                            "外型看起來好像有破損的話$R;" +
                            "可以跟其他道具再次合成。$R;" +
                            "$P喜歡的東西要好好珍惜啊！$R;" +
                            "$P……$R;" +
                            "$P對吧！雖然是聽說的，但也有道理。$R;" +
                            "$R這裡好像有叫『完美的絲』$R;" +
                            "可以提高成功率的道具呢$R;" +
                            "$R要怎樣才能找到呢？$R;");
                        return;
                    case 4:
                        return;
                }
            }

            if (!mask.Test(MZTYSFlags.交給謎語團面具))
            {
                if (mask.Test(MZTYSFlags.幫忙送謎語團面具))
                {
                    if (CountItem(pc, 50025080) >= 1)
                    {
                        Say(pc, 131, "已經把面具轉交給佈魯了嘛？$R;");
                        return;
                    }
                    mask.SetValue(MZTYSFlags.交給謎語團面具, true);
                    TakeItem(pc, 10000306, 1);
                    Say(pc, 131, "冒險者，你好！$R;" +
                        "$R噢！佈魯讓你給我帶藥了？$R;" +
                        "$P看你把藥拿回來，$R;" +
                        "一定已經把面具交給佈魯了。$R;" +
                        "$R真是感謝你呢$R;" +
                        "$P對了，如果你的裝備裡$R;" +
                        "有快壞掉的東西的話$R;" +
                        "現在就告訴我吧$R;" +
                        "$R我會像佈魯的面具那樣$R;" +
                        "給你改裝裝備的外貌的$R;" +
                        "$P像這樣將兩件道具合成一件的技術$R;" +
                        "發明的時候，開始就叫「武器合成」$R;" +
                        "$P如果進行武器合成的話$R;" +
                        "道具會變成一個$R;" +
                        "$R以後就不能變回原狀態了$R;" +
                        "$R所以要注意唷$R;" +
                        "$P像我這樣的叫做「愛美的匠人」$R;" +
                        "$R很久以前$R;" +
                        "據說有很多像我這樣的人$R;" +
                        "但是現在的人，$R如果裝備壞掉，都直接扔掉啦$R;" +
                        "然後再購買新裝備$R;" +
                        "$P所以像我這樣的匠人$R;" +
                        "就剩的不多了$R;" +
                        "$R也許因為時代變化太多了，唉$R;");
                    return;
                }
                if (mask.Test(MZTYSFlags.收到謎語團面具))
                {
                    Say(pc, 131, "已經把面具轉交給佈魯了嘛？$R;");
                    return;
                }
                if (mask.Test(MZTYSFlags.詢問謎語團面具))
                {
                    Say(pc, 131, "冒險者，我有事情想拜託你$R;" +
                        "$R可以將這個面具$R;" +
                        "轉交給佈魯嘛?$R;");
                    switch (Select(pc, "該怎麼辦呢？", "", "交給他", "不交給他"))
                    {
                        case 1:
                            if (CheckInventory(pc, 50025080, 1))
                            {
                                GiveItem(pc, 50025080, 1);
                                mask.SetValue(MZTYSFlags.收到謎語團面具, true);
                                Say(pc, 131, "收到了『佈魯的面具』$R;", "");
                                Say(pc, 131, "打開道具倉，$R;" +
                                    "看看給你的道具吧。$R;" +
                                    "$P看到貼著★的『艾米頭盔』嘛？$R;" +
                                    "這就是剛才給你的道具。$R;" +
                                    "$R像這樣變更外型的話$R;" +
                                    "就會貼著★圖示。$R;" +
                                    "$P貼著★的圖示$R;" +
                                    "外型跟實際的性能不一樣的阿。$R;" +
                                    "一定要記住呀。$R;" +
                                    "哎呀，說的太多了。$R;" +
                                    "一定要幫我把$R;" +
                                    "『佈魯的面具』交給他啊！$R;");
                                return;
                            }
                            Say(pc, 131, "要拿的東西太多了$R;" +
                                "該怎麼辦呢？$R;");
                            break;
                    }
                    return;
                }
                if (mask.Test(MZTYSFlags.交給藥))
                {
                    Say(pc, 131, "這不是冒險者嘛？$R;" +
                        "什麼事啊？$R;");
                    switch (Select(pc, "有什麼事嘛？", "", "佈魯拜託的事情是什麼啊？", "沒什麼事。"))
                    {
                        case 1:
                            mask.SetValue(MZTYSFlags.詢問謎語團面具, true);
                            Say(pc, 131, "請將佈魯弄壞的『謎語團面具』外貌$R;" +
                                "貼在『艾米頭盔』上。$R;" +
                                "$P在謎語團，如果把面具弄壞的話$R;" +
                                "就會被趕出來的$R;" +
                                "$R所以這樣做的話$R;" +
                                "只需要把面具原來的外形$R;" +
                                "貼在別的裝備上，就可以蒙混過關$R;" +
                                "$P這樣看起來就像『謎語團面具』$R;" +
                                "$R謎語團的人$R;" +
                                "一直使用一樣的面具的。$R;" +
                                "$P在我們聊的期間$R;" +
                                "佈魯的面具已經完成了。$R;");
                            Say(pc, 131, "冒險者，我有事情想拜託你$R;" +
                                "$R可以將這個面具$R;" +
                                "轉交給佈魯嘛?$R;");
                            switch (Select(pc, "該怎麼辦呢？", "", "交給他", "不交給他"))
                            {
                                case 1:
                                    if (CheckInventory(pc, 50025080, 1))
                                    {
                                        GiveItem(pc, 50025080, 1);
                                        mask.SetValue(MZTYSFlags.收到謎語團面具, true);
                                        Say(pc, 131, "收到了『佈魯的面具』$R;");
                                        Say(pc, 131, "打開道具倉，$R;" +
                                            "看看給你的道具吧。$R;" +
                                            "$P看到貼著★的『艾米頭盔』嘛？$R;" +
                                            "這就是剛才給你的道具。$R;" +
                                            "$R像這樣變更外型的話$R;" +
                                            "就會貼著★圖示。$R;" +
                                            "$P貼著★的圖示$R;" +
                                            "外型跟實際的性能不一樣的阿。$R;" +
                                            "一定要記住呀。$R;" +
                                            "哎呀，說的太多了。$R;" +
                                            "一定要幫我把$R;" +
                                            "『佈魯的面具』交給他啊！$R;");
                                        return;
                                    }
                                    Say(pc, 131, "要拿的東西太多了$R;" +
                                        "該怎麼辦呢？$R;");
                                    break;
                            }
                            break;
                    }
                    return;
                }
                if (CountItem(pc, 10000306) == 0)
                {
                    Say(pc, 131, "咳咳！$R;" +
                        "$R竟然有訪客？竟然還有這種事$R;" +
                        "真是可愛的冒險者$R;" +
                        "$P真的不好意思，我今天不太舒服$R;" +
                        "請回吧，咳咳！$R;");
                    return;
                }
                mask.SetValue(MZTYSFlags.交給藥, true);
                TakeItem(pc, 10000306, 1);
                Say(pc, 131, "咳咳！$R;" +
                    "$R有人來了……$R大概有別的事吧！$R;" +
                    "真是可愛的冒險者。$R;" +
                    "$P這不是從佈魯那拿來的藥嘛？$R;");
                ShowEffect(pc, 11000767, 4118);
                Wait(pc, 3000);
                Say(pc, 131, "那些藥好像挺有效的$R;" +
                    "謝謝您拿來給我喔$R;" +
                    "$R真的感謝您$R;" +
                    "$P還要代我跟佈魯說聲謝謝…嘻嘻…$R;" +
                    "$R他這個人真奇怪啊！$R;" +
                    "我都不認識他$R;" +
                    "他居然還讓人給我送藥呢$R;" +
                    "$P真好，他身體好多了，$R;" +
                    "雖然只是佈魯拜託的事情$R;" +
                    "也試試看吧……$R;");
                return;
            }

            Say(pc, 131, "噢！$R冒險者，請來$R;" +
                "今天你想做什麼呢？$R;");
            switch (Select(pc, "今天你想做什麼呢？", "", "武器合成", "聽取武器合成的注意要點", "什麼都不做"))
            {
                case 1:
                    ItemFusion(pc, "這個價格、可以么？");
                    break;
                case 2:
                    Say(pc, 131, "想合成武器的話$R;" +
                        "$R需要保留外貌和保留能力$R;" +
                        "的兩種道具。$R;" +
                        "$R一定需要兩種唷$R;" +
                        "$P武器合成後$R;" +
                        "兩件道具將成為一件$R;" +
                        "$R武器將「不能還原」，請注意$R;" +
                        "$P每件道具都有屬性$R;" +
                        "如果兩件道具屬性不和$R;" +
                        "就不能進行武器合成$R;" +
                        "$R屬性的條件有很多$R;" +
                        "$P而最重要的，就是必須同一種裝備$R;" +
                        "$R例如在頭盔的外形上$R;" +
                        "加上甲衣的屬性$R;" +
                        "怎麼看都是不可能的吧$R;" +
                        "$P除此之外，$R還有兩者的性別、種族、$R;" +
                        "職業、所屬團體、等級等，很多條件$R;" +
                        "你們自己找一些屬性相和的道具$R;" +
                        "試試看吧$R;" +
                        "$P武器合成成功的話$R;" +
                        "會出現一個★型圖示$R;" +
                        "$R如果想知道道具的外形$R;" +
                        "或者是更詳盡的資料$R;" +
                        "請用滑鼠右擊，在外形資料室裡查看$R;" +
                        "$P如果可以的話$R;" +
                        "先用比較便宜的道具，試試各種合成$R;" +
                        "因為武器合成可以反覆試驗$R;" +
                        "$R已合成的武器$R;" +
                        "外型看起來好像有破損的話$R;" +
                        "可以跟其他道具再次合成。$R;" +
                        "$P喜歡的東西要好好珍惜啊！$R;" +
                        "$P……$R;" +
                        "$P對吧！雖然是聽說的，但也有道理。$R;" +
                        "$R這裡好像有叫『完美的絲』$R;" +
                        "可以提高成功率的道具呢$R;" +
                        "$R要怎樣才能找到呢？$R;");
                    break;
            }
        }
    }
}