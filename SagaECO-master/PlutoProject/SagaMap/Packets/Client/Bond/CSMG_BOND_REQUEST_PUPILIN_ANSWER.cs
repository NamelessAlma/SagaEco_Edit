﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_BOND_REQUEST_PUPILIN_ANSWER : Packet
    {
        public CSMG_BOND_REQUEST_PUPILIN_ANSWER()
        {
            this.offset = 2;
        }
        
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_BOND_REQUEST_PUPILIN_ANSWER();
        }
        public bool Rejected
        {
            get
            {
                return this.GetByte(2) == 1;
            }
        }
        public uint MasterCharID
        {
            get
            {
                return this.GetUInt(3);
            }
        }
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnBondPupilinAnswer(this);
        }
    }
}