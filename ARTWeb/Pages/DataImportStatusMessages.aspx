<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Pages_DataImportStatusMessages" MasterPageFile="~/MasterPages/ARTMasterPage.master"
    Theme="SkyStemBlueBrown" Codebehind="DataImportStatusMessages.aspx.cs" %>

<%@ Register TagPrefix="cc1" Namespace="SkyStem.Library.Controls.TelerikWebControls" Assembly="TelerikWebControls" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Panel ID="pnlErrorUpload" runat="server">
                    <table width="95%" class="DataImportStatusMessage" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="DataImportStatusMessageTitle" style="width: 6%">
                                <webControls:ExImage ID="imgFailure" runat="server" SkinID="ExpireIcon" Visible="false" />
                                <webControls:ExImage ID="imgSuccess" runat="server" SkinID="SuccessIcon" Visible="false" />
                                <webControls:ExImage ID="imgWarning" runat="server" SkinID="WarningIcon" Visible="false" />
                                <webControls:ExImage ID="imgProcessing" runat="server" SkinID="ProgressIcon" Height="24px"
                                    Width="23px" Visible="false" />
                                <webControls:ExImage ID="imgToBeProcessed" runat="server" SkinID="ToBeProcessedIcon"
                                    Visible="false" />
                                <webControls:ExImage ID="imgReject" runat="server" LabelID="2400" SkinID="RejectIcon"
                                    Visible="false" />
                            </td>
                            <td class="DataImportStatusMessageTitle">
                                <webControls:ExLabel ID="lblStatusHeading" runat="server" SkinID="BlueBold11Arial"></webControls:ExLabel>
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <webControls:ExLabel ID="ExLabel2" runat="server" SkinID="Black11Arial" LabelID="1523"
                                    FormatString="{0}:"></webControls:ExLabel>
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-left: 20px;">
                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td style="width: 30%">
                                            <webControls:ExLabel ID="lblDataImportType" runat="server" SkinID="Black11Arial"
                                                LabelID="1307" FormatString="{0}:"></webControls:ExLabel>
                                        </td>
                                        <td>
                                            <webControls:ExLabel ID="lblDataImportTypeValue" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                        </td>
                                        <td colspan="2" />
                                    </tr>
                                    <tr class="BlankRow">
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <webControls:ExLabel ID="lblProfileName" runat="server" SkinID="Black11Arial" LabelID="1524"
                                                FormatString="{0}:"></webControls:ExLabel>
                                        </td>
                                        <td>
                                            <webControls:ExLabel ID="lblProfileNameValue" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                        </td>
                                        <td>
                                            <webControls:ExLabel ID="lblLoadDate" runat="server" SkinID="Black11Arial" FormatString="{0}:"
                                                LabelID="1528"></webControls:ExLabel>
                                        </td>
                                        <td>
                                            <webControls:ExLabel ID="lblLoadDateValue" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                        </td>
                                    </tr>
                                    <tr class="BlankRow">
                                        <td></td>
                                    </tr>
                                    <asp:Panel ID="pnlSuccess" runat="Server">
                                        <tr>
                                            <td>
                                                <webControls:ExLabel ID="lblNoOfRecords" runat="server" SkinID="Black11Arial" FormatString="{0}:"
                                                    LabelID="1745"></webControls:ExLabel>
                                            </td>
                                            <td>
                                                <webControls:ExLabel ID="lblNoOfRecordsValue" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                            </td>
                                            <td>
                                                <webControls:ExLabel ID="lblForceCommitDate" runat="server" SkinID="Black11Arial"
                                                    FormatString="{0}:" LabelID="1736"></webControls:ExLabel>
                                            </td>
                                            <td>
                                                <webControls:ExLabel ID="lblForceCommitDateValue" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                            <td></td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td colspan="4">
                                            <webControls:ExLabel ID="lblMessage" runat="server" SkinID="Black11Arial" FormatString="{0}:"></webControls:ExLabel>
                                        </td>
                                    </tr>
                                    <tr class="BlankRow">
                                        <td></td>
                                    </tr>
                                    <asp:Panel ID="pnlFailureMessages" runat="server" Visible="false">
                                        <tr>
                                            <td colspan="4">
                                                <asp:Panel runat="server" ScrollBars="Both" Height="200px" CssClass="DataImportErrorPanel">
                                                    <webControls:ExLabel ID="lblFailureMessages" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                            <td></td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlDataImportAccountMessages" runat="server" Visible="false">
                                        <tr>
                                            <td colspan="4">
                                                <webControls:ExLabel ID="lblAccountErrors" runat="server" SkinID="Black11Arial" LabelID="2888"
                                                    FormatString="{0}:"></webControls:ExLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <telerikWebControls:ExRadGrid ID="rgDataImportAccountMessages" runat="server"
                                                    OnItemDataBound="rgDataImportAccountMessages_ItemDataBound"
                                                    OnNeedDataSource="rgDataImportAccountMessages_NeedDataSource" EntityNameLabelID="1621"
                                                    OnDetailTableDataBind="rgDataImportAccountMessages_DetailTableDataBind"
                                                    OnItemCreated="rgDataImportAccountMessages_ItemCreated"
                                                    OnItemCommand="rgDataImportAccountMessages_ItemCommand"
                                                    AllowCustomization="False" AllowSorting="false"
                                                    AllowPaging="False" AllowCustomPaging="False" AllowExportToExcel="True"
                                                    HierarchyLoadMode="Client" AllowExportToPDF="True" CustomPaging="true"
                                                    MasterTableView-GroupLoadMode="Client"
                                                    AllowCustomFilter="False" AllowRefresh="False" ClientSettings-Scrolling-AllowScroll="true">
                                                    <MasterTableView AllowSorting="false" AllowPaging="true"
                                                        HierarchyLoadMode="Client"
                                                        DataKeyNames="DataImportMessageDetailID">
                                                        <DetailTables>
                                                            <telerik:GridTableView Name="rgAccountMessageDetails" EnableColumnsViewState="false"
                                                                AllowSorting="false" AllowPaging="false" runat="server" />
                                                        </DetailTables>
                                                        <GroupHeaderTemplate>
                                                            <webControls:ExImage ID="imgSuccess" runat="server" LabelID="1618" SkinID="SuccessIcon"
                                                                Visible="false" />
                                                            <webControls:ExImage ID="imgFailure" runat="server" LabelID="5000033" SkinID="ExpireIcon"
                                                                Visible="false" />
                                                            <webControls:ExImage ID="imgWarning" runat="server" LabelID="1617" SkinID="WarningIcon"
                                                                Visible="false" />
                                                            <asp:Label runat="server" ID="lblDataImportMessage"></asp:Label>
                                                        </GroupHeaderTemplate>
                                                        <GroupByExpressions>
                                                            <telerik:GridGroupByExpression>
                                                                <GroupByFields>
                                                                    <telerik:GridGroupByField FieldName="DataImportMessageLabelID" />
                                                                </GroupByFields>
                                                                <SelectFields>
                                                                    <telerik:GridGroupByField FieldName="DataImportMessage" Aggregate="First" />
                                                                    <telerik:GridGroupByField FieldName="DataImportMessageTypeID" Aggregate="First" />
                                                                </SelectFields>
                                                            </telerik:GridGroupByExpression>
                                                        </GroupByExpressions>
                                                        <Columns>
                                                            <telerikWebControls:ExGridTemplateColumn LabelID="2863" SortExpression="ExcelRowNumber"
                                                                HeaderStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExcelRowNumber" runat="server" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn LabelID="1337" SortExpression="FSCaption"
                                                                UniqueName="FSCaption" Visible="true">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblFSCaption" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="15%" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn LabelID="1363" SortExpression="AccountType"
                                                                UniqueName="AccountType" DataType="System.String" Visible="true">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblAccountType" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key2" UniqueName="Key2">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblKey2" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key3" UniqueName="Key3">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblKey3" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key4" UniqueName="Key4">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblKey4" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key5" UniqueName="Key5">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblKey5" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key6" UniqueName="Key6">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblKey6" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key7" UniqueName="Key7">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblKey7" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key8" UniqueName="Key8">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblKey8" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key9" UniqueName="Key9">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblKey9" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn LabelID="1357" HeaderStyle-Width="20%" SortExpression="AccountNumber"
                                                                DataType="System.String">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblAccountNumber" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn LabelID="1346" HeaderStyle-Width="15%" SortExpression="AccountName"
                                                                DataType="System.String">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblAccountName" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="20%" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerikWebControls:ExRadGrid>
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                            <td></td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlDataImportMessages" runat="server" Visible="false">
                                        <tr>
                                            <td colspan="4">
                                                <webControls:ExLabel ID="lblOtherErrors" runat="server" SkinID="Black11Arial" LabelID="2889"
                                                    FormatString="{0}:"></webControls:ExLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <telerikWebControls:ExRadGrid ID="rgDataImportMessages" runat="server"
                                                    OnItemDataBound="rgDataImportMessages_ItemDataBound"
                                                    OnNeedDataSource="rgDataImportMessages_NeedDataSource" EntityNameLabelID="1621"
                                                    OnDetailTableDataBind="rgDataImportMessages_DetailTableDataBind"
                                                    OnItemCreated="rgDataImportMessages_ItemCreated"
                                                    OnItemCommand="rgDataImportMessages_ItemCommand"
                                                    AllowCustomization="False" AllowSorting="false"
                                                    AllowPaging="False" AllowCustomPaging="False" AllowExportToExcel="True"
                                                    HierarchyLoadMode="Client" AllowExportToPDF="True" CustomPaging="true"
                                                    MasterTableView-GroupLoadMode="Client"
                                                    AllowCustomFilter="False" AllowRefresh="False" ClientSettings-Scrolling-AllowScroll="true">
                                                    <MasterTableView AllowSorting="false" AllowPaging="true"
                                                        HierarchyLoadMode="Client"
                                                        DataKeyNames="DataImportMessageDetailID">
                                                        <DetailTables>
                                                            <telerik:GridTableView Name="rgMessageDetails" EnableColumnsViewState="false"
                                                                AllowSorting="false" AllowPaging="false" runat="server" />
                                                        </DetailTables>
                                                        <GroupHeaderTemplate>
                                                            <webControls:ExImage ID="imgSuccess" runat="server" LabelID="1618" SkinID="SuccessIcon"
                                                                Visible="false" />
                                                            <webControls:ExImage ID="imgFailure" runat="server" LabelID="5000033" SkinID="ExpireIcon"
                                                                Visible="false" />
                                                            <webControls:ExImage ID="imgWarning" runat="server" LabelID="1617" SkinID="WarningIcon"
                                                                Visible="false" />
                                                            <asp:Label runat="server" ID="lblDataImportMessage"></asp:Label>
                                                        </GroupHeaderTemplate>
                                                        <GroupByExpressions>
                                                            <telerik:GridGroupByExpression>
                                                                <GroupByFields>
                                                                    <telerik:GridGroupByField FieldName="DataImportMessageLabelID" />
                                                                </GroupByFields>
                                                                <SelectFields>
                                                                    <telerik:GridGroupByField FieldName="DataImportMessage" Aggregate="First" />
                                                                    <telerik:GridGroupByField FieldName="DataImportMessageTypeID" Aggregate="First" />
                                                                </SelectFields>
                                                            </telerik:GridGroupByExpression>
                                                        </GroupByExpressions>
                                                        <Columns>
                                                            <telerikWebControls:ExGridTemplateColumn LabelID="2863" SortExpression="ExcelRowNumber"
                                                                HeaderStyle-Width="95%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExcelRowNumber" runat="server" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerikWebControls:ExRadGrid>
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                            <td></td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlWarning" runat="server">
                                        <tr>
                                            <td colspan="4">
                                                <webControls:ExLabel ID="lblConfirmUpload" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                            <td></td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td colspan="4" align="right">
                                            <webControls:ExButton ID="btnYes" runat="server" SkinID="ExButton100" LabelID="1481"
                                                OnClick="btnYes_Click" />&nbsp;
                                            <webControls:ExButton ID="btnReject" runat="server" SkinID="ExButton100" LabelID="1482"
                                                OnClick="btnReject_Click" />&nbsp;
                                            <webControls:ExButton ID="btnBack" runat="server" SkinID="ExButton100" LabelID="1545"
                                                OnClick="btnBack_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
