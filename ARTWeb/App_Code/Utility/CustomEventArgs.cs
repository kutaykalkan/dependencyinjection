using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for CustomEventArgs
    /// </summary>
    public class CustomEventArgs
    {
        public CustomEventArgs()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
    public class ListItemEventArgs : EventArgs
    {
        public List<ListItem> ListOfListItems;

        public void AddListItem(ListItem item)
        {
            ListOfListItems.Add(item);
        }
        public void RemoveListItem(ListItem item)
        {
            if (ListOfListItems.Contains(item))
                ListOfListItems.Remove(item);
        }
    }
}
