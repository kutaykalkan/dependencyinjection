<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IncompleteAttributeList.ascx.cs"
    Inherits="UserControls_Dashboard_IncompleteAttributeList" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<table style="width: 100%" cellpadding="0" cellspacing="0" id="tblMessage" runat="server"
    visible="false">
    <tr>
        <td colspan="2">
            <webControls:ExLabel ID="lblMessage" runat="server" SkinID="ErrorLabel"></webControls:ExLabel>
        </td>
    </tr>
</table>
<table style="width: 100%" cellpadding="0" cellspacing="0" id="tblContent" runat="server">
    <tr class="BlankRow">
        <td>
        </td>
    </tr>
    
    <tr class="BlankRow">
        <td>
        </td>
    </tr>
    <tr>
        <td style="padding-left: 2px; width: 30%;">
            <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="1663" FormatString="{0}:"
                SkinID="Black11Arial"></webControls:ExLabel>
        </td>
        <td>
            <webControls:ExLabel ID="lblTotalAccounts" runat="server" SkinID="Black11ArialValignMiddle"></webControls:ExLabel>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center; vertical-align: top">
            <asp:Chart ID="chrtIncompleteAttributeList" BorderlineColor="Blue" runat="server"
                Width="450px" Height="240px">
            </asp:Chart>
        </td>
    </tr>
    <tr class="BlankRow">
        <td colspan="2" align="center">
            <webControls:ExLabel ID="lblMsg" Visible="False" LabelID="1898" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
        </td>
    </tr>
    <tr class="BlankRow">
        <td>
        </td>
    </tr>
    <tr>
        <td align="left" colspan="2">
            <table>
                <tr>
                    <td valign ="top" >
                        <webControls:ExLabel ID="lblNote" FormatString ="{0}:" runat="server" LabelID="2005" SkinID="Black11Arial"></webControls:ExLabel>
                    </td>
                    <td>
                        <webControls:ExLabel ID="ExLabel3" runat="server" LabelID="2003" SkinID="Black11Arial"></webControls:ExLabel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
