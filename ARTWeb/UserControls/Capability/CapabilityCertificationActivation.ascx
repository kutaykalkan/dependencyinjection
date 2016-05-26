<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_CapabilityCertificationActivation" Codebehind="CapabilityCertificationActivation.ascx.cs" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnlMain" runat="server">
            <%--SkinID="CapabilityPanel"--%>
            <asp:Panel ID="pnlYesNo" runat="server" Width="100%">
                <table width="100%" cellpadding="0" cellspacing="0" class="InputRequrementsHeading">
                    <tr>
                        <td class="ManadatoryField">
                            *
                        </td>
                        <td class="tdCapabilityYesOpt">
                            <webControls:ExLabel ID="lblCertificationActivation" runat="server" LabelID="1019"
                                FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel><%--1253--%>
                        </td>
                        <td class="tdCapabilityNoOpt">
                            <webControls:ExRadioButton ID="optCertificationActivationYes" GroupName="optCertificationActivation"
                                runat="server" OnCheckedChanged="optCertificationActivationYes_CheckedChanged"
                                AutoPostBack="true" LabelID="1252" SkinID="OptBlack11Arial" />
                        </td>
                        <td class="tdCapabilityStatusIcon" >
                            <webControls:ExRadioButton ID="optCertificationActivationNo" GroupName="optCertificationActivation"
                                runat="server" OnCheckedChanged="optCertificationActivationNo_CheckedChanged"
                                AutoPostBack="true" LabelID="1251" SkinID="OptBlack11Arial" />
                        </td>
                        <td>
                            <webControls:ExImage ID="imgStatusCertificationActivation" runat="server" SkinID="CapabilityUpdateStatus" />
                        </td>
                        <td width="4%" align="right">
                            <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlContent" runat="server">
                <table width="100%" class="InputRequrementsTextNoBackColor" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlContentCertificationActivation" runat="server" SkinID="pnlExtended"
                                Width="100%">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <telerikWebControls:ExRadGrid ID="rdCertificationActivation" runat="server" EntityNameLabelID="1421 "
                                                AllowPaging="true" AllowSorting="false" OnItemDataBound="rdCertificationActivation_ItemDataBound"
                                                  AllowExportToExcel="true" AllowExportToPDF ="true" AllowPrint="true" AllowPrintAll="true"  Width="100%">
                                                <%-- AllowMultiRowEdit = "True" OnItemCreated="rdSystemWideSettings_ItemCreated"--%>
                                                <%--<MasterTableView DataKeyNames="ReconciliationCloseDate" EditMode="InPlace">
                                                --%><MasterTableView DataKeyNames="ReconciliationPeriodID" EditMode="InPlace">
                                                    <Columns>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1420" SortExpression="PeriodEndDate"
                                                            HeaderStyle-Width="30%">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblRecPeriod" runat="server" SkinID="Black11Arial" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1453   " SortExpression="PrepareDueDate">
                                                            <ItemTemplate>
                                                                <webControls:ExCalendar  ID="calCertificationStartDate" runat="server" SkinID="" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1454   " SortExpression="ReviewerDueDate">
                                                            <ItemTemplate>
                                                                <webControls:ExCheckBox ID="cbAllowCertificationLockDown" runat="server"    OnClick="ShowHide( this, calCertificationLockDownDate)" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1186  " SortExpression="">
                                                            <ItemTemplate>
                                                                <webControls:ExCalendar   ID="calCertificationLockDownDate" runat="server" SkinID="" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerikWebControls:ExRadGrid>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td align="right">
                                            <webControls:ExButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" LabelID="1315"
                                                SkinID="" />
                                            <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClientClick="CollapseOnCancel()"
                                                OnClick="btnCancel_Click" CausesValidation="false" SkinID="" />
                                            <input id="inputReset" runat="server" class="displayNone" type="reset" />
                                        </td>
                                    </tr>--%>
                                    <%--<tr>
                                        <td colspan="3">
                                            <UserControls:ProgressBar ID="ucProgressBar" runat="server" AssociatedUpdatePanelID="upnlMain" />
                                        </td>
                                    </tr>--%>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:CollapsiblePanelExtender ID="cpeCertificationActivation" TargetControlID="pnlContent"
                ImageControlID="imgCollapse" CollapseControlID="imgCollapse" ExpandControlID="imgCollapse"
                runat="server" SkinID="CollapsiblePanel">
            </ajaxToolkit:CollapsiblePanelExtender>
        </asp:Panel>
         <script type="text/javascript" language="javascript">
//            function ShowHide(sender, args) {
            function ShowHide(cbAllowCertificationLockDownClientID,calCertificationLockDownDateClientID) {
            var cbAllowCertificationLockDown = document.getElementById(cbAllowCertificationLockDownClientID);
            var calCertificationLockDownDate = document.getElementById(calCertificationLockDownDateClientID);
//                var objExtender = $find("<%=cpeCertificationActivation.ClientID%>");
//                try { objExtender._doClose(); } catch (e) { }  // Collapse it
//            

if (cbAllowCertificationLockDown.checked == true )
{
calCertificationLockDownDate.disabled="" ;
    }
    else if(cbAllowCertificationLockDown.checked == false)
    {
    calCertificationLockDownDate.disabled="disabled" ;
    }
}
        </script>
    </ContentTemplate>
</asp:UpdatePanel>
