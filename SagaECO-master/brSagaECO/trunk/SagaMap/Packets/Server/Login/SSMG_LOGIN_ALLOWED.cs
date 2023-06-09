﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;

namespace SagaMap.Packets.Server
{
    public class SSMG_LOGIN_ALLOWED : Packet
    {
        public SSMG_LOGIN_ALLOWED()
        {
            this.data = new byte[10];
            this.offset = 14;
            this.ID = 0x000F;
        }

        public uint FrontWord
        {
            set
            {
                this.PutUInt(value, 2);
            }
        }

        public uint BackWord
        {
            set
            {
                this.PutUInt(value, 6);
            }
        }

    }
}

