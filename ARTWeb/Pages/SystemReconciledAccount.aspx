<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" Inherits="Pages_SystemReconciledAccount"
    Title="Untitled Page" Theme="SkyStemBlueBrown" MaintainScrollPositionOnPostback="true" Codebehind="SystemReconciledAccount.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="InputRequirements" Src="~/UserControls/InputRequirements.ascx" %>
<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="Legend" Src="~/UserControls/LegendOnAccountAndSRAViewer.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
           onpageload();
        });

        function onpageload() {
            var RoleID = '<%= HttpContext.Current.Session[SessionConstants.CURRENT_ROLE_ID] %>';
            var TargetControl = document.getElementById('<%=MyHiddenField.ClientID %>');
            var LstSatatus = TargetControl.value.split("|");
            if (LstSatatus.length == 0 || LstSatatus[0] == "") {
                <%--  var btnDownloadSelected = document.getElementById('<%=btnDownloadSelected.ClientID %>');
                if (btnDownloadSelected != null && btnDownloadSelected != "undefined")
                    btnDownloadSelected.disabled = true;--%>

                if ('<%= CurrentRecProcessStatus.Value %>' != '<%= WebEnums.RecPeriodStatus.Closed %>') {
                    if (RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.SYSTEM_ADMIN) %>') {
                        var btnReset = document.getElementById('<%=btnReset.ClientID %>');
                        if (btnReset != null && btnReset != "undefined")
                            btnReset.disabled = true;
                        var btnReopen = document.getElementById('<%=btnReopen.ClientID %>');
                        if (btnReopen != null && btnReopen != "undefined")
                            btnReopen.disabled = true;
                    }
                    else {
                        var btnAccept = document.getElementById('<%=btnAccept.ClientID %>');
                        if (btnAccept != null && btnAccept != "undefined")
                            btnAccept.disabled = true;
                        var btnSubmit = document.getElementById('<%=btnSubmit.ClientID %>');
                        if (btnSubmit != null && btnSubmit != "undefined")
                            btnSubmit.disabled = true;
                        var btnRemoveSignOff = document.getElementById('<%=btnRemoveSignOff.ClientID %>');
                        if (btnRemoveSignOff != null && btnRemoveSignOff != "undefined")
                            btnRemoveSignOff.disabled = true;
                    }
                }
            }
            else {

                var TargetControl = document.getElementById('<%=MyHiddenField.ClientID %>');

                var LstSatatus = TargetControl.value.split("|");

                if (LstSatatus.length > 0) {

                  <%--  var btnDownloadSelected = document.getElementById('<%=btnDownloadSelected.ClientID %>');
                    if (btnDownloadSelected != null && btnDownloadSelected != "undefined")
                        btnDownloadSelected.disabled = false;--%>

                    if ('<%= CurrentRecProcessStatus.Value %>' != '<%= WebEnums.RecPeriodStatus.Closed %>') {
                        var RoleID = '<%= HttpContext.Current.Session[SessionConstants.CURRENT_ROLE_ID] %>';


                        if (RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.SYSTEM_ADMIN) %>') {
                            EnableDisableResetButton(LstSatatus);
                            EnableDisableReOpenButton(LstSatatus);
                        }

                        if (RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.PREPARER) %>' || RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.BACKUP_PREPARER) %>') {
                            EnableDisableSignOffButton(LstSatatus);
                            EnableDisableSubmitnButton(LstSatatus);
                        }

                        if (RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.REVIEWER) %>' || RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.BACKUP_REVIEWER) %>') {
                            EnableDisableAcceptButton(LstSatatus);
                            EnableDisableReSubmitnButton(LstSatatus);

                        }

                        if (RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.APPROVER) %>' || RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.BACKUP_APPROVER) %>') {
                            EnableDisableApproveAcceptButton(LstSatatus);
                            EnableDisableApproveSubmitnButton(LstSatatus);

                        }
                    }
                }

            }
        }

        function RowSelected(sender, eventArgs) {
            var TargetControl = document.getElementById('<%=MyHiddenField.ClientID %>');

            if (TargetControl.value == "" || TargetControl.value == null)
                TargetControl.value = eventArgs.getDataKeyValue('ReconciliationStatusID') + "^" + eventArgs.getDataKeyValue('GLDataID') + "^" + eventArgs.getDataKeyValue('IsSystemReconcilied') + "^" + eventArgs.getDataKeyValue('IsEditable') + "^" + eventArgs.getDataKeyValue('IsLocked') + "^" + eventArgs.getDataKeyValue('IsRCCValidation');
            else
                TargetControl.value = TargetControl.value + "|" + eventArgs.getDataKeyValue('ReconciliationStatusID') + "^" + eventArgs.getDataKeyValue('GLDataID') + "^" + eventArgs.getDataKeyValue('IsSystemReconcilied') + "^" + eventArgs.getDataKeyValue('IsEditable') + "^" + eventArgs.getDataKeyValue('IsLocked') + "^" + eventArgs.getDataKeyValue('IsRCCValidation');
            var LstSatatus = TargetControl.value.split("|");

            if (LstSatatus.length > 0) {

              <%--  var btnDownloadSelected = document.getElementById('<%=btnDownloadSelected.ClientID %>');
                if (btnDownloadSelected != null && btnDownloadSelected != "undefined")
                    btnDownloadSelected.disabled = false;--%>

                var RoleID = '<%= HttpContext.Current.Session[SessionConstants.CURRENT_ROLE_ID] %>';

                if ('<%= CurrentRecProcessStatus.Value %>' != '<%= WebEnums.RecPeriodStatus.Closed %>') {

                    if (RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.SYSTEM_ADMIN) %>') {
                     EnableDisableResetButton(LstSatatus);
                     EnableDisableReOpenButton(LstSatatus);
                 }

                 if (RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.PREPARER) %>' || RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.BACKUP_PREPARER) %>') {
                     EnableDisableSignOffButton(LstSatatus);
                     EnableDisableSubmitnButton(LstSatatus);
                 }

                 if (RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.REVIEWER) %>' || RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.BACKUP_REVIEWER) %>') {
                     EnableDisableAcceptButton(LstSatatus);
                     EnableDisableReSubmitnButton(LstSatatus);
                 }

                 if (RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.APPROVER) %>' || RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.BACKUP_APPROVER) %>') {
                     EnableDisableApproveAcceptButton(LstSatatus);
                     EnableDisableApproveSubmitnButton(LstSatatus);
                 }
             }
         }
     }

     function RowDeselected(sender, eventArgs) {
         var RoleID = '<%= HttpContext.Current.Session[SessionConstants.CURRENT_ROLE_ID] %>';
            var TargetControl = document.getElementById('<%=MyHiddenField.ClientID %>');
            var deSelVal = eventArgs.getDataKeyValue('ReconciliationStatusID') + "^" + eventArgs.getDataKeyValue('GLDataID') + "^" + eventArgs.getDataKeyValue('IsSystemReconcilied') + "^" + eventArgs.getDataKeyValue('IsEditable') + "^" + eventArgs.getDataKeyValue('IsLocked') + "^" + eventArgs.getDataKeyValue('IsRCCValidation');
            var NewVal = null;
            var temArr = TargetControl.value.split("|");


            if (temArr.length > 0) {
                for (var i = 0; i < temArr.length; i++) {
                    if (!(temArr[i] === deSelVal)) {
                        if (NewVal == null)
                            NewVal = temArr[i]
                        else
                            NewVal = NewVal + "|" + temArr[i];
                    }
                }
            }
            TargetControl.value = NewVal;

            var LstSatatus = TargetControl.value.split("|");
            if (LstSatatus.length > 0 && LstSatatus[0] != "") {

            <%--    var btnDownloadSelected = document.getElementById('<%=btnDownloadSelected.ClientID %>');
                if (btnDownloadSelected != null && btnDownloadSelected != "undefined")
                    btnDownloadSelected.disabled = false;--%>

                if ('<%= CurrentRecProcessStatus.Value %>' != '<%= WebEnums.RecPeriodStatus.Closed %>') {
                    if (RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.SYSTEM_ADMIN) %>') {
                        EnableDisableResetButton(LstSatatus);
                        EnableDisableReOpenButton(LstSatatus);
                    }

                    if (RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.PREPARER) %>' || RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.BACKUP_PREPARER) %>') {
                        EnableDisableSignOffButton(LstSatatus);
                        EnableDisableSubmitnButton(LstSatatus);
                    }

                    if (RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.REVIEWER) %>' || RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.BACKUP_REVIEWER) %>') {
                        EnableDisableAcceptButton(LstSatatus);
                        EnableDisableReSubmitnButton(LstSatatus);
                    }

                    if (RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.APPROVER) %>' || RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.BACKUP_APPROVER) %>') {
                        EnableDisableApproveAcceptButton(LstSatatus);
                        EnableDisableApproveSubmitnButton(LstSatatus);
                    }
                }
            }

            else {
                <%--   var btnDownloadSelected = document.getElementById('<%=btnDownloadSelected.ClientID %>');
             if (btnDownloadSelected != null && btnDownloadSelected != "undefined")
                btnDownloadSelected.disabled = true;--%>

                if ('<%= CurrentRecProcessStatus.Value %>' != '<%= WebEnums.RecPeriodStatus.Closed %>') {
                    if (RoleID == '<%= Convert.ToInt32(ARTEnums.UserRole.SYSTEM_ADMIN) %>') {
                        var btnReset = document.getElementById('<%=btnReset.ClientID %>');
                        if (btnReset != null && btnReset != "undefined")
                            btnReset.disabled = true;
                        var btnReopen = document.getElementById('<%=btnReopen.ClientID %>');
                        if (btnReopen != null && btnReopen != "undefined")
                            btnReopen.disabled = true;
                    }
                    else {
                        var btnAccept = document.getElementById('<%=btnAccept.ClientID %>');
                        if (btnAccept != null && btnAccept != "undefined")
                            btnAccept.disabled = true;
                        var btnSubmit = document.getElementById('<%=btnSubmit.ClientID %>');
                        if (btnSubmit != null && btnSubmit != "undefined")
                            btnSubmit.disabled = true;
                    }

                }
            }
        }

        function EnableDisableResetButton(params) {
            var IsEnableReset = true;
            var btnReset = document.getElementById('<%=btnReset.ClientID %>');
            if (btnReset != null && btnReset != "undefined") {
                for (i = 0; i < params.length; i++) {
                    var temparray = params[i].toString().split("^");
                    if (temparray[4] == "True" || temparray[0] == '<%= Convert.ToInt32(WebEnums.ReconciliationStatus.NotStarted) %>' || (temparray[2] == "True" && temparray[0] == '<%= Convert.ToInt32(WebEnums.ReconciliationStatus.Prepared) %>')) {
                        IsEnableReset = false;
                        break;
                    }
                }
                if (params.length > 0) {
                    if (IsEnableReset == false)
                        btnReset.disabled = true;
                    else
                        btnReset.disabled = false;
                }
            }
        }

        function EnableDisableReOpenButton(params, IsSystemReconcilied) {
            var IsEnableOpen = true;
            var btnReopen = document.getElementById('<%=btnReopen.ClientID %>');
            if (btnReopen != null && btnReopen != "undefined") {
                for (i = 0; i < params.length; i++) {
                    var temparray = params[i].toString().split("^");
                    if (temparray[4] == "True" || !(temparray[0] == '<%= Convert.ToInt32(WebEnums.ReconciliationStatus.Reconciled) %>' || temparray[0] == '<%= Convert.ToInt32(WebEnums.ReconciliationStatus.SysReconciled) %>')) {
                        IsEnableOpen = false;
                        break;
                    }
                }
                if (params.length > 0) {
                    if (IsEnableOpen == false)
                        btnReopen.disabled = true;
                    else
                        btnReopen.disabled = false;
                }
            }
        }


        function EnableDisableSignOffButton(params) {
            var IsEnableSignOff = true;
            var btnSignoff = document.getElementById('<%=btnAccept.ClientID %>');
            var btnRemoveSignOff = document.getElementById('<%=btnRemoveSignOff.ClientID %>'); 
            if (btnSignoff != null && btnSignoff != "undefined") {
                for (i = 0; i < params.length; i++) {
                    var temparry = params[i].toString().split("^");
                    if (!(temparry[0] == '<%= Convert.ToInt32(WebEnums.ReconciliationStatus.PendingModificationPreparer) %>' && temparry[3] == "True" && temparry[5] == "True")) {
                        IsEnableSignOff = false;
                    }
                }
                if (params.length > 0) {
                    if (IsEnableSignOff == false){
                        btnSignoff.disabled = true;
                        btnRemoveSignOff.disabled = true;
                    }
                    else{
                        btnSignoff.disabled = false;
                        btnRemoveSignOff.disabled = false;
                    }
                }
            }
        }

        function EnableDisableSubmitnButton(params) {
            var IsEnableSubmit = true;
            var btnSubmit = document.getElementById('<%=btnSubmit.ClientID %>');
            if (btnSubmit != null && btnSubmit != "undefined") {
                for (i = 0; i < params.length; i++) {
                    var temparry = params[i].toString().split("^");
                    if (!(temparry[0] == '<%= Convert.ToInt32(WebEnums.ReconciliationStatus.Prepared) %>' && temparry[3] == "True" && temparry[5] == "True")) {
                        IsEnableSubmit = false;
                    }
                }
                if (params.length > 0) {
                    if (IsEnableSubmit == false)
                        btnSubmit.disabled = true;
                    else
                        btnSubmit.disabled = false;
                }
            }
        }

        function EnableDisableAcceptButton(params) {
            var IsEnableAccept = true;
            var btnAccept = document.getElementById('<%=btnAccept.ClientID %>');
            if (btnAccept != null && btnAccept != "undefined") {
                for (i = 0; i < params.length; i++) {
                    var temparry = params[i].toString().split("^");
                    if (!((temparry[0] == '<%= Convert.ToInt32(WebEnums.ReconciliationStatus.PendingReview) %>' || temparry[0] == '<%= Convert.ToInt32(WebEnums.ReconciliationStatus.PendingModificationReviewer) %>') && temparry[3] == "True" && temparry[5] == "True")) {
                        IsEnableAccept = false;
                    }
                }
                if (params.length > 0) {
                    if (IsEnableAccept == false)
                        btnAccept.disabled = true;
                    else
                        btnAccept.disabled = false;
                }
            }
        }

        function EnableDisableReSubmitnButton(params) {
            var IsEnableSubmit = true;
            var btnSubmit = document.getElementById('<%=btnSubmit.ClientID %>');
            if (btnSubmit != null && btnSubmit != "undefined") {
                for (i = 0; i < params.length; i++) {
                    var temparry = params[i].toString().split("^");
                    if (!(temparry[0] == '<%= Convert.ToInt32(WebEnums.ReconciliationStatus.Reviewed) %>' && temparry[3] == "True" && temparry[5] == "True")) {
                        IsEnableSubmit = false;
                    }
                }
                if (params.length > 0) {
                    if (IsEnableSubmit == false)
                        btnSubmit.disabled = true;
                    else
                        btnSubmit.disabled = false;
                }
            }
        }

        function EnableDisableApproveAcceptButton(params) {
            var IsEnableAccept = true;
            var btnAccept = document.getElementById('<%=btnAccept.ClientID %>');
            if (btnAccept != null && btnAccept != "undefined") {
                for (i = 0; i < params.length; i++) {
                    var temparry = params[i].toString().split("^");
                    if (!(temparry[0] == '<%= Convert.ToInt32(WebEnums.ReconciliationStatus.PendingApproval) %>' && temparry[3] == "True")) {
                        IsEnableAccept = false;
                    }
                }
                if (params.length > 0) {
                    if (IsEnableAccept == false)
                        btnAccept.disabled = true;
                    else
                        btnAccept.disabled = false;
                }
            }
        }

        function EnableDisableApproveSubmitnButton(params, IsSystemReconcilied) {
            var IsEnableSubmit = true;
            var btnSubmit = document.getElementById('<%=btnSubmit.ClientID %>');
            if (btnSubmit != null && btnSubmit != "undefined") {
                for (i = 0; i < params.length; i++) {
                    var temparry = params[i].toString().split("^");
                    if (!(temparry[0] == '<%= Convert.ToInt32(WebEnums.ReconciliationStatus.Approved) %>' && temparry[3] == "True")) {
                        IsEnableSubmit = false;
                    }
                }
                if (params.length > 0) {
                    if (IsEnableSubmit == false)
                        btnSubmit.disabled = true;
                    else
                        btnSubmit.disabled = false;
                }
            }
        }

    </script>
    <asp:HiddenField ID="MyHiddenField" runat="server" />
    <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlErrorMessageForSkippedRecPeriod" runat="server">
                <table width="100%">
                    <tr>
                        <td width="100%" align="center">
                            <webControls:ExLabel ID="lblError" runat="server" LabelID="1051" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>&nbsp;&nbsp;
                            <webControls:ExLabel ID="lblErrorMessageForSkippedRecPeriod" LabelID="5000183" SkinID="Black11ArialNormal"
                                runat="server"></webControls:ExLabel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlAcctViewer" runat="server">
                <table style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <webControls:ExLabel ID="lblReportingCurrency" runat="server" SkinID="Black11Arial"
                                LabelID="1424 " FormatString="{0}:" />
                            <webControls:ExLabel ID="lblReportingCurrencyValue" runat="server" SkinID="LeftInfoPaneValue" />
                            &nbsp; &nbsp; &nbsp; &nbsp;
                            <webControls:ExLabel ID="lblDueDate" runat="server" SkinID="Black11Arial" LabelID="1499  "
                                FormatString="{0}:" />
                            <webControls:ExLabel ID="lblDueDateValue" runat="server" SkinID="LeftInfoPaneValue" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">
                                <UserControl:SkyStemARTGrid ID="ucSkyStemARTGrid" runat="server" OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound"
                                    Grid-AllowPaging="True" Grid-AllowCustomPaging="True" Grid-AllowExportToExcel="True"
                                    Grid-AllowCustomFilter="False" Grid-AllowRefresh="True" Grid-AllowCustomization="True"
                                    GridType="AccountViewerSRA" Grid-AllowExportToPDF="True" CustomPaging="true"
                                    Grid-MasterTableView-DataKeyNames="NetAccountID,GLDataID,ReconciliationStatusID"
                                    OnGridDetailTableDataBind="ucSkyStemARTGrid_GridDetailTableDataBind" Grid-EntityNameLabelID="1075"
                                    Grid-MasterTableView-ClientDataKeyNames="NetAccountID,GLDataID,ReconciliationStatusID,IsSystemReconcilied,IsLocked,IsEditable,IsRCCValidation">
                                    <DetailTables>
                                        <telerik:GridTableView Name="NetAccountDetails" runat="server" DataKeyNames="NetAccountID,GLDataID"
                                            AllowPaging="false" AllowSorting="false" Width="100%">
                                            <Columns>
                                                <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key2" UniqueName="Key2">
                                                    <ItemTemplate>
                                                        <webControls:ExHyperLink ID="hlKey2" runat="server"></webControls:ExHyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="10%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key3" UniqueName="Key3">
                                                    <ItemTemplate>
                                                        <webControls:ExHyperLink ID="hlKey3" runat="server"></webControls:ExHyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="10%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key4" UniqueName="Key4">
                                                    <ItemTemplate>
                                                        <webControls:ExHyperLink ID="hlKey4" runat="server"></webControls:ExHyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="10%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key5" UniqueName="Key5">
                                                    <ItemTemplate>
                                                        <webControls:ExHyperLink ID="hlKey5" runat="server"></webControls:ExHyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="10%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key6" UniqueName="Key6">
                                                    <ItemTemplate>
                                                        <webControls:ExHyperLink ID="hlKey6" runat="server"></webControls:ExHyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="10%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key7" UniqueName="Key7">
                                                    <ItemTemplate>
                                                        <webControls:ExHyperLink ID="hlKey7" runat="server"></webControls:ExHyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="10%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key8" UniqueName="Key8">
                                                    <ItemTemplate>
                                                        <webControls:ExHyperLink ID="hlKey8" runat="server"></webControls:ExHyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="10%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key9" UniqueName="Key9">
                                                    <ItemTemplate>
                                                        <webControls:ExHyperLink ID="hlKey9" runat="server"></webControls:ExHyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="10%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1337" SortExpression="FSCaption"
                                                    UniqueName="FSCaption" Visible="false">
                                                    <ItemTemplate>
                                                        <webControls:ExHyperLink ID="hlFSCaption" runat="server"></webControls:ExHyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="15%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1363" SortExpression="AccountType"
                                                    UniqueName="AccountType" DataType="System.String" Visible="false">
                                                    <ItemTemplate>
                                                        <webControls:ExHyperLink ID="hlAccountType" runat="server"></webControls:ExHyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="10%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1357" SortExpression="AccountNumber"
                                                    DataType="System.String" UniqueName="AccountNumber">
                                                    <ItemTemplate>
                                                        <webControls:ExHyperLink ID="hlAccountNumber" runat="server" SkinID="GridHyperLink" />
                                                    </ItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1346" SortExpression="AccountName"
                                                    DataType="System.String" UniqueName="AccountName">
                                                    <ItemTemplate>
                                                        <webControls:ExHyperLink ID="hlAccountName" runat="server" SkinID="GridHyperLink" />
                                                    </ItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <%--<telerikWebControls:ExGridTemplateColumn LabelID="1370" SortExpression="ReconciliationStatus" DataType="System.String"
                                        UniqueName="ReconciliationStatus" >
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlReconciliationStatus" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1374" SortExpression="CertificationStatus" DataType="System.String"
                                        UniqueName="CertificationStatus" >
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlCertificationStatus" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>--%>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1382" SortExpression="GLBalanceReportingCurrency"
                                                    DataType="System.Decimal" UniqueName="GLBalance">
                                                    <ItemTemplate>
                                                        <webControls:ExHyperLink ID="hlGLBalance" runat="server" SkinID="GridHyperLink" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <%--<telerikWebControls:ExGridTemplateColumn LabelID="1385" SortExpression="ReconciliationBalanceReportingCurrency" DataType="System.Decimal"
                                        UniqueName="RecBalance">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlReconciliationBalance" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1678" SortExpression="UnexplainedVarianceReportingCurrency" DataType="System.Decimal"
                                        UniqueName="UnexplainedVar">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlUnexplainedVariance" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1425 " SortExpression="WriteOffAmmount" DataType="System.Decimal"
                                        UniqueName="WriteOnOff">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlWriteOnOff" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1372" SortExpression="Materiality" DataType="System.String"
                                        UniqueName="Materiality">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlMateriality" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1256" SortExpression="ZeroBalance" DataType="System.String"
                                        UniqueName="ZeroBalance">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlZeroBalance" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1014"  SortExpression="KeyAccount" DataType="System.String"
                                        UniqueName="KeyAccount">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlKeyAccount" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1013" SortExpression="RiskRating" DataType="System.String"
                                        UniqueName="RiskRating">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlRiskRating" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1130" SortExpression="PreparerFullName" DataType="System.String"
                                        UniqueName="Preparer">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlPreparer" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1131" SortExpression="ReviewerFullName" DataType="System.String"
                                        UniqueName="Reviewer">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlReviewer" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1132" SortExpression="ApproverFullName" DataType="System.String"
                                        UniqueName="Approver">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlApprover" runat="server" SkinID="GridHyperLink"/>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>--%>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <SkyStemGridColumnCollection>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1357" SortExpression="AccountNumber"
                                            DataType="System.String" UniqueName="AccountNumber">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlAccountNumber" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="6%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1346" SortExpression="AccountName"
                                            DataType="System.String" UniqueName="AccountName">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlAccountName" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="14%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1370" SortExpression="ReconciliationStatus"
                                            DataType="System.String" UniqueName="ReconciliationStatus">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlReconciliationStatus" runat="server" SkinID="GridHyperLinkWithUnderline" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1374" SortExpression="CertificationStatus"
                                            DataType="System.String" UniqueName="CertificationStatus">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlCertificationStatus" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1382" SortExpression="GLBalanceReportingCurrency"
                                            DataType="System.Decimal" UniqueName="GLBalance">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlGLBalance" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" Width="15%" />
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1385" SortExpression="ReconciliationBalanceReportingCurrency"
                                            DataType="System.Decimal" UniqueName="RecBalance">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlReconciliationBalance" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" Width="15%" />
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1425 " SortExpression="WriteOnOffAmountReportingCurrency"
                                            DataType="System.Decimal" UniqueName="WriteOnOff">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlWriteOnOff" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" Width="15%" />
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1678" SortExpression="UnexplainedVarianceReportingCurrency"
                                            DataType="System.Decimal" UniqueName="UnexplainedVar">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlUnexplainedVariance" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" Width="15%" />
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1372" SortExpression="Materiality"
                                            DataType="System.String" UniqueName="Materiality">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlMateriality" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1256" SortExpression="ZeroBalance"
                                            DataType="System.String" UniqueName="ZeroBalance">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlZeroBalance" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1014" SortExpression="KeyAccount"
                                            DataType="System.String" UniqueName="KeyAccount">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlKeyAccount" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1013" SortExpression="RiskRating"
                                            DataType="System.String" UniqueName="RiskRating">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlRiskRating" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1130" SortExpression="PreparerFullName"
                                            DataType="System.String" UniqueName="Preparer">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlPreparer" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2501" SortExpression="BackupPreparerFullName"
                                            DataType="System.String" UniqueName="BackupPreparer" Visible="false">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlBackupPreparer" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1131" SortExpression="ReviewerFullName"
                                            DataType="System.String" UniqueName="Reviewer">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlReviewer" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2502" SortExpression="BackupReviewerFullName"
                                            DataType="System.String" UniqueName="BackupReviewer" Visible="false">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlBackupReviewer" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1132" SortExpression="ApproverFullName"
                                            DataType="System.String" UniqueName="Approver">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlApprover" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2503" SortExpression="BackupApproverFullName"
                                            DataType="System.String" UniqueName="BackupApprover" Visible="false">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlBackupApprover" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2041" SortExpression="SystemReconciliationRuleNumber"
                                            DataType="System.String" UniqueName="SRRNumber">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlSRRNumber" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="3%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1417" SortExpression="PreparerDueDate"
                                            DataType="System.DateTime" UniqueName="PreparerDueDate" Visible="false">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlPreparerDueDate" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1418" SortExpression="ReviewerDueDate"
                                            DataType="System.DateTime" UniqueName="ReviewerDueDate" Visible="false">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlReviewerDueDate" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1738" SortExpression="ApproverDueDate"
                                            DataType="System.DateTime" UniqueName="ApproverDueDate" Visible="false">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlApproverDueDate" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2767" SortExpression="CompletedTaskCount"
                                            DataType="System.Int32" UniqueName="TMStatus" Visible="false">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlTMStatus" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </SkyStemGridColumnCollection>
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="true" />
                                        <ClientEvents OnRowSelected="RowSelected" />
                                        <ClientEvents OnRowDeselected="RowDeselected" />
                                    </ClientSettings>
                                </UserControl:SkyStemARTGrid>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td align="right">
                            <webControls:ExButton ID="btnAccept" ValidationGroup="Accept" runat="server" LabelID="1481 "
                                OnClick="btnAccept_Click" SkinID="ExButton100" />
                            <webControls:ExButton ID="btnSubmit" ValidationGroup="Submit" runat="server" LabelID="1238  "
                                OnClick="btnSubmit_Click" SkinID="ExButton100" />
                            <webControls:ExButton ID="btnRemoveSignOff" ValidationGroup="RemoveSignOff" runat="server"
                                LabelID="1627  " OnClick="btnRemoveSignOff_Click" SkinID="ExButton200" />
                            <webControls:ExButton ID="btnReopen" runat="server" LabelID="1764" OnClick="btnReopen_Click"
                                SkinID="ExButton100" ValidationGroup="Reopen" OnClientClick="return confirmReOpen(this);" />
                            <webControls:ExButton ID="btnReset" runat="server" LabelID="2482" OnClientClick="return confirmReset(this);"
                                SkinID="ExButton100" ValidationGroup="Reset" OnClick="btnReset_Click" />
                            <asp:CustomValidator ID="cvAccept" runat="server" Text="!" OnServerValidate="cvAccept_ServerValidate"
                                ValidationGroup="Accept" Font-Bold="true" Font-Size="Medium"></asp:CustomValidator>
                            <asp:CustomValidator ID="cvReopen" runat="server" Text="!" OnServerValidate="cvReopen_ServerValidate"
                                ValidationGroup="Reopen" Font-Bold="true" Font-Size="Medium"></asp:CustomValidator>
                            <asp:CustomValidator ID="cvSubmit" runat="server" Text="!" OnServerValidate="cvSubmit_ServerValidate"
                                ValidationGroup="Submit" Font-Bold="true" Font-Size="Medium"></asp:CustomValidator>
                            <asp:CustomValidator ID="cvRemoveSignOff" runat="server" Text="!" OnServerValidate="cvRemoveSignOff_ServerValidate"
                                ValidationGroup="RemoveSignOff" Font-Bold="true" Font-Size="Medium"></asp:CustomValidator>
                            <asp:CustomValidator ID="cvReSet" runat="server" Text="!" OnServerValidate="cvReSet_ServerValidate"
                                ValidationGroup="Reset" Font-Bold="true" Font-Size="Medium"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                    </tr>
                    <tr>
                        <td colspan="2">
                            <UserControls:Legend ID="ucLegend" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <UserControls:ProgressBar ID="ucProgressBarMain" runat="server" AssociatedUpdatePanelID="upnlMain" />
                        </td>
                    </tr>
                </table>
                <input type="text" id="Sel" runat="server" style="display: none" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        function Selecting(sender, args) {
            var bSelectRow = true;
            var inp = document.getElementById('<% =this.Sel.ClientID %>');
            var data = inp.value;
            var a = Array;
            if (data != "") {
                var rowsData = data.split(":");
                var i = 0;
                while (typeof (rowsData[i]) != "undefined") {
                    if (rowsData[i++] == args.get_itemIndexHierarchical()) {
                        bSelectRow = false;
                        break;
                    }
                }
            }
            if (bSelectRow == true)
                args.set_cancel(false);
            else
                args.set_cancel(true);
        }

        function confirmReOpen(btn) {
            var grid = $find("<%= ucSkyStemARTGrid.RgAccount.ClientID %>");
            if (grid != null && grid != "undefined") {
                var gridSelectedItems = grid.get_selectedItems();
                if (gridSelectedItems.length <= 0) {
                    alert('<% = Helper.GetAlertMessageFromLabelID(WebConstants.NO_SELECTION_ERROR_MESSAGE) %>');
                                    return false;
                                }
                            }
                            return confirm('<% = Helper.GetAlertMessageFromLabelID(WebConstants.CONFIRM_FOR_RE_OPEN) %>');
        }
        function confirmReset(btn) {
            var grid = $find("<%= ucSkyStemARTGrid.RgAccount.ClientID %>");
            if (grid != null && grid != "undefined") {
                var gridSelectedItems = grid.get_selectedItems();
                if (gridSelectedItems.length <= 0) {
                    alert('<% = Helper.GetAlertMessageFromLabelID(WebConstants.NO_SELECTION_ERROR_MESSAGE) %>');
                    return false;
                }
            }
            return confirm('<% = Helper.GetAlertMessageFromLabelID(WebConstants.CONFIRM_FOR_RE_SET) %>');
        }


    </script>

</asp:Content>
