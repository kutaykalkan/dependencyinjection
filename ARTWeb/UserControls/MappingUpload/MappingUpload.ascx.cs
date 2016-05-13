using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.ART.Client.Model.QualityScore;
using SkyStem.ART.Web.Utility;
using Telerik.Web.UI;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Model.MappingUpload;
using SkyStem.ART.Client.IServices;
using SkyStem.Library.Controls.TelerikWebControls;

public partial class UserControls_MappingUpload_MappingUpload : UserControlMappingUpload
{
    #region Properties

    /// <summary>
    /// Do not use variable, use property instead
    /// </summary>
    private List<MappingUploadInfo> _MappingUploadInfoList = null;

    private bool IsCertificationStarted { get; set; }
    /// <summary>
    /// Gets or sets the company quality score info list. 
    /// </summary>
    /// <value>
    /// The company quality score info list.
    /// </value>
    public List<MappingUploadInfo> MappingUploadInfoList
    {
        get
        {
            if (_MappingUploadInfoList == null)
                _MappingUploadInfoList = (List<MappingUploadInfo>)Session[SessionConstants.MAPPING_UPLOAD_LIST];
            return _MappingUploadInfoList;
        }
        set
        {
            _MappingUploadInfoList = value;
            Session[SessionConstants.MAPPING_UPLOAD_LIST] = value;
        }
    }

    #endregion

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {

        ucInputRequirements.ShowInputRequirements(2457);
        SetControlState();
        
    }

   #endregion

    #region Data Binding Functions

    private IList<MappingUploadInfo> LoadComapnyMappingUploadKeys()
    {
        IMappingUpload oMappingUploadClient = RemotingHelper.GetMappingUploadObject();
        if (MappingUploadInfoList == null)
            MappingUploadInfoList = oMappingUploadClient.GetMappingUploadInfoList(SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
        return MappingUploadInfoList;
    }

#endregion

    #region Control Events

    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        Helper.RedirectToHomePage();
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        if (rgMappingUpload.SelectedItems.Count > 0)
        {
            SaveMappingKeys();
        }
        else
        {
            PopulateData();
            Helper.ShowErrorMessageFromUserControl(this, new ARTException(2013));
        }
    }

    #endregion

    #region Grid Events

    protected void rgMappingUpload_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                MappingUploadInfo oMappingUploadInfo = (MappingUploadInfo)e.Item.DataItem;
                GridDataItem oItem = e.Item as GridDataItem;

                CheckBox chkSelectCol = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
                ExLabel lblGeographyClassName = (ExLabel)e.Item.FindControl("lblGeographyClassName");
                ExLabel lblGeographyStructure = (ExLabel)e.Item.FindControl("lblGeographyStructure");
                lblGeographyClassName.LabelID = int.Parse(oMappingUploadInfo.AccountMappingKeyNameLabelID.ToString());
                if (oMappingUploadInfo.IsEnabled)
                {
                    lblGeographyStructure.LabelID = int.Parse(Convert.ToString(oMappingUploadInfo.GeographyStructureLabelID));
                    if (oMappingUploadInfo.SelectedKeysID != null)
                    {
                        chkSelectCol.Checked = true;
                        oItem.Selected = true;
                    }
                }
                else
                {
                    chkSelectCol.Enabled = false;
                    lblGeographyStructure.Text = "-";
                }
            }            
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessageFromUserControl(this, ex);
        } 
    }

    #endregion

    #region Custom Functions
    /// <summary>
    /// Populates the data.
    /// </summary>
    public void PopulateData()
    {
        SetControlState();
        MappingUploadInfoList = null;
        IList<MappingUploadInfo> oMappingUploadInfo = LoadComapnyMappingUploadKeys();
        rgMappingUpload.DataSource = oMappingUploadInfo;
        rgMappingUpload.DataBind();
        if (oMappingUploadInfo == null || oMappingUploadInfo.Count == 0)
        {
            btnSave.Enabled = false;
        }
    }


    /// <summary>
    /// Sets the state of the control.
    /// </summary>
    private void SetControlState()
    {
        pnlMappingUpload.Enabled = true;
        btnSave.Visible = true;
        IsCertificationStarted = CertificationHelper.IsCertificationStarted();
        if (IsCertificationStarted)
        {
            btnSave.Visible = false;
            pnlMappingUpload.Enabled = false;
        }
    }


    /// <summary>
    /// Saves the Mapping of Keys.
    /// </summary>
    public void SaveMappingKeys()
    {
        try
        {
                List<MappingUploadInfo> saveInfoMappingUpload = new List<MappingUploadInfo>();
                saveInfoMappingUpload = SetSelectedMappingKeys();

                bool IsMappingSuccessful = MappingUploadHelper.SaveMappingUploadInfoList(saveInfoMappingUpload, SessionHelper.CurrentUserLoginID);
                if (IsMappingSuccessful)
                    Helper.RedirectToHomePage(2453);
                else
                {
                    PopulateData();
                    Helper.ShowErrorMessageFromUserControl(this, new ARTException(2454));
                }
                
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessageFromUserControl(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessageFromUserControl(this, ex);
        } 
    }

    protected void cvMappingUpload_ServerValidate(object source, ServerValidateEventArgs args)
    {
       
    }

    private List<MappingUploadInfo> SetSelectedMappingKeys()
    {
        List<MappingUploadInfo> keyInfo = new List<MappingUploadInfo>();
        foreach (GridDataItem item in rgMappingUpload.SelectedItems)
        {
            //CheckBox chkSelectCol = (CheckBox)item["CheckboxSelectColumn"].Controls[0];
            //if (chkSelectCol.Checked)
            //{
                int accountMappingKeyID = (int)item.GetDataKeyValue("AccountMappingKeyID");
                MappingUploadInfo accountIDInfo = new MappingUploadInfo();
                accountIDInfo.AccountMappingKeyID = accountMappingKeyID;
                keyInfo.Add(accountIDInfo);
            //}
        }


        return keyInfo;
    }

    #endregion

}
