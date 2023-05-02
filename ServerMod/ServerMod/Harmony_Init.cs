using System;
using HarmonyLib;

namespace _7DaysToDie
{
	// Token: 0x02000005 RID: 5
	internal class Harmony_Init : IModApi
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002334 File Offset: 0x00000534
		public void InitMod(Mod _modInstance)
		{
			string str = "Degradation | Loading patch: ";
			Type type = base.GetType();
			Console.WriteLine(str + ((type != null) ? type.ToString() : null));
			Harmony harmony = new Harmony(base.GetType().ToString());
			harmony.PatchAll();
		}
	}
}
