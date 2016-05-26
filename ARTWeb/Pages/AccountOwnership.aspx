<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Pages_AccountOwnership" MasterPageFile="~/MasterPages/ARTMasterPage.master"
    Theme="SkyStemBlueBrown" Codebehind="AccountOwnership.aspx.cs" %>

<%@ Register Src="~/UserControls/LegendOnAccountSearch.ascx" TagName="LegendOnAccountSearch"
    TagPrefix="UserControl" %>
<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<%@ Register Src="~/UserControls/AccountSearchControl.ascx" TagName="AccountSearchControl"
    TagPrefix="UserControl" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="DropDownList" Src="~/UserControls/UserDropDown.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnlAccountProfile" runat="server">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <UserControl:AccountSearchControl ID="ucAccountSearchControl" IsOnAccountOwnerShipPage="true"
                            runat="server" />
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <asp:Panel ID="pnlMassUpdate" runat="server" Visible="false">
                    <tr>
                        <td>
                            <%--<asp:Panel ID="pnlGridMassUpdate" runat="server" SkinID="RadGridScrollPanelWithBothScroll">--%>
                            <UserControl:SkyStemARTGrid ID="SkyStemARTGridMassUpdate" runat="server" OnGridItemDataBound="SkyStemARTGridMassUpdate_GridItemDataBound"
                                Grid-AllowExportToExcel="True" Grid-AllowExportToPDF="True" Grid-AllowCauseValidationExportToExcel="false"
                                Grid-AllowCauseValidationExportToPDF="false" BasePageTitle="1212" Grid-AllowSorting="true">
                                <ClientSettings>
                                    <Selecting UseClientSelectColumnOnly="true" />
                                </ClientSettings>
                                <SkyStemGridColumnCollection>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="AccountNumber" LabelID="1357"
                                        HeaderStyle-Width="10%" SortExpression="AccountNumber" DataType="System.String">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblAccountNumberMass" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1346" HeaderStyle-Width="15%" SortExpression="AccountName"
                                        UniqueName="AccountName" DataType="System.String">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblAccountNameMass" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1257" HeaderStyle-Width="10%" SortExpression="NetAccount"
                                        UniqueName="NetAccount" DataType="System.String">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblNetAccountMass" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="Preparer" LabelID="1130" HeaderStyle-Width="5%" SortExpression="PreparerFullName"
                                        DataType="System.String">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblPreparer" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="Reviewer" LabelID="1131" HeaderStyle-Width="5%" SortExpression="ReviewerFullName" DataType="System.String">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblReviewer" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="Approver" LabelID="1132" HeaderStyle-Width="5%" SortExpression="ApproverFullName" DataType="System.String">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblApprover" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="BackupPreparer" LabelID="2501" SortExpression="BackupPreparerFullName" DataType="System.String"
                                        Visible="false" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblBackupPreparer" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="BackupReviewer" LabelID="2502" SortExpression="BackupReviewerFullName" DataType="System.String"
                                        Visible="false" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblBackupReviewer" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="BackupApprover" LabelID="2503" SortExpression="BackupApproverFullName" DataType="System.String"
                                        Visible="false" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblBackupApprover" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                </SkyStemGridColumnCollection>
                            </UserControl:SkyStemARTGrid>
                            <%-- </asp:Panel>--%>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr id="rowAttributeSetting" runat="server">
                        <td>
                            <table width="100%">
                                <tr>
                                    <td class="ManadatoryField">
                                        *
                                    </td>
                                    <td>
                                        <webControls:ExLabel ID="lblAttribute" LabelID="1440" runat="server" SkinID="Black11Arial"
                                            FormatString="{0}:"></webControls:ExLabel>
                                        <asp:DropDownList ID="ddlAccountAttribute" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAccountAttribute_SelectedIndexChangedHandler"
                                            SkinID="DropDownList150">
                                        </asp:DropDownList>
                                        <webControls:ExRequiredFieldValidator ID="rfvAccountAttribute" runat="server" InitialValue="-2"
                                            ControlToValidate="ddlAccountAttribute"></webControls:ExRequiredFieldValidator>
                                        &nbsp;
                                        <%-- <UserControls:DropDownList ID="ucPreparer" runat="server" IsPreparer="true" Width="150">
                                        </UserControls:DropDownList>
                                        <UserControls:DropDownList ID="ucReviewer" runat="server" IsReviewer="true" Width="150">
                                        </UserControls:DropDownList>
                                        <UserControls:DropDownList ID="ucApprover" runat="server" IsApprover="true" Width="150">
                                        </UserControls:DropDownList>
                                        <UserControls:DropDownList ID="ucBackupPreparer" runat="server" IsBackupPreparer="true"
                                            Width="150"></UserControls:DropDownList>
                                        <UserControls:DropDownList ID="ucBackupReviewer" runat="server" IsBackupReviewer="true"
                                            Width="150"></UserControls:DropDownList>
                                        <UserControls:DropDownList ID="ucBackupApprover" runat="server" IsBackupApprover="true"
                                            Width="150"></UserControls:DropDownList>--%>
                                        <asp:TextBox ID="txtOwner" runat="server" Visible="true" SkinID="TextBox200" />
                                        <img id="imgUserNameProgress" style="visibility: hidden" alt="imgProgress" src="<%= ResolveClientUrlPath("~/App_Themes/SkyStemBlueBrown/Images/progress_small.gif") %>" />
                                        <ajaxToolkit:AutoCompleteExtender TargetControlID="txtOwner" ServiceMethod="AutoCompleteOwnerName"
                                            runat="server" ID="aceAssignedTo" OnClientPopulating="ShowUserNameProgressIcon"
                                            OnClientPopulated="ShowUserNameProgressIcon" OnClientItemSelected="OwnerSelected"
                                            OnClientShowing="ClearSelectedOwner">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlAccounts" runat="server" Visible="false">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanelWithBothScroll">
                                <UserControl:SkyStemARTGrid ID="ucSkyStemARTGrid" runat="server" OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound"
                                    Grid-AllowExportToExcel="True" Grid-AllowExportToPDF="True" Grid-AllowCauseValidationExportToExcel="false"
                                    Grid-AllowCauseValidationExportToPDF="false" BasePageTitle="1212" Grid-AllowSorting="true">
                                    <ClientSettings>
                                        <Selecting UseClientSelectColumnOnly="true" />
                                    </ClientSettings>
                                    <SkyStemGridColumnCollection>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="AccountNumber" LabelID="1357"
                                            HeaderStyle-Width="10%" SortExpression="AccountNumber" DataType="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblAccountNumber" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1346" HeaderStyle-Width="15%" SortExpression="AccountName"
                                            UniqueName="AccountName" DataType="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblAccountName" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1257" HeaderStyle-Width="10%" SortExpression="NetAccount"
                                            UniqueName="NetAccount" DataType="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblNetAccount" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1130" HeaderStyle-Width="6%" UniqueName="Preparer"
                                            DataType="System.String">
                                            <ItemTemplate>
                                                <UserControls:DropDownList ID="ucPreparer" runat="server" IsPreparer="true" Width="150">
                                                </UserControls:DropDownList>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="PreparerExport" LabelID="1130"
                                            Visible="false" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblPreparerExport" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1131" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Left"
                                            UniqueName="Reviewer">
                                            <ItemTemplate>
                                                <UserControls:DropDownList ID="ucReviewer" runat="server" IsReviewer="true" Width="150">
                                                </UserControls:DropDownList>
                                                <asp:CustomValidator ID="vldComparePreparerAndReviewer" ClientValidationFunction="ValidatePreparerAndReviewer"
                                                    runat="server" Text="!" Font-Bold="true" Font-Size="Medium"></asp:CustomValidator>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="ReviewerExport" LabelID="1131"
                                            Visible="false" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblReviewerExport" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1132" HeaderStyle-Width="6%" UniqueName="Approver">
                                            <ItemTemplate>
                                                <UserControls:DropDownList ID="ucApprover" runat="server" IsApprover="true" Width="150">
                                                </UserControls:DropDownList>
                                                <asp:CustomValidator ID="vldComparePreparerAndApprover" ClientValidationFunction="ValidatePreparerAndApprover"
                                                    runat="server" Text="!" Font-Bold="true" Font-Size="Medium"></asp:CustomValidator>
                                                <asp:CustomValidator ID="vldCompareApproverAndReviewer" ClientValidationFunction="ValidateApproverAndReviewer"
                                                    runat="server" Text="!" Font-Bold="true" Font-Size="Medium"></asp:CustomValidator>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="ApproverExport" LabelID="1132"
                                            Visible="false" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblApproverExport" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2501" HeaderStyle-Width="6%" UniqueName="BackupPreparer"
                                            DataType="System.String">
                                            <ItemTemplate>
                                                <UserControls:DropDownList ID="ucBackupPreparer" runat="server" IsBackupPreparer="true"
                                                    Width="150"></UserControls:DropDownList>
                                                <asp:CustomValidator ID="vldCompareBackupPreparer" ClientValidationFunction="ValidateBackupPreparer"
                                                    runat="server" Text="!" Font-Bold="true" Font-Size="Medium"></asp:CustomValidator>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="BackupPreparerExport" LabelID="2501"
                                            Visible="false" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblBackupPreparerExport" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2502" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Left"
                                            UniqueName="BackupReviewer">
                                            <ItemTemplate>
                                                <UserControls:DropDownList ID="ucBackupReviewer" runat="server" IsBackupReviewer="true"
                                                    Width="150"></UserControls:DropDownList>
                                                <asp:CustomValidator ID="vldCompareBackupReviewer" ClientValidationFunction="ValidatePreparerAndReviewer"
                                                    runat="server" Text="!" Font-Bold="true" Font-Size="Medium"></asp:CustomValidator>
                                                <%--                                                <asp:CustomValidator ID="vldCompareBackupPreparerAndReviewer" ClientValidationFunction="ValidatePreparerAndReviewer"
                                                    runat="server" Enabled="false" Text="!" Font-Bold="true" Font-Size="Medium"></asp:CustomValidator>
