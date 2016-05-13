


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SkyStem.ART.App.DAO
{
    public class ReportParameterKeyMstDAO : ReportParameterKeyMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReportParameterKeyMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<ReportParameterKeyMstInfo> GetAllReportParamKeys()
        {
            IDbCommand oCommand = this.GetAllReportParamKeysCommand();
            oCommand.Connection = this.CreateConnection();
            return this.Select(oCommand);
        }
        private IDbCommand GetAllReportParamKeysCommand()
        {
            IDbCommand oCommand = this.CreateCommand("usp_SEL_AllReportParameterKeys");
            oCommand.CommandType = CommandType.StoredProcedure;

            //IDataParameterCollection oParamCollection = oCommand.Parameters;
            //SqlParameter paramLanguageID = new SqlParameter("@languageID", languageID);
            //SqlParameter paramDefaultLanguageID = new SqlParameter("@defaultLanguageID", defaultLanguageID);

            //oParamCollection.Add(paramLanguageID);
            //oParamCollection.Add(paramDefaultLanguageID);

            return oCommand;
        }
    }
}