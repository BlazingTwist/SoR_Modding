using BepInEx;
using HarmonyLib;
using RogueLibsCore;

namespace BT_MoreHealth_Mutator {
	[BepInPlugin(ModInfo.BepInExPluginID, ModInfo.Title, ModInfo.Version)]
	public class MoreHealthPlugin : BaseUnityPlugin {
		private static UnlockBuilder MoreHealthMutator { get; set; }
		
		public static MoreHealthConfig MoreHealthConfig { get; private set; }
		public static bool IsMoreHealthMutatorEnabled => MoreHealthMutator?.Unlock.IsEnabled ?? false;

		private void Awake() {
			MoreHealthConfig = new MoreHealthConfig(Config);
			
			MoreHealthMutator = RogueLibs.CreateCustomUnlock(new MutatorUnlock(
							name: "bt.moreHealth",
							unlockedFromStart: true
					))
					.WithName(new CustomNameInfo(english: "More Health"))
					.WithDescription(new CustomNameInfo(english: "Increases your max health, no downsides."));

			new Harmony(ModInfo.BepInExHarmonyID).PatchAll();
		}
	}
}