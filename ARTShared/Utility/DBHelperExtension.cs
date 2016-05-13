using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SkyStem.ART.Shared.Utility
{
    public static class DBHelperExtension
    {

        #region Helper Functions

        public static Int16? GetInt16Value(this DataRow dr, string columnName)
        {
            Int16? value = null;
            try
            {
                if (dr.Table.Columns.Contains(columnName))
                    if (dr[columnName] != DBNull.Value) value = (Convert.ToInt16(dr[columnName]));
            }
            catch (Exception) { }
            return value;
        }

        public static Int32? GetInt32Value(this DataRow dr, string columnName)
        {
            Int32? value = null;
            try
            {
                if (dr.Table.Columns.Contains(columnName))
                    if (dr[columnName] != DBNull.Value) value = (Convert.ToInt32(dr[columnName]));
            }
            catch (Exception) { }
            return value;
        }

        public static Int64? GetInt64Value(this DataRow dr, string columnName)
        {
            Int64? value = null;
            try
            {
                if (dr.Table.Columns.Contains(columnName))
                    if (dr[columnName] != DBNull.Value) value = (Convert.ToInt64(dr[columnName]));
            }
            catch (Exception) { }
            return value;
        }

        public static decimal? GetDecimalValue(this DataRow dr, string columnName)
        {
            decimal? value = null;
            try
            {
                if (dr.Table.Columns.Contains(columnName))
                    if (dr[columnName] != DBNull.Value) value = (Convert.ToDecimal(dr[columnName]));
            }
            catch (Exception) { }
            return value;
        }

        public static string GetStringValue(this DataRow dr, string columnName)
        {
            string value = string.Empty;
            try
            {
                if (dr.Table.Columns.Contains(columnName))
                    if (dr[columnName] != DBNull.Value) value = (Convert.ToString(dr[columnName]));
            }
            catch (Exception) { }
            return value;
        }

        public static DateTime? GetDateValue(this DataRow dr, string columnName)
        {
            DateTime? value = null;
            try
            {
                if (dr.Table.Columns.Contains(columnName))
                    if (dr[columnName] != DBNull.Value) value = (Convert.ToDateTime(dr[columnName]));
            }
            catch (Exception) { }
            return value;
        }

        public static bool? GetBooleanValue(this DataRow dr, string columnName)
        {
            bool? value = null;
            try
            {
                if (dr.Table.Columns.Contains(columnName))
                    if (dr[columnName] != DBNull.Value) value = (Convert.ToBoolean(dr[columnName]));
            }
            catch (Exception) { }
            return value;
        }

        public static char GetCharValue(this DataRow dr, string columnName)
        {
            char value = ' ';
            try
            {
                if (dr.Table.Columns.Contains(columnName))
                    if (dr[columnName] != DBNull.Value) value = (Convert.ToChar(dr[columnName]));
            }
            catch (Exception) { }
            return value;
        }

        public static byte? GetByteValue(this DataRow dr, string columnName)
        {
            byte? value = null;
            try
            {
                if (dr.Table.Columns.Contains(columnName))
                    if (dr[columnName] != DBNull.Value) value = (Convert.ToByte(dr[columnName]));
            }
            catch (Exception) { }
            return value;
        }

        #endregion
    }
}
