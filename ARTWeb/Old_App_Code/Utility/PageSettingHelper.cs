using System;
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
using Telerik.Web.UI;
using System.Collections;
using System.Text;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;


/// <summary>
/// Summary description for PageSettingHelper
/// </summary>
public class PageSettingHelper
{
    public PageSettingHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static GridSettings GetGridSettings(ExRadGrid rg, ARTEnums.Grid GridType)
    {
        GridSettings oGridSettings = new GridSettings();
        // PageSize
        oGridSettings.PageSize = rg.MasterTableView.PageSize;

        // GridSortExpression
        GridSortExpressionCollection oExpressionCollection = rg.MasterTableView.SortExpressions;
        GridSortExpression oGridSortExpression = null;
        if (oExpressionCollection != null && oExpressionCollection.Count > 0)
            oGridSortExpression = oExpressionCollection[0];
        if (oGridSortExpression != null)
        {
            oGridSettings.sortExpression = oGridSortExpression.FieldName;
            oGridSettings.sortDirection = oGridSortExpression.SortOrder;
        }

        // FilterCriteria
        string sessionKey = null;
        sessionKey = SessionHelper.GetSessionKeyForGridFilter(GridType);
        List<FilterCriteria> oFilterCriteriaCollection = (List<FilterCriteria>)HttpContext.Current.Session[sessionKey];
        if (oFilterCriteriaCollection != null)
            oGridSettings.oFilterCriteriaCollection = oFilterCriteriaCollection;

        return oGridSettings;
    }
    public static void SetGridSettins(ExRadGrid rg, GridSettings oGridSettings)
    {
        if (rg != null && oGridSettings != null)
        {
            if (oGridSettings.PageSize.HasValue)
            {
                rg.PageSize = oGridSettings.PageSize.Value;
                rg.MasterTableView.PageSize = oGridSettings.PageSize.Value;
            }
            if (oGridSettings.sortExpression != null && oGridSettings.sortDirection != 0)
            {
                GridSortExpression oGridSortExpression = new GridSortExpression();
                oGridSortExpression.FieldName = oGridSettings.sortExpression;
                oGridSortExpression.SortOrder = oGridSettings.sortDirection;
                rg.MasterTableView.SortExpressions.AddSortExpression(oGridSortExpression);
            }
        }
    }
    public static PageSettings GetPageSettings(WebEnums.ARTPages eARTPages)
    {
        PageSettings oPageSettings = null;
        int PageID = (int)eARTPages;
        if (HttpContext.Current.Session[SessionHelper.GetSessionKeyForPageSetting((short)PageID)] != null)
        {
            oPageSettings = (PageSettings)HttpContext.Current.Session[SessionHelper.GetSessionKeyForPageSetting((short)PageID)];
        }
        else
        {
            oPageSettings = new PageSettings();
            oPageSettings.PageID = PageID;
        }
        return oPageSettings;
    }
    public static void SavePageSettings(WebEnums.ARTPages eARTPages, PageSettings oPageSettings)
    {
        int PageID = (int)eARTPages;
        if (oPageSettings != null && PageID > 0)
            HttpContext.Current.Session[SessionHelper.GetSessionKeyForPageSetting((short)PageID)] = oPageSettings;
    }

}
