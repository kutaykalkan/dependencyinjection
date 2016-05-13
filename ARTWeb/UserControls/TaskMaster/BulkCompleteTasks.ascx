<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BulkCompleteTasks.ascx.cs"
    Inherits="UserControls_TaskMaster_BulkCompleteTasks" %>
<%@ Register Src="~/UserControls/TaskMaster/GeneralTaskGrid.ascx" TagPrefix="UserControl"
    TagName="GeneralTaskGrid" %>
<%@ Register Src="~/UserControls/TaskMaster/AccountTaskGrid.ascx" TagPrefix="UserControl"
    TagName="AccountTaskGrid" %>
<%@ Register Src="~/UserControls/TaskMaster/AddTaskComment.ascx" TagPrefix="UserControl"
    TagName="AddTaskComment" %>

<script language="javascript" type="text/javascript">

    function setUsersInfo(AddUsersIDList, AddUsersNameList, TaskUserRoleVal) {
        // alert(TaskUserRoleVal);
        // alert(AddUsersIDList);

        var hdnUser = null;
        var txtUser = null;
        if (TaskUserRoleVal == 4)//AssignedTo
        {
            hdnUser = $get("<%=hdnAssignedTo.ClientID %>");
            txtUser = $get('<%=txtAssignedTo.ClientID %>');
        }
        else if (TaskUserRoleVal == 5)//Reviewer
        {
            hdnUser = $get("<%=hdnReviewer.ClientID %>");
            txtUser = $get('<%=txtReviewer.ClientID %>');
        }
        else if (TaskUserRoleVal == 24)//Approver
        {
            hdnUser = $get("<%=hdnApprover.ClientID %>");
            txtUser = $get('<%=txtApprover.ClientID %>');
        }
        else if (TaskUserRoleVal == 6)//Notify
        {
            hdnUser = $get("<%=hdnNotify.ClientID %>");
            txtUser = $get('<%=txtNotify.ClientID %>');
        }
    if (hdnUser != null)
        hdnUser.value = AddUsersIDList;
    if (txtUser != null)
        txtUser.value = AddUsersNameList;
        //            alert(hdnNotify.value);
}
function ConfirmDeletion(msg, TaskUserRoleVal) {
    var answer = confirm(msg);
    if (answer) {
        setUsersInfo("", "", TaskUserRoleVal);
        return false;
    }
    else {
        return false;
    }
}
function OpenNewWindow(url, QueryStringConstants, TaskUserRole) {

    var hdnNotify = null
    if (TaskUserRole == 4)//AssignedTo
        hdnNotify = $get("<%=hdnAssignedTo.ClientID %>")
        else if (TaskUserRole == 5)//Reviewer
            hdnNotify = $get("<%=hdnReviewer.ClientID %>")
            else if (TaskUserRole == 24)//Approver
                hdnNotify = $get("<%=hdnApprover.ClientID %>")
            else if (TaskUserRole == 6)//Notify
                hdnNotify = $get("<%=hdnNotify.ClientID %>")

    if (hdnNotify != null && hdnNotify != 'undefined') {
        var hdnNotifyVal = removeSpaces(hdnNotify.value);
        if (hdnNotifyVal != null && hdnNotifyVal != "")
            url = url + "&" + QueryStringConstants + "=" + hdnNotifyVal;
    }
    OpenRadWindowFromRadWindow(url, '400', '650');
}
function removeSpaces(string) {
    return string.split(' ').join('');
}

</script>

