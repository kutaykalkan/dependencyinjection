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
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;

namespace SkyStem.ART.Web.UserControls
{
    public delegate void SelectedListItemsEventHandler(object sender, ListItemEventArgs e);
    public partial class UserControls_RoleSelection : UserControlBase
    {
        #region "Private Properties"
        private short _displayAvailableRows = 5;
        private short _displaySelectedRows = 5;
        private int _availableListLableId;
        private int _selectedListLableId;
        private List<ListItem> _lstListItem;
        private int _defaultWidth = 100;
        private bool _isRequired = false;
        private string _errorMessage;
        #endregion

        #region "Public Properties"
        /// <summary>
        /// sets error message to be shown when control valdation fails
        /// </summary>
        public string ErrorMessage
        {
            set
            {
                this._errorMessage = value;
            }
        }
        private string ddlvalue;
        public string setDropDownFromPage
        {
            get
            {
                return this.ddlvalue;
            }
            set
            {
                this.ddlvalue = value;
            }
        }
        /// <summary>
        /// This event can be handeled at Page level to bind controls like drop down list with selected ListItems
        /// It takes 2 parameters, this object as object, List of Selected ListItems as ListItemEventArgs.
        /// ListItemEventArgs contains all ListItems in Selected Items Listbox.
        /// </summary>
        public event SelectedListItemsEventHandler SelectedListItemsChange;
        /// <summary>
        /// Field represented by this control is a required field or not.
        /// </summary>
        public bool IsRequired
        {
            get
            {
                return this._isRequired;
            }
            set
            {
                this._isRequired = value;
            }
        }
        /// <summary>
        /// Default width of listbox control when it is empty
        /// </summary>
        public int DefaultWidth
        {
            set
            {
                this._defaultWidth = value;
            }
        }
        /// <summary>
        /// List of listitems to be defined as datasource of "Available Items" list box
        /// </summary>
        public List<ListItem> ListItems
        {
            set
            {
                this._lstListItem = value;
            }
        }
        /// <summary>
        /// Number of listItems to be displayed in "Available Items" listbox
        /// </summary>
        public short DisplayAvailableRows
        {
            set
            {
                _displayAvailableRows = value;
            }
        }
        /// <summary>
        /// Number of listitems to be displayed in "Selected Items" listBox
        /// </summary>
        public short DisplaySelectedRows
        {
            set
            {
                _displaySelectedRows = value;
            }
        }
        /// <summary>
        /// Caption for "Available Items" listbox
        /// </summary>
        public int AvailableListLableId
        {
            set
            {
                this._availableListLableId = value;
            }
        }
        /// <summary>
        /// Caption for "Selected Items" listbox
        /// </summary>
        public int SelectedListLableId
        {
            set
            {
                this._selectedListLableId = value;
            }
        }

        #endregion

        #region "Page Methods"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.lstUserRoles.DataSource = this._lstListItem;
                this.lstUserRoles.DataTextField = "text";
                this.lstUserRoles.DataValueField = "value";
                this.DataBind();
                //this.cvSelectedUserRoles.ErrorMessage = this._errorMessage ?? string.Empty;
            }
            //btnSelect.Attributes.Add("onclick", "return SelectRoles(" + "'" + lstUserRoles.ClientID + "/" + lstSelectedUserRoles.ClientID + "'" + ")");

            btnSelect.Value = LanguageUtil.GetValue(1261) + " > ";
            btnSelect.Attributes.Add("onclick", "javascript:SelectRoles('"
          + lstUserRoles.ClientID + "', '"
          + lstSelectedUserRoles.ClientID + "', '" + '1' + "'); " + setDropDownFromPage + ";");

            btnSelectAll.Value = LanguageUtil.GetValue(1262) + " >> ";
            btnSelectAll.Attributes.Add("onclick", "javascript:SelectRoles('"
          + lstUserRoles.ClientID + "', '"
          + lstSelectedUserRoles.ClientID + "', '" + '2' + "');" + setDropDownFromPage + ";");

