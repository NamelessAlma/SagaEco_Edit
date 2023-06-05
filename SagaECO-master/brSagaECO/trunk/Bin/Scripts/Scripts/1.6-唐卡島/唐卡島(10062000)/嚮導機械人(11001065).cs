﻿using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001065 : Event
    {
        public S11001065()
        {
            this.EventID = 11001065;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tangka> Tangka_mask = pc.CMask["Tangka"];
            NavigateCancel(pc);
            if (!Tangka_mask.Test(Tangka.嚮導機械人第一次對話))
            {
                Say(pc, 131, "歡迎到工作室城市唐卡$R;" +
                    "我是嚮導系統$R;" +
                    "$P唐卡市是一個$R;" +
                    "活動木偶和飛空庭為代表的城市$R;" +
                    "$R有很多珍貴的活動木偶或$R;" +
                    "改造飛空庭的研究工作室。$R;" +
                    "$P願意的話，我會簡單說明的。$R;" +
                    "怎麼辦呢？$R;");
                switch (Select(pc, "怎麼辦呢？", "", "請說明", "不用了"))
                {
                    case 1:
                        Tangka_mask.SetValue(Tangka.嚮導機械人第一次對話, true);
                        //_7a47 = true;
                        Say(pc, 131, "那麼，開始說明吧！$R;" +
                            "$P從這裡看到的東邊磚瓦建築$R;" +
                            "就是活動木偶研究設施。$R;" +
                            "$R活動木偶總部和$R;" +
                            "叫『多爾斯』的唐卡軍隊$R;" +
                            "正共同研究活動木偶。$R;" +
                            "$P這裡研究的是關於$R;" +
                            "活動木偶的存在理由和狀態，$R;" +
                            "$R對大部分人來說都是挺悶的內容。$R;");
                        Say(pc, 131, "想要活動木偶時，$R;" +
                            "$R請到『個人工作室』看看吧。$R;" +
                            "$P看見西邊的圓柱型建築了嗎？$R;" +
                            "那就是『個人工作室』唷。$R;" +
                            "$R這裡是任何人都可以自由進出的$R;" +
                            "經濟特區所以聚集了很多工匠。$R;" +
                            "$P其中有實力的工匠，$R都擁有個人工作室。$R;" +
                            "$R到那裡也許能拿到活動木偶唷。$R;");
                        Say(pc, 131, "沿著個人工作室的工匠街往西走，$R;" +
                            "那裡有個很大的飛空庭工廠唷。$R;" +
                            "$P那裡可以提供各種$R關於飛空庭的服務，$R由組裝到改造，一應俱全$R;" +
                            "$R如果對擁有的飛空庭不滿的話$R請去看看吧。$R;" +
                            "$P有一點要注意，需要很多那個……$R;" +
                            "那個…錢…$R;" +
                            "$R我當然沒有那麼多錢，$R只靠微薄的工資維持生活，$R;" +
                            "$R我已知足了…哈哈哈$R;");
                        Say(pc, 131, "在工匠街的交叉路向北看，$R可以看到機械神匠總部。$R;" +
                            "$R雙腳步行機器人停在街上，$R;" +
                            "馬上會知道的。$R;" +
                            "$P這個世界的大部分機件都是在$R這總部製作的。$R;" +
                            "$R其實我也是這裡人…$R;" +
                            "誇獎自己，有點不好意思，$R;" +
                            "$R但這裡的技術確實是世界頂級的。$R;" +
                            "其實跟別的地方，沒法相比。$R;" +
                            "$P只是沒有投資者認可技術能力，$R所以…$R;" +
                            "$R機械神匠行會一直很窮，$R;" +
                            "所以能不能捐款…$R;");
                        Say(pc, 131, "到工匠街交叉路南邊的山丘，$R有住宅街和商店街。$R;" +
                            "$P雖然說是商店街，$R但是來的都是這裡的居民，$R;" +
                            "所以不知道有沒有從遠方來的人$R喜歡的東西呀。$R;");
                        Say(pc, 131, "還有山丘上有巨大的皮諾像的公園。$R;" +
                            "$R對了，這裡也能看到。$R;" +
                            "$P也是這條街的地標。$R;" +
                            "對地攤、商人、復活的戰士來說$R挺繁華的街道。$R;" +
                            "$R去看看吧，$R;" +
                            "$P我推薦您一件好東西吧。$R;" +
                            "聽說那個皮諾像有秘密按扭。$R;" +
                            "$P那麼，剩下的，$R您自己去看看吧。$R;" +
                            "都告訴了您就沒意思了。$R;" +
                            "$P哎呀，說得夠長了。$R;" +
                            "謝謝您的聆聽。$R;" +
                            "$R那麼祝您旅途愉快。$R;" +
                            "$R再見了喔$R;");
                        break;
                    case 2:
                        break;
                }
                return;
            }
            switch (Select(pc, "想讓我介紹哪裡，請選擇。", "", "機場", "活動木偶總部", "多爾斯軍本部", "活動木偶研究所", "機械神匠總部", "飛空庭大工廠", "唐卡大廈", "商店街", "放棄"))
            {
                case 1:
                    Say(pc, 131, "機場就在後面不用帶路吧？$R;" +
                        "$R在機場可以搭乘去阿伊恩薩烏斯的$R飛空庭。$R;" +
                        "$P乘一次100金幣，不過那裡的$R哈爾列爾利小子怕小狗…$R;" +
                        "如果帶著狗去…$R;" +
                        "嗯，就當沒聽見好嗎。$R;");
                    break;
                case 2:
                    Navigate(pc, 180, 146);
                    Say(pc, 131, "活動木偶總部就是在東邊的$R那座磚瓦房。$R;" +
                        "$R用箭頭指示，帶您到入口吧。$R;" +
                        "$P那裡有販賣石像產品目錄$R;" +
                        "並提供石像製作、$R;" +
                        "發佈石像產品目錄、$R;" +
                        "設計魔物外型等服務$R;" +
                        "$P總部代表原來是很溫和親切的人，$R;" +
                        "不過最近臉色不太好…$R;" +
                        "$R真擔心哪裡不舒服$R;");
                    break;
                case 3:
            Navigate(pc, 173, 121);
            Say(pc, 131, "多爾斯軍本部就是$R東邊看到的磚瓦房。$R;" +
                "$P多爾斯軍，是由木偶術師軍隊編成的$R;" +
                "唐卡防衛軍。$R;" +
                "$R人員少，大部分都是活動木偶。$R;" +
                "$P叫耶爾德活動木偶的$R特殊活動木偶，$R;" +
                "聽說可以操縱幾十…$R不有時候幾百個活動木偶，$R;" +
                "進行戰鬥唷。$R;" +
                "$P是很特殊的部隊，所以不接受傭兵。$R;" +
                "$R現在只是給您帶路，$R不過去了也會被趕出來的。$R;");
                    break;
                case 4:
            Navigate(pc, 210, 100);
            Say(pc, 131, "從這裡看到的東邊的磚瓦建築就是$R;" +
                "活動木偶研究設施。$R;" +
                "$R活動木偶研究設施分為$R第1研究所和第2研究所。$R;" +
                "$P第1研究所研究關於$R得到活動木偶的方法$R;" +
                "和特性。$R;" +
                "$R如果您有使用活動木偶，不妨聽聽？$R;" +
                "$R也許能聽到有用的消息。$R;" +
                "$P第2研究所就在第1研究所的旁邊。$R;" +
                "但一切都是軍事機密，$R;" +
                "$R入口的哈爾列爾利不會讓閒人進去的。$R;" +
                "$P這是我的想法，$R;" +
                "那個哈爾列爾利很奇怪。$R;" +
                "$R說它害怕呢，還是說它冷淡呢。$R;" +
                "真不敢相信是像我一樣的活動木偶$R;" +
                "$R先躲避為上策。$R;" +
                "$P那麼，先帶您到第1研究所吧。$R;");
                    break;
                case 5:
            Navigate(pc, 132, 27);
            Say(pc, 131, "從這裡看到西北邊的$R那棟藍色混凝土建築就是$R;" +
                "機械神匠總部。$R;" +
                "$R用箭頭指示，帶您到入口吧。$R;" +
                "$P這村子裡的警備機器人或$R阿高普路斯市的$R;" +
                "嚮導系統，都是在總部製作的喔。$R;" +
                "$P我在嚮導系統裡是較高級的，$R;" +
                "與一般的嚮導系統比起來$R;" +
                "語言功能更接近人類。$R;" +
                "$P沒有拒否感，但因為太吵，$R常常被罵。$R;" +
                "$R哎呀，我還是別說廢話，快帶路吧。$R;" +
                "$R對不起$R;");
                    break;
                case 6:
            Navigate(pc, 73, 70);
            Say(pc, 131, "飛空庭工廠在這裡的西北部。$R;" +
                "$R是衣做非常大的大工廠，$R;" +
                "用箭頭指示，帶您到處看看吧$R;" +
                "$P飛空庭組裝$R;" +
                "家具銷售$R;" +
                "木材加工等都可以做。$R;" +
                "$P嗯…只要有那個(錢)，$R這裡是一個令您$R非常愉快的地方。$R;");
                    break;
                case 7:
            Navigate(pc, 93, 151);
            Say(pc, 131, "唐卡大廳在這裡的西南部喔$R;" +
                "$R用箭頭指示，帶您到入口吧。$R;" +
                "$P雖然有市長在，但這裡的$R;" +
                "行會力量太強大，$R沒有什麼可做的事情。$R;" +
                "$P常常可以看到他，換換街燈的燈泡、$R;" +
                "跟老人在一起、$R;" +
                "在公園看著孩子們玩耍的情形。$R;" +
                "$P對了。$R;" +
                "$R在進行清潔計劃$R;" +
                "請一定要利用唷。$R;");
                    break;
                case 8:
                    Navigate(pc, 72, 161);
            Say(pc, 131, "商店街在南邊唷。$R;" +
                "$R用箭頭指示，$R帶您到商店街入口吧。$R;" +
                "$P有裁縫店$R;" +
                "武器店$R;" +
                "古董商$R;" +
                "咖啡館等，居民們使用的店$R;" +
                "$P雖然有點遠$R;" +
                "$R附近還有商店和寶石商喔$R;");
                    break;
                case 9:
                    break;
            }
        }
    }
}