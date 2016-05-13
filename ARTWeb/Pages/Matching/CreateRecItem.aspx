<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateRecItem.aspx.cs" Inherits="Pages_Matching_CreateRecItem"
    MasterPageFile="~/MasterPages/MatchingMaster.master" Theme="SkyStemBlueBrown" %>

<%@ Register TagPrefix="UserControls" TagName="AccountDescription" Src="~/UserControls/AccountDescription.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMatching" runat="Server">
    <asp:UpdatePanel ID="pnlCreateRecItem" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr class="BlankRow">
                    <td style="width: 8%; padding: 5px">
                        <webControls:ExLabel ID="lblDataSource" runat="server" LabelID="2231" FormatString="{0} :"
                            SkinID="Black11Arial"></webControls:ExLabel>
                    </td>
                    <td style="width: 92%;">
                        <webControls:ExLabel ID="lblDataSourceName" runat="server" SkinID="Black9ArialNormal"></webControls:ExLabel>
                        <webControls:ExCustomValidator runat="server" OnServerValidate="cvValidateNormalRecItems_ServerValidate"
                            ID="cvValidateNormalRecItems"></webControls:ExCustomValidator>
                        <webControls:ExCustomValidator runat="server" OnServerValidate="cvValidateScheduleRecItems_ServerValidate"
                            ID="cvValidateScheduleRecItems"></webControls:ExCustomValidator>
                        <webControls:ExCustomValidator runat="server" OnServerValidate="cvValidateWriteOffOnRecItems_ServerValidate"
                            ID="cvValidateWriteOffOnRecItems"></webControls:ExCustomValidator>
                        <asp:HiddenField ID="hdnRecTemplateID" runat="server" Value="0" />
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td style="width: 15%; padding: 5px">
                        <webControls:ExLabel ID="lblRecCategory" runat="server" LabelID="2329" FormatString="{0} :"
                            SkinID="Black11Arial"></webControls:ExLabel>
                    </td>
                    <td style="width: 85%;">
                        <asp:DropDownList ID="ddlRecCategory" runat="server" SkinID="DropDownList200" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlRecCategory_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td style="width: 15%; padding: 5px">
                        <webControls:ExLabel ID="lblRecCategoryType" runat="server" LabelID="2330" FormatString="{0} :"
                            SkinID="Black11Arial"></webControls:ExLabel>
                    </td>
                    <td style="width: 85%;">
                        <asp:DropDownList ID="ddlRecCategoryType" SkinID="DropDownList200" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlRecCategoryType_SelectedIndexChanged" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding: 5px">
                        <webControls:ExLabel ID="ExLabel2" runat="server" LabelID="2328" FormatString="{0} :"
                            SkinID="Black11Arial"></webControls:ExLabel>
                    </td>
                </tr>
                <asp:Panel ID="pnlNormalitems" Visible="false" runat="server">
                    <tr class="BlankRow">
                        <td colspan="2" style="padding: 5px">
                            <telerikWebControls:ExRadGrid ID="rgCreateRecItem" runat="server" EntityNameLabelID="1229"
                                AllowPaging="true" AllowSorting="true" OnItemDataBound="rgCreateRecItem_ItemDataBound"
                                OnNeedDataSource="rgCreateRecItem_NeedDataSource" AllowMultiRowSelection="true"
                                AllowExportToExcel="false" AllowExportToPDF="false" AllowCauseValidationExportToExcel="false"
                                AllowCauseValidationExportToPDF="false" OnPageIndexChanged="rgCreateRecItem_PageIndexChanged">
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True"></Selecting>
                                    <ClientEvents OnRowSelecting="Selecting" />
                                </ClientSettings>
                                <MasterTableView DataKeyNames="MatchSetMatchingSourceDataImportID,ExcelRowNumber,RecordSourceID">
                                    <Columns>
                                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2118">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblRecItemNo" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1399">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblErrorSign" Text="!" Visible="false" CssClass="RedStar"
                                                    runat="server" />
                                                <webControls:ExCalendar ID="calDate" runat="server" SkinID="ExCalendar100" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1408">
                                            <ItemTemplate>
                                                <webControls:ExTextBox ID="txtComments" runat="server" SkinID="ExMulitilineTextBox150" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn ItemStyle-HorizontalAlign="Center" LabelID="1773"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlLocalCurrency" runat="server" CausesValidation="false">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1675" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAmountLCCY" SkinID="TextBox70" runat="server" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2191">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblDataSourceName" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1051" UniqueName="Error" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblError" Visible="false" SkinID="ErrorLabel" runat="server" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="BaseCurrencyExchangeRate" Visible="false">
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="ReportingCurrencyExchangeRate"
                                            Visible="false">
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="MatchSetMatchingSourceDataImportID"
                                            Visible="false">
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="ExcelRowNumber" Visible="false">
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="RecordSourceID" Visible="false">
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="IndexID" Visible="false">
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerikWebControls:ExRadGrid>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlScheduleItems" Visible="false" runat="server">
                    <tr class="BlankRow">
                        <td colspan="2" style="padding: 5px">
                            <asp:Panel ID="Panel1" ScrollBars="Auto" Width="990PX" runat="server">
                                <telerikWebControls:ExRadGrid ID="rgCreateScheduleRecItem" runat="server" EntityNameLabelID="1229"
                                    AllowPaging="true" AllowSorting="true" OnItemDataBound="rgCreateScheduleRecItem_ItemDataBound"
                                    OnNeedDataSource="rgCreateScheduleRecItem_NeedDataSource" AllowMultiRowSelection="true"
                                    AllowExportToExcel="false" AllowExportToPDF="false" AllowCauseValidationExportToExcel="false"
                                    AllowCauseValidationExportToPDF="false" Width="1300px" OnPageIndexChanged="rgCreateScheduleRecItem_PageIndexChanged">
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True"></Selecting>
                                        <ClientEvents OnRowSelecting="Selecting" />
                                    </ClientSettings>
                                    <MasterTableView DataKeyNames="MatchSetMatchingSourceDataImportID,ExcelRowNumber,RecordSourceID">
                                        <Columns>
                                            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                            <telerikWebControls:ExGridTemplateColumn LabelID="2118">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblRecItemNo" runat="server"></webControls:ExLabel>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1399">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblErrorSign" Text="!" Visible="false" CssClass="RedStar"
                                                        runat="server" />
                                                    <webControls:ExCalendar ID="calDate" runat="server" SkinID="ExCalendar80" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="2063">
                                                <ItemTemplate>
                                                    <webControls:ExCalendar ID="calScheduleBeginDate" runat="server" SkinID="ExCalendar80" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1792">
                                                <ItemTemplate>
                                                    <webControls:ExCalendar ID="calScheduleEndDate" runat="server" SkinID="ExCalendar80" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="2052">
                                                <ItemTemplate>
                                                    <webControls:ExTextBox ID="txtScheduleName" runat="server" SkinID="ExTextBox100" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1408">
                                                <ItemTemplate>
                                                    <webControls:ExTextBox ID="txtComments" runat="server" SkinID="ExMulitilineTextBox150" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn ItemStyle-HorizontalAlign="Center" LabelID="1773"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlLocalCurrency" runat="server" CausesValidation="false">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1675" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAmountLCCY" SkinID="TextBox70" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="2191">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblDataSourceName" runat="server"></webControls:ExLabel>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1051" UniqueName="Error" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblError" Visible="false" SkinID="ErrorLabel" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="BaseCurrencyExchangeRate" Visible="false">
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ReportingCurrencyExchangeRate"
                                                Visible="false">
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="MatchSetMatchingSourceDataImportID"
                                                Visible="false">
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ExcelRowNumber" Visible="false">
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="RecordSourceID" Visible="false">
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="IndexID" Visible="false">
                                            </telerikWebControls:ExGridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerikWebControls:ExRadGrid>
                            </asp:Panel>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlWriteOffOnItems" Visible="false" runat="server">
                    <tr class="BlankRow">
                        <td colspan="2" style="padding: 5px">
                            <asp:Panel ID="Panel2" ScrollBars="Auto" Width="990PX" runat="server">
                                <telerikWebControls:ExRadGrid ID="rgCreateWriteOffOnRecItem" runat="server" EntityNameLabelID="1229"
                                    AllowPaging="true" AllowSorting="true" OnItemDataBound="rgCreateWriteOffOnRecItem_ItemDataBound"
                                    OnNeedDataSource="rgCreateWriteOffOnRecItem_NeedDataSource" AllowMultiRowSelection="true"
                                    AllowExportToExcel="false" AllowExportToPDF="false" AllowCauseValidationExportToExcel="false"
                                    AllowCauseValidationExportToPDF="false" Width="1200px" OnPageIndexChanged="rgCreateWriteOffOnRecItem_PageIndexChanged">
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True"></Selecting>
                                        <ClientEvents OnRowSelecting="Selecting" />
                                    </ClientSettings>
                                    <MasterTableView DataKeyNames="MatchSetMatchingSourceDataImportID,ExcelRowNumber,RecordSourceID">
                                        <Columns>
                                            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                            <telerikWebControls:ExGridTemplateColumn LabelID="2118">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblRecItemNo" runat="server"></webControls:ExLabel>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1399">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblErrorSign" Text="!" Visible="false" CssClass="RedStar"
                                                        runat="server" />
                                                    <webControls:ExCalendar ID="calDate" runat="server" SkinID="ExCalendar100" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1408">
                                                <ItemTemplate>
                                                    <webControls:ExTextBox ID="txtComments" runat="server" SkinID="ExMulitilineTextBox150" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn ItemStyle-HorizontalAlign="Center" LabelID="1773"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlLocalCurrency" runat="server" CausesValidation="false">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1675" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAmountLCCY" SkinID="TextBox70" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1425 " ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <webControls:ExRadioButton ID="optWriteOff" runat="server" LabelID="1676" GroupName="opt1"
                                                        SkinID="optblack11arial"></webControls:ExRadioButton>
                                                    <webControls:ExRadioButton ID="optWriteOn" runat="server" LabelID="1677" GroupName="opt1"
                                                        SkinID="optblack11arial"></webControls:ExRadioButton>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="2191">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblDataSourceName" runat="server"></webControls:ExLabel>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1051" UniqueName="Error" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblError" Visible="false" SkinID="ErrorLabel" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="BaseCurrencyExchangeRate" Visible="false">
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ReportingCurrencyExchangeRate"
                                                Visible="false">
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="MatchSetMatchingSourceDataImportID"
                                                Visible="false">
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ExcelRowNumber" Visible="false">
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="RecordSourceID" Visible="false">
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="IndexID" Visible="false">
                                            </telerikWebControls:ExGridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerikWebControls:ExRadGrid>
                            </asp:Panel>
                        </td>
                    </tr>
                </asp:Panel>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <span style="padding: 25px">
                          <webControls:ExButton ID="btnFlipSign" CausesValidation="false" Enabled="false" runat="server" LabelID="2591"
                                SkinID="ExButton100" OnClick="btnFlipSign_Click" />
                            
                            <webControls:ExButton ID="btnSave" Enabled="false" runat="server" LabelID="1315"
                                SkinID="ExButton100" OnClick="btnSave_Click" />
                            <webControls:ExButton ID="btnCreateRecItem" Enabled="false" runat="server" LabelID="2327"
                                SkinID="ExButton150" OnClick="btnCreateRecItem_Click" />
                        </span><span style="padding: 5px">
                            <hr />
                        </span><span style="padding: 25px">
                            <webControls:ExButton ID="btnBackToWorkspace" runat="server" LabelID="2331" SkinID="ExButton150"
                                OnClick="btnBackToWorkspace_Click" />
                        </span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <input type="hidden" id="Sel" runat="server" />
                        <asp:HiddenField ID="hdnIsBindNormalItemGrid" Value="0" runat="server" />
                        <asp:HiddenField ID="hdnIsBindScheduleItemGrid" Value="0" runat="server" />
                        <asp:HiddenField ID="hdnIsBindWriteOffOnGrid" Value="0" runat="server" />
                         <asp:HiddenField ID="hdnFlipedValue" Value="1" runat="server" />
                        <UserControls:ProgressBar ID="ucProgressBar" runat="server" EnableTheming="true"
                            AssociatedUpdatePanelID="pnlCreateRecItem" Visible="true" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">
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
     

    </script>

</asp:Content>
