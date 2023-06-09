using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

namespace SagaScript.M10023000
{

    public class S80000011 : Event
    {
        public S80000011()
        {
            this.EventID = 80000011;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 50002011, 131, "木偶状态不能更换发型哦。$R;");
                return;
            }

            Say(pc, 50002011, 131, "你壕啊!$R;" +
                                   "我是发型师。$R;" +
                                   "$P我可以帮您做出各种各样的发型，$R;" +
                                   "当然，需要您提供对应的发型券啦♪$R;", "发型师");

            Say(pc, 50002011, 131, "由于某位大大又发话了$R;" +
                       "现在开始我将收取你的发型卷！$R;" +
                       "而且我还需要收取你30,000G$R;" +
                       "作为报酬哦♪$R$R;", "发型师");

            int fee = 30000;
            List<uint> htlist = new List<uint>();
            List<string> htnames = new List<string>();
            List<HairStyleList> hslist = new List<HairStyleList>();
            List<string> hsnames = new List<string>();
            int count = 0;

            if (pc != null)
            {
                htlist = gethairlist(pc);
                count = htlist.Count;

                if (count > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        htnames.Add((ItemFactory.Instance.GetItem(htlist[i])).BaseData.name);
                    }
                    string s = "放弃";
                    htnames.Add(s);

                    int sel = Select(pc, "选哪一个发型券呢?", "", htnames.ToArray());
                    if (sel == htlist.Count + 1)
                    {
                        //give up
                        Say(pc, 50002011, 131, "……$R;" +
                                   "$P还没有决定吗?$R;" +
                                   "真是遗憾啊♪$R;", "发型师");
                        return;

                    }
                    else
                    {
                        hslist = gethairstyles(pc, htlist[sel - 1]);
                        for (int i = 0; i < hslist.Count; i++)
                        {
                            hsnames.Add(hslist[i].stylename);
                        }
                        hsnames.Add(s);

                        int sel2 = Select(pc, "选哪一个发型呢?", "", hsnames.ToArray());
                        if (sel2 == hslist.Count + 1)
                        {
                            //give up
                            Say(pc, 50002011, 131, "……$R;" +
                                       "$P还没有决定吗?$R;" +
                                       "真是遗憾啊♪$R;", "发型师");
                            return;
                        }
                        else
                        {

                            Say(pc, 131, "作为报酬,$R需要收取您" + fee.ToString() + "G的手续费哦。$R$R是否真的要更变呢？", "发型师");
                            switch (Select(pc, "需要支付" + fee.ToString() + "G才能改变发型", "", "是的，我想改变发型", "我想只改变主发", "我想只改变副发", "我想...变成光头（只限男性）", "还是算了"))
                            {
                                case 1:
                                    if (pc.Gold >= fee && CountItem(pc, htlist[sel - 1]) > 0)
                                    {
                                        TakeItem(pc, htlist[sel - 1], 1);
                                        pc.Gold -= fee;
                                        pc.HairStyle = hslist[sel2 - 1].hairstyle;
                                        pc.Wig = hslist[sel2 - 1].wig;
                                        Say(pc, 50002011, 131, hslist[sel2 - 1].sayafter, "发型师");
                                    }
                                    else
                                    {
                                        Say(pc, 131, "……$R真是遗憾呀$R$R你似乎没带够钱呢$P欢迎下次再来啦", "发型师");
                                        return;
                                    }
                                    break;
                                case 2:
                                    if (pc.Gold >= fee && CountItem(pc, htlist[sel - 1]) > 0)
                                    {
                                        TakeItem(pc, htlist[sel - 1], 1);
                                        pc.Gold -= fee;
                                        pc.HairStyle = hslist[sel2 - 1].hairstyle;
                                        Say(pc, 50002011, 131, hslist[sel2 - 1].sayafter, "发型师");
                                    }
                                    else
                                    {
                                        Say(pc, 131, "……$R真是遗憾呀$R$R你似乎没带够钱呢$P欢迎下次再来啦", "发型师");
                                        return;
                                    }
                                    break;
                                case 3:
                                    if (pc.Gold >= fee && CountItem(pc, htlist[sel - 1]) > 0)
                                    {
                                        TakeItem(pc, htlist[sel - 1], 1);
                                        pc.Gold -= fee;
                                        pc.Wig = hslist[sel2 - 1].wig;
                                        Say(pc, 50002011, 131, hslist[sel2 - 1].sayafter, "发型师");
                                    }
                                    else
                                    {
                                        Say(pc, 131, "……$R真是遗憾呀$R$R你似乎没带够钱呢$P欢迎下次再来啦", "发型师");
                                        return;
                                    }
                                    break;
                                case 4:
                                    if (pc.Gender != PC_GENDER.MALE) { return; }

                                    Say(pc, 131, "真的吗！？$R你想把头发全部剃光吗！！？？", "发型师");

                                    switch (Select(pc, "真的要剃光头吗", "", "是的！", "不是的！"))
                                    {
                                        case 1:
                                            pc.HairStyle = 7;
                                            pc.Wig = 999;
                                            Say(pc, 131, "……$R$R这次..就不收你钱啦...", "发型师");
                                            break;
                                        case 2:
                                            Say(pc, 131, "真遗憾呐...$R本来还想免费给你剃头的", "发型师");
                                            break;
                                    }
                                    break;
                            }

                        }
                    }
                    return;
                }
                Say(pc, 50002011, 131, "……$R;" +
                                  "$P不过您现在好像没有带着发型券呢?$R;" +
                                  "真是遗憾啊♪$R;", "发型师");
            }
            return;
        }
    }
}
