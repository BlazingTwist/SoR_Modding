using BepInEx;
using SoR_Music_Loader.log_handler;

namespace SoR_Music_Loader {
	[BepInPlugin(pluginGuid, pluginName, pluginVersion)]
	[BepInProcess("StreetsOfRogue.exe")]
	public class SorLogHandler : BaseUnityPlugin {
		public const string pluginGuid = "blazingtwist.sor.loghandler";
		private const string pluginName = "BlazingTwist SoR LogHandler";
		private const string pluginVersion = "1.0.0";

		public void Awake() {
			LoggerConfigManager.AttachToScene();
		}
	}
}