<%@ Page Language="C#" MasterPageFile="~/MasterPages/RecProcessMasterPage.master"
    AutoEventWireup="true" CodeFile="ReviewNotes.aspx.cs" Inherits="Pages_ReviewNotes"
    Theme="SkyStemBlueBrown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphRecProcess" runat="Server">

    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">
                    <telerikWebControls:ExRadGrid ID="rgReviewNotes" runat="server" ShowHeader="true" EntityNameLabelID="1394"
                        AutoGenerateColumns="false" AllowExportToExcel="true" AllowPaging="false"
                        AllowExportToPDF="true" AllowPrint="true" AllowPrintAll="true" AllowSorting="true"
                        OnItemCommand="rgReviewNotes_OnItemCommand"
                        OnItemCreated="rgReviewNotes_ItemCreated"
                        OnNeedDataSource="rgReviewNotes_NeedDataSource" OnSortCommand="rgReviewNotes_SortCommand"
                        OnDeleteCommand="rgReviewNotes_DeleteCommand" OnItemDataBound="rgReviewNotes_ItemDataBound">
                        <MasterTableView>
                            <Columns>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1778" SortExpression="ReviewNoteSubject">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblSubject" runat="server" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1509" SortExpression="DateAdded">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblDateAdded" runat="server" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1508" SortExpression="AddedByFullName">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblAddedBy" runat="server" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn SortExpression="DateRevised" LabelID="1552">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblDateRevised" runat="server" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn SortExpression="RevisedByFullName" LabelID="1543">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblRevisedBy" runat="server" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <%--Documents--%>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="AttachmentCount" LabelID="2056"
                                    SortExpression="AttachmentCount" DataType="System.Int32">
                                    <ItemTemplate>
                                        <webControls:ExHyperLink ID="hlAttachmentCount" runat="server"></webControls:ExHyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <HeaderStyle Width="5%" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <%--Delete Button Column--%>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="ViewEditReviewNote">
                                    <ItemTemplate>
                                        <webControls:ExHyperLink ID="hlItemPopup" runat="server" SkinID="GridHyperLinkImage">
                                        &nbsp;&nbsp;&nbsp;
                                        </webControls:ExHyperLink>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridButtonColumn ConfirmDialogType="Classic" ConfirmTextLabelID="1781" ConfirmTextFormatString="{0}?" ButtonCssClass="DeleteButton" ButtonType="ImageButton" UniqueName="DeleteColumn" CommandName="Delete">
                                </telerikWebControls:ExGridButtonColumn>
                            </Columns>
                        </MasterTableView>
                    </telerikWebControls:ExRadGrid>
                </asp:Panel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td align="right">
                <webControls:ExButton ID="btnAdd" runat="server" LabelID="1560" SkinID="ExButton100" />
                <webControls:ExButton ID="btnCancel" runat="server" LabelID="1545" />
                <asp:HiddenField runat="server" ID="hdIsRefreshData" Value="1" />
            </td>
        </tr>
    </table>
</asp:Content>
