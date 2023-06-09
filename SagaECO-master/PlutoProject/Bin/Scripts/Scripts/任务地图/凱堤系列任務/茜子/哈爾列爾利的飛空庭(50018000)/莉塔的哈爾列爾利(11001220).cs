﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50018000
{
    public class S11001220 : Event
    {
        public S11001220()
        {
            this.EventID = 11001220;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];

            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.去哈爾列爾利的飛空庭) &&
                !Neko_05_cmask.Test(Neko_05.尋找瑪莎的蹤跡))
            {
                Neko_05_cmask.SetValue(Neko_05.尋找瑪莎的蹤跡, true);
                Say(pc, 11001220, 131, "欢迎来到我的飞空庭！$R;");
                if (CountItem(pc, 10017900) >= 1)
                {
                    Say(pc, 0, 131, "哇啊啊！$R;" +
                        "$R我好想你啊！！茜♪$R;", "猫灵（桃子）");
                }
                if (CountItem(pc, 10017906) >= 1)
                {
                    Say(pc, 0, 131, "茜…?茜啊！?$R;" +
                        "$R很健康吧……$R;", "猫灵（菫子）");
                }
                Say(pc, 0, 131, "跑去做什么了！！怎么那么晚啊！！$R;" +
                    "$R怎么这么慢吞吞勒！$R;" +
                    "$P让这么可爱的女孩子等！！$R是不礼貌的行为！$R;", "猫灵（茜子）");
                Say(pc, 0, 131, "！！！！！！！$R;", "猫灵（除茜以外，全都）");
                Say(pc, 11001220, 131, "啊啊！！真对不起！$R;" +
                    "$R因为飞空庭引擎有点慢了…$R;");
                Say(pc, 0, 131, "不用辩解！$R;" +
                    "$R经常让我自己孤单的待着…$R来客人了…??$R;" +
                    "$R你说！这样都进来了……$R;", "猫灵（茜子）");
                if (CountItem(pc, 10017900) >= 1)
                {
                    Say(pc, 0, 131, "哎！不！那个！就是说…$R茜……?$R;" +
                        "$R什么!!?为什么变了…?$R;", "猫灵（桃子）");
                }
                if (CountItem(pc, 10017906) >= 1)
                {
                    Say(pc, 0, 131, "茜…真的是茜吗……?$R;" +
                        "$R性格完全…不一样啊!!$R;", "凯堤（菫子）");
                }
                if (CountItem(pc, 10017905) >= 1)
                {
                    Say(pc, 0, 131, "…这孩子是茜…?$R;", "猫灵（山吹）");
                }
                Say(pc, 0, 131, "哎呀……是姐姐们!!?$R;" +
                    "$R好久不见阿！$R;", "凯堤（茜子）");
                if (CountItem(pc, 10017900) >= 1 && CountItem(pc, 10017906) >= 1)
                {
                    Say(pc, 0, 131, "啊！啊啊啊！姐姐…怎么办！！？$R茜她！！好像变坏了！！！！！！$R;", "惊慌的 猫灵（桃子）");
                    Say(pc, 0, 131, "哇！不管怎么样！先冷静…桃子！！$R;" +
                        "$R茜变成这样…$R我们只能用姐妹的爱把茜复活过来……$R;", "惊慌的 猫灵（菫子）");
                }
                else if (CountItem(pc, 10017900) >= 1)
                {
                    Say(pc, 0, 131, "啊！啊啊啊！姐姐…怎么办！！？$R茜她！！好像变坏了！！！！！！$R;", "惊慌的 猫灵（桃子）");
                }
                else if (CountItem(pc, 10017906) >= 1)
                {
                    Say(pc, 0, 131, "先冷静下来！！$R我们只能用姐妹的爱把茜复活过来……$R;", "惊慌的 猫灵（菫子）");
                }
                else
                {
                    Say(pc, 0, 131, "$R喂！哈利路亚！！$R;" +
                        "$R把我姐姐放在那边到底想干什么?$R;", "猫灵（茜子）");
                }
                Say(pc, 0, 131, "什么啊?你们什么意思啊?$R;" +
                    "$R喂！哈利路亚！！$R;" +
                    "$R把我姐姐放在那边到底想干什么?$R;", "惊慌的 猫灵（茜子）");
                Say(pc, 11001220, 131, "啊啊！按照所学的在调整引擎阿！$R;" +
                    "$R这次成功的话…$R我的飞空庭就可以顺利地在天空中翱翔$R;" +
                    "$R好了！！！我终于成功啦！！！！$R;" +
                    "$R嘿嘿！装着胆子安装了飞行用涡轮引擎♪$R;");
                PlaySound(pc, 2438, false, 100, 50);
                Wait(pc, 1333);
                PlaySound(pc, 2123, false, 100, 50);
                Wait(pc, 2000);
                Say(pc, 11001220, 131, "哎呀…?$R;");
                PlaySound(pc, 2122, false, 100, 50);
                ShowEffect(pc, 7, 5612);
                Wait(pc, 1000);
                Fade(pc, FadeType.Out, FadeEffect.Black);
                Wait(pc, 4000);
                Fade(pc, FadeType.In, FadeEffect.Black);
                Say(pc, 11001220, 131, "……$R;" +
                    "$P好像失败了……$R;");
                Say(pc, 0, 131, "没关系吗…！?没有受伤吧…?$R;" +
                    "$R有没有不舒服?头没有撞伤吧?$R;" +
                    "$R要不要跟莉塔说啊?$R;", "猫灵（茜）");
                Say(pc, 11001220, 131, "不！我只是被吓到了！$R;" +
                    "$R茜！不用那么担心！$R;");
                Say(pc, 0, 131, "太好了…$R;" +
                    "$P哎呀……！$R;" +
                    "$R这是问…姐姐们的！$R不是问哈利路亚的啦！$R;", "猫灵（茜子）");
                Say(pc, 11001220, 131, "我没关系$R「客人」还好吧？$R;" +
                    "$R嗯…涡轮引擎的设定很难啊$R;" +
                    "$P可能是把引擎弄得太紧了……$R;" +
                    "$R我只是想做一个在阿克罗尼亚$R最快的飞空庭而已啦！！$R;" +
                    "$P是啊！$R;" +
                    "$R也许看看飞空庭工匠写的记录$R说不定有什么新发现！$R;" +
                    "$R或许写了什么重要的啊！！$R;" +
                    "$P阿克罗尼亚最快的飞空庭…$R是行会评议会会长的孙女拥有的…$R;" +
                    "$R对！就是！$R;");
                Say(pc, 0, 131, "啊啊！！$R;");
                Say(pc, 11001220, 131, "怎么了?$R;" +
                    "$R难道……你认识那个人?$R;" +
                    "$R那样的话，带我去找他吧！$R;");
                Say(pc, 0, 131, "等一下！$R这次我也要跟着去！！$R;" +
                    "$R哈利路亚总是失手的$R怎么也不能相信！$R;", "“猫灵（茜子）”");
                Say(pc, 0, 131, "啊啊…真是！看得出来…$R;" +
                    "$R阿克罗波利斯…太…远了…吧$R;" +
                    "$R去下城问路蓝，她可能会知道玛莎在哪里…$R;");
                Warp(pc, 10062000, 110, 88);
                //WARP 1285
                return;
            }
            Say(pc, 11001220, 131, "欢迎来到我的飞空庭！$R;");
        }
    }
}