


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
    public class GLDataUnexplainedVarianceDAO : GLDataUnexplainedVarianceDAOBase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public GLDataUnexplainedVarianceDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<GLDataUnexplainedVarianceInfo> GetUnexplainedVarianceHistory(long? glDataID)
        {

            List<GLDataUnexplainedVarianceInfo> objGLDataUnexplainedVarianceInfoCollection = new List<GLDataUnexplainedVarianceInfo>();
            GLDataUnexplainedVarianceInfo objGLDataUnexplainedVarianceInfo = null;
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.SearchUnexplainedvarianceHistoryCommand(glDataID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);


                while (reader.Read())
                {
                    objGLDataUnexplainedVarianceInfo = new GLDataUnexplainedVarianceInfo();
                    objGLDataUnexplainedVarianceInfo = MapObject(reader);
                    objGLDataUnexplainedVarianceInfoCollection.Add(objGLDataUnexplainedVarianceInfo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }

            return objGLDataUnexplainedVarianceInfoCollection;


        }

        private IDbCommand SearchUnexplainedvarianceHistoryCommand(long? glDataID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_GLDataUnexplainedVarianceHistory");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@GLDataID";
            par.Value = glDataID;
            cmdParams.Add(par);

            return cmd;
        }
    }





}