<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true"
    CodeFile="PopupRecFrequencySelection.aspx.cs" Inherits="Pages_PopupRecFrequencySelection"
    Theme="SkyStemBlueBrown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="5%">
        <col width="45%">
        <col width="60%">
        <tr id="trMessage" runat="server">
            <td colspan="3">
                <webControls:ExLabel ID="lblMsg" runat="server" LabelID="1983" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr id="trRecFrequencyDropdown" runat="server">
            <td width="5%">
                &nbsp;
                <asp:HiddenField ID="hdnRecPeriodIDs" runat="server" />
            </td>
            <td width="45%">
                <webControls:ExLabel ID="lblrecfrequency" runat="server" LabelID="1427" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td width="50%">
                <asp:DropDownList ID="ddlrecfrequencyName" runat="server" SkinID="DropDownList200"
                    OnSelectedIndexChanged="ddlrecfrequencyName_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="BlankRow">
            </td>
        </tr>
        <tr id="trRecFreqName" runat="server">
            <td>
                &nbsp;<span class="ManadatoryField" id="spnMandatoryField" runat="server">*</span>
            </td>
            <td>
                <webControls:ExLabel ID="lblReqFrequencyCaption" runat="server" LabelID="1822" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblRecFrequencyNameValue" runat="server" SkinID="ReadOnlyValue" Visible="false"></webControls:ExLabel>
                <webControls:ExTextBox ID="txtRecFrequencyNameValue" ErrorPhraseID="5000203" runat="server"
                    SkinID="ExTextBox200" IsRequired="true" MaxLength="100"></webControls:ExTextBox>
            </td>
        </tr>
        <tr class="BlankRow">
        </tr>
        <tr id="trFinancialYear" runat="server">
            <td width="5%">
                &nbsp;
            </td>
            <td width="28%">
                <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="2011" FormatString="{0}:"
                    SkinID="Black11Arial" />
            </td>
            <td width="22%">
                <asp:DropDownList runat="server" ID="ddlFinancialYear" SkinID="DropDownList200" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <%--<asp:Panel ID="pnlGrid" runat="server" Height="355px" ScrollBars="Vertical" >--%>
                <telerikWebControls:ExRadGrid ID="rgRecFrequency" runat="server" ShowHeader="false"
                    AllowInster="false" AllowExportToExcel="false" AllowExportToPDF="false" AllowPrint="false"
                    AllowPrintAll="false" EntityNameLabelID="1427" AutoGenerateColumns="false" OnItemDataBound="rgRecFrequency_ItemDataBound"
                    AllowMultiRowSelection="true" ClientSettings-Selecting-AllowRowSelect="true"
                    ClientSettings-Selecting-EnableDragToSelectRows="false" ClientSettings-ClientEvents-OnRowSelecting="Selecting">
                    <MasterTableView ClientDataKeyNames="ReconciliationPeriodID">
                        <Columns>
                            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn">
                            </telerikWebControls:ExGridClientSelectColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2011" HeaderStyle-Width="40%">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblFinancialYear" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridBoundColumn LabelID="1626" DataField="PeriodNumber" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="20%">
                            </telerikWebControls:ExGridBoundColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1057" ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-Width="40%" HeaderStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDate" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerikWebControls:ExRadGrid>
            </td>
        </tr>
        <tr>
            <td class="BlankRow">
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3">
                <webControls:ExButton ID="btnSave" runat="server" LabelID="1315" OnClick="btnSave_OnClick" />&nbsp;
                <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" CausesValidation="false"
                    OnClientClick="ClosePage();" />
            </td>
        </tr>
    </table>
    <input type="text" id="Sel" runat="server" style="display: none" />

    <script type="text/javascript" language="javascript">
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

        function Selecting(sender, args) {
            var i = 0;
            while (typeof (a[i]) != "undefined") {
                if (a[i++] == args.get_itemIndexHierarchical()) {
                    args.set_cancel(true);
                }
            }
        }

        function ClosePage() {
            GetRadWindow().Close();
        }
    </script>

</asp:Content>
