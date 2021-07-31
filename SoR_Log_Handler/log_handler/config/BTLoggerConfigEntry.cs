using JetBrains.Annotations;

namespace SoR_Log_Handler.log_handler.config {
	[PublicAPI]
	public class BTLoggerConfigEntry {
		public bool logMessage = true;
		public bool logStackTrace;

		public bool AnythingToLog() {
			return logMessage;
		}
	}
}