<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Async="true" Inherits="Pages_Home" Title="Untitled Page" Theme="SkyStemBlueBrown" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:WebPartManager ID="wpmDashboards" runat="server" Personalization-Enabled="true"
        Personalization-InitialScope="User">
    </asp:WebPartManager>
    <asp:Label ID="lblHiddenWebPartMode" runat="server" Text="Display" Style="display: none;" />
    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="text-align: center">
        <tr>
            <td colspan="3" align="center">
                <table class="AccountMessageBar" width="96%">
                    <tr>
                        <td align="left" width="33%">
                            <webControls:ExImage ID="imgReadExceptionMessages" SkinID="UnReadAlertMsg" runat="server" />
                            <webControls:ExHyperLink ID="hlExceptionMessages" LabelID="1011" SkinID="HyperLinkBold"
                                runat="server"></webControls:ExHyperLink>
                        </td>
                        <td width="33%">
                            <webControls:ExLabel ID="lblMessage" runat="server" SkinID="Black11ArialValignMiddle"></webControls:ExLabel>
                        </td>
                        <td width="33%" align="right">
                            <webControls:ExImage ID="imgReadAccountMessages" SkinID="UnReadAlertMsg" runat="server" />
                            <webControls:ExHyperLink ID="hlAccountMessages" LabelID="1010" runat="server" SkinID="HyperLinkBold"></webControls:ExHyperLink>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <%-- <webControls:ExLinkButton runat="server" ID="lnkEditDashboard" Font-Bold="true" 
                                 Font-Size="11px" Font-Names="Arial" Text="Edit Dashboard" 
                                 onclick="lnkEditDashboard_Click"></webControls:ExLinkButton>--%>
            </td>
        </tr>
        <tr id="trWebParts" runat="server">
            <td colspan="3" valign="top" align="center">
                <table border="0" cellpadding="0" cellspacing="0" width="96%" style="text-align: center">
                    <tr>
                        <td width="49%" valign="top">
                            <asp:WebPartZone WebPartVerbRenderMode="TitleBar" HeaderText="&nbsp;" ID="wpzLeft"
                                runat="server" Width="100%" Height="100%">
                                <CloseVerb Visible="false" />
                                <EditVerb Visible="false" />
                                <MinimizeVerb Text="&nbsp;" ImageUrl="~/App_Themes/SkyStemBlueBrown/Images/Minimize.gif"
                                    Visible="true" />
                                <RestoreVerb Text="&nbsp;" ImageUrl="~/App_Themes/SkyStemBlueBrown/Images/Maximize.gif" />
                                <ZoneTemplate>
                                    <wp:ReconciliationTrackingWP ID="wpReconciliationTracking" AllowMinimize="true" runat="server" />
                                    <wp:AccountReconciliationCoverageWP ID="wpAccountReconciliationCoverage" AllowMinimize="true"
                                        runat="server" />
                                    <wp:AccountOwnershipStatisticsWP ID="wpAccountOwnershipStatistics" AllowMinimize="true"
                                        runat="server" />
                                    <wp:TaskStatusWP ID="TaskStatus" AllowMinimize="true" runat="server" />
                                    
                                </ZoneTemplate>
                            </asp:WebPartZone>
                        </td>
                        <td width="2%">
                            &nbsp;
                        </td>
                        <td width="49%" valign="top">
                            <asp:WebPartZone WebPartVerbRenderMode="TitleBar" HeaderText="&nbsp;" ID="wpzRight"
                                runat="server" Width="100%" Height="100%">
                                <CloseVerb Visible="false" />
                                <EditVerb Visible="false" />
                                <MinimizeVerb Text="&nbsp;" ImageUrl="~/App_Themes/SkyStemBlueBrown/Images/Minimize.gif"
                                    Visible="true" />
                                <RestoreVerb Text="&nbsp;" ImageUrl="~/App_Themes/SkyStemBlueBrown/Images/Maximize.gif" />
                                <ZoneTemplate>
                                    <wp:ExceptionsByFSCaptionWP ID="wpExceptionsByFSCaption" AllowMinimize="true" runat="server" />
                                    <wp:UnassignedAccountOwnershipWP ID="wpUnassignedAccounts" AllowMinimize="true" runat="server" />
                                    <wp:IncompleteAttributeListWP ID="wpIncompleteAttributeList" AllowMinimize="true"
                                        runat="server" />
                                    <wp:ReconciliationStatusByFSCaptionWP ID="wpReconciliationStatusByFSCaption" AllowMinimize="true"
                                        runat="server" />
                                    <wp:OpenItemStatusWP ID="OpenItemStatus" AllowMinimize="true" runat="server" />
                                    <wp:TaskStatusByMonthWP ID="TaskStatusByMonth" AllowMinimize="true" runat="server" />
                                </ZoneTemplate>
                            </asp:WebPartZone>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trErrorMessage" runat="server" visible="false">
            <td colspan="3" align="center">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="text-align: center">
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="10%">
                            <webControls:ExLabel ID="lblError" runat="server" LabelID="1051" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td colspan="2" align="left">
                            <webControls:ExLabel ID="lblErrorMessage" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:CatalogZone ID="CatalogZone1" runat="server" BackColor="#F7F6F3" BorderColor="#CCCCCC"
                    BorderWidth="1px" Font-Names="Verdana" Padding="6">
                    <ZoneTemplate>
                        <asp:PageCatalogPart ID="PageCatalogPart1" runat="server" />
                    </ZoneTemplate>
                    <PartLinkStyle Font-Size="0.8em" />
                    <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" />
                    <EditUIStyle Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" />
                    <PartStyle BorderColor="#F7F6F3" BorderWidth="5px" />
                    <HeaderVerbStyle Font-Bold="False" Font-Size="0.8em" Font-Underline="False" ForeColor="#333333" />
                    <PartChromeStyle BorderColor="#E2DED6" BorderStyle="Solid" BorderWidth="1px" />
                    <EmptyZoneTextStyle Font-Size="0.8em" ForeColor="#333333" />
                    <SelectedPartLinkStyle Font-Size="0.8em" />
                    <VerbStyle Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" />
                    <LabelStyle Font-Size="0.8em" ForeColor="#333333" />
                    <FooterStyle BackColor="#E2DED6" HorizontalAlign="Right" />
                    <HeaderStyle BackColor="#E2DED6" Font-Bold="True" Font-Size="0.8em" ForeColor="#333333" />
                    <InstructionTextStyle Font-Size="0.8em" ForeColor="#333333" />
                </asp:CatalogZone>
                <asp:EditorZone ID="EditorZone1" runat="server">
                    <PartTitleStyle Height="0px" />
                    <ZoneTemplate>
                        <asp:AppearanceEditorPart ID="AppearanceEditorPart1" runat="server" />
                        <asp:LayoutEditorPart ID="LayoutEditorPart1" runat="server" />
                    </ZoneTemplate>
                </asp:EditorZone>
            </td>
        </tr>
    </table>
</asp:Content>
