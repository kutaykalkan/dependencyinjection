<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemInputWriteOff.ascx.cs"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_ItemInputWriteOff" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
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
                                                <telerikWebControls:ExRadGrid ID="rgItemInputWO" runat="server" ShowHeader="true"
                                                    AllowInster="false" InsertLabelID="1413" InsertImageUrl="~/App_Themes/SkyStemBlueBrown/Images/Add_new_rule.gif"
                                                    OnNeedDataSource="rgItemInputWO_NeedDataSource" OnItemCreated="rgItemInputWO_ItemCreated"
                                                    OnItemCommand="rgItemInputWO_ItemCommand" OnItemDataBound="rgItemInputWO_ItemDataBound"
                                                    AllowSorting="true" Width="100%" ClientSettings-Selecting-AllowRowSelect="true"
                                                    AllowMultiRowSelection="true" AllowPrint="true" AllowPrintAll="true" SkinID="SkyStemBlueBrownWithoutBorder">
                                                    <%-- OnInsertCommand="rgItemInputWO_InsertCommand"   OnUpdateCommand="rgItemInputWO_UpdateCommand"  OnDeleteCommand="rgItemInputWO_DeleteCommand" --%>
                                                    <MasterTableView DataKeyNames="GLDataWriteOnOffID" EditMode="InPlace" Width="100%"
                                                        ShowFooter="true">
                                                        <Columns>
                                                            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" Visible="true"
                                                                HeaderStyle-Width="5%">
                                                            </telerikWebControls:ExGridClientSelectColumn>
                                                            <%--Comments--%>
                                                            <telerikWebControls:ExGridTemplateColumn ItemStyle-Width="20%" UniqueName="Comments"
                                                                LabelID="1408  " SortExpression="Comments" DataType="System.String">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblComments" runat="server" Text='<%# Eval("Comments")%>'></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <webControls:ExLabel ID="lblTotal" LabelID="1656" runat="server"></webControls:ExLabel>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <FooterStyle HorizontalAlign="Left" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Amount--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Amount" LabelID="1675 " SortExpression="Amount"
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
                                                            <%--Aging--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Aging" LabelID="1512 " SortExpression="Aging"
                                                                DataType="System.Int32">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblAging" runat="server" Text='<%#Eval("Aging") %>'></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="WriteOffOnItemNumber" LabelID="2120 ">
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
                                                            <%--Added By--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="AddedBy" LabelID="1508" SortExpression="UserName"
                                                                DataType="System.String">
                                                                <ItemTemplate>
                                                                    <%# Eval ("UserName")%>
                                                                </ItemTemplate>
                                                                <%--<EditItemTemplate>
                                                        <%# SessionHelper.GetCurrentUser().LoginID  %>
                                                    </EditItemTemplate>--%>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Write ON / Off--%>
                                                            <%--<telerikWebControls:ExGridDropDownColumn UniqueName="WriteOnOff" LabelID="1425 "
                                                    DropDownControlType="DropDownList" DataField="DbCr" ListDataMember="GetDebitCreditDataTable"
                                                    ListValueField="DbCr" ListTextField="DbCr">
                                                </telerikWebControls:ExGridDropDownColumn>--%>
                                                            <%--Resolution Date--%>
                                                            <%--<telerikWebControls:ExGridTemplateColumn UniqueName="CloseDate" LabelID="1514 ">
                                                    <ItemTemplate>
                                                        <%# Helper.GetDisplayDate((DateTime)Eval("CloseDate"))%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <webControls:ExCalendar ID="calCloseDate" runat="server" Text='<%# Bind("CloseDate","{0:d/M/yyyy}") %>'
                                                            Width="80"></webControls:ExCalendar>
                                                    </EditItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>--%>
                                                            <%--Resolution Comments--%>
                                                            <%--<telerikWebControls:ExGridTemplateColumn UniqueName="ResolutionComments" LabelID="1515 ">
                                                    <ItemTemplate>
                                                        <webControls:ExLabel ID="lblResolutionComments" runat="server" Text='<%# Eval("ResolutionComments")%>'></webControls:ExLabel>
                                                    </ItemTemplate>
                                                    <%--TODO: Put multiline textbox in skin--%>
                                                            <%--<EditItemTemplate>
                                                        <webControls:ExTextBox ID="txtResolutionComments" runat="server" Text='<%# Bind("ResolutionComments") %>'
                                                            TextMode="MultiLine" Rows="4" />
                                                    </EditItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>--%>
                                                            <%--Journal Entry ref--%>
                                                            <%--<telerikWebControls:ExGridTemplateColumn UniqueName="RefNumber" LabelID="1711 ">
                                                    <ItemTemplate>
                                                        <webControls:ExLabel ID="lblJournalRefNumber" runat="server" Text='<%# Eval("JournalEntryRef")%>'></webControls:ExLabel>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <webControls:ExTextBox ID="txtJournalEntryRef" runat="server" Text='<%# Bind("JournalEntryRef") %>'
                                                            SkinID="ExTextBox100" />
                                                    </EditItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerikWebControls:ExGridTemplateColumn>--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ShowInputForm">
                                                                <ItemTemplate>
                                                                    <webControls:ExHyperLink ID="hlShowItemInputForm" runat="server" SkinID="ShowItemInputPopup" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="5%" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridButtonColumn UniqueName="DeleteColumn" CommandName="Delete"
                                                                CommandArgument="GLDataWriteOnOffID" ConfirmDialogType="Classic" ConfirmTextLabelID="1781"
                                                                ConfirmTextFormatString="{0}?" ButtonCssClass="DeleteButton">
                                                            </telerikWebControls:ExGridButtonColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerikWebControls:ExRadGrid>
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                            <td>
                                            </td>
                                        </tr>
                                        <tr id="trOpenItemsButtonRow" runat="server">
                                            <td align="right">
                                                <webControls:ExButton ID="btnClose" runat="server" LabelID="1771" SkinID="ExButton100" />
                                                <webControls:ExButton ID="btnAdd" runat="server" LabelID="1560" SkinID="ExButton100" />
                                                <webControls:ExButton ID="btnDelete" runat="server" LabelID="1564" SkinID="ExButton100"
                                                    OnClick="btnDelete_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlClosedItems" runat="server">
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td class="GridHeaderPadding">
                                                                <webControls:ExLabel ID="lblGridHeading2" runat="server" LabelID="1873" SkinID="BlueBold11ArialUnderline"></webControls:ExLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <telerikWebControls:ExRadGrid ID="rgDataWriteOnOffCloseditems" runat="server" Width="100%"
                                                                    ShowHeader="true" OnItemDataBound="rgDataWriteOnOffCloseditems_ItemDataBound"
                                                                    OnItemCreated="rgDataWriteOnOffCloseditems_ItemCreated" OnItemCommand="rgDataWriteOnOffCloseditems_ItemCommand"
                                                                    OnNeedDataSource="rgDataWriteOnOffCloseditems_NeedDataSource" ClientSettings-Selecting-AllowRowSelect="true"
                                                                    AllowMultiRowSelection="true" AllowPrint="true" AllowPrintAll="true" AllowSorting="true"
                                                                    SkinID="SkyStemBlueBrownWithoutBorder">
                                                                    <MasterTableView DataKeyNames="GLDataWriteOnOffID" EditMode="InPlace" Width="100%">
                                                                        <Columns>
                                                                            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                                                            <%--Amount--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Amount" LabelID="1675" SortExpression="Amount"
                                                                                DataType="System.Decimal">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblAmount" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Amount In Reporting Currency--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="AmountBaseCurrency" LabelID="1673"
                                                                                SortExpression="AmountBaseCurrency" DataType="System.Decimal">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblAmountBaseCurrency" runat="server" Text='<%#Helper.GetDisplayDecimalValue((Decimal?)Eval("AmountBaseCurrency"))%>'></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Amount In Reporting Currency--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="AmountReportingCurrency" LabelID="1674"
                                                                                SortExpression="AmountReportingCurrency" DataType="System.Decimal">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblAmountReportingCurrency" runat="server" Text='<%#Helper.GetDisplayDecimalValue((Decimal?)Eval("AmountReportingCurrency"))%>'></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Open Date--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="OpenDate" LabelID="1511 " SortExpression="OpenDate"
                                                                                DataType="System.DateTime">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblOpenDate" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Comments--%>
                                                                            <%--Aging--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Aging" LabelID="1512 " ItemStyle-HorizontalAlign="Right"
                                                                                SortExpression="Aging" DataType="System.Int32">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblAging" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="WriteOffOnItemNumber" LabelID="2120 ">
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
                                                                            <%--Close/Resolution Date--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="CloseDate" LabelID="1411" SortExpression="CloseDate"
                                                                                DataType="System.DateTime">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblCloseDate" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Documents--%>
                                                                            <%--<telerikWebControls:ExGridTemplateColumn UniqueName="Documents" LabelID="2056" SortExpression="AttachmentCount"
                                                    DataType="System.Int32">
                                                                <ItemTemplate>
                                                                    <userControl:DocumentUpload ID="ucDocumentUpload" runat="server" />
                                                                    <webControls:ExLabel ID="lblAttachmentCount" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </telerikWebControls:ExGridTemplateColumn>--%>
                                                                            <%--Edit Button--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ShowInputForm">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExHyperLink ID="hlShowItemInputForm" runat="server" SkinID="ShowItemInputPopup" /><%--CommandName="ShowInputForm" CommandArgument='<%# Eval("GLReconciliationItemInputID") %>'--%>
                                                                                </ItemTemplate>
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <telerik:GridBoundColumn UniqueName="GLDataWriteOnOffID" Visible="false" DataField="GLDataWriteOnOffID">
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
                            <!-- End - User Control Content here  -->
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
