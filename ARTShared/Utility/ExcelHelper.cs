using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using System.Data;

namespace SkyStem.ART.Shared.Utility
{
    public class ExcelHelper
    {

        public static bool ExportDataToExcel(DataTable dtExport, string filepath, string sheetName)
        {
            string connString = SharedAppSettingHelper.GetConnectionStringForExcelFile(filepath, true);
            using (OleDbConnection con = new OleDbConnection(connString))
            {
                con.Open();

                #region Delete Table if exists else catch exception

                try
                {
                    StringBuilder strSQLDrop = new StringBuilder();
                    strSQLDrop.Append("DROP TABLE ").Append("[" + sheetName + "]");
                    OleDbCommand cmdDelete = new OleDbCommand(strSQLDrop.ToString(), con);
                    cmdDelete.ExecuteNonQuery();
                }
                catch (Exception) { }

                #endregion

                #region Create Table

                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("CREATE TABLE ").Append("[" + sheetName + "]");
                strSQL.Append("(");
                for (int i = 0; i < dtExport.Columns.Count; i++)
                {
                    strSQL.Append("[" + dtExport.Columns[i].ColumnName + "] text,");
                }
                strSQL = strSQL.Remove(strSQL.Length - 1, 1);
                strSQL.Append(")");

                OleDbCommand cmd = new OleDbCommand(strSQL.ToString(), con);
                cmd.ExecuteNonQuery();

                string value = string.Empty;

                for (int i = 0; i < dtExport.Rows.Count; i++)
                {
                    strSQL.Remove(0, strSQL.Length);
                    StringBuilder strfield = new StringBuilder();
                    StringBuilder strvalue = new StringBuilder();
                    for (int j = 0; j < dtExport.Columns.Count; j++)
                    {
                        value = string.Empty;
                        if (dtExport.Rows[i][j] != null && dtExport.Rows[i][j] != DBNull.Value)
                            value = dtExport.Rows[i][j].ToString();
                        strfield.Append("[" + dtExport.Columns[j].ColumnName + "]");
                        strvalue.Append("'" + SharedHelper.ReplaceSpecialChars(value) + "'");
                        if (j != dtExport.Columns.Count - 1)
                        {
                            strfield.Append(",");
                            strvalue.Append(",");
                        }
                    }
                    cmd.CommandText = strSQL.Append(" insert into [" + sheetName + "]( ")
                        .Append(strfield.ToString())
                        .Append(") values (").Append(strvalue).Append(")").ToString();
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                con.Dispose();

                #endregion
                return true;
            }
        }
        public static void LoadXmlToDataSet(DataSet oDataSet, string xmlDataString)
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

        #region Data Import Tmeplate
        public static DataTable GetExcelFileSchemaGL(string filePath, string fileExtension, string SheetName)
        {
            OleDbConnection connExcel = GetConnectionForExcelFile(filePath, fileExtension, false);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            DataTable dts = new DataTable();
            cmdExcel.Connection = connExcel;
            short sheetIndex = -1;
            string query = string.Empty;
            OleDbDataReader oReaderExcel = null;

            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            sheetIndex = FindSheetIndex(dtExcelSchema, SheetName);
            if (sheetIndex < 0)
            {
                throw new Exception("Sheet Name not found");
            }
            if (dtExcelSchema.Rows.Count > sheetIndex)
            {
                SheetName = dtExcelSchema.Rows[sheetIndex]["TABLE_NAME"].ToString();
                dt.Columns.Add("COLUMN_NAME", typeof(System.String));
                query = "SELECT TOP 1 * FROM [" + SheetName + "]";
                OleDbCommand oCommandExcel = new OleDbCommand(query, connExcel);
                oReaderExcel = oCommandExcel.ExecuteReader();

                if (oReaderExcel.HasRows)
                {
                    oReaderExcel.Read();
                    for (int f = 0; f < oReaderExcel.FieldCount; f++)
                    {
                        if (!oReaderExcel.IsDBNull(f))
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = oReaderExcel.GetString(f).Replace("\n", " ").Trim();
                            dt.Rows.Add(dr);
                        }
                    }
                    dts = CreateDataTable(dt);
                }
            }
            return dts;
        }

        public static OleDbConnection GetConnectionForExcelFile(string filePath, string fileExtension, bool readHeader)
        {
            OleDbConnection oConnectionExcel = null;
            string conStr = "";
            if (readHeader)
                conStr = SharedAppSettingHelper.GetConnectionStringForExcelFile(filePath, true);
            else
                conStr = SharedAppSettingHelper.GetConnectionStringForExcelFile(filePath, false);
            oConnectionExcel = new OleDbConnection(conStr);
            return oConnectionExcel;
        }

        private static DataTable CreateDataTable(DataTable dt)
        {
            DataTable dts = new DataTable();

            dts.Columns.Add(new DataColumn("ImportTemplateFieldID", typeof(System.Int32)));
            dts.Columns.Add(new DataColumn("ImportTemplateID", typeof(System.Int32)));
            dts.Columns.Add(new DataColumn("FieldName", typeof(System.String)));
            dts.Columns.Add(new DataColumn("DataTypeID", typeof(System.Int16)));
            dts.Columns.Add(new DataColumn("IsActive", typeof(System.Boolean)));

            DataRow dr = null;
            foreach (DataRow dataRow in dt.Rows)
            {
                foreach (object item in dataRow.ItemArray)
                {
                    dr = dts.NewRow();
                    dr[0] = DBNull.Value;
                    dr[1] = DBNull.Value;
                    dr[2] = item;
                    dr[3] = DBNull.Value;
                    dr[4] = DBNull.Value;
                    dts.Rows.Add(dr);
                }
            }
            return dts;
        }

