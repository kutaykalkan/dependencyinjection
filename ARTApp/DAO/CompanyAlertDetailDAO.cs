 


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO
{   
    public class CompanyAlertDetailDAO : CompanyAlertDetailDAOBase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyAlertDetailDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        private IDbCommand CreateCompanyAlertDetailInfoObjectCommand(int? UserID, int? RoleID, int? RecID, int? AlertTpye)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("[usp_SEL_CompanyAlertDetailByRoleId]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = UserID;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            parRoleID.Value = RoleID;
            cmdParams.Add(parRoleID);


            System.Data.IDbDataParameter parAlertTpye = cmd.CreateParameter();
            parAlertTpye.ParameterName = "@AlertTpye";
            parAlertTpye.Value = AlertTpye;
            cmdParams.Add(parAlertTpye);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if (RecID.HasValue)
                parRecPeriodID.Value = RecID.Value;
            else
                parRecPeriodID.Value = DBNull.Value;
            
            cmdParams.Add(parRecPeriodID);


            
            return cmd;
        }


        public List<CompanyAlertDetailInfo> GetCompanyAlertDetailByRoleId(int? UserID, int? RoleID, int? RecID, int? AlertTpye)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            CompanyAlertDetailInfo oCompanyAlertDetailInfo = null;
            List<CompanyAlertDetailInfo> ooCompanyAlertDetailInfoCollection = new List<CompanyAlertDetailInfo>();
            try
            {
                cmd = CreateCompanyAlertDetailInfoObjectCommand(UserID, RoleID, RecID, AlertTpye);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    oCompanyAlertDetailInfo = (CompanyAlertDetailInfo)MapObjectCompanyAlertDetailInfo(dr);
                    ooCompanyAlertDetailInfoCollection.Add(oCompanyAlertDetailInfo);
                }
              
                dr.ClearColumnHash();
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }

                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }
            return ooCompanyAlertDetailInfoCollection;

        }


        private CompanyAlertDetailInfo MapObjectCompanyAlertDetailInfo(System.Data.IDataReader r)
        {

            CompanyAlertDetailInfo entity = new CompanyAlertDetailInfo();

          
            try
            {
                int ordinal = r.GetOrdinal("CompanyAlertDetailID");
                if (!r.IsDBNull(ordinal)) entity.CompanyAlertDetailID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("CompanyAlertID");
                if (!r.IsDBNull(ordinal)) entity.CompanyAlertID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("AlertDescription");
                if (!r.IsDBNull(ordinal)) entity.AlertDescription = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsActive");
                if (!r.IsDBNull(ordinal)) entity.IsActive = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

       
            try
            {
                int ordinal = r.GetOrdinal("Url");
                if (!r.IsDBNull(ordinal)) entity.Url = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("IsRead");
                if (!r.IsDBNull(ordinal)) entity.IsRead = ((System.Boolean)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            
            entity.CompanyAlertDetailUserID = r.GetInt64Value ("CompanyAlertDetailUserID");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.NumberValue = r.GetInt32Value("NumberValue");
            entity.DateValue = r.GetDateValue("DateValue");

	

            return entity;
        }



        public bool UpdateIsRead(List<long> CompanyAlertDetailUserIDCollection)
        {
            bool result = false;
            IDbConnection con = null;
            IDbCommand cmd = null;         
            try
            {
                cmd = UpdateIsReadCommand(CompanyAlertDetailUserIDCollection);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
               int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                    result = true;
              
            }
            finally
            {
               
                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }

            return result;
        }

        private IDbCommand UpdateIsReadCommand(List<long> oCompanyAlertDetailUserIDCollection)
        {
         
            DataTable dtCompanyAlertDetailUserIDs = ServiceHelper.ConvertLongIDCollectionToDataTable(oCompanyAlertDetailUserIDCollection);
            IDbCommand cmd = null;
            cmd = this.CreateCommand("usp_UPD_CompanyAlertDetailUserIsRead");
            cmd.CommandType = CommandType.StoredProcedure;           
            IDataParameterCollection cmdParams = cmd.Parameters;
            IDbDataParameter parmCompanyAlertDetailUserIDTable = cmd.CreateParameter();
            parmCompanyAlertDetailUserIDTable.ParameterName = "@CompanyAlertDetailUserIDTable";
            parmCompanyAlertDetailUserIDTable.Value = dtCompanyAlertDetailUserIDs;
            cmdParams.Add(parmCompanyAlertDetailUserIDTable);

            return cmd;
        }


        public bool CheckIsReadMsg(int? UserID, int? RoleID, int? RecID, int? AlertTpye)
        {

            IDbConnection con = null;
            IDbCommand cmd = null;
            int numberOfUnReadMsg = -1;         
            try
            {
                cmd = CheckIsReadMsgCommand(UserID, RoleID, RecID, AlertTpye);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                numberOfUnReadMsg = Convert.ToInt32(cmd.ExecuteScalar().ToString());          
              
            }
            finally
            {
             
                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }
            if (numberOfUnReadMsg > 0)
                return true;
            else
                return false;
        
        }


        private IDbCommand CheckIsReadMsgCommand(int? UserID, int? RoleID, int? RecID, int? AlertTpye)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("[usp_SEL_CompanyAlertNoOfUnReadMsg]");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = UserID.Value ;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RoleID";
            parRoleID.Value = RoleID.Value ;
            cmdParams.Add(parRoleID);


            System.Data.IDbDataParameter parAlertTpye = cmd.CreateParameter();
            parAlertTpye.ParameterName = "@AlertTpye";
            parAlertTpye.Value = AlertTpye.Value ;
            cmdParams.Add(parAlertTpye);

            System.Data.IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            if(RecID.HasValue )
               parRecPeriodID.Value = RecID.Value ;
            else
                parRecPeriodID.Value = DBNull.Value  ;
            cmdParams.Add(parRecPeriodID);



            return cmd;
        }
        public List<CompanyAlertDetailInfo> GetCompanyAlertDetail(long? CompanyAlertDetailID)
        {
            List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoList = new List<CompanyAlertDetailInfo>();
            IDbConnection con = this.CreateConnection();
            try
            {
                con.Open();
                IDbCommand oCommand = CreateGetAlertMailDataForCompanyAlertIDCommand(CompanyAlertDetailID);
                oCommand.Connection = con;
                IDataReader reader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oCompanyAlertDetailInfoList.Add(MapObjectCompanyAlertDetailInfo(reader));
                }
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }

            return oCompanyAlertDetailInfoList;
        }
        private IDbCommand CreateGetAlertMailDataForCompanyAlertIDCommand(long? CompanyAlertDetailID)
        {
            IDbCommand cmd = this.CreateCommand("[usp_SEL_CompanyAlertDetailData]");
            cmd.CommandType = CommandType.StoredProcedure;
            AddParamsToCommandForAlertProcessing(CompanyAlertDetailID, cmd);
            return cmd;
        }
        private void AddParamsToCommandForAlertProcessing(long? CompanyAlertDetailID, IDbCommand oCommand)
        {
            IDataParameterCollection cmdParams = oCommand.Parameters;
            IDbDataParameter paramCompanyAlertDetailID = oCommand.CreateParameter();
            paramCompanyAlertDetailID.ParameterName = "@CompanyAlertDetailID";
            paramCompanyAlertDetailID.Value = CompanyAlertDetailID.GetValueOrDefault();
            cmdParams.Add(paramCompanyAlertDetailID);           
        }
       
        #region InsertCompanyAlertDetail

        public void InsertCompanyAlertDetail(DataTable dtCompanyAlertDetail)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;
            try
            {
                cmd = this.CreateCommand("usp_INS_CompanyAlertDetail");
                cmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramCompanyAlertDetailTable = cmd.CreateParameter();
                paramCompanyAlertDetailTable.ParameterName = "@CompanyAlertDetailTable";
                paramCompanyAlertDetailTable.Value = dtCompanyAlertDetail;
                cmdParams.Add(paramCompanyAlertDetailTable);

                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }
        }

        #endregion

    }
}