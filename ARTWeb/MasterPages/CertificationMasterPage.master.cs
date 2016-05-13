using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Language.LanguageUtility;
using System.IO;
using ExpertPdf.HtmlToPdf;

public partial class MasterPages_CertificationMasterPage : CertificationMasterPageBase
{
    private short _RoleID;
    //private int _ReconciliationPeriodID;
    private int _UserID;

    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        oMasterPageBase.RegisterPostBackToControls(imgBtnExportToPDF);

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        string url = "../" + GetUrlForPrint();
        
        hlPrint.NavigateUrl = "javascript:OpenPrintWindow('" + url + "', " + WebConstants.PRINT_POPUP_HEIGHT + ", " + WebConstants.PRINT_POPUP_WIDTH + ");";

        tblExportToolbar.Visible = this.ShowExportToolbar;
    }

    private string GetUrlForPrint()
    {
        string url =this.PrintUrl;

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.User_ID]))
        {
            this._UserID = Convert.ToInt32(Request.QueryString[QueryStringConstants.User_ID]);
        }
        else
        {
            this._UserID = SessionHelper.CurrentUserID.Value;
        }

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ROLE_ID]))
            this._RoleID = Convert.ToInt16(Request.QueryString[QueryStringConstants.ROLE_ID]);
        else
            this._RoleID= SessionHelper.CurrentRoleID.Value;

        url += "?" + QueryStringConstants.User_ID + "=" + this._UserID;
        url += "&" + QueryStringConstants.ROLE_ID + "=" + this._RoleID;
        url += "&" + QueryStringConstants.CERTIFICATION_TYPE_ID + "=" + this.CertificationType.ToString("d");
        if (this.PageTitleLabeID != null)
        {
            url += "&" + QueryStringConstants.PAGE_TITLE_ID + "=" + this.PageTitleLabeID.Value;
        }

        return url;
    }

    protected void imgBtnExportToPDF_Click(object sender, EventArgs e)
    {
        string pageTitle = LanguageUtil.GetValue(this.PageTitleLabeID.Value);
        string fileName = pageTitle + ".pdf";
        fileName = ExportHelper.RemoveInvalidFileNameChars(fileName);

        string url = "~/Pages/" + GetUrlForPrint();
        TextWriter oTextWriter = new StringWriter();
        Server.Execute(url, oTextWriter);

        ExportHelper.GeneratePDFAndRender(pageTitle, fileName, oTextWriter.ToString(), true,true);
    }

    
}