        public static short FindSheetIndex(DataTable dtSchema, string sheetName)
        {
            short sheetIndex = -1;
            string columnName = "";
            for (short i = 0; i < dtSchema.Rows.Count; i++)
            {
                columnName = dtSchema.Rows[i]["TABLE_NAME"].ToString().Replace("'", "");
                if (columnName.ToUpper() == sheetName.ToUpper() + "$")
                {
                    sheetIndex = i;
                    break;
                }
            }
            return sheetIndex;
        }

        #endregion
        #region  DelimitedFile
        public static DataTable GetDataTableFromImportDelimitedFile(string FileFullName, bool bReadHeaders)
        {
            DataTable tbl = new DataTable("DelimitedFileDataTable");
            FileInfo file = new FileInfo(FileFullName);
            List<string> ColumnNameList = GetDelimitedFileColumnNameList(FileFullName);
            System.Object lockThis = new System.Object();
            lock (lockThis)
            {

                if (ColumnNameList != null && ColumnNameList.Count > 0)
                    writeSchema(file, ColumnNameList);
                using (OleDbConnection con =
                      new OleDbConnection(SharedAppSettingHelper.GetConnectionStringForDelimitedFile(file.FullName, bReadHeaders)))
                {
                    using (OleDbCommand cmd = new OleDbCommand(string.Format("SELECT * FROM [{0}]", file.Name), con))
                    {
                        con.Open();
                        // Using a DataTable to process the data
                        using (OleDbDataAdapter objAdp = new OleDbDataAdapter(cmd))
                        {
                            objAdp.Fill(tbl);
                        }
                    }
                }
            }
            string[] UnWantedColumns = null;
            if (tbl != null)
            {
                UnWantedColumns = (from dc in tbl.Columns.Cast<DataColumn>()
                                   where dc.ColumnName.Contains(".NoName")
                                   select dc.ColumnName).ToArray();
            }
            if (UnWantedColumns != null && UnWantedColumns.Length > 0)
                RemoveUnWantedColumns(tbl, UnWantedColumns);
            return tbl;
        }
        private static string GetColumnNameString(string colName)
        {
            return @"""" + colName + @"""";
        }
        private static void writeSchema(FileInfo File, List<string> ColumnNameList)
        {
            FileStream fsOutput = null;
            StreamWriter srOutput = null;
            try
            {
                fsOutput = new FileStream(File.DirectoryName + "\\schema.ini", FileMode.Create, FileAccess.Write);
                srOutput = new StreamWriter(fsOutput);
                string s1;
                s1 = "[" + File.Name + "]";
                srOutput.WriteLine(s1.ToString());
                for (int i = 0; i < ColumnNameList.Count; i++)
                {
                    int x = i + 1;
                    srOutput.WriteLine("\r\n Col" + x.ToString() + " = " + GetColumnNameString(ColumnNameList[i]) + " Text ");
                }
                srOutput.Close();
                fsOutput.Close();

            }
            finally
            {
                fsOutput.Dispose();
                srOutput.Dispose();
            }
        }

        public static List<string> GetDelimitedFileColumnNameList(string filePath)
        {
            string[] columnNames = GetDelimitedFileColumnNames(filePath);
            return columnNames.ToList<string>();
        }
        public static string[] GetDelimitedFileColumnNames(string filePath)
        {
            DataTable tbl = new DataTable("DelimitedFileDataTable");
            FileInfo file = new FileInfo(filePath);
            using (OleDbConnection con =
                  new OleDbConnection(SharedAppSettingHelper.GetConnectionStringForDelimitedFile(file.FullName, true)))
            {
                using (OleDbCommand cmd = new OleDbCommand(string.Format("SELECT * FROM [{0}]", file.Name), con))
                {
                    con.Open();
                    using (OleDbDataAdapter objAdp = new OleDbDataAdapter(cmd))
                    {
                        objAdp.Fill(tbl);
                    }
                }
            }
            string[] UnWantedColumns = null;
            if (tbl != null)
            {
                UnWantedColumns = (from dc in tbl.Columns.Cast<DataColumn>()
                                   where dc.ColumnName.Contains(".NoName")
                                   select dc.ColumnName).ToArray();
            }
            if (UnWantedColumns != null && UnWantedColumns.Length > 0)
                RemoveUnWantedColumns(tbl, UnWantedColumns);

            string[] columnNames = null;
            if (tbl != null)
            {
                columnNames = (from dc in tbl.Columns.Cast<DataColumn>()
                               select dc.ColumnName).ToArray();
            }
            return columnNames;
        }
        public static DataTable GetDelimitedFileSchema(string filePath)
        {
            DataTable dts = new DataTable();
            dts.Columns.Add("COLUMN_NAME", typeof(System.String));
            string[] columnNames = GetDelimitedFileColumnNames(filePath);
            if (columnNames != null)
            {
                for (int f = 0; f < columnNames.Length; f++)
                {
                    if (!string.IsNullOrEmpty(columnNames[f]))
                    {
                        DataRow dr = dts.NewRow();
                        dr[0] = columnNames[f].Replace("\n", " ").Trim();
                        dts.Rows.Add(dr);
                    }
                }
            }
            return dts;
        }
        public static DataTable GetDelimitedFileColumnsDataTable(string filePath)
        {
            DataTable dts = CreateDataTable(GetDelimitedFileSchema(filePath));
            return dts;
        }
        public static void RemoveUnWantedColumns(DataTable dt, string[] UnWantedColumns)
        {
            foreach (string colName in UnWantedColumns)
            {
                dt.Columns.Remove(colName);
            }
        }

        #endregion

    }
}
