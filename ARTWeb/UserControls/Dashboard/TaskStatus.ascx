<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Dashboard_TaskStatus" Codebehind="TaskStatus.ascx.cs" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<table style="width: 100%" cellpadding="0" cellspacing="0" id="tblMessage" runat="server"
    visible="false">
    <tr>
        <td colspan="2">
            <webControls:ExLabel ID="lblMessage" runat="server" SkinID="ErrorLabel"></webControls:ExLabel>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="upnlTaskStatus" runat="server">
    <ContentTemplate>
        <table style="width: 100%" cellpadding="0" cellspacing="0" id="tblContent" runat="server">
            <tr class="BlankRow">
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 2px; width: 30%;">
                    <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="2588" FormatString="{0}:"
                        SkinID="Black11Arial"></webControls:ExLabel>
                </td>
                <td>
                    <webControls:ExLabel ID="lblTotalTasks" runat="server" SkinID="Black11ArialValignMiddle"></webControls:ExLabel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center; vertical-align: top">
                    <asp:Chart ID="chrtTaskStatus" BorderlineColor="Blue" runat="server" Width="450px"
                        Height="240px">
                    </asp:Chart>
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
                            <td valign="top">
                                <webControls:ExLabel ID="lblNote" FormatString="{0}:" LabelID="2005" runat="server"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblmsg" runat="server" LabelID="2630" SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<UserControls:ProgressBar ID="upTaskStatus" runat="server" EnableTheming="true" AssociatedUpdatePanelID="upnlTaskStatus"
    Visible="true" />
