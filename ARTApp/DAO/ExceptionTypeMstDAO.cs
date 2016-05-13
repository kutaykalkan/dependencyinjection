


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
    public class ExceptionTypeMstDAO : ExceptionTypeMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ExceptionTypeMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<ExceptionTypeMstInfo> GetAllExceptionTypes()
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<ExceptionTypeMstInfo> oExceptionTypeMstInfoCollection = null;

            try
            {
                cmd = CreateGetAllExceptionTypesCommand();
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oExceptionTypeMstInfoCollection = MapObjects(dr);
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
            return oExceptionTypeMstInfoCollection;


        }

        private IDbCommand CreateGetAllExceptionTypesCommand()
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_ExceptionType");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }
    }
}