using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.Library.Controls.TelerikWebControls;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;

namespace SkyStem.ART.Web.Classes
{

    /// <summary>
    /// Summary description for UserControlRecItemBase
    /// </summary>
    public class UserControlRecItemBase : System.Web.UI.UserControl
    {

        public UserControlRecItemBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region GLDataHdr Properties

        private GLDataHdrInfo _GLDataHdrInfo = null;
        public GLDataHdrInfo GLDataHdrInfo
        {
            get
            {
                return GetGLDataHdrObject();
            }
            set
            {
                SetGLDataHdrObject(value);
            }
        }

        public string CurrentBCCY
        {
            get
            {
                string _CurrentBCCY = string.Empty;
                if (GLDataHdrInfo != null)
                    _CurrentBCCY = GLDataHdrInfo.BaseCurrencyCode;
                return _CurrentBCCY;
            }
        }

        public WebEnums.ReconciliationStatus GLRecStatus
        {
            get
            {
                WebEnums.ReconciliationStatus _GLRecStatus = WebEnums.ReconciliationStatus.NotStarted;
                if (GLDataHdrInfo != null && GLDataHdrInfo.ReconciliationStatusID.HasValue)
                    _GLRecStatus = (WebEnums.ReconciliationStatus)GLDataHdrInfo.ReconciliationStatusID.Value;
                return _GLRecStatus;
            }
        }

        public long? AccountID
        {
            get
            {
                if (GLDataHdrInfo != null)
                    return (long)GLDataHdrInfo.AccountID;
                return null;
            }
        }

        public int? NetAccountID
        {
            get
            {
                if (GLDataHdrInfo != null)
                    return (int)GLDataHdrInfo.NetAccountID;
                return null;
            }
        }

        public long? GLDataID
        {
            get
            {
                if (GLDataHdrInfo != null)
                    return GLDataHdrInfo.GLDataID;
                return null;
            }
        }

        public bool? IsSRA
        {
            get
            {
                if (GLDataHdrInfo != null)
                    return GLDataHdrInfo.IsSystemReconcilied.GetValueOrDefault();
                return false;
            }
        }

        #endregion

        public int? EntityNameLabelID
        {
            get
            {
                if (ViewState[ViewStateConstants.ENTITY_NAME_LABEL_ID] != null)
                    return (int)ViewState[ViewStateConstants.ENTITY_NAME_LABEL_ID];
                return null;
            }
            set { ViewState[ViewStateConstants.ENTITY_NAME_LABEL_ID] = value; }
        }

        public short? RecCategoryTypeID
        {
            get
            {
                if (ViewState[ViewStateConstants.REC_CATEGORY_TYPE_ID] != null)
                    return (short)ViewState[ViewStateConstants.REC_CATEGORY_TYPE_ID];
                return null;
            }
            set { ViewState[ViewStateConstants.REC_CATEGORY_TYPE_ID] = value; }
        }

        public short? RecCategoryID
        {
            get
            {
                if (ViewState[ViewStateConstants.REC_CATEGORY_ID] != null)
                    return (short)ViewState[ViewStateConstants.REC_CATEGORY_ID];
                else return null;
            }
            set { ViewState[ViewStateConstants.REC_CATEGORY_ID] = value; }
        }

        public virtual bool DisableExportInPrint
        {
            set;
            get;
        }

        public virtual bool IsRefreshData
        {
            get;
            set;
        }

        public virtual bool IsExpanded
        {
            get;
            set;
        }

        public virtual void LoadData()
        {
        }

        public bool IsPrintMode
        {
            get;
            set;
        }

        public void DisableCommandItemForPrint(ExRadGrid rg)
        {
            rg.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            rg.AllowExportToExcel = false;
            rg.AllowExportToPDF = false;
        }

        public void HandlePrintMode(HtmlTableRow trOpenItemsButtonRow, HtmlTableRow trClosedItemsButtonRow, ExRadGrid rg)
        {
            if (this.IsPrintMode)
            {
                trOpenItemsButtonRow.Visible = false;
                if (trClosedItemsButtonRow != null)
                {
                    trClosedItemsButtonRow.Visible = false;
                }

                // Hide Grid Columns
                GridColumn oGridColumn = rg.Columns.FindByUniqueNameSafe("ExportFileIcon");
                if (oGridColumn != null)
                    oGridColumn.Visible = false;

                oGridColumn = rg.Columns.FindByUniqueNameSafe("ShowInputForm");
                if (oGridColumn != null)
                    oGridColumn.Visible = false;

                oGridColumn = rg.Columns.FindByUniqueNameSafe("DeleteColumn");
                if (oGridColumn != null)
                    oGridColumn.Visible = false;
            }
        }

        public void HandlePrintMode(HtmlTableRow trOpenItemsButtonRow, ExRadGrid rg)
        {
            HandlePrintMode(trOpenItemsButtonRow, null, rg);
        }

        public virtual void ExpandCollapse()
        {
        }

        public virtual void OnGLDataIDChanged()
        {
        }

        private GLDataHdrInfo GetGLDataHdrObject()
        {
            if (_GLDataHdrInfo == null)
                _GLDataHdrInfo = (GLDataHdrInfo)ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO];
            return _GLDataHdrInfo;
        }

        private void SetGLDataHdrObject(GLDataHdrInfo oGLDataHdrInfo)
        {
            long oldGLDataID = 0;
            long newGLDataID = 0;
            if (GetGLDataHdrObject() != null)
                oldGLDataID = _GLDataHdrInfo.GLDataID.GetValueOrDefault();
            if (oGLDataHdrInfo != null)
                newGLDataID = oGLDataHdrInfo.GLDataID.GetValueOrDefault();
            _GLDataHdrInfo = oGLDataHdrInfo;
            ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO] = oGLDataHdrInfo;
            if (oldGLDataID != newGLDataID)
                OnGLDataIDChanged();
        }
        WebEnums.RecPeriodStatus? _CurrentRecProcessStatus = null;
        public WebEnums.RecPeriodStatus? CurrentRecProcessStatus
        {
            get
            {
                if (!_CurrentRecProcessStatus.HasValue)
                    _CurrentRecProcessStatus = SessionHelper.CurrentRecProcessStatusEnum;
                return _CurrentRecProcessStatus;
            }
            set
            {
                // Save to View State
                _CurrentRecProcessStatus = value;
            }
        }
    }
}