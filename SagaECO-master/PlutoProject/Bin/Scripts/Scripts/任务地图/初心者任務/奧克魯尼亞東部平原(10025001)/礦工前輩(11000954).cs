﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:礦工前輩(11000954) X:122 Y:137
namespace SagaScript.M10025001
{
    public class S11000954 : Event
    {
        public S11000954()
        {
            this.EventID = 11000954;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;

            Say(pc, 11000954, 0, "初心者啊?$R;" +
                                 "$R让我们来告诉您生产系职业$R;" +
                                 "是什么吧!$R;", "矿工前辈");

            switch (Select(pc, "要听关于生产系的说明吗?", "", "好啊!", "不用了"))
            {
                case 1:
                    selection = Select(pc, "要听哪种职业的说明呢?", "", "生产系职业特征是?", "「矿工」是?", "「农夫」是?", "「商人」是?", "「冒险家」是?", "不用了");

                    while (selection != 6)
                    {
                        switch (selection)
                        {
                            case 1:
                                Say(pc, 11000954, 0, "我先说明一下生产系的特徵吧!$R;" +
                                                     "$P生产系是在采集/搬运中，$R;" +
                                                     "能力超强的职业喔!$R;" +
                                                     "$P还可以学到各种知识，$R;" +
                                                     "也可以搬很多的行李唷!$R;" +
                                                     "$P而且可以制作，$R;" +
                                                     "在一定范围内可以恢复HP的$R;" +
                                                     "「营火」和「帐篷」呀!$R;" +
                                                     "所有生产系职业，$R;" +
                                                     "都可以学习设置「帐篷」的方法。$R;" +
                                                     "$R休息的时候最好在帐篷里。$R;" +
                                                     "$P生产系第2次转职后，$R;" +
                                                     "就可以给宠物下达攻击的命令哦!$R;", "矿工前辈");
                                break;

                            case 2:
                                Say(pc, 11000954, 0, "由我来介绍「矿工」吧!$R;" +
                                                     "$P「矿工」是 「矿物」的专家喔!$R;" +
                                                     "$P是收集矿物的最强能力者。$R;" +
                                                     "$R而且可以把采集的矿物直接冶炼呢!$R;" +
                                                     "$P虽然不能跟战士系相提并论，$R;" +
                                                     "但只要提高战斗能力的话，$R;" +
                                                     "某种程度上还是可以战斗呀!$R;" +
                                                     "$R召唤岩壁抵挡敌人攻击的$R;" +
                                                     "「大地之壁」还蛮实用的。$R;", "矿工前辈");
                                break;

                            case 3:
                                Say(pc, 11000955, 0, "由我来介绍「农夫」吧!$R;" +
                                                     "$P「农夫」是一生发出光芒的职业喔!$R;" +
                                                     "$R「栽培」「精制」「烹调」等$R;" +
                                                     "技能是农夫们的骄傲。$R;" +
                                                     "$P就以烹调为例。$R;" +
                                                     "$R即使在旅行地，$R:" +
                                                     "只要收集材料，$R;" +
                                                     "就可以马上制作出恢复道具唷!$R;" +
                                                     "$R农夫还可以帮您减少行李，$R;" +
                                                     "他们的精制技能同样不可小看呢!$R;" +
                                                     "$P战斗能力也不是很低，$R;" +
                                                     "所以可以边战斗边采集道具喔!$R;", "农夫前辈");
                                break;

                            case 4:
                                Say(pc, 11000956, 0, "由我来介绍「商人」吧!$R;" +
                                                     "$P「商人」是以交易为主的职业喔!$R;" +
                                                     "$P可以把很多行李放在包包里，$R;" +
                                                     "把以低价购入的道具，$R;" +
                                                     "拿到别的商店以高价出售。$R;" +
                                                     "$R这就是叫「低价买入」和$R;" +
                                                     "「高价卖出」的技能呀!$R;" +
                                                     "$P虽然是不适合战斗的体质…$R;" +
                                                     "$R只要有同伴的话，$R;" +
                                                     "还是有可以发挥巨大力量的技能哦!$R;", "商人前辈");
                                break;

                            case 5:
                                Say(pc, 11000954, 0, "冒险家那傢伙去哪里了…$R;" +
                                                     "$R啊，对了!$R;" +
                                                     "那家伙不在这里，$R;" +
                                                     "由我来代他介绍吧!$R;" +
                                                     "$P因为冒险家那傢伙，$R;" +
                                                     "现在还在那边的「帐篷」里面。$R;" +
                                                     "$R晚一点去打个招呼吧?$R;" +
                                                     "$P「冒险家」，$R;" +
                                                     "是拥有丰富魔物知识的职业。$R;" +
                                                     "$R可以收集各类型敌人的$R;" +
                                                     "大量材料呀!$R;" +
                                                     "$P因为战斗能力挺高的，$R;" +
                                                     "所以独自一人也可以战斗喔!$R;" +
                                                     "$R当然，不能跟…$R;" +
                                                     "战士系相提并论啦!$R;" +
                                                     "$P「冒险家」的技能中，$R;" +
                                                     "最有特色的就是 「野营」。$R;" +
                                                     "$R转职时就可以学到。$R;" +
                                                     "$P使用恢复系技能的话，$R;" +
                                                     "比坐下来恢复更快喔!$R;" +
                                                     "$R如果建在像岩石般可靠的地方，$P;" +
                                                     "恢复量会提高啊!$R;", "矿工前辈");
                                break;
                        }

                        selection = Select(pc, "要听哪种职业的说明呢?", "", "生产系职业特征是?", "「矿工」是?", "「农夫」是?", "「商人」是?", "「冒险家」是?", "不用了");
                    }

                    Say(pc, 11000954, 0, "想转职的话，$R;" +
                                         "到「上城」的「行会宫殿」去看看吧!$R;" +
                                         "$R因为大部分职业的「行会总管」，$R;" +
                                         "都在「行会宫殿」里。$R;" +
                                         "$P当然也有例外。$R;" +
                                         "$R像是「祭司」必须在「白之圣堂」转职，$R;" +
                                         "$R「魔攻师」必须在「黑之圣堂」转职。$R;", "矿工前辈");
                    break;
                    
                case 2:
                    break;
            }
        }
    }
}
