using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SkyStem.ART.Client.Model.BulkExportExcel;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices.BulkExportToExcel;
using System.Text;
using SkyStem.ART.Client.IServices;
using System.IO;
using Telerik.Web.UI;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Data;
using System.Web.SessionState;
using SkyStem.ART.Client.Model.GridInfo;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Language.LanguageUtility;
using System.Configuration;
using SkyStem.ART.Shared.Data;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Shared.Utility;

namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for RequestHelper
    /// </summary>
    public class RequestHelper
    {
        public RequestHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void AddColumnsToExportTable(DataTable dtExportAsXML, Dictionary<string, string> columnNames)
        {
            foreach (string keyValue in columnNames.Values)
            {
                dtExportAsXML.Columns.Add(keyValue);
            }
        }

        public static void BindDataTableToBulkUpdate(Dictionary<string, string> objAccountHdr, DataTable dtExport, List<GridDataItemBindingInfo> gridBindingInfo)
        {
            ISubledger oSubledger = RemotingHelper.GetSubledgerObject();
            List<SubledgerSourceInfo> oSubledgerSourceInfoCollection = LanguageHelper.TranslateLabelSubledgerSource(oSubledger.SelectAllByCompanyID(SessionHelper.CurrentCompanyID.Value, Helper.GetAppUserInfo()));

            DataRow drExport = dtExport.NewRow();

            foreach (GridDataItemBindingInfo objBindingInfo in gridBindingInfo)
            {
                string headerTextValue = objBindingInfo.HeaderText;
                if (objBindingInfo.UniqueName != "RecFrequency")
                {
                    string DataValue = ((KeyValuePair<string, string>)objAccountHdr.First(p => p.Key == objBindingInfo.UniqueName)).Value;
                    drExport[headerTextValue] = DataValue == null ? String.Empty : HttpUtility.HtmlDecode(DataValue);
                }
            }
            dtExport.Rows.Add(drExport);
        }

        public static BulkExportToExcelInfo ConvertUserDataToBulkInfoObject(ARTEnums.RequestType eRequestType, string userXMLData, ARTEnums.Grid GridType)
        {
            UserHdrInfo currentUserInfo = SessionHelper.GetCurrentUser();
            BulkExportToExcelInfo objBulkExportInfo = new BulkExportToExcelInfo();
            string messageDescription = string.Empty;
            string subject = string.Empty;
            int? gridId = Convert.ToInt32(GridType);

            string replacement = GetReplacement(eRequestType, GridType);

            subject = string.Format(LanguageUtil.GetValue(2816), replacement);
            messageDescription = string.Format(LanguageUtil.GetValue(2531), replacement);

            objBulkExportInfo.AddedBy = currentUserInfo.LoginID;
            objBulkExportInfo.Data = userXMLData;
            objBulkExportInfo.DateAdded = DateTime.Now;
            objBulkExportInfo.DateRevised = DateTime.Now;

            StringBuilder mailBody = new StringBuilder();
            mailBody.Append(Helper.GetLabelIDValue(1845));
            mailBody.Append(" ");
            mailBody.Append(currentUserInfo.FirstName);
            mailBody.Append("<br>");
            mailBody.Append(messageDescription);
            mailBody.Append("<br>");
            mailBody.Append(MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, currentUserInfo.EmailID));

            objBulkExportInfo.EmailBody = mailBody.ToString(); //"PFA";
            objBulkExportInfo.EmailSubject = subject;//"Account Mass and Bulk Update Export";
            objBulkExportInfo.FinalMessage = "";
            objBulkExportInfo.FromEmailID = currentUserInfo.EmailID;
            objBulkExportInfo.GridID = gridId;
            objBulkExportInfo.IsActive = true;
            objBulkExportInfo.RecperiodID = SessionHelper.CurrentReconciliationPeriodID;
            objBulkExportInfo.RequestTypeID = (short)eRequestType;
            objBulkExportInfo.RevisedBy = currentUserInfo.LoginID;
            objBulkExportInfo.RoleID = SessionHelper.CurrentRoleID;
            objBulkExportInfo.LanguageID = SessionHelper.GetUserLanguage();
            objBulkExportInfo.StatusID = 1;
            objBulkExportInfo.ToEmailID = currentUserInfo.EmailID;
            objBulkExportInfo.UserID = currentUserInfo.UserID;

            return objBulkExportInfo;
        }

        public int StartSaveBulkExport(ExRadGrid exportGrid, ARTEnums.Grid GridType, bool isCustomisationEnabled, AccountSearchCriteria oAccountSearchCriteria)
        {
            int SearchCount = 0;
            //AccountSearchCriteria oAccountSearchCriteria = (AccountSearchCriteria)HttpContext.Current.Session[SessionConstants.ACCOUNT_SEARCH_CRITERIA];
            IBulkExportToExcel objExportToExcel = RemotingHelper.GetBulkExportObject();

            DataSet dsExcelExport = new DataSet();
            DataTable dtRequestHdr = new DataTable(SearchAndEmailConstants.HeaderTableName);
            dsExcelExport.Tables.Add(dtRequestHdr);
            dtRequestHdr.Columns.Add(new DataColumn(SearchAndEmailConstants.HeaderFields.FILENAME, typeof(string)));
            DataRow dr = dtRequestHdr.NewRow();
            dtRequestHdr.Rows.Add(dr);
            dr[SearchAndEmailConstants.HeaderFields.FILENAME] = GetDownloadFileName(ARTEnums.RequestType.ExportToExcel, GridType);

            DataTable dtExportAsXML = new DataTable(SearchAndEmailConstants.DetailTableName);
            List<GridDataItemBindingInfo> gridItemInfoList = new List<GridDataItemBindingInfo>();

            GridHelper.ShowHideColumns(3, exportGrid.MasterTableView, SessionHelper.CurrentCompanyID, GridType, isCustomisationEnabled);

            foreach (GridColumn column in exportGrid.Columns)
            {
                if (column.Visible)
                {
                    if (column.ColumnType == "ExGridTemplateColumn")
                    {
                        if ((!(string.IsNullOrEmpty(((ExGridTemplateColumn)(column)).LabelID.ToString()))))
                        {
                            GridDataItemBindingInfo gridDataIteminfo = new GridDataItemBindingInfo();
                            gridDataIteminfo.UniqueName = column.UniqueName;
                            if (!string.IsNullOrEmpty(column.HeaderText))
                            {
                                gridDataIteminfo.HeaderText = column.HeaderText;
                            }
                            else
                            {
                                gridDataIteminfo.HeaderText = LanguageUtil.GetValue(((ExGridTemplateColumn)(column)).LabelID);
                            }
                            gridDataIteminfo.Value = "";
                            gridDataIteminfo.DataType = column.DataType;
                            gridItemInfoList.Add(gridDataIteminfo);
                        }
                    }
                }
            }

            foreach (GridDataItemBindingInfo itemInfo in gridItemInfoList)
            {
                dtExportAsXML.Columns.Add(itemInfo.HeaderText);
            }

            List<AccountHdrInfo> accountHdrInfo = objExportToExcel.GetAccountDetails(oAccountSearchCriteria, Helper.GetAppUserInfo());
            if (accountHdrInfo.Count > 0)
            {
                SearchCount = accountHdrInfo.Count;
                accountHdrInfo = LanguageHelper.TranslateLabelsAccountHdr(accountHdrInfo);
                foreach (AccountHdrInfo accountInfo in accountHdrInfo)
                {
                    Dictionary<string, string> dictGridDataItemValues = GridHelper.GetAccountBulkUpdataForBinding(accountInfo);
                    BindDataTableToBulkUpdate(dictGridDataItemValues, dtExportAsXML, gridItemInfoList);
                }

                dsExcelExport.Tables.Add(dtExportAsXML);
                SaveRequest(ARTEnums.RequestType.ExportToExcel, GridType, dsExcelExport);
            }

            return SearchCount;
        }

        public static void SaveRequest(ARTEnums.RequestType eRequestType, ARTEnums.Grid eGridType, DataSet oRequestDataSet)
        {
            string xmlData = oRequestDataSet.GetXml();
            IBulkExportToExcel objExportToExcel = RemotingHelper.GetBulkExportObject();

            BulkExportToExcelInfo objBulkExportInfo = RequestHelper.ConvertUserDataToBulkInfoObject(eRequestType, xmlData, eGridType);
            objExportToExcel.SaveBulkExportToExcelDetails(objBulkExportInfo, Helper.GetAppUserInfo());
        }

        public static string GetGridName(ARTEnums.Grid gridType)
        {
            string gridName = string.Empty;
            switch (gridType)
            {
                case ARTEnums.Grid.AccountOwnership:
                    gridName = Helper.GetLabelIDValue(1212);
                    break;
                case ARTEnums.Grid.AccountProfileMassAndBulkUpdate:
                    gridName = Helper.GetLabelIDValue(1214);
                    break;
            }
            return gridName;
        }

        public static string GetReplacement(ARTEnums.RequestType eRequestType, ARTEnums.Grid gridType)
        {
            string replacement = string.Empty;
            string gridName = string.Empty;
            if (eRequestType == ARTEnums.RequestType.ExportToExcel)
            {
                gridName = RequestHelper.GetGridName(gridType);
                replacement = string.Format(LanguageUtil.GetValue(2817), gridName);
            }
            else if (eRequestType == ARTEnums.RequestType.DownloadAllRecFormsDetailed)
            {
                replacement = LanguageUtil.GetValue(2822);
            }
            else if (eRequestType == ARTEnums.RequestType.DownloadSelectedRecFormsDetailed)
            {
                replacement = LanguageUtil.GetValue(2837);
            }
            else if (eRequestType == ARTEnums.RequestType.CreateBinders)
            {
                replacement = LanguageUtil.GetValue(2814);
            }
            return replacement;
        }

        public static string GetDownloadFileName(ARTEnums.RequestType eRequestType, ARTEnums.Grid gridType)
        {
            string fileName = string.Empty;
            if (eRequestType == ARTEnums.RequestType.ExportToExcel)
            {
                switch (gridType)
                {
                    case ARTEnums.Grid.AccountOwnership:
                        fileName = Helper.GetLabelIDValue(1212);
                        break;
                    case ARTEnums.Grid.AccountProfileMassAndBulkUpdate:
                        fileName = Helper.GetLabelIDValue(1214);
                        break;
                }
            }
            else if (eRequestType == ARTEnums.RequestType.DownloadAllRecFormsDetailed)
            {
                fileName = LanguageUtil.GetValue(2809);
            }
            return fileName + "_" + Guid.NewGuid();
        }

        public static DataSet CreateDataSetForDownloadAllRequest(List<GLDataHdrInfo> oGLDataHdrInfoList)
        {
            DataSet oDataSet = new DataSet();
            DataTable dtRequestHdr = new DataTable(DownloadAllRecsConstants.HeaderTableName);
            dtRequestHdr.Columns.Add(DownloadAllRecsConstants.HeaderFields.FILENAME, typeof(System.String));
            dtRequestHdr.Columns.Add(DownloadAllRecsConstants.HeaderFields.RECPERIODID, typeof(System.Int32));
            dtRequestHdr.Columns.Add(DownloadAllRecsConstants.HeaderFields.LANGUAGEID, typeof(System.Int32));
            dtRequestHdr.Columns.Add(DownloadAllRecsConstants.HeaderFields.COMPANYID, typeof(System.Int32));
            dtRequestHdr.Columns.Add(DownloadAllRecsConstants.HeaderFields.DEFAULTLANGUAGEID, typeof(System.Int32));
            dtRequestHdr.Columns.Add(DownloadAllRecsConstants.HeaderFields.USERID, typeof(System.Int32));
            dtRequestHdr.Columns.Add(DownloadAllRecsConstants.HeaderFields.ROLEID, typeof(System.Int16));
            dtRequestHdr.Columns.Add(DownloadAllRecsConstants.HeaderFields.ISDUALREVIEWENABLED, typeof(System.Boolean));
            dtRequestHdr.Columns.Add(DownloadAllRecsConstants.HeaderFields.ISCERTIFICATIONENABLED, typeof(System.Boolean));
            dtRequestHdr.Columns.Add(DownloadAllRecsConstants.HeaderFields.ISMATERIALITYENABLED, typeof(System.Boolean));
            dtRequestHdr.Columns.Add(DownloadAllRecsConstants.HeaderFields.CERTIFICATIONTYPEID, typeof(System.Int16));
            dtRequestHdr.Columns.Add(DownloadAllRecsConstants.HeaderFields.PREPARERATTRIBUTEID, typeof(System.Int32));
            dtRequestHdr.Columns.Add(DownloadAllRecsConstants.HeaderFields.REVIEWERATTRIBUTEID, typeof(System.Int32));
            dtRequestHdr.Columns.Add(DownloadAllRecsConstants.HeaderFields.APPROVERATTRIBUTEID, typeof(System.Int32));
            dtRequestHdr.Columns.Add(DownloadAllRecsConstants.HeaderFields.ISQUALITYSCOREENABLED, typeof(System.Boolean));
            dtRequestHdr.Columns.Add(DownloadAllRecsConstants.HeaderFields.ISREVIEWNOTESENABLED, typeof(System.Boolean));
            dtRequestHdr.Columns.Add(DownloadAllRecsConstants.HeaderFields.LANGUAGEINFO, typeof(System.String));
            oDataSet.Tables.Add(dtRequestHdr);
            DataRow dr = dtRequestHdr.NewRow();
            dtRequestHdr.Rows.Add(dr);
            dr[DownloadAllRecsConstants.HeaderFields.FILENAME] = GetDownloadFileName(ARTEnums.RequestType.DownloadAllRecFormsDetailed, ARTEnums.Grid.AccountViewer);
            dr[DownloadAllRecsConstants.HeaderFields.RECPERIODID] = SessionHelper.CurrentReconciliationPeriodID.Value;
            dr[DownloadAllRecsConstants.HeaderFields.LANGUAGEID] = SessionHelper.GetUserLanguage();
            dr[DownloadAllRecsConstants.HeaderFields.COMPANYID] = SessionHelper.CurrentCompanyID.Value;
            dr[DownloadAllRecsConstants.HeaderFields.DEFAULTLANGUAGEID] = AppSettingHelper.GetDefaultLanguageID();
            dr[DownloadAllRecsConstants.HeaderFields.USERID] = SessionHelper.CurrentUserID.Value;
            dr[DownloadAllRecsConstants.HeaderFields.ROLEID] = SessionHelper.CurrentRoleID.Value;
            dr[DownloadAllRecsConstants.HeaderFields.ISDUALREVIEWENABLED] = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview);
            dr[DownloadAllRecsConstants.HeaderFields.ISCERTIFICATIONENABLED] = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CertificationActivation);
            dr[DownloadAllRecsConstants.HeaderFields.ISMATERIALITYENABLED] = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MaterialitySelection);
            dr[DownloadAllRecsConstants.HeaderFields.CERTIFICATIONTYPEID] = (short)WebEnums.CertificationType.Certification;
            dr[DownloadAllRecsConstants.HeaderFields.PREPARERATTRIBUTEID] = (short)ARTEnums.AccountAttribute.Preparer;
            dr[DownloadAllRecsConstants.HeaderFields.REVIEWERATTRIBUTEID] = (short)ARTEnums.AccountAttribute.Reviewer;
            dr[DownloadAllRecsConstants.HeaderFields.APPROVERATTRIBUTEID] = (short)ARTEnums.AccountAttribute.Approver;
            if (SessionHelper.CurrentRoleID != (short)ARTEnums.UserRole.AUDIT || Helper.IsQualityScoreEnabled())
                dr[DownloadAllRecsConstants.HeaderFields.ISQUALITYSCOREENABLED] = Helper.IsFeatureActivated(WebEnums.Feature.QualityScore, SessionHelper.CurrentReconciliationPeriodID);
            else
                dr[DownloadAllRecsConstants.HeaderFields.ISQUALITYSCOREENABLED] = false;
            if (SessionHelper.CurrentRoleID != (short)ARTEnums.UserRole.AUDIT || Helper.IsReviewNotesEnabled())
                dr[DownloadAllRecsConstants.HeaderFields.ISREVIEWNOTESENABLED] = true;
            else
                dr[DownloadAllRecsConstants.HeaderFields.ISREVIEWNOTESENABLED] = false;
            dr[DownloadAllRecsConstants.HeaderFields.LANGUAGEINFO] = System.Threading.Thread.CurrentThread.CurrentCulture.Name;

            DataTable dtRequestDetail = new DataTable(DownloadAllRecsConstants.DetailTableName);
            dtRequestDetail.Columns.Add(DownloadAllRecsConstants.DetailFields.GLDATAID, typeof(System.Int64));
            dtRequestDetail.Columns.Add(DownloadAllRecsConstants.DetailFields.ACCOUNTID, typeof(System.Int64));
            dtRequestDetail.Columns.Add(DownloadAllRecsConstants.DetailFields.NETACCOUNTID, typeof(System.Int32));
            dtRequestDetail.Columns.Add(DownloadAllRecsConstants.DetailFields.RECONCILIATIONTEMPLATEID, typeof(System.Int32));
            dtRequestDetail.Columns.Add(DownloadAllRecsConstants.DetailFields.ACCOUNTNAME, typeof(System.String));
            oDataSet.Tables.Add(dtRequestDetail);

            oGLDataHdrInfoList = LanguageHelper.TranslateLabelsGLData(oGLDataHdrInfoList);

            foreach (GLDataHdrInfo oGLDataHdrInfo in oGLDataHdrInfoList)
            {
                if (oGLDataHdrInfo.ReconciliationTemplateID.HasValue)
                {
                    DataRow drDetail = dtRequestDetail.NewRow();
                    dtRequestDetail.Rows.Add(drDetail);
                    drDetail[DownloadAllRecsConstants.DetailFields.GLDATAID] = oGLDataHdrInfo.GLDataID;
                    if (oGLDataHdrInfo.AccountID.HasValue)
                        drDetail[DownloadAllRecsConstants.DetailFields.ACCOUNTID] = oGLDataHdrInfo.AccountID.HasValue;
                    else
                        drDetail[DownloadAllRecsConstants.DetailFields.ACCOUNTID] = "0";
                    if (oGLDataHdrInfo.NetAccountID.HasValue)
                        drDetail[DownloadAllRecsConstants.DetailFields.NETACCOUNTID] = oGLDataHdrInfo.NetAccountID;
                    else
                        drDetail[DownloadAllRecsConstants.DetailFields.NETACCOUNTID] = "0";
                    drDetail[DownloadAllRecsConstants.DetailFields.RECONCILIATIONTEMPLATEID] = oGLDataHdrInfo.ReconciliationTemplateID;
                    drDetail[DownloadAllRecsConstants.DetailFields.ACCOUNTNAME] = Helper.GetAccountEntityStringToDisplay(oGLDataHdrInfo);
                }
            }
            return oDataSet;
        }

        public static List<BulkExportToExcelInfo> GetRequests(List<short> RequestTypeList)
        {
            List<BulkExportToExcelInfo> oBulkExportToExcelInfoCollection = null;
            IBulkExportToExcel oBulkExportToExcelClient = RemotingHelper.GetBulkExportObject();
            oBulkExportToExcelInfoCollection = oBulkExportToExcelClient.GetRequests(SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, RequestTypeList, Helper.GetAppUserInfo());
            LanguageHelper.TranslateLabelRequest(oBulkExportToExcelInfoCollection);
            return oBulkExportToExcelInfoCollection;
        }

        public static List<BulkExportToExcelInfo> GetAllRecBinders(List<short> RequestTypeList)
        {
            List<BulkExportToExcelInfo> oBulkExportToExcelInfoCollection = null;
            IBulkExportToExcel oBulkExportToExcelClient = RemotingHelper.GetBulkExportObject();
            oBulkExportToExcelInfoCollection = oBulkExportToExcelClient.GetAllRecBinders(SessionHelper.CurrentCompanyID, SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, RequestTypeList, Helper.GetAppUserInfo());
            LanguageHelper.TranslateLabelRequest(oBulkExportToExcelInfoCollection);
            return oBulkExportToExcelInfoCollection;
        }

        public static GridGroupByExpression[] GetGridGroupByExpressionForERecBinders()
        {
            GridGroupByExpression[] oGridGroupByExpressionList = new GridGroupByExpression[2];
            GridGroupByExpression expressionFY = new GridGroupByExpression();
            oGridGroupByExpressionList[0] = expressionFY;

            GridGroupByField gridGroupByField = new GridGroupByField();
            // SelectFields values (appear in header)

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "FinancialYear";
            gridGroupByField.HeaderText = LanguageUtil.GetValue(2015);
            gridGroupByField.FormatString = "{0}";
            expressionFY.SelectFields.Add(gridGroupByField);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "FinancialYear";
            expressionFY.GroupByFields.Add(gridGroupByField);

            GridGroupByExpression expressionPED = new GridGroupByExpression();
            oGridGroupByExpressionList[1] = expressionPED;

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "MonthYear";
            gridGroupByField.HeaderText = " ";//LanguageUtil.GetValue(1489);
            //gridGroupByField.FormatString = "<strong>{0}</strong>";
            expressionPED.SelectFields.Add(gridGroupByField);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "PeriodEndDate";
            expressionPED.GroupByFields.Add(gridGroupByField);

            return oGridGroupByExpressionList;
        }

        public static void BindCommonFields(WebEnums.ARTPages ePage, Telerik.Web.UI.GridItemEventArgs e)
        {
            BulkExportToExcelInfo oBulkExportToExcelInfo = (BulkExportToExcelInfo)e.Item.DataItem;
            ExLabel lblRequestType = (ExLabel)e.Item.FindControl("lblRequestType");
            ExLabel lblPageGridName = (ExLabel)e.Item.FindControl("lblPageGridName");
            ExLabel lblFileName = (ExLabel)e.Item.FindControl("lblFileName");
            ExLabel lblStatus = (ExLabel)e.Item.FindControl("lblStatus");
            ExLabel lblDate = (ExLabel)e.Item.FindControl("lblDate");
            ExImageButton imgFileTypeExcel = (ExImageButton)e.Item.FindControl("imgFileTypeExcel");
            ExImageButton imgFileTypeZip = (ExImageButton)e.Item.FindControl("imgFileTypeZip");
            ExLabel lblAddedBy = (ExLabel)e.Item.FindControl("lblAddedBy");
            ExLabel lblStatusMessage = (ExLabel)e.Item.FindControl("lblStatusMessage");


            SetLabelText(lblRequestType, Helper.GetDisplayStringValue(oBulkExportToExcelInfo.RequestType));
            SetLabelText(lblPageGridName, Helper.GetDisplayStringValue(oBulkExportToExcelInfo.GridName));
            SetLabelText(lblFileName, Helper.GetDisplayStringValue(oBulkExportToExcelInfo.FileName));
            SetLabelText(lblStatus, Helper.GetDisplayStringValue(oBulkExportToExcelInfo.RequestStatus));
            SetLabelText(lblDate, Helper.GetDisplayDateTime(oBulkExportToExcelInfo.DateAdded));
            SetLabelText(lblAddedBy, Helper.GetDisplayStringValue(oBulkExportToExcelInfo.AddedByUserName));
            SetLabelText(lblStatusMessage, Helper.GetDisplayStringValue(oBulkExportToExcelInfo.StatusMessage));

            //string url = "DownloadAttachment.aspx?" + QueryStringConstants.FILE_PATH + "=" + HttpContext.Current.Server.UrlEncode(SharedHelper.GetDisplayFilePath(oBulkExportToExcelInfo.PhysicalPath)) + "&" + QueryStringConstants.FROM_PAGE + "=" + (short)ePage;
            string url = string.Format("Downloader?{0}={1}&", QueryStringConstants.HANDLER_ACTION, (Int32)WebEnums.HandlerActionType.DownloadRequestFile);
            url += "&" + QueryStringConstants.REQUEST_ID + "=" + oBulkExportToExcelInfo.RequestID.GetValueOrDefault()
            + "&" + QueryStringConstants.REQUEST_TYPE_ID + "=" + oBulkExportToExcelInfo.RequestTypeID.GetValueOrDefault();
           
            if (imgFileTypeExcel != null)
                imgFileTypeExcel.OnClientClick = "document.location.href = '" + url + "';return false;";
            if (imgFileTypeZip != null)
                imgFileTypeZip.OnClientClick = "document.location.href = '" + url + "';return false;";
            if (oBulkExportToExcelInfo.StatusID.HasValue)
            {
                ARTEnums.RequestStatus eRequestStatus = (ARTEnums.RequestStatus)System.Enum.Parse(typeof(ARTEnums.RequestStatus), oBulkExportToExcelInfo.StatusID.Value.ToString());

                switch (eRequestStatus)
                {
                    case ARTEnums.RequestStatus.Success:
                        ExImage imgSuccess = (ExImage)e.Item.FindControl("imgSuccess");
                        if (imgSuccess != null)
                            imgSuccess.Visible = true;
                        break;
                    case ARTEnums.RequestStatus.Error:
                        ExImage imgFailure = (ExImage)e.Item.FindControl("imgFailure");
                        if (imgFailure != null)
                            imgFailure.Visible = true;
                        break;
                    case ARTEnums.RequestStatus.Processing:
                        ExImage imgProcessing = (ExImage)e.Item.FindControl("imgProcessing");
                        if (imgProcessing != null)
                            imgProcessing.Visible = true;
                        break;
                    case ARTEnums.RequestStatus.Submitted:
                        ExImage imgToBeProcessed = (ExImage)e.Item.FindControl("imgToBeProcessed");
                        if (imgToBeProcessed != null)
                            imgToBeProcessed.Visible = true;
                        break;

                }
            }
            if (oBulkExportToExcelInfo.RequestTypeID.HasValue && !string.IsNullOrEmpty(oBulkExportToExcelInfo.PhysicalPath))
            {
                if (imgFileTypeExcel != null)
                    imgFileTypeExcel.Visible = (oBulkExportToExcelInfo.RequestTypeID.Value == (short)ARTEnums.RequestType.ExportToExcel);
                if (imgFileTypeZip != null)
                    imgFileTypeZip.Visible = ((oBulkExportToExcelInfo.RequestTypeID.Value == (short)ARTEnums.RequestType.DownloadAllRecFormsDetailed)
                                            || (oBulkExportToExcelInfo.RequestTypeID.Value == (short)ARTEnums.RequestType.CreateBinders)
                                            || (oBulkExportToExcelInfo.RequestTypeID.Value == (short)ARTEnums.RequestType.DownloadSelectedRecFormsDetailed));
            }
            else
            {
                if (imgFileTypeExcel != null)
                    imgFileTypeExcel.Visible = false;
                if (imgFileTypeZip != null)
                    imgFileTypeZip.Visible = false;
            }
        }

        public static void SetLabelText(ExLabel lbl, string val)
        {
            if (lbl != null)
                lbl.Text = val;
        }

        public static void DeleteRequest(List<int> oRequestIDCollection)
        {
            List<DataImportHdrInfo> oDataImportHdrInfoList = null;
            if (oRequestIDCollection.Count > 0 && SessionHelper.CurrentCompanyID.HasValue)
            {
                IBulkExportToExcel oBulkExportToExcelClient = RemotingHelper.GetBulkExportObject();
                oDataImportHdrInfoList = oBulkExportToExcelClient.DeleteRequests(oRequestIDCollection, SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentUserLoginID, DateTime.Now, Helper.GetAppUserInfo());
            }
            if (oDataImportHdrInfoList != null && oDataImportHdrInfoList.Count > 0)
            {
                foreach (DataImportHdrInfo oDataImportHdrInfo in oDataImportHdrInfoList)
                {
                    DataImportHelper.DeleteFile(oDataImportHdrInfo.PhysicalPath);
                }
            }
        }
    }
}
