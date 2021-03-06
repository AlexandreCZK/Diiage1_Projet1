using System;
using System.Collections.Generic;
using System.Text;

namespace Diiage2020.CSharpGame.Server.Services
{
    public class ServerLog
    {
        private const ConsoleColor ERROR_CONSOLE_COLOR = ConsoleColor.Red;
        private const ConsoleColor WARNING_CONSOLE_COLOR = ConsoleColor.Yellow;
        private const ConsoleColor LOG_CONSOLE_COLOR = ConsoleColor.Cyan;
        private const ConsoleColor DEBUG_CONSOLE_COLOR = ConsoleColor.White;

        public static void Debug(string message)
        {
#if DEBUG
            WriteColorfulConsoleLine(DEBUG_CONSOLE_COLOR, string.Format("[DEBUG] {0}", message));
#endif
        }
        public static void Log(string message)
        {
            WriteColorfulConsoleLine(LOG_CONSOLE_COLOR, string.Format("[LOG] {0}", message));
        }
        public static void Warn(string message)
        {
            WriteColorfulConsoleLine(WARNING_CONSOLE_COLOR, string.Format("[WARNING] {0}", message));
        }
        public static void Error(string message)
        {
            WriteColorfulConsoleLine(ERROR_CONSOLE_COLOR, string.Format("[ERROR] {0}", message));
        }
        public static void Error(Exception exception)
        {
            WriteColorfulConsoleLine(ERROR_CONSOLE_COLOR, string.Format("[ERROR] {0}\n{1}", exception.Message, exception.StackTrace));
        }
        private static void WriteColorfulConsoleLine(ConsoleColor color, string message, bool showTimestamp = true)
        {
            if (message.Trim().Equals("")) return;
            ConsoleColor defaultColor = Console.ForegroundColor;
            string timestamp = showTimestamp ? DateTime.Now.ToString("hh:mm:ss") : "";
            Console.ForegroundColor = color;
            Console.WriteLine(string.Format("[{0}] {1}", timestamp, message));
            Console.ForegroundColor = defaultColor;
        }
        private static void WriteInTxtFiles(string message)
        {

        }
    }
}
