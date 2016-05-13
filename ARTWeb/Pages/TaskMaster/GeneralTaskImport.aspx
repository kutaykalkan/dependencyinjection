<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true"
    CodeFile="GeneralTaskImport.aspx.cs" Inherits="Pages_TaskMaster_GeneralTaskImport"
    Theme="SkyStemBlueBrown" %>

<%@ Register TagPrefix="UserControls" TagName="AccountHierarchyDetail" Src="~/UserControls/AccountHierarchyDetail.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<asp:Content ID="cphAccountTaskImport" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="Server">
    <table width="100%" width="100%" cellpadding="0" cellspacing="0">
        <colgroup>
            <col width="2%" />
            <col width="45%" />
            <col width="6%" />
            <col width="45%" />
        </colgroup>
        <tr>
            <td>
            </td>
            <td>
                <table width="100%" border="0" cellpadding="2px" cellspacing="0">
                    <colgroup>
                        <col width="2%" />
                        <col width="34%" />
                        <col width="2%" />
                        <col width="60%" />
                        <col width="2%" />
                    </colgroup>
                    <tr>
                        <td class="ManadatoryField">
                            *
                        </td>
                        <td style="white-space: nowrap">
                            <webControls:ExLabel ID="lblImportName" runat="server" LabelID="1396" SkinID="Black11Arial"
                                FormatString="{0}:"></webControls:ExLabel>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtImportName" runat="server" SkinID="ExTextBox200" />
                            <webControls:ExRequiredFieldValidator ID="vldImportName" runat="server" ControlToValidate="txtImportName"
                                Text="!" LabelID="5000097"></webControls:ExRequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="ManadatoryField">
                            *
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblImportFile" runat="server" LabelID="1397" SkinID="Black11Arial"
                                FormatString="{0}:"></webControls:ExLabel>
                        </td>
                        <td>
                        </td>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <colgroup>
                                    <col width="90%" />
                                    <col width="10%" />
                                </colgroup>
                                <tr>
                                    <td>
                                        <telerikWebControls:ExRadUpload LabelID="2494" ID="radFileUpload" runat="server"
                                            ControlObjectsVisibility="None" InitialFileInputsCount="1" MaxFileInputsCount="1"  InputSize="50"
                                            Width="100%" />
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
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="5">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <webControls:ExButton ID="btnUpload" runat="server" OnClick="btnUpload_Click" LabelID="1478"
                                SkinID="ExButton100" CausesValidation="true" />
                            <webControls:ExButton ID="btnImportAll" runat="server" LabelID="2470" OnClick="btnImportAll_Click"
                                SkinID="Black11Arial" CausesValidation="true" Visible="false" />&nbsp;
                            <webControls:ExButton ID="btnPreview" runat="server" LabelID="1398" OnClick="btnPreview_Click"
                                SkinID="Black11Arial" CausesValidation="true" Visible="false" />&nbsp;
                            <webControls:ExButton ID="btnPageCancel" runat="server" LabelID="1239" OnClick="btnPageCancel_Click"
                                SkinID="Black11Arial" CausesValidation="false" OnClientClick="HideValidationSummary" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="5">
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="5">
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="5">
                            <input type="hidden" id="Hidden1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%" class="DataImportRulesTable">
                    <tr class="PanelTitleBrown">
                        <td>
                            <webControls:ExLabel ID="lblImportRules1" runat="server" SkinID="BlueBold11Arial"
                                LabelID="1479" />
                        </td>
                        <td align="right" style="margin-right: 2px">
                            <webControls:ExHyperLink ID="hlOpenExcelFile" runat="server" LabelID="1697" Class="BlueBold11Arial"></webControls:ExHyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlRules" runat="server" ScrollBars="Vertical" CssClass="RulesPanel">
                                <webControls:ExLabel ID="lblRules" runat="server"></webControls:ExLabel>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="BlankRow">
        </tr>
        <tr>
            <td colspan="4">
            
                <asp:Panel ID="pnlImportGrid" Visible="false" runat="server" Width="100%">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdnNewPageSize" runat="server" Value="10" />
                                <telerikWebControls:ExRadGrid ID="rgImportList" runat="server" EntityNameLabelID="1229"
                                    AllowPaging="true" AllowSorting="true" OnItemDataBound="rgImportList_ItemDataBound"
                                    OnNeedDataSource="rgImportList_NeedDataSource" OnItemCreated="rgImportList_ItemCreated"
                                    OnPageIndexChanged="rgImportList_PageIndexChanged" OnPageSizeChanged="rgImportList_PageSizeChanged"
                                    AllowMultiRowSelection="true" AllowExportToExcel="false" AllowExportToPDF="false"
                                    AllowPrint="true" AllowPrintAll="true" AllowCustomPaging="true" AllowCauseValidationExportToExcel="false"
                                    AllowCauseValidationExportToPDF="false">
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
                                            <telerikWebControls:ExGridTemplateColumn LabelID="2584">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblTaskListName" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="2545">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblTaskName" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1408">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblDescription" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <%--    <telerikWebControls:ExGridTemplateColumn LabelID="1449">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblStartDate" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>--%>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="2566">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblTaskDueDate" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="2564">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblTaskAsignedto" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1132">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblApprover" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="2567">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblAssigneeDueDate" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <%--<telerikWebControls:ExGridTemplateColumn LabelID="2568">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblApproverDueDate" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>--%>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1051" UniqueName="Error" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblError" runat="server" />
                                                </ItemTemplate>
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
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <hr />
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <webControls:ExLabel ID="lblGridHeading2" LabelID="1219" runat="server" SkinID="BlueBold11ArialUnderline"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel runat="server" ID="pnlServiceDataImport" Width="1000px" ScrollBars="Horizontal">
                    <telerikWebControls:ExRadGrid ID="rgDataImport" runat="server" OnItemDataBound="rgDataImport_ItemDataBound"
                        OnSortCommand="rgDataImport_SortCommand" OnNeedDataSource="rgDataImport_NeedDataSource"
                        AllowExportToExcel="true" AllowExportToPDF="true" AllowPrint="true" AllowPrintAll="true"
                        AllowRefresh="True" OnItemCommand="rgDataImport_ItemCommand" OnItemCreated="rgDataImport_ItemCreated"
                        AllowPaging="true" AllowSorting="true" EntityNameLabelID="1219" AllowMultiRowSelection="false">
                        <MasterTableView ClientDataKeyNames="DataImportStatusID, DataImportTypeID, DataImportID, IsRecordOwner">
                            <Columns>
                                <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="5%" UniqueName="imgStatus">
                                    <ItemTemplate>
                                        <webControls:ExImage ID="imgSuccess" runat="server" LabelID="1618" SkinID="SuccessIcon"
                                            Visible="false" />
                                        <webControls:ExImage ID="imgFailure" runat="server" LabelID="5000033" SkinID="ExpireIcon"
                                            Visible="false" />
                                        <webControls:ExImage ID="imgWarning" runat="server" LabelID="1617" SkinID="WarningIcon"
                                            Visible="false" />
                                        <webControls:ExImage ID="imgProcessing" runat="server" LabelID="1619" SkinID="ProgressIcon"
                                            Height="24px" Width="23px" Visible="false" />
                                        <webControls:ExImage ID="imgToBeProcessed" runat="server" LabelID="1783" SkinID="ToBeProcessedIcon"
                                            Visible="false" />
                                        <webControls:ExImage ID="imgReject" runat="server" LabelID="2400" SkinID="RejectIcon"
                                            Visible="false" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1477" SortExpression="DataImportType"
                                    HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <webControls:ExHyperLink ID="hlImportType" runat="server" SkinID="GridHyperLink" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1396" SortExpression="DataImportName"
                                    HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <webControls:ExHyperLink ID="hlProfileName" runat="server" SkinID="GridHyperLink" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2027" SortExpression="FileName">
                                    <ItemTemplate>
                                        <webControls:ExHyperLink ID="hlFileName" runat="server" SkinID="GridHyperLink" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2087" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <webControls:ExImageButton ID="imgFileType" runat="server" ImageAlign="Left" SkinID="FileDownloadIcon" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1399" HeaderStyle-Width="20%" SortExpression="DateAdded">
                                    <ItemTemplate>
                                        <webControls:ExHyperLink ID="hlDate" runat="server" SkinID="GridHyperLink" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1736" HeaderStyle-Width="15%" SortExpression="ForceCommitDate">
                                    <ItemTemplate>
                                        <webControls:ExHyperLink ID="hlForceCommitDetails" runat="server" SkinID="GridHyperLink" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1745" HeaderStyle-Width="10%" SortExpression="RecordsImported">
                                    <ItemTemplate>
                                        <webControls:ExHyperLink ID="hlRecordsAffected" runat="server" SkinID="GridHyperLink" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1338" HeaderStyle-Width="5%" SortExpression="DataImportStatus">
                                    <ItemTemplate>
                                        <webControls:ExHyperLink ID="hlStatus" runat="server" SkinID="GridHyperLink" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2180" HeaderStyle-Width="25%" SortExpression="DataImportStatus">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblAddedBy" runat="server" Width="100Px" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridBoundColumn UniqueName="DataImportID" DataField="DataImportID"
                                    HeaderStyle-Width="0" Visible="false">
                                </telerikWebControls:ExGridBoundColumn>
                                <telerikWebControls:ExGridBoundColumn UniqueName="IsHidden" DataField="IsHidden"
                                    HeaderStyle-Width="0" Visible="false">
                                </telerikWebControls:ExGridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                            <ClientEvents OnRowSelecting="Selecting" />
                        </ClientSettings>
                    </telerikWebControls:ExRadGrid>
                </asp:Panel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="4">
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
