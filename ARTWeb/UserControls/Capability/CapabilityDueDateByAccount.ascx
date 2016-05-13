<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CapabilityDueDateByAccount.ascx.cs"
    Inherits="UserControls_CapabilityDueDateByAccount" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:UpdatePanel ID="upnlDueDateByAccount" runat="server" UpdateMode="Conditional">
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
                    <tr id="trDueDateByAccount" runat="server">
                        <td class="ManadatoryField"></td>
                        <td width="28%">
                            <webControls:ExLabel ID="lblDueDateByAccount" runat="server" LabelID="2756 " FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExRadioButton ID="OptDueDateByAccountYes" runat="server" GroupName="OptDueDateByAccount"
                                OnCheckedChanged="OptDueDateByAccountYes_CheckedChanged" AutoPostBack="true" LabelID="1252" SkinID="OptBlack11Arial" />
                        </td>
                        <td id="tdCapabilityStatus" runat="server">
                            <webControls:ExRadioButton ID="OptDueDateByAccountNo" runat="server" GroupName="OptDueDateByAccount"
                                OnCheckedChanged="OptDueDateByAccountNo_CheckedChanged" AutoPostBack="true" LabelID="1251" SkinID="OptBlack11Arial" />
                        </td>
                        <td>
                            <webControls:ExImage ID="imgStatusDueDateByAccountForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                            <webControls:ExImage ID="imgStatusDueDateByAccountForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
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
                            <asp:Panel ID="pnlContentDueDateByAccount" runat="server" SkinID="pnlExtended" Width="100%">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <col width="2%" />
                                    <col width="35%" />
                                    <col width="25%" />
                                    <col width="38%" />
                                    <tr>
                                        <td class="ManadatoryField">*
                                        </td>
                                        <td>
                                            <webControls:ExLabel ID="lblDayType" runat="server" LabelID="2963" FormatString="{0}:"
                                                SkinID="Black11Arial"></webControls:ExLabel>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDayType" runat="server" SkinID="DropDownList230">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDayType" runat="server" ControlToValidate="ddlDayType"
                                                InitialValue="-2" Font-Bold="true" Font-Size="Medium">!</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <webControls:ExImage ID="imgStatusDayTypeForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                                            <webControls:ExImage ID="imgStatusDayTypeForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
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
