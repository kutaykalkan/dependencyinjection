<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_FooterMaster" Codebehind="FooterMaster.ascx.cs" %>

<table width="100%" class="Footer">
    <tr>
        <td align="center">
            <webControls:ExLabel ID="lblFooterCopyright" LabelID="1199" runat="server"></webControls:ExLabel>
            &nbsp;
            <webControls:ExLabel ID="lblFooterRight" LabelID="1200" runat="server"></webControls:ExLabel>
        </td>
        <td align="left">
            | &nbsp;&nbsp;
            <webControls:ExLabel ID="lblLastLoggedIn" LabelID="1249" runat="server"></webControls:ExLabel>
            &nbsp;
            <webControls:ExLabel ID="lblDateTime" runat="server"></webControls:ExLabel>
        </td>
    </tr>
</table>
