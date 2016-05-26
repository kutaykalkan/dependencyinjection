using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Web.Data;
using System.Data;
using SkyStem.ART.Web.Utility;

namespace SkyStem.ART.Web.Classes
{
    /// <summary>
    /// Summary description for GridViewItemTemplate
    /// </summary>
    public class GridViewItemTemplate : ITemplate
    {

        private ListItemType _itemType;
        /// <summary>
        /// Column Name to display the Name
        /// </summary>
        private string _columnName;
        public string ColumnName
        {
            get { return _columnName; }
        }

        private string _dataFieldName;
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

        private WebEnums.BindingModes _BindingMode = WebEnums.BindingModes.DataTableBinding;

        /// <summary>
        /// Binding Mode
        /// </summary>
        public WebEnums.BindingModes BindingMode
        {
            get { return _BindingMode; }
            set { _BindingMode = value; }
        }

        #region XML Binding Mode Properties
        private string _XPathValue;
        /// <summary>
        /// XPath for selecting the Value
        /// </summary>
        public string XPathValue
        {
            get { return _XPathValue; }
            set { _XPathValue = value; }
        }
        #endregion

        /// <summary>
        /// Constructor for Template
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="columnName"></param>
        /// <param name="columnID"></param>
        public GridViewItemTemplate(ListItemType itemType, string columnName, string dataFieldName, long columnID, WebEnums.DataType eDataType)
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
                Literal ltData = new Literal();
                ltData.DataBinding += new EventHandler(OnLiteral_DataBinding);
                container.Controls.Add(ltData);
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
            GridDataItem oItem = (GridDataItem)oLiteral.NamingContainer;
            object dataValue = null;
            if (BindingMode == WebEnums.BindingModes.XmlBinding)
            {
                dataValue = XPathBinder.Eval(oItem.DataItem, XPathValue, "");
            }
            else
            {
                dataValue = DataBinder.Eval(oItem.DataItem, DataFieldName);
            }
            if (dataValue != null)
                oLiteral.Text = Helper.GetDisplayValue(dataValue, ColumnDataType);
        }

        #endregion
    }
}