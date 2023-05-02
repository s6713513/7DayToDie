using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsideCheat
{
    internal class Main : IModApi
    {
        
        public void InitMod(Mod _modInstance)
        {
            //string str = "Degradation | Loading patch: ";
            //Type type = base.GetType();
            //Console.WriteLine(str + ((type != null) ? type.ToString() : null));
            //Harmony harmony = new Harmony(base.GetType().ToString());
            //harmony.PatchAll();           
            InsideCheat cheat = new InsideCheat();
            
        }


    }
}
