using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Client.Model.CompanyDatabase;
using System.Threading.Tasks;
using System.Collections.Concurrent;


namespace SkyStem.ART.Service.APP.BLL
{
    class DataProcessingControlBoard
    {
        public static void DataProcessingControl()
        {
            try
            {
                Dictionary<string, CompanyUserInfo> oDictConnectionString = CacheHelper.GetDistinctDatabaseList();
                ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();

                Parallel.ForEach<CompanyUserInfo>(oDictConnectionString.Values, oCompanyUserInfo =>
                {
                    try
                    {
                        string s1 = oCompanyUserInfo.DatabaseName;
                        MaterialityAndSRA oMaterialSRA = new MaterialityAndSRA(oCompanyUserInfo);

                        if (oMaterialSRA.IsProcessingRequiredForMaterialityAndSRA())
                            oMaterialSRA.SetMaterialityAndSRAStaus();

                        AccountDataImport oAccountDataImport = new AccountDataImport(oCompanyUserInfo);
                        if (oAccountDataImport.IsProcessingRequiredForAccountDataImport())
                            oAccountDataImport.ProcessAccountDataImport();

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

                        AccountAttributeDataImport oAcctAttrDI = new AccountAttributeDataImport(oCompanyUserInfo);
                        if (oAcctAttrDI.IsProcessingRequiredForAccountAttributeImport())
                        {
                            oAcctAttrDI.ProcessAccountAttributeImport();
                        }

                        CurrencyDataImport oCurrencyDataImport = new CurrencyDataImport(oCompanyUserInfo);
                        if (oCurrencyDataImport.IsProcessingRequiredForCurrencyDataImport())
                        {
                            oCurrencyDataImport.ProcessCurrencyDataImport();
                        }
                    }
                    catch (Exception ex)
                    {
                        exceptions.Enqueue(ex);
                    }
                });
                while (exceptions.Count > 0)
                {
                    Exception exOut;
                    if (exceptions.TryDequeue(out exOut))
                        Helper.LogError(exOut, null);
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, null);
            }
        }
    }
}
