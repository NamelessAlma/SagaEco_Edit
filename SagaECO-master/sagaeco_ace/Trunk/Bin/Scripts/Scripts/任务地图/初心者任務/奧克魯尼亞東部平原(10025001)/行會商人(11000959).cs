﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:行會商人(11000959) X:40 Y:135
namespace SagaScript.M10025001
{
    public class S11000959 : Event
    {
        public S11000959()
        {
            this.EventID = 11000959;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000959, 131, "是初心者啊!$R;" +
                                   "我是做保管个人财产生意的人。$R;" +
                                   "$P嗯，我能告诉您的只有两三点…$R;" +
                                   "$P使用道具或技能的时候，$R;" +
                                   "设置在「快捷键视窗」的话，$R;" +
                                   "会比较方便喔!$R;" +
                                   "$P快捷键视窗，$R;" +
                                   "就是「F1」或「F2」旁边，$R;" +
                                   "长长的视窗呀!$R;" +
                                   "$P把道具拉到空格里试试看吧!$R;" +
                                   "就这么简单，快捷键就设置完成了。$R;" +
                                   "$P那么 按「F1」键「F2」键，$R;" +
                                   "就可以使用已设置的道具了。$R;" +
                                   "$R也可以设置$R;" +
                                   "「技能图示」和「表情图示」。$R;" +
                                   "$P实际冒险的时候，一定要使用看看啊!$R;" +
                                   "$P啊! 还有一个!$R;" +
                                   "主视窗右上方的键，已经按过了?$R;" +
                                   "$R按一次看看吧?$R;" +
                                   "$P主视窗的形态会变吧?$R;" +
                                   "$R按右上方的键，$R;" +
                                   "主视窗旁边长长的光条，$R;" +
                                   "就会移到游戏画面的最下方喔!$R;" +
                                   "$P再一次按右边底部的键，$R;" +
                                   "就会回到视窗状态，$R;" +
                                   "按照自己的喜好选择使用吧!$R;", "行会商人");
        }
    }
}
