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
    public class GeographyClassMstDAO : GeographyClassMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GeographyClassMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<GeographyClassMstInfo> GetAllGeographyClassMst(short? companyGeographyClassID)
        {
            IDbConnection oConnection = null;
            try
            {
                oConnection = this.CreateConnection();
                IDbCommand oDBCommand = this.CreateSelectKeysCommand(companyGeographyClassID);
                oDBCommand.Connection = oConnection;
                oConnection.Open();
                List<GeographyClassMstInfo> oGeographyClassMstInfoCollection = new List<GeographyClassMstInfo>();
                IDataReader reader = oDBCommand.ExecuteReader();
                while (reader.Read())
                {
                    GeographyClassMstInfo oGeographyClassMst = new GeographyClassMstInfo();
                    oGeographyClassMst = this.MapObject(reader);
                    if (oGeographyClassMst != null)
                        oGeographyClassMstInfoCollection.Add(oGeographyClassMst);
                }
                reader.Close();
                return oGeographyClassMstInfoCollection;
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }
        }


        #region "Create Command"
        /// <summary>
        /// Command for selecting all Geography Class Keys but for Company
        /// </summary>
        /// <returns>IDBCommand</returns>
        protected IDbCommand CreateSelectKeysCommand(short? companyGeographyClassID)
        {
            System.Data.IDbCommand oCommand = this.CreateCommand("usp_SEL_GeographyClassMst");
            oCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oCommand.Parameters;

            IDbDataParameter cmdParamCompanyGeoClassID = oCommand.CreateParameter();
            cmdParamCompanyGeoClassID.ParameterName = "@CompanyGeoClassID";
            cmdParamCompanyGeoClassID.Value = companyGeographyClassID.Value;
            cmdParams.Add(cmdParamCompanyGeoClassID);

            return oCommand;
        }
        #endregion
    }
}