﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31300000
{
    public class S11001742 : Event
    {
        public S11001742()
        {
            this.EventID = 11001742;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001741, 131, "……。$R;", "もこもこバニー");
        }


    }
}


