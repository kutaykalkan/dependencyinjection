using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

public class SelectionChangedEventArgs : EventArgs
{
    private DropDownList _list;
    public SelectionChangedEventArgs(DropDownList list)
        : base()
    {
        this._list = list;
    }
    public DropDownList DropDown
    {
        get
        {
            return this._list;
        }

    }
    public ListItem SelectedItem
    {
        get
        {
            return this._list.SelectedItem;
        }
    }

}
