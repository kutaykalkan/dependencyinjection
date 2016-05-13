<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GLDataRecurringScheduleItems.ascx.cs"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_GLDataRecurringScheduleItems" %>
<%@ Register TagPrefix="UserControls" TagName="GLDataRecurringScheduleItemsGrid"
    Src="~/UserControls/RecForm/GLDataRecurringScheduleItemsGrid.ascx" %>
<div id="divMainContent" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="2px"></td>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <col width="2px" />
                    <col width="82px" />
                    <col width="1210px" />
                    <col width="2px" />
                    <tr>
                        <td class="ExpandPanelTopLeft" height="16" align="left">
                            <asp:Image ID="Image1" SkinID="BorderTopLeft" runat="server" />
                        </td>
                        <td height="16" align="right">
                            <asp:Image SkinID="ArrowTop" runat="server" />
                        </td>
                        <td class="ExpandPanelTopBorder" height="16" align="left">
                            <asp:Image ID="Image5" SkinID="BorderHorizontalTop" runat="server" />
                        </td>
                        <td class="ExpandPanelTopLeft" height="16">
                            <asp:Image ID="Image3" SkinID="BorderTopLeft" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ExpandPanelLeftBorder">
                            <asp:Image ID="Image2" SkinID="BorderVerticalLeft" runat="server" />
                        </td>
                        <td colspan="2">
                            <!-- Start - User Control Content here --->
                            <table id="tblMainContent" width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="GridHeaderPadding">
                                        <webControls:ExLabel ID="lblGridHeading" LabelID="1872" runat="server" SkinID="BlueBold11ArialUnderline"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <UserControls:GLDataRecurringScheduleItemsGrid ID="ucGLDataRecurringScheduleItemsGrid"
                                            AllowExportToExcel="true" AllowExportToPDF="true" AllowCustomFilter="true" AllowCustomPaging="true"
                                            AllowSelectionPersist="true" AllowDisplayCurrencyPnl="false" ShowDescriptionColum="true"
                                            ShowSelectCheckBoxColum="true" ShowCloseDateColum="false" runat="server">
                                            <ColumnCollection>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="2087" ItemStyle-HorizontalAlign="Left"
                                                    UniqueName="ExportFileIcon">
                                                    <ItemTemplate>
                                                        <webControls:ExImageButton ID="imgViewFile" Visible="false" runat="server" ImageAlign="Left"
                                                            SkinID="FileDownloadIcon" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="5%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridButtonColumn UniqueName="CopyColumn" CommandName="Copy" ButtonType="ImageButton"
                                                    ButtonCssClass="DeleteButton" HeaderStyle-Width="3%">
                                                </telerikWebControls:ExGridButtonColumn>
                                                <telerikWebControls:ExGridButtonColumn UniqueName="CopyAndCloseColumn" CommandName="CopyAndClose" ButtonType="ImageButton"
                                                    ButtonCssClass="DeleteButton" HeaderStyle-Width="3%">
                                                </telerikWebControls:ExGridButtonColumn>
                                                <%-- Add RecItem Comment --%>
                                                <telerikWebControls:ExGridTemplateColumn UniqueName="AddRecItemComment">
                                                    <ItemTemplate>
                                                        <webControls:ExHyperLink ID="hlAddRecItemComment" runat="server" SkinID="GridHyperLinkImageAddComment" /><%--CommandName="ShowInputForm" CommandArgument='<%# Eval("GLReconciliationItemInputID") %>'--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="3%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <%--Edit Button--%>
                                                <telerikWebControls:ExGridTemplateColumn UniqueName="ShowInputForm">
                                                    <ItemTemplate>
                                                        <webControls:ExHyperLink ID="hlShowItemInputForm" runat="server" SkinID="ShowItemInputPopup" /><%--CommandName="ShowInputForm" CommandArgument='<%# Eval("GLReconciliationItemInputID") %>'--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="5%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridButtonColumn UniqueName="DeleteColumn" CommandName="Delete"
                                                    ConfirmDialogType="Classic" ConfirmTextLabelID="1781" ConfirmTextFormatString="{0}?"
                                                    ButtonCssClass="DeleteButton" HeaderStyle-Width="5%">
                                                </telerikWebControls:ExGridButtonColumn>
                                            </ColumnCollection>
                                        </UserControls:GLDataRecurringScheduleItemsGrid>
                                    </td>
                                </tr>
                                <tr class="BlankRow">
                                    <td></td>
                                </tr>
                                <tr id="trOpenItemsButtonRow" runat="server">
                                    <td align="right">
                                        <webControls:ExButton ID="btnCopyAndClose" runat="server" LabelID="2784" SkinID="ExButton100" OnClick="btnCopyAndClose_Click" />
                                        <webControls:ExButton ID="btnCopy" runat="server" LabelID="2783" SkinID="ExButton100" OnClick="btnCopy_Click" />
                                        <webControls:ExButton ID="btnClose" runat="server" LabelID="1771" SkinID="ExButton100" />
                                        <webControls:ExButton ID="btnAdd" runat="server" LabelID="1560" SkinID="ExButton100" />
                                        <webControls:ExButton ID="btnDelete" runat="server" LabelID="1564" SkinID="ExButton100"
                                            OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlClosedItems" runat="server" Width="100%">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td class="GridHeaderPadding">
                                                        <webControls:ExLabel ID="lblGridHeading2" runat="server" LabelID="1873" SkinID="BlueBold11ArialUnderline"></webControls:ExLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <UserControls:GLDataRecurringScheduleItemsGrid ID="ucCloseGLDataRecurringScheduleItemsGrid"
                                                            AllowExportToExcel="true" AllowExportToPDF="true" AllowCustomFilter="true" AllowCustomPaging="true"
                                                            AllowSelectionPersist="true" AllowDisplayCurrencyPnl="false" ShowSelectCheckBoxColum="true"
                                                            ShowCloseDateColum="true" runat="server">
                                                            <ColumnCollection>
                                                                <telerikWebControls:ExGridTemplateColumn LabelID="2087" ItemStyle-HorizontalAlign="Left"
                                                                    UniqueName="ExportFileIcon">
                                                                    <ItemTemplate>
                                                                        <webControls:ExImageButton ID="imgViewFile" Visible="false" runat="server" ImageAlign="Left"
                                                                            SkinID="FileDownloadIcon" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                </telerikWebControls:ExGridTemplateColumn>
                                                                <%--Edit Button--%>
                                                                <telerikWebControls:ExGridTemplateColumn UniqueName="ShowInputForm">
                                                                    <ItemTemplate>
                                                                        <webControls:ExHyperLink ID="hlShowItemInputForm" runat="server" SkinID="ShowItemInputPopup" /><%--CommandName="ShowInputForm" CommandArgument='<%# Eval("GLReconciliationItemInputID") %>'--%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="5%" />
                                                                </telerikWebControls:ExGridTemplateColumn>
                                                                <telerikWebControls:ExGridButtonColumn UniqueName="DeleteColumn" CommandName="Delete"
                                                                    ConfirmDialogType="Classic" Visible="false" ConfirmTextLabelID="1781" ConfirmTextFormatString="{0}?"
                                                                    ButtonCssClass="DeleteButton" HeaderStyle-Width="5%">
                                                                </telerikWebControls:ExGridButtonColumn>
                                                            </ColumnCollection>
                                                        </UserControls:GLDataRecurringScheduleItemsGrid>
                                                    </td>
                                                </tr>
                                                <tr class="BlankRow">
                                                    <td></td>
                                                </tr>
                                                <tr id="trClosedItemsButtonRow" runat="server">
                                                    <td align="right">
                                                        <webControls:ExButton ID="btnReopen" runat="server" LabelID="1764" SkinID="ExButton100"
                                                            OnClick="btnReopen_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <!-- End - User Control Content here  --->
                        </td>
                        <td class="ExpandPanelLeftBorder">
                            <asp:Image ID="Image4" SkinID="BorderVerticalLeft" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="ExpandPanelBottomBorder">
                            <asp:Image ID="Image6" SkinID="BorderHorizontalBottom" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="2px"></td>
        </tr>
    </table>
</div>
<asp:HiddenField runat="server" ID="hdIsRefreshData" Value="0" />
<asp:HiddenField runat="server" ID="hdIsExpanded" Value="0" />
