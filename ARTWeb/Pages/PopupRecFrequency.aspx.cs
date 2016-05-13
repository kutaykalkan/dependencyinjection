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

public partial class Pages_PopupRecFrequency : PopupPageBase
{
    private long? _AccountID = 0;
    private int? _NetAccountID = 0;
    private int? _TaskID = 0;
    private short? _TaskRecurrenceTyp;

   

   

    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
            this._AccountID = Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]))
            this._NetAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_ID]))
            this._TaskID = Convert.ToInt32(Request.QueryString[QueryStringConstants.TASK_ID]);
         if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_RECURRENCE_TYPE]))
             this._TaskRecurrenceTyp = Convert.ToInt16(Request.QueryString[QueryStringConstants.TASK_RECURRENCE_TYPE]);
        
        this.BindRecFrequencyGrid();
    }
    private void BindRecFrequencyGrid()
    {
        List<AccountReconciliationPeriodInfo> oAccountReconciliationPeriodInfoCollection = new List<AccountReconciliationPeriodInfo>();
        List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = CacheHelper.GetAllReconciliationPeriods(null);

        if (_TaskID.HasValue && _TaskID > 0)
        {
            this.Title = LanguageUtil.GetValue(2565);
            List<int> RecPeriodIDList = TaskMasterHelper.GetRecurrenceFrequencyByTaskID(_TaskID, SessionHelper.CurrentReconciliationPeriodID);
            if (_TaskRecurrenceTyp == (short)ARTEnums.TaskRecurrenceType.Custom)
            {
                oReconciliationPeriodInfoCollection = (from recPeriod in oReconciliationPeriodInfoCollection
                                                       from TRecPeriod in RecPeriodIDList
                                                       where recPeriod.ReconciliationPeriodID == TRecPeriod
                                                       select recPeriod).ToList();
            }
            else
            {
                oReconciliationPeriodInfoCollection.Clear();
                ReconciliationPeriodInfo oReconciliationPeriodInfo;
                foreach (var item in RecPeriodIDList)
                {
                    oReconciliationPeriodInfo = new ReconciliationPeriodInfo();
                    oReconciliationPeriodInfo.PeriodNumber = (short)item;
                    oReconciliationPeriodInfoCollection.Add(oReconciliationPeriodInfo);
                }
            }
        }
        else
        {
            this.Title = LanguageUtil.GetValue(1427);
            IAccount oAccountClient = RemotingHelper.GetAccountObject();


            if (this._NetAccountID.HasValue && this._NetAccountID.Value > 0)
            {
                AccountHdrInfo oAccountHdrInfo = oAccountClient.GetNetAccountHdrInfoCollectionByNetID(SessionHelper.CurrentReconciliationPeriodID.Value, this._NetAccountID.Value,Helper.GetAppUserInfo()).FirstOrDefault();

                if (oAccountHdrInfo != null)
                {
                    this._AccountID = oAccountHdrInfo.AccountID;
                }
            }

            if (this._AccountID.HasValue && this._AccountID.Value > 0)
            {
                oAccountReconciliationPeriodInfoCollection = oAccountClient.SelectAccountRecPeriodByAccountID(this._AccountID.Value,Helper.GetAppUserInfo());
            }
            oReconciliationPeriodInfoCollection = (from recPeriod in oReconciliationPeriodInfoCollection
                                                   from accRecPeriod in oAccountReconciliationPeriodInfoCollection
                                                   where recPeriod.ReconciliationPeriodID == accRecPeriod.ReconciliationPeriodID
                                                   select recPeriod).ToList();
         
        }
    
        radRecFrequency.DataSource = oReconciliationPeriodInfoCollection;
        radRecFrequency.DataBind();
    }
    protected void radRecFrequency_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                ExLabel lblDate = (ExLabel)e.Item.FindControl("lblDate");
                ReconciliationPeriodInfo oReconciliationPeriodInfo = (ReconciliationPeriodInfo)e.Item.DataItem;
                lblDate.Text = Helper.GetDisplayDate(oReconciliationPeriodInfo.PeriodEndDate);
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }
}
