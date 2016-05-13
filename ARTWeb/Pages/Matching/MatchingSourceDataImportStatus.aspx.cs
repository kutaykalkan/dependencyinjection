using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.ART.Web.Classes;
using Telerik.Web.UI;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.Client.Model.Matching;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using System.IO;
using SkyStem.ART.Client.Model;

public partial class Pages_Matching_MatchingSourceDataImportStatus : PageBaseMatching
{
    bool isExportPDF;
    bool isExportExcel;

    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase oMaster = (MasterPageBase)this.Master.Master;
        oMaster.ReconciliationPeriodChangedEventHandler += new EventHandler(oMaster_ReconciliationPeriodChangedEventHandler);
        GridHelper.SetRecordCount(rgMatchingSourceDataImport);
    }
    void oMaster_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        rgMatchingSourceDataImport.MasterTableView.CurrentPageIndex = 0;
        rgMatchingSourceDataImport.Rebind();
        SetBADDl();
        //LoadMatchingSourceDataImport();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.SetPageTitle(this, 2213);
        Helper.SetBreadcrumbs(this, 1071, 2234, 2185, 2213);
        if (!Page.IsPostBack)
        {
            isExportPDF = false;
            isExportExcel = false;
            // Add Default Sort as Import Data, Desc
            GridSortExpression oGridSortExpression = new GridSortExpression();
            oGridSortExpression.FieldName = "DateAdded";
            oGridSortExpression.SortOrder = GridSortOrder.Descending;
            rgMatchingSourceDataImport.MasterTableView.SortExpressions.AddSortExpression(oGridSortExpression);

            // Add Default Sort as Import Data, Desc
            rgMatchingSourceDataImport.MasterTableView.SortExpressions.AddSortExpression(oGridSortExpression);
            this.ReturnUrl = Helper.ReturnURL(this.Page);
        }
        if ((SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.SYSTEM_ADMIN ))
        {
            pnlBusinessAdminDDL.Visible = true;
        }
        else
        {
            pnlBusinessAdminDDL.Visible = false;
        }
        Sel.Value = "";
    }
    protected void ddlBA_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlBA.SelectedValue == "-2")
        {

            LoadMatchingSourceDataImport(SessionHelper.CurrentUserID ,SessionHelper.CurrentRoleID,true );
        }
        else
        {
            LoadMatchingSourceDataImport(Convert.ToInt32(this.ddlBA.SelectedValue), (short)WebEnums.UserRole.BUSINESS_ADMIN, true);

        }

       
    }
    protected void SetBADDl()
    {
        try
        {
            List<UserHdrInfo> oUserHdrInfoCollection = null;
            oUserHdrInfoCollection = CacheHelper.SelectAllBusinessAdminForCurrentCompany();
            ddlBA.DataSource = oUserHdrInfoCollection;
            ddlBA.DataTextField = "Name";
            ddlBA.DataValueField = "UserID";
            ddlBA.DataBind();
            ListControlHelper.AddListItemForSelectOne(ddlBA);
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    private void LoadMatchingSourceDataImport(int? CurrentUserID, short? RoleID , bool FromDdl)
    {
        try
        {
            MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
            oMatchingParamInfo.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
            oMatchingParamInfo.RoleID = RoleID;
            oMatchingParamInfo.UserID = CurrentUserID;
            //oMatchingParamInfo.RoleID = SessionHelper.CurrentRoleID;
            //oMatchingParamInfo.UserID = ;
            oMatchingParamInfo.ShowOnlySuccessfulMatchingSourceDataImport = false;
            oMatchingParamInfo.IsHidden = chkShowHiddenRows.Checked;
            List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrInfo = null;

            if (SessionHelper.CurrentReconciliationPeriodID != null)
            {
                oMatchingSourceDataImportHdrInfo = MatchingHelper.GetMatchingSources(oMatchingParamInfo);
                for (int i = 0; i < oMatchingSourceDataImportHdrInfo.Count; i++)
                {
                    oMatchingSourceDataImportHdrInfo[i].DataImportStatus = LanguageUtil.GetValue(oMatchingSourceDataImportHdrInfo[i].DataImportStatusLabelID.Value);
                    oMatchingSourceDataImportHdrInfo[i].MatchingSourceTypeName = LanguageUtil.GetValue(oMatchingSourceDataImportHdrInfo[i].MatchingSourceTypeNameLabelID.Value);
                    oMatchingSourceDataImportHdrInfo[i].IsPartofMatchSet = LanguageUtil.GetValue(oMatchingSourceDataImportHdrInfo[i].IsPartofMatchSetLabelID.Value);
                }
            }
            else
                oMatchingSourceDataImportHdrInfo = new List<MatchingSourceDataImportHdrInfo>();

            rgMatchingSourceDataImport.DataSource = oMatchingSourceDataImportHdrInfo;
            if(FromDdl)
            rgMatchingSourceDataImport.DataBind();

            GridHelper.SortDataSource(rgMatchingSourceDataImport.MasterTableView);

        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    public override string GetMenuKey()
    {
        return "MatchingSourceDataImportStatus";
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
            List<Int64> oIDCollection = new List<Int64>();
            foreach (GridDataItem item in rgMatchingSourceDataImport.MasterTableView.GetSelectedItems())
            {
                ExLabel lblMatchSetID = (ExLabel)item.FindControl("lblMatchSetID");
                string PhysicalPath = item["PhysicalPath"].Text;
                if (lblMatchSetID != null && lblMatchSetID.Text != "" && lblMatchSetID.Text!="-")
                {
                    long lngMatchSetID = 0;
                    long.TryParse(lblMatchSetID.Text, out lngMatchSetID);
                    if (lngMatchSetID > 0)
                        throw new ARTException(5000276);
                }
                string MatchingSourceDataImportID = item["MatchingSourceDataImportID"].Text;
                oIDCollection.Add(Convert.ToInt64(MatchingSourceDataImportID));
            }
            if (oIDCollection.Count > 0)
            {
                oMatchingParamInfo.IDList = oIDCollection;
                oMatchingParamInfo.CompanyID = SessionHelper.CurrentCompanyID;
                oMatchingParamInfo.DateRevised = DateTime.Now;
                oMatchingParamInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                bool result=MatchingHelper.DeleteMatchingSourceData(oMatchingParamInfo);
                if (result)
                {
                    foreach (GridDataItem item in rgMatchingSourceDataImport.MasterTableView.GetSelectedItems())
                    {
                        string PhysicalPath = item["PhysicalPath"].Text;
                        if (File.Exists(PhysicalPath))
                            File.Delete(PhysicalPath);
                    }
                    Helper.ShowConfirmationMessage(this, Helper.GetLabelIDValue(2214));
                }
                else
                    Helper.ShowErrorMessage(this, Helper.GetLabelIDValue(5000045));

                rgMatchingSourceDataImport.Rebind();
            }
            else
            {
                Helper.ShowErrorMessage(this, Helper.GetLabelIDValue(5000260));
            }
            
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    protected void rgMatchingSourceDataImport_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {

        if (this.ddlBA.SelectedValue != "-2" && this.ddlBA.SelectedValue != "")
        {
            LoadMatchingSourceDataImport(Convert.ToInt32(this.ddlBA.SelectedValue), (short)WebEnums.UserRole.BUSINESS_ADMIN,false );
        }
        else
        {
            LoadMatchingSourceDataImport(SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, false);
 

        }
    }
    protected void rgMatchingSourceDataImport_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            CheckBox checkBox = (CheckBox)item["CheckboxSelectColumn"].Controls[0];
            MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = (MatchingSourceDataImportHdrInfo)e.Item.DataItem;
            BindCommonFields(e);
            long MatchSetID = oMatchingSourceDataImportHdrInfo.MatchSetID.Value;
            short status = oMatchingSourceDataImportHdrInfo.DataImportStatusID.Value;

            ExImageButton imgbtnShowRows = (ExImageButton)e.Item.FindControl("imgbtnShowRows");
            ExImageButton imgbtnHideRows = (ExImageButton)e.Item.FindControl("imgbtnHideRows");
            imgbtnShowRows.ToolTip = LanguageUtil.GetValue(2108);
            imgbtnHideRows.ToolTip = LanguageUtil.GetValue(2107);
            imgbtnShowRows.Visible = (Boolean)oMatchingSourceDataImportHdrInfo.IsHidden;
            imgbtnHideRows.Visible = !((Boolean)oMatchingSourceDataImportHdrInfo.IsHidden);

            if (status == (short)WebEnums.DataImportStatus.Draft || status == (short)WebEnums.DataImportStatus.Success
                || status == (short)WebEnums.DataImportStatus.Warning || status == (short)WebEnums.DataImportStatus.Failure
                || MatchSetID <= 0)
            {
                if (checkBox != null)
                {
                    checkBox.Enabled = true;
                }
            }
            else
            {
                if (checkBox != null)
                {
                    checkBox.Enabled = false;
                    Sel.Value += e.Item.ItemIndex.ToString() + ":";
                }
            }
        }
    }

    private void BindCommonFields(Telerik.Web.UI.GridItemEventArgs e)
    {
        MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = (MatchingSourceDataImportHdrInfo)e.Item.DataItem;
        ExHyperLink hlMatchingSourceName = (ExHyperLink)e.Item.FindControl("hlMatchingSourceName");
        ExHyperLink hlMatchingSourceType = (ExHyperLink)e.Item.FindControl("hlMatchingSourceType");
        ExHyperLink hlStatus = (ExHyperLink)e.Item.FindControl("hlStatus");
        ExHyperLink hlIsPartofMatchSet = (ExHyperLink)e.Item.FindControl("hlIsPartofMatchSet");
        ExHyperLink hlDate = (ExHyperLink)e.Item.FindControl("hlDate");
        ExImageButton imgFileType = (ExImageButton)e.Item.FindControl("imgFileType");
        ExLabel lblMatchSetID = (ExLabel)e.Item.FindControl("lblMatchSetID");


        hlMatchingSourceName.Text = Helper.GetDisplayStringValue(oMatchingSourceDataImportHdrInfo.MatchingSourceName);
        hlMatchingSourceType.Text = Helper.GetDisplayStringValue(oMatchingSourceDataImportHdrInfo.MatchingSourceTypeName);
        hlStatus.Text = Helper.GetDisplayStringValue(LanguageUtil.GetValue(oMatchingSourceDataImportHdrInfo.DataImportStatusLabelID.Value));

        hlIsPartofMatchSet.Text = Helper.GetDisplayStringValue(oMatchingSourceDataImportHdrInfo.IsPartofMatchSet);
        hlDate.Text = Helper.GetDisplayDate(oMatchingSourceDataImportHdrInfo.DateAdded);
        if (oMatchingSourceDataImportHdrInfo.MatchSetID != null)
            lblMatchSetID.Text = Helper.GetDisplayStringValue(oMatchingSourceDataImportHdrInfo.MatchSetID.ToString());
        else
            lblMatchSetID.Text = "0";

        string url = "../DownloadAttachment.aspx?" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(oMatchingSourceDataImportHdrInfo.PhysicalPath);
        imgFileType.OnClientClick = "document.location.href = '" + url + "';return false;";

        WebEnums.DataImportStatus eDataImportStatus = (WebEnums.DataImportStatus)System.Enum.Parse(typeof(WebEnums.DataImportStatus), oMatchingSourceDataImportHdrInfo.DataImportStatusID.Value.ToString());

        switch (eDataImportStatus)
        {
            case WebEnums.DataImportStatus.Success:
                ExImage imgSuccess = (ExImage)e.Item.FindControl("imgSuccess");
                imgSuccess.Visible = true;
                break;

            case WebEnums.DataImportStatus.Failure:
                ExImage imgFailure = (ExImage)e.Item.FindControl("imgFailure");
                imgFailure.Visible = true;
                break;

            case WebEnums.DataImportStatus.Warning:
                ExImage imgWarning = (ExImage)e.Item.FindControl("imgWarning");
                imgWarning.Visible = true;
                break;

            case WebEnums.DataImportStatus.Processing:
                ExImage imgProcessing = (ExImage)e.Item.FindControl("imgProcessing");
                imgProcessing.Visible = true;
                break;

            case WebEnums.DataImportStatus.Submitted:
                ExImage imgToBeProcessed = (ExImage)e.Item.FindControl("imgToBeProcessed");
                imgToBeProcessed.Visible = true;
                break;
            case WebEnums.DataImportStatus.Draft:
                ExImage imgDraft = (ExImage)e.Item.FindControl("imgDraft");
                imgDraft.Visible = true;
                break;
        }

        if ((e.Item as GridDataItem)["MatchingSourceDataImportID"] != null)
            (e.Item as GridDataItem)["MatchingSourceDataImportID"].Text = oMatchingSourceDataImportHdrInfo.MatchingSourceDataImportID.ToString();

        if ((e.Item as GridDataItem)["PhysicalPath"] != null)
            (e.Item as GridDataItem)["PhysicalPath"].Text = oMatchingSourceDataImportHdrInfo.PhysicalPath.ToString();

        if ((e.Item as GridDataItem)["MatchingSourceTypeID"] != null)
            (e.Item as GridDataItem)["MatchingSourceTypeID"].Text = oMatchingSourceDataImportHdrInfo.MatchingSourceTypeID.ToString();
      
        
        // Set the Hyperlink for Next Page
        SetHyperlinkForStatusPage(oMatchingSourceDataImportHdrInfo, hlMatchingSourceName, hlMatchingSourceType, hlStatus, hlIsPartofMatchSet, hlDate);
    }



    private void SetHyperlinkForStatusPage(MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo, ExHyperLink hlMatchingSourceName,
       ExHyperLink hlMatchingSourceType, ExHyperLink hlStatus, ExHyperLink hlIsPartofMatchSet, ExHyperLink hlDate)
    {
        string url = GetUrlForDataImportStatusPage(oMatchingSourceDataImportHdrInfo);
        hlMatchingSourceName.NavigateUrl = url;
        hlMatchingSourceType.NavigateUrl = url;
        hlStatus.NavigateUrl = url;
        hlIsPartofMatchSet.NavigateUrl = url;
        hlDate.NavigateUrl = url;
    }

    private static string GetUrlForDataImportStatusPage(MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo)
    {
        string url = "MatchingDataImportStatusMessages.aspx?" + QueryStringConstants.DATA_IMPORT_ID + "=" + oMatchingSourceDataImportHdrInfo.MatchingSourceDataImportID.ToString();
        return url;
    }

    protected void rgMatchingSourceDataImport_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        rgMatchingSourceDataImport.Rebind();
    }
    protected void rgMatchingSourceDataImport_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_EXCEL_ICON) as ImageButton;
            Helper.RegisterPostBackToControls(this, ibExportToExcel);
        }
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_PDF_ICON) as ImageButton;
            Helper.RegisterPostBackToControls(this, ibExportToExcel);

        }
       
    }
    protected void rgMatchingSourceDataImport_ItemCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                rgMatchingSourceDataImport.MasterTableView.Columns.FindByUniqueName("imgStatus").Visible = false;
                rgMatchingSourceDataImport.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                rgMatchingSourceDataImport.MasterTableView.Columns.FindByUniqueName("DataTypeMapping").Visible = false;
                rgMatchingSourceDataImport.MasterTableView.Columns.FindByUniqueName("FileType").Visible = false;
                GridHelper.ExportGridToPDF(rgMatchingSourceDataImport, 1219);

            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                rgMatchingSourceDataImport.MasterTableView.Columns.FindByUniqueName("imgStatus").Visible = false;
                rgMatchingSourceDataImport.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                rgMatchingSourceDataImport.MasterTableView.Columns.FindByUniqueName("DataTypeMapping").Visible = false;
                rgMatchingSourceDataImport.MasterTableView.Columns.FindByUniqueName("FileType").Visible = false;
               
                GridHelper.ExportGridToExcel(rgMatchingSourceDataImport, 1219);
            }
            if (e.CommandName == TelerikConstants.GridRefreshCommandName)
            {
                rgMatchingSourceDataImport.Rebind();
            }
            if (e.CommandName == WebConstants.DATA_TYPE_MAPPING)
            {
                RedirectToDataTypeMapping(e);
            }
            if (e.CommandName == WebConstants.HIDE_SHOW_ROWS)
            {
                GridDataItem oGridDataItem = (GridDataItem)e.Item;
                long MatchingSourceDataImportID = 0;
                long.TryParse(oGridDataItem["MatchingSourceDataImportID"].Text, out MatchingSourceDataImportID);
                MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
                oMatchingParamInfo.MatchingSourceDataImportID = MatchingSourceDataImportID;
                oMatchingParamInfo.IsHidden = Convert.ToBoolean(e.CommandArgument.ToString());
                MatchingHelper.UpdateMatchingSourceDataImportHiddenStatus(oMatchingParamInfo);
                rgMatchingSourceDataImport.Rebind();
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    protected void chkShowHiddenRows_OnCheckedChanged(object Sender, EventArgs e)
    {
        rgMatchingSourceDataImport.Rebind();
    }

    private void RedirectToDataTypeMapping(GridCommandEventArgs e)
    {
        List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrInfo = null;
        MatchingSourceDataImportHdrInfo oMatchingSourceDataImportInfo = new MatchingSourceDataImportHdrInfo();
        
        GridDataItem item = (GridDataItem)e.Item;
        oMatchingSourceDataImportInfo.MatchingSourceDataImportID = Convert.ToInt32(item["MatchingSourceDataImportID"].Text);
        oMatchingSourceDataImportInfo.MatchingSourceName = ((ExHyperLink)item.FindControl("hlMatchingSourceName")).Text;
        oMatchingSourceDataImportInfo.PhysicalPath = item["PhysicalPath"].Text;
        oMatchingSourceDataImportInfo.DataImportStatusID = (short)item.GetDataKeyValue("DataImportStatusID");
        oMatchingSourceDataImportInfo.MatchingSourceTypeID = Convert.ToInt16(item["MatchingSourceTypeID"].Text);
        ExHyperLink hlMatchingSourceName = (ExHyperLink)e.Item.FindControl("hlMatchingSourceName");
        oMatchingSourceDataImportHdrInfo = new List<MatchingSourceDataImportHdrInfo>();
        oMatchingSourceDataImportHdrInfo.Add(oMatchingSourceDataImportInfo);

        if (oMatchingSourceDataImportHdrInfo.Count > 0)
        {
            Session[SessionConstants.MATCHING_SOURCE_DATA] = oMatchingSourceDataImportHdrInfo;
            string url = "MatchSourceDataTypeMapping.aspx?" + QueryStringConstants.DATA_IMPORT_ID + "=" + oMatchingSourceDataImportInfo.MatchingSourceDataImportID;
            Response.Redirect(url);
        }
          
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
       //Response.Redirect(ReturnUrl, true);       
         Response.Redirect(URLConstants.URL_MATCHING_VIEW_MATCH_SET, true);
    }
    
}
