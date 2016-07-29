<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" Inherits="CreateFinancialYear" Theme="SkyStemBlueBrown" Codebehind="CreateFinancialYear.aspx.cs" %>

<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnlFinancialYear" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                    </td>
                    <td valign="top">
                        <webControls:ExLabel ID="lblFinancialYear" runat="server" FormatString="{0}:" LabelID="2011"
                            SkinID="Black11Arial"></webControls:ExLabel>
                    </td>
                    <td colspan="2">
                        <asp:DropDownList runat="server" ID="ddlFinancialYear" SkinID="DropDownList200" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" CausesValidation="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <%--Blank Row--%>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <%--Button Row--%>
                <tr>
                    <td class="ManadatoryField" valign="top">
                        *
                    </td>
                    <td valign="top">
                        <webControls:ExLabel ID="lblFinancialName" runat="server" FormatString="{0}:" LabelID="2010"
                            SkinID="Black11Arial"></webControls:ExLabel>
                    </td>
                    <td colspan="2">
                        <webControls:ExTextBox ID="txtFinancialName" SkinID="ExTextBox200" MaxLength="10"
                            runat="server" Width="98%" IsRequired="true" />
                    </td>
                </tr>
                <%--Blank Row--%>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <%--Button Row--%>
                <tr>
                    <td class="ManadatoryField" valign="top">
                        *
                    </td>
                    <td valign="top">
                        <webControls:ExLabel ID="lblStartDate" FormatString="{0}:" LabelID="1449 " SkinID="Black11Arial"
                            runat="server"></webControls:ExLabel>
                    </td>
                    <td colspan="2">
                        <webControls:ExCalendar ID="calStartDate" runat="server" SkinID="ExCalendar200"></webControls:ExCalendar>
                        <webControls:ExRequiredFieldValidator ID="rfvCalenderStartDate" runat="server" ControlToValidate="calStartDate"></webControls:ExRequiredFieldValidator>
                        <webControls:ExCustomValidator ID="cvCalenderStartDate" runat="server" ControlToValidate="calStartDate"
                            Text="!" Font-Bold="true" ClientValidationFunction="ValidateStartDate"></webControls:ExCustomValidator>
                    </td>
                </tr>
                <%--Blank Row--%>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <%--Button Row--%>
                <tr>
                    <td class="ManadatoryField" valign="top">
                        *
                    </td>
                    <td valign="top">
                        <webControls:ExLabel ID="lblEndDate" LabelID="1450" FormatString="{0}:" SkinID="Black11Arial"
                            runat="server"></webControls:ExLabel>
                    </td>
                    <td colspan="2">
                        <webControls:ExCalendar ID="calEndDate" runat="server" SkinID="ExCalendar200"></webControls:ExCalendar>
                        <asp:CustomValidator ID="cvCompareWithStartDate" runat="server" Text="!" Font-Bold="true"
                            ClientValidationFunction="CompareSubscriptionStartAndEndDates">  </asp:CustomValidator>
                        <webControls:ExRequiredFieldValidator ID="rfvCalenderEndDate" runat="server" ControlToValidate="calEndDate"></webControls:ExRequiredFieldValidator>
                        <webControls:ExCustomValidator ID="cvcalEndDate" runat="server" Text="!" Font-Bold="true"
                            ControlToValidate="calStartDate" ClientValidationFunction="ValidateEndDate"></webControls:ExCustomValidator>
                    </td>
                </tr>
                <%--Blank Row--%>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <%--Button Row--%>
                <%--Blank Row--%>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <%--Button Row--%>
                <tr>
                    <td align="right" colspan="3" style="padding-right: 140px;">
                        <webControls:ExButton ID="btnUpdate" runat="server" LabelID="1315" OnClick="btnUpdate_Click" />
                        <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" CausesValidation="false" />
                    </td>
                    <td colspan="1">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <usercontrols:progressbar id="ucProgressBarFinancialYear" runat="server" associatedupdatepanelid="upnlFinancialYear" />

    <script language="javascript" type="text/javascript">

        function CompareSubscriptionStartAndEndDates(sender, args) {

            var SubscriptionStartDate = document.getElementById('<%=calStartDate.ClientID%>');
            var SubscriptionEndDate = document.getElementById('<%=calEndDate.ClientID%>');
            var StartDate;
            var EndDate = SubscriptionEndDate.value;
            StartDate = SubscriptionStartDate.value;

            var result = CompareDates(StartDate, EndDate);

            if (result >= 0) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        function ValidateStartDate(sender, args) {
            var SubscriptionEndDate = document.getElementById('<%=calStartDate.ClientID%>');
            args.IsValid = IsDate(SubscriptionEndDate.value);
        }
        function ValidateEndDate(sender, args) {
            var SubscriptionEndDate = document.getElementById('<%=calEndDate.ClientID%>');
            args.IsValid = IsDate(SubscriptionEndDate.value);
        }
        
    
    </script>

</asp:Content>
