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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Client.Model.Report;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls;

public partial class Pages_ReportsPrint_ReviewNoteReportPrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucSkyStemARTGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
        GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());
        ucSkyStemARTGrid.DataSource = GetGridData();
        if (!IsPostBack)
        {
            // Set default Sorting
            GridHelper.SetSortExpression(ucSkyStemARTGrid.Grid.MasterTableView, "AccountName");

            //Grouping By Account#
            GridGroupByExpression expressionAccount = new GridGroupByExpression();

            GridGroupByFieldList groupByFieldsList = new GridGroupByFieldList();
            GridGroupByField gridGroupByField;

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "AccountNumber";
            gridGroupByField.HeaderText = SkyStem.Language.LanguageUtility.LanguageUtil.GetValue(1712);
            //gridGroupByField.HeaderValueSeparator = " for current group: ";
            gridGroupByField.FormatString = "<strong>{0}</strong>";
            groupByFieldsList.Add(gridGroupByField);

            expressionAccount.SelectFields.AddRange(groupByFieldsList);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "AccountNumber";
            expressionAccount.GroupByFields.Add(gridGroupByField);

            //Grouping By Subject
            GridGroupByExpression expressionSubject = new GridGroupByExpression();

            GridGroupByFieldList groupByFieldsListSubject = new GridGroupByFieldList();

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "Subject";
            gridGroupByField.HeaderText = SkyStem.Language.LanguageUtility.LanguageUtil.GetValue(1778);
            //gridGroupByField.HeaderValueSeparator = " for current group: ";
            gridGroupByField.FormatString = "<strong>{0}</strong>";
            groupByFieldsListSubject.Add(gridGroupByField);
            expressionSubject.SelectFields.AddRange(groupByFieldsListSubject);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "Subject";
            expressionSubject.GroupByFields.Add(gridGroupByField);

            GridGroupByExpressionCollection grdExpressionCollection = new GridGroupByExpressionCollection();
            grdExpressionCollection.Add(expressionAccount);
            grdExpressionCollection.Add(expressionSubject);
            ucSkyStemARTGrid.Grid.MasterTableView.GroupByExpressions.AddRange(grdExpressionCollection);
        }
        //Sort the Data
        GridHelper.SortDataSource(ucSkyStemARTGrid.Grid.MasterTableView);
        ucSkyStemARTGrid.BindGrid();
    }
    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        ReportHelper.ItemDataBoundReviewNotesByUserReport(e);
    }

    private List<ReviewNotesReportInfo> GetGridData()
    {
        List<ReviewNotesReportInfo> oReviewNotesReportInfoCollection = null;

        oReviewNotesReportInfoCollection = (List<ReviewNotesReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_REVIEW_NOTES];
        return oReviewNotesReportInfoCollection;

    }
}
