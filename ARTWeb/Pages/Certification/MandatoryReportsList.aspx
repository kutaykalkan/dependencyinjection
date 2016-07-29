<%@ Page Language="C#" MasterPageFile="~/MasterPages/CertificationMasterPage.master"
    AutoEventWireup="true" Inherits="Pages_MandatoryReportsList"
    Title="Untitled Page" Theme="SkyStemBlueBrown" Codebehind="MandatoryReportsList.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCertification" runat="Server">
    <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divReportList" runat="server">
                <asp:Repeater ID="rptMandatoryReportGroup" runat="server" OnItemDataBound="rptMandatoryReportGroup_ItemDataBound">
                    <ItemTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <%--                                                    <td  class="ReportReportGroup">
                                                             <webControls:ExLabel ID="ExLabel1" SkinID="Black11Arial"  runat="server"></webControls:ExLabel> </td>    --%>
                                <td>
                                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                                        <col width="40%" />
                                        <col width="35%" />
                                        <col width="25%" />
                                        <tr class="ReportReportGroup">
                                            <td>
                                                <webControls:ExLabel ID="lblReportGroup" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                                            </td>
                                            <td align="center">
                                                <webControls:ExLabel ID="lblSignOffStatus" SkinID="Black11Arial" LabelID="1377" runat="server"></webControls:ExLabel>
                                            </td>
                                            <td align="center">
                                                <webControls:ExLabel ID="lblSignOffDate" SkinID="Black11Arial" LabelID="1399 " runat="server"></webControls:ExLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%">
                                    <asp:Repeater ID="rptMandatoryReport" runat="server">
                                        <ItemTemplate>
                                            <table style="width: 100%" cellpadding="0" cellspacing="0" class="reportContent">
                                                <col width="40%" />
                                                <col width="35%" />
                                                <col width="25%" />
                                                <tr class="reportLightBlueRow">
                                                    <td class="ReportNameTD">
                                                        <webControls:ExImage SkinID="BulletIcon" ID="imgBullet" runat="server" />
                                                        <webControls:ExLinkButton ID="lbtnReportName" CommandName="SendToReportCommand" OnCommand="SendToReport"
                                                            runat="server" SkinID="LinkButtonBold" />
                                                    </td>
                                                    <td align="center">
                                                        <webControls:ExLabel ID="lblYN" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                                    </td>
                                                    <td align="center">
                                                        <webControls:ExLabel ID="lblSignOffDate" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                                    </td>
                                                </tr>
                                                <tr class="ReportDescription" >
                                                    <td colspan="3" class="ReportDescriptionTD">
                                                        <webControls:ExLabel ID="lblReportDescription" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
