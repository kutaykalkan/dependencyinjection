


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;

namespace SkyStem.ART.App.DAO
{
    public class DataImportTypeMstDAO : DataImportTypeMstDAOBase
    {
                /// <summary>
        /// Constructor
        /// </summary>
        public DataImportTypeMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public IList<DataImportTypeMstInfo> GetAllImportType()
        {
            DataImportTypeMstInfo objDataImportTypeMstInfo = null;
            IDbConnection oConnection = null;
            IDbCommand oCommand;
            IList<DataImportTypeMstInfo> objDataImportTypeMstInfoList = new List<DataImportTypeMstInfo>();
            try
            {
                oConnection = CreateConnection();
                oConnection.Open();

                oCommand = CreateDataImportTypeCommand();
                oCommand.Connection = oConnection;
                IDataReader oReader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (oReader.Read())
                {
                    objDataImportTypeMstInfo = (DataImportTypeMstInfo)MapObjectDataImportTypeMst(oReader);
                    objDataImportTypeMstInfoList.Add(objDataImportTypeMstInfo);
                }
                oReader.Close();
            }
            finally
            {
                try
                {
                    if (oConnection != null)
                    {
                        oConnection.Dispose();
                    }
                }
                catch (Exception)
                {
                }
            }

            return objDataImportTypeMstInfoList;

        }


        #region "Create Command Methods"
        protected IDbCommand CreateDataImportTypeCommand()
        {
            IDbCommand oCommand = this.CreateCommand("usp_SEL_DataImportType");
            oCommand.CommandType = CommandType.StoredProcedure;
            return oCommand;
        }


        protected DataImportTypeMstInfo MapObjectDataImportTypeMst(System.Data.IDataReader r)
        {

            DataImportTypeMstInfo entity = new DataImportTypeMstInfo();
            entity.RoleID = r.GetInt16Value("RoleID");
            entity.DataImportTypeID  = r.GetInt16Value("DataImportTypeID");
            entity.DataImportType = r.GetStringValue("DataImportType");
            entity.DataImportTypeLabelID = r.GetInt32Value("DataImportTypeLabelID");
            entity.Description = r.GetStringValue("Description");
            entity.UploadRulesLabelID = r.GetInt32Value("UploadRulesLabelID");
            entity.IsActive = r.GetBooleanValue("IsActive");
            entity.DateAdded = r.GetDateValue("DateAdded");
            entity.AddedBy = r.GetStringValue("AddedBy");
            entity.DateRevised = r.GetDateValue("DateRevised");
            entity.RevisedBy = r.GetStringValue("RevisedBy");
            entity.HostName = r.GetStringValue("HostName");
            return entity;
        }

        #endregion
    }
}