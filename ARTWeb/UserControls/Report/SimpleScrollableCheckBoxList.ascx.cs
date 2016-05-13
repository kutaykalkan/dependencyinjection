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
using System.Collections.Generic;
using System.Text;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;

public partial class SimpleScrollableCheckBoxList : UserControlBase
{
    #region "Private Properties"
    private string[] _selectedIDs;
    private List<short> _getSelectedIDs = new List<short>();
    private ListItemCollection _cblDataSource;
    private string SetSelectedIDs
    {
        set
        {
            char[] arrySeperator = ReportHelper.FilterValueSeparator.ToCharArray();
            this._selectedIDs = value.Split(arrySeperator, StringSplitOptions.RemoveEmptyEntries);
            this.SelectIDs();
        }
    }
    public string SetDefaultSelectedIDs
    {
        set
        {
            this.SetSelectedIDs = value;
        }
    }
    #endregion

    #region "Public Properties"
    public ListItemCollection CBLDataSource
    {
        set
        {
            this._cblDataSource = value;
            this.cblOptions.DataSource = this._cblDataSource;
            this.cblOptions.DataTextField = "Text";
            this.cblOptions.DataValueField = "Value";
            this.cblOptions.DataBind();
        }
    }
    public List<short> GetSelectedIDs
    {
        get
        {
            this._getSelectedIDs.Clear();
            foreach (ListItem li in this.cblOptions.Items)
            {
                if (li.Selected)
                    this._getSelectedIDs.Add(Convert.ToInt16(li.Value));
            }
            return this._getSelectedIDs;
        }
    }
    public bool isRequired
    {
        set
        {
            this.rfv.Enabled = value;
        }
    }
    public int ErrorLabelID
    {
        set
        {
            this.rfv.LabelID = value;
        }
    }
    #endregion

    #region "Page EventHandlers"
    protected void Page_Init(object sender, EventArgs e)
    {
        string jsFunction = String.Format("SelectUnselectAll(this,'{0}')", this.cblOptions.ClientID);
        this.chkSelectAll.InputAttributes.Add("onclick", jsFunction);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Page_Prerender(object sender, EventArgs e)
    {
        //Disable Select all checkbox if there is no item in Checkbox list
        if (this.cblOptions.Items.Count > 0)
        {
            this.chkSelectAll.Enabled = true;
            if (!this.IsPostBack)
            {
                foreach (ListItem li in this.cblOptions.Items)
                {
                    if (!li.Selected)
                    {
                        this.chkSelectAll.Checked = false;
                        return;
                    }
                }
                this.chkSelectAll.Checked = true;
            }
        }
        else
        {
            this.chkSelectAll.Enabled = false;
        }
        
        
    }

    #endregion

    #region "Private Methods"
    private void SelectIDs()
    {
        this.cblOptions.ClearSelection();
        foreach (string keyValue in this._selectedIDs)
        {
            ListItem Litem = this.cblOptions.Items.FindByValue(keyValue);
            if (Litem != null)
                Litem.Selected = true;
        }
    }

    #endregion

    #region "Public Methods"
    public void GetCriteria(Dictionary<string, string> oRptCriteriaDictionary, string criteriaKey)
    {
        StringBuilder oSBCriteria = new StringBuilder();
        foreach (ListItem Litem in this.cblOptions.Items)
        {
            if (Litem.Selected)
            {
                if (oSBCriteria.ToString().Equals(String.Empty))
                    oSBCriteria.Append(Litem.Value);
                else
                    oSBCriteria.Append(ReportHelper.FilterValueSeparator + Litem.Value);
            }
        }
        oRptCriteriaDictionary.Remove(criteriaKey);
        oRptCriteriaDictionary.Add(criteriaKey, oSBCriteria.ToString());
    }
    public void SetCriteria(Dictionary<string, string> oRptCriteriaDictionary, string criteriaKey)
    {
        if (oRptCriteriaDictionary.ContainsKey(criteriaKey))
        {
            this.SetSelectedIDs = oRptCriteriaDictionary[criteriaKey];
        }
    }
    #endregion

    protected void cblOptions_DataBound(object sender, EventArgs e)
    {
        CheckBoxList cbl = (CheckBoxList)sender;
        foreach (Control ctrl in this.cblOptions.Controls)
        {
            if (ctrl is CheckBox)
            {
                CheckBox chkBx = (CheckBox)ctrl;
                string functionName = String.Format(" CheckTest('{0}','{1}')", this.cblOptions.ClientID, this.chkSelectAll.ClientID);
                chkBx.InputAttributes.Add("onclick", functionName);
            }
        }

    }
}
