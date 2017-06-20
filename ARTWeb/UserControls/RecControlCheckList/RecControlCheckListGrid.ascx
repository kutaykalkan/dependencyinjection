<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_RecControlCheckListGrid" Codebehind="RecControlCheckListGrid.ascx.cs" %>
<table width="100%" cellpadding="0px" cellspacing="0px">

    <tr>
        <td style="width: 100%" colspan="5" class="">
            <telerikWebControls:ExRadGrid ID="rgRecControlCheckListItems" Width="100%" runat="server" OnItemDataBound="rgRecControlCheckListItems_ItemDataBound"
                OnNeedDataSource="rgRecControlCheckListItems_NeedDataSource" ClientSettings-Selecting-AllowRowSelect="true"
                AllowMultiRowSelection="true" AllowSorting="true" AllowPaging="true" OnItemCommand="rgRecControlCheckListItems_ItemCommand"
                OnPageIndexChanged="rgRecControlCheckListItems_PageIndexChanged" OnItemCreated="rgRecControlCheckListItems_ItemCreated"
                OnPageSizeChanged="rgRecControlCheckListItems_PageSizeChanged" OnPdfExporting="rgRecControlCheckListItems_PdfExporting"
                SkinID="SkyStemBlueBrownRecItems" AllowCauseValidationExportToExcel="false" AllowCauseValidationExportToPDF="false">
                <ClientSettings>
                    <Selecting UseClientSelectColumnOnly="true" />
                </ClientSettings>
                <MasterTableView ClientDataKeyNames="RecControlCheckListID,GLDataRecControlCheckListID"
                    DataKeyNames="RecControlCheckListID,GLDataRecControlCheckListID" Width="100%" ShowFooter="true" TableLayout="Auto"
                    Name="GLDataRecItemsGridView">
                    <PagerTemplate>
                        <asp:Panel ID="PagerPanel" runat="server">
                            <asp:Panel runat="server" ID="pnlPageSizeDDL">
                                <div style="float: left; margin-right: 10px;">
                                    <webControls:ExLabel ID="lblPageSize" runat="server" LabelID="2493"></webControls:ExLabel>
                                    <asp:DropDownList ID="ddlPageSize" SkinID="DropDownList50" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="NumericPagerPlaceHolder" />
                        </asp:Panel>
                    </PagerTemplate>
                    <Columns>
                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" Visible="true"
                            HeaderStyle-Width="5%">
                        </telerikWebControls:ExGridClientSelectColumn>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="CheckListNumber" LabelID="2829"
                            HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblCheckListNumber" runat="server" />
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="Description" LabelID="1408" ItemStyle-Width="28%" ItemStyle-Wrap="true"> 
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblDescription" runat="server" />
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="Completed"
                            LabelID="2559">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlCompleted" runat="server" SkinID="DropDownList100" />
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="Reviewed" LabelID="1093">
                            <ItemTemplate>
                               <asp:DropDownList ID="ddlReviewed" runat="server" SkinID="DropDownList100"/>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn LabelID="2087" ItemStyle-HorizontalAlign="Left"
                            UniqueName="ExportFileIcon">
                            <ItemTemplate>
                                <webControls:ExImageButton ID="imgViewFile" Visible="false" runat="server" ImageAlign="Left"
                                    SkinID="FileDownloadIcon" />
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%-- Add RecControl Checklist Comment --%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="Comments">
                            <ItemTemplate>
                                <webControls:ExHyperLink ID="hlAddComment" runat="server" SkinID="GridHyperLinkImageAddComment" />
                            </ItemTemplate>
                            <HeaderStyle Width="3%" />
                        </telerikWebControls:ExGridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <FooterStyle HorizontalAlign="Right" />
            </telerikWebControls:ExRadGrid>
        </td>
    </tr>
</table>
<iframe id="ifDownloader" runat="server" style="display:none;" />
