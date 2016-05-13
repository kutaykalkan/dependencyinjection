<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CapabilityActivationRCCL.ascx.cs"
    Inherits="UserControls_CapabilityActivationRCCL" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:UpdatePanel ID="upnlDualLevelReview" runat="server" UpdateMode="Conditional">
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
                        <td class="ManadatoryField"></td>
                        <td>
                            <webControls:ExLabel ID="lblRecControlChecklist" runat="server" LabelID="2827"
                                FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExRadioButton ID="optRCCLYes" GroupName="optRCCY" runat="server"
                                OnCheckedChanged="optRCCLYes_CheckedChanged" AutoPostBack="true" LabelID="1252"
                                SkinID="OptBlack11Arial" />
                        </td>
                        <td id="tdCapabilityStatus" runat="server">
                            <webControls:ExRadioButton ID="optRCCLNo" GroupName="optRCCY" runat="server"
                                OnCheckedChanged="optRCCLNo_CheckedChanged" AutoPostBack="true" LabelID="1251"
                                SkinID="OptBlack11Arial" />
                        </td>
                        <td>
                            <webControls:ExImage ID="imgStatusRCCLForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                            <webControls:ExImage ID="imgStatusRCCLForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
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
                            <asp:Panel ID="pnlContentRCCL" runat="server" SkinID="pnlExtended" Width="100%">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <col width="2%" />
                                    <col width="35%" />
                                    <col width="25%" />
                                    <col width="38%" />
                                    <tr>
                                        <td class="ManadatoryField">
                                        </td>
                                        <td>
                                            <webControls:ExLabel ID="lblRCCLValidationType" runat="server" LabelID="2857" FormatString="{0}:"
                                                SkinID="Black11Arial"></webControls:ExLabel>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlRCCLValidationType" runat="server" SkinID="DropDownList230">
                                            </asp:DropDownList>                                           
                                        </td>
                                        <td>
                                            <webControls:ExImage ID="imgStatusRCCValidationTypeForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                                            <webControls:ExImage ID="imgStatusRCCValidationTypeForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:CollapsiblePanelExtender ID="cpeMateriality" TargetControlID="pnlContent"
                ImageControlID="imgCollapse" CollapseControlID="imgCollapse" ExpandControlID="imgCollapse"
                runat="server" SkinID="CollapsiblePanel">
            </ajaxToolkit:CollapsiblePanelExtender>
        </asp:Panel>
    </ContentTemplate>
    
</asp:UpdatePanel>
