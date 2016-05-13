<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Signature.ascx.cs" Inherits="UserControls_Signature" %>
<table style="width: 35%" cellpadding="0" cellspacing="0" class="CertificationSignature">
    <col width="30%" />
    <col width="70%" />
    <tr>
        <td style="padding-left:2px">
            <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="1628" SkinID="Black11Arial"
                FormatString="{0}:" />
        </td>
        <td style="padding-right:2px">
            <webControls:ExLabel ID="lblSignature" runat="server" />
        </td>
    </tr>
</table>
