<%@ Control Language="C#" AutoEventWireup="true" 
Inherits="User" Codebehind="User.ascx.cs" %>
<div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="25%" />
        <col width="75%" />
        <tr>
            <td>
                <webControls:ExLabel ID="lblRiskRating" SkinID="Black11Arial" FormatString="{0}:"
                        LabelID="1533" runat="server"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExTextBox ID="txtUser" runat="server" />
            </td>
        </tr>
    </table>
</div>