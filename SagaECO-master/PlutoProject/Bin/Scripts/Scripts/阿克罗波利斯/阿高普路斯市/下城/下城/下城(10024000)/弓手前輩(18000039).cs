using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:弓手前輩(18000039) X:202 Y:150
namespace SagaScript.M10024000
{
    public class S18000039 : Event
    {
        public S18000039()
        {
            this.EventID = 18000039;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
