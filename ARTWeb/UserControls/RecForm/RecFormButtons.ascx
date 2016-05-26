<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_RecFormButtons" Codebehind="RecFormButtons.ascx.cs" %>
<table border="0" cellpadding="0" cellspacing="0" class="HideInPdf">
    <tr>
        <td>
            <webControls:ExButton ID="btnSave" OnCommand="btn_OnCommand" CommandName="Save" LabelID="1315"
                runat="server" CssClass="BtnLeftMargin" />
            <webControls:ExButton ID="btnSignoff" OnCommand="btn_OnCommand" CommandName="Signoff"
                LabelID="1377" runat="server" CssClass="BtnLeftMargin" />
            <webControls:ExButton ID="btnEditRec" OnCommand="btn_OnCommand" CommandName="EditRec"
                LabelID="1379" runat="server" CssClass="BtnLeftMargin" />
            <webControls:ExButton ID="btnApprove" OnCommand="btn_OnCommand" CommandName="Approve"
                LabelID="1483" runat="server" CssClass="BtnLeftMargin" />
            <webControls:ExButton ID="btnDeny" OnCommand="btn_OnCommand" CommandName="Deny" LabelID="1484"
                runat="server" CssClass="BtnLeftMargin" />
            <webControls:ExButton ID="btnAccept" OnCommand="btn_OnCommand" CommandName="Accept"
                LabelID="1481" runat="server" CssClass="BtnLeftMargin" />
            <webControls:ExButton ID="btnReject" OnCommand="btn_OnCommand" CommandName="Reject"
                LabelID="1482" runat="server" CssClass="BtnLeftMargin" />
            <webControls:ExButton ID="btnRemoveSignOff" OnCommand="btn_OnCommand" CausesValidation="false" Visible="false" CommandName="RemoveSignOff"
                LabelID="1627" runat="server" CssClass="BtnLeftMargin" />
            <webControls:ExButton ID="btnCancel" OnCommand="btn_OnCommand" CommandName="Cancel" CausesValidation="false"
                LabelID="1545" runat="server" CssClass="BtnLeftMargin" />
        </td>
    </tr>
</table>

<script language="javascript" type="text/javascript">
    function ConfirmDelete(msg) {
        var answer = confirm(msg);
        if (answer) {
            return true;
        }
        else {
            return false;
        }
    }

    function resetForm() {
        var oForm = document.forms[0];
        if (oForm != null) {
            //        oForm.reset();
        }
        return false;
    }
        
</script>

