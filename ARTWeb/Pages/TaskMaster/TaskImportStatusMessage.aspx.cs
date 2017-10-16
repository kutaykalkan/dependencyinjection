using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Exception;
using System.Text;
using SkyStem.Language.LanguageUtility;
using System.Data;

public partial class Pages_TaskMaster_TaskImportStatusMessage : PageBaseCompany
{
    public string _ReferrerUrl
    {
        get
        {
            object objReferrerUrl = ViewState["ReferrerUrl"];
            if (objReferrerUrl == null)
                return String.Empty;

            return (string)objReferrerUrl;
        }

        set
        {
            ViewState["ReferrerUrl"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.SetPageTitle(this, 2594);
        Helper.GetUserFullName();

        try
        {

            if (!Page.IsPostBack)
            {
                _ReferrerUrl = Request.UrlReferrer.AbsoluteUri;
                LoadData();
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

    private void LoadData()
    {
        lblFailureMessages.Text = Session["ErrorMessage"].ToString();
    }

    public override string GetMenuKey()
    {
        return "DataImportStatus";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (_ReferrerUrl.Contains(QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE))
        {
            string tempReferrerUrl = _ReferrerUrl;
            int indexStatusMessage = tempReferrerUrl.IndexOf(QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE);
            _ReferrerUrl = _ReferrerUrl.Substring(0, indexStatusMessage - 1);
        }
        string url = _ReferrerUrl;
        url += "?" + QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE + "=2399";
        url += "&" + QueryStringConstants.CONFIRMATION_MESSAGE_FROM_STATUSPAGE + "=1";
        //Response.Redirect(url,false);
        SessionHelper.RedirectToUrl(url);
        return;
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (_ReferrerUrl.Contains(QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE))
            {
                string tempReferrerUrl = _ReferrerUrl;
                int indexStatusMessage = tempReferrerUrl.IndexOf(QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE);
                _ReferrerUrl = _ReferrerUrl.Substring(0, indexStatusMessage - 1);
            }
            string url = _ReferrerUrl;
            url += "?" + QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE + "=1784";
            url += "&" + QueryStringConstants.CONFIRMATION_MESSAGE_FROM_STATUSPAGE + "=3";
            //Response.Redirect(url,false);
            SessionHelper.RedirectToUrl(url);
            return;
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


    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            if (_ReferrerUrl.Contains(QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE))
            {
                string tempReferrerUrl = _ReferrerUrl;
                int indexStatusMessage = tempReferrerUrl.IndexOf(QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE);
                _ReferrerUrl = _ReferrerUrl.Substring(0, indexStatusMessage - 1);
            }
            string url = _ReferrerUrl;
            url += "?" + QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE + "=2399";
            url += "&" + QueryStringConstants.CONFIRMATION_MESSAGE_FROM_STATUSPAGE + "=2";
            //Response.Redirect(url,false);
            SessionHelper.RedirectToUrl(url);
            return;
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


    private string GetUrlForBankImportPage()
    {
        return "GeneralTaskImport.aspx";
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        //Helper.SetBreadcrumbs(this, 2498, 2497);
    }

}
