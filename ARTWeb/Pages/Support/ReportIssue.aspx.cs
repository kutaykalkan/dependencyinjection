using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public partial class Pages_Support_ReportIssue : PopupPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var url = GetSsoUrl(AppSettingHelper.GetAppSettingValue(AppSettingConstants.SUPPORT_SITE_BASE_URL), //including trailing slash
                               AppSettingHelper.GetAppSettingValue(AppSettingConstants.SUPPORT_SITE_PRIVATE_KEY), Helper.GetUserFullName(), SessionHelper.CurrentUserLoginID);
        Response.Redirect(url);
    }

    private string GetSsoUrl(string baseUrl, string secret, string name, string email)
    {
        var timems = (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString(CultureInfo.InvariantCulture);        
        return
            $"{baseUrl}login/sso?name={Server.UrlEncode(name)}&email={Server.UrlEncode(email)}&timestamp={timems}&hash={GetHash(secret, name, email, timems)}";        
    }

    private static string GetHash(string secret, string name, string email, string timems)
    {
        var input = name + secret + email + timems;
        var keybytes = Encoding.UTF8.GetBytes(secret);
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var crypto = new HMACMD5(keybytes);
        var hash = crypto.ComputeHash(inputBytes);
        return hash.Select(b => b.ToString("x2"))
            .Aggregate(new StringBuilder(),
                (current, next) => current.Append(next),
                current => current.ToString());        
    }
}