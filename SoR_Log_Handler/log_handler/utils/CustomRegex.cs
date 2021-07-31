using System.Text.RegularExpressions;

namespace SoR_Log_Handler.log_handler.utils {
	public static class CustomRegex {
		public static bool IsMatch(string value, string regex) {
			if (regex == null) {
				return true;
			}

			if (regex.StartsWith("!")) {
				return !Regex.IsMatch(value, regex.Remove(0, 1));
			}

			if (regex.StartsWith(@"\!")) {
				regex = regex.Remove(0, 1);
			}

			return Regex.IsMatch(value, regex);
		}		
	}
}