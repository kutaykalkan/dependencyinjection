<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" Inherits="Pages_DataImportConfigurationAuditTrail" Theme="SkyStemBlueBrown" Codebehind="DataImportConfigurationAuditTrail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <telerikWebControls:ExRadGrid ID="rgWarningAuditTrail" runat="server" EntityNameLabelID="1380"
                    AllowExportToExcel="true" AllowExportToPDF="true"
                    OnItemDataBound="rgWarningAuditTrail_ItemDataBound" OnItemCreated="rgWarningAuditTrail_ItemCreated" OnItemCommand="rgWarningAuditTrail_ItemCommand" OnPageIndexChanged="rgWarningAuditTrail_PageIndexChanged">
                    <MasterTableView AllowPaging="true" Width="100%" TableLayout="Auto" Name="WarningAuditTrailGridView">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="DataImportTypeLabelID" />
                                </GroupByFields>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldName="DataImportTypeLabel" Aggregate="First" />
                                </SelectFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1307" SortExpression="DataImportType" Visible="false">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDataImportType" runat="server" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2891" SortExpression="DataImportMessage">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDataImportMessage" runat="server" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1204" SortExpression="FirstName">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblFirstName" runat="server" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1278" SortExpression="RoleName">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblRoleName" runat="server" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1579 " SortExpression="IsEnabled">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblIsEnabled" runat="server" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1333" SortExpression="ChangeDate">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblChangeDate" runat="server" />
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
        <tr>
            <td align="right">
                <webControls:ExButton ID="btnBack" LabelID="1545" runat="server" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

