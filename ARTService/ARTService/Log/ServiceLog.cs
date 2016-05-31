using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Service.Data;

namespace SkyStem.ART.Service.Log
{
    public class ServiceLog
    {
        public static void AddToFile(string contents)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                //set up a filestream
                string filePath = GetServiceLogFilePath();
                fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);

                //set up a streamwriter for adding text
                sw = new StreamWriter(fs);

                //find the end of the underlying filestream
                sw.BaseStream.Seek(0, SeekOrigin.End);

                //add the text
                sw.WriteLine(contents);
                //add the text to the underlying filestream

                sw.Flush();
                //close the writer
                sw.Close();
            }
            catch (Exception ex)
            {

            }
            //finally
            //{
            //    if (sw != null)
            //    {
            //        sw.Flush();
            //        sw.Close();
            //    }
            //}
        }

        public static void AddToFile(Exception oException)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                //set up a filestream
                string filePath = GetServiceLogFilePath();
                fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);

                //set up a streamwriter for adding text
                sw = new StreamWriter(fs);

                //find the end of the underlying filestream
                sw.BaseStream.Seek(0, SeekOrigin.End);

                //add the text
                sw.WriteLine(oException.Source);
                sw.WriteLine(oException.Message);
                sw.WriteLine(oException.StackTrace);
                //add the text to the underlying filestream

                sw.Flush();
                //close the writer
                sw.Close();
            }
            catch (Exception ex)
            {

            }
            //finally
            //{
            //    if (sw != null)
            //    {
            //        sw.Flush();
            //        sw.Close();
            //    }
            //}
        }

        private static string GetServiceLogFilePath()
        {
            string filepath = "";
            filepath = Helper.GetAppSettingFromKey("ServiceLogFilePath");
            if (filepath.Equals(string.Empty))
                filepath = ServiceConstants.DEFAULTSERVICELOGFILEPATH;
            return filepath;
        }
    }
}
