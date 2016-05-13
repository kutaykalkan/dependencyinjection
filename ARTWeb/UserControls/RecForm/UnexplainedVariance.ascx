<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UnexplainedVariance.ascx.cs"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_UnexplainedVariance" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%--    <div id="divMainContent">--%>
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
                                        <tr class="BlankRow">
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%--   <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">--%>
                                                <telerikWebControls:ExRadGrid ID="rgUnExpectedVariance" runat="server" ShowHeader="true"
                                                    OnNeedDataSource="rgUnExpectedVariance_NeedDataSource" OnItemCommand="rgUnExpectedVariance_ItemCommand"
                                                    OnItemCreated="rgUnExpectedVariance_ItemCreated" Width="100%" OnItemDataBound="rgUnExpectedVariance_ItemDataBound"
                                                    OnDeleteCommand="rgUnExpectedVariance_DeleteCommand" SkinID="SkyStemBlueBrownWithoutBorder"
                                                    AllowExportToExcel="true" AllowExportToPDF="true">
                                                    <%-- OnInsertCommand="rgUnExpectedVariance_InsertCommand"  OnUpdateCommand="rgUnExpectedVariance_UpdateCommand"--%>
                                                    <MasterTableView DataKeyNames="GLDataUnexplainedVarianceID" EditMode="InPlace" Width="100%">
                                                        <Columns>
                                                            <%--Added By--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="AddedBy" LabelID="1508">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblAddedBy" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--DateAdded--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="DateAdded" LabelID="1399  "
                                                                SortExpression="DateAdded" DataType="System.DateTime">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblDateAdded" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Amount In Base Currency--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="AmountInBaseCurrency" LabelID="1673 "
                                                                FormatString="{0}" SortExpression="AmountBaseCurrency" DataType="System.Decimal">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblAmountBaseCurrency" runat="server" Text='<%#Helper.GetDisplayDecimalValue((Decimal?)Eval("AmountBaseCurrency"))%>'></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            
                                                              <%--Amount In Reporting Currency--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="AmountInReportingCurrency" LabelID="1674 "
                                                                FormatString="{0}" SortExpression="AmountReportingCurrency" DataType="System.Decimal">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblAmountReportingCurrency" runat="server" Text='<%#Helper.GetDisplayDecimalValue((Decimal?)Eval("AmountReportingCurrency"))%>'></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Blank Space--%>
                                                            <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="50">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblSpacer" Width="50" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Comments--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Comments" LabelID="1408">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblComments" runat="server" Text='<%# Eval("Comments")%>'></webControls:ExLabel>
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Edit Button--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ShowInputForm" ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <webControls:ExHyperLink ID="hlShowItemInputForm" runat="server" SkinID="ShowItemInputPopup" /><%--CommandName="ShowInputForm" CommandArgument='<%# Eval("GLReconciliationItemInputID") %>'--%>
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Delete Button Column--%>
                                                            <telerikWebControls:ExGridButtonColumn ConfirmDialogType="Classic" ConfirmTextLabelID="1781"
                                                                ConfirmTextFormatString="{0}?" ButtonCssClass="DeleteButton" ButtonType="ImageButton"
                                                                UniqueName="DeleteColumn" CommandName="Delete">
                                                            </telerikWebControls:ExGridButtonColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerikWebControls:ExRadGrid>
                                                <%--    </asp:Panel>--%>
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                            <td>
                                            </td>
                                        </tr>
                                        <tr id="trOpenItemsButtonRow" runat="server">
                                            <td align="right">
                                                <webControls:ExButton ID="btnAdd" runat="server" LabelID="1560" SkinID="ExButton100" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField runat="server" ID="hdIsRefreshData" Value="0" />
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
<%--Input Form--%>
<telerik:RadWindow ID="rwRecItemInput" VisibleOnPageLoad="false" runat="server" OpenerElementID="<%btnAdd.ClientID %>"
    Modal="true" Width="850px" Height="400px" Top="50px">
</telerik:RadWindow>
<telerik:RadWindow ID="rwBulkClose" VisibleOnPageLoad="false" runat="server" OpenerElementID="<%btnClose.ClientID %>"
    Modal="true" Width="850px" Height="400px" Top="50px">
</telerik:RadWindow>

<script language="javascript" type="text/javascript">

    function ShowRecItemInput(queryString) {

        var oWnd = $find('<%=rwRecItemInput.ClientID%>');
        oWnd.setUrl('EditItemUnexplainedVariance.aspx?' + queryString);
        oWnd.show();
        return false;
    }
        
</script>

