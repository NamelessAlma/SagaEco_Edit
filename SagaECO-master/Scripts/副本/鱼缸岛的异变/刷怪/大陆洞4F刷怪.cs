﻿
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaDB.Actor;
using SagaMap.Mob;
using SagaDB.Mob;
using SagaMap.ActorEventHandlers;
namespace SagaScript.M30210000
{
    public partial class 暗鸣 : Event
    {
        void 大陆B4F刷怪(uint mapid, ActorPC pc)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
            map.SpawnCustomMob(10000000, map.ID, 10180000, 0, 0, 37, 86, 23, 6, 0, 活丧尸Info(), 活丧尸AI(), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 15192000, 0, 0, 73, 71, 12, 2, 0, 剧毒蘑菇Info(), 剧毒蘑菇AI(), null, 0);
            map.SpawnCustomMob(10000000, map.ID, 15192000, 0, 0, 116, 61, 22, 2, 0, 剧毒蘑菇Info(), 剧毒蘑菇AI(), null, 0);
            if (Global.Random.Next(0, 100) < 30 && DateTime.Now.Year == 2017 &&((DateTime.Now.Month == 1 && DateTime.Now.Day >= 11) || (DateTime.Now.Month == 2 && DateTime.Now.Day <=3)))
            {
               ActorMob mobS = map.SpawnCustomMob(10000000, map.ID, 10370000, 0, 0, 82, 62, 0, 1, 0, 迎春宝箱Info(), 迎春宝箱AI(), null, 0)[0];
                ((MobEventHandler)mobS.e).Dying += (s, e) => SInt["副本新春宝箱死亡次数"]++;
            }
        }
    }
}

