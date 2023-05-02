using System;
using System.Collections.Generic;

namespace _7DaysToDie
{
	// Token: 0x02000003 RID: 3
	internal class SetSkillLevel : ConsoleCmdAbstract
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000214C File Offset: 0x0000034C
		public override bool IsExecuteOnClient
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002160 File Offset: 0x00000360
		public override void Execute(List<string> _params, CommandSenderInfo _senderInfo)
		{
			if (!_senderInfo.IsLocalGame)
			{
				SingletonMonoBehaviour<SdtdConsole>.Instance.Output("Command can only be used on clients, use \"setplayer\" instead for other players / remote clients");
				return;
			}
			int level = -1;
			if (_params.Count == 2 && int.TryParse(_params[1], out level))
			{
				EntityPlayerLocal primaryPlayer = GameManager.Instance.World.GetPrimaryPlayer();
				if (primaryPlayer != null)
				{
					if (_params[0].Equals("vip"))
					{
						for (int i = 0; i < this.buffs.Length; i++)
						{
							primaryPlayer.Buffs.RemoveBuff(this.buffs[i], true);
						}
						primaryPlayer.Buffs.AddBuff("buff" + _params[1], -1, true, false, false);
					}
					primaryPlayer.Progression.GetProgressionValue(_params[0]).Level = level;
					return;
				}
			}
			else
			{
				SingletonMonoBehaviour<SdtdConsole>.Instance.Output("setskilllevel requires two arguments!");
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002248 File Offset: 0x00000448
		public override string[] GetCommands()
		{
			return new string[]
			{
				"setskilllevel"
			};
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002268 File Offset: 0x00000468
		public override string GetDescription()
		{
			return "Set a skill to correct level ";
		}

		// Token: 0x04000001 RID: 1
		private string[] buffs = new string[]
		{
			"buff0",
			"buff1",
			"buff2",
			"buff3",
			"buff4",
			"buff5",
			"buff6",
			"buff7"
		};
	}
}
