<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemInputAccurable.ascx.cs"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_ItemInputAccurable" %>
<%@ Register TagPrefix="UserControls" TagName="LegendItemInput" Src="~/UserControls/LegendOnItemInputForm.ascx" %>
<div id="divMainContent" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="2px">
            </td>
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
                        <td height="16">
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
                            <asp:UpdatePanel ID="updpnlMain" runat="server">
                                <ContentTemplate>
                                    <table id="tblMainContent" width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="GridHeaderPadding">
                                                <webControls:ExLabel ID="lblGridHeading" LabelID="1872" runat="server" SkinID="BlueBold11ArialUnderline"></webControls:ExLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <telerikWebControls:ExRadGrid ID="rgGLAdjustments" runat="server" Width="100%" ShowHeader="true"
                                                    OnNeedDataSource="rgGLAdjustments_NeedDataSource" OnItemDataBound="rgGLAdjustments_ItemDataBound"
                                                    OnItemCommand="rgGLAdjustments_ItemCommand" AllowExportToExcel="true" AllowExportToPDF="true"
                                                    OnItemCreated="rgGLAdjustmentst_ItemCreated"
                                                    AllowPrint="true" AllowPrintAll="true" AllowSorting="true" ClientSettings-Selecting-AllowRowSelect="true"
                                                    AllowMultiRowSelection="true" SkinID="SkyStemBlueBrownWithoutBorder">
                                                    <MasterTableView DataKeyNames="GLDataRecItemID" Width="100%" ShowFooter="true">
                                                        <Columns>
                                                            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" Visible="true"
                                                                HeaderStyle-Width="5%" />
                                                            <%--Amount--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Description" LabelID="1408"
                                                                SortExpression="Comments" DataType="System.String">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblDescription" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <webControls:ExLabel ID="lblTotal" LabelID="1656" runat="server"></webControls:ExLabel>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <FooterStyle HorizontalAlign="Left" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Amount--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Amount" LabelID="1675" SortExpression="Amount"
                                                                DataType="System.Decimal">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblAmount" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <webControls:ExLabel ID="lblLocalCurrencyTotal" runat="server"></webControls:ExLabel>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" Width="15%" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Amount In Base Currency--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="AmountBaseCurrency" LabelID="1673"
                                                                SortExpression="AmountBaseCurrency" DataType="System.Decimal" FooterStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblAmountBaseCurrency" runat="server" Text='<%#Helper.GetDisplayDecimalValue((Decimal?)Eval("AmountBaseCurrency"))%>'></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <div style="text-align: right;">
                                                                        <webControls:ExLabel ID="lblBaseCurrencyTotal" runat="server"></webControls:ExLabel>
                                                                    </div>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" Width="15%" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Amount In Reporting Currency--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="AmountReportingCurrency" LabelID="1674"
                                                                SortExpression="AmountReportingCurrency" DataType="System.Decimal" FooterStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblAmountReportingCurrency" runat="server" Text='<%#Helper.GetDisplayDecimalValue((Decimal?)Eval("AmountReportingCurrency"))%>'></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <div style="text-align: right;">
                                                                        <webControls:ExLabel ID="lblReportingCurrencyTotal" runat="server"></webControls:ExLabel>
                                                                    </div>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" Width="15%" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Open Date--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="OpenDate" LabelID="1511 " SortExpression="OpenDate"
                                                                DataType="System.DateTime">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblOpenDate" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Comments--%>
                                                            <%--Aging--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Aging" LabelID="1512 " ItemStyle-HorizontalAlign="Right"
                                                                SortExpression="Aging" DataType="System.Int32">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblAging" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Documents--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Documents" LabelID="2056" SortExpression="AttachmentCount"
                                                                DataType="System.Int32">
                                                                <ItemTemplate>
                                                                    <%--<userControl:DocumentUpload ID="ucDocumentUpload" runat="server" />--%>
                                                                    <webControls:ExLabel ID="lblAttachmentCount" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="5%" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%----Rec Item # ----%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="RecItemNumber" LabelID="2118 ">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblRecItemNumber" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            
                                                             <%----MatchSetRef# ----%>
                                                             <telerikWebControls:ExGridTemplateColumn UniqueName="MatchSetRefNumber" LabelID="2276 ">
                                                                 <ItemTemplate>
                                                                    <webControls:ExHyperLink ID="hlMatchSetRefNumber" runat="server"></webControls:ExHyperLink>
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
                                                            <%--Edit Button--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ShowInputForm">
                                                                <ItemTemplate>
                                                                    <webControls:ExHyperLink ID="hlShowItemInputForm" runat="server" SkinID="ShowItemInputPopup" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="5%" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridButtonColumn UniqueName="DeleteColumn" CommandName="Delete"
                                                                ConfirmDialogType="Classic" ConfirmTextLabelID="1781" ConfirmTextFormatString="{0}?"
                                                                ButtonCssClass="DeleteButton" HeaderStyle-Width="5%">
                                                            </telerikWebControls:ExGridButtonColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </telerikWebControls:ExRadGrid>
                                            </td>
                                        </tr>
                                        <%--Blank Row--%>
                                        <tr class="BlankRow">
                                            <td>
                                            </td>
                                        </tr>
                                        <tr id="trOpenItemsButtonRow" runat="server">
                                            <td align="right">
                                                <webControls:ExButton ID="btnClose" runat="server" LabelID="1771" SkinID="ExButton100"/>
                                                <webControls:ExButton ID="btnAdd" runat="server" LabelID="1560" SkinID="ExButton100"/>
                                                    <webControls:ExButton ID="btnDelete" runat="server" LabelID="1564" SkinID="ExButton100"
                                            OnClick="btnDelete_Click" />
                                                <%--   <webControls:ExButton ID="btnCancel" runat="server" LabelID="1545" SkinID="ExButton100"
                                OnClick="btnCancel_OnClick" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
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
                                                                <telerikWebControls:ExRadGrid ID="rgGLAdjustmentCloseditems" runat="server" Width="100%"
                                                                    ShowHeader="true" OnItemDataBound="rgGLAdjustmentCloseditems_ItemDataBound" OnNeedDataSource="rgGLAdjustmentCloseditems_NeedDataSource"
                                                                      OnItemCreated="rgGLAdjustmentCloseditems_ItemCreated"  OnItemCommand="rgGLAdjustmentCloseditems_ItemCommand"
                                                                    ClientSettings-Selecting-AllowRowSelect="true" AllowMultiRowSelection="true"
                                                                    AllowExportToExcel="true" AllowExportToPDF="true" AllowPrint="true" AllowPrintAll="true"
                                                                    AllowSorting="true" SkinID="SkyStemBlueBrownWithoutBorder">
                                                                    <MasterTableView DataKeyNames="GLDataRecItemID" EditMode="InPlace" Width="100%">
                                                                        <Columns>
                                                                            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                                                            <%--Amount--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Amount" LabelID="1675" SortExpression="Amount"
                                                                                DataType="System.Decimal">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblAmount" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Amount In Base Currency--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="AmountBaseCurrency" LabelID="1674"
                                                                                SortExpression="AmountBaseCurrency" DataType="System.Decimal">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblAmountBaseCurrency" runat="server" Text='<%#Helper.GetDisplayDecimalValue((Decimal?)Eval("AmountBaseCurrency"))%>'></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Amount In Reporting Currency--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="AmountReportingCurrency" LabelID="1674"
                                                                                SortExpression="AmountReportingCurrency" DataType="System.Decimal">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblAmountReportingCurrency" runat="server" Text='<%#Helper.GetDisplayDecimalValue((Decimal?)Eval("AmountReportingCurrency"))%>'></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Open Date--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="OpenDate" LabelID="1511" SortExpression="OpenDate"
                                                                                DataType="System.DateTime">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblOpenDate" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="10%" />
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Comments--%>
                                                                            <%--Aging--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Aging" LabelID="1512 " ItemStyle-HorizontalAlign="Right"
                                                                                SortExpression="Aging" DataType="System.Int32">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblAging" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Right" Width="8%" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Close/Resolution Date--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="CloseDate" LabelID="1411" SortExpression="CloseDate"
                                                                                DataType="System.DateTime">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblCloseDate" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="10%" />
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Documents--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Documents" LabelID="2056" SortExpression="AttachmentCount"
                                                                                DataType="System.Int32">
                                                                                <ItemTemplate>
                                                                                    <%--<userControl:DocumentUpload ID="ucDocumentUpload" runat="server" />--%>
                                                                                    <webControls:ExLabel ID="lblAttachmentCount" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%----Rec Item # ----%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="RecItemNumber" LabelID="2118 ">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblRecItemNumber" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                              <%----MatchSetRef# ----%>
                                                                             <telerikWebControls:ExGridTemplateColumn UniqueName="MatchSetRefNumber" LabelID="2276 ">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExHyperLink ID="hlMatchSetRefNumber" runat="server"></webControls:ExHyperLink>
                                                                                </ItemTemplate>
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            
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
                                                                            <telerik:GridBoundColumn UniqueName="GLRecItemID" Visible="false" DataField="GLDataRecItemID">
                                                                            </telerik:GridBoundColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerikWebControls:ExRadGrid>
                                                            </td>
                                                        </tr>
                                                        <tr class="BlankRow">
                                                            <td>
                                                            </td>
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
                                    <asp:HiddenField runat="server" ID="hdIsRefreshData" Value="1" />
                                    <asp:HiddenField runat="server" ID="hdIsExpanded" Value="0" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
            <td width="2px">
            </td>
        </tr>
    </table>
</div>
