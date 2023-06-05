﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.Npc;
using SagaDB.Quests;
using SagaLib;
using SagaMap;
using SagaMap.Manager;


namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        bool trading = false;
        bool confirmed = false;
        bool performed = false;
        public bool npcTrade = false;
        List<uint> tradeItems = null;
        List<ushort> tradeCounts = null;
        uint tradingGold = 0;
        ActorPC tradingTarget;
        public List<Item> npcTradeItem = null;

        public void OnTradeRequest(Packets.Client.CSMG_TRADE_REQUEST p)
        {
            Actor actor = this.Map.GetActor(p.ActorID);
            if (actor == null)
                return;
            if (actor.type != ActorType.PC)
                return;
            ActorPC pc = (ActorPC)actor;
            MapClient client = MapClient.FromActorPC(pc);

            Packets.Server.SSMG_TRADE_REQUEST_RESULT p1 = new SagaMap.Packets.Server.SSMG_TRADE_REQUEST_RESULT();
                
            if (client.trading == true)
            {
                p1.Result = -3;
            }
            else if (this.trading == true)
            {
                p1.Result = -1;
            }
            else if (this.scriptThread != null)
            {
                p1.Result = -2;
            }
            else if (client.scriptThread != null)
            {
                p1.Result = -4;
            }
            else
            {
                this.tradingTarget = pc;
                client.SendTradeRequest(this.Character);
            }
            this.netIO.SendPacket(p1);
        }

        public void OnTradeRequestAnswer(Packets.Client.CSMG_TRADE_REQUEST_ANSWER p)
        {
            if (this.tradingTarget == null)
                return;
            if (tradingTarget.MapID != this.Character.MapID)
                return;
            MapClient client = MapClient.FromActorPC(tradingTarget);
            switch (p.Answer)
            {
                case 1:
                    this.trading = true;
                    client.trading = true;
                    
                    this.confirmed = false;
                    this.performed = false;
                    client.confirmed = false;
                    client.performed = false;

                    this.SendTradeStart();
                    this.SendTradeStatus(true, false);
                    client.SendTradeStart();
                    client.SendTradeStatus(true, false);
                    break;
                default:
                    this.tradingTarget = null;
                    client.tradingTarget = null;
                    Packets.Server.SSMG_TRADE_REQUEST_RESULT p1 = new SagaMap.Packets.Server.SSMG_TRADE_REQUEST_RESULT();
                    p1.Result = -6;
                    client.netIO.SendPacket(p1);
                    break;
            }
        }

        void OnTradeItemNPC(Packets.Client.CSMG_TRADE_ITEM p)
        {
            if (this.tradeItems != null)
            {
                if (this.tradeItems.Count != 0)
                {
                    this.confirmed = false;
                    this.performed = false;
                    this.SendTradeStatus(true, false);                    
                }
            }
            this.tradeItems = p.InventoryID;
            this.tradeCounts = p.Count;
            this.tradingGold = p.Gold;
        }

        public void OnTradeItem(Packets.Client.CSMG_TRADE_ITEM p)
        {
            if (npcTrade)
            {
                OnTradeItemNPC(p);
                return;
            }
            if (tradingTarget == null)
                return;
            MapClient client = MapClient.FromActorPC(tradingTarget);
            if (this.tradeItems != null)
            {
                if (this.tradeItems.Count != 0)
                {
                    this.confirmed = false;
                    client.confirmed = false;
                    this.performed = false;
                    client.performed = false;
                    this.SendTradeStatus(true, false);
                    client.SendTradeStatus(true, false);
                }
            }
            this.tradeItems = p.InventoryID;
            this.tradeCounts = p.Count;
            this.tradingGold = p.Gold;
            Packets.Server.SSMG_TRADE_ITEM_HEAD p1 = new SagaMap.Packets.Server.SSMG_TRADE_ITEM_HEAD();
            client.netIO.SendPacket(p1);
            for (int i = 0; i < this.tradeItems.Count; i++)
            {
                Packets.Server.SSMG_TRADE_ITEM_INFO p2 = new SagaMap.Packets.Server.SSMG_TRADE_ITEM_INFO();
                Item item = this.Character.Inventory.GetItem(this.tradeItems[i]).Clone();
                if (this.Character.Account.GMLevel < 100)
                {
                    if (item.BaseData.noTrade || item.BaseData.itemType == ItemType.DEMIC_CHIP)
                    {
                        this.tradeItems[i] = 0;
                        this.tradeCounts[i] = 0;
                        continue;
                    }
                }
                if (item.PossessionOwner != null)
                {
                    if (item.PossessionOwner.CharID != this.chara.CharID)
                    {
                        this.tradeItems[i] = 0;
                        this.tradeCounts[i] = 0;
                        continue;
                    }
                }
                if (item.Stack < this.tradeCounts[i])
                    this.tradeCounts[i] = item.Stack;
                item.Stack = this.tradeCounts[i];
                p2.Item = item;
                p2.InventorySlot = this.tradeItems[i];
                p2.Container = ContainerType.BODY;
                client.netIO.SendPacket(p2);
            }
            Packets.Server.SSMG_TRADE_GOLD p3 = new SagaMap.Packets.Server.SSMG_TRADE_GOLD();
            p3.Gold = this.tradingGold;
            client.netIO.SendPacket(p3);
            Packets.Server.SSMG_TRADE_ITEM_FOOT p4 = new SagaMap.Packets.Server.SSMG_TRADE_ITEM_FOOT();
            client.netIO.SendPacket(p4);
        }

        public void OnTradeConfirm(Packets.Client.CSMG_TRADE_CONFIRM p)
        {
            if (npcTrade)
            {
                switch (p.State)
                {
                    case 0:
                        this.confirmed = false;
                        break;
                    case 1:
                        this.confirmed = true;
                        break;
                }
                if (this.confirmed)
                {
                    this.SendTradeStatus(false, true);
                }
            }
            if (tradingTarget == null)
                return;
            switch (p.State)
            {
                case 0:
                    this.confirmed = false;
                    break;
                case 1:
                    this.confirmed = true;
                    break;
            }
            if (this.confirmed && MapClient.FromActorPC(tradingTarget).confirmed)
            {
                this.SendTradeStatus(false, true);
                MapClient.FromActorPC(tradingTarget).SendTradeStatus(false, true);                
            }
        }

        public void OnTradePerform(Packets.Client.CSMG_TRADE_PERFORM p)
        {
            if (this.npcTrade)
            {                
                PerformTradeNPC();
                return;
            }
            if (tradingTarget == null)
                return;
            switch (p.State)
            {
                case 0:
                    this.performed = false;
                    break;
                case 1:
                    this.performed = true;
                    break;
            }
            MapClient client = MapClient.FromActorPC(tradingTarget);
            if (this.performed && client.performed)
            {
                if (this.Character.Gold >= this.tradingGold &&
                    client.Character.Gold >= client.tradingGold &&
                    this.Character.Gold + client.tradingGold < 100000000 && //金钱上限为1亿
                    client.Character.Gold + this.tradingGold < 100000000
                    )
                {
                    this.SendTradeEnd(2);
                    this.PerformTrade();
                    client.SendTradeEnd(2);
                    client.PerformTrade();
                }
                this.SendTradeEnd(1);
                client.SendTradeEnd(1);
            }
        }

        public void OnTradeCancel(Packets.Client.CSMG_TRADE_CANCEL p)
        {
            if (npcTrade)
            {
                this.npcTradeItem = new List<Item>();
                this.npcTrade = false;
                this.SendTradeEnd(3);
                return;
            }
            if (tradingTarget == null)
                return;

            MapClient.FromActorPC(tradingTarget).SendTradeEnd(3);
            this.SendTradeEnd(3);            
        }

        void PerformTradeNPC()
        {
            this.npcTradeItem = new List<Item>();
            this.SendTradeEnd(2);
            if (this.tradeItems != null)
            {
                for (int i = 0; i < this.tradeItems.Count; i++)
                {
                    Item item = this.Character.Inventory.GetItem(this.tradeItems[i]).Clone();
                    item.Stack = this.tradeCounts[i];
                    Logger.LogItemLost(Logger.EventType.ItemNPCLost, this.Character.Name + "(" + this.Character.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                            string.Format("NPCTrade Count:{0}", item.Stack), false);
                    this.DeleteItem(this.tradeItems[i], this.tradeCounts[i], true);
                    this.npcTradeItem.Add(item);
                }
            }
            this.Character.Gold -= (int)this.tradingGold;
            this.SendGoldUpdate();
            this.performed = true;
            this.npcTrade = false;

            this.SendTradeEnd(1);    
        }

        public void PerformTrade()
        {
            if (tradingTarget == null)
                return;
            MapClient client = MapClient.FromActorPC(tradingTarget);            
            if (this.tradeItems != null)
            {
                for (int i = 0; i < this.tradeItems.Count; i++)
                {
                    if (this.tradeItems[i] == 0)
                        continue;
                    Item item = this.Character.Inventory.GetItem(this.tradeItems[i]).Clone();
                    item.Stack = this.tradeCounts[i];
                    Logger.LogItemLost(Logger.EventType.ItemTradeLost, this.Character.Name + "(" + this.Character.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                            string.Format("Trade Count:{0} To:{1}({2})", tradeCounts[i], client.Character.Name, client.Character.CharID), false);
                    this.DeleteItem(this.tradeItems[i], this.tradeCounts[i], true);
                    Logger.LogItemGet(Logger.EventType.ItemTradeGet, client.Character.Name + "(" + client.Character.CharID + ")", item.BaseData.name + "(" + item.ItemID + ")",
                        string.Format("Trade Count:{0} From:{1}({2})", item.Stack, this.Character.Name, this.Character.CharID), false);
                    client.AddItem(item, true);
                }
            }
            this.Character.Gold -= (int)this.tradingGold;
            this.SendGoldUpdate();
            client.Character.Gold += (int)this.tradingGold;
            client.SendGoldUpdate();
        }

        /// <summary>
        /// 结束交易
        /// </summary>
        /// <param name="type">1，清空变量，2，发送结束封包，3，两个都执行</param>
        public void SendTradeEnd(int type)
        {
            if (type == 1 || type == 3)
            {
                this.tradeCounts = null;
                this.tradeItems = null;
                this.trading = false;
                this.tradingGold = 0;
                this.tradingTarget = null;
                this.confirmed = false;
                this.performed = false;
            }
            if (type == 2 || type == 3)
            {
                Packets.Server.SSMG_TRADE_END p = new SagaMap.Packets.Server.SSMG_TRADE_END();
                this.netIO.SendPacket(p);
            }
        }

        public void SendTradeStatus(bool canConfirm, bool canPerform)
        {
            Packets.Server.SSMG_TRADE_STATUS p = new SagaMap.Packets.Server.SSMG_TRADE_STATUS();
            p.Confirm = canConfirm;
            p.Perform = canPerform;
            this.netIO.SendPacket(p);
        }

        public void SendTradeStart()
        {
            if (this.tradingTarget == null)
                return;
            Packets.Server.SSMG_TRADE_START p = new SagaMap.Packets.Server.SSMG_TRADE_START();
            p.SetPara(this.tradingTarget.Name, 0);
            this.netIO.SendPacket(p);
        }

        public void SendTradeStartNPC(string name)
        {
            if (this.npcTrade)
            {
                Packets.Server.SSMG_TRADE_START p = new SagaMap.Packets.Server.SSMG_TRADE_START();
                p.SetPara(name, 1);
                this.netIO.SendPacket(p);
                SendTradeStatus(true, false);
            }
        }

        public void SendTradeRequest(ActorPC pc)
        {
            Packets.Server.SSMG_TRADE_REQUEST p = new SagaMap.Packets.Server.SSMG_TRADE_REQUEST();
            p.Name = pc.Name;
            this.netIO.SendPacket(p);

            this.tradingTarget = pc;
        }
    }
}
