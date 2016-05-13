


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class UserGeographyObjectDAO : UserGeographyObjectDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserGeographyObjectDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public object SaveUserOwnershipDataTable(DataTable dtUserGegographyObjectDataTable, IDbConnection connection, IDbTransaction transaction)
        {
            try
            {
                IDbCommand IDBCmmd = this.CreateCommand("usp_INS_UserGeographyObject");
                IDBCmmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdParams = IDBCmmd.Parameters;
                IDbDataParameter cmdUserGeographyObjectTable = IDBCmmd.CreateParameter();

                cmdUserGeographyObjectTable.ParameterName = "@UserGeographyObjectTable";
                cmdUserGeographyObjectTable.Value = dtUserGegographyObjectDataTable;

                cmdParams.Add(cmdUserGeographyObjectTable);

                IDBCmmd.Connection = connection;
                IDBCmmd.Transaction = transaction;
                return IDBCmmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        #region CheckValid AccountAssociation

        public bool IsValidAccountsAssociated(DataTable dtUserGeographyObjectDataTable, DataTable dtAccountID, int companyID, int recPeriodID, short roleID, int userID)
        {
            bool bValidAccountsAssociated = false;
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateIsValidAccountsAssociatedCommand(dtUserGeographyObjectDataTable, dtAccountID, companyID, recPeriodID, roleID, userID);
                cmd.Connection = con;
                con.Open();


                Object oReturnObject = cmd.ExecuteScalar();
                if (oReturnObject == null)
                {
                    bValidAccountsAssociated = false;
                }
                else
                {
                    bValidAccountsAssociated = (bool)oReturnObject;
                }

            }

             //  To be deleted later , catch block  
            catch (Exception)
            {

            }


            finally
            {

                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }
            return bValidAccountsAssociated;
        }

        private IDbCommand CreateIsValidAccountsAssociatedCommand(DataTable dtUserGeographyObjectDataTable, DataTable dtAccountID, int companyID, int recPeriodID, short roleID, int userID)
        {
            IDbCommand cmd = this.CreateCommand("usp_CHK_IsValidAccountAssociated");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;


            IDbDataParameter paramUserGeographyObjectCollection = cmd.CreateParameter();
            paramUserGeographyObjectCollection.ParameterName = "@GeographyObjectList";
            paramUserGeographyObjectCollection.Value = dtUserGeographyObjectDataTable;
            cmdParams.Add(paramUserGeographyObjectCollection);



            IDbDataParameter paramAccountIDCollection = cmd.CreateParameter();
            paramAccountIDCollection.ParameterName = "@AccountIDList";
            paramAccountIDCollection.Value = dtAccountID;
            cmdParams.Add(paramAccountIDCollection);

            IDbDataParameter paramCompanyID = cmd.CreateParameter();
            paramCompanyID.ParameterName = "@CompanyID";
            paramCompanyID.Value = companyID;
            cmdParams.Add(paramCompanyID);


            IDbDataParameter paramRecPeriodID = cmd.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = recPeriodID;
            cmdParams.Add(paramRecPeriodID);

            IDbDataParameter paramRoleID = cmd.CreateParameter();
            paramRoleID.ParameterName = "@RoleID";
            paramRoleID.Value = roleID;
            cmdParams.Add(paramRoleID);

            IDbDataParameter paramUserID = cmd.CreateParameter();
            paramUserID.ParameterName = "@UserID";
            paramUserID.Value = userID;
            cmdParams.Add(paramUserID);





            return cmd;
        }



        public object DeleteUserGeographyObject(DataTable dtUserGegographyObjectDataTable, IDbConnection connection, IDbTransaction transaction)
        {
            try
            {
                IDbCommand IDBCmmd = this.CreateCommand("usp_DEL_UserGeographyObjectAssociation");
                IDBCmmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdParams = IDBCmmd.Parameters;
                IDbDataParameter cmdUserGeographyObjectTable = IDBCmmd.CreateParameter();

                cmdUserGeographyObjectTable.ParameterName = "@UserGeographyObjectTable";
                cmdUserGeographyObjectTable.Value = dtUserGegographyObjectDataTable;

                cmdParams.Add(cmdUserGeographyObjectTable);

                IDBCmmd.Connection = connection;
                IDBCmmd.Transaction = transaction;
                return IDBCmmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }






        #endregion





    }
}