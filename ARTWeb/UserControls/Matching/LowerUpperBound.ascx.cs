using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Utility;

public partial class LowerUpperBound : UserControlBase
{
    #region Private Variables
    private Int32 _rowID = 0;
    #endregion

    #region Private Properties
    public String SetLowerBoundValue
    {
        set { this.txtLowerBound.Text = value; }
    }
    public String SetUpperBoundValue
    {
        set { this.txtUpperBound.Text = value; }
    }
    public bool SetEnabled
    {
        set
        {
            this.txtUpperBound.Enabled = value;
            this.txtLowerBound.Enabled = value;
        }
    }
    #endregion

    #region Public Properties

    public Int32 RowID
    {
        get { return _rowID; }
        set { _rowID = value; }
    }


    #region Set Propeties
    public Int32 LowerBoundLabelID
    {
        set { lblLowerBound.LabelID = value; }
    }
    public int UpperBoundLabelID
    {
        set { lblUpperBound.LabelID = value; }
    }
    public Boolean IsRequired
    {
        set
        {
            this.rfv1.Enabled = value;
            this.rfv2.Enabled = value;
        }
    }


    #endregion

    #region Get Properties
    public Decimal GetLowerBoundValue
    {
        get
        {
            //foreach (char c in txtLowerBound.Text)
            //{
            //    if (!Char.IsNumber(c))
            //    {
            //        if (c != '.')
            //            throw new Exception(LanguageUtil.GetValue(5000270));
            //    }
            //}
            if (!Helper.IsDecimal(txtLowerBound.Text))
                throw new Exception(LanguageUtil.GetValue(5000270));

            return Convert.ToDecimal(txtLowerBound.Text);

        }
    }
    public Decimal GetUpperBoundValue
    {
        get
        {
            //foreach (char c in txtUpperBound.Text)
            //{
            //    if (!Char.IsNumber(c))
            //    {
            //        if (c != '.')
            //            throw new Exception(LanguageUtil.GetValue(5000271));
            //    }
            //}
            if (!Helper.IsDecimal(txtUpperBound.Text))
                throw new Exception(LanguageUtil.GetValue(5000271));

            return Convert.ToDecimal(txtUpperBound.Text);
        }
    }
    public String GetCriteriaForDisplay
    {
        get;
        set;

    }
    public Boolean HasValue
    {
        get { return (this.txtLowerBound.Text + this.txtUpperBound.Text) != string.Empty; }
    }
    #endregion

    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        this.rfv1.Attributes.Add("txtLowerBoundClientID", txtLowerBound.ClientID);
        this.rfv2.Attributes.Add("txtUpperBoundClientID", txtUpperBound.ClientID);
    }
    #endregion

    #region Private Methods
    private void ClearSelection()
    {
        txtUpperBound.Text = "";
        txtLowerBound.Text = "";
    }
    #endregion


}
