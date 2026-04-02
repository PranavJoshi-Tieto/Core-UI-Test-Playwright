using Serilog;
using Serilog.Events;

namespace PlaywrightFramework.Utils
{
    public static class Logger
    {
        private static ILogger? _logger;

        public static ILogger Instance
        {
            get
            {
                if (_logger == null)
                {
                    var logsDir = Path.Combine(Directory.GetCurrentDirectory(), "logs");
                    Directory.CreateDirectory(logsDir);

                    _logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.Console(outputTemplate:
                            "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                        .WriteTo.File(
                            path: Path.Combine(logsDir, "test-.log"),
                            rollingInterval: RollingInterval.Day,
                            outputTemplate:
                                "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                        .CreateLogger();
                }
                return _logger;
            }
        }

        public static void Info(string message) => Instance.Information(message);
        public static void Debug(string message) => Instance.Debug(message);
        public static void Warning(string message) => Instance.Warning(message);
        public static void Error(string message, Exception? ex = null)
        {
            if (ex != null)
                Instance.Error(ex, message);
            else
                Instance.Error(message);
        }
    }
}
