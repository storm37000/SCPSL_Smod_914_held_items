using Smod2;
using Smod2.Attributes;
using Smod2.Events;
using Smod2.EventHandlers;
using System;

namespace Smod.SCP914HeldItems
{
    [PluginDetails(
        author = "storm37000",
        name = "914 Held Items",
        description = "SCP-914 effects held items.",
        id = "s37k.914helditems",
        version = "1.0.6",
        SmodMajor = 3,
        SmodMinor = 1,
        SmodRevision = 21
        )]
    class SCP914HeldItemsPlugin : Plugin
    {
        public override void OnDisable()
        {
            this.Info(this.Details.name + " has been disabled.");
        }
		public override void OnEnable()
		{
			bool SSLerr = false;
			this.Info(this.Details.name + " has been enabled.");
			string hostfile = "http://pastebin.com/raw/9VQi53JQ";
			string[] hosts = new System.Net.WebClient().DownloadString(hostfile).Split('\n');
			while (true)
			{
				try
				{
					string host = hosts[0];
					if (SSLerr) { host = hosts[1]; }
					ushort version = ushort.Parse(this.Details.version.Replace(".", string.Empty));
					ushort fileContentV = ushort.Parse(new System.Net.WebClient().DownloadString(host + this.Details.name + ".ver"));
					if (fileContentV > version)
					{
						this.Info("Your version is out of date, please visit the Smod discord and download the newest version");
					}
					break;
				}
				catch (System.Exception e)
				{
					if (SSLerr == false)
					{
						SSLerr = true;
						continue;
					}
					this.Error("Could not fetch latest version txt: " + e.Message);
					break;
				}
			}
		}

		public override void Register()
        {
            // Register Events
            this.AddEventHandler(typeof(IEventHandlerSCP914Activate), new EventHandler(this), Priority.Highest);
        }
    }
}
