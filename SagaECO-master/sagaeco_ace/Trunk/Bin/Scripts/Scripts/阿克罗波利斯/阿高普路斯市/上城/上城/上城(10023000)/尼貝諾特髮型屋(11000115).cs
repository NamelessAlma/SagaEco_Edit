using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

namespace SagaScript.M10023000
{

    public class S11000115 : Event
    {
        public class HairStyleList
        {
            public uint id;
            public string name;
        }
        private uint[] id = {
					  10020708,	//ヘアサロン
					  10020706,	//セレブ
					  10020750,	//ヘアサロン特別
					  10020755,	//ドミニオン
					  10020756,	//プリティ
					  10020759,	//ルチル
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
					  10075206	//西洋ロマン
					};
        private List<HairStyleList> stylelist;
        private List<String> stylenames;

        public S11000115()
        {
            this.EventID = 11000115;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            Say(pc, 131, "やあ！ こんにちは。$R;" + 
            "ニーベルングのヘアサロンにようこそ！$R;" + 
            "$Rここでは君の髪型を$R;" + 
            "君の思うがままにしてあげるよ。$R;" + 
            "$Pさぁ、綺麗になろっか？$R;", "ニーベルングのヘアサロン");

            Say(pc, 131, "……？$R;" + 
            "$Pわ～お、君はひょっとして$Rこの街は初めてかい？$R;" + 
            "$Pじゃあお近づきのしるしに$R;" + 
            "とっても素敵なプレゼントをあげるよ！$R;", "ニーベルングのヘアサロン");

            Say(pc, 0, 131, "『ヘアカラー・キャンディーイエロー』$R;" + 
            "を手に入れた！$R;", " ");

            Say(pc, 131, "それは『ヘアカラー』といって$R;" + 
            "君の素敵な髪の色を、もっともーっと$R;" + 
            "素敵な色に染めてくれるアイテムさ！$R;" + 
            "$P使い方は簡単！$R;" + 
            "ヘアカラーをダブルクリックするだけ。$R;" + 
            "$R１度染めた髪色は戻らないから$R;" + 
            "染めるときは気をつけてね。$R;", "ニーベルングのヘアサロン");

            Say(pc, 131, "やあ！こんにちは。$R;" +
            "ニーベルングのヘアサロンにようこそ！$R;" +
            "$Pさぁ、綺麗になろっか？$R;", "ニーベルングのヘアサロン");
            Select(pc, "どうする？", "", "髪型を変更する", "やめておく");
            Say(pc, 131, "カットするかい？$Rそれともスタイリングかい？$R;" + 
            "$Rカットだと３０００ゴールド$Rスタイリングだと$R１００００ゴールドになるよ。$R;", "ニーベルングのヘアサロン");
            Select(pc, "どうする？", "", "カットする","スタイリングする","やめておく");
            Say(pc, 131, "……。$R;" + 
            "$Pあ～ん、残念～$R;" + 
            "君は持ち合わせが足りないようだね？$R;" + 
            "$Rまたおいでよ。$R;", "ニーベルングのヘアサロン");
*/

            BitMask<Acropolisut_01> mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]);

            if (pc.Marionette != null)
            {
                Say(pc, 11000115, 131, "ああ、先にマリオネットを解除して、$R;" +
                                       "それから来てくれないか!$R;"
                                       , "ニーベルングのヘアサロン");
                return;
            }

            if (!mask.Test(Acropolisut_01.已經與尼貝諾特髮型屋進行第一次對話))
            {
                ニーベルング会話初回(pc);
                return;
            }

