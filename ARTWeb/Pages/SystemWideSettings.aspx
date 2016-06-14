<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" Inherits="Pages_SystemWideSettings" Title="Untitled Page"
    Theme="SkyStemBlueBrown" Codebehind="SystemWideSettings.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ErrorAndWarnings" Src="~/UserControls/ErrorAndWarnings.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnlSystemWideSettings" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <%--style="padding-left: 40px; padding-right: 40px"--%>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <%--User Search Criteria--%>
                            <table style="width: 100%">
                                <%--<col width="5%" /> 
                                <col width="25%" />
                                <col width="25%" />--%>
                                <tr>
                                    <td></td>
                                    <td style="width: 20%">
                                        <webControls:ExLabel runat="server" ID="ExLabel1" LabelID="2008" SkinID="Black11Arial"
                                            FormatString="{0}:"></webControls:ExLabel>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList runat="server" ID="ddlFinancialYear" SkinID="DropDownList200" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <%--   <webControls:ExImageButton runat="server" ID="btnAdd" SkinID="CreateUpdateFY" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ManadatoryField">*
                                    </td>
                                    <td style="width: 20%">
                                        <%--style="width:15%" --%>
                                        <webControls:ExLabel runat="server" ID="lblOpenRecPeriod" LabelID="1735" SkinID="Black11Arial"
                                            FormatString="{0}:"></webControls:ExLabel>
                                    </td>
                                    <td align="left">
                                        <%--style="width: 30%"--%>
                                        <asp:DropDownList runat="server" ID="ddlCurrentRecPeriod" OnDataBound="ddlCurrentRecPeriod_DataBound"
                                            SkinID="DropDownList200" OnSelectedIndexChanged="ddlCurrentRecPeriod_SelectedIndexChanged"
                                            onchange=" validateSkipDates123() ">
                                            <%--onchange=" validateSkipDates() "--%>
                                        </asp:DropDownList>
                                        <%--AutoPostBack="true"--%>
                                        <asp:CustomValidator ID="cvCurrentRecPeriod" runat="server" Text="!" ControlToValidate="ddlCurrentRecPeriod"
                                            OnServerValidate="cvCurrentRecPeriod_ServerValidate" Font-Bold="true" Font-Size="Medium"></asp:CustomValidator>
                                        <webControls:ExLabel runat="server" ID="lblPeriodStatus" SkinID="Black11ArialValignMiddle"></webControls:ExLabel>
                                        <input id="hdnCurrentRecPeriodEndDate" type="hidden" runat="server" />
                                        <input id="hdnCurrentRecPeriodSelectedValue" type="hidden" runat="server" />
                                        <input id="hdnCurrentRecPeriodStatusToBeSkipped" type="hidden" runat="server" />
                                        <input id="hdnCurrentRecPeriodSelectedValueTemporary" type="hidden" runat="server" />
                                        <webControls:ExCheckBoxWithLabel LabelID="1741" ID="cbIsPeriodMarkedOpen" runat="server"
                                            SkinID="CheckboxWithLabelBold" />
                                        <webControls:ExCustomValidator runat="server" OnServerValidate="cvIsPeriodMarkedOpen_Validate"
                                            ID="cvIsPeriodMarkedOpen">!</webControls:ExCustomValidator>
                                        <webControls:ExButton ID="btnCloseRecCertStart" CausesValidation="false" SkinID="ExButton265"
                                            Visible="false" runat="server" LabelID="2216" OnClientClick="return confirmCloseRecStartCert(this);"
                                            OnClick="btnCloseRecCertStart_Click" />
                                        <webControls:ExButton ID="btnForceClose" runat="server" CausesValidation="false"
                                            Visible="false" LabelID="2089" OnClick="btnForceClose_Click" />
                                    </td>
                                </tr>
                                <tr id="trErrorAndWarnings" runat="server" visible="false">
                                    <%----%>
                                    <td colspan="3">
                                        <UserControls:ErrorAndWarnings ID="ucErrorAndWarnings" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ManadatoryField">*
                                    </td>
                                    <td>
                                        <webControls:ExLabel runat="server" ID="lblMaximumDocumentSize" LabelID="1350" SkinID="Black11Arial"
                                            FormatString="{0}:"></webControls:ExLabel>
                                    </td>
                                    <td>
                                        <%--colspan="2"--%>
                                        <asp:TextBox runat="server" ID="txtMaximumDocumentSize" SkinID="TextBox200" />
                                        <webControls:ExLabel runat="server" ID="lblInMBs" LabelID="1352" FormatString="({0})"
                                            SkinID="Black11Arial"></webControls:ExLabel>
                                        <asp:RequiredFieldValidator ID="rfvMaximumDocumentSize" runat="server" ControlToValidate="txtMaximumDocumentSize"
                                            Text="!" Font-Bold="true" Font-Size="Medium"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="cvMaximumDocumentSize" runat="server" Text="!" ControlToValidate="txtMaximumDocumentSize"
                                            Font-Bold="true" Font-Size="Medium" ClientValidationFunction="ValidateDocumentSize"
                                            OnServerValidate="cvMaximumDocumentSize_OnServerValidate"></asp:CustomValidator>
                                        <asp:HiddenField ID="hdMaximumDocumentSize" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <webControls:ExLabel runat="server" ID="lblGridHeading" LabelID="1490  " SkinID="Black11Arial"
                                FormatString="{0}:"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--Search Grid--%>
                            <asp:UpdatePanel ID="upnlRadGridSystemWideSettings" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <telerikwebcontrols:exradgrid id="rgSystemWideSettings" runat="server" entitynamelabelid="1421 "
                                        onitemdatabound="rgSystemWideSettings_ItemDataBound" width="100%" allowexporttoexcel="false"
                                        allowexporttopdf="false" allowprint="false" allowprintall="false">
                                        <MasterTableView DataKeyNames="ReconciliationPeriodID" EditMode="InPlace">
                                            <Columns>
                                                <%--Period End Date--%>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1420" HeaderStyle-Width="85px"
                                                    SortExpression="PeriodEndDate" ItemStyle-VerticalAlign="Top">
                                                    <ItemTemplate>
                                                        <webControls:ExLabel ID="lblRecPeriod" runat="server" SkinID="Black11Arial" />                                                        
                                                    </ItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1338" HeaderStyle-Width="85px"
                                                    SortExpression="ReconciliationPeriodStatus" ItemStyle-VerticalAlign="Top">
                                                    <ItemTemplate>
                                                        <webControls:ExLabel ID="lblRecPeriodStatus" runat="server" SkinID="Black11Arial" />
                                                    </ItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <%--Preparer Due Date--%>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1417 " ItemStyle-VerticalAlign="Top">
                                                    <ItemTemplate>
                                                        <webControls:ExCalendar ID="calPrepareDueDate" runat="server" SkinID="ExCalendar100" />
                                                        <webControls:ExCustomValidator runat="server" OnServerValidate="cvIsOnHoliday_ServerValidate"
                                                            ID="cvPrepareDueDateHoliday" ControlToValidate="calPrepareDueDate" LabelID="5000085">!</webControls:ExCustomValidator>
                                                        <asp:RequiredFieldValidator ID="rfvPrepareDueDate" runat="server" ControlToValidate="calPrepareDueDate" Enabled="false" />
                                                        <%--<webControls:ExCompareValidator ID="cmpvPrepareDueDateCurrent" runat="server"  ControlToValidate="calPrepareDueDate"
                                                            Operator="GreaterThanEqual"   Type="Date"></webControls:ExCompareValidator>--%>
                                                        <asp:CustomValidator ID="cvComparePrepareDueDateWithCurrentDate" runat="server" Text="!"
                                                            Font-Bold="true" ControlToValidate="calPrepareDueDate" ClientValidationFunction="CompareDateNotBeforeCurrentDate"></asp:CustomValidator>
                                                    </ItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <%--Reviewer Due Date--%>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1418  " ItemStyle-VerticalAlign="Top">
                                                    <ItemTemplate>
                                                        <webControls:ExCalendar ID="calReviewerDueDate" runat="server" SkinID="ExCalendar100" />
                                                        <webControls:ExCustomValidator runat="server" OnServerValidate="cvIsOnHoliday_ServerValidate"
                                                            ID="cvReviewerDueDateHoliday" ControlToValidate="calReviewerDueDate" LabelID="5000085">!</webControls:ExCustomValidator>
                                                        <asp:CustomValidator ID="cvCompareReviewerDueDateWithCurrentDate" runat="server"
                                                            Text="!" Font-Bold="true" ControlToValidate="calReviewerDueDate" ClientValidationFunction="CompareDateNotBeforeCurrentDate"></asp:CustomValidator>
                                                        <asp:RequiredFieldValidator ID="rfvReviewerDueDate" runat="server" ControlToValidate="calReviewerDueDate" Enabled="false" />
                                                    </ItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <%--Approver Due Date--%>
                                                <telerikWebControls:ExGridTemplateColumn UniqueName="ApproverDueDateColumn" LabelID="1738 "
                                                    SortExpression="ReviewerDueDate" ItemStyle-VerticalAlign="Top">
                                                    <ItemTemplate>
                                                        <webControls:ExCalendar ID="calApproverDueDate" runat="server" SkinID="ExCalendar100" />
                                                        <webControls:ExCustomValidator runat="server" OnServerValidate="cvIsOnHoliday_ServerValidate"
                                                            ID="cvApproverDueDateHoliday" ControlToValidate="calApproverDueDate" LabelID="5000085">!</webControls:ExCustomValidator>
                                                        <asp:CustomValidator ID="cvCompareApproverDueDateWithCurrentDate" runat="server"
                                                            Text="!" Font-Bold="true" ControlToValidate="calApproverDueDate" ClientValidationFunction="CompareDateNotBeforeCurrentDate"></asp:CustomValidator>
                                                        <asp:RequiredFieldValidator ID="rfvApproverDueDate" runat="server" ControlToValidate="calApproverDueDate" Enabled="false" />
                                                    </ItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <%--Certification Start Date--%>
                                                <telerikWebControls:ExGridTemplateColumn UniqueName="CertificationStartDateColumn"
                                                    LabelID="1453" ItemStyle-VerticalAlign="Top">
                                                    <ItemTemplate>
                                                        <webControls:ExCalendar ID="calCertificationStartDate" runat="server" SkinID="ExCalendar100" />
                                                        <webControls:ExCustomValidator runat="server" OnServerValidate="cvIsOnHoliday_ServerValidate"
                                                            ID="cvCertificationStartDateHoliday" ControlToValidate="calCertificationStartDate"
                                                            LabelID="5000085">!</webControls:ExCustomValidator>
                                                        <asp:CustomValidator ID="cvCompareCertificationStartDateWithCurrentDate" runat="server"
                                                            Text="!" Font-Bold="true" ControlToValidate="calCertificationStartDate" ClientValidationFunction="CompareDateNotBeforeCurrentDate"></asp:CustomValidator>
                                                        <asp:RequiredFieldValidator ID="rfvCertificationStartDate" runat="server" ControlToValidate="calCertificationStartDate" Enabled="false" />
                                                    </ItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <%--Allow Certification Lock Down--%>
                                                <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="20px" UniqueName="AllowCertificationLockDownColumn"
                                                    LabelID="1454 " ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <webControls:ExCheckBox ID="cbAllowCertificationLockDown" runat="server" /><%--OnClick="ShowHide( this, calPrepareDueDate)"--%>
                                                    </ItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <%--Close or Lock Down Date--%>
                                                <telerikWebControls:ExGridTemplateColumn UniqueName="CloseOrLockDownDateColumn" SortExpression=""
                                                    HeaderStyle-Width="150px" ItemStyle-VerticalAlign="Top" LabelID="1419">
                                                    <ItemTemplate>
                                                        <webControls:ExCalendar ID="calCloseOrLockDownDate" runat="server" SkinID="ExCalendar100" />
                                                        <webControls:ExCustomValidator runat="server" OnServerValidate="cvIsOnHoliday_ServerValidate"
                                                            ID="cvCloseOrLockDownDateHoliday" ControlToValidate="calCloseOrLockDownDate"
                                                            LabelID="5000085">!</webControls:ExCustomValidator>
                                                        <asp:CustomValidator ID="cvCompareCloseOrLockDownDateWithCurrentDate" runat="server"
                                                            Text="!" Font-Bold="true" ControlToValidate="calCloseOrLockDownDate" ClientValidationFunction="CompareDateNotBeforeCurrentDate"
                                                            OnServerValidate="cvMaximumDocumentSize_OnServerValidate"></asp:CustomValidator>
                                                        <asp:RequiredFieldValidator ID="rfvCloseOrLockDownDate" runat="server" ControlToValidate="calCloseOrLockDownDate" Enabled="false" />
                                                        <asp:CustomValidator ID="cvcalCloseOrLockDownDate" runat="server" ControlToValidate="calCloseOrLockDownDate"
                                                            ClientValidationFunction="ValidateDate">
                                                        </asp:CustomValidator>
                                                        <asp:CustomValidator ID="cvAllDueDateAdjacent" runat="server" Text="!" Font-Bold="true"
                                                            ClientValidationFunction="CompareDateNotBeforeAdjacentDateNewFunda" OnServerValidate="cvAllDueDateAdjacent_OnServerValidate"></asp:CustomValidator>
                                                    </ItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>
                                                 <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="20px" UniqueName="RecPeriodStatusHistoryColumn"  ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                         <webControls:ExHyperLink ID="hlRecPeriodHistory" SkinID="HistoryHyperlink" runat="server"></webControls:ExHyperLink>
                                                    </ItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerikwebcontrols:exradgrid>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <UserControls:ProgressBar ID="ucRadGridSystemWideSettings" runat="server" AssociatedUpdatePanelID="upnlRadGridSystemWideSettings" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">

                            <webControls:ExButton LabelID="2982" runat="server" ID="btnReopenPeriod" Visible="false" OnClick="btnReopenPeriod_Click" CausesValidation="false"
                                SkinID="ExButton150" />
                            <webControls:ExButton LabelID="1315" runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true; this.value = 'Saving...';" UseSubmitBehavior="false"
                                SkinID="ExButton100" />
                            <%-- OnClientClick="return Test();"--%>
                            <webControls:ExButton LabelID="1239" runat="server" ID="btnCancel" CausesValidation="false"
                                OnClick="btnCancel_Click" SkinID="ExButton100" />
                            <asp:HiddenField runat="server" ID="hdIsRefreshData" Value="0" />
                            <asp:CustomValidator ID="cvValidateDatesTopDown" ClientValidationFunction="ValidateDatesTopDown"
                                runat="server" ErrorMessage="Dates not in order" Text="" Font-Bold="true" Font-Size="Medium"
                                OnServerValidate="cvValidateDatesTopDown_OnServerValidate"></asp:CustomValidator>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <UserControls:ProgressBar ID="ucProgressBarSystemWideSettings" runat="server" AssociatedUpdatePanelID="upnlSystemWideSettings" />

    <script type="text/javascript" language="javascript">

        function ValidateDatesTopDown(sender, args) {
            //debugger
            //alert('Test');
            var radGrid = $find('<% = this.rgSystemWideSettings.ClientID %>');
            var masterTable = radGrid.get_masterTableView();
            var rowsCollection = masterTable.get_dataItems();
            var rowsCount = rowsCollection.length;

            var preparerDueDateArray = new Array();
            var reviewerDueDateArray = new Array();
            var approverDueDateArray = new Array();
            var CertStartDateArray = new Array();
            var CloseRecOrLockCertArray = new Array();
            //compare only when rowcount is > 1
            if (rowsCount > 1) {
                for (var i = 0; i < rowsCount; i++) {
                    var row = rowsCollection[i];
                    if (row != null && row != 'undefined') {
                        //Preparer
                        var txtPreparerDueDate = row.findElement('calPrepareDueDate');
                        if (txtPreparerDueDate != null && txtPreparerDueDate != 'undefined') {
                            var dateValueP = removeSpaces(txtPreparerDueDate.value);
                            if (dateValueP != '')
                                preparerDueDateArray.push(dateValueP);
                        }

                        //Reviewer
                        var txtRevDueDate = row.findElement('calReviewerDueDate');
                        if (txtRevDueDate != null && txtRevDueDate != 'undefined') {
                            var dateValueR = removeSpaces(txtRevDueDate.value);
                            if (dateValueR != '')
                                reviewerDueDateArray.push(dateValueR);
                        }

                        //Approver
                        var txtAppDueDate = row.findElement('calApproverDueDate');
                        if (txtAppDueDate != null && txtAppDueDate != 'undefined') {
                            var dateValueA = removeSpaces(txtAppDueDate.value);
                            if (dateValueA != '')
                                approverDueDateArray.push(dateValueA);
                        }

                        //Approver
                        var txtCertStartDate = row.findElement('calCertificationStartDate');
                        if (txtCertStartDate != null && txtCertStartDate != 'undefined') {
                            var dateValueA = removeSpaces(txtCertStartDate.value);
                            if (dateValueA != '')
                                CertStartDateArray.push(dateValueA);
                        }


                        //close rec or lock cert
                        var txtCloseRecOrLockCertDate = row.findElement('calCloseOrLockDownDate');
                        if (txtCloseRecOrLockCertDate != null && txtCloseRecOrLockCertDate != 'undefined') {
                            var dateValueA = removeSpaces(txtCloseRecOrLockCertDate.value);
                            if (dateValueA != '')
                                CloseRecOrLockCertArray.push(dateValueA);
                        }
                    }

                }
                if (CheckDates(preparerDueDateArray) == false) {
                    sender.setAttribute('ErrorMessage', sender.getAttribute("PreparerDueDateErrorMessage"));
                    args.IsValid = false;
                    return;
                }
                if (CheckDates(reviewerDueDateArray) == false) {
                    sender.setAttribute('ErrorMessage', sender.getAttribute("ReviewerDueDateErrorMessage"));
                    args.IsValid = false;
                    return;
                }
                if (CheckDates(approverDueDateArray) == false) {
                    sender.setAttribute('ErrorMessage', sender.getAttribute("ApproverDueDateErrorMessage"));
                    args.IsValid = false;
                    return;
                }
                if (CheckDates(CertStartDateArray) == false) {
                    sender.setAttribute('ErrorMessage', sender.getAttribute("CertStartDateErrorMessage"));
                    args.IsValid = false;
                    return;
                }
                if (CheckDates(CloseRecOrLockCertArray) == false) {
                    sender.setAttribute('ErrorMessage', sender.getAttribute("RecCloseDateErrorMessage"));
                    args.IsValid = false;
                    return;
                }
            }

            return false;

        }

        function CheckDates(datesArray) {
            if (datesArray != null && datesArray != 'undefined') {
                for (var i = 1; i < datesArray.length; i++) {
                    var minValue = datesArray[i - 1];
                    var maxValue = datesArray[i];
                    var result = CompareDates(maxValue, minValue);
                    if (result == -1) {
                        return false;
                    }

                }
            }

        }

        //Check if this works.
        function ShowHideOrCopy(cbAllowCertificationLockDownClientID, calCertificationStartDateClientID, calCertificationLockDownDateClientID) {
            var cbAllowCertificationLockDown = document.getElementById(cbAllowCertificationLockDownClientID);
            var calCertificationLockDownDate = document.getElementById(calCertificationLockDownDateClientID);
            var calCertificationStartDate = document.getElementById(calCertificationStartDateClientID);
            //            alert(calCertificationStartDate);
            //            alert(calCertificationStartDate.value);
            if (cbAllowCertificationLockDown.checked == true) {
                if (calCertificationStartDate != null && calCertificationStartDate.value != "" && calCertificationLockDownDate.value == "") {
                    calCertificationLockDownDate.value = calCertificationStartDate.value
                }
                calCertificationLockDownDate.disabled = "";
            }
            else if (cbAllowCertificationLockDown.checked == false) {
                calCertificationLockDownDate.disabled = "disabled";
            }
        }

        //Rec Period Class
        function RecPeriod(RecPeriodID, PeriodEndDate, StatusID) {
            this.RecPeriodID = RecPeriodID;
            this.PeriodEndDate = PeriodEndDate;
            this.StatusID = StatusID;
        }

        //RecPeriod Collection Class
        function RecPeriodCollection(recPeriodString) {
            this.RecPeriodString = recPeriodString;
        }

        RecPeriodCollection.prototype.GetRecPeriodObjectArray = function () {
            var recPeriodObjectArray = new Array();

            var recPeriodArray = this.RecPeriodString.split(",");
            for (var i = 0; i < recPeriodArray.length; i++) {

                //extract rec period values
                var recPeriod = recPeriodArray[i].split("~");

                //create a new rec period object
                var oRecPeriod = new RecPeriod(recPeriod[0], recPeriod[1], recPeriod[2]);

                //Add to Array
                recPeriodObjectArray.push(oRecPeriod);
            }
            return recPeriodObjectArray;
        }

        RecPeriodCollection.prototype.SortRecPeriodObjectArrayByPeriodEndDate = function (recPeriodObjectArray) {
            var swap = false;
            var n = recPeriodObjectArray.length;
            var date1;
            var date2;
            while (true) {
                swap = false;
                for (var i = 0; i < n - 1; i++) {
                    date1 = recPeriodObjectArray[i].PeriodEndDate;
                    date2 = recPeriodObjectArray[i + 1].PeriodEndDate;
                    if (CompareDates(date1, date2) == 1) {
                        var tmp = recPeriodObjectArray[i];
                        recPeriodObjectArray[i] = recPeriodObjectArray[i + 1];
                        recPeriodObjectArray[i + 1] = tmp;
                        swap = true;
                    }
                }
                n = n - 1;
                if (swap == false)
                    break;
            }
            return recPeriodObjectArray;
        }
        RecPeriodCollection.prototype.GetNotStartedRP = function (selectedRPID, selectedRPEndDate, RPSortedArray) {
            var notStartedRPCollection = new Array();
            for (var i = 0; i < RPSortedArray.length; i++) {
                var RP = RPSortedArray[i];
                var RPStatus = RPSortedArray[i].StatusID;
                var RPEndDate = RPSortedArray[i].PeriodEndDate
                if (CompareDates(selectedRPEndDate, RPEndDate) == 1 && RPStatus == 1) {
                    notStartedRPCollection.push(RP);
                }
            }
            return notStartedRPCollection;

        }
        RecPeriodCollection.prototype.GetRecPeriodByPeriodEndDate = function (periodEndDate, sortedRecPeriodArray) {
            for (var i = 0; i < sortedRecPeriodArray.length; i++) {
                var rp = sortedRecPeriodArray[i];
                if (CompareDates(periodEndDate, rp.PeriodEndDate) == 0) {
                    return rp;
                }
            }
            return new RecPeriod();
        }



        function validateSkipDates123() {

            var msg;
            var msgNoOfSkippedPeriods;
            var msgListOfSkippedPeriods;
            var skippedPeriods = '';
            var countSkippedPeriods = 0;
            var ddlCurrentRecPeriod = document.getElementById("<%=ddlCurrentRecPeriod.ClientID%>");
            var hdnCurrentRecPeriodEndDate = document.getElementById("<%=hdnCurrentRecPeriodEndDate.ClientID%>");
            var hdnCurrentRecPeriodSelectedValue = document.getElementById("<%=hdnCurrentRecPeriodSelectedValue.ClientID%>");
            var hdnCurrentRecPeriodStatusToBeSkipped = document.getElementById("<%=hdnCurrentRecPeriodStatusToBeSkipped.ClientID%>");
            var hdnCurrentRecPeriodSelectedValueTemporary = document.getElementById("<%=hdnCurrentRecPeriodSelectedValueTemporary.ClientID%>");
            var recPeriods = ddlCurrentRecPeriod.getAttribute("RecPeriods");
            if (recPeriods != null) {
                //get RecPeriod collection from attribute.
                var RPCollection = new RecPeriodCollection(recPeriods);

                //get an array of recPeriod Objects
                var RPObjectArray = RPCollection.GetRecPeriodObjectArray();

                //sort rec period object array on the basis of PeriodEndDate
                var RPObjectArraySorted = RPCollection.SortRecPeriodObjectArrayByPeriodEndDate(RPObjectArray);

                //get current selected rec period enddate
                var currentSelectedRPID = ddlCurrentRecPeriod.options[ddlCurrentRecPeriod.selectedIndex].value;
                var currentSelectedRPEndDate = ddlCurrentRecPeriod.options[ddlCurrentRecPeriod.selectedIndex].text;

                //Get currently selected rec period
                var currentSelectedRP = RPCollection.GetRecPeriodByPeriodEndDate(currentSelectedRPEndDate, RPObjectArraySorted);

                //if currently selected rec period status is not skipped
                if (currentSelectedRP.RecPeriodID != null && currentSelectedRP.StatusID != 5) {
                    //find all not started rec period before currently selected rec period.
                    var notStartedRP = RPCollection.GetNotStartedRP(currentSelectedRPID, currentSelectedRPEndDate, RPObjectArraySorted);

                    if (notStartedRP != null && notStartedRP.length > 0) {
                        countSkippedPeriods = notStartedRP.length;
                        for (var r = 0; r < notStartedRP.length; r++) {
                            skippedPeriods = skippedPeriods + ',' + notStartedRP[r].PeriodEndDate;
                        }
                        msgNoOfSkippedPeriods = countSkippedPeriods;
                        msgListOfSkippedPeriods = skippedPeriods;
                        var url = "ConfirmPopUp.aspx?msgNoOfSkippedPeriods=" + msgNoOfSkippedPeriods + "&msgListOfSkippedPeriods=" + msgListOfSkippedPeriods + "&selectedTemporaryValueDDL=" + hdnCurrentRecPeriodSelectedValueTemporary.value + "&selectedValueInDB=" + hdnCurrentRecPeriodSelectedValue.value;
                        OpenRadWindow(url, 300, 500);

                    }
                    else
                        DoPostBackForSkipping();
                }
                else
                    DoPostBackForSkipping();
            }



            //            if (ddlCurrentRecPeriod != null && hdnCurrentRecPeriodEndDate != null && hdnCurrentRecPeriodSelectedValue != null) {
            //                var newRecPeriodEndDate = ddlCurrentRecPeriod.options[ddlCurrentRecPeriod.selectedIndex].text;
            //                var currentRecPeriodEndDate = hdnCurrentRecPeriodEndDate.value;
            //                var iscurrentRecPeriodEndDateNull = false;
            //                if (hdnCurrentRecPeriodStatusToBeSkipped.value == '1') {
            //                    var iscurrentRecPeriodToBeSkipped = true;
            //                }
            //                else {
            //                    var iscurrentRecPeriodToBeSkipped = false;
            //                }
            //                if (currentRecPeriodEndDate == null) {
            //                    iscurrentRecPeriodEndDateNull = true;
            //                }

            //                if (CompareDates(newRecPeriodEndDate, currentRecPeriodEndDate) == 0) {

            //                    DoPostBackForSkipping();
            //                }
            //                else {
            //                    if (((hdnCurrentRecPeriodSelectedValue.value > 0) && iscurrentRecPeriodToBeSkipped))//and not started too, && hdnCurrentRecPeriodSelectedValue.value >0
            //                    {
            //                        countSkippedPeriods = countSkippedPeriods + 1;
            //                        skippedPeriods = skippedPeriods + ',' + currentRecPeriodEndDate;
            //                    }

            //                    for (var i = 1; i <= ddlCurrentRecPeriod.length - 1; i++) {
            //                        var skipedDate = ddlCurrentRecPeriod[i].text;
            //                        if (((hdnCurrentRecPeriodSelectedValue.value <= 0) && (CompareDates(newRecPeriodEndDate, skipedDate) == 1)) ||
            //                    ((CompareDates(skipedDate, currentRecPeriodEndDate) == 1)
            //                    && (CompareDates(newRecPeriodEndDate, skipedDate) == 1))) {
            //                            countSkippedPeriods = countSkippedPeriods + 1;
            //                            skippedPeriods = skippedPeriods + ',' + skipedDate;
            //                        }
            //                    }

            //                    if (countSkippedPeriods > 0) {
            //                        msgNoOfSkippedPeriods = countSkippedPeriods;
            //                        msgListOfSkippedPeriods = skippedPeriods;
            //                        var url = "ConfirmPopUp.aspx?msgNoOfSkippedPeriods=" + msgNoOfSkippedPeriods + "&msgListOfSkippedPeriods=" + msgListOfSkippedPeriods + "&selectedTemporaryValueDDL=" + hdnCurrentRecPeriodSelectedValueTemporary.value + "&selectedValueInDB=" + hdnCurrentRecPeriodSelectedValue.value;
            //                        OpenRadWindow(url, 300, 500)
            //                    }
            //                    else {
            //                        DoPostBackForSkipping()
            //                    }
            //                }

            //            }
            //            else {
            //                DoPostBackForSkipping()
            //            }
        }



        function DoPostBackForSkipping() {
            __doPostBack('', '');
        }


        //  '>'
        function IsDateGreaterDDMMYYYY(DateValueGreater, DateValueSmaller) { // NOTE: parseInt is giving unexpected results:
            ///DDMMYYYY
            //                var indexYear= 2;
            //                var indexMonth= 1;
            //                var indexDay= 0; 
            //MMDDYYYY
            var indexYear = 2;
            var indexMonth = 0;
            var indexDay = 1;

            var isGreater = false;
            var dateArrGreater = DateValueGreater.split("/");
            var dateArrSmaller = DateValueSmaller.split("/");
            if (parseFloat(dateArrGreater[indexYear]) > parseFloat(dateArrSmaller[indexYear])) {
                isGreater = true;
            }
            else
                if (parseFloat(dateArrGreater[indexYear]) == parseFloat(dateArrSmaller[indexYear])) {
                    if (parseFloat(dateArrGreater[indexMonth]) > parseFloat(dateArrSmaller[indexMonth])) {
                        isGreater = true;
                    }
                    else
                        if (parseFloat(dateArrGreater[indexMonth]) == parseFloat(dateArrSmaller[indexMonth])) {
                            if (parseFloat(dateArrGreater[indexDay]) > parseFloat(dateArrSmaller[indexDay])) {
                                isGreater = true;
                            }
                        }
                }
            return isGreater;
        }

        function CompareDateNotBeforeCurrentDate(sender, args) {
            var dateToCompare = sender.dateToCompare;
            var dateCal = document.getElementById(sender.controltovalidate).value;
            var result = CompareDates(dateCal, dateToCompare);
            if (result == -1) //what about <0
            {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        function CompareDateNotBeforeAdjacentDateNewFunda(sender, args) {
            var clientIDList = sender.getAttribute("controlListForAdjacentToCompare");
            var arrayClientID = clientIDList.split(",");
            var calPrepareDueDate = document.getElementById(arrayClientID[0]);
            var calReviewerDueDate = document.getElementById(arrayClientID[1]);
            var calApproverDueDate = document.getElementById(arrayClientID[2]);
            var calCertificationStartDate = document.getElementById(arrayClientID[3]);
            var calCloseOrLockDownDate = document.getElementById(arrayClientID[4]);
            var datesArray = new Array();

            var PrepareDueDate;
            var ReviewerDueDate;
            var ApproverDueDate;
            var CertificationStartDate;
            var CloseOrLockDownDate;
            if (calPrepareDueDate != null && calPrepareDueDate != 'undefined') {
                PrepareDueDate = removeSpaces(calPrepareDueDate.value);
                if (PrepareDueDate != '')
                    datesArray.push(PrepareDueDate);
            }
            if (calReviewerDueDate != null && calReviewerDueDate != 'undefined') {
                ReviewerDueDate = removeSpaces(calReviewerDueDate.value);
                if (ReviewerDueDate != '')
                    datesArray.push(ReviewerDueDate);
            }
            if (calApproverDueDate != null && calApproverDueDate != 'undefined') {
                ApproverDueDate = removeSpaces(calApproverDueDate.value);
                if (ApproverDueDate != '')
                    datesArray.push(ApproverDueDate);
            }
            if (calCertificationStartDate != null && calCertificationStartDate != 'undefined') {
                CertificationStartDate = removeSpaces(calCertificationStartDate.value);
                if (CertificationStartDate != '')
                    datesArray.push(CertificationStartDate);
            }
            if (calCloseOrLockDownDate != null && calCloseOrLockDownDate != 'undefined') {
                CloseOrLockDownDate = removeSpaces(calCloseOrLockDownDate.value);
                if (CloseOrLockDownDate != '')
                    datesArray.push(CloseOrLockDownDate);
            }
            var result;

            //datesArray[0] = PrepareDueDate;
            //datesArray[1] = ReviewerDueDate;
            //datesArray[2] = ApproverDueDate;
            //datesArray[3] = CertificationStartDate;
            //datesArray[4] = CloseOrLockDownDate;

            for (var i = datesArray.length - 1; i >= 1; --i) {
                for (var j = i - 1; j >= 0; --j) {
                    if (i > j) {
                        result = CompareDates(datesArray[i], datesArray[j]);
                        if (result == -1) //what about <0
                        {
                            args.IsValid = false;
                            return;
                        }
                    }
                }
            }
        }


        function removeSpaces(string) {
            return string.split(' ').join('');
        }
        function CancelSkippedPopup(selectedTemporaryValueDDL, selectedValueInDB) {
            var ddlCurrentRecPeriod = document.getElementById('<% =this.ddlCurrentRecPeriod.ClientID %>');

            if (ddlCurrentRecPeriod != null)
                ddlCurrentRecPeriod.selectedIndex = 0;
            if (selectedTemporaryValueDDL != selectedValueInDB) {
                DoPostBackForSkipping();

            }
        }


        function ValidateDocumentSize(sender, args) {
            var txtCurrentDocumentSize = $get('ctl00_ContentPlaceHolder1_txtMaximumDocumentSize');
            var txtMaximumSizetoCompare = $get('ctl00_ContentPlaceHolder1_hdMaximumDocumentSize');

            var txtCurrentDocumentSizeValue = txtCurrentDocumentSize.value;
            var txtMaximumSizetoCompareValue = txtMaximumSizetoCompare.value;

            if (txtCurrentDocumentSizeValue == null || txtCurrentDocumentSizeValue == "") {
                args.IsValid = true;
            }
            else {
                if (IsPositiveDecimal(txtCurrentDocumentSize)) {
                    if (parseFloat(GetDisplayDecimalValue(txtCurrentDocumentSizeValue, 2)) > parseFloat(GetDisplayDecimalValue(txtMaximumSizetoCompareValue, 2)))
                        args.IsValid = false;
                    else
                        args.IsValid = true;
                }
                else {

                    args.IsValid = false;
                }
            }
        }


        function confirmCloseRecStartCert(btn) {

            return confirm('<% = Helper.GetAlertMessageFromLabelID(WebConstants.CONFIRM_FOR_CLOSE_RECPERIOD_AND_START_CERTIFICATION) %>');
        }

        function ValidateDate(sender, args) {
            if (IsDate(document.getElementById(sender.controltovalidate).value)) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }


    </script>


</asp:Content>
