<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true"
    Theme="SkyStemBlueBrown" Inherits="Pages_EmailPopupForRecForm"
    ValidateRequest="false" Codebehind="EmailPopupForRecForm.aspx.cs" %>

<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="3%" />
        <col width="7%" style="padding-left: 15px" />
        <col width="90%" />
        <tr>
            <td class="ManadatoryField"></td>
            <td style="padding-top: 10px;">
                <webControls:ExLabel ID="lblTo" SkinID="Black11Arial" LabelID="1345" runat="server"
                    Width="120px"></webControls:ExLabel>
            </td>
            <td style="padding-top: 10px;">
                <asp:TextBox ID="txtTo" runat="server" SkinID="TextBox200" MaxLength="128"></asp:TextBox>
                <webControls:ExRequiredFieldValidator ID="requiredFieldTo" runat="server" ControlToValidate="txtTo"></webControls:ExRequiredFieldValidator>
                <webControls:ExRegularExpressionValidator ID="revEmailId" runat="server" ControlToValidate="txtTo"
                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*"
                    LabelID="1751"></webControls:ExRegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="ManadatoryField"></td>
            <td style="padding-top: 2px">&nbsp;
            </td>
            <td style="padding-top: 2px">
                <webControls:ExLabel ID="lblEmailToolTip" SkinID="Black9ArialItalic" FormatString="({0})"
                    runat="server" LabelID="1976"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td class="ManadatoryField" style="padding-top: 10px;">*
            </td>
            <td style="padding-top: 10px;">
                <webControls:ExLabel ID="lblSubject" SkinID="Black11Arial" LabelID="1778" runat="server"
                    Width="120px"></webControls:ExLabel>
            </td>
            <td style="padding-top: 10px;">
                <webControls:ExTextBox ID="txtSubject" SkinID="ExTextBox200" IsRequired="true" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="ManadatoryField"></td>
            <td style="padding-top: 2px"></td>
            <td style="padding-top: 10px;">
                <webControls:ExCheckBox ID="chkSendAttachments" runat="server" SkinID="Black11ArialValignMiddle" LabelID="2650" AutoPostBack="true" OnCheckedChanged="chkSendAttachments_CheckedChanged" />
            </td>
        </tr>
        <asp:Panel ID="pnlAttachments" Visible="false" runat="Server">
            <tr>
                <td class="ManadatoryField"></td>
                <td style="padding-top: 2px"></td>
                <td style="padding-top: 10px;">
                    <telerikWebControls:ExRadGrid ID="rgAttachmentList" runat="server" EntityNameLabelID="2618"
                        OnItemDataBound="rgAttachmentList_ItemDataBound" AllowExportToExcel="false" AllowExportToPDF="false"
                        AllowPrint="false" AllowPrintAll="false" ClientSettings-Selecting-AllowRowSelect="true"
                        AllowMultiRowSelection="true" Width="96%">
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" UseClientSelectColumnOnly="true" />
                        </ClientSettings>
                        <MasterTableView AllowSorting="false" AllowPaging="false" DataKeyNames="PhysicalPath" ClientDataKeyNames="FileSize">
                            <Columns>
                                <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" Visible="true"
                                    HeaderStyle-Width="5%">
                                </telerikWebControls:ExGridClientSelectColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1680" SortExpression="DocumentName" HeaderStyle-Width="25%">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblDocumentName" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2027" SortExpression="FileName"
                                    HeaderStyle-Width="25%">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblFileName" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2786" SortExpression="AttachmentType" HeaderStyle-Width="25%">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblAttachmentType" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2785" SortExpression="FileSize" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblFileSize" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerikWebControls:ExRadGrid>
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td class="ManadatoryField"></td>
            <td style="padding-top: 10px;" valign="top">
                <webControls:ExLabel ID="lblMessage" SkinID="Black11Arial" LabelID="1857" runat="server"
                    Width="130px"></webControls:ExLabel>
            </td>
            <td style="padding-top: 10px;">
                <telerik:RadEditor runat="server" ID="txtMessage" Height="140px" EditModes="Design"
                    Width="96%">
                    <Tools>
                        <telerik:EditorToolGroup>
                            <telerik:EditorTool Name="Italic" />
                            <telerik:EditorTool Name="Bold" />
                            <telerik:EditorTool Name="Underline" />
                        </telerik:EditorToolGroup>
                    </Tools>
                    <Content>
                    </Content>
                </telerik:RadEditor>
            </td>
        </tr>
        <tr>
            <td class="ManadatoryField"></td>
            <td></td>
            <td align="right" style="padding-right: 10px; padding-top: 20px">
                <asp:HiddenField ID="hdnInnerHTML" runat="server" />
                <webControls:ExButton ID="btnSend" runat="server" SkinID="ExButton100" LabelID="1903"
                    OnClick="btnSend_Click" />
                &nbsp;
                <webControls:ExButton ID="btnCancel" runat="server" SkinID="ExButton100" LabelID="1239"
                    OnClientClick="window.close();" CausesValidation="false" />
            </td>
        </tr>
        <tr>
            <td colspan="3">&nbsp;
            </td>
        </tr>
        <asp:Panel ID="pnlHeader" runat="server">
            <table class="InputRequrementsHeading" width="800px">
                <tr>
                    <td class="ManadatoryField"></td>
                    <td width="70%">
                        <webControls:ExLabel ID="lblAccounDetails" runat="server" Text="Account Details"
                            SkinID="BlueBold11Arial" />
                    </td>
                    <td width="30%" align="right">
                        <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlMailNotification" runat="server">
            <%--            <webControls:ExLabel ID="lblAccountDetails" SkinID="Black11Arial" runat="server"
                Text="Account Details"></webControls:ExLabel>
            --%>
            <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanelWithBothScroll"
                Style="width: 830px !important;">
                <usercontrol:skystemartgrid id="ucSkyStemARTGrid" runat="server" ongriditemdatabound="ucSkyStemARTGrid_GridItemDataBound"
                    grid-allowexporttoexcel="True" grid-allowexporttopdf="True" grid-allowcausevalidationexporttoexcel="false"
                    grid-allowcausevalidationexporttopdf="false">
                    <ClientSettings>
                        <Selecting UseClientSelectColumnOnly="true" />
                    </ClientSettings>
                    <SkyStemGridColumnCollection>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="AccountNumber" LabelID="1357"
                            SortExpression="AccountNumber" DataType="System.String">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblAccountNumber" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn LabelID="1346" SortExpression="AccountName"
                            UniqueName="AccountName" DataType="System.String">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblAccountName" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn LabelID="1257" SortExpression="NetAccount"
                            UniqueName="NetAccount" DataType="System.String">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblNetAccount" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="PreparerExport" LabelID="1130">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblPreparerExport" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="ReviewerExport" LabelID="1131">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblReviewerExport" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="ApproverExport" LabelID="1132">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblApproverExport" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="BackupPreparerExport" LabelID="2501">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblBackupPreparerExport" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="BackupReviewerExport" LabelID="2502">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblBackupReviewerExport" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="BackupApproverExport" LabelID="2503">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblBackupApproverExport" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn LabelID="1824" UniqueName="ExcludeOwnershipForZBA"
                            Visible="false">
                            <ItemTemplate>
                                <webControls:ExCheckBox ID="chkExcludeOwnershipForZBA" runat="server"></webControls:ExCheckBox>
                                <input id="txtExcludeOwnershipValue" runat="server" style="display: none" />
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                    </SkyStemGridColumnCollection>
                </usercontrol:skystemartgrid>
            </asp:Panel>
        </asp:Panel>
        <ajaxToolkit:CollapsiblePanelExtender ID="cpeMailNotification" TargetControlID="pnlMailNotification"
            ImageControlID="imgCollapse" CollapseControlID="pnlHeader" ExpandControlID="pnlHeader"
            runat="server" SkinID="CollapsiblePanel" Collapsed="false">
        </ajaxToolkit:CollapsiblePanelExtender>
    </table>
    <input type="text" id="Sel" runat="server" style="display: none" />
</asp:Content>
