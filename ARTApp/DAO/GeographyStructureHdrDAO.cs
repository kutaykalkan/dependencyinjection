using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class GeographyStructureHdrDAO : GeographyStructureHdrDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GeographyStructureHdrDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public int InsertHolidayCalendarDataTable(DataTable dtRecPeriod
            , IDbConnection oConnection, IDbTransaction oTransaction, int companyID, bool isActive
            , DateTime dateAdded, string AddedBy, short companyGeographyClassID)
        {
            IDbCommand oDBCommand = null;
            oDBCommand = this.InsertGeoStructIDBCommand(dtRecPeriod, companyID, isActive, dateAdded
                , AddedBy, companyGeographyClassID);
            oDBCommand.Connection = oConnection;
            oDBCommand.Transaction = oTransaction;
            //IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            int rowsAffected = Convert.ToInt32(oDBCommand.ExecuteScalar());
            return rowsAffected;
        }

        public bool IsGeographyStructurePresentByCompanyID(int companyID)
        {
            IDbConnection oConnection = null;
            IDbCommand oCommand = null;
            try
            {
                oConnection = this.CreateConnection();
                oCommand = this.IsGeoStructPresentCommand(companyID);
                oConnection.Open();
                oCommand.Connection = oConnection;
                int rowCount = Convert.ToInt32(oCommand.ExecuteScalar());
                return rowCount > 0;
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }

        }
        #region "Create Command"
        protected IDbCommand InsertGeoStructIDBCommand(DataTable dtGeoStruct, int companyID
            , bool isActive, DateTime dateAdded, string AddedBy, short companyGeographyClassID)
        {
            IDbCommand oCommand = this.CreateCommand("usp_INS_GeographyStructure");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter cmdParamGeoStructTable = oCommand.CreateParameter();
            cmdParamGeoStructTable.ParameterName = "@GeographyStructureHdrTable";
            cmdParamGeoStructTable.Value = dtGeoStruct;
            cmdParams.Add(cmdParamGeoStructTable);

            IDbDataParameter cmdParamCompanyID = oCommand.CreateParameter();
            cmdParamCompanyID.ParameterName = "@companyID";
            cmdParamCompanyID.Value = companyID;
            cmdParams.Add(cmdParamCompanyID);

            IDbDataParameter cmdParamIsActive = oCommand.CreateParameter();
            cmdParamIsActive.ParameterName = "@isActive";
            cmdParamIsActive.Value = isActive;
            cmdParams.Add(cmdParamIsActive);

            IDbDataParameter cmdParamDateAdded = oCommand.CreateParameter();
            cmdParamDateAdded.ParameterName = "@dateAdded";
            cmdParamDateAdded.Value = dateAdded;
            cmdParams.Add(cmdParamDateAdded);

            IDbDataParameter cmdParamAddedBy = oCommand.CreateParameter();
            cmdParamAddedBy.ParameterName = "@addedBy";
            cmdParamAddedBy.Value = AddedBy;
            cmdParams.Add(cmdParamAddedBy);

            IDbDataParameter cmdParamCompanyGeographyClassID = oCommand.CreateParameter();
            cmdParamCompanyGeographyClassID.ParameterName = "@companyGeographyClassID";
            cmdParamCompanyGeographyClassID.Value = companyGeographyClassID;
            cmdParams.Add(cmdParamCompanyGeographyClassID);

            return oCommand;
        }

        protected IDbCommand IsGeoStructPresentCommand(int companyID)
        {
            IDbCommand oCommand = this.CreateCommand("usp_GET_IsGeographyStructurePresentByCompanyID");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;
            IDbDataParameter cmdParamCompanyID = oCommand.CreateParameter();
            cmdParamCompanyID.ParameterName = "@companyID";
            cmdParamCompanyID.Value = companyID;
            cmdParams.Add(cmdParamCompanyID);

            return oCommand;
        }
        #endregion

        #region "GetOrganizationalHierarchy"
        internal List<GeographyStructureHdrInfo> GetOrganizationalHierarchy(int? CompanyID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<GeographyStructureHdrInfo> oGeographyStructureHdrInfoCollection = null;

            try
            {
                cmd = CreateGetOrganizationalHierarchyCommand(CompanyID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oGeographyStructureHdrInfoCollection = MapObjects(dr);
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
            return oGeographyStructureHdrInfoCollection;
        }

        private IDbCommand CreateGetOrganizationalHierarchyCommand(int? CompanyID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_GeographyStructureHdrByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = CompanyID.Value;
            cmdParams.Add(parCompanyID);
            return cmd;
        }
        #endregion
    }
}