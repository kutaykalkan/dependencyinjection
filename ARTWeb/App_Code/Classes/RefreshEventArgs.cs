using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyStem.ART.Web.Classes
{
    /// <summary>
    /// Summary description for RefreshEventArgs
    /// </summary>
    public class RefreshEventArgs : EventArgs
    {
        public RefreshEventArgs()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

    public delegate void RefreshEventHandler(object sender, RefreshEventArgs args);
}