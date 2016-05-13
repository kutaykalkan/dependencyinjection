using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO
{
    public class GridColumnDAO : GridColumnDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GridColumnDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        #region GetGridPrefernce
        internal List<GridColumnInfo> GetGridPrefernce(int? UserID, ARTEnums.Grid eGrid, int? RecPeriodID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<GridColumnInfo> oGridColumnInfoCollection = null;

            try
            {
                cmd = CreateGetGridPrefernceCommand(UserID, eGrid, RecPeriodID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oGridColumnInfoCollection = MapObjects(dr);
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
            return oGridColumnInfoCollection;
        }

        private IDbCommand CreateGetGridPrefernceCommand(int? UserID, ARTEnums.Grid eGrid, int? RecPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_UserGridPreference");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = UserID.Value;
            cmdParams.Add(parUserID);

            IDbDataParameter parGridID = cmd.CreateParameter();
            parGridID.ParameterName = "@GridID";
            parGridID.Value = eGrid.ToString("d");
            cmdParams.Add(parGridID);

            IDbDataParameter parRecPeriodID = cmd.CreateParameter();
            parRecPeriodID.ParameterName = "@RecPeriodID";
            parRecPeriodID.Value = RecPeriodID.Value;
            cmdParams.Add(parRecPeriodID);

            return cmd;
        }
        #endregion

        #region GetAllGridColumns
        internal List<GridColumnInfo> GetAllGridColumnsForRecPeriod(ARTEnums.Grid eGrid, int? RecPeriodID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<GridColumnInfo> oGridColumnInfoCollection = null;

            try
            {
                cmd = CreateGetAllGridColumnsForRecPeriodCommand(eGrid, RecPeriodID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oGridColumnInfoCollection = MapObjects(dr);
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
            return oGridColumnInfoCollection;
        }

        private IDbCommand CreateGetAllGridColumnsForRecPeriodCommand(ARTEnums.Grid eGrid, int? RecPeriodID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_GridColumnForRecPeriod");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parGridID = cmd.CreateParameter();
            parGridID.ParameterName = "@GridID";
            parGridID.Value = eGrid.ToString("d");
            cmdParams.Add(parGridID);

            IDbDataParameter parRecperiodID = cmd.CreateParameter();
            parRecperiodID.ParameterName = "@RecPeriodID";
            parRecperiodID.Value = RecPeriodID.Value;
            cmdParams.Add(parRecperiodID);

            return cmd;
        }
        #endregion

        internal void SaveGridPrefernce(List<int> oGridColumnIDCollection, int? UserID)
        {
            IDbCommand cmd = null;
            try
            {
                cmd = CreateSaveGridPrefernceCommand(oGridColumnIDCollection, UserID);
                this.ExecuteNonQuery(cmd);
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
        }

        private IDbCommand CreateSaveGridPrefernceCommand(List<int> oGridColumnIDCollection, int? UserID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SAV_UserGridColumn");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = UserID.Value;
            cmdParams.Add(parUserID);

            IDbDataParameter parGridID = cmd.CreateParameter();
            parGridID.ParameterName = "@tblGridColumnID";
            parGridID.Value = ServiceHelper.ConvertIDCollectionToDataTable(oGridColumnIDCollection);
            cmdParams.Add(parGridID);

            return cmd;
        }

    }
}