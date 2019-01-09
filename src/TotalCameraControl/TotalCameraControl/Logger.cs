using System;
using System.IO;

namespace TotalCameraControl
{
    public class Logger
    {
        public static void Reset()
        {
            FileInfo fi = new FileInfo(Logger.filePath);
            var di = fi.Directory;
            di.Attributes &= ~FileAttributes.ReadOnly;
            fi.Delete();
            fi.Create();
        }

        public static void Error(Exception ex)
        {
            FileInfo fi = new FileInfo(Logger.filePath);
            var di = fi.Directory;
            di.Attributes &= ~FileAttributes.ReadOnly;

            using (StreamWriter streamWriter = new StreamWriter(Logger.filePath, true))
            {
                streamWriter.WriteLine(string.Concat(new string[]
                {
                    "Message :",
                    ex.Message,
                    "<br/>",
                    Environment.NewLine,
                    "StackTrace :",
                    ex.StackTrace
                }) + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                streamWriter.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }

        public static void Log(string line, bool extras)
        { 
            FileInfo fi = new FileInfo(Logger.filePath);
            var di = fi.Directory;
            di.Attributes &= ~FileAttributes.ReadOnly;

            using (StreamWriter streamWriter = new StreamWriter(Logger.filePath, true))
            {
                if (extras)
                {
                    streamWriter.WriteLine(line + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    streamWriter.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                } else
                {
                    streamWriter.WriteLine(line);
                }
            }
        }

        private static string filePath = string.Format("{0}/Log.txt", TotalCameraControl.ModDirectory);
    }
}