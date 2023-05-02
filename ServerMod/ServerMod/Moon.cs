using System;
using HarmonyLib;

namespace _7DaysToDie
{
	// Token: 0x02000004 RID: 4
	[HarmonyPatch(typeof(EntityPlayer))]
	[HarmonyPatch("AddKillXP")]
	public class Moon
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000022E0 File Offset: 0x000004E0
		private static void Prefix(EntityPlayer __instance, EntityAlive killedEntity, float xpModifier = 1f)
		{
			bool flag = killedEntity.EntityName.ToString().Equals("animalBearBossMod") || killedEntity.EntityName.ToString().Equals("animalBoarBossMod");
			if (flag)
			{
				GameManager.Instance.GameMessage(EnumGameMessages.EntityWasKilled, killedEntity, __instance);
			}
		}
	}
}
