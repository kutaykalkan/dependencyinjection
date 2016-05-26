<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" Inherits="Pages_ReportHome" Title="Untitled Page"
    Theme="SkyStemBlueBrown" Codebehind="ReportHome.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <ajaxToolkit:CollapsiblePanelExtender ID="cpeMyReport" TargetControlID="pnlMyReport"
                            ImageControlID="imgCollapse1" CollapseControlID="imgCollapse1" ExpandControlID="imgCollapse1"
                            runat="server" SkinID="CollapsiblePanel">
                        </ajaxToolkit:CollapsiblePanelExtender>
                        <table class="reportSectionTable" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td colspan="2">
                                    <table class="reportBlueRow" cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="font-size: 14">
                                                <webControls:ExLabel ID="lblHeadingMyReport" LabelID="1077" runat="server" SkinID="SubSectionHeading"></webControls:ExLabel>
                                            </td>
                                            <td width="4%" align="right">
                                                <webControls:ExImage ID="imgCollapse1" runat="server" SkinID="CollapseIcon" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlMyReport" runat="server">
                                        <asp:Repeater ID="rptMyReportGroup" runat="server" OnItemDataBound="rptMyReportGroup_ItemDataBound">
                                            <ItemTemplate>
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="ReportReportGroup">
                                                            <webControls:ExLabel ID="lblMyReportGroup" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <asp:Repeater ID="rptMyReport" runat="server">
                                                                <ItemTemplate>
                                                                    <table style="width: 100%" cellpadding="0" cellspacing="0" class="reportContent">
                                                                        <col width="35%" />
                                                                        <col width="45%" />
                                                                        <col width="20%" />
                                                                        <tr class="reportLightBlueRow">
                                                                            <td class="ReportNameTD">
                                                                                <webControls:ExImage src="../App_Themes/SkyStemBlueBrown/Images/Bullet.gif" ID="imgBullet"
                                                                                    runat="server" />
                                                                                &nbsp;<webControls:ExHyperLink ID="hlMyReport" SkinID="HyperLinkBold" runat="server"></webControls:ExHyperLink>
                                                                            </td>
                                                                            <td>
                                                                                <webControls:ExHyperLink ID="hlMyActivity" LabelID="1616" runat="server" SkinID="HyperLinkBoldBrown"></webControls:ExHyperLink>
                                                                            </td>
                                                                            <td>
                                                                                <webControls:ExImageButton ID="imgBtnDeleteReport"   OnCommand="DeleteMyReport" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr class="ReportDescription">
                                                                            <td colspan="3" class="ReportDescriptionTD">
                                                                                <webControls:ExLabel ID="lblMyReportDescription" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
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
                                        <%--   <table style="width: 100%" cellpadding="0" cellspacing="0" class="reportContent">
                                            <col width="55%" />
                                            <col width="15%" />
                                            <col width="15%" />
                                            <col width="15%" />
                                           <tr class="reportLightBlueRow">
                                                <td>
                                                    <webControls:ExHyperLink ID="hlMyReport1" Text="Unusual Balances Report" runat="server"></webControls:ExHyperLink>
                                                </td>
                                                <td>
                                                    <webControls:ExHyperLink ID="hlSavedReport1" Text="Saved Report" runat="server"></webControls:ExHyperLink>
                                                </td>
                                                <td>
                                                    <webControls:ExHyperLink ID="hlMyReportActivity1" Text="Report Activity" runat="server"></webControls:ExHyperLink>
                                                </td>
                                                <td align="right">
                                                    <webControls:ExImageButton runat="server" ID="imgbtnRemoveReport1" SkinID="DeleteIcon"  OnClientClick="return confirm('Do you wish to delete this item?');" />
                                                </td>
                                            </tr>
                                            <tr class="ReportDescription">
                                                <td colspan="4">
                                                    <webControls:ExLabel ID="lblMyReportDescription1" Text="This Report shows the unusual balances in the accounts"
                                                        runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                                </td>
                                            </tr>
                                            <tr class="BlankRow">
                                            </tr>
                                            <tr class="reportLightBlueRow">
                                                <td>
                                                    <webControls:ExHyperLink ID="hlMyReport2" Text="User Access Report" runat="server"></webControls:ExHyperLink>
                                                </td>
                                                <td>
                                                    <webControls:ExHyperLink ID="hlSavedReport2" Text="Saved Report" runat="server"></webControls:ExHyperLink>
                                                </td>
                                                <td>
                                                    <webControls:ExHyperLink ID="hlMyReportActivity2"  Text="Report Activity" runat="server"></webControls:ExHyperLink>
                                                </td>
                                                <td align="right">
                                                    <webControls:ExImageButton runat="server" ID="imgbtnRemoveReport2" SkinID="DeleteIcon"  OnClientClick="return confirm('Do you wish to delete this item?');"/>
                                                </td>
                                            </tr>
                                            <tr class="ReportDescription">
                                                <td colspan="4">
                                                    <webControls:ExLabel ID="lblMyReportDescription2" Text="It reports about users roles on accounts with tracking of date of activation of roles (On user basis)"
                                                        runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                                </td>
                                            </tr>
                                        </table>--%></asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <ajaxToolkit:CollapsiblePanelExtender ID="cpeStandardReport" TargetControlID="pnlStandardReport"
                            ImageControlID="imgCollapse2" CollapseControlID="imgCollapse2" ExpandControlID="imgCollapse2"
                            runat="server" SkinID="CollapsiblePanel">
                        </ajaxToolkit:CollapsiblePanelExtender>
                        <table class="reportSectionTable" cellpadding="0" cellspacing="0">
                            <tr>
                                <td colspan="2">
                                    <table class="reportBlueRow" cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="font-size: 14;">
                                                <webControls:ExLabel ID="lblHeadingStandardReport" LabelID="1076" runat="server"
                                                    SkinID="SubSectionHeading"></webControls:ExLabel>
                                            </td>
                                            <td width="4%" align="right">
                                                <webControls:ExImage ID="imgCollapse2" runat="server" SkinID="CollapseIcon" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlStandardReport" runat="server">
                                        <asp:Repeater ID="rptStandardReportGroup" runat="server" OnItemDataBound="rptStandardReportGroup_ItemDataBound">
                                            <ItemTemplate>
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="ReportReportGroup">
                                                            <webControls:ExLabel ID="lblReportGroup" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <asp:Repeater ID="rptStandardReport" runat="server">
                                                                <ItemTemplate>
                                                                    <table style="width: 100%" cellpadding="0" cellspacing="0" class="reportContent">
                                                                        <col width="35%" />
                                                                        <col width="65%" />
                                                                        <tr class="reportLightBlueRow">
                                                                            <td class="ReportNameTD">
                                                                                <webControls:ExImage src="../App_Themes/SkyStemBlueBrown/Images/Bullet.gif" ID="imgBullet"
                                                                                    runat="server" />
                                                                                &nbsp;<webControls:ExHyperLink ID="hlReport" SkinID="HyperLinkBold" runat="server"></webControls:ExHyperLink>
                                                                            </td>
                                                                            <td>
                                                                                <webControls:ExHyperLink ID="hlActivity" LabelID="1616" runat="server" SkinID="HyperLinkBoldBrown"></webControls:ExHyperLink>
                                                                            </td>
                                                                        </tr>
                                                                        <tr class="ReportDescription">
                                                                            <td colspan="2" class="ReportDescriptionTD">
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
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                       
                    </td>
                </tr>
            </table>
            <UserControls:ProgressBar ID="ucProgressBarMain" runat="server" AssociatedUpdatePanelID="upnlMain" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
    function ConfirmDelete(msg) {
        var answer = confirm(msg);
        if (answer) {
            return true;
        }
        else {
            return false;
        }
    }   
        
</script>
</asp:Content>



