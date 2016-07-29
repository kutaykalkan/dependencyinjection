<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_TextEditor" Codebehind="TextEditor.ascx.cs" %>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td>
            <telerik:RadEditor runat="server" ID="rdMultiline" SkinID="RadEditAttributeValue"
                OnClientLoad="OnClientLoad" OnClientPasteHtml="UpdateCharCount" Width="96%">
            </telerik:RadEditor>
        </td>
        <td align="left">
            <asp:RequiredFieldValidator ID="rfvMultiline" runat="server" ControlToValidate="rdMultiline"
                Text="!" Font-Bold="true" Font-Size="Medium">
            </asp:RequiredFieldValidator>
            <asp:CustomValidator ID="cvMultiline" runat="server" ControlToValidate="rdMultiline"
                Text="!" Font-Bold="true" Font-Size="Medium" ClientValidationFunction="ValidateMultiline"
                OnServerValidate="cvMultiline_OnServerValidation">
            </asp:CustomValidator>
        </td>
    </tr>
    <tr class="BlankRow" />
    <tr>
        <td colspan="2">
            <webControls:ExLabel ID="lblCharsRemaining" runat="server" LabelID="2620" FormatString="{0} :"
                SkinID="Black11Arial" />&nbsp;
            <webControls:ExLabel ID="lblCharCount" runat="server" SkinID="Black11Arial" />
        </td>
    </tr>
</table>
