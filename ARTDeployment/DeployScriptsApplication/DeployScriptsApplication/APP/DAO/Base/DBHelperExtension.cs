using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

namespace SkyStem.ART.App.DAO.Base 
{
    public static class DBHelperExtension
    {

        private static Hashtable _DataReaderColumns = new Hashtable();

        public static bool HasColumn(this IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        public static int? GetColumnIndex(this IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return i;
            }
            return null;
        }

        public static int? GetColumnIndex(this IDataRecord dr, string columnName, Hashtable ht)
        {
            int? ordinal = null;
            if (ht != null)
            {
                if (ht.Contains(columnName))
                    ordinal = (int?)ht[columnName];
                else
                {
                    ordinal = GetColumnIndex(dr, columnName);
                    ht.Add(columnName, ordinal);
                }
            }
            else
                ordinal = GetColumnIndex(dr, columnName);
            return ordinal;
        }

        public static int? GetDataReaderOrdinal(this IDataRecord dr, string columnName)
        {
            Hashtable ht = null;
            if(_DataReaderColumns.Contains(dr))
                ht = (Hashtable)_DataReaderColumns[dr];
            else
            {    ht = new Hashtable();
                _DataReaderColumns.Add(dr, ht);
            }
            int? ordinal = GetColumnIndex(dr, columnName, ht);
            return ordinal;
        }

        public static void ClearColumnHash(this IDataRecord dr)
        {
            if (_DataReaderColumns.Contains(dr))
            {
                Hashtable ht = (Hashtable)_DataReaderColumns[dr];
                ht.Clear();
                _DataReaderColumns.Remove(dr);
            }
        }

        #region Helper Functions

        public static Int16? GetInt16Value(this IDataRecord dr, string columnName)
        {
            Int16? value = null;
            try
            {
                int? ordinal = dr.GetDataReaderOrdinal(columnName);
                if (ordinal != null)
                    if (!dr.IsDBNull(ordinal.Value)) value = (Convert.ToInt16(dr.GetValue(ordinal.Value)));
            }
            catch (Exception) { }
            return value;
        }

        public static Int32? GetInt32Value(this IDataRecord dr, string columnName)
        {
            Int32? value = null;
            try
            {
                int? ordinal = dr.GetDataReaderOrdinal(columnName);
                if (ordinal != null)
                    if (!dr.IsDBNull(ordinal.Value)) value = ((System.Int32)(dr.GetValue(ordinal.Value)));
            }
            catch (Exception) { }
            return value;
        }

        public static Int64? GetInt64Value(this IDataRecord dr, string columnName)
        {
            Int64? value = null;
            try
            {
                int? ordinal = dr.GetDataReaderOrdinal(columnName);
                if (ordinal != null)
                    if (!dr.IsDBNull(ordinal.Value)) value = ((System.Int64)(dr.GetValue(ordinal.Value)));
            }
            catch (Exception) { }
            return value;
        }

        public static decimal? GetDecimalValue(this IDataRecord dr, string columnName)
        {
            decimal? value = null;
            try
            {
                int? ordinal = dr.GetDataReaderOrdinal(columnName);
                if (ordinal != null)
                    if (!dr.IsDBNull(ordinal.Value)) value = ((System.Decimal)(dr.GetValue(ordinal.Value)));
            }
            catch (Exception) { }
            return value;
        }

        public static string GetStringValue(this IDataRecord dr, string columnName)
        {
            string value = string.Empty;
            try
            {
                int? ordinal = dr.GetDataReaderOrdinal(columnName);
                if (ordinal != null)
                    if (!dr.IsDBNull(ordinal.Value)) value = ((System.String)(dr.GetValue(ordinal.Value)));
            }
            catch (Exception) { }
            return value;
        }

        public static DateTime? GetDateValue(this IDataRecord dr, string columnName)
        {
            DateTime? value = null;
            try
            {
                int? ordinal = dr.GetDataReaderOrdinal(columnName);
                if (ordinal != null)
                    if (!dr.IsDBNull(ordinal.Value)) value = ((System.DateTime)(dr.GetValue(ordinal.Value)));
            }
            catch (Exception) { }
            return value;
        }

        public static bool? GetBooleanValue(this IDataRecord dr, string columnName)
        {
            bool? value = null;
            try
            {
                int? ordinal = dr.GetDataReaderOrdinal(columnName);
                if (ordinal != null)
                    if (!dr.IsDBNull(ordinal.Value)) value = ((System.Boolean)(dr.GetValue(ordinal.Value)));
            }
            catch (Exception) { }
            return value;
        }

        public static char GetCharValue(this IDataRecord dr, string columnName)
        {
            char value = ' ';
            try
            {
                int? ordinal = dr.GetDataReaderOrdinal(columnName);
                if (ordinal != null)
                    if (!dr.IsDBNull(ordinal.Value)) value = (Convert.ToChar(dr.GetValue(ordinal.Value)));
            }
            catch (Exception) { }
            return value;
        }

        public static byte? GetByteValue(this IDataRecord dr, string columnName)
        {
            byte? value = null;
            try
            {
                int? ordinal = dr.GetDataReaderOrdinal(columnName);
                if (ordinal != null)
                    if (!dr.IsDBNull(ordinal.Value)) value = ((byte)(dr.GetValue(ordinal.Value)));
            }
            catch (Exception) { }
            return value;
        }

        #endregion
    }
}
