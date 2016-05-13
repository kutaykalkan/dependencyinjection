<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemInputAmortizableTemplate.ascx.cs"
    Inherits="UserControls_ItemInputAmortizableTemplate" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<style type="text/css">
    </style>
<div id="divMainContent" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="2px">
            </td>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <col width="2px" />
                    <col width="82px" />
                    <col width="1300px" />
                    <col width="2px" />
                    <tr>
                        <td class="ExpandPanelTopLeft" height="16" align="left">
                            <asp:Image ID="Image1" SkinID="BorderTopLeft" runat="server" />
                        </td>
                        <td height="16" align="right">
                            <asp:Image ID="imgArrowTop" SkinID="ArrowTop" runat="server" />
                        </td>
                        <td class="ExpandPanelTopBorder" height="16" align="left">
                            <asp:Image ID="Image5" SkinID="BorderHorizontalTop" runat="server" />
                        </td>
                        <td class="ExpandPanelTopBorder" height="16">
                            <asp:Image ID="Image3" SkinID="BorderTopLeft" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ExpandPanelLeftBorder" align="left">
                            <asp:Image ID="Image2" SkinID="BorderVerticalLeft" runat="server" />
                        </td>
                        <td colspan="2" align="left">
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
                                                <telerikWebControls:ExRadGrid ID="rgAmortizable" runat="server" ShowHeader="true"
                                                    AllowInster="false" InsertLabelID="1413" InsertImageUrl="~/App_Themes/SkyStemBlueBrown/Images/Add_new_rule.gif"
                                                    OnItemCommand="rgAmortizable_ItemCommand" OnItemDataBound="rgAmortizable_ItemDataBound"
                                                    OnItemCreated="rgAmortizable_ItemCreated" OnNeedDataSource="rgAmortizable_NeedDataSource"
                                                    OnDeleteCommand="rgAmortizable_DeleteCommand" ClientSettings-Selecting-AllowRowSelect="true"
                                                    AllowMultiRowSelection="true" AllowSorting="true" SkinID="SkyStemBlueBrownWithoutBorder"
                                                    AllowExportToExcel="true" AllowExportToPDF="true">
                                                    <MasterTableView DataKeyNames="GLDataRecurringItemScheduleID" ShowFooter="true">
                                                        <Columns>
                                                            <%--Original Amount LCCY--%>
                                                            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" Visible="true"
                                                                HeaderStyle-Width="5%">
                                                            </telerikWebControls:ExGridClientSelectColumn>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="OriginalAmount" ItemStyle-Width="50px"
                                                                SortExpression="ScheduleAmount" DataType="System.Decimal" LabelID="1700">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblOriginalAmountLCCY" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <webControls:ExLabel ID="lblTotal" LabelID="1787" runat="server"></webControls:ExLabel>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Open Date--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="OpenDate" LabelID="1511 " SortExpression="OpenDate"
                                                                DataType="System.DateTime">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblOpenDate" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Schedule Name--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ScheduleName" LabelID="2052">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblScheduleName" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Schedule Begin Date--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ScheduleBeginDate" LabelID="2053"
                                                                SortExpression="ScheduleBeginDate" DataType="System.DateTime">
                                                                <ItemTemplate>
                                                                    <%# Helper.GetDisplayDate((DateTime?)Eval("ScheduleBeginDate"))%>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="false" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Schedule End Date--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ScheduleEndDate" LabelID="1450"
                                                                SortExpression="ScheduleEndDate" DataType="System.DateTime">
                                                                <ItemTemplate>
                                                                    <%# Helper.GetDisplayDate((DateTime?)Eval("ScheduleEndDate"))%>
                                                                </ItemTemplate>
                                                                <ItemStyle Wrap="false" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Original Amount RCCY--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="OriginalAmountRCCY" ItemStyle-Width="50px"
                                                                SortExpression="ScheduleAmountReportingCurrency" DataType="System.Decimal">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblOriginalAmountRCCY" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <div style="text-align: right;">
                                                                        <webControls:ExLabel ID="lblOriginalAmountRCCYTotalValue" runat="server"></webControls:ExLabel>
                                                                    </div>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Total Amortized Amount   UniqueName="RecPeriodAmountReportingCurrency" --%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="AmortizedAmountRCCY" SortExpression="RecPeriodAmountReportingCurrency"
                                                                DataType="System.Decimal" FooterStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblAmortizedAmountRCCY" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <div style="text-align: right;">
                                                                        <webControls:ExLabel ID="lblAmortizedAmountRCCYTotalValue" runat="server"></webControls:ExLabel>
                                                                    </div>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Remaining Amortizable Amount RCCY--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="BalanceReportingCurrency" LabelID="1701"
                                                                SortExpression="BalanceReportingCurrency">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblBalanceRCCY" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <div style="text-align: right;">
                                                                        <webControls:ExLabel ID="lblBalanceRCCYTotalValue" runat="server"></webControls:ExLabel>
                                                                    </div>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Documents--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Documents" LabelID="2056" SortExpression="AttachmentCount"
                                                                ItemStyle-Width="10px" DataType="System.Int32">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblAttachmentCount" runat="server"></webControls:ExLabel>
                                                                </ItemTemplate>
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
                                                                        LabelID="2028" SkinID="FileDownloadIcon" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--View/Edit Button Column--%>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ShowInputForm">
                                                                <ItemTemplate>
                                                                    <webControls:ExHyperLink ID="hlShowItemInputForm" runat="server" SkinID="ShowItemInputPopup"
                                                                        ImageAlign="AbsMiddle" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%--Delete Button Column--%>
                                                            <telerikWebControls:ExGridButtonColumn ConfirmDialogType="Classic" ConfirmTextLabelID="1781"
                                                                ConfirmTextFormatString="{0}?" ButtonCssClass="DeleteButton" ButtonType="ImageButton"
                                                                UniqueName="DeleteColumn" CommandName="Delete" ItemStyle-Width="10px">
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
                                                <%--<webControls:ExButton ID="btnCancel" runat="server" LabelID="1545" SkinID="ExButton100"
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
                                                                <telerikWebControls:ExRadGrid ID="rgGLAdjustmentCloseditems" runat="server" ShowHeader="true"
                                                                    OnItemDataBound="rgGLAdjustmentCloseditems_ItemDataBound" OnNeedDataSource="rgGLAdjustmentCloseditems_NeedDataSource"
                                                                    OnItemCommand="rgGLAdjustmentCloseditems_ItemCommand" OnItemCreated="rgGLAdjustmentCloseditems_ItemCreated"
                                                                    ClientSettings-Selecting-AllowRowSelect="true" AllowMultiRowSelection="true"
                                                                    AllowSorting="true" SkinID="SkyStemBlueBrownWithoutBorder" ShowFooter="true"
                                                                    AllowExportToExcel="true" AllowExportToPDF="true">
                                                                    <MasterTableView DataKeyNames="GLDataRecurringItemScheduleID" EditMode="InPlace"
                                                                        Width="800px">
                                                                        <Columns>
                                                                            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                                                            <%--Original Amount LCCY--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="OriginalAmount" ItemStyle-Width="50px"
                                                                                SortExpression="ScheduleAmount" DataType="System.Decimal" LabelID="1700">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblOriginalAmountLCCY" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <webControls:ExLabel ID="lblTotal" LabelID="1787" runat="server"></webControls:ExLabel>
                                                                                </FooterTemplate>
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                <FooterStyle HorizontalAlign="Right" />
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Open Date--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="OpenDate" LabelID="1511" SortExpression="OpenDate"
                                                                                DataType="System.DateTime">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblOpenDate" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Schedule Name--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ScheduleName" LabelID="2052">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblScheduleName" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Schedule Begin Date--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ScheduleBeginDate" LabelID="2053"
                                                                                SortExpression="ScheduleBeginDate" DataType="System.DateTime">
                                                                                <ItemTemplate>
                                                                                    <%# Helper.GetDisplayDate((DateTime?)Eval("ScheduleBeginDate"))%>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Wrap="false" />
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Schedule End Date--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ScheduleEndDate" LabelID="1450"
                                                                                SortExpression="ScheduleEndDate" DataType="System.DateTime">
                                                                                <ItemTemplate>
                                                                                    <%# Helper.GetDisplayDate((DateTime?)Eval("ScheduleEndDate"))%>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Wrap="false" />
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Close Date--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="CloseDate" LabelID="1411" SortExpression="CloseDate"
                                                                                DataType="System.DateTime">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblCloseDate" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Original Amount RCCY--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="OriginalAmountRCCY" ItemStyle-Width="50px"
                                                                                SortExpression="ScheduleAmount" DataType="System.Decimal">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblOriginalAmountRCCY" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <div style="text-align: right;">
                                                                                        <webControls:ExLabel ID="lblOriginalAmountRCCYTotalValue" runat="server"></webControls:ExLabel>
                                                                                    </div>
                                                                                </FooterTemplate>
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Total Amortized Amount--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="AmortizedAmountRCCY" SortExpression="RecPeriodAmountReportingCurrency"
                                                                                DataType="System.Decimal" FooterStyle-HorizontalAlign="Right">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblAmortizedAmountRCCY" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <div style="text-align: right;">
                                                                                        <webControls:ExLabel ID="lblAmortizedAmountRCCYTotalValue" runat="server"></webControls:ExLabel>
                                                                                    </div>
                                                                                </FooterTemplate>
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Remaining Amortizable Amount RCCY--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="BalanceReportingCurrency" LabelID="1701"
                                                                                SortExpression="BalanceReportingCurrency">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblBalanceRCCY" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <div style="text-align: right;">
                                                                                        <webControls:ExLabel ID="lblBalanceRCCYTotalValue" runat="server"></webControls:ExLabel>
                                                                                    </div>
                                                                                </FooterTemplate>
                                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Documents--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Documents" LabelID="2056" SortExpression="AttachmentCount"
                                                                                ItemStyle-Width="10px" DataType="System.Int32">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExLabel ID="lblAttachmentCount" runat="server"></webControls:ExLabel>
                                                                                </ItemTemplate>
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
                                                                                        LabelID="2028" SkinID="FileDownloadIcon" />
                                                                                </ItemTemplate>
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <%--Edit Button--%>
                                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ShowInputForm">
                                                                                <ItemTemplate>
                                                                                    <webControls:ExHyperLink ID="hlShowItemInputForm" runat="server" SkinID="ShowItemInputPopup"
                                                                                        ImageAlign="AbsMiddle" /><%--CommandName="ShowInputForm" CommandArgument='<%# Eval("GLReconciliationItemInputID") %>'--%>
                                                                                </ItemTemplate>
                                                                            </telerikWebControls:ExGridTemplateColumn>
                                                                            <telerik:GridBoundColumn UniqueName="GLDataRecurringItemScheduleID" Visible="false"
                                                                                DataField="GLDataRecurringItemScheduleID">
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
                                        <tr>
                                            <td>
                                                <div id="updateProgerssDiv">
                                                    <UserControls:ProgressBar ID="ucProgressBar" runat="server" EnableTheming="true"
                                                        AssociatedUpdatePanelID="updpnlMain" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
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
            oWnd.setUrl('EditItemAmortizable.aspx?' + queryString);
            oWnd.show();
            return false;
        }

        function ShowBulkClose(queryString) {

            var oWnd = $find('<%=rwBulkClose.ClientID%>');
            oWnd.setUrl('BulkCloseAmortizable.aspx?' + queryString);
            oWnd.show();
            return false;
        }        
        
    </script>

</div>
<asp:HiddenField runat="server" ID="hdIsRefreshData" Value="1" />
<asp:HiddenField runat="server" ID="hdIsExpanded" Value="0" />
