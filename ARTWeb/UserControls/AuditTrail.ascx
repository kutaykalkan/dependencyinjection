<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_AuditTrail" Codebehind="AuditTrail.ascx.cs" %>
<table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <telerikWebControls:ExRadGrid ID="rgAuditTrail" runat="server" EntityNameLabelID="1380"
                    AllowPaging="false" AllowSorting="true" AllowExportToExcel="true" AllowExportToPDF ="true" AllowPrint="true" AllowPrintAll="true" 
                      OnNeedDataSource="rgAuditTrail_NeedDataSource" OnItemDataBound="rgAuditTrail_ItemDataBound"
                      OnSortCommand="rgReviewNotes_SortCommand"
                               OnItemCreated="rgAuditTrail_ItemCreated" OnItemCommand ="rgAuditTrail_OnItemCommand">
                    
                    <MasterTableView>
                        <Columns>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="StatusDate">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDate" runat="server"/>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1533 " SortExpression="FullName">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblUser" runat="server" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1579" SortExpression="Status">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAction" runat="server" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerikWebControls:ExRadGrid>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td align="right">
                <webControls:ExButton ID="btnBack" LabelID="1545" runat="server" />
            </td>
        </tr>
    </table>