<table width="100%" cellpadding="0px" cellspacing="0px">
    <col width="10%" />
    <col width="70%" />
    <col width="20%" />
    <tr>
        <td colspan="3">
            <asp:Panel ID="pnlcomment" runat="server" Visible="false">
                <table width="100%" cellpadding="0px" cellspacing="0px">
                    <colgroup>
                        <col width="10%" />
                        <col width="90%" />
                        <tr>
                            <td align="center" valign="middle">
                                <webControls:ExLabel ID="lblComment" runat="server" FormatString="{0}:" LabelID="2587"
                                    SkinID="Black11Arial" />
                            </td>
                            <td align="left" colspan="2">
                                <UserControl:AddTaskComment ID="ucAddTaskComment" runat="server" />
                            </td>
                        </tr>
                    </colgroup>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlUserAssignment" runat="server" Visible="false">
                <table width="100%" cellpadding="0px" cellspacing="0px">
                    <colgroup>
                        <col width="2%" />
                        <col width="20%" />
                        <col width="78%" />
                        <tr>
                            <td class="ManadatoryField">&nbsp;
                            </td>
                            <td valign="top">
                                <webControls:ExLabel ID="lblAssignedTo" runat="server" FormatString="{0}:" LabelID="2564"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtAssignedTo" runat="server" SkinID="TextBox150" />
                                <webControls:ExHyperLink ID="hlAssignedTo" LabelID="1607" Visible="true" runat="server"
                                    ToolTipLabelID="1607" SkinID="AddUser" />
                                <webControls:ExLabel ID="lblAssignedToValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>                               
                                <webControls:ExImageButton CommandName="ClearAssignee" ID="btnClearAssignedTo" ToolTipLabelID="2956"
                                    runat="server" SkinID="ClearNotify" CausesValidation="false" OnClick="btnClearAssignedTo_Click" />
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td colspan="3"></td>
                        </tr>

                        <tr>
                            <td class="ManadatoryField">&nbsp;
                            </td>
                            <td valign="top">
                                <webControls:ExLabel ID="lblReviewer" runat="server" LabelID="1131" FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtReviewer" runat="server" SkinID="TextBox150"></asp:TextBox>
                                <webControls:ExHyperLink ID="hlReviewer" LabelID="1607" Visible="true" runat="server"
                                    ToolTipLabelID="1607" SkinID="AddUser" />
                                <webControls:ExLabel ID="lblReviewerValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>                            
                                <webControls:ExImageButton CommandName="ClearReviewer" ID="btnClearReviewer" ToolTipLabelID="2957"
                                    runat="server" SkinID="ClearNotify" CausesValidation="false" OnClick="btnClearReviewer_Click" />
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td colspan="3"></td>
                        </tr>

                        <tr>
                            <td></td>
                            <td>
                                <webControls:ExLabel ID="lblApprover" runat="server" FormatString="{0}:" LabelID="1132"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <asp:TextBox ID="txtApprover" runat="server" SkinID="TextBox150"></asp:TextBox>
                                <webControls:ExHyperLink ID="hlApprover" LabelID="1607" Visible="true" runat="server"
                                    ToolTipLabelID="1607" SkinID="AddUser" />
                                <webControls:ExLabel ID="lblApproverValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>                              
                                <webControls:ExImageButton CommandName="ClearApprover" ID="btnClearApprover" ToolTipLabelID="2958"
                                    runat="server" SkinID="ClearNotify" CausesValidation="false" OnClick="btnClearApprover_Click" />
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td colspan="3"></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <webControls:ExLabel ID="lblNotify" runat="server" FormatString="{0}:" LabelID="2525"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNotify" runat="server" SkinID="TextBox150" />
                                <webControls:ExHyperLink ID="hlNotify" runat="server" LabelID="1607" SkinID="AddUser"
                                    ToolTipLabelID="1607" Visible="true" />
                                <webControls:ExLabel ID="lblNotifyValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>                              
                                <webControls:ExImageButton CommandName="ClearNotify" ID="btnClearNotify" ToolTipLabelID="2663"
                                    runat="server" SkinID="ClearNotify" CausesValidation="false" OnClick="btnClearNotify_Click" />
                            </td>
                        </tr>
                    </colgroup>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td>&nbsp;
        </td>
        <td align="right">&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="3" style="width: 100%">
            <UserControl:GeneralTaskGrid ID="ucGeneralTaskGrid" runat="server" AllowCustomPaging="true"
                Grid-ClientSettings-Scrolling-AllowScroll="true" AllowExportToExcel="false" AllowExportToPDF="false"
                AllowSelectionPersist="true" Grid-Width="990px" Grid-Hieght="300px">
            </UserControl:GeneralTaskGrid>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="width: 100%">
            <UserControl:AccountTaskGrid ID="ucAccountTaskGrid" runat="server" AllowCustomPaging="true"
                Grid-ClientSettings-Scrolling-AllowScroll="true" AllowExportToExcel="false" AllowExportToPDF="false"
                AllowSelectionPersist="true" Grid-Width="990px" Grid-Hieght="300px"></UserControl:AccountTaskGrid>
        </td>
    </tr>
    <tr class="BlankRow">
        <td colspan="3"></td>
    </tr>
    <tr>
        <td colspan="3" style="width: 100%" align="right">
            <webControls:ExButton ID="btnDeletePermanent" LabelID="1564" runat="server" CausesValidation="False"
                OnClick="btnDeletePermanent_Click" />
                <webControls:ExButton ID="btnSave" LabelID="1315 " runat="server" CausesValidation="False"
                OnClick="btnSave_Click" />
            <webControls:ExButton ID="btnMarkComplete" runat="server" CausesValidation="true"
                OnClick="btnMarkComplete_Click" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnAssignedTo" runat="server" />
<asp:HiddenField ID="hdnReviewer" runat="server" />
<asp:HiddenField ID="hdnApprover" runat="server" />
<asp:HiddenField ID="hdnNotify" runat="server" />
<asp:HiddenField ID="hdnNotifyUserName" runat="server" />
