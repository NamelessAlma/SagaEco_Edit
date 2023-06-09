using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:黑哈因髮型屋(11000411) X:103 Y:74		JP:ヘルヘイム
namespace SagaScript.M10023000
{

    public class S11000411 : Event
    {

        public class HairStyleList
        {
            public uint id;
            public string name;
        }
        private uint[] id = {
//					  10020750,	//ヘアサロン特別
//					  10020759,	//ルチル
					  10020708,	//ヘアサロン
					  10020706,	//セレブ
					  10020755,	//ドミニオン
					  10020753,	//アニキ
					  10020761,	//ちょいかわ
					  10020764,	//ゴージャス
					  10020765,	//おてんば
					  10020766,	//おめかし
					  10020767,	//姫様
					  10020768,	//ラブモテ
					  10020769,	//プリンセス
					  10020770,	//クラシック
//					  10020771,	//少佐
					  10020773,	//ふたご座
					  10020760,	//機械時代
					  10020774,	//ラテン
					  10020775,	//魔女
					  10020776,	//ワイルド
					  10020777,	//武家
					  10020779,	//バレンタイン
					  10020780, //デビュー
					  10020782,	//スイート
					  10020783,	//おとぎ
					  10020784,	//歌姫
					  10020785,	//マネージャー
//					  10020786,	//ひぐらし
//					  10020787,	//空賊
					  10020788,	//真夏
					  10020789,	//委員長
//					  10020790,	//貴族
//					  10020791,	//レナ
					  10020792,	//ハロウィン
					  10020793,	//真冬
					  10020794,	//ハイカラ
					  10020795,	//アルバイト
					  10020796,	//軍式
					  10020797,	//伝説
					  10074400,	//愛され
					  10074800,	//夏休み
					  10075200,	//ふたご座☆
					  10075201,	//ロビン・グッドフェロウ
					  10075202,	//厳島 美晴
					  10075203,	//アングリー
					  10075204,	//コウモリタ
					  10075205,	//ハングリー
					  10075206,	//西洋ロマン
					  10075267	//ムツキ
					};

        private List<HairStyleList> stylelist;
        private List<String> stylenames;

        public S11000411()
        {
            this.EventID = 11000411;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Marionette != null)
            {
                Say(pc, 11000411, 131, "マリオネットには興味ないの、$R;" +
                                       "あっち行きなさいよ～。$R;");
                return;
            }

            if (pc.Gender == PC_GENDER.FEMALE)
            {
                Say(pc, 11000411, 131, "アタシ、女には興味ないの。$R;" +
                                       "あっち行きなさいよ～。$R;");
                return;
            }

            Say(pc, 11000411, 131, "あらぁん、こんにちは!$R;" +
                                   "ヘルヘイムのヘアサロンへようこそ。$R;" +
                                   "$Pエクステをセットすると$R;" +
                                   "今のエクステがなくなっちゃうから$R;" +
                                   "気をつけてね♪$R;", "ヘルヘイムのヘアサロン");


            SagaDB.Item.Item item;
            int count = 0;
            stylelist = new List<HairStyleList>();
            stylenames = new List<String>();
            for (uint i = 0; i < id.Length; i++)
            {
                if (CountItem(pc, id[i]) > 0)
                {
                    item = pc.Inventory.GetItem(id[i], Inventory.SearchType.ITEM_ID);
                    if (item == null)
                    {
                        continue;
                    }
                    HairStyleList style = new HairStyleList();
                    style.id = id[i];
                    style.name = item.BaseData.name;
                    stylelist.Add(style);
                    count++;
                }
            }
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    stylenames.Add(stylelist[i].name);
                }
                string s = "やめる";
                stylenames.Add(s);

