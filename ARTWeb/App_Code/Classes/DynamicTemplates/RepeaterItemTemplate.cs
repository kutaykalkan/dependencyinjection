using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Data;
using System.Data;
using SkyStem.ART.Web.Utility;

namespace SkyStem.ART.Web.Classes
{
    /// <summary>
    /// Summary description for RepeaterItemTemplate
    /// </summary>
    public class RepeaterItemTemplate : ITemplate
    {

        private WebEnums.BindingModes _BindingMode = WebEnums.BindingModes.DataTableBinding;

        /// <summary>
        /// Binding Mode
        /// </summary>
        public WebEnums.BindingModes BindingMode
        {
            get { return _BindingMode; }
            set { _BindingMode = value; }
        }

        private ListItemType _itemType;

        private string _columnName;
        /// <summary>
        /// Column Name
        /// </summary>
        public string ColumnName
        {
            get { return _columnName; }
        }

        private string _dataFieldName;
        /// <summary>
        /// Data Field to display the value
        /// </summary>
        public string DataFieldName
        {
            get { return _dataFieldName; }
        }

        private long _columnID;
        /// <summary>
        /// Column ID
        /// </summary>
        public long ColumnID
        {
            get { return _columnID; }
        }

        private WebEnums.DataType _columnDataType;
        /// <summary>
        /// Column Data Type
        /// </summary>
        public WebEnums.DataType ColumnDataType
        {
            get { return _columnDataType; }
        }

        private string _XPathValue;
        /// <summary>
        /// XPath to get Value
        /// </summary>
        public string XPathValue
        {
            get { return _XPathValue; }
            set { _XPathValue = value; }
        }

        private string _CssClass;
        /// <summary>
        /// Css Class
        /// </summary>
        public string CssClass
        {
            get { return _CssClass; }
            set { _CssClass = value; }
        }
        /// <summary>
        /// Constructor for Repeater Template
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="columnName"></param>
        /// <param name="columnID"></param>
        public RepeaterItemTemplate(ListItemType itemType, string columnName, string dataFieldName, long columnID, WebEnums.DataType eDataType)
        {
            _itemType = itemType;
            _columnName = columnName;
            _dataFieldName = dataFieldName;
            _columnID = columnID;
            _columnDataType = eDataType;
        }

        #region ITemplate Members
        /// <summary>
        /// Interface method
        /// </summary>
        /// <param name="container"></param>
        public void InstantiateIn(Control container)
        {
            if (_itemType == ListItemType.Item || _itemType == ListItemType.AlternatingItem)
            {
                // Table is being used for providing indentation
                Table oTable = new Table();
                TableRow oTableRow = new TableRow();
                oTable.Controls.Add(oTableRow);
                TableCell oTableCell = new TableCell();
                oTableRow.Controls.Add(oTableCell);
                Literal oLiteral = new Literal();
                oLiteral.DataBinding += new EventHandler(OnLiteral_DataBinding);
                oTableCell.Controls.Add(oLiteral);
                if (!string.IsNullOrEmpty(_CssClass))
                    oTableCell.CssClass = _CssClass;
                container.Controls.Add(oTable);
            }
        }
        /// <summary>
        /// Display Data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnLiteral_DataBinding(object sender, EventArgs e)
        {
            Literal oLiteral = (Literal)sender;
            RepeaterItem rptItem = (RepeaterItem)oLiteral.NamingContainer;
            object dataValue = null;
            if (BindingMode == WebEnums.BindingModes.XmlBinding)
            {
                dataValue = XPathBinder.Eval(rptItem.DataItem, XPathValue, "");
            }
            else
            {
                DataRow dr = rptItem.DataItem as DataRow;
                if (dr != null)
                    dataValue = dr[DataFieldName];
                else
                    dataValue = DataBinder.Eval(rptItem.DataItem, DataFieldName);
            }
            if (dataValue != null)
                oLiteral.Text = Helper.GetDisplayValue(dataValue, ColumnDataType);
        }

        #endregion
    }
}