            btnUnSelect.Value = "<" + " " + LanguageUtil.GetValue(1261);
            btnUnSelect.Attributes.Add("onclick", "javascript:SelectRoles('"
          + lstUserRoles.ClientID + "', '"
          + lstSelectedUserRoles.ClientID + "', '" + '3' + "');" + setDropDownFromPage + ";");

            btnUnSelectAll.Value = "<<" + " " + LanguageUtil.GetValue(1262);
            btnUnSelectAll.Attributes.Add("onclick", "javascript:SelectRoles('"
          + lstUserRoles.ClientID + "', '"
          + lstSelectedUserRoles.ClientID + "', '" + '4' + "');" + setDropDownFromPage + ";");

        }
        protected void Page_Init(object sender, EventArgs e)
        {
            this.lstUserRoles.Rows = this._displayAvailableRows;
            this.lstSelectedUserRoles.Rows = this._displaySelectedRows;

            this.lblAvailableRolesCaption.LabelID = this._availableListLableId;
            this.lblSelectedRolesCaption.LabelID = this._selectedListLableId;

            this.lstSelectedUserRoles.CausesValidation = (this.IsRequired);

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //if (this.lstUserRoles.Items.Count < 1)
            //{
            //    this.lstUserRoles.Width = new Unit(this._defaultWidth);
            //}
            //else
            //{
            //    this.lstUserRoles.Width = new Unit("");
            //}
            //if (this.lstSelectedUserRoles.Items.Count < 1)
            //{
            //    this.lstSelectedUserRoles.Width = new Unit(this._defaultWidth);
            //}
            //else
            //{
            //    this.lstSelectedUserRoles.Width = new Unit("");
            //}
        }
        #endregion

        #region "Control Functionality"
        protected void OnSelectedListItemsChange(ListItemEventArgs args)
        {
            if (SelectedListItemsChange != null)
            {
                SelectedListItemsChange(this, args);
            }
        }
        //protected void btnSelect_Click(object sender, EventArgs e)
        //{
        //    List<ListItem> lstSelectedItems = this.GetSelectedItems(this.lstUserRoles);
        //    this.AddItemsToListbox(this.lstSelectedUserRoles, lstSelectedItems);
        //    this.RemoveItemsFromListBox(this.lstUserRoles, lstSelectedItems);
        //    this.lstSelectedUserRoles.ClearSelection();

        //    ListItemEventArgs args = new ListItemEventArgs();
        //    args.ListOfListItems = this.GetSelectedItems();
        //    this.OnSelectedListItemsChange(args);
        //}
        protected void fAll_Click(object sender, EventArgs e)
        {
            this.MoveAllItems(lstUserRoles, lstSelectedUserRoles);
            this.lstSelectedUserRoles.ClearSelection();

            ListItemEventArgs args = new ListItemEventArgs();
            args.ListOfListItems = this.GetSelectedItems();
            this.OnSelectedListItemsChange(args);

        }
        protected void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            this.MoveAllItems(lstSelectedUserRoles, lstUserRoles);
            this.lstUserRoles.ClearSelection();

            ListItemEventArgs args = new ListItemEventArgs();
            args.ListOfListItems = this.GetSelectedItems();
            this.OnSelectedListItemsChange(args);
        }
        protected void btnUnSelect_Click(object sender, EventArgs e)
        {
            List<ListItem> lstSelectedItems = this.GetSelectedItems(this.lstSelectedUserRoles);
            this.AddItemsToListbox(this.lstUserRoles, lstSelectedItems);
            this.RemoveItemsFromListBox(this.lstSelectedUserRoles, lstSelectedItems);
            this.lstUserRoles.ClearSelection();

            ListItemEventArgs args = new ListItemEventArgs();
            args.ListOfListItems = this.GetSelectedItems();
            this.OnSelectedListItemsChange(args);
        }

        #endregion

        #region "Private Methods"
        /// <summary>
        /// gets a list of all Selected ListItems from specified Listbox
        /// </summary>
        /// <param name="lstBox">Listbox object</param>
        /// <returns>List of Selected ListItems</returns>
        private List<ListItem> GetSelectedItems(ListBox lstBox)
        {
            int[] arrSelectedIndices = lstBox.GetSelectedIndices();
            List<ListItem> lstListItem = new List<ListItem>();
            for (int i = 0; i < arrSelectedIndices.Length; i++)
            {
                lstListItem.Add(lstBox.Items[arrSelectedIndices[i]]);
            }
            return lstListItem;
        }
        /// <summary>
        /// removes list of listitems from listbox specified in first parameter
        /// </summary>
        /// <param name="lstbx">listbox from which listitems to be removed</param>
        /// <param name="lstListItem">list of ListItems to be removed</param>
        private void RemoveItemsFromListBox(ListBox lstbx, List<ListItem> lstListItem)
        {
            foreach (ListItem lstItem in lstListItem)
            {
                if (lstbx.Items.FindByValue(lstItem.Value) != null)
                {
                    lstbx.Items.Remove(lstItem);
                }
            }
        }


        public void RemoveaddItemsFromListBoxonvalue(List<int> ListitemValue, bool ForCompanyAdd)
        {
            for (int i = 0; i < lstUserRoles.Items.Count; i++)
            {
                foreach (int obj in ListitemValue)
                {

                    if (lstUserRoles.Items[i].Value == obj.ToString())
                    {
                        ListItem li = new ListItem();
                        li = lstUserRoles.Items[i];
                        lstUserRoles.Items.Remove(li);
                        if (_lstListItem != null && this._lstListItem.Contains(li) && ForCompanyAdd)
                            this._lstListItem.Remove(li);
                        if (lstSelectedUserRoles.Items.FindByValue(li.Value) == null)
                            lstSelectedUserRoles.Items.Add(li);
                    }
                }
            }
        }


        /// <summary>
        /// Adds listitems to listbox 
        /// </summary>
        /// <param name="lstbx">listbox to which items needs to be added</param>
        /// <param name="lstListItem">list of listitems to be added to listbox</param>
        private void AddItemsToListbox(ListBox lstbx, List<ListItem> lstListItem)
        {
            foreach (ListItem lstItem in lstListItem)
            {
                if (lstbx.Items.FindByValue(lstItem.Value) == null)
                {
                    lstbx.Items.Add(lstItem);
                }
            }
            lstbx.ClearSelection();
        }
        /// <summary>
        /// moves all listitems from one listbox to another
        /// </summary>
        /// <param name="fromListBox">listbox from which listitems needs to be removed</param>
        /// <param name="ToListBox">listbox to which listitems needs to be added</param>
        private void MoveAllItems(ListBox fromListBox, ListBox ToListBox)
        {
            ListItem[] arrListItem = new ListItem[fromListBox.Items.Count];
            fromListBox.Items.CopyTo(arrListItem, 0);
            fromListBox.Items.Clear();
            ToListBox.Items.AddRange(arrListItem);
        }
        #endregion

        #region "Public Methods"
        /// <summary>
        /// This method return selected ListItems from Selected values listbox
        /// </summary>
        /// <returns>List of selected ListItems</returns>
        public List<ListItem> GetSelectedItems()
        {
            List<ListItem> lstListItems = new List<ListItem>();
            foreach (ListItem newItem in this.lstSelectedUserRoles.Items)
            {
                lstListItems.Add(newItem);
            }
            return lstListItems;
        }
        /// <summary>
        /// This method returns ListItem collection of all items in Selection values listbox.
        /// </summary>
        /// <returns>ListItemCollection</returns>
        public ListItemCollection GetSelectedItemsCollection()
        {
            return this.lstSelectedUserRoles.Items;
        }

        public void ClearSelectedListBox()
        {
            lstSelectedUserRoles.Items.Clear();
        }

        public void BindAgain()
        {
            this.lstUserRoles.DataSource = this._lstListItem;
            this.lstUserRoles.DataTextField = "text";
            this.lstUserRoles.DataValueField = "value";
            this.DataBind();
        }
        #endregion
    }
}
