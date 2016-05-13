


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
    public class OperatorMstDAO : OperatorMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public OperatorMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<OperatorMstInfo> GetOperatorsByColumnID(short columnID)
        {
            IDbCommand oCommand = null;
            IDbConnection oConnection = null;
            IDataReader reader = null;
            List<OperatorMstInfo> oOperatorCollection = null;
            try
            {
                oCommand = this.CreateGetOperatorByColumnIDCommand(columnID);
                oConnection = this.CreateConnection();
                oCommand.Connection = oConnection;
                oCommand.Connection.Open();
                reader = oCommand.ExecuteReader();
                oOperatorCollection = this.MapObjects(reader);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Close();
            }
            return oOperatorCollection;
        }


        private IDbCommand CreateGetOperatorByColumnIDCommand(short columnID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_OperatorByColumnID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramColumnID = cmd.CreateParameter();
            paramColumnID.ParameterName = "@ColumnID";
            paramColumnID.Value = columnID;

            cmdParams.Add(paramColumnID);
            return cmd;
        }

        public List<OperatorMstInfo> GetOperatorsByDynamicColumnID(short columnID)
        {
            IDbCommand oCommand = null;
            IDbConnection oConnection = null;
            IDataReader reader = null;
            List<OperatorMstInfo> oOperatorCollection = null;
            try
            {
                oCommand = this.CreateGetOperatorByDynamicColumnIDCommand(columnID);
                oConnection = this.CreateConnection();
                oCommand.Connection = oConnection;
                oCommand.Connection.Open();
                reader = oCommand.ExecuteReader();
                oOperatorCollection = this.MapObjects(reader);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Close();
            }
            return oOperatorCollection;
        }

        private IDbCommand CreateGetOperatorByDynamicColumnIDCommand(short columnID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_OperatorByDynamicColumnID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramColumnID = cmd.CreateParameter();
            paramColumnID.ParameterName = "@DataTypeID";
            paramColumnID.Value = columnID;

            cmdParams.Add(paramColumnID);
            return cmd;
        }

        internal List<OperatorMstInfo> GetOperatorList()
        {
            List<OperatorMstInfo> oOperatorInfoCollection = new List<OperatorMstInfo>();


            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                con.Open();
                IDataReader reader;
                cmd = this.CreateGetOperatorNameByOperatorID();
                cmd.Connection = con;
                reader = cmd.ExecuteReader();
                OperatorMstInfo oOperatorMstInfo = null;
                while (reader.Read())
                {
                    oOperatorMstInfo = new OperatorMstInfo();
                    oOperatorMstInfo.OperatorID = reader.GetInt16Value("OperatorID");
                    oOperatorMstInfo.OperatorName = reader.GetStringValue("OperatorName");
                    oOperatorMstInfo.OperatorNameLabelID = reader.GetInt32Value("OperatorNameLabelID");
                    oOperatorMstInfo.IsActive = reader.GetBooleanValue("IsActive");
                    oOperatorInfoCollection.Add(oOperatorMstInfo);
                }

            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return oOperatorInfoCollection;
        }

        private IDbCommand CreateGetOperatorNameByOperatorID()
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_OperatorList");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }
    }
}