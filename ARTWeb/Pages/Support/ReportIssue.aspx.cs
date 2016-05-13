using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Support_ReportIssue : PopupPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = GetSsoUrl(AppSettingHelper.GetAppSettingValue(AppSettingConstants.SUPPORT_SITE_BASE_URL), //including trailing slash
                               AppSettingHelper.GetAppSettingValue(AppSettingConstants.SUPPORT_SITE_PRIVATE_KEY), Helper.GetUserFullName(), SessionHelper.CurrentUserLoginID);
        Response.Redirect(url);
    }

    string GetSsoUrl(string baseUrl, string secert, string name, string email)
    {
        return String.Format("{0}login/sso/?name={1}&email={2}&hash={3}", baseUrl, Server.UrlEncode(name),
                             Server.UrlEncode(email), GetHash(secert, name, email));
    }

    static string GetHash(string secert, string name, string email)
    {
        string input = name + email + secert;

        MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();
        foreach (byte b in hash)
        {
            string hexValue = b.ToString("X").ToLower(); // Lowercase for compatibility on case-sensitive systems
            sb.Append((hexValue.Length == 1 ? "0" : "") + hexValue);
        }
        return sb.ToString();
    }
}