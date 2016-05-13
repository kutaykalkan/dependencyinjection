<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CapabilityActivationDualLevelReview.ascx.cs"
    Inherits="UserControls_CapabilityActivationDualLevelReview" %>
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
                        <td class="ManadatoryField">
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblDualReview" runat="server" LabelID="1015" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel><%--1253--%>
                        </td>
                        <td>
                            <webControls:ExRadioButton ID="optDualReviewYes" GroupName="optDualLevelReview" runat="server"
                                OnCheckedChanged="optDualReviewYes_CheckedChanged" AutoPostBack="true" LabelID="1252"
                                SkinID="OptBlack11Arial" />
                        </td>
                        <td id="tdCapabilityStatus" runat="server">
                            <webControls:ExRadioButton ID="optDualReviewNo" GroupName="optDualLevelReview" runat="server"
                                OnCheckedChanged="optDualReviewNo_CheckedChanged" AutoPostBack="true" LabelID="1251"
                                SkinID="OptBlack11Arial" />
                        </td>
                        <td>
                            <webControls:ExImage ID="imgStatusDualReviewForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                            <webControls:ExImage ID="imgStatusDualReviewForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
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
                        <asp:Panel ID="pnlContentDualLevelReview" runat="server" SkinID="pnlExtended" Width="100%">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <col width="2%" />
                                    <col width="35%" />
                                    <col width="25%" />
                                    <col width="38%" />
                                    <tr>
                                        <td class="ManadatoryField">
                                            *
                                        </td>
                                        <td>
                                            <webControls:ExLabel ID="lblDualLevelReviewType" runat="server" LabelID="2765 " FormatString="{0}:"
                                                SkinID="Black11Arial"></webControls:ExLabel>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDualLevelReviewType" runat="server" SkinID="DropDownList230">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDualLevelReviewType" runat="server" ControlToValidate="ddlDualLevelReviewType"
                                                InitialValue="-2" Font-Bold="true" Font-Size="Medium">!</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <webControls:ExImage ID="imgStatusDualLevelReviewTypeForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                                            <webControls:ExImage ID="imgStatusDualLevelReviewTypeForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
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
    <%--<Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="optMaterialityYes" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="optMaterialityNo" EventName="CheckedChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlMaterialityType" EventName="SelectedIndexChanged" />
         </Triggers>--%>
</asp:UpdatePanel>
<%--<script type="text/javascript" language="javascript">
    function CollapseOnCancelMateriality(sender, args) {
        var objExtender = $find("<%=cpeMateriality.ClientID%>");
        try { objExtender._doClose(); } catch (e) { }  // Collapse it
    }
        
</script>--%>
