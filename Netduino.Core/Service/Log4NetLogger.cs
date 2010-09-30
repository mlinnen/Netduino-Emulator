using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace Netduino.Core.Services
{
    public class Log4netLogger : ILog
    {
        #region Fields
        private readonly log4net.ILog _innerLogger;
        #endregion

        #region Constructors
        public Log4netLogger(Type type)
        {
            //_innerLogger = log4net.LogManager.GetLogger(type);
            log4net.ILog[] loggers = log4net.LogManager.GetCurrentLoggers();
            _innerLogger = log4net.LogManager.GetLogger("Logging");
            log4net.Config.DOMConfigurator.Configure();

        }
        #endregion

        #region ILog Members
        public void Error(Exception exception)
        {
            _innerLogger.Error(exception.Message, exception);
        }
        public void Info(string format, params object[] args)
        {
            _innerLogger.InfoFormat(format, args);
        }
        public void Warn(string format, params object[] args)
        {
            _innerLogger.WarnFormat(format, args);
        }
        #endregion
    }
}
