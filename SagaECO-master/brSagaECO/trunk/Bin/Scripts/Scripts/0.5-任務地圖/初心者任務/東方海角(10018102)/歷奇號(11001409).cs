using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:歷奇號(11001409) X:222 Y:61
namespace SagaScript.M10018102
{
    public class S11001409 : Event
    {
        public S11001409()
        {
            this.EventID = 11001409;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.已經與埃米爾進行第一次對話))
            {
                尚未與埃米爾對話(pc);
                return;
            }

            Say(pc, 11001409, 500, "唔…$R;", "歷奇號");

            Say(pc, 11001408, 131, "啊，對不起，可能是看到陌生的臉孔$R;" +
                                   "所以有點害怕$R;" +
                                   "$R這小子叫歷奇號，是我的搭檔唷$R;" +
                                   "$P對商人來說這個小子最棒了，$R;" +
                                   "看到帶著這個傢伙的人，$R;" +
                                   "就跟他說話吧$R;" +
                                   "$R可能會給您提示的呀$R;", "利基先生");
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11001409, 0, "唔…$R;", "歷奇號");
        }  
    }
}
