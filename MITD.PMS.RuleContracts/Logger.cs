using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.RuleContracts
{
    public class Logger
    {
        public static void Log(string log)
        {
            using (StreamWriter _testData = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\CalculationLog.txt", true))
            {
                _testData.WriteLine(log);
            }
        }

        //private static string getFileLogPath()
        //{
        //    //string logPath = ConfigurationManager.AppSettings["LogFilesPath"];
        //    //string fileCreationType = ConfigurationManager.AppSettings["LogFileCreationType"];
        //    //string filePath = AppDomain.CurrentDomain.BaseDirectory + logPath + "\\Log" +
        //    //    "-" + DateTime.Today.ToString(fileCreationType) + ".log";

        //    DirectoryInfo logDirInfo = null;
        //    FileInfo logFileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory+"\\log.txt");

        //    logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
        //    if (!logDirInfo.Exists)
        //        logDirInfo.Create();

        //    return filePath;
        //}
    }
}
