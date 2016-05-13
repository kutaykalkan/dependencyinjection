<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExchangeRateBar.ascx.cs"
    Inherits="UserControls_ExchangeRateBar" %>
<table width="80%" cellpadding="0" cellspacing="0" border="0" class="ExchangeRateBar">
    <col width="2%" />
    <col width="20%" />
    <col width="25%" />
    <col width="6%" />
    <col width="20%" />
    <col width="25%" />
    <col width="2%" />
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <webControls:ExLabel ID="Label1" runat="server" LabelID="2048" FormatString="{0}:"
                SkinID="Black11Arial"></webControls:ExLabel>
        </td>
        <td>
            <webControls:ExLabel ID="lblLCCYtoBCCYValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="2049" FormatString="{0}:"
                SkinID="Black11Arial"></webControls:ExLabel>
        </td>
        <td>
            <webControls:ExLabel ID="lblLCCYtoRCCYValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
