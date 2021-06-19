using JetBrains.Annotations;

namespace SoR_Music_Loader.log_handler.config {
	[PublicAPI]
	public class BTLoggerConfigEntry {
		public bool logMessage = true;
		public bool logStackTrace;

		public bool AnythingToLog() {
			return logMessage;
		}
	}
}