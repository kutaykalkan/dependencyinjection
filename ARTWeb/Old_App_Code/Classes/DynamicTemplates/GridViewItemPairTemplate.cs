using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using System.Data;

namespace SkyStem.ART.Web.Classes
{
    /// <summary>
    /// Summary description for GridViewItemPairTemplate
    /// </summary>
    public class GridViewItemPairTemplate : ITemplate
    {
        private Repeater _RepeaterSource1;
        /// <summary>
        /// Repeater for Source1
        /// </summary>
        public Repeater RepeaterSource1
        {
            get { return _RepeaterSource1; }
        }

        private Repeater _RepeaterSource2;
        /// <summary>
        /// Repeater for Source2
        /// </summary>
        public Repeater RepeaterSource2
        {
            get { return _RepeaterSource2; }
        }

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
        private string _XPathSource1;
        /// <summary>
        /// XPath for selecting the data for column from source1
        /// </summary>
        public string XPathSource1
        {
            get { return _XPathSource1; }
            set { _XPathSource1 = value; }
        }

        private string _XPathSource2;
        /// <summary>
        ///  XPath for selecting the data for column from source1
        /// </summary>
        public string XPathSource2
        {
            get { return _XPathSource2; }
            set { _XPathSource2 = value; }
        }

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

        #region DataTable Binding Mode Properties
        /// <summary>
        /// Data Relation Name from Parent to Source 1
        /// </summary>
        public string RelationNameSource1 { get; set; }

        /// <summary>
        /// Data Relation Name from Parent to Source 2
        /// </summary>
        public string RelationNameSource2 { get; set; }
        #endregion

        private string _CssClassSource1;
        /// <summary>
        /// Css Class for Source 1
        /// </summary>
        public string CssClassSource1
        {
            get { return _CssClassSource1; }
            set { _CssClassSource1 = value; }
        }

        private string _CssClassSource2;
        /// <summary>
        /// Css Class for Source 2
        /// </summary>
        public string CssClassSource2
        {
            get { return _CssClassSource2; }
            set { _CssClassSource2 = value; }
        }


        /// <summary>
        /// Constructor for Template
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="columnName"></param>
        /// <param name="columnID"></param>
        public GridViewItemPairTemplate(ListItemType itemType, string columnName, string dataFieldName, long columnID, WebEnums.DataType eDataType)
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
                // Get Data Grid Item
                   GridDataItem oGridDataItem = container.NamingContainer as GridDataItem;
                   int Source1Rows = 0;
                   int Source2Rows = 0;
                   MatchingHelper.GetRowsCount(oGridDataItem, out Source1Rows, out Source2Rows);               
                // Create Container Table
                    Table oTable = new Table();
                    container.Controls.Add(oTable);
                    oTable.CssClass = CssClassSource1;
                    oTable.Style.Add("disabled", "disabled");

                 // Create Table Row for Source 1
                    TableRow oTableRowSource1 = new TableRow();
                    oTableRowSource1.CssClass = CssClassSource1;
                    oTableRowSource1.Style.Add("disabled", "disabled");

                    oTable.Controls.Add(oTableRowSource1);
                    TableCell oTableCellSource1 = new TableCell();
                    oTableCellSource1.CssClass = CssClassSource1;
                    oTableCellSource1.Style.Add("disabled", "disabled");
                    oTableRowSource1.Controls.Add(oTableCellSource1);
                   
                    // Create Repeater for Source 1
                    _RepeaterSource1 = CreateRepeator(CssClassSource1);
                    oTableCellSource1.Controls.Add(_RepeaterSource1);
                    BindRepeator(_RepeaterSource1, oGridDataItem, _XPathSource1, RelationNameSource1, Source1Rows);

                    // Create Table Row for Source 2
                    TableRow oTableRowSource2 = new TableRow();
                    oTableRowSource2.CssClass = CssClassSource2;
                    oTable.Controls.Add(oTableRowSource2);
                    oTableRowSource2.Style.Add("disabled", "disabled");
                    TableCell oTableCellSource2 = new TableCell();
                    oTableCellSource2.CssClass = CssClassSource2;
                    oTableCellSource2.Style.Add("disabled", "disabled");
                    oTableRowSource2.Controls.Add(oTableCellSource2);
                    // Create Repeator for Source 2
                    _RepeaterSource2 = CreateRepeator(CssClassSource2);
                    oTableCellSource2.Controls.Add(_RepeaterSource2);
                    BindRepeator(_RepeaterSource2, oGridDataItem, _XPathSource2, RelationNameSource2, Source2Rows);
                  
            }
        }

       

        /// <summary>
        /// Create Repeator for Source
        /// </summary>
        /// <returns></returns>
        private Repeater CreateRepeator(string CssClassName)
        {
            Repeater rpt = new Repeater();
            RepeaterItemTemplate rptItemTemplate = new RepeaterItemTemplate(_itemType, _columnName, _dataFieldName, _columnID, _columnDataType);
            rptItemTemplate.XPathValue = _XPathValue;
            rptItemTemplate.BindingMode = _BindingMode;
            rptItemTemplate.CssClass = CssClassName;
            rpt.ItemTemplate = rptItemTemplate;
            return rpt;
        }

        /// <summary>
        /// Bind Repeator
        /// </summary>
        /// <param name="rpt"></param>
        /// <param name="oGridDataItem"></param>
        private void BindRepeator(Repeater rpt, GridDataItem oGridDataItem, string xPathSource, string relationName, int rowCount)
        {
            if (_BindingMode == WebEnums.BindingModes.XmlBinding)
                rpt.DataSource = XPathBinder.Select(oGridDataItem.DataItem, xPathSource);
            else
            {
                if (oGridDataItem.DataItem != null)
                {
                    DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;
                    DataRow[] childRowsSource;
                    DataRow[] SourceRows = new DataRow[rowCount]; 
                    childRowsSource = dr.GetChildRows(relationName);
                    for (int i = 0; i < childRowsSource.Length ; i++)
                    {
                        SourceRows[i] = childRowsSource[i];
                        if ((i + 1) == rowCount)
                            break;
                    }
                    //rpt.DataSource = dr.GetChildRows(relationName);
                    rpt.DataSource = SourceRows;
                }
                else
                {
                    rpt.DataSource = null;
                }
            }
            rpt.DataBind();
        }

        #endregion
    }
}