            if (pc.Gender == PC_GENDER.FEMALE)
            {

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
                    使用髮型介紹書2(pc);
                    return;
                }
            }
            Say(pc, 131, "やあ！ こんにちは。$R;" +
                "ニーベルングのヘアサロンにようこそ！$R;" +
                "$Rここでは君の髪型を$R;" +
                "君の思うがままにしてあげるよ。$R;" +
                "$Pさぁ、綺麗になろっか？$R;", "ニーベルングのヘアサロン");

            基本髮型目錄(pc);
            return;
        }

        private void MaleCancel(ActorPC pc, String card)
        {
            Say(pc, 11000115, 131, "……$R;" +
                           "$Pああ、申し訳ない!!$R;" +
                           "$R言い忘れたけど$R" +
                           "『" + card + "』は女性専用なんだ。$R;" +
                           "$P悪いけど君では使えないよ!$R;", "ニーベルングのヘアサロン");
        }

        private void NoItem(ActorPC pc, String card)
        {
            Say(pc, 11000115, 131, "おや?!$R;" +
                       "$P『" + card + "』を$R持ってないようだね?$R;", "ニーベルングのヘアサロン");
        }

        private void Complete(ActorPC pc)
        {
            Say(pc, 11000115, 131, "うまくできたかな!$R;" +
                                   "$Rわ～!!$R;" +
                                   "素敵だよ! よく似合ってる!$R;" +
                                   "またおいでよ!!$R;", "ニーベルングのヘアサロン");
        }

        void ニーベルング会話初回(ActorPC pc)
        {
            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]);

            int Gift;

            Acropolisut_01_mask.SetValue(Acropolisut_01.已經與尼貝諾特髮型屋進行第一次對話, true);

            Say(pc, 131, "やあ！ こんにちは。$R;" +
                        "ニーベルングのヘアサロンにようこそ！$R;" +
                        "$Rここでは君の髪型を$R;" +
                        "君の思うがままにしてあげるよ。$R;" +
                        "$Pさぁ、綺麗になろっか？$R;", "ニーベルングのヘアサロン");

            Say(pc, 131, "……？$R;" +
                        "$Pわ～お、君はひょっとして$Rこの街は初めてかい？$R;" +
                        "$Pじゃあお近づきのしるしに$R;" +
                        "とっても素敵なプレゼントをあげるよ！$R;", "ニーベルングのヘアサロン");

            /*
            Say(pc, 11000115, 131, "哦，您好!$R;" +
                                   "歡迎來到「尼貝隆肯髮型屋」。$R;" +
                                   "$R這裡可以做您喜歡的髮型。$R;" +
                                   "$P那麼就來試試啊?$R;", "尼貝諾特髮型屋");

            Say(pc, 11000115, 131, "……?$R;" +
                                   "$P哇!!$R;" +
                                   "$R看樣子您應該是第一次來這裡吧?$R;" +
                                   "$P來，先送您個代表友誼的禮物吧，$R;" +
                                   "很漂亮的唷!$R;", "尼貝諾特髮型屋");
            */
            Gift = Global.Random.Next(1, 12);

            switch (Gift)
            {
                case 1:
                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 10031301, 1);
                    Say(pc, 0, 131, "『ヘアカラー・チェリーレッド』$R;" + "を手に入れた！$R;", " ");
                    break;

                case 2:
                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 10031302, 1);
                    Say(pc, 0, 131, "『ヘアカラー・ジェードパープル』$R;" + "を手に入れた！$R;", " ");
                    break;

                case 3:
                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 10031303, 1);
                    Say(pc, 0, 131, "『ヘアカラー・ブルー』$R;" + "を手に入れた！$R;", " ");
                    break;

                case 4:
                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 10031304, 1);
                    Say(pc, 0, 131, "『ヘアカラー・アイスブルー』$R;" + "を手に入れた！$R;", " ");
                    break;

                case 5:
                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 10031305, 1);
                    Say(pc, 0, 131, "『ヘアカラー・グリーン』$R;" + "を手に入れた！$R;", " ");
                    break;

                case 6:
                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 10031306, 1);
                    Say(pc, 0, 131, "『ヘアカラー・ライトグリーン』$R;" + "を手に入れた！$R;", " ");
                    break;

                case 7:
                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 10031307, 1);
                    Say(pc, 0, 131, "『ヘアカラー・キャンディーイエロー』$R;" + "を手に入れた！$R;", " ");
                    break;

                case 8:
                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 10031308, 1);
                    Say(pc, 0, 131, "『ヘアカラー・オレンジ』$R;" + "を手に入れた！$R;", " ");
                    break;

                case 9:
                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 10031309, 1);
                    Say(pc, 0, 131, "『ヘアカラー・マットブラック』$R;" + "を手に入れた！$R;", " ");
                    break;

                case 10:
                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 10031310, 1);
                    Say(pc, 0, 131, "『ヘアカラー・ダルグレー』$R;" + "を手に入れた！$R;", " ");
                    break;

                case 11:
                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 10031311, 1);
                    Say(pc, 0, 131, "『ヘアカラー・シルバー』$R;" + "を手に入れた！$R;", " ");
                    break;

                case 12:
                    PlaySound(pc, 2040, false, 100, 50);
                    GiveItem(pc, 10031312, 1);
                    Say(pc, 0, 131, "『ヘアカラー・モイストシルバー』$R;" + "を手に入れた！$R;", " ");
                    break;
            }
            Say(pc, 131, "それは『ヘアカラー』といって$R;" +
                        "君の素敵な髪の色を、もっともーっと$R;" +
                        "素敵な色に染めてくれるアイテムさ！$R;" +
                        "$P使い方は簡単！$R;" +
                        "ヘアカラーをダブルクリックするだけ。$R;" +
                        "$R１度染めた髪色は戻らないから$R;" +
                        "染めるときは気をつけてね。$R;", "ニーベルングのヘアサロン");

        }

        void 基本髮型目錄(ActorPC pc)
        {
            switch (Select(pc, "どうする?", "", "髪型を変える", "やめる"))
            {
                case 1:
                    if (pc.HairStyle == 10)
                    {
                        Say(pc, 11000115, 131, "ああ，惜しいな!$R;" +
                                               "$R君の髪が帽子に隠れちゃってる，$R;" +
                                               "帽子を外したらまた来てくれるかな！，$R;", "ニーベルングのヘアサロン");
                        return;
                    }

                    if (pc.Gender == PC_GENDER.FEMALE)
                    {
                        女性髮型處理判斷(pc);
                        return;
                    }

                    if (pc.Gender == PC_GENDER.MALE)
                    {
                        男性髮型處理判斷(pc);
                        return;
                    }
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そっかぁ，$R;" +
                                           "髪型を変えたくなったら，$R;" +
                                           "また私のところへおいでよ！$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 男性髮型處理判斷(ActorPC pc)
        {
            if (pc.HairStyle == 7)
            {
                Say(pc, 11000115, 131, "……$R;" +
                                       "$Pなんてことだ!$R;" +
                                       "$Rキミには髪がないじゃないか！$R;" +
                                       "$R髪が伸びたらまたおいで！$R;", "ニーベルングのヘアサロン");
                return;
            }

            if (pc.HairStyle == 4)
            {
                男性剪頭髮光頭髮型處理(pc);
                return;
            }

            if (pc.HairStyle == 11 ||
                pc.HairStyle == 12)
            {
                男性剪頭髮破壞髮型處理(pc);
                return;
            }

            Say(pc, 131, "カットするかい？$Rそれともスタイリングかい？$R;" +
                        "$Rカットだと３０００ゴールド$Rスタイリングだと$R１００００ゴールドになるよ。$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "カットする", "セットする", "やめる"))
            {
                case 1:
                    男性髮型剪頭髮處理(pc);
                    break;

                case 2:
                    男性髮型燙頭髮處理(pc);
                    break;

                case 3:
                    Say(pc, 11000115, 131, "そう?$R;" +
                                           "またおいでよ!$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 男性髮型剪頭髮處理(ActorPC pc)
        {
            if (pc.Gold >= 3000)
            {
                if (pc.HairStyle == 0 ||
                    pc.HairStyle == 2 ||
                    pc.HairStyle == 5 ||
                    pc.HairStyle == 8)
                {
                    男性剪頭髮平頭髮型處理(pc);
                    return;
                }

                if (pc.HairStyle == 1 ||
                    pc.HairStyle == 3 ||
                    pc.HairStyle == 6 ||
                    pc.HairStyle == 13 ||
                    pc.HairStyle == 14 ||
                    pc.HairStyle == 15 ||
                    pc.HairStyle == 16 ||
                    pc.HairStyle == 20 ||
                    pc.HairStyle == 21 ||
                    pc.HairStyle == 22)
                {
                    男性剪頭髮自然髮型處理(pc);
                    return;
                }
            }
            else
            {
                Say(pc, 131, "……。$R;" +
                            "$Pあ～ん、残念～$R;" +
                            "君は持ち合わせが足りないようだね？$R;" +
                            "$Rまたおいでよ。$R;", "ニーベルングのヘアサロン");
            }
        }

        void 男性剪頭髮平頭髮型處理(ActorPC pc)
        {
            Say(pc, 11000115, 131, "カットするんだね?$R;" +
                                   "$R丸刈りになるから$R;" +
                                   "$P他の髪型に$R;" +
                                   "這樣也沒關係嗎?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "カットする", "やめる"))
            {
                case 1:
                    pc.Gold -= 3000;

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 4;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000115, 131, "セットしようか!$R;", "ニーベルングのヘアサロン");

                    switch (Select(pc, "どうする?", "", "セットする", "やめる"))
                    {
                        case 1:
                            男性髮型燙頭髮處理(pc);
                            break;

                        case 2:
                            Say(pc, 11000115, 131, "そう?$R;" +
                                                   "またおいでよ!$R;", "ニーベルングのヘアサロン");
                            break;
                    }
                    break;
            }
        }

        void 男性剪頭髮自然髮型處理(ActorPC pc)
        {
            Say(pc, 11000115, 131, "わかった，カットだね?$R;" +
                                   "自然な感じに仕上げるよ。$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "カットする", "やめる"))
            {
                case 1:
                    pc.Gold -= 3000;

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 0;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そう?$R;" +
                                           "またおいでよ!$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 男性剪頭髮光頭髮型處理(ActorPC pc)
        {
            Say(pc, 11000115, 131, "髪が短すぎてセットは無理だね。$R;" +
                                   "$Rカットだけになるよ?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "カットする", "やめる"))
            {
                case 1:
                    Say(pc, 11000115, 131, "本当にする?$R;", "ニーベルングのヘアサロン");

                    switch (Select(pc, "どうする?", "", "カットする", "やめる"))
                    {
                        case 1:
                            if (pc.Gold >= 3000)
                            {
                                pc.Gold -= 3000;

                                Say(pc, 11000115, 131, "……$R;" +
                                                       "$P…じゃあ。$R;" +
                                                       "$R君の髪にははさみが使えないから，$R;" +
                                                       "このバリカンで……$R;" +
                                                       "$Pはじめるよ～!$R;", "ニーベルングのヘアサロン");

                                ShowEffect(pc, 4112);
                                pc.Wig = 255;
                                pc.HairStyle = 7;

                                Complete(pc);
                                return;
                            }
                            Say(pc, 131, "……。$R;" +
                                        "$Pあ～ん、残念～$R;" +
                                        "君は持ち合わせが足りないようだね？$R;" +
                                        "$Rまたおいでよ。$R;", "ニーベルングのヘアサロン");

                            return;

                        case 2:
                            Say(pc, 11000115, 131, "そう?$R;" +
                                                   "またおいでよ!$R;", "ニーベルングのヘアサロン");
                            break;
                    }
                    break;

                case 2:
                    Say(pc, 11000115, 131, "じゃあ，$R;" +
                                           "髪が伸びたら、またおいでよ！$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 男性剪頭髮破壞髮型處理(ActorPC pc)
        {
            Say(pc, 11000115, 131, "んー，髪は素敵なんだけど，$R;" +
                                   "すごく強ばってるね。$R;" +
                                   "$R全部刈っちゃうことになるね?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "全部刈ってしまう?", "", "やめます", "やっちゃってください"))
            {
                case 1:
                    break;

                case 2:
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 7;

                    Say(pc, 11000115, 131, "できたよ!$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 男性髮型燙頭髮處理(ActorPC pc)
        {
            if (pc.Gold >= 10000)
            {
                Say(pc, 11000115, 131, "わかった，セットだね?$R;", "ニーベルングのヘアサロン");

                if (pc.HairStyle == 0)
                {
                    switch (Select(pc, "どんなふうにする?", "", "カッコよく決めて", "さわやかに決めて", "おとなしめにして", "やめる"))
                    {
                        case 1:
                            男性燙頭髮ワイルド處理(pc);
                            return;

                        case 2:
                            男性燙頭髮ボーイズ處理(pc);
                            return;

                        case 3:
                            男性燙頭髮コンサバティブ處理(pc);
                            return;

                        case 4:
                            Say(pc, 11000115, 131, "そう?$R;" +
                                                   "またおいでよ!$R;", "ニーベルングのヘアサロン");
                            return;
                    }
                }

                if (pc.HairStyle == 1 ||
                    pc.HairStyle == 20)
                {
                    switch (Select(pc, "どんなふうにする?", "", "ワイルドに決めて", "クールに決めて", "やめる"))
                    {
                        case 1:
                            男性燙頭髮オールバック處理(pc);
                            return;

                        case 2:
                            男性燙頭髮ワンレングス處理(pc);
                            return;

                        case 3:
                            Say(pc, 11000115, 131, "そう?$R;" +
                                                   "またおいでよ!$R;", "ニーベルングのヘアサロン");
                            return;
                    }
                }

                if (pc.HairStyle == 2)
                {
                    switch (Select(pc, "どんなふうにする?", "", "ナチュラルにして", "さわやかに決めて", "おとなしめにして", "やめる"))
                    {
                        case 1:
                            男性燙頭髮ロング處理(pc);
                            return;

                        case 2:
                            男性燙頭髮ボーイズ處理(pc);
                            return;

                        case 3:
                            男性燙頭髮コンサバティブ處理(pc);
                            return;

                        case 4:
                            Say(pc, 11000115, 131, "そう?$R;" +
                                                   "またおいでよ!$R;", "ニーベルングのヘアサロン");
                            return;
                    }
                }

                if (pc.HairStyle == 3)
                {
                    switch (Select(pc, "どんなふうにする?", "", "ナチュラルにして", "ワイルドに決めて", "やめる"))
                    {
                        case 1:
                            男性燙頭髮ロング處理(pc);
                            return;

                        case 2:
                            男性燙頭髮オールバック處理(pc);
                            return;

                        case 3:
                            Say(pc, 11000115, 131, "そう?$R;" +
                                                   "またおいでよ!$R;", "ニーベルングのヘアサロン");
                            return;
                    }
                }

                if (pc.HairStyle == 5 ||
                    pc.HairStyle == 8)
                {
                    switch (Select(pc, "どんなふうにする?", "", "ナチュラルにして", "カッコよく決めて", "おとなしめにして", "やめる"))
                    {
                        case 1:
                            男性燙頭髮ロング處理(pc);
                            return;

                        case 2:
                            男性燙頭髮ワイルド處理(pc);
                            return;

                        case 3:
                            男性燙頭髮コンサバティブ處理(pc);
                            return;

                        case 4:
                            Say(pc, 11000115, 131, "そう?$R;" +
                                                   "またおいでよ!$R;", "ニーベルングのヘアサロン");
                            return;
                    }
                }

                if (pc.HairStyle == 6)
                {
                    switch (Select(pc, "どんなふうにする?", "", "ナチュラルにして", "クールに決めて", "やめる"))
                    {
                        case 1:
                            男性燙頭髮ロング處理(pc);
                            return;

                        case 2:
                            男性燙頭髮ワンレングス處理(pc);
                            return;

                        case 3:
                            Say(pc, 11000115, 131, "そう?$R;" +
                                                   "またおいでよ!$R;", "ニーベルングのヘアサロン");
                            return;
                    }
                }

                if (pc.HairStyle == 13 ||
                    pc.HairStyle == 14 ||
                    pc.HairStyle == 15 ||
                    pc.HairStyle == 16 ||
                    pc.HairStyle == 21 ||
                    pc.HairStyle == 22)
                {
                    switch (Select(pc, "どんなふうにする?", "", "ワイルドに決めて", "クールに決めて", "ナチュラルにして", "やめる"))
                    {
                        case 1:
                            男性燙頭髮オールバック處理(pc);
                            return;

                        case 2:
                            男性燙頭髮ワンレングス處理(pc);
                            return;

                        case 3:
                            男性燙頭髮ロング處理(pc);
                            return;

                        case 4:
                            Say(pc, 11000115, 131, "そう?$R;" +
                                                   "またおいでよ!$R;", "ニーベルングのヘアサロン");
                            return;
                    }
                }
            }
            else
            {
                Say(pc, 131, "……。$R;" +
                            "$Pあ～ん、残念～$R;" +
                            "君は持ち合わせが足りないようだね？$R;" +
                            "$Rまたおいでよ。$R;", "ニーベルングのヘアサロン");

            }
        }

        void 男性燙頭髮ワイルド處理(ActorPC pc)
        {
            pc.Gold -= 10000;

            Say(pc, 11000115, 131, "じゃあ、はじめるよ!!$R;", "ニーベルングのヘアサロン");

            ShowEffect(pc, 4112);
            pc.Wig = 255;
            pc.HairStyle = 2;

            Complete(pc);
        }

        void 男性燙頭髮ボーイズ處理(ActorPC pc)
        {
            pc.Gold -= 10000;

            Say(pc, 11000115, 131, "じゃあ、はじめるよ!!$R;", "ニーベルングのヘアサロン");

            ShowEffect(pc, 4112);
            pc.Wig = 255;
            pc.HairStyle = 5;

            Complete(pc);
        }

        void 男性燙頭髮コンサバティブ處理(ActorPC pc)
        {
            pc.Gold -= 10000;

            Say(pc, 11000115, 131, "じゃあ、はじめるよ!!$R;", "ニーベルングのヘアサロン");

            ShowEffect(pc, 4112);
            pc.Wig = 255;
            pc.HairStyle = 8;

            Complete(pc);
        }

        void 男性燙頭髮ロング處理(ActorPC pc)
        {
            pc.Gold -= 10000;

            Say(pc, 11000115, 131, "じゃあ、はじめるよ!!$R;", "ニーベルングのヘアサロン");

            ShowEffect(pc, 4112);
            pc.Wig = 255;
            pc.HairStyle = 1;

            Complete(pc);
        }

        void 男性燙頭髮オールバック處理(ActorPC pc)
        {
            pc.Gold -= 10000;

            Say(pc, 11000115, 131, "じゃあ、はじめるよ!!$R;", "ニーベルングのヘアサロン");

            ShowEffect(pc, 4112);
            pc.Wig = 255;
            pc.HairStyle = 6;

            Complete(pc);
        }

        void 男性燙頭髮ワンレングス處理(ActorPC pc)
        {
            pc.Gold -= 10000;

            Say(pc, 11000115, 131, "じゃあ、はじめるよ!!$R;", "ニーベルングのヘアサロン");

            ShowEffect(pc, 4112);
            pc.Wig = 255;
            pc.HairStyle = 3;

            Complete(pc);
        }

        void 女性髮型處理判斷(ActorPC pc)
        {
            if (pc.HairStyle == 14)		//	ベリーショート
            {

                Say(pc, 11000115, 131, "……$R;" +
                                       "$Pなんてことだ!$R;" +
                                       "$Rキミの髪は短すぎて，$R;" +
                                       "どんな髪型にも出来ないよ!$R;" +
                                       "$R髪が伸びたらまたおいで！$R;", "ニーベルングのヘアサロン");
                return;
            }

            Say(pc, 131, "カットするかい？$Rそれともスタイリングかい？$R;" +
                        "$Rカットだと３０００ゴールド$Rスタイリングだと$R１００００ゴールドになるよ。$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする？", "", "カットする", "スタイリングする", "やめておく"))
            {
                case 1:
                    女性髮型カット処理(pc);
                    break;

                case 2:
                    女性髮型セット処理(pc);
                    break;

                case 3:
                    Say(pc, 11000115, 131, "そっか$R;" +
                                           "またおいでよ!$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 女性髮型カット処理(ActorPC pc)
        {
            if (pc.Gold >= 3000)
            {
                if (pc.HairStyle == 0 ||		//ショート
                    pc.HairStyle == 16 ||
                    pc.HairStyle == 18)
                {
                    女性剪頭髮平頭髮型處理(pc);
                    return;
                }

                if (pc.HairStyle == 1 ||		//レイヤー
                    pc.HairStyle == 7 ||		//セミロング
                    pc.HairStyle == 9 ||		//アンティーク
                    pc.HairStyle == 17)
                {
                    女性剪頭髮短髮髮型處理(pc);
                    return;
                }

                if (pc.HairStyle == 2 ||		//ロング
                    pc.HairStyle == 3 ||
                    pc.HairStyle == 4 ||
                    pc.HairStyle == 5 ||
                    pc.HairStyle == 6 ||
                    pc.HairStyle == 8 ||
                    pc.HairStyle == 19 ||
                    pc.HairStyle == 20 ||
                    pc.HairStyle == 21 ||
                    pc.HairStyle == 22 ||
                    pc.HairStyle == 23 ||
                    pc.HairStyle == 24 ||
                    pc.HairStyle == 25)
                {
                    女性剪頭髮半長髮型處理(pc);
                    return;
                }
            }
            else
            {
                Say(pc, 131, "……。$R;" +
                            "$Pあ～ん、残念～$R;" +
                            "君は持ち合わせが足りないようだね？$R;" +
                            "$Rまたおいでよ。$R;", "ニーベルングのヘアサロン");
            }
        }

        void 女性剪頭髮平頭髮型處理(ActorPC pc)
        {
            Say(pc, 11000115, 131, "うん，カットだね?$R;" +
                                   "$Rカットすると，かなり短くなってしまうね。$R;" +
                                   "$Pそうなると後で$R;" +
                                   "他の髪型にかえることが，$R;" +
                                   "できなくなるよ。$R;" +
                                   "それでもいいかな?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "カットする", "やめる"))
            {
                case 1:
                    pc.Gold -= 3000;

                    Say(pc, 11000115, 131, "じゃあ，はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 14;		// ベリーショート

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そう?$R;" +
                                           "またおいでよ!$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 女性剪頭髮短髮髮型處理(ActorPC pc)
        {
            Say(pc, 11000115, 131, "うん，カットだね?$R;" +
                                   "$Rかなり短くなってしまうから$R;" +
                                   "$Pあとでカットできる髪型が$R;" +
                                   "限られちゃうね。$R;" +
                                   "それでもいいかな?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "カットする", "やめる"))
            {
                case 1:
                    pc.Gold -= 3000;

                    Say(pc, 11000115, 131, "じゃあ，はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 0;		// ショート

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そう?$R;" +
                                           "またおいでよ!$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 女性剪頭髮半長髮型處理(ActorPC pc)
        {

            Say(pc, 11000115, 131, "あとでカットできる髪型が$R;" +
                                   "限られちゃうけど、$R;" +
                                   "それでもいいかな?$R;", "ニーベルングのヘアサロン");


            switch (Select(pc, "どうする?", "", "カットする", "やめる"))
            {
                case 1:
                    pc.Gold -= 3000;

                    Say(pc, 11000115, 131, "じゃあ，はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 7;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そう?$R;" +
                                           "またおいでよ!$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 女性髮型セット処理(ActorPC pc)
        {
            if (pc.Gold >= 10000)
            {
                if (pc.HairStyle == 0 ||
                    pc.HairStyle == 16 ||
                    pc.HairStyle == 18)
                {
                    Say(pc, 11000115, 131, "髪が短すぎるから$R" +
                                            "カットだけになるよ$R;" +
                                            "$Rそれでいいかい?$R;", "ニーベルングのヘアサロン");

                    switch (Select(pc, "どうする?", "", "カットする", "やめる"))
                    {
                        case 1:
                            pc.Gold -= 3000;

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 255;
                            pc.HairStyle = 14;

                            Complete(pc);
                            return;

                        case 2:
                            Say(pc, 11000115, 131, "そう?$R;" +
                                                   "またおいでよ!$R;", "ニーベルングのヘアサロン");
                            return;
                    }
                }

                Say(pc, 11000115, 131, "どんな感じにしようか?$R;" +
                                       "$R今の長さに合わせた$R;" +
                                       "髪型になるけど?$R;", "ニーベルングのヘアサロン");

                switch (Select(pc, "どうする?", "", "考える", "やめる"))
                {
                    case 1:
                        if (pc.HairStyle == 1)
                        {
                            switch (Select(pc, "どうする?", "", "肩と同じくらいに揃えて", "前髪ぱっつんにして", "やめる"))
                            {
                                case 1:
                                    女性造型設計セミロング處理(pc);
                                    return;

                                case 2:
                                    女性造型設計アンティーク處理(pc);
                                    return;

                                case 3:
                                    Say(pc, 11000115, 131, "そう?$R;" +
                                                           "またおいでよ!$R;", "ニーベルングのヘアサロン");
                                    return;
                            }
                        }

                        if (pc.HairStyle == 2)
                        {
                            switch (Select(pc, "どうする?", "", "内巻きパーマをかけて", "外巻きパーマをかけて", "ちょっと大人っぽく", "やめる"))
                            {
                                case 1:
                                    女性造型設内巻きヘア處理(pc);
                                    return;

                                case 2:
                                    女性造型設計外巻きヘア處理(pc);
                                    return;

                                case 3:
                                    女性造型設計ナチュラル處理(pc);
                                    return;

                                case 4:
                                    Say(pc, 11000115, 131, "そう?$R;" +
                                                           "またおいでよ!$R;", "ニーベルングのヘアサロン");
                                    return;
                            }
                        }

                        if (pc.HairStyle == 3)
                        {
                            switch (Select(pc, "どうする?", "", "ストレートパーマをかけて", "内巻きパーマをかけて", "ちょっと大人っぽく", "やめる"))
                            {
                                case 1:
                                    女性造型設計ロング處理(pc);
                                    return;

                                case 2:
                                    女性造型設内巻きヘア處理(pc);
                                    return;

                                case 3:
                                    女性造型設計ナチュラル處理(pc);
                                    return;

                                case 4:
                                    Say(pc, 11000115, 131, "そう?$R;" +
                                                           "またおいでよ!$R;", "ニーベルングのヘアサロン");
                                    return;
                            }
                        }

                        if (pc.HairStyle == 4)
                        {
                            switch (Select(pc, "どうする?", "", "ストレートパーマをかけて", "外巻きパーマをかけて", "ちょっと大人っぽく", "やめる"))
                            {
                                case 1:
                                    女性造型設計ロング處理(pc);
                                    return;

                                case 2:
                                    女性造型設計外巻きヘア處理(pc);
                                    return;

                                case 3:
                                    女性造型設計ナチュラル處理(pc);
                                    return;

                                case 4:
                                    Say(pc, 11000115, 131, "そう?$R;" +
                                                           "またおいでよ!$R;", "ニーベルングのヘアサロン");
                                    return;
                            }
                        }

                        if (pc.HairStyle == 7)
                        {
                            switch (Select(pc, "どうする?", "", "全体的に軽くする", "前髪ぱっつんにする", "やめる吧"))
                            {
                                case 1:
                                    女性造型設計レイヤー處理(pc);
                                    return;

                                case 2:
                                    女性造型設計アンティーク處理(pc);
                                    return;

                                case 3:
                                    Say(pc, 11000115, 131, "そう?$R;" +
                                                           "またおいでよ!$R;", "ニーベルングのヘアサロン");
                                    return;
                            }
                        }

                        if (pc.HairStyle == 8)
                        {
                            switch (Select(pc, "どうする?", "", "ストレートパーマをかけて", "内巻きパーマをかけて", "外巻きパーマをかけて", "やめる"))
                            {
                                case 1:
                                    女性造型設計ロング處理(pc);
                                    return;

                                case 2:
                                    女性造型設内巻きヘア處理(pc);
                                    return;

                                case 3:
                                    女性造型設計外巻きヘア處理(pc);
                                    return;

                                case 4:
                                    Say(pc, 11000115, 131, "そう?$R;" +
                                                           "またおいでよ!$R;", "ニーベルングのヘアサロン");
                                    return;
                            }
                        }

                        if (pc.HairStyle == 9)
                        {
                            switch (Select(pc, "どうする?", "", "肩と同じくらいに揃えて", "全体的に軽くする", "やめる"))
                            {
                                case 1:
                                    女性造型設計セミロング處理(pc);
                                    return;

                                case 2:
                                    女性造型設計レイヤー處理(pc);
                                    return;

                                case 3:
                                    Say(pc, 11000115, 131, "そう?$R;" +
                                                           "またおいでよ!$R;", "ニーベルングのヘアサロン");
                                    return;
                            }
                        }

                        if (pc.HairStyle == 5 ||
                            pc.HairStyle == 6 ||
                            pc.HairStyle == 17 ||
                            pc.HairStyle == 19 ||
                            pc.HairStyle == 20 ||
                            pc.HairStyle == 21 ||
                            pc.HairStyle == 22 ||
                            pc.HairStyle == 23 ||
                            pc.HairStyle == 24 ||
                            pc.HairStyle == 25 ||
                            pc.HairStyle > 26)
                        {
                            switch (Select(pc, "どうする?", "", "ストレートパーマをかけて", "内巻きパーマをかけて", "外巻きパーマをかけて", "ちょっと大人っぽく", "やめる"))
                            {
                                case 1:
                                    女性造型設計ロング處理(pc);
                                    return;

                                case 2:
                                    女性造型設内巻きヘア處理(pc);
                                    return;

                                case 3:
                                    女性造型設計外巻きヘア處理(pc);
                                    return;

                                case 4:
                                    女性造型設計ナチュラル處理(pc);
                                    return;

                                case 5:
                                    Say(pc, 11000115, 131, "そう?$R;" +
                                                           "またおいでよ!$R;", "ニーベルングのヘアサロン");
                                    return;
                            }
                        }
                        return;

                    case 2:
                        Say(pc, 11000115, 131, "そう?$R;" +
                                               "またおいでよ!$R;", "ニーベルングのヘアサロン");
                        return;
                }
            }
            else
            {
                Say(pc, 11000115, 131, "……$R;" +
                                    "$Pあ～ん、残念～$R;" +
                                    "君は持ち合わせが足りないようだね？$R;" +
                                    "$Rまたおいでよ。$R;", "ニーベルングのヘアサロン");
            }
        }

        void 女性造型設計レイヤー處理(ActorPC pc)
        {
            pc.Gold -= 10000;

            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

            ShowEffect(pc, 4112);
            pc.HairStyle = 1;
            pc.Wig = 255;

            Complete(pc);
        }

        void 女性造型設計ロング處理(ActorPC pc)
        {
            pc.Gold -= 10000;

            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

            ShowEffect(pc, 4112);
            pc.HairStyle = 2;
            pc.Wig = 255;

            Complete(pc);
        }

        void 女性造型設計外巻きヘア處理(ActorPC pc)
        {
            pc.Gold -= 10000;

            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

            ShowEffect(pc, 4112);
            pc.HairStyle = 3;
            pc.Wig = 255;

            Complete(pc);
        }

        void 女性造型設内巻きヘア處理(ActorPC pc)
        {
            pc.Gold -= 10000;

            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

            ShowEffect(pc, 4112);
            pc.HairStyle = 4;
            pc.Wig = 255;

            Complete(pc);
        }

        void 女性造型設計セミロング處理(ActorPC pc)
        {
            pc.Gold -= 10000;

            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

            ShowEffect(pc, 4112);
            pc.HairStyle = 7;
            pc.Wig = 255;

            Complete(pc);
        }

        void 女性造型設計ナチュラル處理(ActorPC pc)
        {
            pc.Gold -= 10000;

            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

            ShowEffect(pc, 4112);
            pc.HairStyle = 8;
            pc.Wig = 255;

            Complete(pc);
        }

        void 女性造型設計アンティーク處理(ActorPC pc)
        {
            pc.Gold -= 10000;

            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

            ShowEffect(pc, 4112);
            pc.HairStyle = 9;
            pc.Wig = 255;

            Complete(pc);
        }

        void 使用髮型介紹書2(ActorPC pc)
        {
            Say(pc, 11000115, 131, "やあ、こんにちは!$R;" +
                                   "$R「ニーベルングのヘアサロン」へ$Rようこそ。$R;" +
                                   "$Rおや、紹介状があるんだね?$R;", "ニーベルングのヘアサロン");
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

                case 10020759:
                    ルチルのヘアカタログ(pc);
                    break;

                case 10020756:
                    プリティ紹介状(pc);
                    break;

                case 10020750:
                    ヘアサロン特別紹介状(pc);
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
                case 10020780: //デビュー
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
                    Say(pc, 11000115, 131, "そっかぁ$R;" +
                                           "髪型を変えたくなったら、$R;" +
                                           "またおいでよ！$R;", "ニーベルングのヘアサロン");
                    break;
            }


        }

        void 使用髮型介紹書(ActorPC pc)
        {
            Say(pc, 11000115, 131, "やあ、こんにちは!$R;" +
                                   "$R「ニーベルングのヘアサロン」へ$Rようこそ。$R;" +
                                   "$Rおや、紹介状があるんだね?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どの紹介状を使う?", "", "ヘアサロンの紹介状", "セレブの紹介状", "ゴージャス紹介状", "おめかし紹介状", "おてんば紹介状", "姫さまの紹介状", "ちょいかわ紹介状", "ルチルのヘアカタログ", "プリティ紹介状", "ヘアサロン特別紹介状", "機械時代のヘアカタログ", "ドミニオンのヘアカタログ", "やめる"))
            {
                case 1:
                    if (CountItem(pc, 10020708) > 0)
                    {
                        ヘアサロンの紹介状(pc);
                    }
                    else
                    {
                        NoItem(pc, "ヘアサロンの紹介状");
                        //Say(pc, 11000115, 131, "おや?!$R;" +
                        //                       "$P『ヘアサロンの紹介状』を持ってないようだね?$R;", "ニーベルングのヘアサロン");                   
                    }
                    break;

                case 2:
                    if (CountItem(pc, 10020706) > 0)
                    {
                        セレブの紹介状(pc);
                    }
                    else
                    {
                        NoItem(pc, "セレブの紹介状");
                    }
                    break;

                case 3:
                    if (CountItem(pc, 10020764) > 0)
                    {
                        ゴージャス紹介状(pc);
                    }
                    else
                    {
                        NoItem(pc, "ゴージャス紹介状");
                    }
                    break;

                case 4:
                    if (CountItem(pc, 10020766) > 0)
                    {
                        おめかし紹介状(pc);
                    }
                    else
                    {
                        NoItem(pc, "おめかし紹介状");
                    }
                    break;

                case 5:
                    if (CountItem(pc, 10020765) > 0)
                    {
                        おてんば紹介状(pc);
                    }
                    else
                    {
                        NoItem(pc, "おてんば紹介状");
                    }
                    break;

                case 6:
                    if (CountItem(pc, 10020767) > 0)
                    {
                        姫さまの紹介状(pc);
                    }
                    else
                    {
                        NoItem(pc, "姫さまの紹介状");
                    }
                    break;

                case 7:
                    if (CountItem(pc, 10020761) > 0)
                    //if (CountItem(pc, 26000021) > 0)
                    {
                        ちょいかわ紹介状(pc);
                    }
                    else
                    {
                        NoItem(pc, "ちょいかわ紹介状");
                    }
                    break;

                case 8:
                    //if (CountItem(pc, 26000022) > 0)
                    if (CountItem(pc, 10020759) > 0)
                    {
                        ルチルのヘアカタログ(pc);
                    }
                    else
                    {
                        NoItem(pc, "ルチルのヘアカタログ");
                    }
                    break;

                case 9:
                    if (CountItem(pc, 10020756) > 0)
                    {
                        プリティ紹介状(pc);
                    }
                    else
                    {
                        NoItem(pc, "プリティ紹介状");
                    }
                    break;

                case 10:
                    if (CountItem(pc, 10020750) > 0)
                    {
                        ヘアサロン特別紹介状(pc);
                    }
                    else
                    {
                        NoItem(pc, "ヘアサロン特別紹介状");
                    }
                    break;

                case 11:
                    if (CountItem(pc, 10020760) > 0)
                    {
                        機械時代のヘアカタログ(pc);
                    }
                    else
                    {
                        NoItem(pc, "機械時代のヘアカタログ");
                    }
                    break;

                case 12:
                    //if (CountItem(pc, 26000021) > 0)
                    if (CountItem(pc, 10020755) > 0)
                    {
                        ドミニオンのヘアカタログ(pc);
                    }
                    else
                    {
                        NoItem(pc, "ドミニオンのヘアカタログ");
                    }
                    break;

                case 13:
                    Say(pc, 11000115, 131, "そっかぁ$R;" +
                                           "髪型を変えたくなったら、$R;" +
                                           "またおいでよ！$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void ヘアサロンの紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "わぁ～お♪$R;" +
                                   "君は『ヘアサロンの紹介状』を$R;" +
                                   "持ってるのかい！？$R;" +
                                   "$Rじゃあ君は$R特別なお客様だよ！$R;" +
                                   "$P僕のとっておきのテクニックで$R;" +
                                   "君の髪にスペシャルでフェミニングな$R;" +
                                   "セットをしてあげる！$R;" +
                                   "$Pどうだい？$R;" +
                                   "『ヘアサロンの紹介状』を使って$R;" +
                                   "君の髪をセットしてみないかい？", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする？", "", "紹介状を使う", "使わない"))
            {
                case 1:
                    switch (Select(pc, "どうする？", "", "一つに結んでください", "アップで三つ編みにしてください", "三つ編みにしてください", "アップできつく三つ編みにしてください", "きつく三つ編みにしてください", "アップで結んでゴムでまとめてください", "結んでゴムでまとめてください", "アップでお団子にしてください", "お団子にしてください", "やめておきます"))
                    {
                        case 1:
                            TakeItem(pc, 10020708, 1);

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 6;
                            pc.HairStyle = 6;

                            Complete(pc);
                            break;

                        case 2:
                            TakeItem(pc, 10020708, 1);

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 1;
                            pc.HairStyle = 5;

                            Complete(pc);
                            break;

                        case 3:
                            TakeItem(pc, 10020708, 1);

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 0;
                            pc.HairStyle = 6;

                            Complete(pc);
                            break;

                        case 4:
                            TakeItem(pc, 10020708, 1);

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 13;
                            pc.HairStyle = 5;

                            Complete(pc);
                            break;

                        case 5:
                            TakeItem(pc, 10020708, 1);

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 12;
                            pc.HairStyle = 6;

                            Complete(pc);
                            break;

                        case 6:
                            TakeItem(pc, 10020708, 1);

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 10;
                            pc.HairStyle = 5;

                            Complete(pc);
                            break;

                        case 7:
                            TakeItem(pc, 10020708, 1);

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 9;
                            pc.HairStyle = 6;

                            Complete(pc);
                            break;

                        case 8:
                            TakeItem(pc, 10020708, 1);

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 16;
                            pc.HairStyle = 5;

                            Complete(pc);
                            break;

                        case 9:
                            TakeItem(pc, 10020708, 1);

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 15;
                            pc.HairStyle = 6;

                            Complete(pc);
                            break;

                        case 10:
                            Say(pc, 11000115, 131, "そう?$R;" +
                                                   "またおいでよ!$R;", "ニーベルングのヘアサロン");
                            break;
                    }
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そっかぁ、残念だなぁ。$R;" +
                                           "またおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void セレブの紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "おや!!" +
                                   "$Rお客さんは……$R;" +
                                   "『セレブの紹介状』をお持ちなので?$R;" +
                                   "$Pこれは光栄です。$R;" +
                                   "特別にあなたの髪型を$Rセットしてあげましょうか?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "『セレブの紹介状』を使う", "使わない"))
            {
                case 1:
                    セレブの紹介状第一頁(pc);
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そうですか、$R;" +
                                           "スタイリングご希望の際には、$R;" +
                                           "またお越しください！$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        private void セレブComplete(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほほ、とても綺麗ですよ!!$R;" +
                //"$R嘻嘻! 不好意思失禮了。$R;" +
                                   "$Pまたのお越しを!$R;", "ニーベルングのヘアサロン");
        }

        void セレブの紹介状第一頁(ActorPC pc)
        {
            switch (Select(pc, "どんな髪型にする?", "", "アップで三つ編みにしてリボンをつけて", "三つ編みにしてリボンをつけて", "右で三つ編みにしてリボンをつけて", "左で三つ編みにしてリボンをつけて", "右サイドで結んで", "左サイドで結んで", "右で三つ編みにして", "左で三つ編みにして", "他にはないの?", "やめる"))
            {
                case 1:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 4;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 2:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 3;
                    pc.HairStyle = 6;

                    セレブComplete(pc);
                    break;

                case 3:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 23;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 4:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 22;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 5:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 25;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 6:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 24;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 7:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 23;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 8:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 22;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 9:
                    セレブの紹介状第二頁(pc);
                    break;

                case 10:
                    Say(pc, 11000115, 131, "そうですか、$R;" +
                                           "セットをご希望の際には、$R;" +
                                           "またお越しください！$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void セレブの紹介状第二頁(ActorPC pc)
        {
            switch (Select(pc, "どんな髪型にする?", "", "両サイドできつく三つ編みに", "左できつく三つ編みに", "右できつく三つ編みに", "ポニーテールにして", "両サイドでお団子に", "左でお団子に", "右でお団子に", "他にはないの?", "前に戻る", "やめる"))
            {
                case 1:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 14;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 2:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 28;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 3:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 29;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 4:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 7;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 5:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 17;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 6:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 30;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 7:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 31;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 8:
                    セレブの紹介状第三頁(pc);
                    break;

                case 9:
                    セレブの紹介状第一頁(pc);
                    break;

                case 10:
                    Say(pc, 11000115, 131, "そうですか、$R;" +
                                           "セットをご希望の際には、$R;" +
                                           "またお越しください！$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void セレブの紹介状第三頁(ActorPC pc)
        {
            switch (Select(pc, "どんな髪型にする?", "", "両サイドでお団子三つ編みに", "左でお団子三つ編みに", "右でお団子三つ編みに", "サイドで結んでヘアゴムでまとめて", "左で結んでヘアゴムでまとめて", "右で結んでヘアゴムでまとめて", "他にはないの?", "前に戻る", "やめる"))
            {
                case 1:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 18;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 2:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 32;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 3:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 33;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 4:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 11;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 5:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 26;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 6:
                    TakeItem(pc, 10020706, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 27;
                    pc.HairStyle = 5;

                    セレブComplete(pc);
                    break;

                case 7:
                    Say(pc, 11000115, 131, "申し訳ありません。$R;" +
                                           "$R私の技術ではこれが精一杯です。$R;", "ニーベルングのヘアサロン");
                    break;

                case 8:
                    セレブの紹介状第二頁(pc);
                    break;

                case 9:
                    Say(pc, 11000115, 131, "そうですか、$R;" +
                                           "セットをご希望の際には、$R;" +
                                           "またお越しください！$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void ゴージャス紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!!$R;" +
                                   "$R『ゴージャス紹介状』かい?!$R;" +
                                   "$Pよく来たね!$R;" +
                                   "$R『ゴージャス紹介状』があれば、$R;" +
                                   "キミの髪型を高貴に見せられるよ?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "『ゴージャス紹介状』を使う", "使わない"))
            {
                case 1:
                    switch (Select(pc, "どんな髪型にする?", "", "縦ロール", "正統派おさげ", "やめる"))
                    {
                        case 1:
                            TakeItem(pc, 10020764, 1);

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 39;
                            pc.HairStyle = 21;

                            Complete(pc);
                            break;

                        case 2:
                            TakeItem(pc, 10020764, 1);

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 255;
                            pc.HairStyle = 22;

                            Complete(pc);
                            break;

                        case 3:
                            Say(pc, 11000115, 131, "そっか、$R;" +
                                                   "髪型を変えたくなったら$R;" +
                                                   "またおいでよ!$R;", "ニーベルングのヘアサロン");
                            break;
                    }
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そっか。$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void おめかし紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!!$R;" +
                                   "$R『おめかし紹介状』かい?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『おめかし紹介状』で$R;" +
                                   "試してみないかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "ロングツイン", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020766, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 41;
                    pc.HairStyle = 24;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そっか。$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void おてんば紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『おてんば紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊をかけて$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『おてんば紹介状』で$R;" +
                                   "試してみないかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "元気のいい妹の髪形", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020765, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 40;
                    pc.HairStyle = 23;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そっか。$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 姫さまの紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『姫さまの紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『姫さまの紹介状』で$R;" +
                                   "試してみないかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "姫さまの髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020767, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 25;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そっか。$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void ちょいかわ紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "へ～!!$R;" +
                                   "$R『ちょいかわ紹介状』だね?$R;" +
                                   "$Pどうだい?$R;" +
                                   "$R『ちょいかわ紹介状』で、$R;" +
                                   "キミの髪をさわやかに決めてあげるよ?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "『ちょいかわ紹介状』を使う", "使わない"))
            {
                case 1:
                    switch (Select(pc, "どんな髪型にする?", "", "ショート＆エクステ", "スポーティーポニー", "やめる"))
                    {
                        case 1:
                            TakeItem(pc, 10020761, 1);

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 38;
                            pc.HairStyle = 0;

                            Complete(pc);
                            break;

                        case 2:
                            TakeItem(pc, 10020761, 1);

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 37;
                            pc.HairStyle = 20;

                            Complete(pc);
                            break;

                        case 3:
                            Say(pc, 11000115, 131, "そっか、$R;" +
                                                   "髪型を変えたくなったら$R;" +
                                                   "またおいでよ!$R;", "ニーベルングのヘアサロン");
                            break;
                    }
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そっか。$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void ルチルのヘアカタログ(ActorPC pc)
        {
            Say(pc, 11000115, 131, "わぁ～!$R;" +
                                   "$R『ルチルのヘアカタログ』だね?$R;" +
                                   "$P「ルチル」はきれいだよね?$R;" +
                                   "彼女はここの常連客なんだ!$R;" +
                                   "$R月に一度は来るね。$R;" +
                                   "$Pどうする?$R;" +
                                   "$R『ルチルのヘアカタログ』で$R;" +
                                   "試してみる?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "ルチルの髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020759, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 36;
                    pc.HairStyle = 19;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そっか。$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void プリティ紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "わぁ～!$R;" +
                                   "$R『プリティ紹介状』だね?$R;" +
                                   "$Pこれは特別なお客さんだ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうする?$R;" +
                                   "この『プリティ紹介状』で、$R;" +
                                   "試してみる?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "『プリティ紹介状』を使う", "使わない"))
            {
                case 1:
                    switch (Select(pc, "どんな髪型にする?", "", "ショートツインテール", "外はねぼぶ", "やめる"))
                    {
                        case 1:
                            TakeItem(pc, 10020756, 1);

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 35;
                            pc.HairStyle = 7;

                            Complete(pc);
                            break;

                        case 2:
                            TakeItem(pc, 10020756, 1);

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 255;
                            pc.HairStyle = 18;

                            Complete(pc);
                            break;

                        case 3:
                            Say(pc, 11000115, 131, "よく考えて、またおいで!$R;", "ニーベルングのヘアサロン");
                            break;
                    }
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そっか。$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void ヘアサロン特別紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!!$R;" +
                                   "$R『ヘアサロン特別紹介状』だね?$R;" +
                                   "$Pこれは特別なお客さんだ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうする?$R;" +
                                   "『ヘアサロン特別紹介状』で、$R;" +
                                   "試してみる?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "『ヘアサロン特別紹介状』を使う", "使わない"))
            {
                case 1:
                    switch (Select(pc, "どんな髪型にする?", "", "特別なポニー", "ツインテール", "やめる"))
                    {
                        case 1:
                            TakeItem(pc, 10020750, 1);

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 19;
                            pc.HairStyle = 5;

                            Complete(pc);
                            break;

                        case 2:
                            TakeItem(pc, 10020750, 1);

                            Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                            ShowEffect(pc, 4112);
                            pc.Wig = 8;
                            pc.HairStyle = 5;

                            Complete(pc);
                            break;

                        case 3:
                            Say(pc, 11000115, 131, "よく考えて、またおいで!$R;", "ニーベルングのヘアサロン");
                            break;
                    }
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そっか。$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 機械時代のヘアカタログ(ActorPC pc)
        {
            Say(pc, 11000115, 131, "それは!?$R;" +
                                   "$R『機械時代のヘアカタログ』だって?$R;" +
                                   "$Pこんな昔の髪型にするのかい?$R;" +
                                   "冗談のつもりかい?$R;" +
                                   "$Rこの髪型は斬新だって?$R;" +
                                   "$Pえ、できるかって!$R;" +
                                   "$Rん、悪くない悪くない!$R;" +
                                   "いい!　とてもいいよ!$R;" +
                //"$P太好了$R;" +
                                   "$Rどうする?$R;" +
                                   "このカタログの髪型を?$R;" +
                                    "試してみる?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "機械時代の髪型（女の子）", "使わない"))
            {
                case 1:
                    if (pc.HairStyle != 14 &&
                        pc.HairStyle != 0)
                    {
                        TakeItem(pc, 10020760, 1);

                        Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                        ShowEffect(pc, 4112);
                        pc.Wig = 255;
                        pc.HairStyle = 17;

                        Complete(pc);
                    }
                    else
                    {
                        Say(pc, 11000115, 131, "この髪型にするには、$R;" +
                                               "もっと髪が長くないとね。$R;" +
                                               "$Rまた今度おいでよ?$R;", "ニーベルングのヘアサロン");
                    }
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そっか。$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void ドミニオンのヘアカタログ(ActorPC pc)
        {
            Say(pc, 11000115, 131, "なんだい、これは？$R;" +
                                   "$P……。$R;" +
                                   "$Pわぁ～～～～～～～～～～～～～ぉ！$R;" +
                                   "$Rなんって斬新！$R;" +
                                   "なんってトロピカル！$R;" +
                                   "なんってドラマチック！$R;" +
                                   "$P不思議……なぜか涙が止まらないよ！$R;" +
                                   "$Rどっ、どうだろう？$R;" +
                                   "このヘアカタログに載っている髪型を$R;" +
                                   "試してみないかい？$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする？", "", "お願いする", "やめておく"))
            //			switch (Select(pc, "どうする?", "", "ルルイエの髪型", "使わない"))
            {
                case 1:
                    if (pc.HairStyle != 14 &&
                        pc.HairStyle != 0)
                    {
                        TakeItem(pc, 10020755, 1);

                        Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                        ShowEffect(pc, 4112);
                        pc.Wig = 255;
                        pc.HairStyle = 16;

                        Complete(pc);
                    }
                    else
                    {
                        Say(pc, 11000115, 131, "この髪型にするには、$R;" +
                                               "もっと髪が長くないとね。$R;" +
                                               "$Rまた今度おいでよ?$R;", "ニーベルングのヘアサロン");
                    }
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そっか。$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }

        }

        void ラブモテ紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『ラブモテ紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『ラブモテ紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "新しい出会いを予感させる髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020768, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 26;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void クラシックな紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『クラシックな紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『クラシックな紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "片側ロールヘアー", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020770, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 43;
                    pc.HairStyle = 29;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void プリンセスの紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『プリンセスの紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『プリンセスの紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "プリンスの髪型", "プリンセスの髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020769, 1);

                    //Start(pc);

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 28;

                    Complete(pc);
                    break;

                case 2:
                    TakeItem(pc, 10020769, 1);

                    //Start(pc);

                    ShowEffect(pc, 4112);
                    pc.Wig = 45;
                    pc.HairStyle = 27;

                    Complete(pc);
                    break;

                case 3:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またおいでよ!$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 少佐の紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『少佐の紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『少佐の紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "草薙素子の髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020771, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 30;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったら$R;" +
                                           "またおいでよ!$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void ふたご座の紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『ふたご座の紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『ふたご座の紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "後ろで髪の一部を結ったロングヘア", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020773, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 44;
                    pc.HairStyle = 31;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void ラテンの紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『ラテンの紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『ラテンの紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "情熱的なアップ", "情熱的なショート", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020774, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 46;
                    pc.HairStyle = 32;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020774, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 34;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 魔女の紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『魔女の紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『魔女の紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "アンニュイ", "ヘアピン", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020775, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 35;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020775, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 36;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void ワイルドな紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『ワイルドな紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『ワイルドな紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "ワイルドショート", "ワイルドミディアム", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020776, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 37;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020776, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 38;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 武家の紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『武家の紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『武家の紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "小花の髪飾り付きのアップヘア", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020777, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 49;
                    pc.HairStyle = 39;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void バレンタイン紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『バレンタイン紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『バレンタイン紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "かわいいツーサイドアップ", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020779, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 50;
                    pc.HairStyle = 40;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void スイートな紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『スイートな紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『スイートな紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "シュークリームボブ", "ロールツインテール", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020782, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 42;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020782, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 51;
                    pc.HairStyle = 43;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void デビューへの紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『デビューへの紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『デビューへの紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "優等生ぽいロングヘアー", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020780, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 41;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void おとぎの紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『おとぎの紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『おとぎの紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "くりんくりんショート", "ゆる三つ編み", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020783, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 45;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020783, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 44;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }


        void マネージャーの紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『マネージャーの紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『マネージャーの紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "なまいきポニー", "世話焼きショート", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020785, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 54;
                    pc.HairStyle = 49;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020785, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 48;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void ひぐらしの紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『ひぐらしの紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『ひぐらしの紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "梨花の髪型", "沙都子の髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020786, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 53;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020786, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 52;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 空賊の紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『空賊の紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『空賊の紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "キャリーの髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020787, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 54;
                    pc.HairStyle = 55;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 歌姫の紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『歌姫の紹介状紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『歌姫の紹介状紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "歌姫らしいふわふわロングヘアー", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020784, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 47;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 真夏の紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『真夏の紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『真夏の紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "ロングのミニツイン付きヘアー", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020788, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 56;
                    pc.HairStyle = 56;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 委員長の紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『委員長の紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『委員長の紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "イケてるベリーショート", "イケてるボブ", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020789, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 58;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020789, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 57;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 貴族の紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『貴族の紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『貴族の紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "儚げなくるくるショート", "麗しのミニ縦ロール", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020790, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 59;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020790, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 60;
                    pc.HairStyle = 60;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void レナの紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『レナの紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『レナの紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ヘルヘイムのヘアサロン");

            switch (Select(pc, "どうする?", "", "竜宮レナの髪形", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020791, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ヘルヘイムのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 54;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void ハロウィン紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『ハロウイン紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『ハロウイン紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "リボン付きツインテール", "リボンなし", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020792, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 58;
                    pc.HairStyle = 61;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020792, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 59;
                    pc.HairStyle = 61;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 真冬の紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『真冬の紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『真冬の紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "憧れのストレートロングヘア", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020793, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 62;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void ハイカラ紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『ハイカラ紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『ハイカラ紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "ボーイッシュなさらさらショート", "清楚なストレートロング（ウィッグあり）", "清楚なストレートロング（ウィッグなし）", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020794, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 71;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020794, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 63;
                    pc.HairStyle = 72;
                    Complete(pc);
                    break;
                case 3:
                    TakeItem(pc, 10020794, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 72;
                    Complete(pc);
                    break;
                case 4:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void アルバイト紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『アルバイト紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『アルバイト紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "清潔感のあるミディアム", "清潔感のあるショート", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020795, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 73;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020795, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 63;
                    pc.HairStyle = 74;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 軍式紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『軍式紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『軍式紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "風格あるロングおさげ", "半人前ショートおさげ", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020796, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 75;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020796, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 76;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 伝説の紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "わぁ～お♪$R;" +
                                   "君は『伝説の紹介状』を$R;" +
                                   "持っているのかい！？$R;" +
                                   "$P憧れるよね、おとぎ話の$R;" +
                                   "主人公のようになってみたいってね。$R;" +
                                   "$Rボクもね、そんな事を$R;" +
                                   "夢見ていた時期があったのさ……。$R;" +
                                   "$Pさあ、この話はおしまいだ！$R;" +
                                   "$R『伝説の紹介状』を使って$R;" +
                                   "君の髪をセットしてみるかい？$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "おとぎ話の主人公風ショート", "おとぎ話の主人公風ロング", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10020797, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 77;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10020797, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 78;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 愛され紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "わぁ～お♪$R;" +
                                   "君は『愛され紹介状』を$R;" +
                                   "持っているのかい！？$R;" +
                                   "$P僕のテクニックで$R;" +
                                   "誰からでも愛される髪型に$R;" +
                                   "してあげるよ！$R;" +
                                   "$R『愛され紹介状』を使って$R;" +
                                   "君の髪をセットしてみるかい？$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "マニッシュボブ", "愛されポニー（リボンつき）", "愛されポニー（リボンなし）", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10074400, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 79;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10074400, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 66;
                    pc.HairStyle = 82;
                    Complete(pc);
                    break;
                case 3:
                    TakeItem(pc, 10074400, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 64;
                    pc.HairStyle = 82;
                    Complete(pc);
                    break;
                case 4:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 夏休みの紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『夏休みの紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『夏休みの紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "ワイルドオールバック", "ニードルショート", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10074800, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 68;
                    pc.HairStyle = 84;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10074800, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 85;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void ふたご座の紹介状2(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『ふたご座の紹介状☆』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『ふたご座の紹介状☆』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "後ろで髪の一部を結ったセミロングヘア", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10075200, 1);

                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");

                    ShowEffect(pc, 4112);
                    pc.Wig = 44;
                    pc.HairStyle = 89;

                    Complete(pc);
                    break;

                case 2:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void ロビン・グッドフェロウの紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『ロビンの紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『ロビンの紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "カチューシャあり", "カチューシャなし", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10075201, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 69;
                    pc.HairStyle = 87;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10075201, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 69;
                    pc.HairStyle = 88;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 厳島美晴の紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『厳島 美晴の紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『厳島 美晴の紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "厳島 美晴の髪型", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10075202, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 86;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void アングリー紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『アングリー紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『アングリー紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "スイートドーリーボブ", "ドーリーカールヘア（カチューシャあり）", "ドーリーカールヘア（カチューシャなし）", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10075203, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 98;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10075203, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 70;
                    pc.HairStyle = 93;
                    Complete(pc);
                    break;
                case 3:
                    TakeItem(pc, 10075203, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 71;
                    pc.HairStyle = 93;
                    Complete(pc);
                    break;
                case 4:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void コウモリタ紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『コウモリタ紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『コウモリタ紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "クラシカルマッシュ", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10075204, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 94;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void ハングリー紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『ハングリー紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『ハングリー紹介状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "斬新なモヒカン", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10075205, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 95;
                    Complete(pc);
                    break;
                case 2:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }

        void 西洋ロマンの紹介状(ActorPC pc)
        {
            Say(pc, 11000115, 131, "ほう!$R;" +
                                   "$R『西洋ロマンの紹介状』だね?$R;" +
                                   "$Pこれはこれは…$R;" +
                                   "大変珍しいお客様だ!$R;" +
                                   "$P私の全身全霊を持って、$R;" +
                                   "キミを特別な髪型にしてあげるよ!$R;" +
                                   "$Pどうかな?$R;" +
                                   "$Rその『西洋ロマンの紹介状状』で$R;" +
                                   "髪型を変えてみるかい?$R;", "ニーベルングのヘアサロン");

            switch (Select(pc, "どうする?", "", "ナチュラルロングウェーブ", "シャギーハイレイヤー", "使わない"))
            {
                case 1:
                    TakeItem(pc, 10075206, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 99;
                    Complete(pc);
                    break;
                case 2:
                    TakeItem(pc, 10075206, 1);
                    Say(pc, 11000115, 131, "じゃあ、はじめるよ!$R;", "ニーベルングのヘアサロン");
                    ShowEffect(pc, 4112);
                    pc.Wig = 255;
                    pc.HairStyle = 100;
                    Complete(pc);
                    break;
                case 3:
                    Say(pc, 11000115, 131, "そっか、$R;" +
                                           "髪型を変えたくなったらまたおいでよ。$R;", "ニーベルングのヘアサロン");
                    break;
            }
        }



    }
}
