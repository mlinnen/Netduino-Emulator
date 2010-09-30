using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.IO;

namespace Netduino.Core.Service
{
    public class TextFileLogger : ILog
    {
        public void Error(Exception exception)
        {
            File.AppendAllText("log.txt", exception.Message);
        }

        public void Info(string format, params object[] args)
        {
            File.AppendAllText("log.txt", string.Format(format,args));
            File.AppendAllText("log.txt", Environment.NewLine);
        }

        public void Warn(string format, params object[] args)
        {
            File.AppendAllText("log.txt", string.Format(format, args));
            File.AppendAllText("log.txt", Environment.NewLine);
        }
    }
}
