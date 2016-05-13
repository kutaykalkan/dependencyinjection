<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditTaskList.aspx.cs" Inherits="Pages_EditTaskList"
    MasterPageFile="~/MasterPages/PopUpMasterPage.master" Theme="SkyStemBlueBrown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="5%" />
        <col width="35%" />
        <col width="60%" />
        <tr class="BlankRow">
            <td colspan="3"></td>
        </tr>
        <tr>
            <td class="ManadatoryField">*
            </td>
            <td>
                <webControls:ExLabel ID="lblTaskListName" runat="server" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExTextBox ID="txtNewTaskListName" MaxLength="100" IsRequired="true"
                    runat="server" SkinID="ExTextBox200" />
                <asp:CustomValidator ID="cvTaskListName" runat="server" Text="!" Font-Bold="true"
                    OnServerValidate="cvTaskListName_OnServerValidate"></asp:CustomValidator>

            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="3"></td>
        </tr>
        <tr>
            <td align="right" colspan="3">
                <webControls:ExButton ID="btnUpdate" runat="server" LabelID="1315" OnClick="btnUpdate_Click" />
                <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClick="btnCancel_Click"
                    CausesValidation="false" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="3"></td>
        </tr>
    </table>
</asp:Content>
