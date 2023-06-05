﻿
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.Mob;
using SagaMap.Skill;
using SagaDB.Mob;
using SagaMap.ActorEventHandlers;
namespace WeeklyExploration
{
    public partial class GTSQuest : Event
    {

        static ActorMob.MobInfo 花音Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.maxhp = 11745925;//血量
            info.level = 75;
            info.name = "暴走族团长·花音";
            info.speed = 520;//移動速度
            info.atk_min = 500;//最低物理攻擊
            info.atk_max = 800;//最高物理攻擊
            info.matk_min = 200;//最低魔法攻擊
            info.matk_max = 300;//最高物理攻擊
            info.def = 20;//物理左防
            info.mdef = 10;//魔法左防
            info.def_add = 50;//物理右防
            info.mdef_add = 50;//魔法右防
            info.hit_critical = 50;//暴擊值
            info.hit_magic = 100;//魔法命中值（目前沒用
            info.hit_melee = 300;//近戰命中值
            info.hit_ranged = 300;//遠程命中值
            info.avoid_critical = 20;//暴擊閃避值
            info.avoid_magic = 0;//魔法閃避值
            info.avoid_melee = 20;//近戰閃避值
            info.avoid_ranged = 30;//遠程閃避值
            info.Aspd = 900;//攻速
            info.Cspd = 100;//唱速
            info.AttackType = SagaDB.Actor.ATTACK_TYPE.BLOW;//攻擊類型，打 刺 斬，一般可以不管
            info.elements[SagaLib.Elements.Fire] = 0;//火屬性
            info.elements[SagaLib.Elements.Earth] = 0;//地屬性
            info.elements[SagaLib.Elements.Dark] = 0;//暗屬性
            info.elements[SagaLib.Elements.Holy] = 0;//光屬性
            info.elements[SagaLib.Elements.Neutral] = 0;//無屬性
            info.elements[SagaLib.Elements.Water] = 30;//水屬性
            info.elements[SagaLib.Elements.Wind] = 0;//風屬性
            info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 0;//混亂抗性
            info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 0;//冰抗性
            info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 0;//麻痺
            info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 0;//毒抗
            info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 0;//沉默抗
            info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 0;//睡抗
            info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 0;//石抗
            info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 0;//暈抗
            info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 0;//頓足抗
            info.baseExp = info.maxhp/2;//基礎經驗值
            info.jobExp = info.maxhp/2;//職業經驗值
            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            newDrop.ItemID = 110051810;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.count = 500;
            newDrop.Public = true;
            info.dropItems.Add(newDrop);

            newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            newDrop.ItemID = 110051810;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.count = 500;
            newDrop.Public20 = true;
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        static AIMode 花音AI()
        {
            AIMode ai = new AIMode(1);
            ai.MobID = 10000000;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 5;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 5;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------鱼人践踏---------*/
            skillinfo.CD = 25;//技能CD
            skillinfo.Rate = 60;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31044, skillinfo);//將這個技能加進進程技能表
            /*---------鱼人践踏---------*/

            /*---------八卦掌---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 45;//技能CD
            skillinfo.Rate = 60;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31045, skillinfo);//將這個技能加進進程技能表

            /*---------炎龙拳---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 20;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31046, skillinfo);//將這個技能加進進程技能表

            /*---------鱼人乱打---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 35;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31047, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31047, skillinfo);

            /*---------闪电弧---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 40;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 70;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(16302, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(16302, skillinfo);

            /*---------光跑---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 20;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(17002, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(17002, skillinfo);

            /*---------地裂---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 20;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(16401, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(16401, skillinfo);
            return ai;
        }
    }
}

