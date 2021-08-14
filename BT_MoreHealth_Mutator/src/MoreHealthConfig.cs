using BepInEx.Configuration;

namespace BT_MoreHealth_Mutator {
	public class MoreHealthConfig {
		private const string section_moreHealth = "MoreHealth";
		
		private readonly ConfigEntry<int> moreHealthMultiplier;

		public int MoreHealthMultiplier => moreHealthMultiplier.Value;

		public MoreHealthConfig(ConfigFile config) {
			moreHealthMultiplier = config.Bind(
					section: section_moreHealth,
					key: nameof(moreHealthMultiplier),
					defaultValue: 10,
					description: "The factor to apply to the agent's Endurance stat (which increases health linearly)");
		}
	}
}