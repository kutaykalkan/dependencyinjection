<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CapabilityRiskRatingAll.ascx.cs"
    Inherits="UserControls_CapabilityRiskRatingAll" %>
<%@ Register TagPrefix="UserControls" TagName="CapabilityRiskRating" Src="~/UserControls/Capability/CapabilityRiskRating.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:UpdatePanel ID="upnlRiskRating" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnlMain" runat="server">
            <%--SkinID="CapabilityPanel"--%>
            <asp:Panel ID="pnlYesNo" runat="server" Width="100%">
                <table width="100%" cellpadding="0" cellspacing="0" class="InputRequrementsHeading">
                    <col width="2%" />
                    <col width="35%" />
                    <col width="10%" />
                    <col width="15%" />
                    <col width="34%" />
                    <col width="4%" />
                    <tr>
                        <td class="ManadatoryField">
                        </td>
                        <td class="tdCapabilityYesOpt">
                            <webControls:ExLabel ID="lblRiskRating" runat="server" LabelID="1013" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td class="tdCapabilityNoOpt">
                            <webControls:ExRadioButton ID="optRiskRatingYes" runat="server" GroupName="optRiskRating"
                                OnCheckedChanged="optRiskRatingYes_CheckedChanged" AutoPostBack="true" LabelID="1252"
                                SkinID="OptBlack11Arial" />
                        </td>
                        <td id="tdCapabilityStatus" runat="server" align="left">
                            <%--class="tdCapabilityStatusIcon"--%>
                            <webControls:ExRadioButton ID="optRiskRatingNo" runat="server" GroupName="optRiskRating"
                                OnCheckedChanged="optRiskRatingNo_CheckedChanged" AutoPostBack="true" LabelID="1251"
                                SkinID="OptBlack11Arial" />
                        </td>
                        <td>
                            <webControls:ExImage ID="imgStatusRiskRatingForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                            <webControls:ExImage ID="imgStatusRiskRatingForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
                        </td>
                        <td width="4%" align="right">
                            <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlContent" runat="server">
                <table width="100%" class="InputRequrementsTextNoBackColor" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlRiskRatingFrequencyExtended" runat="server" SkinID="pnlExtended">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <UserControls:CapabilityRiskRating ID="ucCapabilityRiskRating1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <UserControls:CapabilityRiskRating ID="ucCapabilityRiskRating2" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <UserControls:CapabilityRiskRating ID="ucCapabilityRiskRating3" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:CollapsiblePanelExtender ID="cpeRiskRating" TargetControlID="pnlContent"
                ImageControlID="imgCollapse" CollapseControlID="imgCollapse" ExpandControlID="imgCollapse"
                runat="server" SkinID="CollapsiblePanel">
            </ajaxToolkit:CollapsiblePanelExtender>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
