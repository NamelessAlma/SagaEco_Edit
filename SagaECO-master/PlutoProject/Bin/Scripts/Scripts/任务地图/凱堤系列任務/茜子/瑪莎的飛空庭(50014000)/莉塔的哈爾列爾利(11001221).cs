﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50014000
{
    public class S11001221 : Event
    {
        public S11001221()
        {
            this.EventID = 11001221;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.向瑪莎詢問飛空庭引擎的相關情報) &&
                !Neko_05_cmask.Test(Neko_05.了解玛莎和埃米尔的关系))
            {
                Neko_05_cmask.SetValue(Neko_05.了解玛莎和埃米尔的关系, true);
                Say(pc, 11001221, 131, "埃米尔，你有飞空庭吗?$R;");
                Say(pc, 11001227, 131, "现在没有！$R;" +
                    "$R虽然以前可能有$R;");
                Say(pc, 11001221, 131, "???$R;");
                Say(pc, 11001227, 131, "其实是…我…记不起以前的事了$R;" +
                    "$R从我受到重伤的时候开始，$R我的记忆就丧失了…$R;" +
                    "$R在我复原之前，一直都是玛莎照顾着我的…$R;");
                Say(pc, 11001221, 131, "这样啊，我想一定很累吧！$R;");
                Say(pc, 11001227, 131, "啊！但是最近慢慢……$R;" +
                    "$P一直保护着我的…温柔的…白色翅膀$R;" +
                    "$R闪耀的金色长发…$R;" +
                    "$R慈祥但带点忧郁的眼睛…$R;" +
                    "$P那孩子到底是谁呢…$R;" +
                    "$R总是看着我，流露出一丝丝微笑…$R;");
                Say(pc, 11001226, 131, "……$R;" +
                    "$P别急着…想起以前的事情喔！$R;");
                Say(pc, 11001227, 131, "为什么?$R;");
                Say(pc, 11001226, 131, "因为！！$R;");
                if (CountItem(pc, 10017900) >= 1 && CountItem(pc, 10017903) >= 1 && CountItem(pc, 10017905) >= 1)
                {
                    Say(pc, 0, 131, "……$R;" +
                        "$R是三角关系吗?$R;", "猫灵（桃子）");
                    对话1(pc);
                    return;
                }
                if (CountItem(pc, 10017900) >= 1 && CountItem(pc, 10017903) >= 1)
                {
                    Say(pc, 0, 131, "…$R;" +
                        "$R是三角关系吗?$R;", "猫灵（桃子）");
                    Say(pc, 0, 131, "埃米尔记忆中的女性一定是泰达尼亚！$R这是命运中注定的爱情！$R;" +
                        "$R哇喔！太浪漫了！！$R;" +
                        "$P如果埃米尔找回记忆会选择哪一边呢?$R;", "猫灵（蓝子）");
                    Say(pc, 0, 131, "会不会…是玛莎啊?$R;" +
                        "$R她一直陪伴在埃米尔身边$R;", "猫灵（桃子）");
                    if (CountItem(pc, 10017906) == 0)
                    {
                        return;
                    }
                    Say(pc, 0, 131, "我不这么想！$R因为这是神安排的爱情考验！$R;" +
                        "$R埃米尔会根据记忆选择泰达尼亚女生的$R;" +
                        "$P菫姐姐，你也这么想吗?$R;", "猫灵（蓝子）");
                    Say(pc, 0, 131, "好了…大家都不要说别人了$R;", "猫灵（菫子）");
                    Say(pc, 0, 131, "哦……$R;", "猫灵（桃子）");
                    对话2(pc);
                    return;
                }
                if (CountItem(pc, 10017900) >= 1 && CountItem(pc, 10017905) >= 1)
                {
                    Say(pc, 0, 131, "……$R;" +
                        "$R好像是三角关系吧?$R;" +
                        "$R埃米尔记得的女生好像是泰达尼亚啊?$R;", "猫灵（桃子）");
                    Say(pc, 0, 131, "$P如果埃米尔找到记忆$R会选择哪一边呢?$R;", "猫灵（山吹）");
                    Say(pc, 0, 131, "会不会…是玛莎啊?$R;" +
                        "$R她一直陪伴在埃米尔身边$R;", "猫灵（桃子）");
                    Say(pc, 0, 131, "我也这么想，应该是玛莎吧！$R;" +
                        "$R胸也够大啊$R;", "猫灵（山吹）");
                    if (CountItem(pc, 10017906) == 0)
                    {
                        Say(pc, 0, 131, "真的吗！！?山吹…！$R;" +
                            "$R胸大就可以了嘛！！$R;", "猫灵（桃子）");
                        对话2(pc);
                        return;
                    }
                    Say(pc, 0, 131, "真是的！！$R才不会…因为…这个…做选择(脸红)$R;" +
                        "$R绝对不会因为胸大才受喜爱的！！！$R;", "猫灵（菫子）");
                    Say(pc, 0, 131, "呵…$R;" +
                        "$R菫姐姐…为什么突然…那么激动啊?$R;", "凯堤(山吹)");
                    Say(pc, 0, 131, "啊…那个…哈哈$R;" +
                        "$R来来！不要做无谓的讨论了！$R;", "猫灵（菫子）");
                    Say(pc, 0, 131, "哦……$R;", "猫灵（桃子+山吹）");
                    对话2(pc);
                    return;
                }
                if (CountItem(pc, 10017903) >= 1 && CountItem(pc, 10017905) >= 1)
                {
                    Say(pc, 0, 131, "……$R;" +
                        "$R是三角关系吗?$R;", "猫灵（山吹）");
                    对话1(pc);
                    return;
                }
                if (CountItem(pc, 10017900) >= 1)
                {
                    Say(pc, 0, 131, "……$R;" +
                        "$R这里好像有三角关系！$R茜也这么想吧?$R;", "猫灵（桃子）");
                    Say(pc, 0, 131, "跟我没关系！$R;", "猫灵（茜子）");
                    Say(pc, 0, 131, "那倒是，但是…$R;", "猫灵（桃子）");
                    对话2(pc);
                    return;
                }
                if (CountItem(pc, 10017903) >= 1)
                {
                    Say(pc, 0, 131, "……$R;" +
                        "埃米尔记忆中的女性一定是泰达尼亚！$R这是命运中注定的爱情！$R;" +
                        "$R哇喔！太浪漫了！！$R;", "猫灵（蓝子）");
                    Say(pc, 0, 131, "蓝子姐姐一点都没变喔$R还是有这么丰富的想像力…哈哈…$R;", "猫灵（茜子）");
                    对话2(pc);
                    return;
                }
                if (CountItem(pc, 10017905) >= 1)
                {
                    Say(pc, 0, 131, "……$R;" +
                        "$R是三角关系吗?$R;", "猫灵（山吹）");
                    Say(pc, 0, 131, "跟我没关系！$R;", "猫灵（茜子）");
                    Say(pc, 0, 131, "那倒是！$R三角关系又不能挣钱$R;", "猫灵（山吹）");
                    对话2(pc);
                    return;
                }
                if (CountItem(pc, 10017906) >= 1)
                {
                    Say(pc, 0, 131, "……$R;" +
                        "$R(难道是三角关系?)$R;", "猫灵（菫子）");
                    Say(pc, 0, 131, "……$R;" +
                        "$R(三角关系??)$R;", "猫灵（茜子）");
                    return;
                }
                if (CountItem(pc, 10017902) >= 1)
                {
                    Say(pc, 0, 131, "……$R;" +
                        "$R三角关系…$R;", "猫灵（绿子）");
                    Say(pc, 0, 131, "我不可能跟哈利路亚有关系的！$R;", "猫灵（茜子）");
                    Say(pc, 0, 131, "跟主人也…不会有关系的！$R;" +
                        "$R只有我…什么都不是…唉$R;", "猫灵（绿子）");
                    return;
                }
                return;
            }
            Say(pc, 11001221, 131, "这个就是玛莎的…冰之飞空庭！$R;" +
                "$R真的是很漂亮的飞空庭啊♪$R;");
            Say(pc, 11001226, 131, "冰是……$R;");
            Say(pc, 0, 131, "……$R;" +
                "$P给叫成「客人」不是好很多吗……$R;");
        }

        void 对话1(ActorPC pc)
        {
            Say(pc, 0, 131, "埃米尔记忆中的女性一定是泰达尼亚！$R这是命运中注定的爱情！$R;" +
                "$R哇喔！太浪漫了！！$R;" +
                "$P如果埃米尔找回记忆会选择哪一边呢?$R;", "猫灵（蓝子）");
            Say(pc, 0, 131, "我觉得会选择这个女孩！$R;", "猫灵（山吹）");
            Say(pc, 0, 131, "听我说！$R这是神安排的爱情考验！$R;" +
                "$R埃米尔会根据记忆选择泰达尼亚女生的$R;", "猫灵（蓝子）");
            if (CountItem(pc, 10017906) == 0)
            {
                Say(pc, 0, 131, "不是啊！$R;" +
                    "$R因为这女生，胸又大，钱又多喔！$R;", "猫灵（山吹）");
                if (CountItem(pc, 10017900) >= 1)
                {
                    Say(pc, 0, 131, "真的吗！！?山吹…！$R;" +
                        "$R胸大就可以了嘛！！$R;", "猫灵（桃子）");
                }
                对话2(pc);
                return;
            }
            Say(pc, 0, 131, "不是这样的！$R;" +
                "$R这女生的不是很富有吗！$R不是吗?蓝子姐姐?$R;", "猫灵（山吹）");
            Say(pc, 0, 131, "当然！经济条件确实也很重要…$R;", "猫灵（菫子）");
            Say(pc, 0, 131, "对吧?所以我想会选择这边的！$R;" +
                "$R而且胸也够大吧！$R;", "猫灵（山吹）");
            Say(pc, 0, 131, "真是的！！$R才不会…因为…这个…做选择(脸红)$R;" +
                "$R绝对不会因为胸大才受喜爱的！！！$R;", "猫灵（菫子）");
            Say(pc, 0, 131, "呵…$R;" +
                "$R菫姐姐…为什么突然…那么激动啊?$R;", "猫灵（山吹）");
            if (CountItem(pc, 10017902) >= 1)
            {
                Say(pc, 0, 131, "……$R;", "猫灵（绿子）");
                Say(pc, 0, 131, "绿姐姐怎么什么都不说?$R;" +
                    "$R难道绿姐姐也觉得…$R埃米尔会选择这个人?$R;", "猫灵（山吹）");
                Say(pc, 0, 131, "真是的！！$R埃米尔一定会选择泰达尼亚女生！$R;" +
                    "$R绿姐姐，你也这么想的吧?$R;", "猫灵（蓝子）");
                Say(pc, 0, 131, "……$R;" +
                    "$P是啊…真的要选择的话…还是两位都?$R;", "猫灵（绿子）");
                Say(pc, 0, 131, "……$R;", "猫灵（除了绿以外，全都）");
                if (CountItem(pc, 10017900) == 0)
                {
                    return;
                }
                Say(pc, 0, 131, "嗯……$R;" +
                    "$R好了好了！到此为止吧！$R;" +
                    "$R嗯…?桃子是怎么了?$R;", "猫灵（菫子）");
                Say(pc, 0, 131, "姐姐…我…肚子疼…$R;", "猫灵（桃子）");
                return;
            }
            Say(pc, 0, 131, "啊…那个…哈哈$R;" +
                "$R来来！不要做无谓的讨论了！$R;", "猫灵（菫子）");
        }

        void 对话2(ActorPC pc)
        {
            if (CountItem(pc, 10017902) >= 1)
            {
                if (CountItem(pc, 10017905) >= 1)
                {
                    Say(pc, 0, 131, "埃米尔他…找到记忆后会选择谁啊?$R;" +
                        "$R绿姐姐，你也那么想的吧?$R;", "猫灵（山吹）");
                }
                else if (CountItem(pc, 10017903) >= 1)
                {
                    Say(pc, 0, 131, "姐姐…绿姐姐！$R;" +
                        "$R埃米尔他…找到记忆后会选择谁啊?$R;", "猫灵（蓝子）");
                }
                else if (CountItem(pc, 10017900) >= 1)
                {
                    Say(pc, 0, 131, "埃米尔他…找到记忆后会去哪边呢…$R;" +
                        "$R绿姐姐，你怎么想啊?$R;", "猫灵（桃子）");
                }
                Say(pc, 0, 131, "……$R;" +
                    "$P是啊…选择的话…会不会两个都?$R;", "猫灵（绿子）");
                Say(pc, 0, 131, "……$R;", "猫灵（绿以外，全都）");
                return;
            } 
        }
    }
}