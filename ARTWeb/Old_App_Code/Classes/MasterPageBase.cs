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
using System.Globalization;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using System.Collections.Generic;
using SkyStem.Library.Controls.TelerikWebControls;

namespace SkyStem.ART.Web.Classes
{
    /// <summary>
    /// Summary description for MasterPageBase
    /// </summary>
    public class MasterPageBase : MasterPage
    {
        public event EventHandler FinancialYearChangedEventHandler;
        public event EventHandler ReconciliationPeriodChangedEventHandler;
        public event EventHandler CompanyChangedEventHandler;
        public event RefreshEventHandler RefreshRequested;

        public MasterPageBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public virtual void SetPageTitle(int LabelID)
        {
        }

        public virtual void SetPageTitle(string labeltext)
        {
        }

        public virtual void SetBreadcrumbs(string path)
        {
        }

        /// <summary>
        /// Show the Notes / Input Requirements Sections
        /// </summary>
        /// <param name="oLabelIDCollection"></param>
        public virtual void ShowInputRequirements(int[] oLabelIDCollection)
        {
        }

        public virtual void ShowRequirement(int label, int[] oLabelIDCollection)
        {
            
        }

        /// <summary>
        /// Format and Show Error Message
        /// </summary>
        /// <param name="errorMessage"></param>
        public virtual void ShowErrorMessage(Exception ex)
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
        /// Format and Show Error Message with no bullet
        /// </summary>
        /// <param name="errorMessageLabelID"></param>
        public virtual void ShowErrorMessageWithNoBullet(string errorMessage)
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
        /// Hide Confirmation Message
        /// </summary>
        public virtual void HideMessage()
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

        public void RaiseFinancialYearChangedEvent(object sender, EventArgs e)
        {
            if (FinancialYearChangedEventHandler != null)
            {
                FinancialYearChangedEventHandler(sender, e);
            }
        }

        public void RaiseReconciliationPeriodChangedEvent(object sender, EventArgs e)
        {
            if (ReconciliationPeriodChangedEventHandler != null)
            {
                ReconciliationPeriodChangedEventHandler(sender, e);
            }
        }

        public void RaiseCompanyChangedEvent(object sender, EventArgs e)
        {
            if (CompanyChangedEventHandler != null)
            {
                CompanyChangedEventHandler(sender, e);
            }
        }

        /// <summary>
        /// Clear Cache and Reload the Rec Periods after 
        /// </summary>
        public virtual void ReloadRecPeriods()
        {
        }

        /// <summary>
        /// Clear Cache and Reload the Rec Periods after 
        /// </summary>
        public virtual void ReloadRecPeriods(bool raiseEvent)
        {
        }

        /// <summary>
        /// Clear Cache and Reload the Company Dropdown
        /// </summary>
        public virtual void ReloadCompanies(int? CompanyID)
        {
        }
        
        public virtual ScriptManager GetScriptManager()
        {
            return null;
        }

        public virtual void RegisterPostBackToControls(Control oControl)
        {
        }

        public virtual void SetMasterPageSettings(MasterPageSettings oMasterPageSettings)
        {
        }

        public void RaiseRefreshRequested(object sender, RefreshEventArgs args)
        {
            if (RefreshRequested != null)
            {
                RefreshRequested.Invoke(sender, args);
            }
        }
    }//end of class
}//end of namespace