                HairStyleList style = new HairStyleList();
                style.id = 0;
                stylelist.Add(style);
                使用髮型介紹書(pc);
                return;
            }

            Say(pc, 11000411, 131, "$P……$R;" +
                                   "$Pあーん、紹介状を持っていないのね?$R;" +
                                   "残念だわ♪$R;", "ヘルヘイムのヘアサロン");
        }

        private void Start(ActorPC pc)
        {
            Say(pc, 11000411, 131, "じゃ、はじめるわよ♪$R;", "ヘルヘイムのヘアサロン");
        }

        private void Complete(ActorPC pc)
        {
            Say(pc, 11000411, 131, "どうかしら!$R;" +
                                   "$Pわ～!!$R;" +
                                   "素敵よ! 　よく似合ってる!$R;" +
                                   "またきてね♪$R;", "ヘルヘイムのヘアサロン");
        }

        private void Complete2(ActorPC pc)
        {
            Say(pc, 11000411, 131, "は～い、できたわ!$R;" +
                                   "$Rいいわ!　 よく似合ってる～!$R;" +
                                   "$P髪型変えたらなくなっちゃうから$R;" +
                                   "気をつけてね♪～$R;");
        }

        private void Cancel(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あらぁ、残念ね。$R;" +
                           "髪型を変えたくなったら、$R;" +
                           "また来てね♪～$R;", "ヘルヘイムのヘアサロン");
        }

        void 使用髮型介紹書(ActorPC pc)
        {
            Say(pc, 11000411, 131, "さあ、どんな紹介状があるのかしら?$R;", "ヘルヘイムのヘアサロン");

            if (pc.HairStyle == 7)
            {
                Say(pc, 11000411, 131, "……！$R;" +
                                       "$Pそぉんな頭じゃ何も出来ないわ!$R;" +
                                       "髪が伸びたら、またきてね♪～$R;");
                return;
            }

            int sel = Select(pc, "どの紹介状を使う?", "", stylenames.ToArray());
            switch (stylelist[sel - 1].id)
            {
                case 10020708:
                    ヘアサロンの紹介状(pc);
                    break;

                case 10020706:
                    セレブの紹介状(pc);
                    break;

                case 10020764:
                    ゴージャス紹介状(pc);
                    break;

                case 10020766:
                    おめかし紹介状(pc);
                    break;

                case 10020765:
                    おてんば紹介状(pc);
                    break;

                case 10020767:
                    姫さまの紹介状(pc);
                    break;

                case 10020761:
                    ちょいかわ紹介状(pc);
                    break;

                case 10020753:
                    アニキの紹介状(pc);
                    break;

                case 10020760:
                    機械時代のヘアカタログ(pc);
                    break;

                case 10020755:
                    ドミニオンのヘアカタログ(pc);
                    break;

                case 10020768:	//ラブモテ
                    ラブモテ紹介状(pc);
                    break;

                case 10020769:	//プリンセス
                    プリンセスの紹介状(pc);
                    break;
                case 10020770:	//クラシック
                    クラシックな紹介状(pc);
                    break;
                case 10020771:	//少佐
                    少佐の紹介状(pc);
                    break;
                case 10020773:	//ふたご座
                    ふたご座の紹介状(pc);
                    break;
                case 10020774:	//ラテン
                    ラテンの紹介状(pc);
                    break;
                case 10020775:	//魔女
                    魔女の紹介状(pc);
                    break;
                case 10020776:	//ワイルド
                    ワイルドな紹介状(pc);
                    break;
                case 10020777:	//武家
                    武家の紹介状(pc);
                    break;
                case 10020779:	//バレンタイン
                    バレンタイン紹介状(pc);
                    break;
                case 10020780:	//デビュー
                    デビューへの紹介状(pc);
                    break;
                case 10020782:	//スイート
                    スイートな紹介状(pc);
                    break;
                case 10020783:	//おとぎ
                    おとぎの紹介状(pc);
                    break;
                case 10020784:	//歌姫
                    歌姫の紹介状(pc);
                    break;
                case 10020785:	//マネージャー
                    マネージャーの紹介状(pc);
                    break;
                case 10020786:	//ひぐらし
                    ひぐらしの紹介状(pc);
                    break;
                case 10020787:	//空賊
                    空賊の紹介状(pc);
                    break;
                case 10020788:	//真夏
                    真夏の紹介状(pc);
                    break;
                case 10020789:	//委員長
                    委員長の紹介状(pc);
                    break;
                case 10020790:	//貴族
                    貴族の紹介状(pc);
                    break;
                case 10020791:	//レナ
                    レナの紹介状(pc);
                    break;
                case 10020792:	//ハロウィン
                    ハロウィン紹介状(pc);
                    break;
                case 10020793:	//真冬
                    真冬の紹介状(pc);
                    break;
                case 10020794:	//ハイカラ
                    ハイカラ紹介状(pc);
                    break;
                case 10020795:	//アルバイト
                    アルバイト紹介状(pc);
                    break;
                case 10020796:	//軍式
                    軍式紹介状(pc);
                    break;
                case 10020797:	//伝説
                    伝説の紹介状(pc);
                    break;
                case 10074400:	//愛され
                    愛され紹介状(pc);
                    break;
                case 10074800:	//夏休み
                    夏休みの紹介状(pc);
                    break;
                case 10075200:	//ふたご座☆
                    ふたご座の紹介状2(pc);
                    break;
                case 10075201:	//ロビン・グッドフェロウ
                    ロビン・グッドフェロウの紹介状(pc);
                    break;
                case 10075202:	//厳島 美晴
                    厳島美晴の紹介状(pc);
                    break;
                case 10075203:	//アングリー
                    アングリー紹介状(pc);
                    break;
                case 10075204:	//コウモリタ
                    コウモリタ紹介状(pc);
                    break;
                case 10075205:	//ハングリー
                    ハングリー紹介状(pc);
                    break;
                case 10075206:	//西洋ロマン
                    西洋ロマンの紹介状(pc);
                    break;

                case 0:
                    Say(pc, 11000411, 131, "あらそう? 残念ねぇ。$R;" +
                                           "髪型を変えたくなったら、$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }


        }

        void ヘアサロンの紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ!!$R;" +
                                   "それは『ヘアサロンの紹介状』ね?$R;" +
                                   "$Pあなたにエクステを$R;" +
                                   "つけてあげられるわ!$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "『ヘアサロンの紹介状』を使う", "使わない"))
            {
                case 1:
                    if (pc.HairStyle == 11 ||
                        pc.HairStyle == 12)
                    //？　　pc.HairStyle == 20)	// スポーティーポニー  
                    {
                        破壞髮型(pc);
                        return;
                    }

                    byte wig = 255;
                    if (pc.HairStyle == 1 ||
                        pc.HairStyle == 3 ||
                        pc.HairStyle == 6 ||
                        pc.HairStyle == 13 ||
                        pc.HairStyle == 19 ||
                        pc.HairStyle == 20 ||
                        pc.HairStyle == 40 ||
                        pc.HairStyle == 14)
                    {
                        switch (Select(pc, "どんなエクステにする?", "", "一つゴムエクステダウン", "二つゴムエクステダウン", "三つ編みエクステダウン", "三つ編みエクステサイド", "きつい三つ編みエクステダウン", "きつい三つ編みエクステサイド", "やめる"))
                        {
                            case 1:
                                wig = 6;
                                break;
                            case 2:
                                wig = 9;
                                break;
                            case 3:
                                wig = 0;
                                break;
                            case 4:
                                wig = 2;
                                break;
                            case 5:
                                wig = 12;
                                break;
                            case 6:
                                wig = 14;
                                break;
                        }
                    }

                    if (pc.HairStyle == 0 ||
                        pc.HairStyle == 2 ||
                        pc.HairStyle == 5 ||
                        pc.HairStyle == 8)
                    {
                        switch (Select(pc, "どんなエクステにする?", "", "一つゴムエクステダウン", "二つゴムエクステダウン", "三つ編みエクステサイド", "きつい三つ編みエクステサイド", "やめる"))
                        {
                            case 1:
                                wig = 6;
                                break;
                            case 2:
                                wig = 9;
                                break;
                            case 3:
                                wig = 2;
                                break;
                            case 4:
                                wig = 14;
                                break;
                        }
                    }

                    if (pc.HairStyle == 4)
                    {
                        switch (Select(pc, "どんなエクステにする?", "", "三つ編みエクステサイド", "きつい三つ編みエクステサイド", "やめる"))
                        {
                            case 1:
                                wig = 2;
                                break;
                            case 2:
                                wig = 14;
                                break;
                        }
                    }
                    if (wig < 255)
                    {
                        Start(pc);

                        Wait(pc, 1000);
                        pc.Wig = wig;
                        ShowEffect(pc, 4112);
                        TakeItem(pc, 10020708, 1);

                        Wait(pc, 1000);

                        Complete2(pc);
                    }
                    else
                    {
                        Say(pc, 11000411, 131, "あらぁ、残念だけど$R;" +
                                               "今の髪型には$R;" +
                                               "つけられないわね!$R;", "ヘルヘイムのヘアサロン");
                    }
                    return;
                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void セレブの紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ、『セレブの紹介状』ね?$R;" +
                                   "弟からかしら?$R;" +
                                   "$Rいいわ、好みのエクステを$R;" +
                                   "つけてあげる♪$R;");

            switch (Select(pc, "どうする?", "", "セレブの紹介状を使う", "使わない"))
            {
                case 1:
                    if (pc.HairStyle == 1 ||
                        pc.HairStyle == 3 ||
                        pc.HairStyle == 6 ||
                        pc.HairStyle == 13 ||
                        pc.HairStyle == 14)
                    {
                        名人介紹書第一種(pc);
                        return;
                    }

                    if (pc.HairStyle == 0 ||
                        pc.HairStyle == 2 ||
                        pc.HairStyle == 5 ||
                        pc.HairStyle == 8)
                    {
                        名人介紹書第二種(pc);
                        return;
                    }

                    if (pc.HairStyle == 4)
                    {
                        名人介紹書第三種(pc);
                        return;
                    }

                    if (pc.HairStyle == 7)
                    {
                        名人介紹書第四種(pc);
                        return;
                    }

                    if (pc.HairStyle == 11 ||
                        pc.HairStyle == 12)
                    {
                        破壞髮型(pc);
                        return;
                    }
                    if (pc.HairStyle == 13 ||
                        pc.HairStyle == 19 ||
                        pc.HairStyle == 14 ||
                        pc.HairStyle == 23 ||
                        pc.HairStyle == 20 ||
                        pc.HairStyle == 40)
                    {
                        名人介紹書第一種(pc);
                    }

                    Say(pc, 11000411, 131, "あ～ん、その髪型には$R;" +
                                           "つけられないわね～!$R;");
                    break;

                case 2:
                    break;
            }
        }

        void セレブStart(ActorPC pc)
        {
            Say(pc, 11000411, 131, "始めるわよ!$R;" +
                                   "$Rふふ、あなたの髪はいい触り心地ね!$R;" +
                                   "期待してね♪$R;");
        }

        void セレブComplete(ActorPC pc)
        {
            Say(pc, 11000411, 131, "はい、できあがり!$R;" +
                                   "$Rいいわ～! よく似合ってる!$R;" +
                                   "$Pあら、ほこりが…$R;" +
                                   "$Pふふ…$R;" +
                                   "$Pうまくできたわ。$R;" +
                                   "$Rでも髪型を変えたら元に戻っちゃうから$R;" +
                                   "気をつけてね♪$R;");
        }

        void 破壞髮型(ActorPC pc)
        {
            Say(pc, 11000411, 131, "今の髪型には無理があるわね。$R;" +
                                   "$R一度髪を切ってしまった方が、$R;" +
                                   "いいと思うわ?$R;");

            switch (Select(pc, "スキンヘッドにする?", "", "いいよ", "やっぱりだめ"))
            {
                case 1:
                    Wait(pc, 1000);
                    pc.Wig = 255;
                    pc.HairStyle = 7;

                    ShowEffect(pc, 4112);
                    Wait(pc, 1000);

                    Say(pc, 11000411, 131, "はい、終わったわよ!$R;");
                    break;

                case 2:
                    switch (Select(pc, "どの位置にする?", "", "ダウン", "選び直す"))
                    {
                        case 1:
                            Say(pc, 11000411, 131, "じゃ、はじめるわよ!$R;");

                            Wait(pc, 1000);
                            pc.Wig = 9;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020708, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            破壞髮型(pc);
                            break;
                    }
                    break;
            }
        }

        void 名人介紹書第一種(ActorPC pc)
        {
            Say(pc, 11000411, 131, "これなら$R;" +
                                   "どんな髪型にでもつけられるわ$R;" +
                                   "$Rどれも素敵よ?$R;");

            switch (Select(pc, "どんなエクステにする?", "", "根っこでまとめて", "根っこと先でまとめて", "三つ編みにして", "小さく三つ編みにして", "やめる"))
            {
                case 1:
                    switch (Select(pc, "どの位置にする?", "", "アップ", "ダウン", "選び直す"))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 7;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 6;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 3:
                            名人介紹書第一種(pc);
                            break;
                    }
                    break;

                case 2:		//根っこと先でまとめて
                    switch (Select(pc, "どの位置にする?", "", "アップ", "ダウン", "両サイド", "選び直す"))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 10;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 9;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 3:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 11;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 4:
                            名人介紹書第一種(pc);
                            break;
                    }
                    break;

                case 3:	//三つ編み
                    switch (Select(pc, "どの位置にする?", "", "アップ", "ダウン", "両サイド", "選び直す"))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 1;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 0;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 3:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 2;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 4:
                            名人介紹書第一種(pc);
                            break;
                    }
                    break;

                case 4:	// 小さく三つ編み
                    switch (Select(pc, "どの位置にする?", "", "アップ", "ダウン", "両サイド", "選び直す"))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 13;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 12;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 3:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 14;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 4:
                            名人介紹書第一種(pc);
                            break;
                    }
                    break;

                case 5:
                    break;
            }
        }

        void 名人介紹書第二種(ActorPC pc)
        {
            Say(pc, 11000411, 131, "これなら$R;" +
                                   "どんな髪型にでもつけられるわ$R;" +
                                   "$Rどれも素敵よ?$R;");

            switch (Select(pc, "どんなエクステにする?", "", "根っこでまとめて", "根っこと先でまとめて", "三つ編みにして", "小さく三つ編みにして", "やめる"))
            {
                case 1:
                    switch (Select(pc, "どの位置にする?", "", "アップ", "ダウン", "選び直す"))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 7;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 6;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 3:
                            名人介紹書第二種(pc);
                            break;
                    }
                    break;

                case 2:
                    switch (Select(pc, "どの位置にする?", "", "アップ", "ダウン", "両サイド", "選び直す"))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 10;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 9;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 3:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 11;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 4:
                            名人介紹書第二種(pc);
                            break;
                    }
                    break;

                case 3:
                    switch (Select(pc, "どの位置にする?", "", "アップ", "ダウン", "選び直す"))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 1;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 2;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 3:
                            名人介紹書第二種(pc);
                            break;
                    }
                    break;

                case 4:
                    switch (Select(pc, "どの位置にする?", "", "アップ", "両サイド", "選び直す"))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 13;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 14;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 3:
                            名人介紹書第二種(pc);
                            break;
                    }
                    break;

                case 5:
                    break;
            }
        }

        void 名人介紹書第三種(ActorPC pc)
        {
            Say(pc, 11000411, 131, "これなら$R;" +
                                   "どんな髪型にでもつけられるわ$R;" +
                                   "$Rどれも素敵よ?$R;");

            switch (Select(pc, "どんなエクステにする?", "", "根っこでまとめて", "根っこと先でまとめて", "三つ編みにして", "小さく三つ編みにして", "お団子にして", "やめる"))
            {
                case 1:
                    switch (Select(pc, "どの位置にする?", "", new string[] { "アップ", "選び直す" }))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 7;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            名人介紹書第三種(pc);
                            break;
                    }
                    break;

                case 2:
                    switch (Select(pc, "どの位置にする?", "", "アップ", "両サイド", "選び直す"))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 10;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 11;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 3:
                            名人介紹書第三種(pc);
                            break;
                    }
                    break;

                case 3:
                    switch (Select(pc, "どの位置にする?", "", "アップ", "ダウン", "選び直す"))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 1;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 2;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 3:
                            名人介紹書第三種(pc);
                            break;
                    }
                    break;

                case 4:
                    switch (Select(pc, "どの位置にする?", "", "アップ", "両サイド", "選び直す"))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 13;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 14;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 3:
                            名人介紹書第三種(pc);
                            break;
                    }
                    break;

                case 5:
                    switch (Select(pc, "どの位置にする?", "", "両サイド", "選び直す"))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 17;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            名人介紹書第三種(pc);
                            break;
                    }
                    break;

                case 6:
                    break;
            }
        }

        void 名人介紹書第四種(ActorPC pc)
        {
            Say(pc, 11000411, 131, "これなら$R;" +
                                   "どんな髪型にでもつけられるわ$R;" +
                                   "$Rどれも素敵よ?$R;");

            switch (Select(pc, "どんなエクステにする?", "", "根っこでまとめて", "根っこと先でまとめて", "三つ編みにして", "小さく三つ編みにして", "お団子にして", "やめる"))
            {
                case 1:
                    switch (Select(pc, "どの位置にする?", "", "アップ", "選び直す"))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 7;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            名人介紹書第四種(pc);
                            break;
                    }
                    break;

                case 2:
                    switch (Select(pc, "どの位置にする?", "", "アップ", "選び直す"))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 10;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            名人介紹書第四種(pc);
                            break;
                    }
                    break;

                case 3:
                    switch (Select(pc, "どの位置にする?", "", "アップ", "選び直す"))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 1;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            名人介紹書第四種(pc);
                            break;
                    }
                    break;

                case 4:
                    switch (Select(pc, "どの位置にする?", "", "アップ", "選び直す"))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 13;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            名人介紹書第四種(pc);
                            break;
                    }
                    break;

                case 5:
                    switch (Select(pc, "どの位置にする?", "", "両サイド", "選び直す"))
                    {
                        case 1:
                            セレブStart(pc);

                            Wait(pc, 1000);
                            pc.Wig = 17;
                            Wait(pc, 1000);

                            ShowEffect(pc, 4112);

                            TakeItem(pc, 10020706, 1);

                            セレブComplete(pc);
                            break;

                        case 2:
                            名人介紹書第四種(pc);
                            break;
                    }
                    break;

                case 6:
                    break;
            }
        }

        void アニキの紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!!$R;" +
                                   "$R『アニキの紹介状』じゃない?$R;" +
                                   "$Rアクロニア大陸で$R;" +
                                   "とても流行った髪型なのよ～$R;" +
                                   "やってみる?$R;");

            switch (Select(pc, "どうする?", "", "アニキの紹介状を使う", "使わない"))
            {
                case 1:
                    switch (Select(pc, "どんな髪型にする?", "", "モヒカン", "リーゼント", "やめる"))
                    {
                        case 1:
                            TakeItem(pc, 10020753, 1);

                            Say(pc, 11000411, 131, "モヒカンね?$R;" +
                                                   "$Rじゃ、はじめるわよ!$R;");
                            Wait(pc, 1000);
                            pc.Wig = 255;
                            pc.HairStyle = 7;

                            ShowEffect(pc, 4112);
                            Wait(pc, 2000);
                            PlaySound(pc, 0x8a5, false, 100, 50);
                            Wait(pc, 1000);

                            Say(pc, 11000411, 131, "まだまだよ!$R;");

                            Wait(pc, 1000);
                            pc.Wig = 255;
                            pc.HairStyle = 11;

                            ShowEffect(pc, 4112);
                            Wait(pc, 3000);

                            Say(pc, 11000411, 131, "……まあ!!$R;" +
                                                   "$Pカッコ良くて$R;" +
                                                   "皆振り返るわよ!$R;" +
                                                   "$Rもちろんアタシもね!$R;" +
                                                   "$Pああ、でも髪型を変えると$R;" +
                                                   "とれちゃうから$R;" +
                                                   "気をつけてね♪$R;");
                            break;

                        case 2:
                            TakeItem(pc, 10020753, 1);

                            Say(pc, 11000411, 131, "リーゼントね?$R;" +
                                                   "$Rじゃ、はじめるわよ!$R;");
                            Wait(pc, 1000);
                            pc.Wig = 255;
                            pc.HairStyle = 7;

                            ShowEffect(pc, 4112);
                            Wait(pc, 2000);
                            PlaySound(pc, 0x8a5, false, 100, 50);
                            Wait(pc, 1000);

                            Say(pc, 11000411, 131, "まだまだよ!$R;");

                            Wait(pc, 1000);
                            pc.Wig = 255;
                            pc.HairStyle = 12;

                            ShowEffect(pc, 4112);
                            Wait(pc, 3000);

                            Say(pc, 11000411, 131, "……まあ!!$R;" +
                                                   "$Pカッコ良くて$R;" +
                                                   "皆振り返るわよ!$R;" +
                                                   "$Rもちろんアタシもね!$R;" +
                                                   "$Pああ、でも髪型を変えると$R;" +
                                                   "とれちゃうから$R;" +
                                                   "気をつけてね♪$R;");
                            break;

                        case 3:
                            break;
                    }
                    break;

                case 2:
                    break;
            }
        }

        void ゴージャス紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!!$R;" +
                                   "$R『ゴージャス紹介状』じゃない?!$R;" +
                                   "気品溢れる髪にしてあげるわ～?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "『ゴージャス紹介状』を使う", "使わない"))
            {
                case 1:
                    switch (Select(pc, "どんな髪型にする?", "", "縦ロール", "正統派おさげ", "やめる"))
                    {
                        case 1:
                            TakeItem(pc, 10020764, 1);

                            Start(pc);

                            ShowEffect(pc, 4112);
                            pc.Wig = 39;
                            pc.HairStyle = 21;

                            Complete(pc);
                            break;

                        case 2:
                            TakeItem(pc, 10020764, 1);

                            Start(pc);

                            ShowEffect(pc, 4112);
                            pc.Wig = 255;
                            pc.HairStyle = 22;

                            Complete(pc);
                            break;

                        case 3:
                            Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                                   "髪型を変えたくなったら$R;" +
                                                   "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                            break;
                    }
                    break;

                case 2:
                    Cancel(pc);
                    break;
            }
        }

        void おめかし紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ!!$R;" +
                                   "$R『おめかし紹介状』じゃない?$R;" +
                                   "どこかお出かけするの～?$R;" +
                                   "$Pじゃあ!$R;" +
                                   "こんな髪型はどうかしら?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "サラサラボブ", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020766, 1);

                    Start(pc);

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 15;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void おてんば紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ!!$R;" +
                                   "$R『おてんば紹介状』じゃない?$R;" +
                                   "$P元気いっぱいに見せるには…$R;" +
                                   "こんな髪型とかはどうかしらね?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "元気のいい弟の髪形", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020765, 1);

                    Start(pc);

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 14;

                    Complete(pc);
                    break;

                case 2:
                    Cancel(pc);
                    break;
            }
        }

        void 姫さまの紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ!$R;" +
                                   "$R『姫さまの紹介状』ね?$R;" +
                                   "どんな姫なのかしらね…$R;" +
                                   "$Pその子にふさわしく$R;" +
                                   "負けないような髪にしてあげるわ～!$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "若様の髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020767, 1);

                    Start(pc);

                    ShowEffect(pc, 4112);
                    pc.Wig = 42;
                    pc.HairStyle = 16;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void ちょいかわ紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ!!$R;" +
                                   "$R『ちょいかわ紹介状』ね?$R;" +
                                   "$Pどう?$R;" +
                                   "さわやかに決めてみない～?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "『ちょいかわ紹介状』を使う", "使わない"))
            {
                case 1:
                    switch (Select(pc, "どんな髪型にする?", "", "ショート＆エクステ", "スポーティーポニー", "やめる"))
                    {
                        case 1:
                            TakeItem(pc, 10020761, 1);

                            Start(pc);

                            ShowEffect(pc, 4112);
                            pc.Wig = 38;
                            pc.HairStyle = 1;

                            Complete(pc);
                            break;

                        case 2:
                            TakeItem(pc, 10020761, 1);

                            Start(pc);

                            ShowEffect(pc, 4112);
                            pc.Wig = 37;
                            pc.HairStyle = 20;

                            Complete(pc);
                            break;

                        case 3:
                            Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                                   "髪型を変えたくなったら$R;" +
                                                   "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                            break;
                    }
                    break;

                case 2:
                    Cancel(pc);
                    break;
            }
        }


        void 機械時代のヘアカタログ(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                                   "$R『機械時代のヘアカタログ』?$R;" +
                                   "$P…こんな昔の髪型にしちゃうの?$R;" +
                                   "$Rまあ、この髪型は斬新ですって?$R;" +
                                   "$Rううん、そうね。悪くないわね!$R;" +
                                   "うん、とてもいいかもね!$R;" +
                                   "$Rじゃあ?$R;" +
                                   "やってみようかしら?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "機械時代の髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020760, 1);

                    Start(pc);

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 19;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void ドミニオンのヘアカタログ(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあまあ!?$R;" +
                                   "$P……$R;" +
                                   "$Pは～～!!$R;" +
                                   "$Rこれは意外と新鮮で綺麗ね。$R;" +
                                   "真夏の情熱に溢れているわ!$R;" +
                                   "$Pなんだか、涙が溢れてくるみたい。$R;" +
                                   "$Rどう?$R;" +
                                   "このカタログに載っている$R;" +
                                   "髪型にしてみようかしら?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "ベリアルの髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020755, 1);

                    Start(pc);

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 13;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }

        }

        void ラブモテ紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ!$R;" +
                                   "$R『ラブモテ紹介状』じゃない?$R;" +
                                   "$Pふふ、まかせといて！$R;" +
                                   "誰もが振り返る素敵な髪型にしてあげるわ♪$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "新しい出会いを予感させる髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020768, 1);

                    Start(pc);

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 17;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void クラシックな紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ!$R;" +
                                   "$R『クラシックな紹介状』ね?$R;" +
                                   "$Pふふ、まかせといて！$R;" +
                                   "できる男の素敵な髪型にしてあげるわ♪$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "清涼感のあるフォーマルな髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020770, 1);

                    Start(pc);

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 18;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void 少佐の紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                                   "$R『少佐の紹介状』?$R;" +
                                   "$Pこれは珍しいわね。$R;" +
                                   "$Pどう?$R;" +
                                   "$Rその『少佐の紹介状』で$R;" +
                                   "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "草薙素子の髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020771, 1);

                    Say(pc, 11000411, 131, "じゃあ、はじめるよ!$R;", "ヘルヘイムのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 30;

                    Complete(pc);
                    break;

                case 2:
                    Cancel(pc);
                    break;
            }
        }

        void プリンセスの紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ!$R;" +
                                   "$R『プリンセスの紹介状』ね?$R;" +
                                   "$Pふふ、まかせといて！$R;" +
                                   "$Pお姫様の相手にふさわしい、$R;" +
                                   "素敵な髪型にしてあげるわ!$R;" +
                                   "$P……それともお姫様みたいになりたいの?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "プリンスの髪型", "プリンセスの髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020769, 1);

                    Start(pc);

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 28;

                    Complete(pc);
                    break;

                case 2:
                    TakeItem(pc, 10020769, 1);

                    Start(pc);

                    ShowEffect(pc, 4112);
                    pc.Wig = 45;
                    pc.HairStyle = 27;

                    Complete(pc);
                    break;

                case 3:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void ふたご座の紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ!$R;" +
                                   "$R『ふたご座の紹介状』ね?$R;" +
                                   "$Pこれは女の子用とセットなのよね。$R;" +
                                   "二人並ぶと一層素敵に見えるわ!$R;" +
                                   "$Pどう?$R;" +
                                   "$Rその『ふたご座の紹介状』で$R;" +
                                   "髪型を変えてみない?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "メッシュ入りショートヘア", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020773, 1);

                    Start(pc);

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 23;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void ラテンの紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!$R;" +
                                   "$R『ラテンの紹介状』じゃない?$R;" +
                                   "$Pん～、情熱溢れる素敵な髪型ね!$R;" +
                                   "$Pきっとあなたにも似合うわよ♪$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "情熱的なアップ", "情熱的なショート", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020774, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 46;
                    pc.HairStyle = 32;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020774, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 34;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void 魔女の紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ!$R;" +
                                   "$R『魔女の紹介状』ね?$R;" +
                                   "$Pたまにはこんな髪型もいいのかもね$R;" +
                                   "$Pどう?$R;" +
                                   "セットしてみない～?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "アンニュイ", "ヘアピン", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020775, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 35;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020775, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 36;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void ワイルドな紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ!?$R;" +
                                   "$R『ワイルドな紹介状』ね?$R;" +
                                   "$P野性味あふれ、活動的で…$R;" +
                                   "$Pうん、あなたにぴったりなのがあるわ～!$R;" +
                                   "$Pこんなのはどうかしら?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "ワイルドショート", "ワイルドミディアム", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020776, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 37;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020776, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 38;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void 武家の紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ!$R;" +
                                   "$R『武家の紹介状』じゃない?$R;" +
                                   "$P東洋風でワイルドな感じがいいわね～$R;" +
                                   "$Pどう?$R;" +
                                   "あなたもこんな風になってみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "ワイルドな流浪人のような髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020777, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 49;
                    pc.HairStyle = 39;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void バレンタイン紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                                   "$R『バレンタイン紹介状』?$R;" +
                                   "$Pこれは珍しいわね。$R;" +
                                   "$Pどう?$R;" +
                                   "$Rその『バレンタイン紹介状』で$R;" +
                                   "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "とんがった前髪が特徴的なショート", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020779, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 40;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void スイートな紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ!$R;" +
                                   "$R『スイートな紹介状』ね?$R;" +
                                   "$Pふわふわ感のある素敵な髪型よ!$R;" +
                                   "$Pどう?$R;" +
                                   "あなたもそんな風になってみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "シュークリームボブ", "ロールツインテール", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020782, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 42;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020782, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 51;
                    pc.HairStyle = 43;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void デビューへの紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ!$R;" +
                                   "$R『デビューへの紹介状』ね?$R;" +
                                   "$P新人っぽく、そして$R;" +
                                   "さわやかで…!$R;" +
                                   "$Pそんな素敵な髪型にしてあげましょう♪$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "長めのコンサバヘアー", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020780, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 41;
                    Complete(pc);
                    break;
                case 2:
                    Cancel(pc);
                    break;
            }
        }

        void おとぎの紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!$R;" +
                                   "$R『おとぎの紹介状』ね?$R;" +
                                   "おとぎ話の人物の気分に$R;" +
                                   "浸るのもいいわね!$R;" +
                                   "$Pふふ、まかせといて！$R;" +
                                   "$Pアタシならあなたをそんな気持ちに$R;" +
                                   "させてあげられるわ♪$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "くりんくりんショート", "ゆる三つ編み", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020783, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 45;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020783, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 44;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }


        void マネージャーの紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!$R;" +
                                   "$R『マネージャーの紹介状』ね?$R;" +
                                   "$P仕事の出来るそんなイメージ…$R;" +
                                   "$Pうん、あなたにぴったりなのがあるわ!$R;" +
                                   "$Pこんなのはどうかしら?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "なまいきポニー", "世話焼きショート", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020785, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 54;
                    pc.HairStyle = 49;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020785, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 48;
                    Complete(pc);
                    break;
                case 3:
                    Cancel(pc);
                    break;
            }
        }

        void ひぐらしの紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                                   "$R『ひぐらしの紹介状』?$R;" +
                                   "$Pこれは珍しいわね。$R;" +
                                   "$Pどう?$R;" +
                                   "$Rその『ひぐらしの紹介状』で$R;" +
                                   "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "沙都子の髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020786, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 52;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void 空賊の紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                                   "$R『空賊の紹介状』?$R;" +
                                   "$Pこれは珍しいわね。$R;" +
                                   "$Pどう?$R;" +
                                   "$Rその『空賊の紹介状』で$R;" +
                                   "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "ウィリーの髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020787, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 55;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void 歌姫の紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!$R;" +
                                   "$R『歌姫の紹介状』ね?$R;" +
                                   "$Pそうねぇ$R;" +
                                   "$P少し古風で気品も持たせて…$R;" +
                                   "$Pうん、素敵な髪型になるわよ?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "クラシック風に束ねた髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020784, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 53;
                    pc.HairStyle = 47;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void 真夏の紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!$R;" +
                                   "$R『真夏の紹介状』ね?$R;" +
                                   "$P開放感溢れる、そして$R;" +
                                   "活発な感じにしてみない?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "前髪長めのツンツンヘアー", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020788, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 25;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void 委員長の紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ!$R;" +
                                   "$R『委員長の紹介状』ね?$R;" +
                                   "$P真面目で堅苦しく$R;" +
                                   "見られるかもしれないけど、$R;" +
                                   "気持ちも引き締まっていいかもね!$R;" +
                                   "$P髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "イケてるベリーショート", "イケてるボブ", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020789, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 58;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020789, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 57;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void 貴族の紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                                   "$R『貴族の紹介状』?$R;" +
                                   "$Pこれは珍しいわね。$R;" +
                                   "$Pどう?$R;" +
                                   "$Rその『貴族の紹介状』で$R;" +
                                   "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "儚げなくるくるショート", "麗しのミニ縦ロール", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020790, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 59;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020790, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 60;
                    pc.HairStyle = 60;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void レナの紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                                   "$R『レナの紹介状』?$R;" +
                                   "$Pこれは珍しいわね。$R;" +
                                   "$Pどう?$R;" +
                                   "$Rその『レナの紹介状』で$R;" +
                                   "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "竜宮レナの髪形", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020791, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 54;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void ハロウィン紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                                   "$R『ハロウィン紹介状』?$R;" +
                                   "$Pこれは珍しいわね。$R;" +
                                   "$Pどう?$R;" +
                                   "$Rその『ハロウィン紹介状』で$R;" +
                                   "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "さばさばしたイケメン風ロングヘアー", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020792, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 61;
                    Complete(pc);
                    break;
                case 2:
                    Cancel(pc);
                    break;
            }
        }

        void 真冬の紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                                   "$R『真冬の紹介状』?$R;" +
                                   "$Pどう?$R;" +
                                   "$Rその『真冬の紹介状』で$R;" +
                                   "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "一つ結いのミディアムヘア", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020793, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 60;
                    pc.HairStyle = 63;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void ハイカラ紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ!$R;" +
                                   "$R『ハイカラ紹介状』ね?$R;" +
                                   "$Pちょっぴり古風なところが$R;" +
                                   "魅力な髪型よね♪$R;" +
                                   "$Pこういうのはどうかしらね?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "ボーイッシュなさらさらショート", "清楚なストレートロング（ウィッグあり）", "清楚なストレートロング（ウィッグなし）", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020794, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 71;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020794, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 63;
                    pc.HairStyle = 72;
                    Complete(pc);
                    break;
                case 3:
                    TakeItem(pc, 10020794, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 72;
                    Complete(pc);
                    break;
                case 4:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void アルバイト紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!$R;" +
                                   "$R『アルバイト紹介状』ね?$R;" +
                                   "$Pちょっとお仕事で人前に出たりするとき$R;" +
                                   "印象って大切よね!$R;" +
                                   "$P清潔感を表に出すように$R;" +
                                   "工夫してみるわね?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "清潔感のあるミディアム", "清潔感のあるショート", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020795, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 73;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020795, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 74;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void 軍式紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                                   "$R『軍式紹介状』?$R;" +
                                   "$Pこれは珍しいわね。$R;" +
                                   "$Pどう?$R;" +
                                   "$Rその『軍式紹介状』で$R;" +
                                   "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "風格あるロングおさげ", "半人前ショートおさげ", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020796, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 75;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020796, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 76;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void 伝説の紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                       "$R『伝説の紹介状』?$R;" +
                       "$Pこれは珍しいわね。$R;" +
                       "$Pどう?$R;" +
                       "$Rその『伝説の紹介状』で$R;" +
                       "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "おとぎ話の主人公風ショート", "おとぎ話の主人公風ロング", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020797, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 77;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020797, 1);
                    Start(pc);
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 78;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void 愛され紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                       "$R『愛され紹介状』?$R;" +
                       "$Pこれは珍しいわね。$R;" +
                       "$Pどう?$R;" +
                       "$Rその『愛され紹介状』で$R;" +
                       "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "マニッシュボブ", "愛されポニー（リボンつき）", "愛されポニー（リボンなし）", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10074400, 1);
                    Say(pc, 11000411, 131, "始めるわよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 79;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10074400, 1);
                    Say(pc, 11000411, 131, "始めるわよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 66;
                    pc.HairStyle = 82;
                    Complete(pc);
                    break;
                case 3:
                    TakeItem(pc, 10074400, 1);
                    Say(pc, 11000411, 131, "始めるわよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 64;
                    pc.HairStyle = 82;
                    Complete(pc);
                    break;
                case 4:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void 夏休みの紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                       "$R『夏休みの紹介状』?$R;" +
                       "$Pこれは珍しいわね。$R;" +
                       "$Pどう?$R;" +
                       "$Rその『夏休みの紹介状』で$R;" +
                       "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "ワイルドオールバック", "ニードルショート", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10074800, 1);
                    Say(pc, 11000411, 131, "始めるわよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 68;
                    pc.HairStyle = 84;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10074800, 1);
                    Say(pc, 11000411, 131, "始めるわよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 85;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void ふたご座の紹介状2(ActorPC pc)
        {
            Say(pc, 11000411, 131, "まあ!$R;" +
                                   "$R『ふたご座の紹介状☆』ね?$R;" +
                                   "$Pこれは女の子用とセットなのよね。$R;" +
                                   "二人並ぶと一層素敵に見えるわ!$R;" +
                                   "$Pどう?$R;" +
                                   "$Rその『ふたご座の紹介状☆』で$R;" +
                                   "髪型を変えてみない?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "爽やかなショートヘア", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10075200, 1);
                    Say(pc, 11000411, 131, "始めるわよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 89;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void ロビン・グッドフェロウの紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                       "$R『ロビンの紹介状』?$R;" +
                       "$Pこれは珍しいわね。$R;" +
                       "$Pどう?$R;" +
                       "$Rその『ロビンの紹介状』で$R;" +
                       "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "カチューシャあり", "カチューシャなし", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10075201, 1);
                    Say(pc, 11000411, 131, "始めるわよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 69;
                    pc.HairStyle = 87;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10075201, 1);
                    Say(pc, 11000411, 131, "始めるわよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 69;
                    pc.HairStyle = 88;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void 厳島美晴の紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                       "$R『厳島 美晴の紹介状』?$R;" +
                       "$Pこれは珍しいわね。$R;" +
                       "$Pどう?$R;" +
                       "$Rその『厳島 美晴の紹介状』で$R;" +
                       "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "厳島 美晴の髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10075202, 1);
                    Say(pc, 11000411, 131, "始めるわよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 86;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void アングリー紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                       "$R『アングリー紹介状』?$R;" +
                       "$Pこれは珍しいわね。$R;" +
                       "$Pどう?$R;" +
                       "$Rその『アングリー紹介状』で$R;" +
                       "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "スイートドーリーボブ", "ドーリーカールヘア（カチューシャあり）", "ドーリーカールヘア（カチューシャなし）", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10075203, 1);
                    Say(pc, 11000411, 131, "始めるわよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 98;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10075203, 1);
                    Say(pc, 11000411, 131, "始めるわよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 70;
                    pc.HairStyle = 93;
                    Complete(pc);
                    break;
                case 3:
                    TakeItem(pc, 10075203, 1);
                    Say(pc, 11000411, 131, "始めるわよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 71;
                    pc.HairStyle = 93;
                    Complete(pc);
                    break;
                case 4:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void コウモリタ紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                       "$R『コウモリタ紹介状』?$R;" +
                       "$Pこれは珍しいわね。$R;" +
                       "$Pどう?$R;" +
                       "$Rその『コウモリタ紹介状』で$R;" +
                       "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "クラシカルマッシュ", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10075204, 1);
                    Say(pc, 11000411, 131, "始めるわよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 94;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void ハングリー紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                       "$R『ハングリー紹介状』?$R;" +
                       "$Pこれは珍しいわね。$R;" +
                       "$Pどう?$R;" +
                       "$Rその『ハングリー紹介状』で$R;" +
                       "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "斬新なモヒカン", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10075205, 1);
                    Say(pc, 11000411, 131, "始めるわよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 95;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }

        void 西洋ロマンの紹介状(ActorPC pc)
        {
            Say(pc, 11000411, 131, "あら!?$R;" +
                       "$R『西洋ロマンの紹介状』?$R;" +
                       "$Pこれは珍しいわね。$R;" +
                       "$Pどう?$R;" +
                       "$Rその『西洋ロマンの紹介状』で$R;" +
                       "髪型を変えてみる?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "ナチュラルロングウェーブ", "シャギーハイレイヤー", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10075206, 1);
                    Say(pc, 11000411, 131, "始めるわよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 99;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10075206, 1);
                    Say(pc, 11000411, 131, "始めるわよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 100;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000411, 131, "そう、残念ねぇ。$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またいらっしゃい♪$R;", "ヘルヘイムのヘアサロン");
                    break;
            }
        }


    }
}
