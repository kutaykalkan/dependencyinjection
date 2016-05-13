using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO
{
    public class GLDataStatusDAO : GLDataStatusDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GLDataStatusDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<GLDataStatusInfo> GetAuditTrailData(long? GLDataID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<GLDataStatusInfo> oGLDataStatusInfoCollection = null;

            try
            {
                cmd = CreateGetAuditTrailDataCommand(GLDataID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oGLDataStatusInfoCollection = MapObjects(dr);
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
            return oGLDataStatusInfoCollection;
        }

        private IDbCommand CreateGetAuditTrailDataCommand(long? GLDataID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_GLDataStatusByGLDataID");

            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramGLDataID = cmd.CreateParameter();
            paramGLDataID.ParameterName = "@GLDataID";
            paramGLDataID.Value = GLDataID;
            cmdParams.Add(paramGLDataID);

            return cmd;
        }
    }
}