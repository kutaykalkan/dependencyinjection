


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class FSCaptionHdrDAO : FSCaptionHdrDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FSCaptionHdrDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        #region SelectFSCaptionByCompanyIDAndPrefixText
        /// <summary>
        /// This method is used to auto populate FS Caption text box based on the basis of 
        /// the prefix text typed in the text box
        /// </summary>
        /// <param name="companyId">Company id</param>
        /// <param name="prefixText">The text which was typed in the text box</param>
        /// <param name="count">Number of results to be returned</param>
        /// <returns>List of FS Caption</returns>
        public string[] SelectFSCaptionByCompanyIDAndPrefixText(int companyId, string prefixText, int count, int LCID, int businessEntityID, int defaultLCID)
        {
            List<string> oFSCaptionCollection = new List<string>();
            IDbConnection oConnection = null;
            IDbCommand oCommand = null;
            try
            {
                oConnection = this.CreateConnection();
                oCommand = this.CreateSelectFSCaptionByCompanyIDAndPrefixTextCommand(companyId, prefixText, count, LCID, businessEntityID, defaultLCID);
                oCommand.Connection = oConnection;
                oConnection.Open();
                IDataReader reader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oFSCaptionCollection.Add(reader.GetStringValue("FSCaption"));
                }
                reader.Close();

            }
            finally
            {
                if ((oConnection != null) && (oConnection.State != ConnectionState.Closed))
                    oConnection.Dispose();
            }

            return oFSCaptionCollection.ToArray();
        }

        /// <summary>
        /// Creates command to use stored procedure and fills up all parameter value
        /// </summary>
        /// <param name="companyId">Company id</param>
        /// <param name="prefixText">The text which was typed in the text box</param>
        /// <param name="count">Number of results to be returned</param>
        /// <returns>Command which is to be executed</returns>
        private IDbCommand CreateSelectFSCaptionByCompanyIDAndPrefixTextCommand(int companyId, string prefixText, int count, int LCID, int businessEntityID, int defaultLCID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_FSCaptionByCompanyIDAndPrefixText");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parCompanyID = cmd.CreateParameter();
            IDbDataParameter parPrefixText = cmd.CreateParameter();
            IDbDataParameter parCount = cmd.CreateParameter();

            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = companyId;

            parPrefixText.ParameterName = "@PrefixText";
            parPrefixText.Value = prefixText;

            parCount.ParameterName = "@Count";
            parCount.Value = count;

            cmdParams.Add(parCompanyID);
            cmdParams.Add(parPrefixText);
            cmdParams.Add(parCount);

            ServiceHelper.AddCommonLanguageParameters(cmd, cmdParams, LCID, businessEntityID, defaultLCID);

            return cmd;
        }
        #endregion
    }
}