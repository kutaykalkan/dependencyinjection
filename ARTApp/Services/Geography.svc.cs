using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Geography" here, you must also update the reference to "Geography" in Web.config.
    public class Geography : IGeography
    {
        ///// <summary>
        ///// Selects all geography structure for a company
        ///// </summary>
        ///// <param name="companyID">Unique identifier of a company</param>
        ///// <returns>List of geography structures</returns>
        //public List<GeographyStructureHdrInfo> SelectAllGeographyStructureByCompanyID(int companyID)
        //{
        //    try
        //    {
        //        GeographyStructureHdrDAO oGeographyStructureHdrDAO = new GeographyStructureHdrDAO(oAppUserInfo);
        //        return (List<GeographyStructureHdrInfo>)oGeographyStructureHdrDAO.SelectAllByCompanyID(companyID);
        //    }
        //    catch (SqlException ex)
        //    {
        //        ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
        //    }

        //    return null;
        //}
    }
}
