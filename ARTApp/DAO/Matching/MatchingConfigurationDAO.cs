


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Matching.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Matching
{
    public class MatchingConfigurationDAO : MatchingConfigurationDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MatchingConfigurationDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<MatchingConfigurationInfo> GetAllMatchingConfiguration(MatchingParamInfo oMatchingParamInfo)
        {
            List<MatchingConfigurationInfo> oMatchingConfigurationInfoCollection = new List<MatchingConfigurationInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_SEL_MatchingConfiguration");
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramMatchSetSubSetCombinationID = cmd.CreateParameter();
                paramMatchSetSubSetCombinationID.ParameterName = "@IDTableMatchSetSubSetCombinationID";
                paramMatchSetSubSetCombinationID.Value = ServiceHelper.ConvertLongIDCollectionToDataTable(oMatchingParamInfo.IDList);
                cmdParams.Add(paramMatchSetSubSetCombinationID);

                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                MatchingConfigurationInfo oMatchingConfigurationInfo = null;
                while (reader.Read())
                {
                    oMatchingConfigurationInfo = this.MapObject(reader);
                    oMatchingConfigurationInfoCollection.Add(oMatchingConfigurationInfo);
                }
                reader.Close();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oMatchingConfigurationInfoCollection;
        }


        public int UpdateMatchingConfigurationDisplayedColumn(MatchingParamInfo oMatchingParamInfo)
        {
            int recordsAffected = 0;
            IDbConnection con = null;
            IDbCommand cmd = null;
            DataTable child;
            DataTable dtMatchingConfiguration = ServiceHelper.ConvertMatchingConfigurationToDataTable(oMatchingParamInfo.oMatchingConfigurationInfoList, false, out child);
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_UPD_MatchingConfiguration");
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramMatchingConfiguration = cmd.CreateParameter();
                paramMatchingConfiguration.ParameterName = "@dtMatchingConfiguration";
                paramMatchingConfiguration.Value = dtMatchingConfiguration;
                cmdParams.Add(paramMatchingConfiguration);

                IDbDataParameter parDateRevised = cmd.CreateParameter();
                parDateRevised.ParameterName = "@DateRevised";
                parDateRevised.Value = oMatchingParamInfo.DateRevised.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
                cmdParams.Add(parDateRevised);

                IDbDataParameter parRevisedBy = cmd.CreateParameter();
                parRevisedBy.ParameterName = "@RevisedBy";
                parRevisedBy.Value = oMatchingParamInfo.RevisedBy;
                cmdParams.Add(parRevisedBy);

                cmd.Connection = con;
                con.Open();

                recordsAffected = cmd.ExecuteNonQuery();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return recordsAffected;
        }



        protected override MatchingConfigurationInfo MapObject(System.Data.IDataReader r)
        {
            MatchingConfigurationInfo entity = new MatchingConfigurationInfo();

            entity = base.MapObject(r);


            try
            {
                int ordinal = r.GetOrdinal("RecItemColumnID");
                if (!r.IsDBNull(ordinal)) entity.RecItemColumnID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }


            try
            {
                int ordinal = r.GetOrdinal("Source1ColumnName");
                if (!r.IsDBNull(ordinal)) entity.ColumnName1 = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }


            try
            {
                int ordinal = r.GetOrdinal("Source1ColumnDataTypeID");
                if (!r.IsDBNull(ordinal)) entity.Source1ColumnDataTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Source2ColumnName");
                if (!r.IsDBNull(ordinal)) entity.ColumnName2 = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("Source2ColumnDataTypeID");
                if (!r.IsDBNull(ordinal)) entity.Source2ColumnDataTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }



            //********Set the DataTypeID of MatchingConguration object on the basis Source1ColumnDataTypeID & Source2ColumnDataTypeID  
            if (entity.Source1ColumnDataTypeID != null)
            {
                entity.DataTypeID = entity.Source1ColumnDataTypeID;
            }
            else if (entity.Source2ColumnDataTypeID != null)
            {
                entity.DataTypeID = entity.Source2ColumnDataTypeID;
            }
            //********************************************************************************************************

            return entity;

        }


        public int SaveMatchingConfiguration(DataTable dtMatchingConfiguration, DataTable dtMatchingConfigurationRule, MatchingParamInfo oMatchingParamInfo)
        {
            int recordsAffected = 0;
            IDbCommand cmd = null;
            IDbConnection con = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_INS_MatchingConfiguration");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdMatchingConfigurationDataTable = cmd.CreateParameter();
                cmdMatchingConfigurationDataTable.ParameterName = "@dtMatchingConfiguration";
                cmdMatchingConfigurationDataTable.Value = dtMatchingConfiguration;
                cmdParams.Add(cmdMatchingConfigurationDataTable);

                IDbDataParameter cmdMatchingConfigurationRuleDataTable = cmd.CreateParameter();
                cmdMatchingConfigurationRuleDataTable.ParameterName = "@dtMatchingConfigurationRule";
                cmdMatchingConfigurationRuleDataTable.Value = dtMatchingConfigurationRule;
                cmdParams.Add(cmdMatchingConfigurationRuleDataTable);

                IDbDataParameter cmdIsActive = cmd.CreateParameter();
                cmdIsActive.ParameterName = "@IsActive";
                cmdIsActive.Value = oMatchingParamInfo.IsActive;
                cmdParams.Add(cmdIsActive);

                IDbDataParameter cmdDateAdded = cmd.CreateParameter();
                cmdDateAdded.ParameterName = "@DateAdded";
                cmdDateAdded.Value = oMatchingParamInfo.DateAdded;
                cmdParams.Add(cmdDateAdded);

                IDbDataParameter cmdAddedBy = cmd.CreateParameter();
                cmdAddedBy.ParameterName = "@AddedBy";
                cmdAddedBy.Value = oMatchingParamInfo.AddedBy;
                cmdParams.Add(cmdAddedBy);

                IDbDataParameter cmdDateRevised = cmd.CreateParameter();
                cmdDateRevised.ParameterName = "@DateRevised";
                cmdDateRevised.Value = oMatchingParamInfo.DateRevised;
                cmdParams.Add(cmdDateRevised);

                IDbDataParameter cmdRevisedBy = cmd.CreateParameter();
                cmdRevisedBy.ParameterName = "@RevisedBy";
                cmdRevisedBy.Value = oMatchingParamInfo.RevisedBy;
                cmdParams.Add(cmdRevisedBy);

                con.Open();
                recordsAffected = cmd.ExecuteNonQuery();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return recordsAffected;
        }
    }
}