﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace TourPlanner.HelperLayer.Logger
{
    public class LoggerWrapper : ILoggerWrapper
    {
        private readonly ILog logger;

        public static LoggerWrapper CreateLogger(string configPath, string caller)
        {
            if (!File.Exists(configPath))
            {
                //throw new ArgumentException("Does not exist.", nameof(configPath));
            }

            log4net.Config.XmlConfigurator.Configure(new FileInfo(configPath));
            var logger = LogManager.GetLogger(caller);  // System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
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