<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigureAndScheduleAlerts.aspx.cs"
    Inherits="Pages_ConfigureAndScheduleAlerts" MasterPageFile="~/MasterPages/ARTMasterPage.master"
    Theme="SkyStemBlueBrown" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <webControls:ExLabel ID="lblGeneralAlerts" runat="server" SkinID="BlueBold11ArialUnderline"
                    LabelID="1865"></webControls:ExLabel>
            </td>
            <tr>
                <td>
                    <asp:Panel ID="pnlGrid" runat="server">
                        <telerikWebControls:ExRadGrid ID="rgGeneralAlerts" runat="server" ShowHeader="true"
                            OnItemDataBound="rgGeneralAlerts_ItemDataBound" OnNeedDataSource="rgGeneralAlerts_NeedDataSource"
                            ClientSettings-Selecting-AllowRowSelect="true" AllowMultiRowSelection="true"
                            AllowPrint="true" AllowPrintAll="true" AllowSorting="true" ClientSettings-Selecting-EnableDragToSelectRows="false"
                            ClientSettings-ClientEvents-OnRowSelecting="Selecting" ClientSettings-ClientEvents-OnRowCreated="OnRowCreated"
                            ClientSettings-ClientEvents-OnRowDeselecting="Devalidate">
                            <MasterTableView DataKeyNames="AlertID, DefaultThresholdTypeID">
                                <Columns>
                                    <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                    <%--Alert--%>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="Alert" LabelID="1854" SortExpression="Alert">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblAlert" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="Condition" LabelID="1859" SortExpression="Condition"
                                        DataType="System.String">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblCondition" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <%--Alert Type--%>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="AlertType" LabelID="1690" SortExpression="AlertType"
                                        DataType="System.String">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblAlertType" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <%--Threshold--%>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="Threshold" LabelID="1860">
                                        <ItemTemplate>
                                            <table>
                                                <col width="10%" align="right" />
                                                <col width="90%" align="left" />
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <col width="97%" align="right" />
                                                            <col width="3%" align="left" />
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtThreshold" runat="server" Width="30"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <webControls:ExCustomValidator ID="vldThreshold" ControlToValidate="txtThreshold"
                                                                        runat="server" Enabled="false" Text="!" Font-Bold="true" Font-Size="Medium" ClientValidationFunction="validateForNonEmptyAndPositive" ValidateEmptyText="true" OnServerValidate="NonEmptyAndPositive_OnServerValidate"></webControls:ExCustomValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblPercentOrDays" runat="server"></webControls:ExLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <%--Recurrence--%>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="Recurrence" LabelID="1861">
                                        <ItemTemplate>
                                            <table>
                                                <col width="30%" align="right" />
                                                <col width="10%" align="center" />
                                                <col width="60%" align="left" />
                                                <tr>
                                                    <td>
                                                        <webControls:ExLabel ID="lblRecurrence" runat="server"></webControls:ExLabel>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <col width="97%" align="left" />
                                                            <col width="3%" align="left" />
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtRecurrence" runat="server" Width="30"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <webControls:ExCustomValidator ID="vldRecurrence" ControlToValidate="txtRecurrence"
                                                                        runat="server" Enabled="false" Text="!" Font-Bold="true" Font-Size="Medium" ClientValidationFunction="validateForNonEmptyAndPositive" ValidateEmptyText="true" OnServerValidate="NonEmptyAndPositive_OnServerValidate"></webControls:ExCustomValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblHours" runat="server"></webControls:ExLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerikWebControls:ExRadGrid>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <webControls:ExLabel ID="ExLabel1" runat="server" SkinID="BlueBold11ArialUnderline"
                        LabelID="1866"></webControls:ExLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlSystemAdminGrid" runat="server">
                        <telerikWebControls:ExRadGrid ID="rgSysAdminAlerts" runat="server" ShowHeader="true"
                            OnItemDataBound="rgSysAdminAlerts_ItemDataBound" OnNeedDataSource="rgSysAdminAlerts_NeedDataSource"
                            ClientSettings-Selecting-AllowRowSelect="true" AllowMultiRowSelection="true"
                            AllowPrint="true" AllowPrintAll="true" AllowSorting="true" ClientSettings-Selecting-EnableDragToSelectRows="false"
                            ClientSettings-ClientEvents-OnRowSelecting="SelectingSysAdminAlert" ClientSettings-ClientEvents-OnRowCreated="OnRowCreated"
                            ClientSettings-ClientEvents-OnRowDeselecting="DevalidateSysAdmin">
                            <MasterTableView DataKeyNames="AlertID">
                                <Columns>
                                    <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                    <%--Alert--%>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="Alert" LabelID="1854" SortExpression="Alert">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblAlert" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="Condition" LabelID="1859" SortExpression="Condition"
                                        DataType="System.String">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblCondition" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <%--Alert Type--%>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="AlertType" LabelID="1690" SortExpression="AlertType"
                                        DataType="System.String">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblAlertType" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <%--Threshold--%>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="Threshold" LabelID="1860">
                                        <ItemTemplate>
                                            <table>
                                                <col width="10%" align="right" />
                                                <col width="90%" align="left" />
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <col width="97%" align="right" />
                                                            <col width="3%" align="left" />
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtThreshold" runat="server" Width="30"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <webControls:ExCustomValidator ID="vldThreshold" ControlToValidate="txtThreshold"
                                                                        runat="server" Enabled="false" Text="!" Font-Bold="true" Font-Size="Medium" ClientValidationFunction="validateForNonEmptyAndPositive" ValidateEmptyText="true" OnServerValidate="NonEmptyAndPositive_OnServerValidate"></webControls:ExCustomValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblPercentOrDays" runat="server"></webControls:ExLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <%--Recurrence--%>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="Recurrence" LabelID="1861">
                                        <ItemTemplate>
                                            <table>
                                                <col width="30%" align="right" />
                                                <col width="10%" align="center" />
                                                <col width="60%" align="left" />
                                                <tr>
                                                    <td>
                                                        <webControls:ExLabel ID="lblRecurrence" runat="server"></webControls:ExLabel>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <col width="97%" align="left" />
                                                            <col width="3%" align="left" />
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtRecurrence" runat="server" Width="30"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <webControls:ExCustomValidator ID="vldRecurrence" ControlToValidate="txtRecurrence"
                                                                        runat="server" Enabled="false" Text="!" Font-Bold="true" Font-Size="Medium" ClientValidationFunction="validateForNonEmptyAndPositive" ValidateEmptyText="true" OnServerValidate="NonEmptyAndPositive_OnServerValidate"></webControls:ExCustomValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblHours" runat="server"></webControls:ExLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerikWebControls:ExRadGrid>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <webControls:ExLabel ID="lblOtherAlerts" runat="server" SkinID="BlueBold11ArialUnderline"
                        LabelID="2509"></webControls:ExLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlOtherAlerts" runat="server">
                        <telerikWebControls:ExRadGrid ID="rgOtherAlerts" runat="server" ShowHeader="true"
                            OnItemDataBound="rgOtherAlerts_ItemDataBound" OnNeedDataSource="rgOtherAlerts_NeedDataSource"
                            ClientSettings-Selecting-AllowRowSelect="true" AllowMultiRowSelection="true"
                            AllowPrint="true" AllowPrintAll="true" AllowSorting="true" ClientSettings-Selecting-EnableDragToSelectRows="false"
                            ClientSettings-ClientEvents-OnRowSelecting="SelectingSysAdminAlert" ClientSettings-ClientEvents-OnRowCreated="OnRowCreated"
                            ClientSettings-ClientEvents-OnRowDeselecting="DevalidateSysAdmin">
                            <MasterTableView DataKeyNames="AlertID">
                                <Columns>
                                    <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                    <%--Alert--%>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="Alert" LabelID="1854" SortExpression="Alert">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblAlert" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="Condition" LabelID="1859" SortExpression="Condition"
                                        DataType="System.String">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblCondition" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <%--Alert Type--%>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="AlertType" LabelID="1690" SortExpression="AlertType"
                                        DataType="System.String">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblAlertType" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <%--Threshold--%>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="Threshold" LabelID="1860">
                                        <ItemTemplate>
                                            <table>
                                                <col width="10%" align="right" />
                                                <col width="90%" align="left" />
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <col width="97%" align="right" />
                                                            <col width="3%" align="left" />
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtThreshold" runat="server" Width="30"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <webControls:ExCustomValidator ID="vldThreshold" ControlToValidate="txtThreshold"
                                                                        runat="server" Enabled="false" Text="!" Font-Bold="true" Font-Size="Medium" ClientValidationFunction="validateForNonEmptyAndPositive" ValidateEmptyText="true" OnServerValidate="NonEmptyAndPositive_OnServerValidate"></webControls:ExCustomValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblPercentOrDays" runat="server"></webControls:ExLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <%--Recurrence--%>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="Recurrence" LabelID="1861">
                                        <ItemTemplate>
                                            <table>
                                                <col width="30%" align="right" />
                                                <col width="10%" align="center" />
                                                <col width="60%" align="left" />
                                                <tr>
                                                    <td>
                                                        <webControls:ExLabel ID="lblRecurrence" runat="server"></webControls:ExLabel>
                                                    </td>
                                                    <td>
                                                        <table>
                                                            <col width="97%" align="left" />
                                                            <col width="3%" align="left" />
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtRecurrence" runat="server" Width="30"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <webControls:ExCustomValidator ID="vldRecurrence" ControlToValidate="txtRecurrence"
                                                                        runat="server" Enabled="false" Text="!" Font-Bold="true" Font-Size="Medium" ClientValidationFunction="validateForNonEmptyAndPositive" ValidateEmptyText="true" OnServerValidate="NonEmptyAndPositive_OnServerValidate"></webControls:ExCustomValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblHours" runat="server"></webControls:ExLabel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerikWebControls:ExRadGrid>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <webControls:ExButton ID="btnSave" runat="server" LabelID="1315" OnClick="btnSave_Click" />&nbsp;
                    <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClick="btnCancel_Click"
                        CausesValidation="false" OnClientClick="return HideValidationSummary()" />&nbsp;
                </td>
            </tr>
    </table>
    <input type="text" id="DeselectGeneralAlerts" runat="server" style="display: none" />
    <input type="text" id="DeselectSysAdminAlerts" runat="server" style="display: none" />
    <input type="text" id="DeselectOtherAlerts" runat="server" style="display: none" />

    <script type="text/javascript" language="javascript">
        //        $("div.pnlSystemAdminGrid :input").attr("disabled", "disabled");
        //        $("div.pnlSystemAdminGrid :input").removeAttr("disabled"); 
        function Selecting(sender, args) {
            var i = 0;
            var inp = document.getElementById('<% =this.DeselectGeneralAlerts.ClientID %>');
            var data = inp.value;
            if (data != "") {
                var rowsData = data.split(":");
                while (typeof (rowsData[i]) != "undefined") {
                    if (rowsData[i++] == args.get_itemIndexHierarchical()) {
                        args.set_cancel(true);
                    }
                }
            }


            var gridDataItems = args.get_gridDataItem();
            var elements = gridDataItems.get_element();
            var spanItems = elements.getElementsByTagName('SPAN');

            if (spanItems != null && spanItems != undefined && spanItems.length == 8) {
                var vldThreshold = document.getElementById(spanItems[3].id);
                var vldRecurrence = document.getElementById(spanItems[6].id);

                ValidatorEnable(vldThreshold, true);
                ValidatorEnable(vldRecurrence, true);
            }
            else if (spanItems != null && spanItems != undefined && spanItems.length == 6) {
                var vldRecurrence = document.getElementById(spanItems[4].id);

                ValidatorEnable(vldRecurrence, true);
            }
            else if (spanItems != null && spanItems != undefined && spanItems.length == 5) {
                var vldThreshold = document.getElementById(spanItems[3].id);

                ValidatorEnable(vldThreshold, true);
            }
        }

        function Devalidate(sender, args) {
            var gridDataItems = args.get_gridDataItem();
            var elements = gridDataItems.get_element();
            var spanItems = elements.getElementsByTagName('SPAN');

            if (spanItems != null && spanItems != undefined && spanItems.length == 8) {
                var vldThreshold = document.getElementById(spanItems[3].id);
                var vldRecurrence = document.getElementById(spanItems[6].id);

                ValidatorEnable(vldThreshold, false);
                ValidatorEnable(vldRecurrence, false);
            }
            else if (spanItems != null && spanItems != undefined && spanItems.length == 6) {
                var vldRecurrence = document.getElementById(spanItems[4].id);

                ValidatorEnable(vldRecurrence, false);
            }
            else if (spanItems != null && spanItems != undefined && spanItems.length == 5) {
                var vldThreshold = document.getElementById(spanItems[3].id);

                ValidatorEnable(vldThreshold, false);
            }
        }


        function SelectingSysAdminAlert(sender, args) {
            var i = 0;
            var inpSysAlert = document.getElementById('<% =this.DeselectSysAdminAlerts.ClientID %>');
            var dataSysAlert = inpSysAlert.value;
            if (dataSysAlert != "") {
                var rowsDataSysAlert = dataSysAlert.split(":");
                while (typeof (rowsDataSysAlert[i]) != "undefined") {
                    if (rowsDataSysAlert[i++] == args.get_itemIndexHierarchical()) {
                        args.set_cancel(true);
                    }
                }
            }

            var gridDataItems = args.get_gridDataItem();
            var elements = gridDataItems.get_element();
            var spanItems = elements.getElementsByTagName('SPAN');

            if (spanItems != null && spanItems != undefined && spanItems.length == 8) {
                var vldThreshold = document.getElementById(spanItems[3].id);
                var vldRecurrence = document.getElementById(spanItems[6].id);

                ValidatorEnable(vldThreshold, true);
                ValidatorEnable(vldRecurrence, true);
            }
            else if (spanItems != null && spanItems != undefined && spanItems.length == 6) {
                var vldRecurrence = document.getElementById(spanItems[4].id);

                ValidatorEnable(vldRecurrence, true);
            }
            else if (spanItems != null && spanItems != undefined && spanItems.length == 5) {
                var vldThreshold = document.getElementById(spanItems[3].id);

                ValidatorEnable(vldThreshold, true);
            }
        }

        function DevalidateSysAdmin(sender, args) {
            var gridDataItems = args.get_gridDataItem();
            var elements = gridDataItems.get_element();
            var spanItems = elements.getElementsByTagName('SPAN');

            if (spanItems != null && spanItems != undefined && spanItems.length == 8) {
                var vldThreshold = document.getElementById(spanItems[3].id);
                var vldRecurrence = document.getElementById(spanItems[6].id);

                ValidatorEnable(vldThreshold, false);
                ValidatorEnable(vldRecurrence, false);
            }
            else if (spanItems != null && spanItems != undefined && spanItems.length == 6) {
                var vldRecurrence = document.getElementById(spanItems[4].id);

                ValidatorEnable(vldRecurrence, false);
            }
            else if (spanItems != null && spanItems != undefined && spanItems.length == 5) {
                var vldThreshold = document.getElementById(spanItems[3].id);

                ValidatorEnable(vldThreshold, false);
            }
        }
        
        function validateForNonEmptyAndPositive(sender, args) {
            var ValidationText = args.Value;
            if(ValidationText.match('^[1-9]+[0-9]*$'))
             args.IsValid = true;
            else
                args.IsValid = false;
            }
    </script>

</asp:Content>
