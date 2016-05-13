using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Service.DAO;
using SkyStem.ART.Service.Service;
using Microsoft.Reporting.WinForms;

namespace SkyStem.ART.Service.APP.BLL
{
    class DataProcessingControlBoard
    {
        public static void DataProcessingControl()
        {
            try
            {
                Dictionary<string, CompanyUserInfo> oDictConnectionString = CacheHelper.GetDistinctDatabaseList();

                foreach (CompanyUserInfo oCompanyUserInfo in oDictConnectionString.Values)
                {
                    string s1 = oCompanyUserInfo.DatabaseName;
                    //MaterialityAndSRA oMaterialSRA = new MaterialityAndSRA(oCompanyUserInfo);

                    //if (oMaterialSRA.IsProcessingRequiredForMaterialityAndSRA())
                    //    oMaterialSRA.SetMaterialityAndSRAStaus();

                    //AccountDataImport oAccountDataImport = new AccountDataImport(oCompanyUserInfo);
                    //if (oAccountDataImport.IsProcessingRequiredForAccountDataImport())
                    //    oAccountDataImport.ProcessAccountDataImport();

                    GLDataImport oGLDataImport = new GLDataImport(oCompanyUserInfo);
                    if (oGLDataImport.IsProcessingRequiredForGLDataImport())
                    {
                        oGLDataImport.ProcessGLDataImport();
                    }

                    SubledgerDataImport oSubledgerDataImport = new SubledgerDataImport(oCompanyUserInfo);
                    if (oSubledgerDataImport.IsProcessingRequiredForSubledgerDataImport())
                    {
                        oSubledgerDataImport.ProcessSubledgerDataImport();
                    }

                    //AccountAttributeDataImport oAcctAttrDI = new AccountAttributeDataImport(oCompanyUserInfo);
                    //if (oAcctAttrDI.IsProcessingRequiredForAccountAttributeImport())
                    //{
                    //    oAcctAttrDI.ProcessAccountAttributeImport();
                    //}
                    //MultilingualDataImport oMultilingualDataImport = new MultilingualDataImport(oCompanyUserInfo);
                    //if (oMultilingualDataImport.IsProcessingRequiredForMultilingualDataImport())
                    //    oMultilingualDataImport.ProcessMultilingualDataImport();
                    //UserUpload oUserUpload = new UserUpload(oCompanyUserInfo);
                    //if (oUserUpload.IsProcessingRequiredForUserDataImport())
                    //    oUserUpload.ProcessUserDataImport();
                    //ExportToExcel oExportToExcel = new ExportToExcel(oCompanyUserInfo);
                    //if (oExportToExcel.IsProcessingRequiredForRequests())
                    //{
                    //    oExportToExcel.ProcessRequests();
                    //}
                    //MatchingDataImport oMatchingDataImport = new MatchingDataImport(oCompanyUserInfo);
                    //if (oMatchingDataImport.IsProcessingRequiredForMatchingDataImport())
                    //{
                    //    oMatchingDataImport.ProcessMatchingDataImport();
                    //}
                    //ScheduleRecItemImport oScheduleRecItemImport = new ScheduleRecItemImport(oCompanyUserInfo);
                    //if (oScheduleRecItemImport.IsProcessingRequiredForScheduleRecItemImport())
                    //{ 
                    //    oScheduleRecItemImport.ProcessScheduleRecItemImport();
                    //}
                    //TaskUpload oTaskUpload = new TaskUpload(oCompanyUserInfo);
                    //if (oTaskUpload.IsProcessingRequiredForTaskUpload())
                    //    oTaskUpload.ProcessTaskImport();
                    //ExportToExcel oExportToExcel = new ExportToExcel(oCompanyUserInfo);
                    //if (oExportToExcel.IsProcessingRequiredForRequests())
                    //{
                    //    oExportToExcel.ProcessRequests();
                    //}
                    //FTPDataImport oFTPDataImport = new FTPDataImport(oCompanyUserInfo);
                    //if(oFTPDataImport.IsProcessingRequiredForFTPDataImport())
                    //{
                    //    oFTPDataImport.ProcessFTPDataImport();
                    //}
                    CurrencyDataImport oCurrencyDataImport = new CurrencyDataImport(oCompanyUserInfo);
                    if (oCurrencyDataImport.IsProcessingRequiredForCurrencyDataImport())
                    {
                        oCurrencyDataImport.ProcessCurrencyDataImport();
                    }
                }
                //CompanyCreation oCompanyCreation = new CompanyCreation();
                //oCompanyCreation.ProcessCompanyCreation();
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, null);
            }
        }
    }
}
