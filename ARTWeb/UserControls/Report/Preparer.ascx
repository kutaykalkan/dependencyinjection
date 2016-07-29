<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Report_Preparer" Codebehind="Preparer.ascx.cs" %>
<div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="25%" />
        <col width="75%" />
        <tr>
            <td>
                <webControls:ExLabel ID="lblPreparer" SkinID="Black11Arial" FormatString="{0}:"
                        LabelID="1130" runat="server"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExTextBox ID="txtPreparer" runat="server" />
            </td>
        </tr>
    </table>
</div>