--%>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="BackupReviewerExport" LabelID="2502"
                                            Visible="false" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblBackupReviewerExport" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2503" HeaderStyle-Width="6%" UniqueName="BackupApprover">
                                            <ItemTemplate>
                                                <UserControls:DropDownList ID="ucBackupApprover" runat="server" IsBackupApprover="true"
                                                    Width="150"></UserControls:DropDownList>
                                                <asp:CustomValidator ID="vldCompareBackupApprover" ClientValidationFunction="ValidatePreparerAndReviewer"
                                                    runat="server" Text="!" Font-Bold="true" Font-Size="Medium"></asp:CustomValidator>
                                                <asp:CustomValidator ID="cvApproverBackupApprover" runat="server" Text="!" Font-Bold="true"
                                                    ClientValidationFunction="ValidateApproverBackupApprover"></asp:CustomValidator>
                                                <%--                                                <asp:CustomValidator ID="vldCompareBackupPreparerAndApprover" ClientValidationFunction="ValidatePreparerAndApprover"
                                                    runat="server" Enabled="false" Text="!" Font-Bold="true" Font-Size="Medium"></asp:CustomValidator>
                                                <asp:CustomValidator ID="vldCompareBackupApproverAndReviewer" ClientValidationFunction="ValidateApproverAndReviewer"
                                                    runat="server" Enabled="false" Text="!" Font-Bold="true" Font-Size="Medium"></asp:CustomValidator>
