using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;

namespace HW.Core.Services
{
	public class LoggingService
	{
		static ILog log;
		
		static LoggingService()
		{
			XmlConfigurator.Configure();
			log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		}
		
		public static void Debug(string message)
		{
			log.Debug(message);
		}
		
		public static void Info(string message)
		{
//			log.Info(message);
			using (StreamWriter w = File.AppendText("log.txt")) {
				w.WriteLine(message);
			}
		}
	}
}
