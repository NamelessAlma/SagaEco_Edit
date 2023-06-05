using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using SagaLib;
using SagaDB.Actor;
using SagaMap.Skill;
using SagaMap.Network.Client;
namespace SagaMap.Tasks.PC
{
    public partial class Recover : MultiRunTask
    {
        MapClient client;
        public Recover(MapClient client)
        {
            this.dueTime = 3000;
            this.period = 3000;
            this.client = client;
        }

        public override void CallBack()
        {
            try
            {
                if (client != null)
                {
                    ActorPC pc = client.Character;
                    if (pc == null) return;
                    if ((DateTime.Now - pc.TTime["�ϴ���Ȼ�ظ�ʱ��"]).TotalSeconds < 3)
                    {
                        Deactivate();
                        client.Character.Tasks.Remove("Recover");
                        return;
                    }
                    if (client.Character.Tasks.ContainsKey("Recover"))
                    {
                        if (client.Character.Tasks["Recover"] != this)
                        {
                            Deactivate();
                            return;
                        }
                    }
                    if (!client.Character.Tasks.ContainsKey("Recover"))
                        client.Character.Tasks.Add("Recover", this);

                    pc.TTime["�ϴ���Ȼ�ظ�ʱ��"] = DateTime.Now;
                    DateTime s = DateTime.Now;
                    BuffChecker(pc);
                    if ((Logger.defaultlogger.LogLevel | Logger.LogContent.Custom) == Logger.defaultlogger.LogLevel)
                        Logger.ShowError("���" + client.Character.Name + "BUFF����ʱ��" + (DateTime.Now - s).TotalMilliseconds.ToString());
                    if (pc.MapID == 91000999 && pc.FurnitureID != 0 && pc.FurnitureID != 255)
                        client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.FURNITURE_SIT, null, pc, true);
                    if (pc.HP > 0 && pc.Buff.Dead)
                    {
                        pc.Buff.Dead = false;
                        pc.Buff.�Ϥˤʤ� = false;
                    }
                    if (pc.HP > 0 && !pc.Buff.�Ϥˤʤ� && !pc.Buff.Dead)
                    {
                        if (pc.TInt["����ɱģʽ"] == 1)
                        {
                            pc.Mode = PlayerMode.COLISEUM_MODE;
                            if (pc.HP == pc.MaxHP)
                            {
                                if (pc.EP > 50)
                                    pc.EP -= 50;
                                else
                                    pc.EP = 0;
                            }
                            else
                            {
                                if (pc.EP > 100)
                                {
                                    pc.EP -= 100;
                                    pc.HP += 100;
                                    if (pc.HP > pc.MaxHP)
                                        pc.HP = pc.MaxHP;
                                }
                                else
                                    pc.EP = 0;

                            }
                            if (pc.EP == 0)
                            {
                                if (pc.HP < 100)
                                    client.map.Announce(pc.name + "������~");
                                SkillHandler.Instance.CauseDamage(pc, pc, 100, true);
                                SkillHandler.Instance.ShowVessel(pc, 100, 0);
                            }
                            pc.MP += 100;
                            pc.SP += 100;
                            if (pc.MP > pc.MaxMP)
                                pc.MP = pc.MaxMP;
                            if (pc.SP > pc.MaxSP)
                                pc.SP = pc.MaxSP;
                            client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, pc, true);
                        }
                        else if (pc.Job == PC_JOB.ASTRALIST)//ħ��ʦ
                            ASTRALIST(pc);
                        else if (pc.Job == PC_JOB.SOULTAKER)
                            SOULTAKER(pc);
                        else if (pc.Job == PC_JOB.HAWKEYE)
                            HAWKEYE(pc);
                        else if (pc.HP < pc.MaxHP || pc.MP < pc.MaxMP || pc.SP < pc.MaxSP || (pc.Job != PC_JOB.CARDINAL && pc.EP < pc.MaxEP) ||
                            (pc.Job == PC_JOB.CARDINAL && pc.EP != pc.Status.BeliefBalace))//����ְҵ
                            CARDINAL(pc);

                        if (pc.PossesionedActors.Count > 0 && pc.MapID != 10054000)
                            SkillHandler.Instance.PossessionCancel(pc, PossessionPosition.NONE);

                        if (pc.Status.Additions.ContainsKey("����ն�ƶ��ٶ�UP"))
                            pc.Speed = 1100;
                        else if (((Actor)pc).TInt["SPEEDDOWN"] != 0)
                            pc.Speed = (ushort)((Actor)pc).TInt["SPEEDDOWN"];
                        else if (client.Map.OriID == 70000000 || client.Map.OriID == 75000000)
                            pc.Speed = 150;
                        else
                            pc.Speed = 750;
                        if (pc.Pet != null)
                        {
                            if (pc.Pet.Ride)
                                pc.Speed = 1000;
                        }
                        //pc.e.PropertyUpdate(UpdateEvent.SPEED, 0);
                    }
                    else
                    {
                        this.Deactivate();
                        this.client.Character.Tasks.Remove("Recover");
                    }
                    if ((Logger.defaultlogger.LogLevel | Logger.LogContent.Custom) == Logger.defaultlogger.LogLevel)
                        Logger.ShowError("���" + client.Character.Name + "��Ȼ�ָ��ܺ�ʱ��" + (DateTime.Now - s).TotalMilliseconds.ToString());
                }

