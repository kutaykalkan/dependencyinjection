<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CapabilityActivationMateriality.ascx.cs"
    Inherits="UserControls_CapabilityActivationMateriality" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:UpdatePanel ID="upnlMateriality" runat="server" UpdateMode="Conditional">
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
                            <webControls:ExLabel ID="lblMateriality" runat="server" LabelID="1253" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel><%--1253--%>
                        </td>
                        <td>
                            <webControls:ExRadioButton ID="optMaterialityYes" GroupName="optMateriality" runat="server"
                                OnCheckedChanged="optMaterialityYes_CheckedChanged" AutoPostBack="true" LabelID="1252"
                                SkinID="OptBlack11Arial" />
                        </td>
                        <td id="tdCapabilityStatus" runat="server">
                            <webControls:ExRadioButton ID="optMaterialityNo" GroupName="optMateriality" runat="server"
                                OnCheckedChanged="optMaterialityNo_CheckedChanged" AutoPostBack="true" LabelID="1251"
                                SkinID="OptBlack11Arial" />
                        </td>
                        <td>
                            <webControls:ExImage ID="imgStatusMaterialityForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                            <webControls:ExImage ID="imgStatusMaterialityForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
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
                            <asp:Panel ID="pnlContentMateriality" runat="server" SkinID="pnlExtended" Width="100%">
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
                                            <webControls:ExLabel ID="lblMaterialityType" runat="server" LabelID="1281 " FormatString="{0}:"
                                                SkinID="Black11Arial"></webControls:ExLabel>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMaterialityType" runat="server" OnSelectedIndexChanged="ddlMaterialityType_SelectedIndexChanged"
                                                AutoPostBack="true" SkinID="DropDownList200">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvMaterialityType" runat="server" ControlToValidate="ddlMaterialityType"
                                                InitialValue="-2" Font-Bold="true" Font-Size="Medium">!</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <webControls:ExImage ID="imgStatusMaterialityTypeForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                                            <webControls:ExImage ID="imgStatusMaterialityTypeForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="divCompanywideMaterialityThreshold" runat="server" Width="100%">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <col width="2%" />
                                        <col width="35%" />
                                        <col width="63%" />
                                        <tr class="BlankRow">
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ManadatoryField">
                                                *
                                            </td>
                                            <td>
                                                <webControls:ExLabel ID="lblCompanywideMaterialityThreshold" runat="server" LabelID="1282  "
                                                    FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel><%--Companywide Materiality Threshold--%>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCompanywideMaterialityThreshold" runat="server" SkinID="ExTextBox200" />
                                                <asp:HiddenField ID="hdnCompanywideMaterialityThreshold" runat="server" />
                                                <asp:RequiredFieldValidator ID="rfvMaterialValue" runat="server" ControlToValidate="txtCompanywideMaterialityThreshold"
                                                    Text="!" Font-Bold="true" Font-Size="Medium"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="cvCompanywideMaterialValue" runat="server" Text="!" ControlToValidate="txtCompanywideMaterialityThreshold"
                                                    Font-Bold="true" Font-Size="Medium" ClientValidationFunction="ValidateNumbers"></asp:CustomValidator>
                                             <%--   <webControls:ExRegularExpressionValidator LabelID="2492" Text="!" ValidationExpression="^(\d{0,14}\.\d{0,4}|\d{0,14})$"
                                                    ID="rgVariance" runat="server" ControlToValidate="txtCompanywideMaterialityThreshold" Font-Bold="true"
                                                    Font-Size="Medium">
                                                </webControls:ExRegularExpressionValidator>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="divFSCaptionwideMaterialityThreshold" runat="server" Width="100%">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <telerikWebControls:ExRadGrid ID="rdFSCaptionwideMateriality" runat="server" EntityNameLabelID="1422 "
                                                    AllowSorting="false" OnItemDataBound="rdFSCaptionwideMateriality_ItemDataBound"
                                                    AllowExportToExcel="false" AllowExportToPDF="false" AllowPrint="false" AllowPrintAll="false"
                                                    Width="99.80%">
                                                    <MasterTableView DataKeyNames="FSCaptionID">
                                                        <Columns>
                                                            <telerikWebControls:ExGridTemplateColumn LabelID="1337" SortExpression="FSCaption"
                                                                HeaderStyle-Width="30%">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblFSCaptionName" runat="server" FormatString="{0}:" SkinID="Black11Arial" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn LabelID="1259 " SortExpression="MaterialityThreshold">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtMaterialValue" runat="server" SkinID="ExTextBox200" />
                                                                    <asp:RequiredFieldValidator ID="rfvMaterialValue" runat="server" ControlToValidate="txtMaterialValue"
                                                                        Text="!" Font-Bold="true" Font-Size="Medium"></asp:RequiredFieldValidator>
                                                                    <asp:CustomValidator ID="cvMaterialValue" runat="server" Text="!" ControlToValidate="txtMaterialValue"
                                                                        Font-Bold="true" Font-Size="Medium" ClientValidationFunction="ValidateNumbers"></asp:CustomValidator>
                                                              <%--      <webControls:ExRegularExpressionValidator LabelID="2492" Text="!" ValidationExpression="^(\d{0,14}\.\d{0,4}|\d{0,14})$"
                                                                        ID="rgVariance" runat="server" ControlToValidate="txtMaterialValue" Font-Bold="true"
                                                                        Font-Size="Medium">
                                                                    </webControls:ExRegularExpressionValidator>--%>
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerikWebControls:ExRadGrid>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
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
