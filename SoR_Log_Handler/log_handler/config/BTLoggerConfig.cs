using System.Collections.Generic;
using JetBrains.Annotations;

namespace SoR_Log_Handler.log_handler.config {
	[PublicAPI]
	public class BTLoggerConfig {
		public float fileCheckInterval;
		public Dictionary<string, BTLoggerConfigEntry> loggerConfig;
		public List<string> logMessageFilter;
	}
}