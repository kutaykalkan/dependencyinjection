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
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Data;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Data;

public partial class Pages_PopupUserLockdownDetail : PopupPageBase
{
    private int? _UserID;
    protected void Page_Load(object sender, EventArgs e)
    {

        PopupHelper.SetPageTitle(this, 2943);
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.User_ID]))
            this._UserID = Convert.ToInt32(Request.QueryString[QueryStringConstants.User_ID]);

        this.BindRecFrequencyGrid();
    }
    private void BindRecFrequencyGrid()
    {

        List<UserLockdownDetailInfo> oUserLockdownDetailInfoList = Helper.GetLockdownDetail( _UserID);
        rgUserLockdownDetail.DataSource = oUserLockdownDetailInfoList;
        rgUserLockdownDetail.DataBind();
    }
    protected void rgUserLockdownDetail_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                ExLabel lblLockoutDateAndTime = (ExLabel)e.Item.FindControl("lblLockoutDateAndTime");
                ExLabel lblResetDateAndTime = (ExLabel)e.Item.FindControl("lblResetDateAndTime");
                UserLockdownDetailInfo oUserHdrInfo = (UserLockdownDetailInfo)e.Item.DataItem;
                lblLockoutDateAndTime.Text = Helper.GetDisplayDateTime(oUserHdrInfo.LockdownDateTime);
                lblResetDateAndTime.Text = Helper.GetDisplayDateTime(oUserHdrInfo.ResetDateTime);
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }
}
