using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncreaseMaxQuality
{
    internal class Harmony_Init : IModApi
    {
        // Token: 0x0600000C RID: 12 RVA: 0x00002334 File Offset: 0x00000534
        public void InitMod(Mod _modInstance)
        {
            string str = "IncreaseMaxQuality | Loading patch: ";
            Type type = base.GetType();
            Console.WriteLine(str + ((type != null) ? type.ToString() : null));
            Harmony harmony = new Harmony(base.GetType().ToString());
            harmony.PatchAll();
        }
    }
}
