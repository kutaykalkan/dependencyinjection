


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using System.Data.SqlClient;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{   
    public class AgingCategoryMstDAO : AgingCategoryMstDAOBase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public AgingCategoryMstDAO(AppUserInfo oAppUserInfo) :
            base( oAppUserInfo)
        {           
        }
        public IList<AgingCategoryMstInfo> GetAllAgingcategory(int languageID, int defaultLanguageID)
        {
            IDbCommand oCommand = this.GetAllAgingcategoryCommand(languageID, defaultLanguageID);
            oCommand.Connection = this.CreateConnection();
            return this.Select(oCommand);
        }
        private IDbCommand GetAllAgingcategoryCommand(int languageID, int defaultLanguageID)
        {
            IDbCommand oCommand = this.CreateCommand("usp_SEL_AllAgingcategory");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection oParamCollection = oCommand.Parameters;
            SqlParameter paramLanguageID = new SqlParameter("@languageID", languageID);
            SqlParameter paramDefaultLanguageID = new SqlParameter("@defaultLanguageID", defaultLanguageID);

            oParamCollection.Add(paramLanguageID);
            oParamCollection.Add(paramDefaultLanguageID);

            return oCommand;
        }

        public IList<AgingCategoryMstInfo> GetAllAgingcategory()
        {
            IDbCommand oCommand = this.GetAllAgingcategoryCommand();
            oCommand.Connection = this.CreateConnection();
            return this.Select(oCommand);
        }
        private IDbCommand GetAllAgingcategoryCommand()
        {
            IDbCommand oCommand = this.CreateCommand("usp_SEL_AllAgingcategory");
            oCommand.CommandType = CommandType.StoredProcedure;
            
            return oCommand;
        }
    }
}