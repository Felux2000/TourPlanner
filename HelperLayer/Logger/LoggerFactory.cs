using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.HelperLayer.Logger
{
    public class LoggerFactory
    {
        public static ILoggerWrapper GetLogger()
        {
            StackTrace stackTrace = new(1, false); //Captures 1 frame, false for not collecting information about the file
            var type = stackTrace.GetFrame(1).GetMethod().DeclaringType;
            return LoggerWrapper.CreateLogger(Path.GetFullPath("./log4net.config"), type.FullName);
        }
    }
}
