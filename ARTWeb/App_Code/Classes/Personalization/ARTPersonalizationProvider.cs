using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using SkyStem.ART.Web.Utility;

namespace SkyStem.ART.Web.Classes
{
    /// <summary>
    /// Summary description for ARTPersonalizationProvider
    /// </summary>
    public class ARTPersonalizationProvider : SqlPersonalizationProvider
    {
        public ARTPersonalizationProvider()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override void LoadPersonalizationBlobs(WebPartManager webPartManager, string path, string userName, ref byte[] sharedDataBlob, ref byte[] userDataBlob)
        {
            string userNameWithRole = userName + "_" + SessionHelper.CurrentRoleID.GetValueOrDefault();
            base.LoadPersonalizationBlobs(webPartManager, path, userNameWithRole, ref sharedDataBlob, ref userDataBlob);
        }

        protected override void SavePersonalizationBlob(WebPartManager webPartManager, string path, string userName, byte[] dataBlob)
        {
            string userNameWithRole = userName + "_" + SessionHelper.CurrentRoleID.GetValueOrDefault();
            base.SavePersonalizationBlob(webPartManager, path, userNameWithRole, dataBlob);
        }

        protected override void ResetPersonalizationBlob(WebPartManager webPartManager, string path, string userName)
        {
            string userNameWithRole = userName + "_" + SessionHelper.CurrentRoleID;
            base.ResetPersonalizationBlob(webPartManager, path, userNameWithRole);
        }
    }
}