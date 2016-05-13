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
using SkyStem.ART.Web.Classes;

public partial class UserRole : UserControlBase 
{
    // Delegate declaration
    public delegate void OnFetchUserClick(List<short> selectedRoleIDs);
    // Event declaration
    public event OnFetchUserClick btnFetchUserHandler;
    
    public ListItemCollection DataSourceForRolesList
    {
        set
        {
            this.ucRoles.CBLDataSource = value;

        }
    }

    public ListItemCollection DataSourceForUserList
    {
        set
        {
            this.ucUsers.CBLDataSource = value;
        }
    }

    public List<short> GetSelectedRoles
    {
        get
        {
            return this.ucRoles.GetSelectedIDs;
        }
    }
    public bool isRequiredUser
    {
        set
        {
            //this.rowMandatory.Visible = true;
            this.ucUsers.isRequired = value;
        }
    }
    public bool isRequiredRole
    {
        set
        {
            //this.rowMandatory.Visible = true;
            this.ucRoles.isRequired = value;
        }
    }
    public int ErrorLabelIDUser
    {
        set
        {
            this.ucUsers.ErrorLabelID = value;
        }
    }
    public int ErrorLabelIDRole
    {
        set
        {
            this.ucRoles.ErrorLabelID = value;
        }
    }
    public void GetUserCriteria(Dictionary<string, string> oRptCriteria, string criteriaKeyName)
    {
        this.ucUsers.GetCriteria(oRptCriteria, criteriaKeyName);
    }
    public void GetRoleCriteria(Dictionary<string, string> oRptCriteria, string criteriaKeyName)
    {
        this.ucRoles.GetCriteria(oRptCriteria, criteriaKeyName);
    }
    public void SetUserCriteria(Dictionary<string, string> oRptCriteria, string criteriaKeyName)
    {
        this.ucUsers.SetCriteria(oRptCriteria, criteriaKeyName);
    }
    public void SetRoleCriteria(Dictionary<string, string> oRptCriteria, string criteriaKeyName)
    {
        this.ucRoles.SetCriteria(oRptCriteria, criteriaKeyName);
    }
    
    public string SetDefaultSelectedRoles
    {
        set
        {
            this.ucRoles.SetDefaultSelectedIDs = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnFetchUser_Click(object sender, EventArgs e)
    {
        List<short> selectedRoleIDs = this.ucRoles.GetSelectedIDs;
        if (btnFetchUserHandler != null)
            btnFetchUserHandler(selectedRoleIDs);
    }
}
