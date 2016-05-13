using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace SkyStem.ART.Shared.Utility
{
    public class DataHelper
    {
        private DataHelper()
        {
        }

        public static string ReplaceSpecialChars(string str)
        {
            str = str.Replace("'", "''");
            return str;
        }

        /// <summary>
        /// Convert Xml to Data Set
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static void LoadXmlToDataSet(DataSet oDataSet, string xmlDataString)
        {
            try
            {
                if (oDataSet != null)
                {
                    oDataSet.Clear();
                    if (!string.IsNullOrEmpty(xmlDataString))
                    {
                        StringReader oStringReader = new StringReader(xmlDataString);
                        oDataSet.ReadXml(oStringReader);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Get Data Set with optional schema load.
        /// </summary>
        /// <param name="xmlSchemaString">The XML schema string.</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string xmlSchemaString)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(xmlSchemaString))
                {
                    StringReader oStringReader = new StringReader(xmlSchemaString);
                    oDataSet.ReadXmlSchema(oStringReader);
                }
            }
            catch (Exception)
            {
            }
            return oDataSet;
        }
    }
}
