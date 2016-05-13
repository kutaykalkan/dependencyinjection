using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.Design;

namespace SkyStem.ART.Web.Classes.UserControl
{
    /// <summary>
    /// Summary description for UserControlWizardBase
    /// </summary>
    public class UserControlWizardBase : UserControlBase
    {

        #region Properties

        /// <summary>
        /// For Setting the Form Mode
        /// </summary>
        public virtual bool IsEditMode { get; set; }

        /// <summary>
        /// For Detecting Changes
        /// </summary>
        public virtual bool IsDataChanged { get; set; }

        /// <summary>
        /// Is Clear Data required for dependents
        /// </summary>
        public virtual bool IsClearDataRequired { get; set; }

        /// <summary>
        /// For Setting Step Title
        /// </summary>
        public virtual int TitlePhraseID { get; set; }

        #endregion

        #region Constructor

        public UserControlWizardBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion

        #region Virtual Functions

        /// <summary>
        /// Override this method for cleaning the data in Wizard Step
        /// </summary>
        public virtual void ClearData()
        {
        }

        /// <summary>
        /// Override this method for Loading the data in Wizard Step
        /// </summary>
        public virtual void LoadData()
        {
        }

        /// <summary>
        /// Set Control State after Data Load
        /// </summary>
        public virtual void SetControlStatePostLoad()
        {
        }

        /// <summary>
        /// Override this method for Saving the data in Wizard Step
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveData()
        {
            return true;
        }

        /// <summary>
        /// Override this method to check whether step can be loaded or not
        /// </summary>
        /// <returns></returns>
        public virtual bool CanLoadStep()
        {
            return true;
        }

        /// <summary>
        /// Override this method for validations before saving the Wizard Step Data
        /// </summary>
        /// <returns></returns>
        public virtual bool CanSaveData()
        {
            return true;
        }

        /// <summary>
        /// Override this method to restrict move forward from current step
        /// </summary>
        /// <returns></returns>
        public virtual bool CanMoveForward()
        {
            return true;
        }

        /// <summary>
        /// Override this method to restrict move backward from current step
        /// </summary>
        /// <returns></returns>
        public virtual bool CanMoveBackward()
        {
            return true;
        }

        /// <summary>
        /// Override this method to clear data from database and clear session
        /// </summary>
        public virtual void ClearControls()
        {
        }

        /// <summary>
        /// Refresh Data in the current control
        /// </summary>
        public virtual void RefreshData()
        {
        }

        /// <summary>
        /// Override this method to submit the data in Wizard Step 6
        /// </summary>
        public virtual bool SubmitData()
        {
            return true;
        }
        #endregion
    }
}
