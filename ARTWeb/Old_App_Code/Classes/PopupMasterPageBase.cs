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

namespace SkyStem.ART.Web.Classes
{

    /// <summary>
    /// Summary description for PopupMasterPageBase
    /// </summary>
    public class PopupMasterPageBase : MasterPage
    {
        public PopupMasterPageBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Show the Notes / Input Requirements Sections
        /// </summary>
        /// <param name="oLabelIDCollection"></param>
        public virtual void ShowInputRequirements(int[] oLabelIDCollection)
        {
        }

        /// <summary>
        /// Format and Show Error Message
        /// </summary>
        /// <param name="errorMessage"></param>
        public virtual void ShowErrorMessage(string errorMessage)
        {
        }

        /// <summary>
        /// Format and Show Error Message
        /// </summary>
        /// <param name="errorMessageLabelID"></param>
        public virtual void ShowErrorMessage(int errorMessageLabelID)
        {
        }


        /// <summary>
        /// Hide Error Message
        /// </summary>
        public virtual void HideErrorMessage()
        {
        }

        /// <summary>
        /// Show Confirmation Message
        /// </summary>
        /// <param name="message"></param>
        public virtual void HideConfirmationMessage()
        {
        }

        /// <summary>
        /// Show Confirmation Message
        /// </summary>
        /// <param name="message"></param>
        public virtual void ShowConfirmationMessage(string message)
        {
        }

        /// <summary>
        /// Show Confirmation Message
        /// </summary>
        /// <param name="confirmationMessageLabelID"></param>
        public virtual void ShowConfirmationMessage(int confirmationMessageLabelID)
        {
        }


    }
}