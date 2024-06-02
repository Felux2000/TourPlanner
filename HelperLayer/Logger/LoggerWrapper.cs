using log4net;

namespace TourPlanner.HelperLayer.Logger
{
    public class LoggerWrapper : ILoggerWrapper
    {
        private readonly ILog logger;

        public static LoggerWrapper CreateLogger(string configPath, string caller)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(configPath));
            var logger = LogManager.GetLogger(caller);
            return new LoggerWrapper(logger);
        }

        private LoggerWrapper(ILog logger)
        {
            this.logger = logger;
        }

        public void Debug(string message)
        {
            this.logger.Debug(message);
        }
        public void Warn(string message)
        {
            this.logger.Warn(message);
        }

        public void Error(string message)
        {
            this.logger.Error(message);
        }

        public void Fatal(string message)
        {
            this.logger.Fatal(message);
        }
    }
}
