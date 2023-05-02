using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

// Token: 0x0200009D RID: 157
public class OnPress
{
    
    // Token: 0x0200024A RID: 586
    [HarmonyPatch]
    public class MaxQuality
    {
        public static MethodBase TargetMethod()
        {
            return AccessTools.Method("XUiC_ULM_ModificationStationWindow:UpdateData", new Type[] { }, null);           
        }      
       
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> list = new List<CodeInstruction>(instructions);            
            for (int i = 0; i < list.Count; i++)
            {
                bool flag = list[i].opcode == OpCodes.Ldc_I4_S;
                if (flag)
                {
                    list[i].opcode = OpCodes.Ldc_I4;
                    list[i].operand = 160;
                    break;
                }
            }
            return list.AsEnumerable<CodeInstruction>();
        }
    }

    [HarmonyPatch]
    public class StepQuality
    {
        //static int num = 0;
        //static bool isModItem = false;
        static bool flag = false;
        public static MethodBase TargetMethod()
        {
            return AccessTools.Method("XUiC_ULM_ModificationStationWindow:PerformUpgrade", new Type[] { typeof(ItemStack),typeof(ULM_ItemModification) }, null);           
        }

        //private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        //{
        //    List<CodeInstruction> list = new List<CodeInstruction>(instructions);

        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        bool flag = list[i].opcode == OpCodes.Ldc_I4_S;               
        //        if (flag)
        //        {                   
        //            if (num != 0)
        //            {
        //                list[i].opcode = OpCodes.Ldc_I4;
        //                list[i].operand = 160;
        //            }
        //            num++;
        //        }              
        //    }
        //    return list.AsEnumerable<CodeInstruction>();
        //}
        private static bool Prefix(ItemStack _itemStack, ULM_ItemModification modification)
        {
            EntityPlayerLocal playerLocal = GameManager.Instance.World.GetPrimaryPlayer();
            //GameManager.ShowTooltip(playerLocal, playerLocal.Progression.Level.ToString());
            if (_itemStack.itemValue.Quality >= 90)
            {
                foreach (string s in _itemStack.itemValue.ItemClass.ItemTags.GetTagNames())
                {
                    if (s.Contains("tool"))
                    {
                        GameManager.ShowTooltip(playerLocal, "工具无法继续升级");
                        flag = false;
                        return false;
                    }
                }
            }
            if (_itemStack.itemValue.Quality >= 90 && playerLocal.Progression.Level < 50)
            {                
                GameManager.ShowTooltip(playerLocal, "玩家等级小于50无法升级");
                flag = false;
                return false;
            }
            if (_itemStack.itemValue.Quality >= 120 && playerLocal.Progression.Level < 90)
            {
                GameManager.ShowTooltip(playerLocal, "玩家等级小于90无法升级");
                flag = false;
                return false;
            }
            if (_itemStack.itemValue.Quality >= 140 && playerLocal.Progression.Level < 152)
            {
                GameManager.ShowTooltip(playerLocal, "玩家等级小于152无法升级");
                flag = false;
                return false;
            }            
            flag = true;
            return true;
        }
        private static void Postfix(ref ItemStack _itemStack)
        {
            if (GameManager.Instance.World.GetPrimaryPlayer().bag.HasItem(ItemClass.GetItem("maxSuccess", false)) && flag)
            {
                GameManager.Instance.World.GetPrimaryPlayer().bag.DecItem(ItemClass.GetItem("maxSuccess", false), 1);
            }
           
            //num = 0;
            //if (isModItem && _itemStack.itemValue.Quality > 90)
            //{
            //    AccessTools.
            //}
        }
    }

    [HarmonyPatch]
    public class FilterItems
    {
        
        public static MethodBase TargetMethod()
        {
            return AccessTools.Method("XUiC_ULM_ModificationStationGrid:SetItemInSlot", new Type[] { typeof(int), typeof(ItemStack) }, null);
        }

        private static void Prefix(int i, ref ItemStack stack)
        {
            if (stack.itemValue.Quality > 90)
            {                
                foreach (string s in stack.itemValue.ItemClass.ItemTags.GetTagNames())
                {
                    if (s.Contains("tool"))
                    {
                        ItemValue itemValue = new ItemValue(stack.itemValue.type, 90, 90, false, null, 1f);
                        stack.itemValue = itemValue;
                        break;
                    }
                }
            }
        }
      
    }

    [HarmonyPatch]
    public class ProbSuccess
    {
        public static MethodBase TargetMethod()
        {
            return AccessTools.Method("XUiC_ULM_ModificationStationWindow:GetUpgradeChance", new Type[] {  }, null);           
        }
       
        private static void Postfix(ref int __result)
        {
            if (GameManager.Instance.World.GetPrimaryPlayer().bag.HasItem(ItemClass.GetItem("maxSuccess", false)))
            {
                __result = 100;
            }
        }
    }

}
