﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;

namespace SagaMap.Packets.Client
{
    public class CSMG_POSSESSION_CATALOG_ITEM_INFO_REQUEST : Packet
    {
        public CSMG_POSSESSION_CATALOG_ITEM_INFO_REQUEST()
        {
            this.offset = 2;
        }

        public uint ActorID
        {
            get
            {
                return this.GetUInt(2);
            }
        }

        public uint ItemID
        {
            get
            {
                return this.GetUInt(6);
            }
        }
        public override SagaLib.Packet New()
        {
            return (SagaLib.Packet)new SagaMap.Packets.Client.CSMG_POSSESSION_CATALOG_ITEM_INFO_REQUEST();
        }

        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)(client)).OnPossessionCatalogItemInfoRequest(this);
        }

    }
}