--%>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="BackupApproverExport" LabelID="2503"
                                            Visible="false" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblBackupApproverExport" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1824" UniqueName="ExcludeOwnershipForZBA">
                                            <ItemTemplate>
                                                <webControls:ExCheckBox ID="chkExcludeOwnershipForZBA" runat="server"></webControls:ExCheckBox>
                                                <input id="txtExcludeOwnershipValue" runat="server" style="display: none" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </SkyStemGridColumnCollection>
                                </UserControl:SkyStemARTGrid>
                            </asp:Panel>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td align="right">
                        <webControls:ExButton ID="btnSave" runat="server" Visible="false" LabelID="1315"
                            OnClick="btnSave_Click" OnClientClick="return ResetAllValidationGroup();" />&nbsp;
                        <webControls:ExButton ID="btnReset" runat="server" LabelID="2482" Visible="false"
                            OnClientClick="return confirmReset(this);" SkinID="ExButton100" OnClick="btnReset_Click" />&nbsp;
                        <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" Visible="false"
                            OnClick="btnCancel_Click" CausesValidation="false" OnClientClick="return HideValidationSummary()" />&nbsp;
                        <asp:HiddenField ID="hdnConfirm" runat="server" />
                        <asp:HiddenField ID="hdnOwner" runat="server" />
                        <asp:HiddenField ID="hdnConfirm_BlankOwner" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <UserControl:LegendOnAccountSearch ID="LegendOnAccountSearch" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <UserControls:ProgressBar ID="ucAccountProfileMassAndBulkUpdate" runat="server" EnableTheming="true"
                            AssociatedUpdatePanelID="upnlAccountProfile" Visible="true" />
                        <input type="text" id="Sel" runat="server" style="display: none" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        function ConfirmNetAccount(btnSave, hdnConf, msg) {
            var confirm_value = document.getElementById(hdnConf);
            var btnSave = document.getElementById(btnSave);
            if (confirm(msg)) {
                confirm_value.value = "Yes";
                btnSave.click();
            } else {
                confirm_value.value = "No";
            }
        }
        function ConfirmBlankOwner(btnSave, hdnConfirm_BlankOwner, msg) {
            var confirm_value = document.getElementById(hdnConfirm_BlankOwner);
            var btnSave = document.getElementById(btnSave);
            if (confirm(msg)) {
                confirm_value.value = "Yes";
                btnSave.click();
            } else {
                confirm_value.value = "No";
            }
        }

        function confirmReset(btn) {
            var grid = $find("<%= ucSkyStemARTGrid.RgAccount.ClientID %>");
            if (grid != null && grid != "undefined") {
                var gridSelectedItems = grid.get_selectedItems();
                if (gridSelectedItems.length <= 0) {
                    alert('<% = Helper.GetAlertMessageFromLabelID(WebConstants.NO_SELECTION_ERROR_MESSAGE) %>');
                    return false;
                }
            }
            return confirm('<% = Helper.GetAlertMessageFromLabelID(WebConstants.CONFIRM_FOR_RE_SET) %>');
        }

        function Selecting(sender, args) {

            var bSelectRow = true;
            var inp = document.getElementById('<% =this.Sel.ClientID %>');
            var data = inp.value;
            var a = Array;
            if (data != "") {
                var rowsData = data.split(":");
                var i = 0;
                while (typeof (rowsData[i]) != "undefined") {
                    if (rowsData[i++] == args.get_itemIndexHierarchical()) {
                        bSelectRow = false;
                        break;
                    }
                }
            }
            if (bSelectRow == true)
                args.set_cancel(false);
            else
                args.set_cancel(true);
        }
        function ValidateApproverBackupApprover(sender, args) {
            var clientIDList = sender.getAttribute("ApproverBackupApproverClientID");
            var arrayClientID = clientIDList.split(",");
            var ddlApprover = document.getElementById(arrayClientID[0]);
            var ddlBackUpApprover = document.getElementById(arrayClientID[1]);
            var ddlApproverValue;
            var ddlBackUpApproverValue;
            if (ddlApprover != null && ddlApprover != 'undefined') {
                ddlApproverValue = ddlApprover.options[ddlApprover.selectedIndex].value;
            }
            if (ddlBackUpApprover != null && ddlBackUpApprover != 'undefined') {
                ddlBackUpApproverValue = ddlBackUpApprover.options[ddlBackUpApprover.selectedIndex].value;
            }
            if (ddlApproverValue == "-2" && ddlBackUpApproverValue != "-2") {
                args.IsValid = false;
                return;
            }

        }
        function OwnerSelected(source, eventArgs) {
            //alert(" Key : " + eventArgs.get_text() + "  Value :  " + eventArgs.get_value());           
            $get("<%=hdnOwner.ClientID %>").value = "";
            $get("<%=hdnOwner.ClientID %>").value = eventArgs.get_value();
        }
        function ClearSelectedOwner(source, eventArgs) {
            $get("<%=hdnOwner.ClientID %>").value = "";
        }
        
    </script>

</asp:Content>
