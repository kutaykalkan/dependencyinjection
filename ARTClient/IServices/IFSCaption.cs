using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IFSCaption" here, you must also update the reference to "IFSCaption" in Web.config.
    [ServiceContract]
    public interface IFSCaption
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        IList<FSCaptionHdrInfo> SelectAllFSCaptionByCompanyID(int companyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        //IList<FSCaptionInfo_ExtendedWithMaterialityInfo> SelectAllFSCaptionMergeMaterilityByCompanyID(int companyID);
        IList<FSCaptionInfo_ExtendedWithMaterialityInfo> SelectAllFSCaptionMergeMaterilityByReconciliationPeriodID(int? reconciliationPeriodID, AppUserInfo oAppUserInfo);

        //[OperationContract]
        //int SaveMaterilityByFSCaptionTableValue(int? reconciliationPeriodID, IList<FSCaptionMaterialityInfo> lstFSCaptionMaterialityInfo, int companyID);

        /// <summary>
        /// This method is used to auto populate FS Caption text box based on the basis of 
        /// the prefix text typed in the text box
        /// </summary>
        /// <param name="companyId">Company id</param>
        /// <param name="prefixText">The text which was typed in the text box</param>
        /// <param name="count">Number of results to be returned</param>
        /// <returns>List of FS Caption</returns>
        [OperationContract]
        string[] SelectFSCaptionByCompanyIDAndPrefixText(int companyId, string prefixText, int count, int LCID, int businessEntityID, int defaultLCID, AppUserInfo oAppUserInfo);
    }//end of interface
}//end of namespace
