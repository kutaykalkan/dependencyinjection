using SkyStem.ART.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SkyStem.ART.Web.Classes
{

    /// <summary>
    /// Summary description for UserControlWebPartBase
    /// </summary>
    public class UserControlWebPartBase : System.Web.UI.UserControl
    {
        private Boolean? _LoadData = false;
        public Boolean? LoadData
        {
            get { return _LoadData; }
            set { _LoadData = value; }
        }

        public UserControlWebPartBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public virtual IAsyncResult GetDataAsync()
        {
            return null;
        }
        public virtual void DataLoaded(IAsyncResult result){ }
    }
}