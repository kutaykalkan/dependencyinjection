<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="SkyStem.ART.Web.UserControls.Matching.Wizard.MatchingConfiguration" Codebehind="MatchingConfiguration.ascx.cs" %>
<%@ Register TagPrefix="UserControl" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register TagPrefix="usc" TagName="MatchingCombinationSelection" Src="~/UserControls/Matching/Wizard/MatchingCombinationSelection.ascx" %>
<%@ Register TagPrefix="usc" TagName="RuleDisplayControl" Src="~/UserControls/Matching/RuleDisplayControl.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Register TagPrefix="usc" TagName="MatchSetInfo" Src="~/UserControls/Matching/MatchSetInfo.ascx" %>
<asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <usc:MatchSetInfo ID="uscMatchSetInfo" runat="server" />
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr class="BlankRow">
                <td></td>
            </tr>
            <tr id="trDataSourceName" runat="server">
                <td valign="middle">
                    <usc:MatchingCombinationSelection ID="ddlMatchingCombinationSelection" runat="server" />
                </td>
            </tr>
            <tr class="BlankRow">
                <td></td>
            </tr>
            <asp:Panel ID="GridPnl" runat="server">
                <tr>
                    <webControls:ExCustomValidator ID="cvUniqueSource2Column" runat="server" ClientValidationFunction="validateUniqueSource2Column"
                        Text="" Display="None" LabelID="2349"></webControls:ExCustomValidator>
                    <webControls:ExCustomValidator ID="cvPartialKeys" runat="server" ClientValidationFunction="validatePartialKeys"
                        Text="" Display="None" LabelID="2343"></webControls:ExCustomValidator>
                    <webControls:ExCustomValidator ID="ExCustomValidator1" runat="server" ClientValidationFunction="validateKeyCount"
                        Text="" Display="None" LabelID="2342"></webControls:ExCustomValidator>
                    <td colspan="3">
                        <telerikWebControls:ExRadGrid ID="rgMappingColumns" runat="server" OnItemDataBound="rgMappingColumns_ItemDataBound"
                            OnNeedDataSource="rgMappingColumns_NeedDataSource" Width="770px">
                            <MasterTableView DataKeyNames="MatchingSource1ColumnID">
                                <Columns>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2261" HeaderStyle-Width="150px"
                                        HeaderStyle-HorizontalAlign="Center" UniqueName="Source1ColumnName">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblSource1ColumnName" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="5px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            =
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2262" HeaderStyle-HorizontalAlign="Center"
                                        HeaderStyle-Font-Bold="true" HeaderStyle-Width="160px" DataType="System.String"
                                        UniqueName="Source2Column">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlSource2Column" runat="server" SkinID="DropDownList150">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="MatchKey" LabelID="2263" HeaderStyle-Width="50px">
                                        <ItemTemplate>
                                            <webControls:ExCheckBox ID="chkMatchKey" runat="server"></webControls:ExCheckBox>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="PartialMatchKey" LabelID="2264"
                                        HeaderStyle-Width="75px">
                                        <ItemTemplate>
                                            <webControls:ExCheckBox ID="chkPartialMatchKey" runat="server"></webControls:ExCheckBox>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="IsAmountColumn" LabelID="2536"
                                        HeaderStyle-Width="75px">
                                        <ItemTemplate>
                                            <webControls:ExCheckBox ID="chkAmountColumn" runat="server" onclick="IsAmountClick(this,event)"></webControls:ExCheckBox>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="MatchingRulesIcon" HeaderStyle-Width="10px">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="lnkAddRule" runat="server" SkinID="AddNewRule" ToolTipLabelID="2288"></webControls:ExHyperLink>
                                            <%--<webControls:ExHyperLink ID="lnkAddRuleDisbled" runat="server" Enabled="false" SkinID="AddNewRuleDisbled"
                                                Visible="false" ToolTipLabelID="2288"></webControls:ExHyperLink>--%>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="MatchingRules" LabelID="2265"
                                        HeaderStyle-Width="250px">
                                        <ItemTemplate>
                                            <usc:RuleDisplayControl ID="RuleDisplayControl" runat="server" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings>
                                <Scrolling AllowScroll="true" />
                                <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                            </ClientSettings>
                        </telerikWebControls:ExRadGrid>
                    </td>
                </tr>
                <asp:HiddenField ID="hdnSource2" runat="server" Value="" />
            </asp:Panel>
            <tr class="BlankRow">
                <td></td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>
                    <UserControl:ProgressBar ID="ucProgressBar" runat="server" AssociatedUpdatePanelID="upnlMain" />
                </td>
            </tr>
        </table>

        <script language="javascript" type="text/javascript">

            function validateUniqueSource2Column(source, args) {
                args.IsValid = checkUniqueSource2Column();
            }
            function checkUniqueSource2Column() {
                var grid = $find('<%= rgMappingColumns.ClientID %>');
                var MasterTable = grid.get_masterTableView();

                for (var i = 0; i < MasterTable.get_dataItems().length; i++) {
                    var ddlSource2Column1 = MasterTable.get_dataItems()[i].findElement("ddlSource2Column").value;
                    if (ddlSource2Column1 != "-2") {
                        for (var j = i + 1; j < MasterTable.get_dataItems().length; j++) {
                            var ddlSource2Column2 = MasterTable.get_dataItems()[j].findElement("ddlSource2Column").value;
                            if (ddlSource2Column2 != "-2" && ddlSource2Column1 == ddlSource2Column2) {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }


            function validatePartialKeys(source, args) {
                args.IsValid = checkForPartialKeys();
            }

            function checkForPartialKeys() {
                var grid = $find('<%= rgMappingColumns.ClientID %>');
                var MasterTable = grid.get_masterTableView();
                var matchKeyCount = 0;
                var partialMatchKeyCount = 0;
                for (var i = 0; i < MasterTable.get_dataItems().length; i++) {
                    var matchKeyColumn = MasterTable.get_dataItems()[i].findElement("chkMatchKey");
                    var partalMatchKeyColumn = MasterTable.get_dataItems()[i].findElement('chkPartialMatchKey');
                    if (matchKeyColumn != null && matchKeyColumn.checked) {
                        matchKeyCount = matchKeyCount + 1;
                    }
                    if (partalMatchKeyColumn != null && partalMatchKeyColumn.checked) {
                        partialMatchKeyCount = partialMatchKeyCount + 1;
                    }
                }

                if ((matchKeyCount != 0 && partialMatchKeyCount != 0) && (partialMatchKeyCount >= matchKeyCount)) {
                    return false;
                }
                return true;
            }

            function validateKeyCount(source, args) {
                // args.IsValid = checkForKeyCount();
            }

            function checkForKeyCount() {
                var grid = $find('<%= rgMappingColumns.ClientID %>');
                var MasterTable = grid.get_masterTableView();
                var matchKeyCount = 0;
                var partialMatchKeyCount = 0;
                var ddlSource2ColumnSelectedCount = 0;

                for (var i = 0; i < MasterTable.get_dataItems().length; i++) {
                    var matchKeyColumn = MasterTable.get_dataItems()[i].findElement("chkMatchKey");
                    var partalMatchKeyColumn = MasterTable.get_dataItems()[i].findElement('chkPartialMatchKey');
                    var ddlSource2Column1 = MasterTable.get_dataItems()[i].findElement("ddlSource2Column");
                    if (matchKeyColumn != null && matchKeyColumn.checked) {
                        matchKeyCount = matchKeyCount + 1;
                    }
                    if (partalMatchKeyColumn != null && partalMatchKeyColumn.checked) {
                        partialMatchKeyCount = partialMatchKeyCount + 1;
                    }
                }

                if ((matchKeyCount != 0) && (matchKeyCount < 3 || partialMatchKeyCount < 2)) {
                    return false;
                }
                return true;
            }

            function LoadRuleSetupPopup(ID, ddlSource2, matchingSourceID) {

                if (typeof (Page_ClientValidate) == 'function') {
                    Page_ClientValidate();
                }
                if (Page_IsValid) {

                    var ddlSource2Column = document.getElementById(ddlSource2);
                    if (ddlSource2Column.options[ddlSource2Column.selectedIndex].value > 0) {
                        var strCol2 = ddlSource2Column.options[ddlSource2Column.selectedIndex].text + "_" + ddlSource2Column.options[ddlSource2Column.selectedIndex].value;
                        var urlRuleSetupPopup = "RuleSetupPopup.aspx?mode=add&ID=" + encodeURIComponent(ID) + "&Col2=" + encodeURIComponent(strCol2) + "&msID=" + encodeURIComponent(matchingSourceID);
                        OpenRadWindowForHyperlink(urlRuleSetupPopup, 375, 500);
                    }
                    else {
                        alert('<% = Helper.GetAlertMessageFromLabelID(5000268) %>');
                    }
                }
            }
        </script>

    </ContentTemplate>
</asp:UpdatePanel>

<script language="javascript" type="text/javascript">


    function ddlSource2Change(ddlSource2, chkMatchKey, chkPMatchKey, chkAmountColumn) {

        var chkMatchKey = document.getElementById(chkMatchKey);
        var chkPMatchKey = document.getElementById(chkPMatchKey);
        var chkAmtColumn = document.getElementById(chkAmountColumn);

        if (ddlSource2.options[ddlSource2.selectedIndex].value == -2) {
            chkMatchKey.checked = false;
            chkMatchKey.disabled = true;
            chkPMatchKey.checked = false;
            chkPMatchKey.disabled = true;
            if (chkAmtColumn != null) {
                chkAmtColumn.checked = false;
                chkAmtColumn.disabled = true;
            }

        }
        else {
            chkMatchKey.disabled = false;
            chkMatchKey.parentNode.disabled = false;
        }

        return true;
    }


    function MatchKeyCheckedChange(chk, ddlSource2, chkPMatchKey, chkAmountColumn) {
        var ddlSource2Column = document.getElementById(ddlSource2);
        var chkPMatchKey = document.getElementById(chkPMatchKey);
        var chkAmountColumn = document.getElementById(chkAmountColumn);

        if (chk.checked == false) {
            chkPMatchKey.checked = false;
            chkPMatchKey.disabled = true;
            chkAmountColumn.checked = false;
            chkAmountColumn.disabled = true;
        }
        else {
            chkPMatchKey.disabled = false;
            chkPMatchKey.parentNode.disabled = false;
            chkAmountColumn.disabled = false;
            chkAmountColumn.parentNode.disabled = false;
        }
    }
    function IsAmountClick(sender, e) {

        var grid = $find('<%= rgMappingColumns.ClientID %>');
        var MasterTable = grid.get_masterTableView();
        for (var i = 0; i < MasterTable.get_dataItems().length; i++) {
            var chkAmountColumn = MasterTable.get_dataItems()[i].findElement("chkAmountColumn");
            if (chkAmountColumn != null) {
                if (chkAmountColumn == sender)
                    continue;
                chkAmountColumn.checked = false;
            }
        }
    }

</script>

