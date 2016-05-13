using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;

/// <summary>
/// Summary description for PageBaseRecForm
/// </summary>
public abstract class PageBaseRecForm: PageBaseRecPeriod
{
    public PageBaseRecForm()
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

    public short? GLRecStatusID
    {
        get
        {
            if (GLDataHdrInfo != null)
                return GLDataHdrInfo.ReconciliationStatusID;
            return null;
        }
    }
    #endregion

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
}
