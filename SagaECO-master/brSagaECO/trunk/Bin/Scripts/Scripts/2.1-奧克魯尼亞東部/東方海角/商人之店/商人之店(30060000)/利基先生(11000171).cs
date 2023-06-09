﻿using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M30060000
{
    public class S11000171 : Event
    {
        public S11000171()
        {
            this.EventID = 11000171;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DFHJFlags> mask = new BitMask<DFHJFlags>(pc.CMask["DFHJ"]);

            int selection;
            Say(pc, 131, "我不帥嗎??$R;" +
                "$R不管什麽隨便問吧!$R;");
            if (mask.Test(DFHJFlags.给过青菜))
            {
                switch (Select(pc, "問什麽呢?", "", "關於武器", "關於帽子", "關於T恤", "關於褲子", "關於皮鞋", "關於你", "關於歷卡", "沒什麽可問得"))
                {
                    case 1:
                        Say(pc, 131, "啊!這個啊?$R;" +
                            "你看特別的地方啊$R;" +
                            "$R這是『褓子』媽媽親自做給我的$R;" +
                            "$P雖然是把2張布曡成做成的$R;" +
                            "$R比想像中用的還多$R;" +
                            "$P爲了縫製布需要『針』和『線』$R;" +
                            "$R雖然是理所當然的事情$R;");
                        break;
                    case 2:
                        Say(pc, 131, "啊!這個啊?$R;" +
                            "你看特別的地方啊$R;" +
                            "$R這是『布頭巾』$R;" +
                            "像是商人的象徵一樣的東西$R;" +
                            "$P是把『布』卷起來的$R;" +
                            "$R看這個『玻璃珠』$R;" +
                            "用『膠水』貼上去的$R;" +
                            "不會輕易掉下來$R;" +
                            "$R問玻璃珠是從哪裡弄得?$R;" +
                            "$R是啊?是從爸爸那裡收到的所以…$R;" +
                            "不過『玻璃材料』是原材料$R;" +
                            "這一點是沒有錯的…$R;");
                        break;
                    case 3:
                        Say(pc, 131, "這是『工地夾克』裡$R;" +
                            "附加光屬性的特殊夾克$R;" +
                            "只要能得到在奧克魯尼亞海岸中$R;" +
                            "的『光明召喚石』$R;" +
                            "裁縫阿姨會幫你做的$R;" +
                            "$P光明召喚可以的話$R;" +
                            "『黑暗召喚石』是不是也可以呢$R;" +
                            "$R當作實驗想做一下看看$R;" +
                            "但是我害怕去『不死之島』$R;" +
                            "你要不要代替我去?$R;");
                        break;
                    case 4:
                        Say(pc, 131, "啊!這個啊?$R;" +
                            "你看特別的地方啊，這是『長褲』$R;" +
                            "$R因爲是爸爸穿完不穿給我的$R;" +
                            "所以比較鬆$R;" +
                            "$P爸爸是摩根的出身$R;" +
                            "$R所以應該是在西邊買的$R;");
                        break;
                    case 5:
                        Say(pc, 131, "啊!這個啊?$R;" +
                            "你看特別的地方啊$R;" +
                            "$R這是『真皮涼鞋』$R;" +
                            "在旁邊的大叔正在賣$R;" +
                            "如果喜歡的話買一個吧$R;");
                        break;
                    case 6:
                        Say(pc, 131, "我叫利基先生$R;" +
                            "一邊跟爸爸做生意一邊旅行$R;" +
                            "$R有什麽喜歡的東西就買一個吧?$R;");
                        Say(pc, 131, "什麽?!$R;" +
                            "對客人那是什麽話啊?$R;");
                        break;
                    case 7:
                        mask.SetValue(DFHJFlags.给过青菜, false);
                        selection = Global.Random.Next(1, 4);
                        switch (selection)
                        {
                            case 1:
                                Say(pc, 131, "歷卡是我養的『爬爬蟲凱莉娥』$R;" +
                                    "的名字啊$R;" +
                                    "$R我從小就養了!$R;");
                                break;
                            case 2:
                                Say(pc, 131, "替我把『青菜』給了歷卡$R;" +
                                    "真的謝謝$R;" +
                                    "$R這是我今天的零食$R;" +
                                    "給你…快收下吧$R;");
                                GiveItem(pc, 10009000, 1);
                                break;
                            case 3:
                                Say(pc, 131, "歷卡特別喜歡青菜$R;" +
                                    "像『洋生菜』看都不看!$R;");
                                break;
                            case 4:
                                Say(pc, 131, "歷卡有兄弟的$R;" +
                                    "但現在不知道都去哪裡了$R;");
                                break;
                        }
                        break;
                }
                return;
            }
            switch (Select(pc, "問什麽呢?", "", "關於武器", "關於盾牌", "關於頭髮裝飾", "關於衣服", "關於皮鞋", "關於你", "沒什麽可問得"))
            {
                case 1:
                    Say(pc, 131, "啊!這個啊?$R;" +
                        "你看特別的地方啊$R;" +
                        "$R這是『褓子』媽媽親自做給我的$R;" +
                        "$P雖然是把2張布曡成做成的$R;" +
                        "$R比想像中用的還多$R;" +
                        "$P爲了縫製布需要『針』和『線』$R;" +
                        "$R雖然是理所當然的事情$R;");
                    break;
                case 2:
                    Say(pc, 131, "啊!這個啊?$R;" +
                        "你看特別的地方啊$R;" +
                        "$R這是『布頭巾』$R;" +
                        "像是商人的象徵一樣的東西$R;" +
                        "$P是把『布』卷起來的$R;" +
                        "$R看這個『玻璃珠』$R;" +
                        "用『膠水』貼上去的$R;" +
                        "不會輕易掉下來$R;" +
                        "$R問玻璃珠是從哪裡弄得?$R;" +
                        "$R是啊?是從爸爸那裡收到的所以…$R;" +
                        "不過『玻璃材料』是原材料$R;" +
                        "這一點是沒有錯的…$R;");
                    break;
                case 3:
                    Say(pc, 131, "這是『工地夾克』裡$R;" +
                        "附加光屬性的特殊夾克$R;" +
                        "只要能得到在奧克魯尼亞海岸中$R;" +
                        "的『光明召喚石』$R;" +
                        "裁縫阿姨會幫你做的$R;" +
                        "$P光明召喚可以的話$R;" +
                        "『黑暗召喚石』是不是也可以呢$R;" +
                        "$R當作實驗想做一下看看$R;" +
                        "但是我害怕去『不死之島』$R;" +
                        "你要不要代替我去?$R;");
                    break;
                case 4:
                    Say(pc, 131, "啊!這個啊?$R;" +
                        "你看特別的地方啊，這是『長褲』$R;" +
                        "$R因爲是爸爸穿完不穿給我的$R;" +
                        "所以比較鬆$R;" +
                        "$P爸爸是摩根的出身$R;" +
                        "$R所以應該是在西邊買的$R;");
                    break;
                case 5:
                    Say(pc, 131, "啊!這個啊?$R;" +
                        "你看特別的地方啊$R;" +
                        "$R這是『真皮涼鞋』$R;" +
                        "在旁邊的大叔正在賣$R;" +
                        "如果喜歡的話買一個吧$R;");
                    break;
                case 6:
                    Say(pc, 131, "我叫利基先生$R;" +
                        "一邊跟爸爸做生意一邊旅行$R;" +
                        "$R有什麽喜歡的東西就買一個吧?$R;");
                    Say(pc, 131, "什麽?!$R;" +
                        "對客人那是什麽話啊?$R;");
                    break;
                case 7:
                    break;
            }
        }
    }
}