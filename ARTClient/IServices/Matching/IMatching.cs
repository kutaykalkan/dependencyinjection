using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Client.Params;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.Client.Model;


namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IMatching" here, you must also update the reference to "IMatching" in Web.config.
    [ServiceContract]
    public interface IMatching
    {
        /// <summary>
        /// Select all Match Sets
        /// </summary>
        /// <returns>List of Match Sets</returns>
        [OperationContract]
        List<MatchSetHdrInfo> SelectAllMatchSetHdrInfo(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);



        [OperationContract]
        List<MatchingSourceTypeInfo> GetAllMatchingSourceType( AppUserInfo oAppUserInfo);

        [OperationContract]
        List<MatchingSourceDataImportHdrInfo> SaveMatchingSource(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<MatchingSourceColumnInfo> GetMatchingSourceColumn(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<DataTypeMstInfo> GetAllDataType( AppUserInfo oAppUserInfo);

        [OperationContract]
        bool SaveMatchingSourceColumn(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<MatchingSourceDataImportHdrInfo> GetMatchingSources(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool DeleteMatchingSourceData(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        MatchingSourceDataImportHdrInfo GetMatchingSourceDataImportInfo(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool UpdateMatchingSourceDataImportForForceCommit(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        string GetKeyFieldsByCompanyID(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        MatchSetHdrInfo GetMatchSet(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        MatchSetMatchingSourceDataImportInfo GetMatchSetMatchingSourceDataImport(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<MatchingSourceDataInfo> GetMatchingSourceData(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        long SaveMatchSet(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);


        [OperationContract]
        List<GLDataHdrInfo> GetAccountDetails(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        void DeleteMatchSet(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        void EditMatchSet(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<MatchSetSubSetCombinationInfo> UpdateMatchSetMatchingSourceDataImportInfo(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool IsRecItemCreated(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);


        [OperationContract]
        List<RecItemColumnMstInfo> GetAllRecItemColumns( AppUserInfo oAppUserInfo);


        [OperationContract]
        List<MatchingConfigurationInfo> GetAllMatchingConfiguration(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<MatchSetSubSetCombinationInfo> GetAllMatchSetSubSetCombination(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);


        [OperationContract]
        int UpdateMatchingConfigurationDisplayedColumn(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

		 [OperationContract]
        bool SaveRecItemColumnMapping(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
         int SaveMatchingConfiguration(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);
        
        [OperationContract]
        bool CleanRecItemColumnMapping(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool UpdateMatchingSourceDataImportHiddenStatus(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);


        [OperationContract]
        bool UpdateMatchSetSubSetCombinationForConfigStatus(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);


        [OperationContract]
        MatchSetHdrInfo GetMatchSetResults(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<MatchingSourceDataImportHdrInfo> GetAllMatchSetMatchingSources(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo );

        [OperationContract]
        bool UpdateMatchSetResults(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);
        [OperationContract]
        MatchSetHdrInfo GetMatchSetStatusMessage(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        MatchSetSubSetCombinationInfoForNetAmountCalculation GetMatchSetSubSetCombinationForNetAmountCalculationByID(Int64? MatchSetSubSetCombinationId, AppUserInfo oAppUserInfo );
    }
}
