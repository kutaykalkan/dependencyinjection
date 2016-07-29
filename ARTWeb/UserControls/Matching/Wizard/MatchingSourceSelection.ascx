<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="SkyStem.ART.Web.UserControls.Matching.Wizard.MatchingSourceSelection" Codebehind="MatchingSourceSelection.ascx.cs" %>

<script type="text/javascript" language="javascript">
    function CheckOtherIsCheckedByGVID(spanChk) {
        var IsChecked = spanChk.checked;
        var CurrentRdbID = spanChk.id;
        //var Chk = spanChk;

        var Parent = document.getElementById('<%=this.rgGLTBSFiles.ClientID %>');
        var items = Parent.getElementsByTagName('input');
        for (i = 0; i < items.length; i++) {
            if (items[i].id != CurrentRdbID && items[i].type == "radio") {
                if (items[i].checked) {
                    items[i].checked = false;
                }
            }
        }
    }

    function validateRowSelectionForGLTBSFiles(source, args) {
        var grid = document.getElementById('<%= rgGLTBSFiles.ClientID %>');
        var items = grid.getElementsByTagName('input');
        var isGLTBSSelected = false;
        for (i = 0; i < items.length; i++) {
            if (items[i].type == "radio") {
                if (items[i].checked) {
                    isGLTBSSelected = true;
                    break;
                }
            }
        }
        args.IsValid = isGLTBSSelected;
    }

    function validateRowSelectionForNBFFiles(source, args) {
        var IsEditMode = '<%=this.IsEditMode%>';
        if (IsEditMode.toLowerCase() == 'true') {
            var grid = $find('<%= rgNBFFiles.ClientID %>');
            var selectedRowCount = grid.get_selectedItems().length;
            var hdnMinCount = $get('<%= hdnMinNBFFileCount.ClientID %>')
            var minCount = hdnMinCount.value;
            var maxCount = 3
            if (selectedRowCount < minCount || selectedRowCount > maxCount)
                args.IsValid = false;
            else
                args.IsValid = true;
        }
        else {
            args.IsValid = true;
        }
    }

    function IsMatchingSourceChanged(prevSelections, dispMsg) {
        if (!Page_IsValid)
            return false;
        //alert(prevSelections);
        var prevArr = prevSelections.split(",");
        var currArr = new Array();
        var itemIndex = 0;

        var gridGLTBS = $find('<%= rgGLTBSFiles.ClientID %>');
        if (gridGLTBS != null) {
            var gltbsItems = gridGLTBS.get_masterTableView().get_dataItems();
            for (var j = 0; j < gltbsItems.length; j++) {
                var row = gltbsItems[j];
                var radio = row.findElement("rbMatchingSource");
                if (radio.checked) {
                    var matchingSourceDataImportID = row.getDataKeyValue("MatchingSourceDataImportID");
                    currArr[itemIndex++] = matchingSourceDataImportID;
                }
            }
        }

        var gridNBF = $find('<%= rgNBFFiles.ClientID %>');
        if (gridNBF != null) {
            var selectedNBFItems = gridNBF.get_masterTableView().get_selectedItems();
            for (var j = 0; j < selectedNBFItems.length; j++) {
                var row = selectedNBFItems[j];
                var matchingSourceDataImportID = row.getDataKeyValue("MatchingSourceDataImportID");
                currArr[itemIndex++] = matchingSourceDataImportID;
            }
        }
        var isSame = IsArrayDataSame(prevArr, currArr);
        if (!isSame) {
            if (!confirm(dispMsg))
                return false;
        }
        return true;
    }

    // If Same return true otherwise false
    function IsArrayDataSame(arr1, arr2) {
        if (arr1 != null && arr2 != null) {
            if (arr1.lengh != arr2.lengh)
                return false;
            arr1.sort(sortValues);
            arr2.sort(sortValues);
            for (var i = 0; i < arr1.length; i++)
                if (arr1[i] != arr2[i])
                return false;
        }
        return true;
    }

    function sortValues(a, b) {
        return a - b;
    }
    var GrdView = null;
    function ddlPageSize_SelectedIndexChanged(ID, SourceID) {

        if ($find(SourceID) != null) {
            GrdView = $find(SourceID).get_masterTableView();

        }
        if (GrdView != null)
            GrdView.set_pageSize(document.getElementById(ID).value);

    }
    