                else
                {
                    this.Deactivate();
                    this.client.Character.Tasks.Remove("Recover");
                }

            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.Deactivate();
                this.client.Character.Tasks.Remove("Recover");
            }
        }
        void CARDINAL(ActorPC pc)
        {
            if (!pc.Status.Additions.ContainsKey("Sacrifice"))
            {
                if (pc.TInt["Χ��Ⱥ��"] == 1)
                {
                    if (pc.Mode == PlayerMode.COLISEUM_MODE)
                        pc.HP += (uint)(0.018f * pc.MaxHP) + (uint)(pc.Status.hp_recover / 3.2);
                }
                else
                    pc.HP += (uint)(0.018f * pc.MaxHP) + (uint)(pc.Status.hp_recover / 3.2);
                if (pc.HP > pc.MaxHP)
                    pc.HP = pc.MaxHP;
            }
            uint MPheal = 30;
            if (pc.TInt["������������"] > 0 && pc.EP > 5000)
                MPheal += (uint)pc.TInt["������������"];
            if (pc.Job == PC_JOB.HAWKEYE)
                MPheal = 0;
            if (pc.Job != PC_JOB.HAWKEYE)
            {
                if (pc.TInt["Χ��Ⱥ��"] == 1)
                {
                    if (pc.Mode == PlayerMode.COLISEUM_MODE)
                        pc.MP += MPheal;
                }
                else
                    pc.MP += MPheal;
            }
            if (pc.MP > pc.MaxMP)
                pc.MP = pc.MaxMP;
            uint SPheal = 30;
            if (pc.Job == PC_JOB.HAWKEYE)
                SPheal = 0;
            if (pc.TInt["Χ��Ⱥ��"] == 1)
            {
                if (pc.Mode == PlayerMode.COLISEUM_MODE)
                    pc.SP += SPheal;
            }
            else
                pc.SP += SPheal;
            if (pc.SP > pc.MaxSP)
                pc.SP = pc.MaxSP;
            if (pc.Job != PC_JOB.CARDINAL)
            {
                if (pc.TInt["Χ��Ⱥ��"] == 1)
                {
                    if (pc.Mode == PlayerMode.COLISEUM_MODE)
                        pc.EP += (uint)(50 + pc.JobLevel3 * 1.5);
                }
                else
                    pc.EP += (uint)(50 + pc.JobLevel3 * 1.5);
            }
            else if (!pc.Status.Additions.ContainsKey("��־�ᶨ") && pc.EP != pc.Status.BeliefBalace)
            {
                if (pc.TInt["�ƺ����"] < pc.TInt["�ƺ⼶��"])
                    pc.TInt["�ƺ����"]++;
                else
                {
                    if (pc.TInt["Χ��Ⱥ��"] == 0 || pc.Mode == PlayerMode.COLISEUM_MODE)
                    {
                        int value = (int)(pc.EP - pc.Status.BeliefBalace) / 10;
                        if (value == 0)
                            value = pc.EP > pc.Status.BeliefBalace ? 1 : -1;
                        pc.EP = (uint)(pc.EP - value);

                        pc.TInt["�ƺ����"] = 0;
                    }
                }
            }
            this.client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, pc, true);

        }
        void HAWKEYE(ActorPC pc)
        {
            if (!pc.Status.Additions.ContainsKey("Sacrifice"))
            {
                if (pc.TInt["Χ��Ⱥ��"] == 1)
                {
                    if (pc.Mode == PlayerMode.COLISEUM_MODE)
                        pc.HP += (uint)(0.018f * pc.MaxHP) + (uint)(pc.Status.hp_recover / 3.2);
                }
                else
                    pc.HP += (uint)(0.018f * pc.MaxHP) + (uint)(pc.Status.hp_recover / 3.2);
                if (pc.HP > pc.MaxHP)
                    pc.HP = pc.MaxHP;
            }
            uint mpheal = 1;
            if (pc.Buff.��תǹ����)
                mpheal = 3;
            pc.MP += mpheal;
            if (pc.MP > pc.MaxMP)
                pc.MP = pc.MaxMP;
            pc.EP += 50;
            if (pc.EP > pc.MaxEP)
                pc.EP = pc.MaxEP;
            if ((DateTime.Now - pc.LastAttackTime).TotalSeconds > 60)
            {
                if (pc.SP > 0)
                    pc.SP -= 1;
            }
            client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, pc, true);
        }
        void SOULTAKER(ActorPC pc)
        {
            if (pc.TInt["Χ��Ⱥ��"] == 1)
            {
                if (pc.Mode == PlayerMode.COLISEUM_MODE)
                    pc.HP += (uint)(0.018f * pc.MaxHP) + (uint)(pc.Status.hp_recover / 3.2);
            }
            else
                pc.HP += (uint)(0.018f * pc.MaxHP) + (uint)(pc.Status.hp_recover / 3.2);
            if (pc.HP > pc.MaxHP)
                pc.HP = pc.MaxHP;
            client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, pc, true);
        }

        void ASTRALIST(ActorPC pc)
        {
            if (!pc.Status.Additions.ContainsKey("Sacrifice"))
            {
                if (pc.TInt["Χ��Ⱥ��"] == 1)
                {
                    if (pc.Mode == PlayerMode.COLISEUM_MODE)
                        pc.HP += (uint)(0.018f * pc.MaxHP) + (uint)(pc.Status.hp_recover / 3.2);
                    else pc.HP += 0;
                }
                else
                    pc.HP += (uint)(0.018f * pc.MaxHP) + (uint)(pc.Status.hp_recover / 3.2);
                if (pc.HP > pc.MaxHP)
                    pc.HP = pc.MaxHP;
            }
            if (pc.TInt["Χ��Ⱥ��"] == 1)
            {
                if (pc.Mode == PlayerMode.COLISEUM_MODE)
                    pc.MP += (uint)(30 + pc.Int / 2);
                else pc.MP += 0;
            }
            else
                pc.MP += (uint)(30 + pc.Int / 2);
            if (pc.MP > pc.MaxMP)
                pc.MP = pc.MaxMP;
            if (pc.SP < pc.MaxSP)
            {
                if (pc.TInt["Χ��Ⱥ��"] == 1)
                {
                    if (pc.Mode == PlayerMode.COLISEUM_MODE)
                        pc.TInt["�����ָ�"]++;
                }
                else
                    pc.TInt["�����ָ�"]++;
                if (pc.TInt["�����ָ�"] >= 20)
                {
                    pc.SP = pc.MaxSP;
                    SkillHandler.Instance.ShowEffectOnActor(pc, 2507);
                    pc.TInt["�����ָ�"] = 0;
                }
            }
            else
                pc.TInt["�����ָ�"] = 0;

            int TotalInt = pc.Int + pc.Status.int_item + pc.Status.int_iris + pc.Status.int_rev + pc.Status.int_skill + pc.Status.int_ano + pc.Status.int_tit + pc.Status.int_food;
            int damage = (int)(pc.EP - TotalInt * (10 + pc.TInt["ħ������"]));

            if (damage > 0)//��������
            {
                if (pc.Skills.ContainsKey(14005) && pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.STAFF)
                        damage -= (int)(damage * pc.Skills[14005].Level * 0.10f);
                }

                SkillHandler.Instance.CauseDamage(pc, pc, damage, true);
                int spheal = 0;
                if (pc.Status.Additions.ContainsKey("ħ����Ů") && pc.TInt["��������"] == 1 && pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) && pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE)
                {
                    spheal = Math.Min(200, damage / 2);
                    pc.SP += (uint)spheal;
                    if (pc.SP > pc.MaxSP)
                        pc.SP = pc.MaxSP;
                }
                SkillHandler.Instance.ShowVessel(pc, damage, 0, -spheal);
                SkillHandler.Instance.ShowEffectOnActor(pc, 5293);
                if (pc.Skills.ContainsKey(14050) && damage > 0)
                {
                    Map map = Manager.MapManager.Instance.GetMap(pc.MapID);
                    List<Actor> targets = map.GetActorsArea(pc, 500, true);
                    foreach (var item in targets)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(pc, item))
                        {
                            damage = SkillHandler.Instance.CalcDamage(false, pc, item, null, SkillHandler.DefType.MDef, Elements.Neutral, 0, 1f);
                            SkillHandler.Instance.CauseDamage(pc, item, damage, true);
                            SkillHandler.Instance.ShowVessel(item, damage);
                            SkillHandler.Instance.ShowEffectOnActor(item, 5293);

                            //ը����ʹ�þ�ͨ�������Ч��ɱ��100ֻ���
                            if (item.Buff.Dead)
                                SkillHandler.Instance.TitleProccess(pc, 95, 1);
                        }
                    }
                }

                if (!pc.Buff.Burning)
                {
                    pc.Buff.Burning = true;
                    client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, pc, true);
                }
            }
            else
            {
                if (pc.Buff.Burning)
                {
                    pc.Buff.Burning = false;
                    client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, pc, true);
                }
            }
            if (pc.EP > 0)
            {
                int d = pc.Int * 3;
                if (pc.Status.Additions.ContainsKey("ʱ��Ť��"))
                    d *= 2;
                if (pc.Skills.ContainsKey(14050) && damage > 0)   //��ͨ������
                    d *= 2;
                if (d > pc.EP)
                    pc.EP = 0;
                else
                    pc.EP -= (uint)d;
            }
            client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, pc, true);
        }
    }
}
