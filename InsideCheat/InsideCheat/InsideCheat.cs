using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InsideCheat 
{
    public class InsideCheat
    {
        static EntityPlayerLocal playerLocal = null;
        System.Timers.Timer t = new System.Timers.Timer();
        public InsideCheat() 
        {
            t = new System.Timers.Timer(100);
            t.Elapsed += new System.Timers.ElapsedEventHandler(Update);
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
            t.Start();

        }
        public void Update(object source, System.Timers.ElapsedEventArgs e)
        {
            t.Stop();
            if (Input.GetKey(KeyCode.F2) && GameManager.Instance.World != null)
            {
                playerLocal = GameManager.Instance.World.GetPrimaryPlayer();
                if (playerLocal.transform.gameObject.GetComponent<Cheat>() == null)
                {

                    playerLocal.transform.gameObject.AddComponent<Cheat>();
                }
                else
                {
                    GameObject.Destroy(playerLocal.transform.gameObject.GetComponent<Cheat>());
                    GameObject.Destroy(playerLocal.transform.gameObject.GetComponent<AddItems>());
                }
            }
            t.Start();
        }


        class Cheat : MonoBehaviour
        {                     
            public void OnGUI()
            {
                GUI.Box(new Rect(0, 0, 200, 800), "启程内置修改");
                #region 无消耗制作，秒制作
                bool decreaseCost = GUI.Button(new Rect(50, 50, 150, 100), "三分之一材料消耗");
                if (decreaseCost)
                {
                    foreach (Recipe recipe in CraftingManager.GetAllRecipes())
                    {
                        recipe.craftingTime /= 3;
                        foreach (ItemStack stack in recipe.ingredients)
                        {
                            stack.count /= 3;
                            if (stack.count <= 0)
                            {
                                stack.count = 1;
                            }
                        }
                    }
                }
                #endregion
                #region 秒做任务
                bool finishTask = GUI.Button(new Rect(50, 200, 150, 100), "直接获得当前任务奖励");
                if (finishTask && playerLocal.QuestJournal != null && playerLocal.QuestJournal.TrackedQuest != null)
                {
                    foreach (BaseReward baseReward in playerLocal.QuestJournal.TrackedQuest.Rewards)
                    {
                        baseReward.GiveReward();
                    }
                    playerLocal.KilledZombies += 3;
                }
                #endregion
                #region 添加指定物品
                bool addItem = GUI.Button(new Rect(50, 350, 150, 100), "添加指定物品");
                if (addItem)
                {
                    if (playerLocal.transform.gameObject.GetComponent<AddItems>() == null)
                    {
                        playerLocal.transform.gameObject.AddComponent<AddItems>();
                    }
                    else
                    {
                        GameObject.Destroy(playerLocal.transform.gameObject.GetComponent<AddItems>());
                    }
                }
                #endregion
                #region 修复手持物品耐久
                bool fixItem = GUI.Button(new Rect(50, 500, 150, 100), "修复手持物品耐久");
                if (fixItem)
                {
                    ItemValue itemValue = playerLocal.inventory.GetSlots()[playerLocal.inventory.holdingItemIdx].itemValue;
                    itemValue.UseTimes = 0;
                }
                #endregion
                #region vip7
                bool vip7 = GUI.Button(new Rect(50, 650, 150, 100), "vip7");
                if (vip7)
                {
                    playerLocal.Buffs.AddBuff("buff7", -1, true, false, false);
                    playerLocal.Progression.GetProgressionValue("vip").Level = 7;
                }               
                #endregion
                #region 杀死指定玩家
                DamageResponse damageResponse = default(DamageResponse);
                DamageSource sources = new DamageSource(EnumDamageSource.External, EnumDamageTypes.Suicide);
                sources.DamageMultiplier = 999;
                damageResponse.Source = sources;
                damageResponse.Strength = 999;
                damageResponse.Critical = true;
                damageResponse.HitDirection = Utils.EnumHitDirection.None;
                damageResponse.MovementState = 1;
                damageResponse.Random = 100;
                damageResponse.ImpulseScale = 100;
                damageResponse.HitBodyPart = EnumBodyPartHit.Head;
                damageResponse.ArmorSlot = XMLData.Item.EnumEquipmentSlot.None;
                damageResponse.ArmorSlotGroup = XMLData.Item.EnumEquipmentSlotGroup.LowerBody;
                #endregion
            }

        }
        class AddItems : MonoBehaviour
        {
            private readonly Rect addItemRect = new Rect(Screen.width / 2, Screen.height / 2 - 200, 500, 800);
            private static string[] itemIDs = new string[]
            {
            "resourceTestosteroneExtract",
            "ulmResourceIngotGold",
            "resourceAcid",
            "resourceRawDiamond",
            "ulmResourceResearch",
            "smallEngine",
            "ulmResourceElectronicCircuitboard",
            "modGunMeleeBlessedMetal",
            "ulmToolBerserkerDrill",
            "gunAK47RE",            
            "ulmAmmoFuelGasoline",
            "Typhoon",
            "ulmResourceBook",
            "resourceScrapPolymers",
            "ulmResourceAdhesive",
            "ulmResourceIngotBrass"
            };
            private static string[] displayItemNames = new string[]
            {
            "睾丸素提取物",
            "金锭",
            "一瓶酸液",
            "钻石原石",
            "研究数据",
            "小型发动机",
            "电路板",
            "异域合金",
            "女武神钻头",
            "AK47加强版",           
            "汽油",
            "排笛",
            "旧书",
            "废塑料",
            "粘合剂",
            "黄铜锭"
            };
            void OnGUI()
            {
                GUI.Box(addItemRect, "待添加物品列表");
                for (int i = 1; i <= itemIDs.Length; i++)
                {
                    int max = (int)addItemRect.size.y / 100;
                    int x = 100 * (int)Math.Ceiling((decimal)i / max);
                    if(i % max == 0)
                    {
                        x = 100 * ((int)Math.Ceiling((decimal)i / max) + 1);
                    }
                    int y = 100 * i;
                    if (y >= addItemRect.size.y)
                    {
                        y = 100 * (i / (int)Math.Floor((decimal)i / max) - max + 1);                        
                    }
                    if (GUI.Button(new Rect(addItemRect.x + x, addItemRect.y + y, 100, 100), displayItemNames[i - 1]))
                    {
                        int quality = 0;                        
                        ItemValue item = ItemClass.GetItem(itemIDs[i - 1], false);                                          
                        if (item.HasQuality)
                        {
                           quality = 10;
                        }
                        if(itemIDs[i - 1].Equals("gunAK47RE") || itemIDs[i - 1].Equals("modGunMeleeBlessedMetal"))
                        {
                            quality = 160;
                        }
                        ItemStack itemStack = new ItemStack(new ItemValue(item.type, quality, quality, false, null, 1f), 1);
                        if(itemStack.CanStack(2))
                        {
                            itemStack.count = 50;
                        }
                        bool flag = playerLocal.bag.AddItem(itemStack);
                        if (flag)
                        {
                            GameManager.ShowTooltip(playerLocal, "添加" + displayItemNames[i - 1] + "成功");                            
                        }
                        else
                        {
                            GameManager.ShowTooltip(playerLocal, "添加" + displayItemNames[i - 1] + "失败");
                        }
                    }
                }
            }
        }
    }
}
