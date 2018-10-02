using Smod2;
using Smod2.API;
using Smod2.Events;
using Smod2.EventHandlers;

namespace Smod.TestPlugin
{
    class EventHandler : IEventHandlerSCP914Activate
    {
        private static readonly System.Random getrandom = new System.Random();
        private Plugin plugin;

        public EventHandler(Plugin plugin)
        {
            this.plugin = plugin;
        }

        public void OnSCP914Activate(SCP914ActivateEvent ev)
        {
            KnobSetting settingID = ev.KnobSetting;
            object[] inputs = ev.Inputs;
            Scp914 objectOfType = UnityEngine.Object.FindObjectOfType<Scp914>();
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
            foreach (UnityEngine.Collider collider in inputs)
            {
                Pickup component1 = collider.GetComponent<Pickup>();
                if ((UnityEngine.Object)component1 == (UnityEngine.Object)null)
                {
                    NicknameSync component2 = collider.GetComponent<NicknameSync>();
                    CharacterClassManager component3 = collider.GetComponent<CharacterClassManager>();
                    PlyMovementSync component4 = collider.GetComponent<PlyMovementSync>();
                    PlayerStats component5 = collider.GetComponent<PlayerStats>();
                    if ((UnityEngine.Object)component2 != (UnityEngine.Object)null && (UnityEngine.Object)component3 != (UnityEngine.Object)null && ((UnityEngine.Object)component4 != (UnityEngine.Object)null && (UnityEngine.Object)component5 != (UnityEngine.Object)null) && (UnityEngine.Object)collider.gameObject != (UnityEngine.Object)null)
                    {
                        UnityEngine.GameObject gameObject = collider.gameObject;
                        ServerMod2.API.SmodPlayer player = new ServerMod2.API.SmodPlayer(gameObject);
                        if (player.TeamRole.Team != Smod2.API.Team.SCP && player.TeamRole.Team != Smod2.API.Team.NONE && player.TeamRole.Team != Smod2.API.Team.SPECTATOR)
                        {
                            if ((UnityEngine.Object)objectOfType == (UnityEngine.Object)null)
                            {
                                this.plugin.Error("Couldnt find SCP-914");
                            }
                            else
                            {
                                foreach (Smod2.API.Item item in player.GetInventory())
                                {
                                    sbyte outputitem = (sbyte)(objectOfType.recipes[(byte)item.ItemType].outputs[(byte)settingID].outputs[getrandom.Next(0,objectOfType.recipes[(byte)item.ItemType].outputs[(byte)settingID].outputs.Count)]);
                                    item.Remove();
                                    this.plugin.Debug(item.ItemType + " ==> " + (ItemType)outputitem);
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
        }
    }
}