﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_TRADE_REQUEST_ANSWER : Packet
    {
        public CSMG_TRADE_REQUEST_ANSWER()
        {
            this.offset = 2;
        }

        public byte Answer
        {
            get
            {
                return this.GetByte(2);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_TRADE_REQUEST_ANSWER();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnTradeRequestAnswer(this);
        }

    }
}