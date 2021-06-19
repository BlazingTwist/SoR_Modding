using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BlazingTwistConfigTools.blazingtwist.config;
using SoR_Music_Loader.log_handler.config;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace SoR_Music_Loader.log_handler {
	public class LoggerConfigManager : MonoBehaviour {
		private static string configPath;
		private static string logFilePath;
		private static GameObject targetParent;
		private static float nextConfigCheckIn;
		private static BTLoggerConfig config;

		internal static void AttachToScene() {
			if (targetParent != null) {
				return;
			}

			configPath = Application.dataPath + "/../BepInEx/config/" + SorLogHandler.pluginGuid + ".cs";
			logFilePath = Application.dataPath + "/../BepInEx/" + SorLogHandler.pluginGuid + "_log.txt";
			Application.logMessageReceived += HandleLog;
			ReloadConfig();

			try {
				targetParent = new GameObject("BTLogHandlerObject");
				targetParent.AddComponent<LoggerConfigManager>();
				DontDestroyOnLoad(targetParent);
			} catch (Exception e) {
				Debug.LogError("Attaching LoggerConfigManager failed due to exception: " + e);
			}
		}

		private void LateUpdate() {
			nextConfigCheckIn -= Time.unscaledDeltaTime;
			if (nextConfigCheckIn <= 0) {
				ReloadConfig();
			}
		}

		private static void ReloadConfig() {
			config = BTConfigUtils.LoadConfigFile(configPath, config);
			nextConfigCheckIn = config?.fileCheckInterval ?? 30f;
		}

		private static void HandleLog(string logString, string stackTrace, LogType type) {
			if (string.IsNullOrWhiteSpace(logString)) {
				logString = "nullString";
			}
			if (string.IsNullOrWhiteSpace(stackTrace)) {
				stackTrace = "nullString";
			}

			string logTypeString = type.ToString();
			BTLoggerConfigEntry loggerConfigEntry;
			if (config != null && config.loggerConfig.ContainsKey(logTypeString)) {
				loggerConfigEntry = config.loggerConfig[logTypeString];
			} else {
				loggerConfigEntry = new BTLoggerConfigEntry();
			}

			if (!loggerConfigEntry.AnythingToLog()) {
				return;
			}
			if (config != null && config.logMessageFilter.Any(filter => Regex.IsMatch(logString, filter))) {
				return;
			}
			
			var logBuilder = new StringBuilder();
			logBuilder
					.Append("[")
					.Append(logTypeString)
					.Append("]\t[")
					.Append(DateTime.Now.ToString("T"))
					.Append("]");

			if (loggerConfigEntry.logMessage) {
				logBuilder
						.Append("\n")
						.Append(logString);
			}

			if (loggerConfigEntry.logStackTrace) {
				logBuilder.Append("\nProvided Trace: ").Append(stackTrace);
				var trace = new StackTrace();
				logBuilder.Append("\n").Append(trace);
			}

			logBuilder.Append("\n");

			using (var writer = new StreamWriter(logFilePath.Replace('/', Path.DirectorySeparatorChar), true)) {
				writer.WriteLine(logBuilder.ToString());
			}
		}
	}
}