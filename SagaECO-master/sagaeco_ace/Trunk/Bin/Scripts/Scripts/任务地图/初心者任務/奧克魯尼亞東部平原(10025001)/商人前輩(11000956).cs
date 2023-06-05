using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:商人前輩(11000956) X:122 Y:139
namespace SagaScript.M10025001
{
    public class S11000956 : Event
    {
        public S11000956()
        {
            this.EventID = 11000956;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;

            Say(pc, 11000956, 0, "哎呀，您好!!$R;" +
                                 "$R對生產系職業有興趣嗎?$R;", "商人前輩");

            switch (Select(pc, "要聽關於生產系的說明嗎?", "", "好啊!", "不用了"))
            {
                case 1:
                    selection = Select(pc, "要聽哪種職業的說明呢?", "", "生產系職業特徵是?", "「礦工」是?", "「農夫」是?", "「商人」是?", "「冒險家」是?", "不用了");

                    while (selection != 6)
                    {
                        switch (selection)
                        {
                            case 1:
                                Say(pc, 11000954, 0, "我先說明一下生產系的特徵吧!$R;" +
                                                     "$P生產系是在採集/搬運中，$R;" +
                                                     "能力超強的職業喔!$R;" +
                                                     "$P還可以學到各種知識，$R;" +
                                                     "也可以搬很多的行李唷!$R;" +
                                                     "$P而且可以製作，$R;" +
                                                     "在一定範圍內可以恢復HP的$R;" +
                                                     "「營火」和「帳篷」呀!$R;" +
                                                     "所有生產系職業，$R;" +
                                                     "都可以學習設置「帳篷」的方法。$R;" +
                                                     "$R休息的時候最好在帳篷裡。$R;" +
                                                     "$P生產系第2次轉職後，$R;" +
                                                     "就可以給寵物下達攻擊的命令唷!$R;", "礦工前輩");
                                break;

                            case 2:
                                Say(pc, 11000954, 0, "由我來介紹「礦工」吧!$R;" +
                                                     "$P「礦工」是 「礦物」的專家喔!$R;" +
                                                     "$P是收集礦物的最強能力者。$R;" +
                                                     "$R而且可以把採得的礦物直接冶煉呢!$R;" +
                                                     "$P雖然不能跟戰士系相提並論，$R;" +
                                                     "但只要提高戰鬥能力的話，$R;" +
                                                     "某種程度上還是可以戰鬥呀!$R;" +
                                                     "$R召喚岩壁抵擋敵人攻擊的$R;" +
                                                     "「大地之壁」還蠻實用的。$R;", "礦工前輩");
                                break;

                            case 3:
                                Say(pc, 11000955, 0, "由我來介紹「農夫」吧!$R;" +
                                                     "$P「農夫」是一生發出光芒的職業喔!$R;" +
                                                     "$R「栽培」「精製」「烹調」等$R;" +
                                                     "技能是農夫們的驕傲。$R;" +
                                                     "$P就以烹調為例。$R;" +
                                                     "$R即使在旅行地，$R:" +
                                                     "只要收集材料，$R;" +
                                                     "就可以馬上製作出恢復道具唷!$R;" +
                                                     "$R農夫還可以幫您減少行李，$R;" +
                                                     "他們的精製技能同樣不可小看呢!$R;" +
                                                     "$P戰鬥能力也不是很低，$R;" +
                                                     "所以可以邊戰鬥邊採集道具喔!$R;", "農夫前輩");
                                break;

                            case 4:
                                Say(pc, 11000956, 0, "由我來介紹「商人」吧!$R;" +
                                                     "$P「商人」是以交易為主的職業喔!$R;" +
                                                     "$P可以把很多行李放在包包裡，$R;" +
                                                     "把以低價購入的道具，$R;" +
                                                     "拿到別的商店以高價出售。$R;" +
                                                     "$R這就是叫「敲詐」和$R;" +
                                                     "「信賴」的技能呀!$R;" +
                                                     "$P雖然是不適合戰鬥的體質…$R;" +
                                                     "$R只要有同伴的話，$R;" +
                                                     "還是有可以發揮巨大力量的技能唷!$R;", "商人前輩");
                                break;

                            case 5:
                                Say(pc, 11000954, 0, "冒險家那傢伙去哪裡了…$R;" +
                                                     "$R啊，對了!$R;" +
                                                     "那傢伙不在這裡，$R;" +
                                                     "由我來代他介紹吧!$R;" +
                                                     "$P因為冒險家那傢伙，$R;" +
                                                     "現在還在那邊的「帳篷」裡面。$R;" +
                                                     "$R晚一點去打個招呼吧?$R;" +
                                                     "$P「冒險家」，$R;" +
                                                     "是擁有豐富魔物知識的職業。$R;" +
                                                     "$R可以收集各類型敵人的$R;" +
                                                     "大量材料呀!$R;" +
                                                     "$P因為戰鬥能力挺高的，$R;" +
                                                     "所以獨自一人也可以戰鬥喔!$R;" +
                                                     "$R當然，不能跟…$R;" +
                                                     "戰士系相提並論啦!$R;" +
                                                     "$P「冒險家」的技能中，$R;" +
                                                     "最有特色的就是 「野營」。$R;" +
                                                     "$R轉職時就可以學到。$R;" +
                                                     "$P使用恢復系技能的話，$R;" +
                                                     "比坐下來恢復更快速喔!$R;" +
                                                     "$R如果建在像岩石般可靠的地方，$P;" +
                                                     "恢復量會提高啊!$R;", "礦工前輩");
                                break;
                        }

                        selection = Select(pc, "要聽哪種職業的說明呢?", "", "生產系職業特徵是?", "「礦工」是?", "「農夫」是?", "「商人」是?", "「冒險家」是?", "不用了");
                    }

                    Say(pc, 11000954, 0, "想轉職的話，$R;" +
                                         "到「上城」的「行會宮殿」去看看吧!$R;" +
                                         "$R因為大部分職業的「行會總管」，$R;" +
                                         "都在「行會宮殿」裡。$R;" +
                                         "$P當然也有例外。$R;" +
                                         "$R像是「祭司」必須在「白之聖堂」轉職，$R;" +
                                         "$R「魔攻師」必須在「黑之聖堂」轉職。$R;", "礦工前輩");
                    break;

                case 2:
                    break;
            }
        }
    }
}
