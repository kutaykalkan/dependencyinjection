<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_CapabilityMultiCurrency" Codebehind="CapabilityMultiCurrency.ascx.cs" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:UpdatePanel ID="upnlMultiCurrency" runat="server" UpdateMode="Conditional">
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
                    <tr id="trMultiCurrency" runat="server">
                        <td class="ManadatoryField"></td>
                        <td width="28%">
                            <webControls:ExLabel ID="lblMultiCurrency" runat="server" LabelID="1018" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExRadioButton ID="OptMultiCurrencyYes" runat="server" GroupName="OptMultiCurrency"
                                OnCheckedChanged="OptMultiCurrencyYes_CheckedChanged" AutoPostBack="true" LabelID="1252" SkinID="OptBlack11Arial" />
                        </td>
                        <td id="tdCapabilityStatus" runat="server">
                            <webControls:ExRadioButton ID="OptMultiCurrencyNo" runat="server" GroupName="OptMultiCurrency"
                                OnCheckedChanged="OptMultiCurrencyNo_CheckedChanged" AutoPostBack="true" LabelID="1251" SkinID="OptBlack11Arial" />
                        </td>
                        <td>
                            <webControls:ExImage ID="imgStatusMultiCurrencyForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                            <webControls:ExImage ID="imgStatusMultiCurrencyForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
                        </td>
                        <td align="right">
                            <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlContent" runat="server">
                <table width="100%" class="InputRequrementsTextNoBackColor" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlContentMultiCurrency" runat="server" SkinID="pnlExtended" Width="100%">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <col width="2%" />
                                    <col width="35%" />
                                    <col width="25%" />
                                    <col width="38%" />
                                    <tr>
                                        <td class="ManadatoryField">*
                                        </td>
                                        <td>
                                            <webControls:ExLabel ID="lblReopenRecOnCCYReload" runat="server" LabelID="2988" FormatString="{0}:"
                                                SkinID="Black11Arial"></webControls:ExLabel>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlReopenRecOnCCYReload" runat="server" SkinID="DropDownList230">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvReopenRecOnCCYReload" runat="server" ControlToValidate="ddlReopenRecOnCCYReload"
                                                InitialValue="-2" Font-Bold="true" Font-Size="Medium">!</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <webControls:ExImage ID="imgStatusReopenRecOnCCYReloadForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                                            <webControls:ExImage ID="imgStatusReopenRecOnCCYReloadForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:CollapsiblePanelExtender ID="cpeMultiCurrency" TargetControlID="pnlContent"
                ImageControlID="imgCollapse" CollapseControlID="imgCollapse" ExpandControlID="imgCollapse"
                runat="server" SkinID="CollapsiblePanel">
            </ajaxToolkit:CollapsiblePanelExtender>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
