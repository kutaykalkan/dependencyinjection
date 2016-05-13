<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CapabilityMandatoryReportSignoff.ascx.cs"
    Inherits="UserControls_CapabilityMandatoryReportSignoff" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:UpdatePanel ID="upnlMandatoryReportSignoff" runat="server" UpdateMode="Conditional">
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
                            <webControls:ExLabel ID="lblMandatoryReportSignoff" runat="server" LabelID="1980"
                                FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td class="tdCapabilityNoOpt">
                            <webControls:ExRadioButton ID="optMandatoryReportSignoffYes" GroupName="optMandatoryReportSignoff"
                                runat="server" OnCheckedChanged="optMandatoryReportSignoffYes_CheckedChanged"
                                AutoPostBack="true" LabelID="1252" SkinID="OptBlack11Arial" />
                        </td>
                        <td id="tdCapabilityStatus" runat="server">
                            <%--class="tdCapabilityStatusIcon"--%>
                            <webControls:ExRadioButton ID="optMandatoryReportSignoffNo" GroupName="optMandatoryReportSignoff"
                                runat="server" OnCheckedChanged="optMandatoryReportSignoffNo_CheckedChanged"
                                AutoPostBack="true" LabelID="1251" SkinID="OptBlack11Arial" />
                        </td>
                        <td>
                            <webControls:ExImage ID="imgStatusMandatoryReportSignoffForwardYes" runat="server"
                                SkinID="CapabilityForwardedYes" />
                            <webControls:ExImage ID="imgStatusMandatoryReportSignoffForwardNo" runat="server"
                                SkinID="CapabilityForwardedNo" />
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
                            <asp:Panel ID="pnlContentMandatoryReportSignoff" runat="server" SkinID="pnlExtended"
                                Width="100%">
                                <asp:Panel ID="pnlContentMandatoryReportSignoffReviewer" runat="server" Width="100%">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <col width="2%" />
                                        <col width="35%" />
                                        <col width="25%" />
                                        <col width="38%" />
                                        <tr class="BlankRow">
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ManadatoryField">
                                            </td>
                                            <td>
                                                <webControls:ExLabel ID="lblReviewerMandatoryReportSignoffRole" runat="server" LabelID="1278 "
                                                    FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <td>
                                                <webControls:ExLabel ID="lblReviewerMandatoryReportSignoffRoleValue" runat="server"
                                                    LabelID="1131  " SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ManadatoryField">
                                            </td>
                                            <td valign="top">
                                                <webControls:ExLabel ID="lblReviewerReport" runat="server" LabelID="1375 " FormatString="{0}:"
                                                    SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <td>
                                                <asp:CheckBoxList ID="cblReviewerMandatoryReportSignoff" runat="server" SkinID="cblDefault" />
                                            </td>
                                            <td valign="top">
                                                <webControls:ExCustomValidator runat="server" ClientValidationFunction="validateAtLeastOneItemInCBL"
                                                    ID="cvReviewerMandatoryReport" Font-Bold="true" Font-Size="Medium">!</webControls:ExCustomValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlContentMandatoryReportSignoffApprover" runat="server" Width="100%">
                                    <hr />
                                    <%--<table style="border-bottom: solid 1px #ab6501" width="100%">--%>
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <col width="2%" />
                                        <col width="35%" />
                                        <col width="25%" />
                                        <col width="38%" />
                                        <tr class="BlankRow">
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ManadatoryField">
                                            </td>
                                            <td>
                                                <webControls:ExLabel ID="lblApproverMandatoryReportSignoffRole" runat="server" LabelID="1278"
                                                    FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <td>
                                                <webControls:ExLabel ID="lblApproverMandatoryReportSignoffRoleValue" runat="server"
                                                    LabelID="1132  " SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ManadatoryField">
                                            </td>
                                            <td valign="top">
                                                <webControls:ExLabel ID="lblApproverReport" runat="server" LabelID="1375  " FormatString="{0}:"
                                                    SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <td>
                                                <asp:CheckBoxList ID="cblApproverMandatoryReportSignoff" runat="server" SkinID="cblDefault" />
                                            </td>
                                            <td valign="top">
                                                <webControls:ExCustomValidator runat="server" ClientValidationFunction="validateAtLeastOneItemInCBL"
                                                    ID="cvApproverMandatoryReport" Font-Bold="true" Font-Size="Medium">!</webControls:ExCustomValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:CollapsiblePanelExtender ID="cpeMandatoryReportSignoff" TargetControlID="pnlContent"
                ImageControlID="imgCollapse" CollapseControlID="imgCollapse" ExpandControlID="imgCollapse"
                runat="server" SkinID="CollapsiblePanel">
            </ajaxToolkit:CollapsiblePanelExtender>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
