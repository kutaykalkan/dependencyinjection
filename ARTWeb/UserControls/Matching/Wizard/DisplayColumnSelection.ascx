<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="SkyStem.ART.Web.UserControls.Matching.Wizard.DisplayColumnSelection" Codebehind="DisplayColumnSelection.ascx.cs" %>
<%@ Register Src="~/UserControls/Matching/Wizard/MatchingCombinationSelection.ascx"
    TagName="MatchingCombinationSelection" TagPrefix="uc1" %>
<%@ Register TagPrefix="usc" TagName="MatchSetInfo" Src="~/UserControls/Matching/MatchSetInfo.ascx" %>

<script type="text/javascript" language="javascript">
    function FillFromSource1Column() {

        var grid = $find("<%=rgDisplayColumnSelection.ClientID %>");
        var MasterTable = grid.get_masterTableView();
        for (var i = 0; i < MasterTable.get_dataItems().length; i++) {
            var lblSource1ColumnName = MasterTable.get_dataItems()[i].findElement("lblSource1ColumnName");
            var txtDisplayName = MasterTable.get_dataItems()[i].findElement("txtDisplayName");
            if (lblSource1ColumnName != null) {
                if ($('#' + lblSource1ColumnName.id).text() == '-') {
                    txtDisplayName.value = '';
                }
                else {
                    txtDisplayName.value = $('#' + lblSource1ColumnName.id).text();
                }
            }
        }

    }

    function FillFromSource2Column() {

        var grid = $find("<%=rgDisplayColumnSelection.ClientID %>");
        var MasterTable = grid.get_masterTableView();
        for (var i = 0; i < MasterTable.get_dataItems().length; i++) {
            var lblSource2ColumnName = MasterTable.get_dataItems()[i].findElement("lblSource2ColumnName");
            var txtDisplayName = MasterTable.get_dataItems()[i].findElement("txtDisplayName");
            if (lblSource2ColumnName != null) {
                if ($('#' + lblSource2ColumnName.id).text() == '-') {
                    txtDisplayName.value = '';
                }
                else {
                    txtDisplayName.value = $('#' + lblSource2ColumnName.id).text();

                }
            }
        }

    }

    function checkDisplayNameUnique() {

        var grid = $find('<%= rgDisplayColumnSelection.ClientID %>');
        var MasterTable = grid.get_masterTableView();

        for (var i = 0; i < MasterTable.get_dataItems().length; i++) {
            var DisplayName1 = MasterTable.get_dataItems()[i].findElement("txtDisplayName").value;
            DisplayName1 = $.trim(DisplayName1);
            if (DisplayName1 != "") {
                for (var j = i + 1; j < MasterTable.get_dataItems().length; j++) {
                    var DisplayName2 = MasterTable.get_dataItems()[j].findElement("txtDisplayName").value;
                    DisplayName2 = $.trim(DisplayName2);
                    if (DisplayName2 != "" && DisplayName1 == DisplayName2) {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    function validateUniqueDisplayName(source, args) {
        args.IsValid = checkDisplayNameUnique();
    }
  

</script>

<div id="divDisplayColumnSelection" runat="server">
    <usc:MatchSetInfo ID="uscMatchSetInfo" runat="server" />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                &nbsp;
                <uc1:MatchingCombinationSelection ID="ucMatchingCombinationSelection" runat="server" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlDisplayColumn" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr style="height: 10px;">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <webControls:ExRadioButton ID="rbFirstMatchingSource" SkinID="Black11Arial" LabelID="2282"
                        runat="server" GroupName="MatchingSource" onclick="javascript:FillFromSource1Column();" />
                </td>
                <td>
                    <webControls:ExRadioButton ID="rbSecondMatchingSource" SkinID="Black11Arial" LabelID="2283"
                        runat="server" GroupName="MatchingSource" onclick="javascript:FillFromSource2Column();" />
                </td>
            </tr>
            <tr class="BlankRow">
                <tr class="BlankRow">
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <webControls:ExCustomValidator ID="cvDisplayName" runat="server" ClientValidationFunction="validateUniqueDisplayName"
                        Text="" Display="None" LabelID="2289"></webControls:ExCustomValidator>
                    <telerikWebControls:ExRadGrid ID="rgDisplayColumnSelection" runat="server" Width="100%"
                        ShowHeader="true" OnItemDataBound="rgDisplayColumnSelection_ItemDataBound" AllowPrint="true"
                        AllowPrintAll="true" AllowSorting="true" SkinID="SkyStemBlueBrownWithoutBorder"
                        ClientSettings-Selecting-AllowRowSelect="true" AllowMultiRowSelection="true"
                        AutoGenerateColumns="false">
                        <MasterTableView Width="100%" ShowFooter="true" DataKeyNames="MatchingConfigurationID">
                            <Columns>
                                <%--<telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" DataTextField="IsDisplayedColumn"  Visible="true"
                                HeaderStyle-Width="5%" />--%>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="chkSelectColumn">
                                    <ItemTemplate>
                                        <webControls:ExCheckBox ID="chkSelectColumn" runat="server" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>                             
                                <telerikWebControls:ExGridTemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblMatchingConfigurationID" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2267">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblSource1ColumnName" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2268">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblSource2ColumnName" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2269" UniqueName="DisplayName">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDisplayName" runat="server"></asp:TextBox>
                                        <%--<webControls:ExTextBox ID="txtDisplayName" runat="server"  />--%>
                                        <webControls:ExRegularExpressionValidator ID="revDisplayName" runat="server" ControlToValidate="txtDisplayName"
                                            Text="!" ValidationExpression="^[a-zA-Z\s][a-zA-Z0-9#\s]{0,100}$" LabelID="5000294"></webControls:ExRegularExpressionValidator>
                                        <%--<webControls:ExLabel ID="lblDisplayColumnName" runat="server"></webControls:ExLabel>--%>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblDataTypeID" runat="server" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn Visible="false">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblMatchSetSubSetCombinationID" runat="server" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                        </ClientSettings>
                    </telerikWebControls:ExRadGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
