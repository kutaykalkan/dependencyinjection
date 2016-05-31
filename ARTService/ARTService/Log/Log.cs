using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SkyStem.ART.Service.Log
{
    public class Log
    {
        private string logFileName;
        private string logFileNameSecondary;
        private int maxLogSize = 20 * 1024 * 1024;
        private static Log log = new Log();

        public static Log GetInstance()
        {
            return log;
        }
        private Log()
        {
            logFileName = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\log.txt";
            logFileNameSecondary = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\log.tx_";
        }
        public void WriteLn(string line)
        {
            //IS : Lock Added
            lock (this)
            {
                try
                {
                    DeleteLogIfTooBig();
                    TextWriter tw = new StreamWriter(logFileName, true);
                    tw.WriteLine(DateTime.Now + ": " + line);
                    //Console.WriteLine(DateTime.Now + ": " + line);
                    tw.Close();
                }
                catch (Exception)
                { 
                }
            }

        }
        private void DeleteLogIfTooBig()
        {
            try
            {
                FileInfo fi = new FileInfo(logFileName);
                if (fi.Length > maxLogSize)
                {
                    TextWriter tw = new StreamWriter(logFileName, true);
                    tw.WriteLine(DateTime.Now + ": " + "File reached its maximum limit. Swapping to new file.");
                    tw.Close();

                    fi.CopyTo(logFileNameSecondary, true);
                    fi.Delete();
                }

            }
            catch
            {
                //TODO: Where to throw exception
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SkyStem.ART.Service.Log
{
    public class Log
    {
        private string logFileName;
        private string logFileNameSecondary;
        private int maxLogSize = 20 * 1024 * 1024;
        private static Log log = new Log();

        public static Log GetInstance()
        {
            return log;
        }
        private Log()
        {
            logFileName = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\log.txt";
            logFileNameSecondary = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\log.tx_";
        }
        public void WriteLn(string line)
        {
            //IS : Lock Added
            lock (this)
            {
                try
                {
                    DeleteLogIfTooBig();
                    TextWriter tw = new StreamWriter(logFileName, true);
                    tw.WriteLine(DateTime.Now + ": " + line);
                    //Console.WriteLine(DateTime.Now + ": " + line);
                    tw.Close();
                }
                catch (Exception)
                { 
                }
            }

        }
        private void DeleteLogIfTooBig()
        {
            try
            {
                FileInfo fi = new FileInfo(logFileName);
                if (fi.Length > maxLogSize)
                {
                    TextWriter tw = new StreamWriter(logFileName, true);
                    tw.WriteLine(DateTime.Now + ": " + "File reached its maximum limit. Swapping to new file.");
                    tw.Close();

                    fi.CopyTo(logFileNameSecondary, true);
                    fi.Delete();
                }

            }
            catch
            {
                //TODO: Where to throw exception
            };
        }
    }
}
