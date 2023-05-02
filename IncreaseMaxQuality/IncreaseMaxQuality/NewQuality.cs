using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;

// Token: 0x0200009D RID: 157
public class NewQuality
{
    // Token: 0x0200024A RID: 586
    [HarmonyPatch(typeof(QualityInfo), "Cleanup")]
    public class ModQuality
    {
        // Token: 0x06000C72 RID: 3186 RVA: 0x00081388 File Offset: 0x0007F588
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> list = new List<CodeInstruction>(instructions);
            int num = 0;
            for (int i = 0; i < list.Count; i++)
            {
                bool flag = list[i].opcode == OpCodes.Ldc_I4_7 && num < 2;
                if (flag)
                {
                    bool flag2 = num == 0;
                    if (flag2)
                    {
                    }
                    list[i].opcode = OpCodes.Ldc_I4_S;
                    list[i].operand = 17;
                    num++;
                }
                else
                {
                    bool flag3 = num >= 2;
                    if (flag3)
                    {
                        break;
                    }
                }
            }
            return list.AsEnumerable<CodeInstruction>();
        }
    }

    // Token: 0x0200024B RID: 587
    [HarmonyPatch(typeof(QualityInfo), "GetTierColor")]
    [HarmonyPatch(new Type[]
    {
        typeof(int)
    })]
    public class Tiercolor
    {
        // Token: 0x06000C74 RID: 3188 RVA: 0x00081438 File Offset: 0x0007F638
        private static bool Prefix(ref int _tier)
        {
            bool flag = _tier > 0;
            if (flag)
            {
                _tier = (int)Math.Ceiling(_tier / 10m);
            }
            return true;
        }
    }

    // Token: 0x0200024C RID: 588
    [HarmonyPatch(typeof(QualityInfo), "GetQualityColorHex")]
    [HarmonyPatch(new Type[]
    {
        typeof(int)
    })]
    public class Qualitycolorhex
    {
        // Token: 0x06000C76 RID: 31100 RVA: 0x00081480 File Offset: 0x0007F680
        private static bool Prefix(ref int _quality)
        {
            bool flag = _quality > 0;
            if (flag)
            {
                _quality = (int)Math.Ceiling(_quality / 10m);
            }
            return true;
        }
    }

    // Token: 0x0200024D RID: 589
    [HarmonyPatch(typeof(QualityInfo), MethodType.StaticConstructor)]
    public class ModQualityVars
    {
        // Token: 0x06000C78 RID: 3192 RVA: 0x000814C8 File Offset: 0x0007F6C8
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> list = new List<CodeInstruction>(instructions);
            int num = 0;
            for (int i = 0; i < list.Count; i++)
            {
                bool flag = list[i].opcode == OpCodes.Ldc_I4_7 && num < 2;
                if (flag)
                {
                    bool flag2 = num == 0;
                    if (flag2)
                    {
                    }
                    list[i].opcode = OpCodes.Ldc_I4_S;
                    list[i].operand = 17;
                    num++;
                }
                else
                {
                    bool flag3 = num >= 2;
                    if (flag3)
                    {
                        break;
                    }
                }
            }
            return list.AsEnumerable<CodeInstruction>();
        }
    }

    // Token: 0x0200024E RID: 5100
    [HarmonyPatch(typeof(XUiC_RecipeStack))]
    [HarmonyPatch("SetRecipe")]
    [HarmonyPatch(new Type[]
    {
        typeof(Recipe),
        typeof(int),
        typeof(float),
        typeof(bool),
        typeof(int),
        typeof(int),
        typeof(float)
    })]
    public class OutputQuality
    {
        // Token: 0x06000C7A RID: 3194 RVA: 0x00081578 File Offset: 0x0007F778
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> list = new List<CodeInstruction>(instructions);
            for (int i = 0; i < list.Count; i++)
            {
                bool flag = list[i].opcode == OpCodes.Ldc_R4 && (float)list[i].operand == 6f;
                if (flag)
                {
                   
                    list[i].operand = 160f;
                    break;
                }
            }
            return list.AsEnumerable<CodeInstruction>();
        }
    }

    // Token: 0x0200024F RID: 591
    [HarmonyPatch(typeof(QualityInfo))]
    [HarmonyPatch("GetQualityLevelName")]
    public class Qualityname
    {
        // Token: 0x06000C7C RID: 3196 RVA: 0x0008160C File Offset: 0x0007F80C
        private static bool Prefix(ref string __result, ref int _quality, ref bool _useQualityColor)
        {
            bool flag = _quality != 0;
            if (flag)
            {
                string text = string.Empty;
                _quality /= 10;
                switch (_quality)
                {
                    case 0:
                        text = Localization.Get("lblQualityDamaged");
                        break;
                    case 1:
                        text = Localization.Get("lblQualityPoor");
                        break;
                    case 2:
                        text = Localization.Get("lblQualityAverage");
                        break;
                    case 3:
                        text = Localization.Get("lblQualityGreat");
                        break;
                    case 4:
                        text = Localization.Get("lblQualityFlawless");
                        break;
                    case 5:
                        text = Localization.Get("lblQualityLegendary");
                        break;
                    case 6:
                        text = Localization.Get("lblQualityLegendaryPlus");
                        break;
                    case 7:
                        text = Localization.Get("lblQualityRelic");
                        break;
                    case 8:
                        text = Localization.Get("lblQualityDemonic");
                        break;
                    case 9:
                        text = Localization.Get("lblQualityCrazy");
                        break;
                    case 10:
                        text = Localization.Get("lblQualitySup1");
                        break;
                    case 11:
                        text = Localization.Get("lblQualitySup2");
                        break;
                    case 12:
                        text = Localization.Get("lblQualitySup3");
                        break;
                    case 13:
                        text = Localization.Get("lblQualitySup4");
                        break;
                    case 14:
                        text = Localization.Get("lblQualitySup5");
                        break;
                    case 15:
                        text = Localization.Get("lblQualitySup6");
                        break;
                    case 16:
                        text = Localization.Get("lblQualitySup7");
                        break;
                }
                bool flag2 = _useQualityColor;
                if (flag2)
                {
                    text = string.Format("[{0}]{1}[-]", QualityInfo.GetQualityColorHex(_quality), text);
                    __result = text;
                }
            }
            else
            {
                __result = Localization.Get("lblQualityBroken");
            }
            return false;
        }
    }

    // Token: 0x02000250 RID: 592
    [HarmonyPatch(typeof(TraderInfo))]
    [HarmonyPatch("SpawnItem")]
    public class TraderSpawnItem
    {
        // Token: 0x06000C7E RID: 3198 RVA: 0x0008172C File Offset: 0x0007F92C
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> list = new List<CodeInstruction>(instructions);
            for (int i = 0; i < list.Count; i++)
            {
                bool flag = i == 133 && list[i].opcode == OpCodes.Ldc_I4_6;
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

    // Token: 0x02000251 RID: 593
    [HarmonyPatch(typeof(XUiM_Trader))]
    [HarmonyPatch("GetBuyPrice")]
    public class Traderbuyprice
    {
        // Token: 0x06000C80 RID: 3200 RVA: 0x000817BC File Offset: 0x0007F9BC
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> list = new List<CodeInstruction>(instructions);
            for (int i = 0; i < list.Count; i++)
            {
                bool flag = list[i].opcode == OpCodes.Ldc_R4 && (float)list[i].operand == 5f;
                if (flag)
                {
                   
                    list[i].operand = 160f;
                    break;
                }
            }
            return list.AsEnumerable<CodeInstruction>();
        }
    }

    // Token: 0x02000252 RID: 594
    [HarmonyPatch(typeof(XUiM_Trader))]
    [HarmonyPatch("GetSellPrice")]
    public class Tradersellprice
    {
        // Token: 0x06000C82 RID: 3202 RVA: 0x00081850 File Offset: 0x0007FA50
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> list = new List<CodeInstruction>(instructions);
            for (int i = 0; i < list.Count; i++)
            {
                bool flag = list[i].opcode == OpCodes.Ldc_R4 && (float)list[i].operand == 5f;
                if (flag)
                {
                    
                    list[i].operand = 160f;
                    break;
                }
            }
            return list.AsEnumerable<CodeInstruction>();
        }
    }

    // Token: 0x02000253 RID: 595
    [HarmonyPatch(typeof(ItemValue), MethodType.Constructor)]
    [HarmonyPatch("ItemValue")]
    [HarmonyPatch(new Type[]
    {
        typeof(int),
        typeof(bool)
    })]
    public class Itemvaluequality
    {
        // Token: 0x06000C84 RID: 3204 RVA: 0x000818E4 File Offset: 0x0007FAE4
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> list = new List<CodeInstruction>(instructions);
            for (int i = 0; i < list.Count; i++)
            {
                bool flag = list[i].opcode == OpCodes.Ldc_I4_6;
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
}