</script>

<asp:Panel ID="pnlMatchingSource" runat="server">
    <div id="Description">
        <table width="100%">
            <tr>
                <td id="tdMatchSetRef" runat="server" visible="false" colspan="6">
                    <webControls:ExLabel ID="lblMatchSetRef" runat="server" LabelID="2276" SkinID="Black11Arial"
                        FormatString="{0} :"></webControls:ExLabel>
                    &nbsp;
                    <webControls:ExLabel ID="lblMatchSetRefNumber" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                </td>
            </tr>
            <tr style="vertical-align:top !important;">
                <td class="ManadatoryField">
                    *
                </td>
                <td>
                    <webControls:ExLabel style="vertical-align:top !important;" ID="lblName" SkinID="Black11Arial" LabelID="1287" runat="server">
                    </webControls:ExLabel>
                </td>
                <td>
                    <asp:TextBox style="vertical-align:top !important;" ID="txtName" SkinID="ExTextBox200" MaxLength="100" runat="server">
                    </asp:TextBox>
                    <webControls:ExRequiredFieldValidator ID="requiredFieldMatchSetName" runat="server"
                        ControlToValidate="txtName"></webControls:ExRequiredFieldValidator>
                </td>
                <td class="ManadatoryField">
                    *
                </td>
                <td>
                    <webControls:ExLabel style="vertical-align:top !important;" ID="lblDescription" SkinID="Black11Arial" LabelID="1408" runat="server">
                    </webControls:ExLabel>
                </td>
                <td>
                    <asp:TextBox style="vertical-align:top !important;" ID="txtDescription" SkinID="ExTextBox200" MaxLength="500" TextMode="MultiLine"
                        runat="server">
                    </asp:TextBox>
                    <webControls:ExRequiredFieldValidator ID="requiredFieldMatchSetDescription" runat="server"
                        ControlToValidate="txtDescription"></webControls:ExRequiredFieldValidator>
                </td>
            </tr>
            <tr class="BlankRow">
                <tr class="BlankRow">
        </table>
    </div>
    <div id="divGLTBSFiles" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <webControls:ExLabel ID="lblGLTBSFiles" SkinID="Black11Arial" runat="server" LabelID="2244"></webControls:ExLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <%--<telerikWebControls:ExRadGrid ID="rgGLTBSFiles" runat="server" Width="100%" ShowHeader="true"
                    OnItemDataBound="rgGLTBSFiles_ItemDataBound" AllowPrint="true" AllowPrintAll="true"
                    AllowSorting="true" SkinID="SkyStemBlueBrownWithoutBorder" AllowMultiRowSelection="true"
                    AutoGenerateColumns="false">--%>
                    <asp:HiddenField ID="hdnNewPageSize" runat="server" Value="10" />
                    <webControls:ExCustomValidator ID="cvGLTBSFiles" runat="server" ClientValidationFunction="validateRowSelectionForGLTBSFiles"
                        Text="" Display="None" LabelID="2291"></webControls:ExCustomValidator>
                    <telerikWebControls:ExRadGrid ID="rgGLTBSFiles" runat="server" OnItemDataBound="rgGLTBSFiles_ItemDataBound"
                        OnNeedDataSource="rgGLTBSFiles_NeedDataSource" OnSortCommand="rgGLTBSFiles_SortCommand"
                        AllowRefresh="false" OnItemCommand="rgGLTBSFiles_ItemCommand" OnItemCreated="rgGLTBSFiles_ItemCreated"
                        AllowPaging="true"  AllowSorting="true" AllowMultiRowSelection="true" PageSize="5"
                        AutoGenerateColumns="false" AllowCustomPaging="true" 
                        OnPageSizeChanged="rgGLTBSFiles_PageSizeChanged" >
                        <MasterTableView DataKeyNames="MatchingSourceDataImportID" 
                        ClientDataKeyNames="MatchingSourceDataImportID"
                            Width="100%" ShowFooter="true" >
                            <PagerTemplate>
                                <asp:Panel ID="PagerPanel" runat="server">
                                    <asp:Panel runat="server" ID="pnlPageSizeDDL">
                                        <div style="float: left; margin-right: 10px;">
                                            <%--<span style="margin-right: 3px;">Page size:</span>--%>
                                            <webControls:ExLabel ID="lblPageSize" runat="server" LabelID="2493"></webControls:ExLabel>
                                            <%-- <telerik:RadComboBox ID="ddlPageSize"   Width="150px"    runat="server"    OnClientSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                            </telerik:RadComboBox>--%>
                                            <asp:DropDownList ID="ddlPageSize" SkinID="DropDownList50" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="NumericPagerPlaceHolder" />
                                </asp:Panel>
                            </PagerTemplate>
                            <Columns>
                                <%--<telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" Visible="true"
                                HeaderStyle-Width="5%" />--%>
                                <telerikWebControls:ExGridTemplateColumn Visible="false" DataType="System.Int32"
                                    UniqueName="MatchingSourceDataImportID">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblMatchingSourceDataImportID" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="CheckboxSelectColumn" DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExRadioButton ID="rbMatchingSource" onclick="javascript:CheckOtherIsCheckedByGVID(this);"
                                            runat="server" />
                                        <%--<webControls:ExRadioButton ID="rbMatchingSource"  runat="server" />--%>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="MatchingSourceName" LabelID="2191"
                                    SortExpression="MatchingSourceName" DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblMatchingSourceName" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="MatchingSourceType" Visible="false"
                                    LabelID="1408" SortExpression="MatchingSourceType" DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblMatchingSourceType" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="FileName" LabelID="2027" SortExpression="FileName"
                                    DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblFileName" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2087" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <webControls:ExImageButton ID="imgDownloadFile" runat="server" ImageAlign="Left"
                                            SkinID="FileDownloadIcon" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="TotalRecords" LabelID="2218"
                                    SortExpression="RecordsImported" DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblTotalRecords" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="UsedRecords" LabelID="2219"
                                    SortExpression="RecItemCreatedCount" DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblUsedRecords" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="RecordsLeft" LabelID="2220"
                                    SortExpression="RecordsLeft" DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblRecordsLeft" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="AddedBy" LabelID="2180" SortExpression="AddedBy"
                                    DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblAddedBy" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="DateAdded" LabelID="1509" SortExpression="DateAdded"
                                    DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblDateAdded" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="ViewAccounts">
                                    <ItemTemplate>
                                        <webControls:ExHyperLink ID="hlShowAccounts" runat="server" SkinID="GridHyperLinkImageWithoutUnderlineReadOnlyMode"
                                            ToolTipLabelID="2223"></webControls:ExHyperLink>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                        </ClientSettings>
                        <FooterStyle HorizontalAlign="Right" />
                    </telerikWebControls:ExRadGrid>
                    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

                        <script type="text/javascript">
                            function changePage(argument) {
                                tableView.page(argument);
                            }
                            function RadNumericTextBox1_ValueChanged(sender, args) {
                                tableView.page(sender.get_value());
                            }
                        </script>

                    </telerik:RadScriptBlock>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div id="divNBFFiles" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <webControls:ExLabel ID="lblOtherFiles" SkinID="Black11Arial" runat="server" LabelID="2245"></webControls:ExLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hdnMinNBFFileCount" runat="server" />
                    <asp:HiddenField ID="hdnNBFNewPazeSize" runat="server" Value="10" />
                    <webControls:ExCustomValidator ID="cvRowSelection" runat="server" ClientValidationFunction="validateRowSelectionForNBFFiles"
                        Text="" Display="None" LabelID="2258"></webControls:ExCustomValidator>
                    <telerikWebControls:ExRadGrid ID="rgNBFFiles" runat="server" OnItemDataBound="rgNBFFiles_ItemDataBound"
                        OnNeedDataSource="rgNBFFiles_NeedDataSource" OnSortCommand="rgNBFFiles_SortCommand"
                        AllowRefresh="false" OnItemCommand="rgNBFFiles_ItemCommand" OnItemCreated="rgNBFFiles_ItemCreated"
                        AllowPaging="true" AllowSorting="true" AllowCustomPaging="true" OnPageSizeChanged="rgNBFFiles_PageSizeChanged"
                        AllowMultiRowSelection="true" ClientSettings-Selecting-AllowRowSelect="true"
                        AutoGenerateColumns="false">
                        <MasterTableView DataKeyNames="MatchingSourceDataImportID" ClientDataKeyNames="MatchingSourceDataImportID"
                            Width="100%" ShowFooter="true"  AllowPaging="true">
                            <PagerTemplate>
                                <asp:Panel ID="NBFPagerPanel" runat="server">
                                    <asp:Panel runat="server" ID="pnlNBFPageSizeDDL">
                                        <div style="float: left; margin-right: 10px;">
                                            <%--<span style="margin-right: 3px;">Page size:</span>--%>
                                            <webControls:ExLabel ID="lblPageSize" runat="server" LabelID="2493"></webControls:ExLabel>
                                            <%-- <telerik:RadComboBox ID="ddlPageSize"   Width="150px"    runat="server"    OnClientSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                            </telerik:RadComboBox>--%>
                                            <asp:DropDownList ID="ddlNBFPageSize" SkinID="DropDownList50" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="NBFNumericPagerPlaceHolder" />
                                </asp:Panel>
                            </PagerTemplate>
                            <Columns>
                                <%--<telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" Visible="true"
                                HeaderStyle-Width="5%" />--%>
                                <telerikWebControls:ExGridTemplateColumn Visible="false" DataType="System.Int32"
                                    UniqueName="MatchingSourceDataImportID">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblMatchingSourceDataImportID" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <%--    <telerikWebControls:ExGridTemplateColumn UniqueName="CheckboxSelectColumn">
                                <ItemTemplate>
                                 <webControls:ExCheckBox ID="chkMatchingSource" runat="server" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>--%>
                                <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" Visible="true"
                                    HeaderStyle-Width="5%" />
                                <telerikWebControls:ExGridTemplateColumn UniqueName="MatchingSourceName" LabelID="2191"
                                    SortExpression="MatchingSourceName" DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblMatchingSourceName" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="MatchingSourceType" Visible="false"
                                    LabelID="1408" SortExpression="MatchingSourceType" DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblMatchingSourceType" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="FileName" LabelID="2027" SortExpression="FileName"
                                    DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblFileName" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2087" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <webControls:ExImageButton ID="imgDownloadFile" runat="server" ImageAlign="Left"
                                            SkinID="FileDownloadIcon" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="TotalRecords" LabelID="2218"
                                    SortExpression="RecordsImported" DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblTotalRecords" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="UsedRecords" LabelID="2219"
                                    SortExpression="RecItemCreatedCount" DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblUsedRecords" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="RecordsLeft" LabelID="2220"
                                    SortExpression="RecordsLeft" DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblRecordsLeft" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="AddedBy" LabelID="2180" SortExpression="AddedBy"
                                    DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblAddedBy" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="DateAdded" LabelID="1509" SortExpression="DateAdded"
                                    DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblDateAdded" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                        </ClientSettings>
                        <FooterStyle HorizontalAlign="Right" />
                    </telerikWebControls:ExRadGrid>
                      <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">

                        <script type="text/javascript">
                            var tableNBFView = null;
                            var tableView = null;
                            function pageLoad(sender, args) {
                                tableNBFView = $find("<%= rgNBFFiles.ClientID %>").get_masterTableView();
                                if ($find("<%= rgGLTBSFiles.ClientID %>") != null) {
                                    tableView = $find("<%= rgGLTBSFiles.ClientID %>").get_masterTableView();
                                }
                            }
                            function ddlNBFPageSize_SelectedIndexChanged(ID) {
                                tableNBFView.set_pageSize(document.getElementById(ID).value);
                            }
                            function changePage(argument) {
                                tableNBFView.page(argument);
                            }
                            function RadNumericTextBox1_ValueChanged(sender, args) {
                                tableNBFView.page(sender.get_value());
                            }
                        </script>

                    </telerik:RadScriptBlock>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
