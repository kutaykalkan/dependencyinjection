<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ScheduleIntervalDetails.ascx.cs"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_ScheduleIntervalDetails" %>
<asp:Panel ID="<%=this.ClientID%>">
    <asp:Panel ID="pnlHeader" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%" class="blueBorder">
            <tr class="blueRow">
                <td width="96%">
                    <webControls:ExLabel ID="lblIntervalDetails" runat="server" LabelID="2437" SkinID="Black11Arial" />
                </td>
                <td width="4%" align="right">
                    <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlContent" runat="server" CssClass="blueBorder">
        <telerikWebControls:ExRadGrid ID="rgScheduleIntervalDetails" runat="server" ShowHeader="true"
            AllowInster="false" InsertLabelID="1413" EntityNameLabelID="2270" InsertImageUrl="~/App_Themes/SkyStemBlueBrown/Images/Add_new_rule.gif"
            ClientSettings-Selecting-AllowRowSelect="true" AllowMultiRowSelection="true"
            ShowFooter="true" AllowSorting="true" SkinID="SkyStemBlueBrownWithoutBorder"
            OnItemDataBound="rgScheduleIntervalDetails_OnItemDataBound" OnPreRender="rgScheduleIntervalDetails_PreRender">
            <MasterTableView DataKeyNames="GLDataRecurringItemScheduleIntervalDetailID,ReconciliationPeriodID,SystemIntervalAmount"
                    ClientDataKeyNames="PeriodEndDate"
                ShowFooter="true">
                <Columns>
                    <%--Rec Period ID--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="RecPeriodID" Visible="false">
                        <ItemTemplate>
                            <webControls:ExLabel ID="lblRecPeriodID" runat="server"></webControls:ExLabel>
                        </ItemTemplate>
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Rec Period End Date--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="PeriodEndDate" LabelID="1978"
                        SortExpression="PeriodEndDate" DataType="System.DateTime">
                        <ItemTemplate>
                            <webControls:ExLabel ID="lblRecPeriodEndDate" runat="server"></webControls:ExLabel>
                            <asp:HiddenField ID="hdnRecPeriodCompareResult" runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <webControls:ExLabel ID="lblOverUnderAmount" FormatString="{0} : " runat="server"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblUnderOverAmountValue" runat="server"></webControls:ExLabel>
                        </FooterTemplate>
                    </telerikWebControls:ExGridTemplateColumn>
                    <%-- RecPeriod System Interval Amount--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="SystemIntervalAmount" SortExpression="SystemIntervalAmount"
                        DataType="System.Decimal" LabelID="2701">
                        <ItemTemplate>
                            <webControls:ExLabel ID="lblSystemIntervalAmount" runat="server"></webControls:ExLabel>
                        </ItemTemplate>
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--RecPeriod Schedule Amount--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="IntervalAmount" ItemStyle-Width="175px"
                        SortExpression="IntervalAmount" DataType="System.Decimal" LabelID="2429" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <webControls:ExTextBox ID="txtIntervalAmount" runat="server" />
                            <asp:HiddenField ID="hdnSystemIntervalAmount" runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <webControls:ExLabel ID="lblTotalAmount" LabelID="1656" FormatString="{0} : " runat="server"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblTotalAmountValue" runat="server"></webControls:ExLabel>
                        </FooterTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                    </telerikWebControls:ExGridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerikWebControls:ExRadGrid>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeInputRequirements" TargetControlID="pnlContent"
        ImageControlID="imgCollapse" CollapseControlID="imgCollapse" ExpandControlID="imgCollapse"
        runat="server" SkinID="CollapsiblePanel" Collapsed="true">
    </ajaxToolkit:CollapsiblePanelExtender>

    <script type="text/javascript" language="javascript">

        // Amount Total in Grid
        function GetGridTotal()
        {
            var grid = $find('<%=rgScheduleIntervalDetails.ClientID %>');
            var masterTable = grid.get_masterTableView();
            var rows = masterTable.get_dataItems();
            var cell;
            var amt = 0.0;
            var gridTotal = 0.0;
            for (var i = 0; i < rows.length; i++) 
            {    
                var row = rows[i];
                cell = masterTable.getCellByColumnUniqueName(row, "IntervalAmount");
                // First Children in Span and Further fisrt children in Text Box in ExTextBox Control
                if (cell.children[0].children[0] != null && cell.children[0].children[0] != 'undefined' && cell.children[0].children[0].value != '') 
                {   
                    amt = parseFloat(cell.children[0].children[0].value);
                    gridTotal += amt;
                }
            }
            return gridTotal;
        }

        // Amount that is consumed in Current Rec Period
        function GetCurrentConsumedAmount()
        {
            var grid = $find('<%=rgScheduleIntervalDetails.ClientID %>');
            var masterTable = grid.get_masterTableView();
            var rows = masterTable.get_dataItems();
            var cell;
            var recPeriodCell;
            var amt = 0.0;
            for (var i = 0; i < rows.length; i++) 
            {    
                var row = rows[i];
                cell = masterTable.getCellByColumnUniqueName(row, "IntervalAmount");
                recPeriodCell = masterTable.getCellByColumnUniqueName(row, "PeriodEndDate");
                // children[1] contains hidden field for rec period end date and current rec period end date comparison result
                // -1 : Rec Period End Date is Less than Current Rec Period End Date
                //  0 : Rec Period End Date is equal to Current Rec Period End Date
                //  1 : Rec Period End Date is greater than Current Rec Period End Date
                if(recPeriodCell.children[1] != null && recPeriodCell.children[1] != 'undefined')
                {
                    if(recPeriodCell.children[1].value == "0")
                    {
                        // First Children in Span and Further fisrt children in Text Box in ExTextBox Control
                        if (cell.children[0].children[0] != null && cell.children[0].children[0] != 'undefined' && cell.children[0].children[0].value != '') 
                        {   
                            amt = parseFloat(cell.children[0].children[0].value);
                        }
                    }
                }
            }
            return amt;
        }
        
        // Amount consumed upto the current rec period
        function GetTotalConsumedAmount()
        {
            var grid = $find('<%=rgScheduleIntervalDetails.ClientID %>');
            var masterTable = grid.get_masterTableView();
            var rows = masterTable.get_dataItems();
            var cell;
            var recPeriodCell;
            var amt = 0.0;
            for (var i = 0; i < rows.length; i++) 
            {    
                var row = rows[i];
                cell = masterTable.getCellByColumnUniqueName(row, "IntervalAmount");
                recPeriodCell = masterTable.getCellByColumnUniqueName(row, "PeriodEndDate");
                // children[1] contains hidden field for rec period end date and current rec period end date comparison result
                // -1 : Rec Period End Date is Less than Current Rec Period End Date
                //  0 : Rec Period End Date is equal to Current Rec Period End Date
                //  1 : Rec Period End Date is greater than Current Rec Period End Date
                if(recPeriodCell.children[1] != null && recPeriodCell.children[1] != 'undefined')
                {
                    if(recPeriodCell.children[1].value == "-1" || recPeriodCell.children[1].value == "0")
                    {
                        // First Children in Span and Further fisrt children in Text Box in ExTextBox Control
                        if (cell.children[0].children[0] != null && cell.children[0].children[0] != 'undefined' && cell.children[0].children[0].value != '') 
                        {   
                            amt+= parseFloat(cell.children[0].children[0].value);
                        }
                    }
                }
            }
            return amt;
        }
        
       function AmmountDiffIndicator(txtIntervalAmountClientID,hdnSystemIntervalAmountClientID)
       {
        var txtIntervalAmount = $get(txtIntervalAmountClientID);
        var hdnSystemIntervalAmount = $get(hdnSystemIntervalAmountClientID);
        var IntervalAmt = 0.0;
        var SystemIntervalAmt = 0.0;
         if (hdnSystemIntervalAmount != null && hdnSystemIntervalAmount != 'undefined' && hdnSystemIntervalAmount.value != '') 
            {   
             SystemIntervalAmt = parseFloat(hdnSystemIntervalAmount.value);
            }
        if (txtIntervalAmount != null && txtIntervalAmount != 'undefined' && txtIntervalAmount.value != '') 
            {   
             IntervalAmt = parseFloat(txtIntervalAmount.value);
            }
       
        if(SystemIntervalAmt != IntervalAmt)
            txtIntervalAmount.style.color = 'red';
        else
            txtIntervalAmount.style.color = 'black';
       }

        function UpdateTotals(originalAmt, recCategoryType, decimalPlaces, txtIntervalAmountClientID, lblTotalAmountClientID, lblUnderOverClientID,hdnSystemIntervalAmountClientID) {
            if (txtIntervalAmountClientID != undefined && lblTotalAmountClientID != undefined && lblUnderOverClientID != undefined) {
            AmmountDiffIndicator(txtIntervalAmountClientID,hdnSystemIntervalAmountClientID);
                var txtIntervalAmount = $get(txtIntervalAmountClientID);
                var lblTotalAmount = $get(lblTotalAmountClientID);
                var lblUnderOver = $get(lblUnderOverClientID);
                var gridTotal = GetGridTotal();
                lblTotalAmount.firstChild.data = GetDisplayDecimalValue(gridTotal, decimalPlaces);
                var absDiff = Math.abs(originalAmt) - Math.abs(gridTotal);
                var underOverValue = originalAmt - gridTotal;
                lblUnderOver.firstChild.data = GetDisplayDecimalValue(underOverValue, decimalPlaces);
                if(recCategoryType == <%= (short)SkyStem.ART.Web.Data.WebEnums.RecCategoryType.Amortizable_SupportingDetail_RecurringAmortizableSchedule %>)
                {
                    if (absDiff > 0)
                        lblUnderOver.firstChild.data = "("+lblUnderOver.firstChild.data+")"
                }
                if(recCategoryType == <%= (short)SkyStem.ART.Web.Data.WebEnums.RecCategoryType.Accrual_SupportingDetail_RecurringAccrualSchedule %>)
                {
                    if (absDiff < 0)
                        lblUnderOver.firstChild.data = "("+lblUnderOver.firstChild.data+")"
                }
                if('<%=_OnScheduleAmountChanged %>' != '')
                {
                    var args = new Object();
                    args.OriginalAmount = originalAmt;
                    args.CurrentConsumedAmount = GetCurrentConsumedAmount();
                    args.TotalConsumedAmount = GetTotalConsumedAmount();
                    args.DecimalPlacesExRate = <%= SkyStem.ART.Web.Data.TestConstant.DECIMAL_PLACES_FOR_EXCHANGE_RATE_ROUND %>
                    args.DecimalPlaces = <%= SkyStem.ART.Web.Data.TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND %>
                    <%=_OnScheduleAmountChanged %>(txtIntervalAmountClientID, args);
                } 
            }
        }
        
    </script>

</asp:Panel>
