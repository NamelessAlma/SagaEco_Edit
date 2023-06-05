using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

using SagaLib;
namespace SagaScript.M50072000
{
    public class S18000247 : Event
    {
        public S18000247()
        {
            this.EventID = 18000247;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DEMNewbie> newbie = new BitMask<DEMNewbie>(pc.CMask["DEMNewbie"]);

            Say(pc, 131, "初めまして…かな？$R;" +
            "$P本当のことをいうと$R;" +
            "これが初めてじゃないんだけどねぇ～。$R;" +
            "$R壊れてた君を直した時、$R;" +
            "もう会ってるから。$R;" +
            "$P動かなくなったＤＥＭが発見されたって$R;" +
            "聞いた時には驚いたよ。$R;" +
            "$R何でエミル界にＤＥＭがいるのかな…$R;" +
            "ってね。$R;" +
            "$Pクォーク博士から$R;" +
            "「修理を手伝ってくれないか？」$R;" +
            "と話がきたんだ。$R;" +
            "$P自慢じゃないけどＤＥＭに関してなら、$R;" +
            "あたい以上詳しいやつはいないからね！$R;" +
            "$P…ああ、ごめん、ごめん、$R;" +
            "自己紹介がまだだったね。$R;" +
            "$Pあたいは、$R;" +
            "人呼んで「ＤＥＭ何でも博士」！$R;" +
            "$R…でも、それだとちょっと長くて$R;" +
            "覚えにくいから、$R;" +
            "略して「ＤＥＭ博士」でいいよ。$R;" +
            "$P君が目覚めたと聞いたときは、$R;" +
            "本当に…本当に嬉しかったよ。$R;" +
            "$Pでも君のための設備の復旧作業が$R;" +
            "忙しすぎて話かける暇もなかったんだ。$R;" +
            "$P折角目覚めたっていうのに、$R;" +
            "もし問題でも発生したら未完成のままだと$R;" +
            "どうしようもないからね。$R;" +
            "$Pいつでもここで君を待っているから、$R;" +
            "必要な時には遠慮なく帰ってきてね。$R;" +
            "$Pあ、そうそう！$R;" +
            "$Pクォーク博士から君に渡してもらった$R;" +
            "「ＤＥＭａｎｕａｌ」だけど、$R;" +
            "これを読めば見聞が広がると思うよ！$R;" +
            "$P具体的には、読めば読むほど$R;" +
            "ＥＰを得られると思うから、$R;" +
            "ちゃーんと一読しておいてね！$R;", "ＤＥＭ何でも博士");

            switch (Select(pc, "それじゃあ、何を話そうかな……", "", "お話がしたい", "ＤＥＭの基礎に関して知りたい", "カスタマイズの説明が聞きたい", "ＤＥＭの特徴と制限が知りたい", "話を終える"))
            {
                case 1:
                    Say(pc, 131, "ノーマルフォームはね、簡単に言えば、$R;" +
                    "ＤＥＭの人間型って感じだね。$R;" +
                    "$Pドミニオン界やエミル界にＤＥＭを$R;" +
                    "スパイとして送るために用意された$R;" +
                    "フォームだとも言われているけど…$R;" +
                    "実際はどうなんだろうね。$R;" +
                    "$Pノーマルフォームの特徴は、$R;" +
                    "種族専用装備を除く、$Rほとんどの武器や防具を$R;" +
                    "装備できるってとこかな。$R;" +
                    "$Rもちろん、装備レベル制限は$R;" +
                    "受けるけどね。$R;" +
                    "$Pだけど万能なわけじゃなくて、$R;" +
                    "ノーマルフォームは戦闘に向いてないの。$R;" +
                    "$P諸説はともかく、非戦闘用に用意された$R;" +
                    "フォームっぽいから、$R;" +
                    "ＤＥＭ本体に負担がかからないよう、$R;" +
                    "ＤＥＭＩＣに装着した各種チップに$R;" +
                    "リミッターが作動してしまうよ。$R;" +
                    "$Pステータス上昇効果が全てゼロになり、$R;" +
                    "スキルチップ装着により追加された$R;" +
                    "所持スキルも全く使えないんだ。$R;" +
                    "$Pつまり装備品の持つ能力のみで$R;" +
                    "戦わなければいけないから、$R;" +
                    "戦闘には向かないフォームなわけ。$R;" +
                    "$R戦闘時はマシナフォームになることを$R;" +
                    "忘れないでね！$R;" +
                    "$Pちなみにフォームを変えたい時は、$R;" +
                    "自分にカーソルを合わせて右クリック、$R;" +
                    "そこで表示されるメニューの中から、$R;" +
                    "「フォームチェンジ」を選択すればＯＫ！$R;" +
                    "$Rどこでも簡単に$R;" +
                    "フォームチェンジができるんだ。$R;", "ＤＥＭ何でも博士");
                    break;
                case 2:
                    {
                        int sel2;
                        do
                        {
                            sel2 = Select(pc, "何の話をしようかな……", "", "ＤＥＭＩＣとＥＰについて", "ステータスチップについて", "スキルチップについて", "リミテーションチップについて", "エンゲージタスクについて", "フォームについて", "他のことを聞きたい");
                            switch (sel2)
                            {
                                case 1:
                                    Say(pc, 131, "ＤＥＭを形成している様々な回路の中でも$R;" +
                                    "ＤＥＭＩＣは一番中心になる回路だよ。$R;" +
                                    "$RＤＥＭＩＣは複数の「パネル」で$R;" +
                                    "構成されているの。$R;" +
                                    "$Pそして各パネルに、$R;" +
                                    "ステータスチップ$R;" +
                                    "スキルチップ$R;" +
                                    "リミテーションチップ$R;" +
                                    "を装着することにより$R;" +
                                    "ＤＥＭは強くなれる！$R;" +
                                    "$PＤＥＭＩＣのマスの数を$R;" +
                                    "「コストリミット」っていうんだけど、$R;" +
                                    "コストリミットを上昇させることこそが、$R;" +
                                    "ＤＥＭの成長の基本だよ。$R;" +
                                    "$P一枚のＤＥＭＩＣパネルは$R;" +
                                    "９×９マスまで拡張できるんだ。$R;" +
                                    "$Rそれ以上拡張すると、$R;" +
                                    "次のＤＥＭＩＣパネルが出現するよ。$R;" +
                                    "$Pただし、コストリミットを上げるには、$R;" +
                                    "ＥＰを消費するから、たくさんのＥＰを$R;" +
                                    "集めなくてはならないだ。$R;" +
                                    "$Pそこで、ＥＰとは何かというとね……。$R;" +
                                    "$PＥＰは他人との絆から生まれる力、$R;" +
                                    "大切な仲間を想うこころの力、$R;" +
                                    "または絆そのものを意味するよ。$R;" +
                                    "$P元々、ＤＥＭは回路を取り替えて$R;" +
                                    "コストリミットを上昇させるんだけど、$R;" +
                                    "君みたいに「こころ」を宿したＤＥＭは、$R;" +
                                    "その「こころ」を大事に育てることにより$R;" +
                                    "コストリミットが上がるみたい。$R;" +
                                    "$P不思議だよね？$R;" +
                                    "純粋な「こころ」の力が、$R;" +
                                    "具現化するなんて…$R;" +
                                    "$Rまだまだ研究すべきことだらけだよ。$R;", "ＤＥＭ何でも博士");
                                    break;
                                case 3:
                                    Say(pc, 131, "熟練した戦士の動きや、$R;" +
                                    "特定の魔法スキル、$R;" +
                                    "それを一つの回路として仕上げた代物を$R;" +
                                    "スキルチップと言うんだ。$R;" +
                                    "$PＤＥＭＩＣにスキルチップを装着すると$R;" +
                                    "君はそのスキルを使えるようになるよ。$R;" +
                                    "$PＤＥＭの施設には、$R;" +
                                    "様々な世界から集められたデータが$R;" +
                                    "保存されているライブラリーがあって、$R;" +
                                    "殆どのスキルチップの製造が$R;" +
                                    "可能だとも噂されているけどね…。$R;" +
                                    "$Pここだと設備も限られてるし、$R;" +
                                    "あたいとクォーク博士が$R;" +
                                    "ゼロから作り上げるしか方法がないから$R;" +
                                    "試行錯誤の連続だよ。$R;" +
                                    "$Pもしスキルチップが欲しかったら、$R;" +
                                    "もとになる戦闘データ（ＪＯＢＥＸＰ）を$R;" +
                                    "集めてきてね！$R;" +
                                    "$P高性能のスキルチップほど、$R;" +
                                    "作成するのに多くのＪＯＢＥＸＰが$R;" +
                                    "必要になるよ。$R;" +
                                    "$Pあ、スキルチップも君自身のデータから$R;" +
                                    "作成するものだから、$R;" +
                                    "他のＤＥＭには使えないし、$R;" +
                                    "トレードもできないことを忘れないでね。$R;" +
                                    "$Pあと、各スキルには最大レベルがあるから$R;" +
                                    "スキルチップを最大レベルより多く$R;" +
                                    "装着しても意味がないよ。$R;" +
                                    "$Rスキルの最大レベルを超えて$R;" +
                                    "スキルチップを作らないようにね！$R;", "ＤＥＭ何でも博士");
                                    break;
                                case 4:
                                    Say(pc, 131, "リミテーションチップは、$R;" +
                                    "ＨＰやＳＰなどの基本能力値や、$R;" +
                                    "複数の基本能力値を同時に上昇させる、$R;" +
                                    "極めて高性能なチップだよ。$R;" +
                                    "$P高性能なのは良いんだけど、$R;" +
                                    "リミテーションチップは$R;" +
                                    "チップの装着制限数が１しかないんだ。$R;" +
                                    "$Pつまり、１人のＤＥＭに１つまでしか$R;" +
                                    "装着できないから、$R;" +
                                    "自分の方向性にあったものを装着してね。$R;", "ＤＥＭ何でも博士"); 
                                    break;
                                case 5:
                                    Say(pc, 131, "ＤＥＭＩＣのパネルを開放した際、$R;" +
                                    "変なブロックが出現することがあるよ。$R;" +
                                    "$Pそのブロックのことを、$R;" +
                                    "「エンゲージタスク」と呼ぶの。$R;" +
                                    "$P…これが何で発生するのかは、$R;" +
                                    "わかってないんだけどね。$R;" +
                                    "$RＤＥＭＩＣを開放した際に出る$R;" +
                                    "ノイズのせいだと思うんだけど…。$R;" +
                                    "$R…まぁ、仮説は仮説に過ぎないね。$R;" +
                                    "$Pそれはともかく、エンゲージタスクは$R;" +
                                    "ただのお邪魔なブロックじゃなくて、$R;" +
                                    "それを上回る利点もあるんだよ。$R;" +
                                    "$Pなんたって、エンゲージタスクに隣接した$R;" +
                                    "チップを強化してくれるんだからね。$R;" +
                                    "$P具体的にいうと、$R;" +
                                    "ステータスチップやスキルチップが$R;" +
                                    "エンゲージタスクに隣接した場合は、$R;" +
                                    "そのチップが２つ装着したのと$R;" +
                                    "同じ効果を発揮するの。$R;" +
                                    "$PＳＴＲ＋２の効果のあるチップなら$R;" +
                                    "倍になってＳＴＲ＋４の効果になるし、$R;" +
                                    "スキルチップなら、スキルのレベルが$R;" +
                                    "２も上昇するようになるよ！$R;" +
                                    "$Rもちろん、スキルの最大レベルを$R;" +
                                    "超えることはないけどね。$R;" +
                                    "$Pそれとリミテーションチップの場合は、$R;" +
                                    "他のチップと違って、$R;" +
                                    "２つ装着していることにはならないの。$R;" +
                                    "$Rただ、「ランク」を１つ上げてくれるから$R;" +
                                    "それでも十分に効果は高いよ！$R;" +
                                    "$Pもっとも、エンゲージタスクがあるマスは$R;" +
                                    "チップを配置できなくなるから、$R;" +
                                    "必ずしも良いものとは言えないけどね。$R;" +
                                    "$R有効に活用すれば、$R;" +
                                    "きっと君の力になるよ！$R;", "ＤＥＭ何でも博士");
                                    break;
                                case 6:
                                    Say(pc, 131, "ＤＥＭには「ノーマルフォーム」と$R;" +
                                    "「マシナフォーム」っていう２つの$R;" +
                                    "「フォーム」が存在するよ。$R;" +
                                    "$P君は、好きな時にフォームを変える…$R;" +
                                    "「フォームチェンジ」ができるんだ。$R;" +
                                    "$Rフォームチェンジをするには、$R;" +
                                    "自分を右クリックで表示されるメニューで$R;" +
                                    "「フォームチェンジ」を選択すればＯＫ！$R;" +
                                    "$Pノーマルフォームというものは、$R;" +
                                    "エミル、タイタニア、ドミニオンの$R;" +
                                    "武器や防具を装備できる、$R;" +
                                    "外見が完全人型のフォームだよ。$R;" +
                                    "$P詳細に関してはまた別で説明するけど、$R;" +
                                    "戦闘には向いていないフォームなんだ。$R;" +
                                    "$R「ノーマルフォーム」では戦わないように$R;" +
                                    "注意してね。$R;" +
                                    "$P一方のマシナフォームは、$R;" +
                                    "戦闘に特化したフォームだよ。$R;" +
                                    "$Pヘッド・ボディ・アーム・レッグ・バック$R;" +
                                    "の５箇所を入れ替えることができるんだ。$R;" +
                                    "$P各部位に装着するパーツのことを、$R;" +
                                    "「カスタマイズパーツ」って言ってるよ。$R;" +
                                    "$Pマシナフォームは戦闘に特化しているから$R;" +
                                    "戦闘はこっちのフォームを使ってね。$R;" +
                                    "$Pそうそう、フォームチェンジについて$R;" +
                                    "重要な注意点が一つあるんだ。$R;" +
                                    "$Pノーマルフォームからより高性能である$R;" +
                                    "マシナフォームに切り替えると、$R;" +
                                    "カスタマイズパーツや各種チップの効果に$R;" +
                                    "よって上昇された分のＨＰ、ＭＰ、ＳＰは$R;" +
                                    "その場で満タンにはならないんだ。$R;" +
                                    "$P戦場でいきなりチェンジしても、$R;" +
                                    "回復が必要だから、$R;" +
                                    "事前にマシナフォームになっていたほうが$R;" +
                                    "安心だよ！$R;", "ＤＥＭ何でも博士");
                                    break;
                            }
                        } while (sel2 != 7);                        
                    }
                    break;
            }


        }
    }
}
