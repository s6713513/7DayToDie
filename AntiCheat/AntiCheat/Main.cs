using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static GameUtils;
using static System.Net.Mime.MediaTypeNames;

namespace AntiCheat
{
    public class Main : IModApi
    {
       
        System.Timers.Timer t = new System.Timers.Timer();
        public void InitMod(Mod _modInstance)
        {
            string str = "AntiCheat | Loading patch: ";
            Type type = base.GetType();
            Console.WriteLine(str + ((type != null) ? type.ToString() : null));
            Harmony harmony = new Harmony(base.GetType().ToString());
            harmony.PatchAll();
            t = new System.Timers.Timer(5000);
            t.Elapsed += new System.Timers.ElapsedEventHandler(Execute);
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
            t.Start();
        }

        public void Execute(object source, System.Timers.ElapsedEventArgs e)
        {
            t.Stop();

            if (GameManager.Instance != null && GameManager.Instance.World != null)
            {
                foreach (KeyValuePair<int, EntityPlayer> dict in GameManager.Instance.World.Players.dict)
                {
                    EntityPlayer player = dict.Value;
                    if(player.Progression.Level == 300)
                    {
                        player.Progression.Level = 1;
                        player.Progression.ExpToNextLevel = 1;
                    }
                    if (player.inventory != null && player.inventory.holdingItem != null && (player.inventory.holdingItem.GetItemName().ToString().Contains("terrainRemove")||
                        player.inventory.holdingItem.GetItemName().ToString().Contains("terrainAdd") || player.inventory.holdingItem.GetItemName().ToString().Contains("toolTerrainModelerAdmin")))
                    {
                        DateTime dateTimes = DateTime.Now;
                        DateTime finalTime = dateTimes.AddYears(10);
                        ClientInfo clientInfo = ConsoleHelper.ParseParamIdOrName(player.entityId.ToString(), true, false);
                        GameUtils.EKickReason kickReason = GameUtils.EKickReason.Banned;
                        GameUtils.KickPlayerForClientInfo(clientInfo, new GameUtils.KickPlayerData(kickReason, 0, finalTime, "地形工具"));
                        GameManager.Instance.adminTools.AddBan(clientInfo.playerName, clientInfo.PlatformId, finalTime, "地形工具", true);
                    }                   
                }                
            }
            t.Start();
        }

    }
}
