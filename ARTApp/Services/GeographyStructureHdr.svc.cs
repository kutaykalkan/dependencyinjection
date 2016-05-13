using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Data;
using SkyStem.ART.App.DAO;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Exception;
using System.Data.SqlClient;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "GeographyStructureHdr" here, you must also update the reference to "GeographyStructureHdr" in Web.config.
    public class GeographyStructureHdr : IGeographyStructureHdr
    {


        #region IGeographyStructureHdr Members

        public void DoWork()
        {
        }
        public int InsertGeographyStructureHdr(List<GeographyStructureHdrInfo> oGeogStructHdrInfoCollection
            , IDbConnection oConnection, IDbTransaction oTransaction, int companyID, bool isActive
            , DateTime dateAdded, string addedBy, short companyGeographyClassID, AppUserInfo oAppUserInfo)
        {
            int rowsAffected = 0;
            ServiceHelper.SetConnectionString(oAppUserInfo);
            GeographyStructureHdrDAO oGeogStructHdrDAO = new GeographyStructureHdrDAO(oAppUserInfo);
            DataTable dt = DataImportServiceHelper.ConvertGeoStructListToDataTable(oGeogStructHdrInfoCollection);
            rowsAffected = oGeogStructHdrDAO.InsertHolidayCalendarDataTable(dt, oConnection, oTransaction, companyID, isActive
                , dateAdded, addedBy, companyGeographyClassID);
            return rowsAffected;
        }

        public bool IsGeographyStructurePresentByCompanyID(int companyID, AppUserInfo oAppUserInfo)
        {
            GeographyStructureHdrDAO oGeogStructHdrDAO = null;
            bool isPresent = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                oGeogStructHdrDAO = new GeographyStructureHdrDAO(oAppUserInfo);
                isPresent = oGeogStructHdrDAO.IsGeographyStructurePresentByCompanyID(companyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return isPresent;
        }
        #endregion
    }
}
