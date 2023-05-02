using HarmonyLib;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

namespace CuteServer
{
    
    public class Recycle
    {
        
        [HarmonyPatch(typeof(XUiC_ULM_RecyclerWindow))]
        [HarmonyPatch("Update")]
        public class LockGrid
        {
            static void SetLocked(XUiC_ULM_RecyclerWindow __instance, bool isOn)
            {
                foreach (XUiController controller in __instance.gridInput.GetItemStackControllers())
                {
                    XUiC_ItemStack xuiC_RequiredItemStack = controller as XUiC_ItemStack;

                    if (xuiC_RequiredItemStack != null)
                    {
                        xuiC_RequiredItemStack.ToolLock = isOn;
                    }
                }
            }
            private static void Prefix(XUiC_ULM_RecyclerWindow __instance)
            {
                TileEntityRecycler te = (TileEntityRecycler)AccessTools.Field(typeof(XUiC_ULM_RecyclerWindow), "te").GetValue(__instance);
                //foreach(FieldInfo field in AccessTools.GetDeclaredFields(__instance.GetType()))
                //{
                //    FileLog.Log(field.FieldType.Name + " " + field.Name + "\n");
                //}
                if (te.isRunning)
                {
                    SetLocked(__instance, true);
                    //return false;
                }
                else
                {
                    SetLocked(__instance, false);
                }

              
            }
        }

        //[HarmonyPatch(typeof(TileEntityRecycler))]
        //[HarmonyPatch("HandleQueueRecursively")]
        //public class HandleQueueRecursively
        //{          
        //    private static void Prefix(TileEntityRecycler __instance)
        //    {
        //        if(__instance.queueTimeLeft == 0)
        //        {
        //            FileLog.Log("分解完成1");
        //        }


        //    }
        //}

        [HarmonyPatch(typeof(TileEntityRecycler))]
        [HarmonyPatch("AddToOutput")]
        public class AddToOutput
        {
            static DateTime lastTime = DateTime.MinValue;
            private static void Prefix(TileEntityRecycler __instance, ULM_RecycleRecipe recipe, XUiC_ULM_RecyclerWindow window = null)
            {
                __instance.queueTimeLeft = 1;
            }
        }
    }

}
