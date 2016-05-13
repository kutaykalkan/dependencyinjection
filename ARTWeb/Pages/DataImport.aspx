<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataImport.aspx.cs" Inherits="Pages_DataImport"
    MasterPageFile="~/MasterPages/ARTMasterPage.master" Theme="SkyStemBlueBrown" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Import Namespace="SkyStem.Language.LanguageUtility" %>
<%@ Import Namespace="SkyStem.ART.Client.Data" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="__content" runat="server">
    <style id="Style1" type="text/css" runat="server">
        .radupload {
            float: left;
            margin-bottom: 20px;
        }

        .bigModule {
            clear: both;
        }

        .smallModule {
            margin-bottom: 20px;
        }

        #controlContainer {
            vertical-align: top;
            padding: 20px 10px;
        }

        .ruProgressArea {
            position: absolute;
            top: 0;
            left: 10px;
        }

        input.RadUploadSubmit {
            margin-top: 20px;
        }

        .rc3 {
            padding-left: 40px !important;
        }

            .rc3 .title {
                margin-left: -20px;
            }
    </style>
    <asp:UpdatePanel ID="upnlDataImport" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <%-- code starts from here--%>
                <tr class="BlankRow">
                    <td></td>
                </tr>
                <tr>
                    <td style="padding-left: 15px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="ManadatoryField">*
                                </td>
                                <td>
                                    <webControls:ExLabel ID="lblImportType" runat="server" SkinID="Black11Arial" LabelID="1307"
                                        FormatString="{0}:"></webControls:ExLabel>
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlImportType" runat="server" SkinID="DropDownList200" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlImportType_SelectedIndexChanged" onchange="HideValidationSummary();">
                                    </asp:DropDownList>
                                    <webControls:ExRequiredFieldValidator ID="rfvImportType" runat="server" ControlToValidate="ddlImportType"
                                        Display="Static">!</webControls:ExRequiredFieldValidator>                                    
                                </td>
                            </tr>
                            <tr class="BlankRow">
                                <td></td>
                            </tr>
                            <tr id="trDataImportTemaplate" runat="server">
                                <td class="ManadatoryField">*
                                </td>
                                <td>
                                    <webControls:ExLabel ID="lblDataImportTemaplate" runat="server" SkinID="Black11Arial" LabelID="2870"
                                        FormatString="{0}:"></webControls:ExLabel>
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlDataimportTemplate" runat="server" SkinID="DropDownList200">
                                    </asp:DropDownList>
                                    <webControls:ExRequiredFieldValidator ID="rfvDataimportTemplate" runat="server" ControlToValidate="ddlDataimportTemplate"
                                        Display="Static">!</webControls:ExRequiredFieldValidator>                                  
                                </td>
                            </tr>
                            <tr class="BlankRow">
                                <td></td>
                            </tr>
                            <tr>
                                <td class="ManadatoryField">*
                                </td>
                                <td>
                                    <webControls:ExLabel ID="lblProfileName" runat="server" SkinID="Black11Arial" LabelID="1308"
                                        FormatString="{0}:" />
                                </td>
                                <td colspan="2">
                                    <webControls:ExTextBox ID="txtProfileName" runat="server" SkinID="ExTextBox200" IsRequired="true"
                                        EnableViewState="true" MaxLength="50" />
                                </td>
                            </tr>
                            <tr class="BlankRow">
                                <td></td>
                            </tr>
                            <tr>
                                <td class="ManadatoryField">*
                                </td>
                                <td>
                                    <webControls:ExLabel ID="lblFilelocation" runat="server" SkinID="Black11Arial" LabelID="1309"
                                        FormatString="{0}:" />
                                </td>
                                <td colspan="2" style="white-space: nowrap;">
                                    <%--<asp:FileUpload ID="fileUpload" runat="server" SkinID="FileUpload200" />--%>
                                    <%--<input type="file" runat="server" id="inputFile" />--%>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <colgroup>
                                            <col width="80%" />
                                            <col width="20%" />
                                            <tr>
                                                <td>
                                                    <%--<telerik:RadProgressManager ID="Radprogressmanager1" runat="server" />--%>
                                                    <telerikWebControls:ExRadUpload LabelID="2494" ID="RadFileUpload" runat="server" ControlObjectsVisibility="None"
                                                        InitialFileInputsCount="1" InputSize="50" MaxFileInputsCount="1" />
                                                    <%--<telerik:RadProgressArea ID="RadProgressArea1" runat="server" />--%>
                                                </td>
                                                <td>
                                                    <asp:CustomValidator ID="cvFileUpload" runat="server" ClientValidationFunction="ValidateFileExtension"
                                                        Display="Dynamic" ErrorMessage="" OnServerValidate="cvFileUpload_ServerValidate"
                                                        Text="!">
                                                    </asp:CustomValidator>
                                                </td>
                                            </tr>
                                        </colgroup>
                                    </table>
                                    <%--ValidationGroup="SaveFileToServer"--%>
                                </td>
                            </tr>
                            <tr class="BlankRow">
                                <td></td>
                            </tr>
                            <asp:Panel ID="pnlLanguageSelection" runat="server" Visible="false">
                                <tr>
                                    <td class="ManadatoryField">*
                                    </td>
                                    <td>
                                        <webControls:ExLabel ID="lblFromLanguage" runat="server" SkinID="Black11Arial" LabelID="2476"
                                            FormatString="{0}:" />
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlFromLanguage" runat="server" SkinID="DropDownList200" />
                                        <webControls:ExRequiredFieldValidator ID="rfvFromLanguage" runat="server" ControlToValidate="ddlFromLanguage"
                                            Display="Static">!</webControls:ExRequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr class="BlankRow">
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="ManadatoryField">*
                                    </td>
                                    <td>
                                        <webControls:ExLabel ID="lblToLanguage" runat="server" SkinID="Black11Arial" LabelID="2477"
                                            FormatString="{0}:" />
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="ddlToLanguage" runat="server" SkinID="DropDownList200" />
                                        <webControls:ExRequiredFieldValidator ID="rfvToLanguage" runat="server" ControlToValidate="ddlToLanguage"
                                            Display="Static">!</webControls:ExRequiredFieldValidator>
                                        <webControls:ExCompareValidator ID="cmpvToLanguage" LabelID="2480" runat="server"
                                            ControlToCompare="ddlFromLanguage" ControlToValidate="ddlToLanguage" Operator="NotEqual"
                                            Display="Static">!</webControls:ExCompareValidator>
                                    </td>
                                </tr>
                                <tr class="BlankRow">
                                    <td></td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td></td>
                                <td colspan="3">
                                    <webControls:ExLabel ID="lblNotifications" LabelID="1310" SkinID="SubSectionHeading"
                                        runat="server"></webControls:ExLabel>
                                </td>
                            </tr>
                            <tr class="BlankRow">
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <webControls:ExLabel ID="lblEMail" runat="server" SkinID="Black11Arial" LabelID="1311" />
                                </td>
                                <td>
                                    <webControls:ExLabel ID="lblNotifyUser" runat="server" SkinID="Black11Arial" LabelID="1312" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <webControls:ExLabel ID="lblSucess" runat="server" SkinID="Black11Arial" LabelID="1314"
                                        FormatString="{0}:" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmailSucess" runat="server" SkinID="TextBox200" />
                                    <%--<asp:RegularExpressionValidator ID="check" runat="server" ControlToValidate="txtEmailSucess" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*" ErrorMessage="invalid email">!</asp:RegularExpressionValidator>--%>
                                    <webControls:ExRegularExpressionValidator ID="revEmailId" runat="server" ControlToValidate="txtEmailSucess"
                                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*"
                                        LabelID="1751">!</webControls:ExRegularExpressionValidator>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUserNameSucess" runat="server" SkinID="TextBox150" />
                                    &nbsp;
                                    <webControls:ExImageButton ID="imgUserNameSucess" runat="server" SkinID="AddUser"
                                        LabelID="1607" />
                                </td>
                            </tr>
                            <tr class="BlankRow">
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td style="width: 20%">
                                    <webControls:ExLabel ID="lblFailure" runat="server" SkinID="Black11Arial" LabelID="1313"
                                        FormatString="{0}:" />
                                </td>
                                <td style="width: 40%">
                                    <asp:TextBox ID="txtEmailFailure" runat="server" SkinID="TextBox200" />
                                    <webControls:ExRegularExpressionValidator ID="revEmailIdFailure" runat="server" ControlToValidate="txtEmailFailure"
                                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*"
                                        LabelID="1751">!</webControls:ExRegularExpressionValidator>
                                </td>
                                <td style="width: 40%">
                                    <asp:TextBox ID="txtUserNameFailure" runat="server" SkinID="TextBox150" />
                                    &nbsp;
                                    <webControls:ExImageButton ID="imgUserNameFailure" runat="server" SkinID="AddUser"
                                        LabelID="1607" />
                                </td>
                            </tr>
                            <tr class="BlankRow">
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="3" align="right">
                                    <webControls:ExButton ID="btnUpload" runat="server" OnClick="btnUpload_Click" LabelID="1478"
                                        SkinID="ExButton100" CausesValidation="true" />
                                    <%--ValidationGroup="SaveFileToServer" --%>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 2%">&nbsp;
                    </td>
                    <td valign="top" style="width: 40%">
                        <table cellpadding="0" cellspacing="0" width="100%" class="DataImportRulesTable">
                            <tr class="PanelTitleBrown">
                                <td>
                                    <webControls:ExLabel ID="lblImportRules1" runat="server" SkinID="BlueBold11Arial"
                                        LabelID="1479" />
                                </td>
                                <td align="right" style="margin-right: 2px">
                                    <webControls:ExHyperLink ID="hlOpenExcelFile" runat="server" LabelID="1697" CssClass="BlueBold11Arial"></webControls:ExHyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Panel ID="pnlRules" runat="server" ScrollBars="Vertical" CssClass="RulesPanel">
                                        <webControls:ExLabel ID="lblRules" runat="server"></webControls:ExLabel>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td></td>
                </tr>
                <tr class="BlankRow">
                    <td></td>
                </tr>
                <tr id="rowDataImportGrid" runat="server">
                    <td colspan="3">
                        <asp:Panel ID="pnlHolidayCal" runat="server">
                            <telerikWebControls:ExRadGrid ID="rgHolidayCal" runat="server" EntityNameLabelID="1229"
                                AllowPaging="false" AllowMultiRowSelection="true" AllowExportToExcel="false"
                                AllowExportToPDF="false" AllowPrint="false" AllowPrintAll="false" OnItemDataBound="rgHolidayCal_ItemDataBound">
                                <MasterTableView CommandItemDisplay="None">
                                    <Columns>
                                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="HolidayDate" LabelID="1399"
                                            SortExpression="Date" DataField="Date">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblDate" runat="server" Text='<%# Helper.GetDisplayDate ((DateTime?)Eval("Date"))%>'
                                                    SkinID="Black11Arial" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="HolidayName" LabelID="1480"
                                            DataField="Holiday Name">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblHolidayName" runat="server" Text='<%# Eval("Holiday Name") %>'
                                                    SkinID="Black11Arial" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                    <ClientEvents OnRowSelecting="Selecting" />
                                </ClientSettings>
                            </telerikWebControls:ExRadGrid>
                        </asp:Panel>
                        <asp:Panel ID="pnlPeriodEndDate" runat="server">
                            <telerikWebControls:ExRadGrid ID="rgPeriodEndDate" runat="server" EntityNameLabelID="1229"
                                AllowPaging="false" AllowMultiRowSelection="true" AllowExportToExcel="false"
                                AllowExportToPDF="false" AllowPrint="true" AllowPrintAll="true" OnItemDataBound="rgPeriodEndDate_ItemDataBound">
                                <MasterTableView CommandItemDisplay="None">
                                    <Columns>
                                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1626" UniqueName="PeriodNumber"
                                            HeaderStyle-Width="35%" SortExpression="Period #" DataField="Period #">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblReconciliationPeriodNumber" runat="server" Text='<%# Eval("Period #")%>' />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1057" HeaderStyle-Width="35%" SortExpression="Period end date"
                                            DataField="Period end date">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblPeriodEndDate" runat="server" Text='<%# Helper.GetDisplayDate((DateTime?)Eval("Period end date"))%>' />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                    <ClientEvents OnRowSelecting="Selecting" />
                                </ClientSettings>
                            </telerikWebControls:ExRadGrid>
                        </asp:Panel>
                        <asp:Panel ID="pnlCurrency" runat="server">
                            <telerikWebControls:ExRadGrid ID="rgCurrency" runat="server" EntityNameLabelID="1229"
                                AllowPaging="false" AllowMultiRowSelection="true" AllowExportToExcel="false"
                                AllowExportToPDF="false" AllowPrint="true" AllowPrintAll="true" OnItemDataBound="rgCurrency_ItemDataBound">
                                <MasterTableView CommandItemDisplay="None">
                                    <Columns>
                                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" HeaderStyle-Width="5%" />
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1248" HeaderStyle-Width="15%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblPeriod" runat="server" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1486" HeaderStyle-Width="20%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblFromCurrency" runat="server" Text='<%# Eval("From Currency Code")%>' />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1487" HeaderStyle-Width="20%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblToCurrency" runat="server" Text='<%# Eval("To Currency Code")%>' />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1488" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblRate" runat="server" Text='<%# Eval("Rate")%>' />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1051" HeaderStyle-Width="25%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblCurrencyError" runat="server" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerikWebControls:ExRadGrid>
                        </asp:Panel>
                        <asp:Panel ID="pnlSubLedger" runat="server">
                            <telerikWebControls:ExRadGrid ID="rgSubLedger" runat="server" EntityNameLabelID="1229"
                                AllowPaging="false" AllowMultiRowSelection="true" AllowExportToExcel="false"
                                AllowExportToPDF="false" AllowPrint="true" AllowPrintAll="true" OnItemDataBound="rgSubLedger_ItemDataBound">
                                <MasterTableView CommandItemDisplay="None">
                                    <Columns>
                                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1632 ">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblSubledgerSourceName" runat="server" Text='<%# Eval("Subledger Source Name")%>' />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1051">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblSubledgerSourceError" runat="server" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                    <ClientEvents OnRowSelecting="Selecting" />
                                </ClientSettings>
                            </telerikWebControls:ExRadGrid>
                        </asp:Panel>
                        <asp:Panel ID="pnlRecControlChecklist" runat="server">
                            <telerikWebControls:ExRadGrid ID="rgRecControlChecklist" runat="server" EntityNameLabelID="2827"
                                AllowPaging="false" AllowMultiRowSelection="true" AllowExportToExcel="false"
                                AllowExportToPDF="false" AllowPrint="true" AllowPrintAll="true" OnItemDataBound="rgRecControlChecklist_ItemDataBound">
                                <MasterTableView CommandItemDisplay="None">
                                    <Columns>
                                        <telerikWebControls:ExGridClientSelectColumn HeaderStyle-Width="10px" UniqueName="CheckboxSelectColumn" />
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1408">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblDescription" runat="server" Text='<%# Eval("Description")%>' />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <%--                                        <telerikWebControls:ExGridTemplateColumn LabelID="1051">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblRecControlChecklistError" runat="server" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>--%>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                    <ClientEvents OnRowSelecting="Selecting" />
                                </ClientSettings>
                            </telerikWebControls:ExRadGrid>
                        </asp:Panel>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td></td>
                </tr>
                <tr id="trButtons" runat="server" visible="false">
                    <td align="right" colspan="3">
                        <webControls:ExButton ID="btnImport" runat="server" LabelID="1410" SkinID="ExButton100"
                            OnClick="btnImport_Click" OnClientClick="return ValidateImport()" CausesValidation="false" />
                        <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" SkinID="ExButton100"
                            OnClick="btnCancel_Click" CausesValidation="false" />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <UserControls:ProgressBar ID="ucDataImport" runat="server" EnableTheming="true" AssociatedUpdatePanelID="upnlDataImport"
                            Visible="true" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="Sel" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<telerik:RadWindow ID="RadWindowUserlstOnSucess" VisibleOnPageLoad="false" runat="server"  OpenerElementID='<%ExImage1.ClientID %>' Modal="true" Width="850px" Height="400px" Top="50px"  OnClientclose="OnClientclose" VisibleStatusbar="false">
    </telerik:RadWindow> 
     <telerik:RadWindow ID="RadWindowUserlstOnFailure" VisibleOnPageLoad="false" runat="server"  OpenerElementID='<%ExImage1.ClientID %>' Modal="true" Width="850px" Height="400px" Top="50px"  OnClientclose="OnClientcloseFailure">
    </telerik:RadWindow>--%>

    <script type="text/javascript">
        var a = Array;
        window.onload = function () {
            var inp = document.getElementById('<% =this.Sel.ClientID %>');
            var data = inp.value;
            if (data != "") {
                var rowsData = data.split(":");
                var i = 0;
                while (typeof (rowsData[i]) != "undefined") {
                    if (rowsData[i] != "") {
                        a[i] = rowsData[i];
                    }
                    i++;
                }
            }
        }



        function ClientCallBackFunction(radWindow, returnValue) {
            //check if a value is returned from the dialog

            if (returnValue.newtext) {

                document.getElementById("txtEmailSucess").value = returnValue.newtext;

            }
        }
        function OnClientclose(sender, eventArgs) {
            var arg = eventArgs.get_argument();          
            if (arg) {

                if (arg.newtext != null)
                    document.getElementById('<% =this.txtUserNameSucess.ClientID %>').value = arg.newtext;
            }
        }

        function OnClientcloseFailure(sender, eventArgs) {
            var arg = eventArgs.get_argument();

            if (arg) {
                if (arg.newtext != null)
                    document.getElementById('<% =this.txtUserNameFailure.ClientID %>').value = arg.newtext;
            }
        }


        function Selecting(sender, args) {
            var i = 0;
            while (typeof (a[i]) != "undefined") {
                if (a[i++] == args.get_itemIndexHierarchical()) {
                    args.set_cancel(true);
                }
            }
        }
    </script>

    <script type="text/javascript">
        var holidayCalendar = '<% = (short)ARTEnums.DataImportType.HolidayCalendar %>';
        var periodEndDate = '<% = (short)ARTEnums.DataImportType.PeriodEndDates %>';
        var subledgerSource = '<% = (short)ARTEnums.DataImportType.SubledgerSource %>';
        var currencyExchangeRate = '<% = (short)ARTEnums.DataImportType.CurrencyExchangeRateData %>';
        var glData = '<% = (short)ARTEnums.DataImportType.GLData %>';
        var subledgerLoad = '<% = (short)ARTEnums.DataImportType.SubledgerData %>';
        var accountAttribute = '<% = (short)ARTEnums.DataImportType.AccountAttributeList %>';
        var recControlChecklist = '<% = (short)ARTEnums.DataImportType.RecControlChecklist%>';

        function ddlChange() {

            var ddl = $get('<%= ddlImportType.ClientID %>');
            if (ddl != null) {
                var currentSelection = ddl.options[ddl.selectedIndex].value;
                var hyperLink = $get("<%= hlOpenExcelFile.ClientID %>");
                if (hyperLink != null) {
                    switch (currentSelection) {
                        case holidayCalendar:
                            hyperLink.href = "../Templates/HolidayUpload.xlsx";
                            break;
                        case periodEndDate:
                            hyperLink.href = "../Templates/PeriodEndDates.xlsx";
                            break;
                        case subledgerSource:
                            hyperLink.href = "../Templates/SubledgerSource.xlsx";
                            break;
                        case currencyExchangeRate:
                            hyperLink.href = "../Templates/CurrencyExchange.xlsx";
                            break;
                        case glData:
                            hyperLink.href = "../Templates/GLDataUpload.xlsx";
                            break;
                        case subledgerLoad:
                            hyperLink.href = "../Templates/Subledger.xlsx";
                            break;
                        case accountAttribute:
                            hyperLink.href = "../Templates/AccountsUpload.xlsx";
                            break;
                        case recControlChecklist:
                            hyperLink.href = "../Templates/RecControlChecklist.xlsx";
                            break;
                    }
                }
            }

        }
        function ValidateFileExtension(source, arguments) {
            var flag = 0;
            var uploadControl = getRadUpload('<%= RadFileUpload.ClientID %>');
            if (uploadControl != null) {
                var fileInputs = uploadControl.getFileInputs();
                if (fileInputs[0].value == "") {
                    //source.errormessage = "Source file location cannot be empty.";
                    source.errormessage = '<%= Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField,1309) %>';
                    arguments.IsValid = false;
                }
                if (!uploadControl.validateExtensions()) {
                    //source.errormessage = "Invalid file extension.";
                    source.errormessage = '<%= LanguageUtil.GetValue(5000036) %>';
                    arguments.IsValid = false;
                }
            }
        }
        function ValidateImport() {
            var grid;
            var ddl = $get('<%= ddlImportType.ClientID %>');
            if (ddl != null) {
                var currentSelection = ddl.options[ddl.selectedIndex].value;
                switch (currentSelection) {
                    case holidayCalendar:
                        grid = $find("<%= rgHolidayCal.ClientID %>");
                        break;
                    case periodEndDate:
                        grid = $find("<%= rgPeriodEndDate.ClientID %>");
                        break;
                    case subledgerSource:
                        grid = $find("<%= rgSubLedger.ClientID %>");
                        break;
                    case currencyExchangeRate:
                        grid = $find("<%= rgCurrency.ClientID %>");
                        break;
                    case recControlChecklist:
                        grid = $find("<%= rgRecControlChecklist.ClientID %>");
                        break;
                }
                if (grid != undefined && grid != null) {
                    var gridSelectedItems = grid.get_selectedItems();
                    if (gridSelectedItems.length <= 0) {
                        alert('<% = Helper.GetAlertMessageFromLabelID(WebConstants.NO_SELECTION_ERROR_MESSAGE) %>');
                        return false;
                    }
                    else
                        return true;
                }

            }
        }

        function setMailList(obj) {

            var txtUserNameSucess = document.getElementById('<% =this.txtUserNameSucess.ClientID %>');
            txtUserNameSucess.value = obj;

        }
        function setMailListFailure(obj) {

            var txtUserNameFailure = document.getElementById('<% =this.txtUserNameFailure.ClientID %>');
            txtUserNameFailure.value = obj;

        }



    </script>

</asp:Content>
