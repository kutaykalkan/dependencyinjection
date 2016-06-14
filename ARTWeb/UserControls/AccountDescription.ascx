<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_AccountDescription" Codebehind="AccountDescription.ascx.cs" %>
<table width="100%" cellpadding="0" cellspacing="0" class="blueBorder">
    <tr class="blueRow">
        <td align="center">
            <webControls:ExLabel ID="lblAccountDetails" runat="server" SkinID="AccountDetail"></webControls:ExLabel>
        </td>
        <td align="right" style="padding-right: 5px; padding-top: 2px">
            <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Panel ID="pnlDetail" runat="server">
                <table width="100%">
                    <tr>
                        <td valign="middle">
                            <webControls:ExHyperLink ID="hlAccountPolicyURL" runat="server" SkinID="HyperLinkBoldBlue" LabelID="1461">
                                        &nbsp;&nbsp;&nbsp;
                            </webControls:ExHyperLink>
                      
                            <%--<webControls:ExLabel ID="lblAccountPolicyURL" runat="server" LabelID="1461" SkinID="SubSectionHeading"></webControls:ExLabel>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="AccountPolicyDetails">
                            <webControls:ExLabel ID="lblAccountPolicyURLValue" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                         <webControls:ExHyperLink ID="hlReconciliationProcedure" runat="server" SkinID="HyperLinkBoldBlue" LabelID="1360">
                                        &nbsp;&nbsp;&nbsp;
                            </webControls:ExHyperLink>
                           <%-- <webControls:ExLabel ID="lblReconciliationProcedure" runat="server" LabelID="1360"
                                SkinID="SubSectionHeading"></webControls:ExLabel>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="AccountPolicyDetails">
                            <webControls:ExLabel ID="lblReconciliationProcedureValue" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <webControls:ExHyperLink ID="hlAccountDescription" runat="server" SkinID="HyperLinkBoldBlue" LabelID="1460">
                                        &nbsp;&nbsp;&nbsp;
                            </webControls:ExHyperLink>
                            <%--<webControls:ExLabel ID="lblAccountDescription" runat="server" LabelID="1460" SkinID="SubSectionHeading"></webControls:ExLabel>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="AccountPolicyDetails" >
                            <webControls:ExLabel ID="lblAccountDescriptionValue" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:CollapsiblePanelExtender runat="server" ID="cpeAccountDetail" BehaviorID="cpeAccountDetail"
                TargetControlID="pnlDetail" ExpandControlID="imgCollapse" CollapseControlID="imgCollapse"
                ExpandedText="Collapse..." CollapsedText="Expand..." CollapsedImage="~/App_Themes/SkyStemBlueBrown/Images/Expand.gif"
                ExpandedImage="~/App_Themes/SkyStemBlueBrown/Images/Collapse.gif" ImageControlID="imgCollapse"
                ExpandDirection="Vertical" CollapsedSize="0" SuppressPostBack="true">
            </ajaxToolkit:CollapsiblePanelExtender>
        </td>
    </tr>
</table>
