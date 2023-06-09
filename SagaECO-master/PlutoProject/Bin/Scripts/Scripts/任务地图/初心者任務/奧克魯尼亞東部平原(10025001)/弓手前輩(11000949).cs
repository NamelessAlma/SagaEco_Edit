﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:弓手前輩(11000949) X:150 Y:106
namespace SagaScript.M10025001
{
    public class S11000949 : Event
    {
        public S11000949()
        {
            this.EventID = 11000949;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;

            Say(pc, 11000949, 0, "您好! 很高兴见到您!$R;" +
                                 "$R对战士系职业，有什么想知道的吗?$R;", "弓手前辈");

            switch (Select(pc, "想问有关战士系的消息吗?", "", "好啊", "不用了"))
            {
                case 1:
                    selection = Select(pc, "想听哪种的说明呢?", "", "战士系的特徵", "「剑士」是?", "「骑士」是?", "「盗贼」是?", "「弓手」是?", "不用了");

                    while (selection != 6)
                    {
                        switch (selection)
                        {
                            case 1:
                                Say(pc, 11000946, 131, "我先说明一下战士系的特征吧!$R;" +
                                                       "$P战士系拥有全面性的超强攻击力!$R;" +
                                                       "$R每个人的方法可能都不一样，$R;" +
                                                       "但即使是单独行动也可以喔!$R;" +
                                                       "$P但是无法学习「知识技能」，$R;" +
                                                       "能拿的行李也不多，$R;" +
                                                       "所以…并不适合采集或搬运呀。$R;" +
                                                       "$R练功时，$R;" +
                                                       "可能需要帮忙搬行李的同伴也不一定。$R;", "剑士前辈");
                                break;

                            case 2:
                                Say(pc, 11000946, 131, "由我来介绍「剑士」吧!$R;" +
                                                       "$P「剑士」是在最前线，$R;" +
                                                       "用剑或斧头攻击敌人的职业呀!!$R;" +
                                                       "$R可以使用双手剑的只有剑士唷!$R;" +
                                                       "$P可以拥有高攻击力和强化的技能。$R;" +
                                                       "$R如果想要连续使用的话，$R;" +
                                                       "我会推荐$R;" +
                                                       "能发挥巨大威力的「居和」系技能!$R;", "剑士前辈");
                                break;

                            case 3:
                                Say(pc, 11000947, 0, "由我来介绍「骑士」吧!$R;" +
                                                     "$P「骑士」是在最前线上，$R;" +
                                                     "用尖锐的剑或长枪，$R;" +
                                                     "来战斗的职业喔!$R;" +
                                                     "$R是唯一可以装备枪类武器的职业呀!$R;" +
                                                     "$P可以学会很多防御高$R;" +
                                                     "与提升防御力相关的技能。$R;" +
                                                     "$P学会可以提升一定防御的$R;" +
                                                     "「重装铠化」也不错呢!$R;" +
                                                     "$R使用这个技能的话，$R;" +
                                                     "即使受到攻击也不会疼啊!$R;", "骑士前辈");
                                break;

                            case 4:
                                Say(pc, 11000948, 0, "由我来介绍「盗贼」吧!$R;" +
                                                     "$P「盗贼」是特殊化攻击力$R;" +
                                                     "和动作迅速的职业喔!$R;" +
                                                     "$R擅长使用投掷武器和短剑，$R;" +
                                                     "而且有强化技能可以学唷!$R;" +
                                                     "$P只要学会隐藏身体的「隐身」技术，$R;" +
                                                     "就可以很简单的回避战斗。$R;", "盗贼前辈");
                                break;

                            case 5:
                                Say(pc, 11000949, 0, "好啊、好啊~!$R;" +
                                                     "由我来介绍「弓手」吧!$R;" +
                                                     "$P「弓手」的特征是用弓箭攻击$R;" +
                                                     "远距离的目标喔!$R;" +
                                                     "$R但是因为防御力有点低，$R;" +
                                                     "魔物多的时候，$R;" +
                                                     "可能会有点棘手。$R;" +
                                                     "$P能强化弓的攻击力的技能也有很多，$R;" +
                                                     "所以不会比其他职业逊色哦!$R;" +
                                                     "$R能把对方打飞的$R;" +
                                                     "「冲击之箭」真是厉害啊!$R;", "弓手前辈");
                                break;
                        }

                        selection = Select(pc, "想听哪种的说明呢?", "", "战士系的特征", "「剑士」是?", "「骑士」是?", "「盗贼」是?", "「弓手」是?", "不用了");
                    }
                    break;

                case 2:
                    break;
            }
        }
    }
}
