using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using Telerik.Web.UI;

namespace SkyStem.ART.Web.Classes
{
    /// <summary>
    /// Summary description for GridSettings
    /// </summary>
    public class GridSettings
    {
        public int? PageSize { get; set; }
        public string sortExpression { get; set; }
        public GridSortOrder sortDirection { get; set; }
        public List<FilterCriteria> oFilterCriteriaCollection { get; set; }     
        public GridSettings()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}