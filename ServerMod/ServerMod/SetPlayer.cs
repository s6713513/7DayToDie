using System;
using System.Collections.Generic;

namespace _7DaysToDie
{
	
	internal class SetPlayer : ConsoleCmdAbstract
	{

		public override void Execute(List<string> _params, CommandSenderInfo _senderInfo)
		{
			bool flag = _params.Count != 3;
			if (flag)
			{
				SingletonMonoBehaviour<SdtdConsole>.Instance.Output("Invalid arguments, requires a target player, a skill name and level");
			}
			else
			{
				string str = _params[1] + " " + _params[2];
				ClientInfo clientInfo = ConsoleHelper.ParseParamIdOrName(_params[0], true, false);
				bool flag2 = clientInfo != null;
				if (flag2)
				{
					SingletonMonoBehaviour<ConnectionManager>.Instance.SendToServer(NetPackageManager.GetPackage<NetPackageConsoleCmdClient>().Setup("setskilllevel " + str, true));
				}


				else
				{
					bool isLocalGame = _senderInfo.IsLocalGame;
					if (isLocalGame)
					{
						SingletonMonoBehaviour<SdtdConsole>.Instance.Output("Use the \"setskilllevel\" command for the local player.");
					}
					else
					{
						SingletonMonoBehaviour<SdtdConsole>.Instance.Output("Playername or entity ID not found.");
					}
				}
				
			}
		}

		
		public override string[] GetCommands()
		{
			return new string[]
			{
				"setplayer"
			};
		}

		
		public override string GetDescription()
		{
			return "set skill level";
		}
	}
}
