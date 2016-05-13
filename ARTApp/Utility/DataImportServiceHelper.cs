using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.Utility
{
    public class DataImportServiceHelper
    {
        internal static DataTable ConvertHolidayCalendarListToDataTable(List<HolidayCalendarInfo> oHolidayCalendarInfoCollection)
        {
            DataTable dt;

            dt = new DataTable();
            dt.Columns.Add("DataImportID", System.Type.GetType("System.Int32"));
            dt.Columns.Add("HolidayName", System.Type.GetType("System.String"));
            dt.Columns.Add("HolidayNameLabelID", System.Type.GetType("System.Int32"));
            dt.Columns.Add("HolidayDate", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("GeographyObjectID", System.Type.GetType("System.Int32"));
            dt.Columns.Add("IsActive", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("DateAdded", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("AddedBy", System.Type.GetType("System.String"));
            dt.Columns.Add("DateRevised", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("RevisedBy", System.Type.GetType("System.String"));
            DataRow dr;
            foreach (HolidayCalendarInfo oHolidayCal in oHolidayCalendarInfoCollection)
            {
                dr = dt.NewRow();
                if (!oHolidayCal.IsDataImportIDNull)
                    dr["DataImportID"] = oHolidayCal.DataImportID.Value;
                if (!oHolidayCal.IsHolidayNameNull)
                    dr["HolidayName"] = oHolidayCal.HolidayName;
                if (!oHolidayCal.IsHolidayDateNull)
                    dr["HolidayDate"] = Convert.ToDateTime(oHolidayCal.HolidayDate.Value.ToShortDateString());
                if (!oHolidayCal.IsHolidayNameLabelIDNull)
                    dr["HolidayNameLabelID"] = oHolidayCal.HolidayNameLabelID.Value;
                if (!oHolidayCal.IsGeographyObjectIDNull)
                    dr["GeographyObjectID"] = oHolidayCal.GeographyObjectID.Value;
                if (!oHolidayCal.IsAddedByNull)
                    dr["AddedBy"] = oHolidayCal.AddedBy;
                if (!oHolidayCal.IsDateAddedNull)
                    dr["DateAdded"] = Convert.ToDateTime(oHolidayCal.DateAdded.Value.ToShortDateString());
                if (!oHolidayCal.IsIsActiveNull)
                    dr["IsActive"] = oHolidayCal.IsActive.Value;
                if (!oHolidayCal.IsDateRevisedNull)
                    dr["DateRevised"] = Convert.ToDateTime(oHolidayCal.DateRevised.Value.ToShortDateString());
                if (!oHolidayCal.IsRevisedByNull)
                    dr["RevisedBy"] = oHolidayCal.RevisedBy;

                dt.Rows.Add(dr);
                dr = null;
            }

            return dt;
        }

        internal static DataTable ConvertRecPeriodListToDataTable(List<ReconciliationPeriodInfo> oRecPeriodInfoCollection)
        {
            DataTable dtRecPeriod = new DataTable();

            dtRecPeriod.Columns.Add("CompanyID", System.Type.GetType("System.Int32"));
            dtRecPeriod.Columns.Add("DataImportID", System.Type.GetType("System.Int32"));
            dtRecPeriod.Columns.Add("PeriodNumber", System.Type.GetType("System.Int32"));
            dtRecPeriod.Columns.Add("PeriodEndDate", System.Type.GetType("System.DateTime"));
            dtRecPeriod.Columns.Add("ReconciliationPeriodStatusID", System.Type.GetType("System.Int16"));
            dtRecPeriod.Columns.Add("CertificationStartDate", System.Type.GetType("System.DateTime"));
            dtRecPeriod.Columns.Add("CertificationLockDownDate", System.Type.GetType("System.DateTime"));
            dtRecPeriod.Columns.Add("ReconciliationCloseDate", System.Type.GetType("System.DateTime"));
            dtRecPeriod.Columns.Add("ReportingCurrencyCode", System.Type.GetType("System.String"));
            dtRecPeriod.Columns.Add("ActivationDate", System.Type.GetType("System.DateTime"));
            dtRecPeriod.Columns.Add("BaseCurrency", System.Type.GetType("System.String"));
            dtRecPeriod.Columns.Add("ReportingCurrency", System.Type.GetType("System.String"));
            dtRecPeriod.Columns.Add("MaterialityTypeID", System.Type.GetType("System.Int16"));
            dtRecPeriod.Columns.Add("CompanyMaterialityThreshold", System.Type.GetType("System.Decimal"));
            dtRecPeriod.Columns.Add("UnexplainedVarianceThreshold", System.Type.GetType("System.Decimal"));
            dtRecPeriod.Columns.Add("IsActive", System.Type.GetType("System.Boolean"));
            dtRecPeriod.Columns.Add("DateAdded", System.Type.GetType("System.DateTime"));
            dtRecPeriod.Columns.Add("AddedBy", System.Type.GetType("System.String"));
            dtRecPeriod.Columns.Add("DateRevised", System.Type.GetType("System.DateTime"));
            dtRecPeriod.Columns.Add("RevisedBy", System.Type.GetType("System.String"));
            
            DataRow dr;
            foreach (ReconciliationPeriodInfo oRecPeriodInfo in oRecPeriodInfoCollection)
            {
                dr = dtRecPeriod.NewRow();

                if (!oRecPeriodInfo.IsDataImportIDNull)
                    dr["CompanyID"] = oRecPeriodInfo.CompanyID.Value;

                if (!oRecPeriodInfo.IsDataImportIDNull)
                    dr["DataImportID"] = oRecPeriodInfo.DataImportID.Value;

                if (!oRecPeriodInfo.IsPeriodNumberNull)
                    dr["PeriodNumber"] = oRecPeriodInfo.PeriodNumber.Value;

                if (!oRecPeriodInfo.IsPeriodEndDateNull)
                    dr["PeriodEndDate"] = oRecPeriodInfo.PeriodEndDate.Value;

                if (!oRecPeriodInfo.IsReconciliationPeriodStatusIDNull)
                    dr["ReconciliationPeriodStatusID"] = oRecPeriodInfo.ReconciliationPeriodStatusID.Value;

                if (!oRecPeriodInfo.IsCertificationStartDateNull)
                    dr["CertificationStartDate"] = Convert.ToDateTime (oRecPeriodInfo.CertificationStartDate.Value.ToShortTimeString ());

                if (!oRecPeriodInfo.IsCertificationLockDownDateNull)
                    dr["CertificationLockDownDate"] = Convert.ToDateTime (oRecPeriodInfo.CertificationLockDownDate.Value.ToShortTimeString());

                if (!oRecPeriodInfo.IsReconciliationCloseDateNull)
                    dr["ReconciliationCloseDate"] = Convert.ToDateTime (oRecPeriodInfo.ReconciliationCloseDate.Value.ToShortTimeString());

                if (!oRecPeriodInfo.IsReportingCurrencyCodeNull)
                    dr["ReportingCurrencyCode"] = oRecPeriodInfo.ReportingCurrencyCode;

                if (!oRecPeriodInfo.IsActivationDateNull)
                    dr["ActivationDate"] = Convert.ToDateTime (oRecPeriodInfo.ActivationDate.Value.ToShortTimeString());

                if (!oRecPeriodInfo.IsBaseCurrencyNull)
                    dr["BaseCurrency"] = oRecPeriodInfo.BaseCurrency;

                if (!oRecPeriodInfo.IsReportingCurrencyNull)
                    dr["ReportingCurrency"] = oRecPeriodInfo.ReportingCurrency;

                if (!oRecPeriodInfo.IsMaterialityTypeIDNull)
                    dr["MaterialityTypeID"] = oRecPeriodInfo.MaterialityTypeID.Value;

                if (!oRecPeriodInfo.IsCompanyMaterialityThresholdNull)
                    dr["CompanyMaterialityThreshold"] = oRecPeriodInfo.CompanyMaterialityThreshold.Value;

                if (!oRecPeriodInfo.IsUnexplainedVarianceThresholdNull)
                    dr["UnexplainedVarianceThreshold"] = oRecPeriodInfo.UnexplainedVarianceThreshold.Value;

                if (!oRecPeriodInfo.IsIsActiveNull)
                    dr["IsActive"] = oRecPeriodInfo.IsActive.Value;

                if (!oRecPeriodInfo.IsDateAddedNull)
                    dr["DateAdded"] = Convert.ToDateTime (oRecPeriodInfo.DateAdded.Value.ToShortTimeString ());

                if (!oRecPeriodInfo.IsAddedByNull)
                    dr["AddedBy"] = oRecPeriodInfo.AddedBy;

                if (!oRecPeriodInfo.IsDateRevisedNull)
                    dr["DateRevised"] = Convert.ToDateTime (oRecPeriodInfo.DateRevised.Value.ToShortTimeString ());

                if (!oRecPeriodInfo.IsRevisedByNull)
                    dr["RevisedBy"] = oRecPeriodInfo.RevisedBy;

                dtRecPeriod.Rows.Add(dr);
                dr = null;
            }
            return dtRecPeriod;
        }

        internal static DataTable ConvertGeoStructListToDataTable(List<GeographyStructureHdrInfo> oGeoStructInfoCollection)
        {
            DataTable dtGeoStruct = new DataTable();
            //CompanyID, GeographyClassID, GeographyStructure
            //, GeographyStructureLabelID, IsActive, DateAdded, AddedBy

            //dtGeoStruct.Columns.Add("CompanyID", System.Type.GetType("System.Int32"));
            dtGeoStruct.Columns.Add("GeographyClassID", System.Type.GetType("System.Int32"));
            dtGeoStruct.Columns.Add("GeographyStructure", System.Type.GetType("System.String"));
            dtGeoStruct.Columns.Add("GeographyStructureLabelID", System.Type.GetType("System.Int32"));
            //dtGeoStruct.Columns.Add("ParentGeographyStructureID", System.Type.GetType("System.Int32"));
            //dtGeoStruct.Columns.Add("IsActive", System.Type.GetType("System.Boolean"));
            //dtGeoStruct.Columns.Add("DateAdded", System.Type.GetType("System.DateTime"));
            //dtGeoStruct.Columns.Add("AddedBy", System.Type.GetType("System.String"));
            DataRow dr;
            foreach (GeographyStructureHdrInfo oGeoStructInfo in oGeoStructInfoCollection)
            {
                dr = dtGeoStruct.NewRow();
                //if (!oGeoStructInfo.IsCompanyIDNull)
                //    dr["CompanyID"] = oGeoStructInfo.CompanyID.Value;
                if (!oGeoStructInfo.IsGeographyClassIDNull)
                    dr["GeographyClassID"] = oGeoStructInfo.GeographyClassID.Value;
                if (!oGeoStructInfo.IsGeographyStructureNull)
                    dr["GeographyStructure"] = oGeoStructInfo.GeographyStructure;
                if (!oGeoStructInfo.IsGeographyStructureLabelIDNull)
                    dr["GeographyStructureLabelID"] = oGeoStructInfo.GeographyStructureLabelID.Value;
                //if (!oGeoStructInfo.IsParentGeographyStructureIDNull)
                //    dr["ParentGeographyStructureID"] = oGeoStructInfo.ParentGeographyStructureID.Value;
                //if (!oGeoStructInfo.IsIsActiveNull)
                //    dr["IsActive"] = oGeoStructInfo.IsActive.Value;
                //if (!oGeoStructInfo.IsDateAddedNull)
                //    dr["DateAdded"] = oGeoStructInfo.DateAdded.Value;
                //if (!oGeoStructInfo.IsAddedByNull)
                //    dr["AddedBy"] = oGeoStructInfo.AddedBy;
                dtGeoStruct.Rows.Add(dr);
            }
            return dtGeoStruct;
        }


        internal static DataTable ConvertCurrencyExchangeRateListToDataTable(List<ExchangeRateInfo> oExchangeRateInfoInfoCollection)
        {
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add("FromCurrencyCode", System.Type.GetType("System.String"));
            dt.Columns.Add("ToCurrencyCode", System.Type.GetType("System.String"));
            dt.Columns.Add("ExchangeRate", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("ReconciliationPeriodID", System.Type.GetType("System.Int32"));
            dt.Columns.Add("IsActive", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("DateAdded", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("AddedBy", System.Type.GetType("System.String"));
            //dt.Columns.Add("DateRevised", System.Type.GetType("System.DateTime"));
            //dt.Columns.Add("RevisedBy", System.Type.GetType("System.String"));
            DataRow dr;
            foreach (ExchangeRateInfo oExchangeRateInfo in oExchangeRateInfoInfoCollection)
            {
                dr = dt.NewRow();
                dr["FromCurrencyCode"] = oExchangeRateInfo.FromCurrencyCode;
                dr["ToCurrencyCode"] = oExchangeRateInfo.ToCurrencyCode;
                dr["ExchangeRate"] = oExchangeRateInfo.ExchangeRate;
                dr["ReconciliationPeriodID"] = oExchangeRateInfo.ReconciliationPeriodID;
                dr["AddedBy"] = oExchangeRateInfo.AddedBy;
                dr["DateAdded"] = Convert.ToDateTime(oExchangeRateInfo.DateAdded.Value.ToShortTimeString());
                dr["IsActive"] = oExchangeRateInfo.IsActive.Value;
                dt.Rows.Add(dr);
                dr = null;
            }

            return dt;
        }


        internal static DataTable ConvertCurrencyCodeListToDataTable(List<CurrencyCodeInfo> oCurrencyCodeInfoCollection)
        {
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add("ComapnyID", System.Type.GetType("System.Int32"));
            dt.Columns.Add("CurrencyCode", System.Type.GetType("System.String"));
            dt.Columns.Add("Description", System.Type.GetType("System.String"));
            dt.Columns.Add("IsActive", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("DateAdded", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("AddedBy", System.Type.GetType("System.String"));
            //dt.Columns.Add("DateRevised", System.Type.GetType("System.DateTime"));
            //dt.Columns.Add("RevisedBy", System.Type.GetType("System.String"));
            DataRow dr;
            foreach (CurrencyCodeInfo oCurrencyCodeInfo in oCurrencyCodeInfoCollection)
            {
                dr = dt.NewRow();
                dr["ComapnyID"] = oCurrencyCodeInfo.ComapnyID;
                dr["CurrencyCode"] = oCurrencyCodeInfo.CurrencyCode;
                dr["Description"] = oCurrencyCodeInfo.Description;
                dr["IsActive"] = oCurrencyCodeInfo.IsActive;
                dr["DateAdded"] = Convert.ToDateTime(oCurrencyCodeInfo.DateAdded.Value.ToShortTimeString());
                dr["AddedBy"] = oCurrencyCodeInfo.AddedBy;
               
                dt.Rows.Add(dr);
                dr = null;
            }

            return dt;
        }

        internal static DataTable ConvertSubledgerListToDataTable(List<SubledgerSourceInfo> oSubledgerSourceInfoInfoCollection)
        {
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add("SubledgerSource", System.Type.GetType("System.String"));
            dt.Columns.Add("CompanyID", System.Type.GetType("System.Int32"));
            dt.Columns.Add("IsActive", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("DateAdded", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("AddedBy", System.Type.GetType("System.String"));
            dt.Columns.Add("SubledgerSourceLabelID", System.Type.GetType("System.Int32"));
            //dt.Columns.Add("DateRevised", System.Type.GetType("System.DateTime"));
            //dt.Columns.Add("RevisedBy", System.Type.GetType("System.String"));
            DataRow dr;
            foreach (SubledgerSourceInfo oSubledgerSourceInfo in oSubledgerSourceInfoInfoCollection)
            {
                dr = dt.NewRow();
                dr["SubledgerSource"] = oSubledgerSourceInfo.SubledgerSource;
                dr["CompanyID"] = oSubledgerSourceInfo.CompanyID;

                dr["AddedBy"] = oSubledgerSourceInfo.AddedBy;
                dr["DateAdded"] = Convert.ToDateTime(oSubledgerSourceInfo.DateAdded.Value.ToShortTimeString ());
                dr["IsActive"] = oSubledgerSourceInfo.IsActive.Value;
                dr["SubledgerSourceLabelID"] = oSubledgerSourceInfo.SubledgerSourceLabelID;
                dt.Rows.Add(dr);
                dr = null;
            }

            return dt;
        }

        internal static DataTable ConvertGLDataRecItemListToDataTable(List<GLDataRecItemInfo> oGLDataRecItemInfoCollection)
        {
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add("Amount", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("AmountInBaseCurrency", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("AmountInReportingCurrency", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OpenDate", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("Comments", System.Type.GetType("System.String"));
            dt.Columns.Add("LocalCurrency", System.Type.GetType("System.String"));
            dt.Columns.Add("ExcelRowNumber", System.Type.GetType("System.Int64"));
            dt.Columns.Add("MatchSetMatchingSourceDataImportID", System.Type.GetType("System.Int64"));
            dt.Columns.Add("IndexID", System.Type.GetType("System.Int16"));
            dt.Columns.Add("WriteOnOffID", System.Type.GetType("System.Int16"));

            DataRow dr;
          
            foreach (GLDataRecItemInfo oGLDataRecItemInfo in oGLDataRecItemInfoCollection)
            {
                dr = dt.NewRow();
                if (oGLDataRecItemInfo.Amount.HasValue)
                    dr["Amount"] = oGLDataRecItemInfo.Amount.Value  ;
                if(oGLDataRecItemInfo.AmountBaseCurrency.HasValue )
                    dr["AmountInBaseCurrency"] = oGLDataRecItemInfo.AmountBaseCurrency ;
                if (oGLDataRecItemInfo.AmountReportingCurrency.HasValue)
                    dr["AmountInReportingCurrency"] = oGLDataRecItemInfo.AmountReportingCurrency ;
                if (oGLDataRecItemInfo.OpenDate.HasValue)
                    dr["OpenDate"] =oGLDataRecItemInfo.OpenDate;
                if (oGLDataRecItemInfo.ExcelRowNumber.HasValue)
                    dr["ExcelRowNumber"] = Convert.ToInt64(oGLDataRecItemInfo.ExcelRowNumber.Value);
                if (oGLDataRecItemInfo.MatchSetMatchingSourceDataImportID.HasValue)
                    dr["MatchSetMatchingSourceDataImportID"] = Convert.ToInt64(oGLDataRecItemInfo.MatchSetMatchingSourceDataImportID.Value);
                if (oGLDataRecItemInfo.IndexID.HasValue)
                    dr["IndexID"] = Convert.ToInt16(oGLDataRecItemInfo.IndexID.Value);  

                dr["Comments"] = oGLDataRecItemInfo.Comments ;
                dr["LocalCurrency"] = oGLDataRecItemInfo.LocalCurrencyCode ;
                dr["WriteOnOffID"] = 0;
                dt.Rows.Add(dr);
                dr = null;
            }

            return dt;
        }

        internal static DataTable GetMatchSetGLDataRecItemTable(List<GLDataRecItemInfo> oGLDataRecItemInfoCollection)
        {
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add("MatchSetSubSetCombinationID", System.Type.GetType("System.Int64"));
            dt.Columns.Add("GLDataRecItemRefID", System.Type.GetType("System.Int64"));
            dt.Columns.Add("RecItemNumber", System.Type.GetType("System.String"));
            dt.Columns.Add("ExcelRowNumber", System.Type.GetType("System.Int64"));
            dt.Columns.Add("MatchSetMatchingSourceDataImportID", System.Type.GetType("System.Int64")); 
            DataRow dr;
            foreach (GLDataRecItemInfo oGLDataRecItemInfo in oGLDataRecItemInfoCollection)
            {
                dr = dt.NewRow();
                if (oGLDataRecItemInfo.MatchSetSubSetCombinationID .HasValue)
                    dr["MatchSetSubSetCombinationID"] = Convert.ToInt64(oGLDataRecItemInfo.MatchSetSubSetCombinationID.Value);
                if (oGLDataRecItemInfo.GLDataRecItemID .HasValue)
                    dr["GLDataRecItemRefID"] = Convert.ToInt64(oGLDataRecItemInfo.GLDataRecItemID.Value);
                dr["RecItemNumber"] = oGLDataRecItemInfo.RecItemNumber;
                if (oGLDataRecItemInfo.ExcelRowNumber.HasValue)
                    dr["ExcelRowNumber"] = Convert.ToInt64(oGLDataRecItemInfo.ExcelRowNumber.Value);
                if (oGLDataRecItemInfo.MatchSetMatchingSourceDataImportID.HasValue)
                    dr["MatchSetMatchingSourceDataImportID"] = Convert.ToInt64(oGLDataRecItemInfo.MatchSetMatchingSourceDataImportID.Value);
                dt.Rows.Add(dr);
                dr = null;
            }
            return dt;
        }
        internal static DataTable GetMatchSetGLDataRecItemTable(List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection)
        {
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add("MatchSetSubSetCombinationID", System.Type.GetType("System.Int64"));
            dt.Columns.Add("GLDataRecItemRefID", System.Type.GetType("System.Int64"));
            dt.Columns.Add("RecItemNumber", System.Type.GetType("System.String"));
            dt.Columns.Add("ExcelRowNumber", System.Type.GetType("System.Int64"));
            dt.Columns.Add("MatchSetMatchingSourceDataImportID", System.Type.GetType("System.Int64"));
            DataRow dr;
            foreach (GLDataWriteOnOffInfo oGLDataWriteOnOffInfo in oGLDataWriteOnOffInfoCollection)
            {
                dr = dt.NewRow();
                if (oGLDataWriteOnOffInfo.MatchSetSubSetCombinationID.HasValue)
                    dr["MatchSetSubSetCombinationID"] = Convert.ToInt64(oGLDataWriteOnOffInfo.MatchSetSubSetCombinationID.Value);
                if (oGLDataWriteOnOffInfo.GLDataWriteOnOffID .HasValue)
                    dr["GLDataRecItemRefID"] = Convert.ToInt64(oGLDataWriteOnOffInfo.GLDataWriteOnOffID.Value);
                dr["RecItemNumber"] = oGLDataWriteOnOffInfo.RecItemNumber;
                if (oGLDataWriteOnOffInfo.ExcelRowNumber.HasValue)
                    dr["ExcelRowNumber"] = Convert.ToInt64(oGLDataWriteOnOffInfo.ExcelRowNumber.Value);
                if (oGLDataWriteOnOffInfo.MatchSetMatchingSourceDataImportID.HasValue)
                    dr["MatchSetMatchingSourceDataImportID"] = Convert.ToInt64(oGLDataWriteOnOffInfo.MatchSetMatchingSourceDataImportID.Value);
                dt.Rows.Add(dr);
                dr = null;
            }
            return dt;
        }

        internal static DataTable GetMatchSetGLDataRecItemTable(List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection)
        {
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add("MatchSetSubSetCombinationID", System.Type.GetType("System.Int64"));
            dt.Columns.Add("GLDataRecItemRefID", System.Type.GetType("System.Int64"));
            dt.Columns.Add("RecItemNumber", System.Type.GetType("System.String"));
            dt.Columns.Add("ExcelRowNumber", System.Type.GetType("System.Int64"));
            dt.Columns.Add("MatchSetMatchingSourceDataImportID", System.Type.GetType("System.Int64"));
            DataRow dr;
            foreach (GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo in oGLDataRecurringItemScheduleInfoCollection)
            {
                dr = dt.NewRow();
                if (oGLDataRecurringItemScheduleInfo.MatchSetSubSetCombinationID.HasValue)
                    dr["MatchSetSubSetCombinationID"] = Convert.ToInt64(oGLDataRecurringItemScheduleInfo.MatchSetSubSetCombinationID.Value);
                if (oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleID.HasValue)
                    dr["GLDataRecItemRefID"] = Convert.ToInt64(oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleID.Value);
                dr["RecItemNumber"] = oGLDataRecurringItemScheduleInfo.RecItemNumber;
                if (oGLDataRecurringItemScheduleInfo.ExcelRowNumber.HasValue)
                    dr["ExcelRowNumber"] = Convert.ToInt64(oGLDataRecurringItemScheduleInfo.ExcelRowNumber.Value);
                if (oGLDataRecurringItemScheduleInfo.MatchSetMatchingSourceDataImportID.HasValue)
                    dr["MatchSetMatchingSourceDataImportID"] = Convert.ToInt64(oGLDataRecurringItemScheduleInfo.MatchSetMatchingSourceDataImportID.Value);
                dt.Rows.Add(dr);
                dr = null;
            }
            return dt;
        }

        internal static DataTable ConvertGLDataWriteOnOffRecItemListToDataTable(List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection)
        {
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add("Amount", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("AmountInBaseCurrency", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("AmountInReportingCurrency", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OpenDate", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("Comments", System.Type.GetType("System.String"));
            dt.Columns.Add("LocalCurrency", System.Type.GetType("System.String"));
            dt.Columns.Add("ExcelRowNumber", System.Type.GetType("System.Int64"));
            dt.Columns.Add("MatchSetMatchingSourceDataImportID", System.Type.GetType("System.Int64"));
            dt.Columns.Add("IndexID", System.Type.GetType("System.Int16"));
            dt.Columns.Add("WriteOnOffID", System.Type.GetType("System.Int16"));
            
            DataRow dr;

            foreach (GLDataWriteOnOffInfo oGLDataWriteOnOffInfo in oGLDataWriteOnOffInfoCollection)
            {
                dr = dt.NewRow();
                if (oGLDataWriteOnOffInfo.Amount.HasValue)
                    dr["Amount"] = oGLDataWriteOnOffInfo.Amount.Value;
                if (oGLDataWriteOnOffInfo.AmountBaseCurrency.HasValue)
                    dr["AmountInBaseCurrency"] = oGLDataWriteOnOffInfo.AmountBaseCurrency;
                if (oGLDataWriteOnOffInfo.AmountReportingCurrency.HasValue)
                    dr["AmountInReportingCurrency"] = oGLDataWriteOnOffInfo.AmountReportingCurrency;
                if (oGLDataWriteOnOffInfo.OpenDate.HasValue)
                    dr["OpenDate"] = oGLDataWriteOnOffInfo.OpenDate;
                if (oGLDataWriteOnOffInfo.ExcelRowNumber.HasValue)
                    dr["ExcelRowNumber"] = Convert.ToInt64(oGLDataWriteOnOffInfo.ExcelRowNumber.Value);
                if (oGLDataWriteOnOffInfo.MatchSetMatchingSourceDataImportID.HasValue)
                    dr["MatchSetMatchingSourceDataImportID"] = Convert.ToInt64(oGLDataWriteOnOffInfo.MatchSetMatchingSourceDataImportID.Value);
                if (oGLDataWriteOnOffInfo.IndexID.HasValue)
                    dr["IndexID"] = Convert.ToInt16(oGLDataWriteOnOffInfo.IndexID.Value);
                if (oGLDataWriteOnOffInfo.WriteOnOffID.HasValue)
                    dr["WriteOnOffID"] = Convert.ToInt16(oGLDataWriteOnOffInfo.WriteOnOffID.Value);

                dr["Comments"] = oGLDataWriteOnOffInfo.Comments;
                dr["LocalCurrency"] = oGLDataWriteOnOffInfo.LocalCurrencyCode;
                dt.Rows.Add(dr);
                dr = null;
            }

            return dt;
        }
    }


}
