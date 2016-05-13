using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SkyStem.ART.Service.Model;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Client.Model.CompanyDatabase;
namespace SkyStem.ART.Service.APP.DAO
{
    public class MatchingConfigurationDAO : AbstractDAO
    {
        #region "Column Name"
        public static readonly string COLUMN_MATCHINGCONFIGURATIONID = "MatchingConfigurationID";
        public static readonly string COLUMN_MATCHSETSUBSETCOMBINATIONID = "MatchSetSubSetCombinationID";
        public static readonly string COLUMN_MATCHINGSOURCE1COLUMNID = "MatchingSource1ColumnID";
        public static readonly string COLUMN_MATCHINGSOURCE2COLUMNID = "MatchingSource2ColumnID";

        public static readonly string COLUMN_MATCHINGSOURCE1COLUMN_NAME = "MatchingSource1ColumnName";
        public static readonly string COLUMN_MATCHINGSOURCE2COLUMN_NAME = "MatchingSource2ColumnName";

        public static readonly string COLUMN_ISMATCHING = "IsMatching";
        public static readonly string COLUMN_ISPARTIALMATCHING = "IsPartialMatching";
        public static readonly string COLUMN_ISDISPLAYCOLUMN = "IsDisplayColumn";
        public static readonly string COLUMN_DATATYPEID = "DataTypeID";
        public static readonly string COLUMN_DISPLAYCOLUMNNAME = "DisplayColumnName";
        public static readonly string COLUMN_MATCHINGCONFIGRULE = "MatchingConfigRule";

        public static readonly string COLUMN_ISACTIVE = "IsActive";
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        #endregion

        public MatchingConfigurationDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {
        }

        #region "Public Methods"
        public List<MatchingConfigurationInfo> GetMatchingConfigurationForMatchSetID(long matchSetHdrID)
        {
            SqlConnection oConn = null;
            SqlCommand oCmd = null;
            SqlDataReader reader = null;
            List<MatchingConfigurationInfo> oMatchingConfigInfo = null;
            try
            {
                oConn = this.GetConnection();
                oCmd = this.GetMatchingConfigurationCommand(matchSetHdrID);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                reader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);

                //Get All Matching Configuration information and Matching configuration rules
                oMatchingConfigInfo = this.MapAllObjects(reader);


                return oMatchingConfigInfo;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }
        #endregion

        #region "Private Methods"
        private List<MatchingConfigurationInfo> MapAllObjects(SqlDataReader reader)
        {
            List<MatchingConfigurationInfo> oMatchingConfigInfoCollection = null;
            if (reader.HasRows)
            {
                oMatchingConfigInfoCollection = new List<MatchingConfigurationInfo>();
                while (reader.Read())
                {
                    oMatchingConfigInfoCollection.Add(this.MapObject(reader));
                }
            }
            return oMatchingConfigInfoCollection;
        }

        private MatchingConfigurationInfo MapObject(SqlDataReader reader)
        {
            MatchingConfigurationInfo oEntity = new MatchingConfigurationInfo();
            int ordinal;


            try
            {
                ordinal = reader.GetOrdinal(COLUMN_MATCHINGCONFIGURATIONID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.MatchingConfigurationID = reader.GetInt64(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_MATCHINGSOURCE1COLUMNID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.MatchingSource1ColumnID = reader.GetInt64(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_MATCHINGSOURCE2COLUMNID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.MatchingSource2ColumnID = reader.GetInt64(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_MATCHINGSOURCE1COLUMN_NAME);
                if (!reader.IsDBNull(ordinal))
                    oEntity.MatchingSource1ColumnName = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_MATCHINGSOURCE2COLUMN_NAME);
                if (!reader.IsDBNull(ordinal))
                    oEntity.MatchingSource2ColumnName = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }
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
                ordinal = reader.GetOrdinal(COLUMN_MATCHINGCONFIGRULE);
                if (!reader.IsDBNull(ordinal))
                {
                    oEntity.MatchingConfigurationRuleCollection = this.DeSerializeRulesList(reader.GetString(ordinal), ServiceConstants.MATCHING_RULESLISTXMLTYPS);
                }
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_DATATYPEID);
                if (!reader.IsDBNull(ordinal))
                {
                    oEntity.DataTypeID = reader.GetInt16(ordinal);
                }
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_DISPLAYCOLUMNNAME);
                if (!reader.IsDBNull(ordinal))
                {
                    oEntity.DisplayColumnName = reader.GetString(ordinal);
                }
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }


            try
            {
                ordinal = reader.GetOrdinal(COLUMN_ISDISPLAYCOLUMN);
                if (!reader.IsDBNull(ordinal))
                {
                    oEntity.IsDisplayColumn = reader.GetBoolean(ordinal);
                }
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_ISMATCHING);
                if (!reader.IsDBNull(ordinal))
                {
                    oEntity.IsMatching = reader.GetBoolean(ordinal);
                }
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_ISPARTIALMATCHING);
                if (!reader.IsDBNull(ordinal))
                {
                    oEntity.IsPartialMatching = reader.GetBoolean(ordinal);
                }
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

            return oEntity;
        }

        private SqlCommand GetMatchingConfigurationCommand(long matchSetHdrID)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "[Matching].[usp_SVC_GET_MatchingConfigurationByMatchSetID]";

            SqlParameterCollection oParams = oCommand.Parameters;

            SqlParameter oParamMatchSetID = new SqlParameter();
            oParamMatchSetID.ParameterName = "@MatchSetID";
            oParamMatchSetID.Value = matchSetHdrID;

            oParams.Add(oParamMatchSetID);

            return oCommand;
        }

        private List<MatchingConfigurationRuleInfo> DeSerializeRulesList(string rulesXml, string rootNode)
        {
            XmlSerializer xmlSerial = null;
            StringReader strReader = null;
            XmlTextReader txtReader = null;
            try
            {
                if (string.IsNullOrEmpty(rootNode))
                {
                    xmlSerial = new XmlSerializer(typeof(List<MatchingConfigurationRuleInfo>));
                }
                else
                {
                    XmlAttributeOverrides over = new XmlAttributeOverrides();
                    XmlAttributes attr = new XmlAttributes();
                    attr.XmlRoot = new XmlRootAttribute(rootNode);
                    over.Add(typeof (List<MatchingConfigurationRuleInfo>),attr);
                    xmlSerial = new XmlSerializer(typeof(List<MatchingConfigurationRuleInfo>), over);

                }
                strReader = new StringReader(rulesXml);
                txtReader = new XmlTextReader(strReader);
                return (List<MatchingConfigurationRuleInfo>)xmlSerial.Deserialize(txtReader);
            }
            finally
            {
                if (txtReader != null)
                    txtReader.Close();
                if (strReader != null)
                    strReader.Close();
            }
        }
        #endregion
    }
}
