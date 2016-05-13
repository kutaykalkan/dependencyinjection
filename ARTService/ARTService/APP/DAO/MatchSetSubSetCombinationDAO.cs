using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SkyStem.ART.Service.Model;
using System.Data;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.APP.DAO
{
    public class MatchSetSubSetCombinationDAO : AbstractDAO
    {
        #region "Column Names"
        public static readonly string COLUMN_MATCHSETSUBSETCOMBINATIONID = "MatchSetSubSetCombinationID";
        public static readonly string COLUMN_MATCHSETMATCHINGSOURCEDATAIMPORT1ID = "MatchSetMatchingSourceDataImport1ID";
        public static readonly string COLUMN_MATCHSETMATCHINGSOURCEDATAIMPORT2ID = "MatchSetMatchingSourceDataImport2ID";
        public static readonly string COLUMN_MATCHSETSUBSETCOMBINATIONNAME = "MatchSetSubSetCombinationName";
        public static readonly string COLUMN_ISCONFIGURATIONCOMPLETE = "IsConfigurationComplete";

        public static readonly string COLUMN_ISACTIVE = "IsActive";
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";

        public static readonly string COLUMN_SOURCE1_NAME = "Source1Name";
        public static readonly string COLUMN_SOURCE1_DATA = "Source1Data";
        public static readonly string COLUMN_SOURCE1_TYPEID = "Source1TypeID";
        public static readonly string COLUMN_SOURCE1_MATCHINGSOURCEDATAIMPORTID = "MatchingSource1DataImportID";

        public static readonly string COLUMN_SOURCE2_NAME = "Source2Name";
        public static readonly string COLUMN_SOURCE2_DATA = "Source2Data";
        public static readonly string COLUMN_SOURCE2_TYPEID = "Source2TypeID";
        public static readonly string COLUMN_SOURCE2_MATCHINGSOURCEDATAIMPORTID = "MatchingSource2DataImportID";

        #endregion 

        public MatchSetSubSetCombinationDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {
        }
        #region "Public Methods"
        public List<MatchSetSubSetCombinationInfo> GetMatchSetSubSetCombinationForMatchSetID(long matchSetHdrID)
        {
            SqlConnection oConn = null;
            SqlCommand oCmd = null;
            SqlDataReader reader = null;
            List<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationInfoList = null;
            try
            {
                oConn = this.GetConnection();
                oCmd = this.GetMetchSetSubSetCombinationCommand(matchSetHdrID);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);

                //Get All MatchSetSubSetCombinationInfo objects & all MatchingSubSetInfo object along with xml data
                oMatchSetSubSetCombinationInfoList = this.MapAllObjects(reader);
                return oMatchSetSubSetCombinationInfoList;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }
        #endregion

        #region "Private Methods"
        private List<MatchSetSubSetCombinationInfo> MapAllObjects(SqlDataReader reader)
        {
            List<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationCollection = null;
            if (reader.HasRows)
            {
                oMatchSetSubSetCombinationCollection = new List<MatchSetSubSetCombinationInfo>();
                while (reader.Read())
                {
                    MatchSetSubSetCombinationInfo oMatchSetSubSetCombination = this.MapObject(reader);
                    oMatchSetSubSetCombinationCollection.Add(oMatchSetSubSetCombination);
                }
            }
            return oMatchSetSubSetCombinationCollection;
        }

        private MatchSetSubSetCombinationInfo MapObject(SqlDataReader reader)
        {
            MatchSetSubSetCombinationInfo oEntity = new MatchSetSubSetCombinationInfo();
            oEntity.Source1 = new MatchSetMatchingSourceDataImportInfo();
            oEntity.Source2 = new MatchSetMatchingSourceDataImportInfo();
            int ordinal = -1;

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_MATCHSETSUBSETCOMBINATIONID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.MatchSetSubSetCombinationID = reader.GetInt64(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_MATCHSETMATCHINGSOURCEDATAIMPORT1ID);
                if (!reader.IsDBNull(ordinal))
                {
                    oEntity.MatchSetMatchingSourceDataImport1ID = reader.GetInt64(ordinal);
                    oEntity.Source1.MatchSetMatchingSourceDataImportID = oEntity.MatchSetMatchingSourceDataImport1ID;
                }
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_MATCHSETMATCHINGSOURCEDATAIMPORT2ID);
                if (!reader.IsDBNull(ordinal))
                {
                    oEntity.MatchSetMatchingSourceDataImport2ID = reader.GetInt64(ordinal);
                    oEntity.Source2.MatchSetMatchingSourceDataImportID = oEntity.MatchSetMatchingSourceDataImport2ID;
                }
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_MATCHSETSUBSETCOMBINATIONNAME);
                if (!reader.IsDBNull(ordinal))
                    oEntity.MatchSetSubSetCombinationName = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_ISCONFIGURATIONCOMPLETE);
                if (!reader.IsDBNull(ordinal))
                    oEntity.IsConfigurationComplete = reader.GetBoolean(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }



            try
            {
                ordinal = reader.GetOrdinal(COLUMN_ISACTIVE);
                if (!reader.IsDBNull(ordinal))
                    oEntity.IsActive = reader.GetBoolean(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_ADDEDBY);
                if (!reader.IsDBNull(ordinal))
                    oEntity.AddedBy = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_DATEADDED);
                if (!reader.IsDBNull(ordinal))
                    oEntity.DateAdded = reader.GetDateTime(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_REVISEDBY);
                if (!reader.IsDBNull(ordinal))
                    oEntity.RevisedBy = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_DATEREVISED);
                if (!reader.IsDBNull(ordinal))
                    oEntity.DateRevised = reader.GetDateTime(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //Source1 properties
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_SOURCE1_MATCHINGSOURCEDATAIMPORTID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.Source1.MatchingSourceDataImportID = reader.GetInt64(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_SOURCE1_DATA);
                if (!reader.IsDBNull(ordinal))
                    oEntity.Source1.SubSetData = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_SOURCE1_NAME);
                if (!reader.IsDBNull(ordinal))
                    oEntity.Source1.SubSetName = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_SOURCE1_TYPEID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.Source1.MatchSetMatchingSourceTypeID = reader.GetInt16(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //Source2 properties
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_SOURCE2_MATCHINGSOURCEDATAIMPORTID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.Source2.MatchingSourceDataImportID = reader.GetInt64(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_SOURCE2_DATA);
                if (!reader.IsDBNull(ordinal))
                    oEntity.Source2.SubSetData = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_SOURCE2_NAME);
                if (!reader.IsDBNull(ordinal))
                    oEntity.Source2.SubSetName = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_SOURCE2_TYPEID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.Source2.MatchSetMatchingSourceTypeID = reader.GetInt16(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            return oEntity;
        }

        private SqlCommand GetMetchSetSubSetCombinationCommand(long matchSetHdrID)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "[Matching].[usp_SVC_GET_MatchSetSubSetCombinationByMatchSetID]";

            SqlParameterCollection oParams = oCommand.Parameters;

            SqlParameter oParamMatchSetID = new SqlParameter();
            oParamMatchSetID.ParameterName = "@MatchSetID";
            oParamMatchSetID.Value = matchSetHdrID;

            oParams.Add(oParamMatchSetID);

            return oCommand;
        }
        #endregion
    }
}
