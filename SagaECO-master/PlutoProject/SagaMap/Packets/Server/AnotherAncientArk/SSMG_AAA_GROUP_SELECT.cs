﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_AAA_GROUP_SELECT : Packet
    {
        public SSMG_AAA_GROUP_SELECT()
        {
            this.data = new byte[4];
            this.offset = 2;
            this.ID = 0x0;
        }
      
    }
}

