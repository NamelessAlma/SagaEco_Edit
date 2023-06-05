﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_PLAYER_STATS_UP : Packet
    {
        public CSMG_PLAYER_STATS_UP()
        {
            this.offset = 2;
        }

        public byte Type
        {
            get
            {
                return this.GetByte(2);
            }
        }

        public ushort Str
        {
            get
            {
                return this.GetUShort(3);
            }
        }
        public ushort Dex
        {
            get
            {
                return this.GetUShort(5);
            }
        }
        public ushort Int
        {
            get
            {
                return this.GetUShort(7);
            }
        }
        public ushort Vit
        {
            get
            {
                return this.GetUShort(9);
            }
        }
        public ushort Agi
        {
            get
            {
                return this.GetUShort(11);
            }
        }
        public ushort Mag
        {
            get
            {
                return this.GetUShort(13);
            }
        }

        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_PLAYER_STATS_UP();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnStatsUp(this);
        }

    }
}