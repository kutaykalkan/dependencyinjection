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
using SkyStem.ART.Client.IServices;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;

public partial class UserControls_CapabilityCertificationActivation : UserControlBase
{
    IUtility oUtilityClient;
    int _companyID;
    bool? _isCertificationActivationYesChecked = false;
    bool? _isCertificationActivationYesCheckedCurrent = false;
    public int CompanyID
    {
        get { return _companyID; }
        set { _companyID = value; }
    }
    public bool? IsCertificationActivationYesChecked
    {
        get { return _isCertificationActivationYesChecked; }
        set { _isCertificationActivationYesChecked = value; }
    }
    public bool? IsCertificationActivationYesCheckedCurrent
    {
        get { return _isCertificationActivationYesCheckedCurrent; }
        set { _isCertificationActivationYesCheckedCurrent = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        oUtilityClient = RemotingHelper.GetUtilityObject();
        if (!IsPostBack)
        {
            if (_isCertificationActivationYesChecked == true)
            {
                optCertificationActivationYes.Checked = true;
                optCertificationActivationNo.Checked = false;
                //pnlMain.CssClass = "PanelCapability";
                //pnlContent.CssClass = "PanelContent";
                //pnlYesNo.CssClass = "PanelCapabilityYesNo";
                BindSystemWideSettingsRadGrid();
            }
            else if (_isCertificationActivationYesChecked == false )
            {
                optCertificationActivationYes.Checked = false;
                optCertificationActivationNo.Checked = true;
                //pnlMain.CssClass = "";
                //pnlContent.CssClass = "";
                //pnlYesNo.CssClass = "";
            }
            else if (_isCertificationActivationYesChecked == null)
            {
                optCertificationActivationYes.Checked = false;
                optCertificationActivationNo.Checked = false;
                //pnlMain.CssClass = "";
                //pnlContent.CssClass = "";
                //pnlYesNo.CssClass = "";
            }
        }
        ShowHideForRadioButtonYesNoChecked();
    }

    protected void BindSystemWideSettingsRadGrid()
    {
        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = oCompanyClient.SelectAllReconciliationPeriodByCompanyID(_companyID,SessionHelper.CurrentFinancialYearID .Value , Helper.GetAppUserInfo());
        rdCertificationActivation.DataSource = oReconciliationPeriodInfoCollection;
        rdCertificationActivation.DataBind();
    }

    protected void rdCertificationActivation_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
           e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            //if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))

            ExLabel lblRecPeriod = (ExLabel)e.Item.FindControl("lblRecPeriod");
            CheckBox cbAllowCertificationLockDown = (CheckBox)e.Item.FindControl("cbAllowCertificationLockDown");
            ExCalendar calCertificationStartDate = (ExCalendar)e.Item.FindControl("calCertificationStartDate");
            ExCalendar calCertificationLockDownDate = (ExCalendar)e.Item.FindControl("calCertificationLockDownDate");
            ReconciliationPeriodInfo oReconciliationPeriodInfo = new ReconciliationPeriodInfo();
            oReconciliationPeriodInfo = (ReconciliationPeriodInfo)e.Item.DataItem;
            calCertificationStartDate.Text = Helper.GetDisplayDate(oReconciliationPeriodInfo.CertificationStartDate);
            calCertificationLockDownDate.Text = Helper.GetDisplayDate(oReconciliationPeriodInfo.ReconciliationCloseDate);
            lblRecPeriod.Text = Helper.GetDisplayDate(oReconciliationPeriodInfo.PeriodEndDate);
            
            //bool isCertificationEnabled = true;//TODO: make a field for allowLockDownDate
            //if (isCertificationEnabled == true)
            //{
            //    //cbAllowCertificationLockDown.Enabled = false;
            //    calCertificationLockDownDate.Enabled = false;
            //}
            cbAllowCertificationLockDown.Attributes.Add("OnClick", "ShowHide('" + cbAllowCertificationLockDown.ClientID +"','" +calCertificationLockDownDate.ClientID + "')");
            //calCertificationLockDownDate.Enabled = false;
        }
    }

    protected void optCertificationActivationYes_CheckedChanged(object sender, EventArgs e)
    {
        ShowHideForRadioButtonYesNoChecked();
        //BindAfterYesNoSelection();
        BindSystemWideSettingsRadGrid();
    }

    protected void optCertificationActivationNo_CheckedChanged(object sender, EventArgs e)
    {
        ShowHideForRadioButtonYesNoChecked();
    }

    protected void ShowHideForRadioButtonYesNoChecked()
    {
        if (optCertificationActivationYes.Checked == true && optCertificationActivationNo.Checked == false )
        {
            pnlContentCertificationActivation.Visible = true;
            pnlContent.Visible = true;
            _isCertificationActivationYesCheckedCurrent = true;
            //imgCollapse.Visible = true;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "block";
            //pnlMain.CssClass = "PanelCapability";
            //pnlContent.CssClass = "PanelContent";
            //pnlYesNo.CssClass = "PanelCapabilityYesNo";
        }
        else if (optCertificationActivationYes.Checked == false  && optCertificationActivationNo.Checked == true)
        {
            pnlContentCertificationActivation.Visible = false;
            pnlContent.Visible = false;
            _isCertificationActivationYesCheckedCurrent = false;
            //imgCollapse.Visible = false;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "none";
            //pnlMain.CssClass = "";
            //pnlContent.CssClass = "";
            //pnlYesNo.CssClass = "";
        }
        else if (optCertificationActivationYes.Checked == false && optCertificationActivationNo.Checked == false)
        {
            pnlContentCertificationActivation.Visible = false;
            pnlContent.Visible = false;
            _isCertificationActivationYesCheckedCurrent = null ;
            //imgCollapse.Visible = false;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "none";
            //pnlMain.CssClass = "";
            //pnlContent.CssClass = "";
            //pnlYesNo.CssClass = "";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //BindSystemWideSettingsRadGrid();
    }

    //Handle masterpage DDLs change
    public void ChangedEventHandler()
    {
        oUtilityClient = RemotingHelper.GetUtilityObject();
            if (_isCertificationActivationYesChecked == true)
            {
                optCertificationActivationYes.Checked = true;
                optCertificationActivationNo.Checked = false;
                //pnlMain.CssClass = "PanelCapability";
                //pnlContent.CssClass = "PanelContent";
                //pnlYesNo.CssClass = "PanelCapabilityYesNo";
                BindSystemWideSettingsRadGrid();
            }
            else if (_isCertificationActivationYesChecked == false )
            {
                optCertificationActivationYes.Checked = false;
                optCertificationActivationNo.Checked = true;
                //pnlMain.CssClass = "";
                //pnlContent.CssClass = "";
                //pnlYesNo.CssClass = "";
            }
            else if (_isCertificationActivationYesChecked == null)
            {
                optCertificationActivationYes.Checked = false;
                optCertificationActivationNo.Checked = false;
                //pnlMain.CssClass = "";
                //pnlContent.CssClass = "";
                //pnlYesNo.CssClass = "";
            }
        ShowHideForRadioButtonYesNoChecked();
    }


    public IList<ReconciliationPeriodInfo> GetReconciliationPeriodDueDatesObjectToBeSavedFromUC()
    {
        //ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = null;
        //if (_isFSCaptionMaterialitySelected == true)( as _isFSCaptionMaterialitySelected is not set ,(method is called directly from parent page)))
        oReconciliationPeriodInfoCollection = new List<ReconciliationPeriodInfo>();
            for (int i = 0; i < rdCertificationActivation.Items.Count; i++)
            {
                ExCalendar calCertificationStartDate = (ExCalendar)rdCertificationActivation.Items[i].FindControl("calCertificationStartDate");
                ExCheckBox cbAllowCertificationLockDown = (ExCheckBox)rdCertificationActivation.Items[i].FindControl("cbAllowCertificationLockDown");
                ExCalendar calCertificationLockDownDate = (ExCalendar)rdCertificationActivation.Items[i].FindControl("calCertificationLockDownDate");
                ReconciliationPeriodInfo oReconciliationPeriodInfo = new ReconciliationPeriodInfo();
                string recPeriodID = rdCertificationActivation.MasterTableView.DataKeyValues[i]["ReconciliationPeriodID"].ToString();
                oReconciliationPeriodInfo.ReconciliationPeriodID = Convert.ToInt32(recPeriodID);
                oReconciliationPeriodInfo.CertificationStartDate = Convert.ToDateTime(calCertificationStartDate.Text);
                //oReconciliationPeriodInfo.AllowCertificationLockDown = cbAllowCertificationLockDown.Checked;
                oReconciliationPeriodInfo.CertificationLockDownDate = Convert.ToDateTime(calCertificationLockDownDate.Text);
                oReconciliationPeriodInfoCollection.Add(oReconciliationPeriodInfo);
            }
            //int returnValue = oFSCaptionClient.SaveMaterilityByFSCaptionTableValue(lstFSCaptionMaterialityInfo, _companyID);
            return oReconciliationPeriodInfoCollection;
    }

}//end of class
