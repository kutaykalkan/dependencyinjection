﻿using System;
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
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Classes;


namespace SkyStem.ART.Web.WebParts
{

    /// <summary>
    /// Summary description for AccountOwnershipStatistics
    /// </summary>
    public class AccountOwnershipStatisticsWP : WebPartBase
    {
        public AccountOwnershipStatisticsWP()
        {
            base.DashboardID = 2;
            base.DefaultZoneID = 1;
        }

    }
}