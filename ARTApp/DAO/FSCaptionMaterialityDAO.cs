


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
    public class FSCaptionMaterialityDAO : FSCaptionMaterialityDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FSCaptionMaterialityDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        protected System.Data.IDbCommand CreateSelectAllMaterilityWithFSCaptionByReconciliationPeriodIDCommand(int? reconciliationPeriodID)
        {
            //System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_FSCaptionAndMaterialityByCompanyID");
            System.Data.IDbCommand oCommand = this.CreateCommand("usp_SEL_FSCaptionMaterialityByRecPeriodID");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            System.Data.IDbDataParameter parReconciliationPeriodID = oCommand.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@InputReconciliationPeriodID";
            parReconciliationPeriodID.Value = reconciliationPeriodID;
            cmdParams.Add(parReconciliationPeriodID);

            return oCommand;
        }

        //public IList<FSCaptionInfo_ExtendedWithMaterialityInfo> GetAllMaterilityWithFSCaptionByCompanyID(int companyID)
        public IList<FSCaptionInfo_ExtendedWithMaterialityInfo> GetAllMaterilityWithFSCaptionByReconciliationPeriodID(int? reconciliationPeriodID)
        {
            IList<FSCaptionInfo_ExtendedWithMaterialityInfo> oFSCaptionWithMaterialityInfoCollection = new List<FSCaptionInfo_ExtendedWithMaterialityInfo>();
            FSCaptionInfo_ExtendedWithMaterialityInfo oFSCaptionWithMaterialityInfo = null;
            IDbConnection oConnection = null;
            try
            {
                oConnection = CreateConnection();
                oConnection.Open();
                IDbCommand oCommand;
                //oCommand = CreateSelectAllMaterilityWithFSCaptionByCompanyIDCommand(companyID);
                oCommand = CreateSelectAllMaterilityWithFSCaptionByReconciliationPeriodIDCommand(reconciliationPeriodID);
                oCommand.Connection = oConnection;
                IDataReader reader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oFSCaptionWithMaterialityInfo = (FSCaptionInfo_ExtendedWithMaterialityInfo)MapObjectForFSCaptionWithMaterialityInfo(reader);
                    oFSCaptionWithMaterialityInfoCollection.Add(oFSCaptionWithMaterialityInfo);
                }
                reader.Close();
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }
            return oFSCaptionWithMaterialityInfoCollection;
        }

        //public int InsertFSCaptionMaterialityByTableValueCommand(int? reconciliationPeriodID,IList<FSCaptionMaterialityInfo> MaterialityList)
        //{
        //    IDbConnection oConnection = null;
        //    IDbTransaction oTransaction = null;

        //    int intResult = 0;
        //    try
        //    {
        //        DataTable dtFSCaptionWithMaterialityList = ServiceHelper.ConvertIDListToDataTable_FSCaptionWithMaterialityInfo(MaterialityList);
        //        IDbCommand oCommand = CreateInsertFSCaptionMaterialityByTableValueCommand(reconciliationPeriodID,dtFSCaptionWithMaterialityList);
        //        oConnection = CreateConnection();
        //        oConnection.Open();
        //        oCommand.Connection = oConnection;
        //        oTransaction = oConnection.BeginTransaction();
        //        oCommand.Transaction = oTransaction;
        //        intResult = oCommand.ExecuteNonQuery();
        //        oTransaction.Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        if ((oTransaction != null) && (oConnection.State == ConnectionState.Open))
        //        {
        //            oTransaction.Rollback();
        //        }
        //        throw ex;
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            if (oConnection != null)
        //                oConnection.Close();
        //        }
        //        catch (Exception)
        //        {
        //        }
        //    }
        //    return intResult;
        //}

        //public int InsertFSCaptionMaterialityByTableValueCommand(int? reconciliationPeriodID, IList<FSCaptionMaterialityInfo> oFSCaptionMaterialityInfoCollection, IDbConnection oConnection, IDbTransaction oTransaction)
        //{
        //    int intResult = 0;
        //    //try
        //    //{
        //        DataTable dtFSCaptionMateriality = ServiceHelper.ConvertFSCaptionWithMaterialityInfoCollectionToDataTable(oFSCaptionMaterialityInfoCollection);
        //        IDbCommand oCommand = CreateInsertFSCaptionMaterialityByTableValueCommand(reconciliationPeriodID,dtFSCaptionMateriality);
        //        //oConnection = CreateConnection();
        //        //oConnection.Open();
        //        oCommand.Connection = oConnection;
        //        //oTransaction = oConnection.BeginTransaction();
        //        oCommand.Transaction = oTransaction;
        //        intResult = oCommand.ExecuteNonQuery();
        //        //oTransaction.Commit();
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    if ((oTransaction != null) && (oConnection.State == ConnectionState.Open))
        //    //    {
        //    //        oTransaction.Rollback();
        //    //    }
        //    //    throw ex;
        //    //}
        //    //finally
        //    //{
        //    //    try
        //    //    {
        //    //        if (oConnection != null)
        //    //            oConnection.Close();
        //    //    }
        //    //    catch (Exception)
        //    //    {
        //    //    }
        //    //}
        //    return intResult;
        //}


        //protected System.Data.IDbCommand CreateInsertFSCaptionMaterialityByTableValueCommand(DataTable tblFSCaptionMateriality)
        //protected System.Data.IDbCommand CreateInsertFSCaptionMaterialityByTableValueCommand(int? reconciliationPeriodID, DataTable tblFSCaptionMateriality)
        //{
        //    //DataTable FSCaptionWithMaterialityList = ServiceHelper.ConvertIDListToDataTableFSCaptionWithMaterialityInfo(MaterialityList);
        //    //System.Data.IDbCommand cmd = this.CreateCommand("usp_INS_FSCaptionMaterialityTableValue");
        //    System.Data.IDbCommand oCommand = this.CreateCommand("usp_Test_INS_FSCaptionMaterialityTableValue");

        //    oCommand.CommandType = CommandType.StoredProcedure;

        //    IDataParameterCollection cmdParams = oCommand.Parameters;

        //    System.Data.IDbDataParameter FSCaptionWithMaterialityInfoTable = oCommand.CreateParameter();
        //    FSCaptionWithMaterialityInfoTable.ParameterName = "@tblFSCaptionWithMaterialityInfo";
        //    //if (!entity.IsFSCaptionIDNull)
        //    FSCaptionWithMaterialityInfoTable.Value = tblFSCaptionMateriality;
        //    //else
        //    //    parFSCaptionID.Value = System.DBNull.Value;
        //    cmdParams.Add(FSCaptionWithMaterialityInfoTable);

        //    System.Data.IDbDataParameter parReconciliationPeriodID = oCommand.CreateParameter();
        //    parReconciliationPeriodID.ParameterName = "@InputReconciliationPeriodID";
        //    parReconciliationPeriodID.Value = reconciliationPeriodID;
        //    cmdParams.Add(parReconciliationPeriodID);

        //    return oCommand;
        //}




        /// <summary>
        /// To merge FSCaptionInfo And MaterilityFSCaptionInfo, as it should have 1-to-1 mapping
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //protected System.Data.IDbCommand CreateSelectAllMaterilityWithFSCaptionByCompanyIDCommand(int companyID)
        //{
        //    System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_FSCaptionAndMaterialityByCompanyID");
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    IDataParameterCollection cmdParams = cmd.Parameters;

        //    System.Data.IDbDataParameter parLoginID = cmd.CreateParameter();
        //    parLoginID.ParameterName = "@CompanyID";
        //    parLoginID.Value = companyID;
        //    cmdParams.Add(parLoginID);

        //    return cmd;

        //}

        protected FSCaptionInfo_ExtendedWithMaterialityInfo MapObjectForFSCaptionWithMaterialityInfo(System.Data.IDataReader reader)
        {
            FSCaptionInfo_ExtendedWithMaterialityInfo entity = new FSCaptionInfo_ExtendedWithMaterialityInfo();
            //FSCaptionMaterialityInfo part
            try
            {
                int ordinal = reader.GetOrdinal("FSCaptionMaterialityID");
                if (!reader.IsDBNull(ordinal)) entity.FSCaptionMaterialityID = ((System.Int32)(reader.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = reader.GetOrdinal("MaterialityThreshold");
                if (!reader.IsDBNull(ordinal)) entity.MaterialityThreshold = ((System.Decimal)(reader.GetValue(ordinal)));
            }
            catch (Exception) { }


            // FSCaptionInfo part
            try
            {
                int ordinal = reader.GetOrdinal("FSCaptionID");
                if (!reader.IsDBNull(ordinal)) entity.FSCaptionID = ((System.Int32)(reader.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = reader.GetOrdinal("FSCaption");
                if (!reader.IsDBNull(ordinal)) entity.FSCaption = ((System.String)(reader.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = reader.GetOrdinal("FSCaptionLabelID");
                if (!reader.IsDBNull(ordinal)) entity.FSCaptionLabelID = ((System.Int32)(reader.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = reader.GetOrdinal("Description");
                if (!reader.IsDBNull(ordinal)) entity.Description = ((System.String)(reader.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = reader.GetOrdinal("CompanyID");
                if (!reader.IsDBNull(ordinal)) entity.CompanyID = ((System.Int32)(reader.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = reader.GetOrdinal("IsActive");
                if (!reader.IsDBNull(ordinal)) entity.IsActive = ((System.Boolean)(reader.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = reader.GetOrdinal("CreationPeriodEndDate");
                if (!reader.IsDBNull(ordinal)) entity.CreationPeriodEndDate = ((System.DateTime)(reader.GetValue(ordinal)));
            }
            catch (Exception) { }

            //try
            //{
            //    int ordinal = r.GetOrdinal("DateAdded");
            //    if (!r.IsDBNull(ordinal)) entity.DateAdded = ((System.DateTime)(r.GetValue(ordinal)));
            //}
            //catch (Exception) { }

            //try
            //{
            //    int ordinal = r.GetOrdinal("AddedBy");
            //    if (!r.IsDBNull(ordinal)) entity.AddedBy = ((System.String)(r.GetValue(ordinal)));
            //}
            //catch (Exception) { }

            //try
            //{
            //    int ordinal = r.GetOrdinal("DateRevised");
            //    if (!r.IsDBNull(ordinal)) entity.DateRevised = ((System.DateTime)(r.GetValue(ordinal)));
            //}
            //catch (Exception) { }

            //try
            //{
            //    int ordinal = r.GetOrdinal("RevisedBy");
            //    if (!r.IsDBNull(ordinal)) entity.RevisedBy = ((System.String)(r.GetValue(ordinal)));
            //}
            //catch (Exception) { }

            //try
            //{
            //    int ordinal = r.GetOrdinal("HostName");
            //    if (!r.IsDBNull(ordinal)) entity.HostName = ((System.String)(r.GetValue(ordinal)));
            //}
            //catch (Exception) { }

            return entity;
        }





    }//end of class
}//end of namespace