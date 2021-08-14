using HarmonyLib;
using JetBrains.Annotations;

namespace BT_MoreHealth_Mutator {
	[HarmonyPatch(declaringType: typeof(Agent))]
	public class AgentPatches {
		[UsedImplicitly, HarmonyPrefix, HarmonyPatch(methodName: nameof(Agent.SetEndurance), argumentTypes: new[] { typeof(int), typeof(bool) })]
		private static void SetEndurance_Prefix(ref int enduranceNum, bool doOnline, Agent __instance) {
			if (!MoreHealthPlugin.IsMoreHealthMutatorEnabled) {
				return;
			}

			// Only increase player-health
			if (__instance.isPlayer != 0) {
				enduranceNum = (enduranceNum <= 0 ? 1 : enduranceNum) * MoreHealthPlugin.MoreHealthConfig.MoreHealthMultiplier;
			}
		}
	}
}