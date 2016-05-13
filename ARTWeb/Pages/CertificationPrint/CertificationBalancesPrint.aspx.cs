using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Model;
using Telerik.Web.UI;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;

public partial class Pages_CertificationPrint_CertificationBalancesPrint : PopupPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucSkyStemARTGrid.CompanyID = SessionHelper.CurrentCompanyID;
        ucSkyStemARTGrid.Grid.AllowPaging = false;

        List<GLDataHdrInfo> oGLDataHdrInfoCollection = (List<GLDataHdrInfo>)Session[SessionConstants.CERTIFICATION_BALANCES_DATA];;

        ucSkyStemARTGrid.DataSource = oGLDataHdrInfoCollection;
        ucSkyStemARTGrid.GridGroupByExpression = Helper.GetGridGroupByExpressionForFSCaption();
        ucSkyStemARTGrid.BindGrid();
    }

    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        CertificationHelper.ItemDataBoundCertificationBalances(e);
    }

}
