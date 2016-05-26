<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Pages_OSDepositBankNsfImport" Theme="SkyStemBlueBrown" MasterPageFile="~/MasterPages/RecProcessMasterPage.master" Codebehind="OSDepositBankNsfImport.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="AccountHierarchyDetail" Src="~/UserControls/AccountHierarchyDetail.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphRecProcess" runat="Server">
    <table width="100%" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td width="7%">
            </td>
            <td width="10%">
                &nbsp
            </td>
            <td width="20%">
                &nbsp
            </td>
            <td width="10%">
                &nbsp
            </td>
            <td width="40%">
                &nbsp
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp<webControls:ExLabel ID="lblImportName" runat="server" LabelID="1396" SkinID="Black11Arial"
                    FormatString="{0}:"></webControls:ExLabel>
            </td>
            <td>
                <asp:TextBox ID="txtImportName" runat="server" SkinID="ExTextBox200" />
                <webControls:ExRequiredFieldValidator ID="vldImportName" runat="server" ControlToValidate="txtImportName"
                    Text="!" LabelID="5000097"></webControls:ExRequiredFieldValidator>
            </td>
            <td>
            </td>
            <td>
                <%--<asp:FileUpload ID="radFileUpload" runat="server" />--%>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <colgroup>
                        <col width="25%" />
                        <col width="35%" />
                        <col width="10%" />
                        <col width="30%" />
                    </colgroup>
                    <tr>
                        <td>
                            <webControls:ExLabel ID="lblImportFile" runat="server" LabelID="1397" SkinID="Black11Arial"
                                FormatString="{0}:"></webControls:ExLabel>
                        </td>
                        <td>
                            <table width="100%">
                                <colgroup>
                                    <col width="90%" />
                                    <col width="10%" />
                                </colgroup>
                                <tr>
                                    <td>
                                        <telerikWebControls:ExRadUpload LabelID="2494" ID="radFileUpload" runat="server" ControlObjectsVisibility="None"
                                            InitialFileInputsCount="1" MaxFileInputsCount="1" Width="100%" />
                                    </td>
                                    <td>
                                        <asp:CustomValidator ID="cvFileUpload" runat="server" ClientValidationFunction="ValidateFileExtension"
                                            Display="Dynamic" ErrorMessage="" OnServerValidate="cvFileUpload_ServerValidate"
                                            Text="!">
                                        </asp:CustomValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <webControls:ExHyperLink ID="hlOpenExcelFile" runat="server" LabelID="1697" Class="BlueBold11Arial"></webControls:ExHyperLink>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="BlankRow">
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
                &nbsp
            </td>
            <td align="right">
                <webControls:ExButton ID="btnImportAll" runat="server" LabelID="2470" OnClick="btnImportAll_Click"
                    SkinID="Black11Arial" CausesValidation="true" />&nbsp;
                <webControls:ExButton ID="btnPreview" runat="server" LabelID="1398" OnClick="btnPreview_Click"
                    SkinID="Black11Arial" CausesValidation="true" />&nbsp;
                <webControls:ExButton ID="btnPageCancel" runat="server" LabelID="1239" OnClick="btnPageCancel_Click"
                    SkinID="Black11Arial" CausesValidation="false" OnClientClick="HideValidationSummary" />
            </td>
        </tr>
        <tr class="BlankRow">
        </tr>
        <tr class="BlankRow">
        </tr>
        <tr>
            <td colspan="5">
                <asp:Panel ID="pnlImportGrid" runat="server" Width="100%">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdnNewPageSize" runat="server" Value="10" />
                                <telerikWebControls:ExRadGrid ID="rgImportList" runat="server" EntityNameLabelID="1229"
                                    AllowPaging="true" AllowSorting="true" OnItemDataBound="rgImportList_ItemDataBound"
                                    OnNeedDataSource="rgImportList_NeedDataSource" OnItemCreated="rgImportList_ItemCreated" OnPageIndexChanged="rgImportList_PageIndexChanged"
                                    OnPageSizeChanged="rgImportList_PageSizeChanged" AllowMultiRowSelection="true"
                                    AllowExportToExcel="false" AllowExportToPDF="false" AllowPrint="true" AllowPrintAll="true"
                                    AllowCustomPaging="true" AllowCauseValidationExportToExcel="false" AllowCauseValidationExportToPDF="false">
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="true" />
                                        <ClientEvents OnRowSelecting="Selecting" OnCommand="RadGrid1_Command" />
                                    </ClientSettings>
                                    <MasterTableView>
                                        <PagerTemplate>
                                            <asp:Panel ID="PagerPanel" runat="server">
                                                <asp:Panel runat="server" ID="pnlPageSizeDDL">
                                                    <div style="float: left; margin-right: 10px;">
                                                    <webControls:ExLabel ID="lblPageSize" runat="server" LabelID="2493"></webControls:ExLabel>
                                                        <%--<span style="margin-right: 3px;">Page size:</span>--%>
                                                        <asp:DropDownList ID="ddlPageSize" SkinID="DropDownList50" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel runat="server" ID="NumericPagerPlaceHolder" />
                                            </asp:Panel>
                                        </PagerTemplate>
                                        <Columns>
                                            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1399">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblDate" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1408">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblDescription" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1773" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblLocalCurrencyCode" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1675" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblLocalCCY" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1513" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblRefNo" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1051" UniqueName="Error" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblError" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="BaseCurrencyExchangeRate" Visible="false">
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ReportingCurrencyExchangeRate"
                                                Visible="false">
                                            </telerikWebControls:ExGridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerikWebControls:ExRadGrid>
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td align="right">
                                <webControls:ExButton ID="btnImport" runat="server" LabelID="1410" OnClick="btnImport_Click"
                                    CausesValidation="false" OnClientClick="return ValidateImport()" />
                                &nbsp;
                                <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClick="btnCancel_Click"
                                    CausesValidation="false" OnClientClick="HideValidationSummary" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
                <input type="hidden" id="Sel" runat="server" />
            </td>
        </tr>
    </table>
    <%--<table width="100%" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="5">
                        <UserControls:ProgressBar ID="ucAccountProfileMassAndBulkUpdate" runat="server" EnableTheming="true"
                            AssociatedUpdatePanelID="upnlDataImport" Visible="true" />
                    </td>
                </tr>
            </table>--%>
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <script type="text/javascript">
            var tableView = null;
            function ddlPageSize_SelectedIndexChanged(ID) {

                if ($find("<%= rgImportList.ClientID %>") != null) {
                    tableView = $find("<%= rgImportList.ClientID %>").get_masterTableView();

                }
                if (tableView != null)
                    tableView.set_pageSize(document.getElementById(ID).value);

            }

         
        </script>

    </telerik:RadScriptBlock>

    <script language="javascript" type="text/javascript">
        var a = Array;
        window.onload = function() {


            var inp = document.getElementById('<% =this.Sel.ClientID %>');
            var data = inp.value;
            if (data != "") {
                var rowsData = data.split(":");
                var i = 0;
                while (typeof (rowsData[i]) != "undefined") {
                    if (rowsData[i] != "") {
                        a[i] = rowsData[i];
                    }
                    i++;
                }
            }
        }
        
        function RadGrid1_Command(sender, args) {
            if (args.get_commandName() == 'Page') {
                a = [];
            }
        }

        function Selecting(sender, args) {
            // Code to reassign value for hidden variable
            var inp = document.getElementById('<% =this.Sel.ClientID %>');
            var data = inp.value;
            if (data != "") {
                var rowsData = data.split(":");
                var i = 0;
                while (typeof (rowsData[i]) != "undefined") {
                    if (rowsData[i] != "") {
                        a[i] = rowsData[i];
                    }
                    i++;
                }
            }
            //
        
            var i = 0;

            while (typeof (a[i]) != "undefined") {
                if (a[i++] == args.get_itemIndexHierarchical()) {
                    args.set_cancel(true);
                }
            }
        }


        function ValidateFileExtension(source, arguments) {
            var flag = 0;

            var uploadControl = getRadUpload('<%= radFileUpload.ClientID %>');
            if (uploadControl != null) {
                var fileInputs = uploadControl.getFileInputs();
                if (fileInputs[0].value == "") {
                    //source.errormessage = "Source file location cannot be empty.";
                    source.errormessage = source.fileNameErrorMessage;
                    arguments.IsValid = false;
                }
                if (!uploadControl.validateExtensions()) {
                    //source.errormessage = "Invalid file extension.";
                    source.errormessage = source.fileExtensionErrorMessage;
                    arguments.IsValid = false;
                }
            }
        }

        function ValidateImport() {
            var grid;
            grid = $find("<%= rgImportList.ClientID %>");
            if (grid != undefined && grid != null) {
                var gridSelectedItems = grid.get_selectedItems();
                if (gridSelectedItems.length <= 0) {
                    alert('<% = Helper.GetAlertMessageFromLabelID(WebConstants.NO_SELECTION_ERROR_MESSAGE) %>');
                    return false;
                }
                else
                    return true;
            }
        }

    </script>

</asp:Content>
