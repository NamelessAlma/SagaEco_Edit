﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum Beginner_01
    {
        //初心者任務

        //離開阿高普路斯市
        司令官下令發射主砲 = 0x1,
        再次遇上微微 = 0x2,
        變身成為微微的塞爾曼德 = 0x4,
        遇到敵人 = 0x8,
        離開阿高普路斯市 = 0x10,

        //說明ECO的基本操作
        已經與埃米爾進行第一次對話 = 0x20,
        埃米爾給予埃米爾徽章 = 0x40,
        埃米爾給予埃米爾介紹書 = 0x80,

        //NPC物品買賣教學
        NPC物品買賣教學開始 = 0x100,
        販賣巧克力完成 = 0x200,
        購買蘋果完成 = 0x400,
        NPC物品買賣教學完成 = 0x800,

        //說明ECO的戰鬥操作
        已經與貝利爾進行第一次對話 = 0x1000,
        貝利爾開始進行相關說明 = 0x2000,

        已經與貝利爾進行過戰鬥教學 = 0x4000,
        戰鬥教學開始 = 0x8000,
        HP恢復教學完成 = 0x10000,
        戰鬥教學完成 = 0x20000,

        已經與貝利爾進行過技能教學 = 0x40000,
        技能教學開始 = 0x80000,
        得到第二顆技能石 = 0x100000,
        得到第三顆技能石 = 0x200000,
        技能教學完成 = 0x400000,

        貝利爾給予初心者緞帶 = 0x800000,

        //瑪莎說明ECO的飛空庭系統
        已經與瑪莎進行第一次對話 = 0x1000000,
        發現床底下的東西 = 0x2000000,

         //說明ECO的基本操作2
        埃米爾的蘋果 = 0x4000000,
        埃米爾的重物 = 0x8000000,
        埃米爾的木偶 = 0x10000000,
        埃米爾的桃子 = 0x20000000,
        埃米爾的刀 = 0x40000000,
    }
}
