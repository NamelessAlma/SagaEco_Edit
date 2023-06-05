﻿using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30102000
{
    public class S11000039 : Event
    {
        public S11000039()
        {
            this.EventID = 11000039;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Knights> Knights_mask = pc.CMask["Knights"];

            if (Knights_mask.Test(Knights.加入南軍騎士團))
            {
                if (!Knights_mask.Test(Knights.獲得騎士團皮盔甲) && pc.Fame > 29)
                {
                    Say(pc, 131, "看起来最近做的很认真啊$R;" +
                        "我也听说过了$R;" +
                        "$R给您这个作为奖励吧$R;" +
                        "$P这个是军用盔甲$R;" +
                        "我想您会喜欢的$R;" +
                        "挺帅的！$R;");
                    GiveItem(pc, 60500000, 1);
                    if (CountItem(pc, 60500000) >= 1)
                    {
                        Say(pc, 131, "得到了『南军皮盔甲』$R;");
                        Knights_mask.SetValue(Knights.獲得騎士團皮盔甲, true);
                        return;
                    }
                    Say(pc, 131, "…$R;" +
                        "$P行李太多了，不能给您啊$R;" +
                        "$R请扔掉或者减少道具后，再来吧$R;");
                    return;
                }
                Say(pc, 131, pc.Name + "?$R;" +
                    "来得好！$R;");
                //EVT1100003961
                switch (Select(pc, "怎么办呢？", "", "任务服务台", "什么也不做"))
                {
                    case 1:
                        //GOTO EVT1100003913;
                        break;
                    case 2:
                        Say(pc, 131, "想执行任务吗？$R;");
                        break;
                }
                return;
            }
            if (pc.Fame < 1)
            {
                Say(pc, 131, "我就是$R阿克罗尼亚混成骑士团南军长官$R;" +
                    "我们军队是．．．$R;" +
                    "$P．．．？？$R;" +
                    "$P您到这个城市没有多久吧？$R;" +
                    "$R对吧？$R逃不过我的眼睛的啦$R;" +
                    "$R我们军队虽然招募佣兵。$R但身份不明的人是不需要的$R;" +
                    "您首先在这个城市提高名气后再来吧$R;");
                Knights_mask.SetValue(Knights.告知無法加入南軍, true);
                return;
            }
            if (!Knights_mask.Test(Knights.聽取南軍騎士團說明) && 
                !Knights_mask.Test(Knights.已經加入騎士團))
            {
                Say(pc, 131, "年轻人！$R我是阿克罗尼亚骑士团南团长官$R;" +
                    "格尔尼奥$R;" +
                    "我们骑士团需要您这样的年轻人！$R;");
                switch (Select(pc, "怎么办呢？", "", "加入南军", "听一听关于骑士团的故事", "不听"))
                {
                    case 1:
                        switch (Select(pc, "真的想加入南军吗？", "", "入团", "不入团", "再想一想"))
                        {
                            case 1:
                                Say(pc, 131, "好！$R就是应该这样！$R那么就在这文件上签名吧！$R;");
                                PlaySound(pc, 2030, false, 100, 50);
                                Say(pc, 131, "拿到文件了$R;");
                                switch (Select(pc, "想要签名吗？", "", "签名", "还是放弃"))
                                {
                                    case 1:
                                        Say(pc, 131, "嗯…很好！$R;" +
                                            "$R现在开始$R您是我们阿克罗尼亚混成骑士团南军$R见习军团员啦$R;" +
                                            "$P先收下这个吧$R;");
                                        GiveItem(pc, 10041500, 1);
                                        PlaySound(pc, 4006, false, 100, 50);
                                        Say(pc, 0, 131, "得到$R『艾恩萨乌斯骑士团证』$R;");
                                        Knights_mask.SetValue(Knights.加入南軍騎士團, true);
                                        Say(pc, 131, "这就是您的$R混成骑士团南军团员的证明。$R;" +
                                            "$R同时是艾恩萨乌斯的公民权，$R好好保管吧$R;" +
                                            "$P还有这是阿克罗波利斯的通行证。$R;");
                                        GiveItem(pc, 10042800, 1);
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, 31, "得到$R『阿克罗波利斯通行证』$R;");
                                        Knights_mask.SetValue(Knights.取得上城通行證, true);
                                        Say(pc, 131, "不久我们军队会得到任务的。$R先认真锻炼吧$R;" +
                                            "$R期待您的活跃表现。$R;");
                                        Knights_mask.SetValue(Knights.已經加入騎士團, true);
                                        break;
                                    case 2:
                                        Say(pc, 131, "是吗？…我们军队非常强大啊，$R等什么时候改变想法再来吧。$R;");
                                        break;
                                }
                                break;
                            case 2:
                                Say(pc, 131, "是吗？…我们军队非常强大啊，$R等什么时候改变想法再来吧。$R;");
                                break;
                            case 3:
                                Say(pc, 131, "是吗？那么我会等您的$R;" +
                                    "随时来找我$R;");
                                Knights_mask.SetValue(Knights.考慮加入騎士團, true);
                                break;
                        }
                        break;
                    case 2:
                        Say(pc, 131, "阿克罗尼亚混成骑士团$R是为了守护自由之都：阿克罗波利斯$R;" +
                            "$R由世界的4大国家派遣的联合军，$R;" +
                            "$P南门由我们$R艾恩萨乌斯共和国管辖$R;" +
                            "$R混成骑士团除了我们，$R还有东军、西军、北军一共4个军队$R;" +
                            "最强的当然是我们南军啦。$R;" +
                            "$P而且入团还有特权！$R入团者可以拿到$R艾恩萨乌斯公民权。$R;" +
                            "$R有了公民权可以在$R艾恩萨乌斯自由出入。$R艾恩萨乌斯出产的武器最强！$R;" +
                            "$P那么！加入我们南军？怎么样呢？$R;");
                        Knights_mask.SetValue(Knights.聽取騎士團說明, true);
                        Knights_mask.SetValue(Knights.聽取南軍騎士團說明, true);
                        switch (Select(pc, "想要加入吗？", "", "入团", "不入团", "先想想"))
                        {
                            case 1:
                                Say(pc, 131, "哦！真的入团吗！？$R;");
                                switch (Select(pc, "真的想加入南军吗？", "", "入团", "不入团", "再想一想"))
                                {
                                    case 1:
                                        Say(pc, 131, "好！$R就是应该这样！$R那么就在这文件上签名吧！$R;");
                                        PlaySound(pc, 2030, false, 100, 50);
                                        Say(pc, 131, "拿到文件了$R;");
                                        switch (Select(pc, "想要签名吗？", "", "签名", "还是放弃"))
                                        {
                                            case 1:
                                                Say(pc, 131, "嗯…很好！$R;" +
                                                    "$R现在开始$R您是我们阿克罗波利斯混成骑士团南军$R见习军团员啦$R;" +
                                                    "$P先收下这个吧$R;");
                                                GiveItem(pc, 10041500, 1);
                                                PlaySound(pc, 4006, false, 100, 50);
                                                Say(pc, 0, 131, "得到$R『艾恩萨乌斯骑士团证』$R;");
                                                Knights_mask.SetValue(Knights.加入南軍騎士團, true);
                                                Say(pc, 131, "这就是您的$R混成骑士团南军团员的证明。$R;" +
                                                    "$R同时是艾恩萨乌斯的公民权，$R好好保管吧$R;" +
                                                    "$P还有这是阿克罗波利斯的通行证。$R;");
                                                GiveItem(pc, 10042800, 1);
                                                PlaySound(pc, 2040, false, 100, 50);
                                                Say(pc, 0, 31, "得到$R『阿克罗波利斯通行证』$R;");
                                                Knights_mask.SetValue(Knights.取得上城通行證, true);
                                                Say(pc, 131, "不久我们军队会得到任务的。$R先认真锻炼吧$R;" +
                                                    "$R期待您的活跃表现。$R;");
                                                Knights_mask.SetValue(Knights.已經加入騎士團, true);
                                                break;
                                            case 2:
                                                Say(pc, 131, "是吗？…我们军队非常强大啊，$R等什么时候改变想法再来吧。$R;");
                                                break;
                                        }
                                        break;
                                    case 2:
                                        Say(pc, 131, "是吗？…我们军队非常强大啊，$R等什么时候改变想法再来吧。$R;");
                                        break;
                                    case 3:
                                        Say(pc, 131, "是吗？那么我会等您的$R;" +
                                            "随时来找我$R;");
                                        Knights_mask.SetValue(Knights.考慮加入騎士團, true);
                                        break;
                                }
                                break;
                            case 2:
                                Say(pc, 131, "是吗？…我们军队非常强大啊，$R等什么时候改变想法再来吧。$R;");
                                break;
                            case 3:
                                Say(pc, 131, "是吗？那么我会等您的$R;" +
                                    "随时来找我$R;");
                                Knights_mask.SetValue(Knights.考慮加入騎士團, true);
                                break;
                        }
                        break;
                    case 3:
                        Say(pc, 131, "是吗？…我们军队非常强大啊，$R等什么时候改变想法再来吧。$R;");
                        break;
                }
                return;
            }
            if (!Knights_mask.Test(Knights.已經加入騎士團) &&
                Knights_mask.Test(Knights.聽取南軍騎士團說明))
            {
                Say(pc, 131, "原来是您啊$R;" +
                    "怎么样？$R是不是想参加我们的骑士团了？$R;");
                switch (Select(pc, "想要加入吗？", "", "入团", "不入团", "先想想"))
                {
                    case 1:
                        Say(pc, 131, "哦！真的入团吗！？$R;");
                        switch (Select(pc, "真的想加入南军吗？", "", "入团", "不入团", "再想一想"))
                        {
                            case 1:
                                Say(pc, 131, "好！$R就是应该这样！$R那么就在这文件上签名吧！$R;");
                                PlaySound(pc, 2030, false, 100, 50);
                                Say(pc, 131, "拿到文件了$R;");
                                switch (Select(pc, "想要签名吗？", "", "签名", "还是放弃"))
                                {
                                    case 1:
                                        Say(pc, 131, "嗯…很好！$R;" +
                                            "$R现在开始$R您是我们阿克罗尼亚混成骑士团南军$R见习军团员啦$R;" +
                                            "$P先收下这个吧$R;");
                                        GiveItem(pc, 10041500, 1);
                                        PlaySound(pc, 4006, false, 100, 50);
                                        Say(pc, 0, 131, "得到$R『艾恩萨乌斯骑士团证』$R;");
                                        Knights_mask.SetValue(Knights.加入南軍騎士團, true);
                                        Say(pc, 131, "这就是您的$R混成骑士团南军团员的证明。$R;" +
                                            "$R同时是艾恩萨乌斯的公民权，$R好好保管吧$R;" +
                                            "$P还有这是阿克罗波利斯的通行证。$R;");
                                        GiveItem(pc, 10042800, 1);
                                        PlaySound(pc, 2040, false, 100, 50);
                                        Say(pc, 0, 31, "得到$R『阿克罗波利斯通行证』$R;");
                                        Knights_mask.SetValue(Knights.取得上城通行證, true);
                                        Say(pc, 131, "不久我们军队会得到任务的。$R先认真锻炼吧$R;" +
                                            "$R期待您的活跃表现。$R;");
                                        Knights_mask.SetValue(Knights.已經加入騎士團, true);
                                        break;
                                    case 2:
                                        Say(pc, 131, "是吗？…我们军队非常强大啊，$R等什么时候改变想法再来吧。$R;");
                                        break;
                                }
                                break;
                            case 2:
                                Say(pc, 131, "是吗？…我们军队非常强大啊，$R等什么时候改变想法再来吧。$R;");
                                break;
                            case 3:
                                Say(pc, 131, "是吗？那么我会等您的$R;" +
                                    "随时来找我$R;");
                                Knights_mask.SetValue(Knights.考慮加入騎士團, true);
                                break;
                        }
                        break;
                    case 2:
                        Say(pc, 131, "是吗？…我们军队非常强大啊，$R等什么时候改变想法再来吧。$R;");
                        break;
                    case 3:
                        Say(pc, 131, "是吗？那么我会等您的$R;" +
                            "随时来找我$R;");
                        Knights_mask.SetValue(Knights.考慮加入騎士團, true);
                        break;
                }
                return;
            }
            Say(pc, 131, "我就是$R阿克罗尼亚混成骑士团南军长官$R;" +
                "我们是军队中最强大的。$R;");
        }
    }
}