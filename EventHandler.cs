using Smod2.API;
using Smod2.Events;
using Smod2.EventHandlers;
using System.Linq;
using System.Collections.Generic;

namespace SCP914HeldItems
{
	class EventHandler : IEventHandlerSCP914Activate
	{
		private static readonly System.Random getrandom = new System.Random();
		private Main plugin;

		public EventHandler(Main plugin)
		{
			this.plugin = plugin;
		}

		public void OnSCP914Activate(SCP914ActivateEvent ev)
		{
			Scp914 objectOfType = UnityEngine.Object.FindObjectOfType<Scp914>();
			if ((UnityEngine.Object)objectOfType == (UnityEngine.Object)null)
			{
				this.plugin.Error("Couldnt find SCP-914");
				return;
			}
//            string[] strArray = new string[5] { "Very Rough", "Rough", "1 to 1", "Fine", "Very Fine" };
//            for (byte index1 = 0; index1 < objectOfType.recipes.Length; ++index1) //item id
//            {
//                this.plugin.Debug("==== Recipe for: " + component.availableItems[index1].label + " ====");
//                for (byte index2 = 0; index2 < objectOfType.recipes[index1].outputs.Count; ++index2) //knob setting id
//                {
//                    foreach(sbyte itm in objectOfType.recipes[index1].outputs[index2].outputs) //output item id
//                    {
//                        this.plugin.Debug(strArray[index2] + ": " + (itm == -1 ? "NULL" : component.availableItems[itm].label));
//                    }
//                }
//            }
			IEnumerable<Player> players = plugin.Server.GetPlayers().Where(p => ev.Inputs.OfType<UnityEngine.CapsuleCollider>().Select(s => s.GetComponent<CharacterClassManager>().SteamId).Any(s => p.SteamId == s));
			foreach (Player player in players)
			{
				if (player.TeamRole.Team != Smod2.API.Team.SCP && player.TeamRole.Team != Smod2.API.Team.NONE && player.TeamRole.Team != Smod2.API.Team.SPECTATOR)
				{
					foreach (Smod2.API.Item item in player.GetInventory())
					{
						sbyte outputitem = -2;
						try
						{
							outputitem = (sbyte)(objectOfType.recipes[(byte)item.ItemType].outputs[(byte)ev.KnobSetting].outputs[getrandom.Next(0, objectOfType.recipes[(byte)item.ItemType].outputs[(byte)ev.KnobSetting].outputs.Count)]);
						}
						catch (System.Exception)
						{
							this.plugin.Error("Recipe for " + item.ItemType + "does not exist!  Ask the game devs to add a recipe for it!");
						}
						if (outputitem != -2)
						{
							item.Remove();
							this.plugin.Debug(item.ItemType + " ==> " + (ItemType)outputitem);
						}
						if (outputitem >= 0)
						{
							player.GiveItem((ItemType)outputitem);
						}
					}
				}
			}
		}
	}
}
