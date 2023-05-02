using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using HarmonyLib;

namespace IncreaseMaxQuality
{
    // Token: 0x02000002 RID: 2
    [HarmonyPatch(typeof(EntityBuffs))]
    [HarmonyPatch("AddBuff")]
    public class ZombieEnhanced
    {
        //static bool flag = false;
        //static float rate = 1;
        //static float originalMaxHealth = 0;
        //private static bool GetMax(string _name, EntityBuffs __instance)
        // {

        // }
       
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        private static bool Prefix(string _name,EntityBuffs __instance)
        {            
            //取进化最大值，拦截不合理的jd buff
            for (int j = 1; j <= 10; j++)
            {
                if (_name.Equals("jd" + j.ToString()) && __instance.parent != null)
                {
                    for (int i = j; i <= 10; i++)
                    {
                        foreach (BuffValue buffValue in __instance.ActiveBuffs)
                        {
                            if (buffValue.BuffName.Equals("jd" + i.ToString()))
                            {
                                return false;
                            }
                        }
                    }
                    //合理附加jd buff时

                    //rate = __instance.parent.Health / __instance.parent.Stats.Health.Max;
                    //originalMaxHealth = __instance.parent.Stats.Health.Max;
                        
                    //flag = true;
                    break;
                }
            }
            ////合理附加jd buff时
            //for (int j = 1; j <= 10; j++)
            //{
            //    if (_name.Equals("jd" + j.ToString()) && __instance.parent != null)
            //    {
            //        flag = true;
            //        break;
            //    }
            //}
            return true;
        }
        
        //private static void Postfix(string _name, EntityBuffs __instance)
        //{
          
        //    //修改怪物血量
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        if (_name.Equals("jd" + i.ToString()) && flag)
        //        {
        //            flag = false;
        //            GameManager.ShowTooltip(GameManager.Instance.World.GetPrimaryPlayer(), rate.ToString());
        //            if(__instance.parent.Stats.Health.Max == originalMaxHealth)
        //            {
        //                __instance.parent.Stats.Health.BaseMax *= (int)Math.Round(i == 1 ? 1 : i * 0.6);
        //            }
        //            float temp = __instance.parent.Stats.Health.Max * rate;
        //            __instance.parent.Health = (int)(__instance.parent.Stats.Health.Max * rate);
        //            if (temp / __instance.parent.Stats.Health.Max > rate)
        //            {

        //            }
        //            break;
        //        }
        //    }


        //}

      
    }
}
