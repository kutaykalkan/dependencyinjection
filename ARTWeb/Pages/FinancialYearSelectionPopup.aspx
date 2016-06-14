<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true" Inherits="FinancialYearSelectionPopup"
    Title="Untitled Page" Theme="SkyStemBlueBrown" Codebehind="FinancialYearSelectionPopup.aspx.cs" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="4">
                <%--<webControls:ExRequiredFieldValidator ID="rfvFY" runat="server" InitialValue="-2" Display="None"  ControlToValidate="ddlFySelection"></webControls:ExRequiredFieldValidator>--%>
                <%--<webControls:ExCustomValidator ID="cvFYSelection" runat="server" Display="None" OnServerValidate="cvFYSelection_OnServerValidate"></webControls:ExCustomValidator>--%>
                <webControls:ExCustomValidator ID="cvRecPeriodExist" runat="server" Display="None"
                    OnServerValidate="cvRecPeriodExist_OnServerValidate" ></webControls:ExCustomValidator>
                    <webControls:ExCustomValidator ID="cvRecPeriodSelected" runat="server" Display="None"
                    OnServerValidate="cvRecPeriodSelected_OnServerValidate" ></webControls:ExCustomValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 5%">
            &nbsp;
            </td>
            <td style="width: 60%">
                <webControls:ExLabel ID="lblCurrentFY" runat="server" LabelID="2016" FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td style="width: 35%" colspan="2">
                <webControls:ExLabel ID="lblCurrentFYValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ManadatoryField" style="width: 5%">
                *&nbsp;
            </td>
            <td style="width: 50%">
                <webControls:ExLabel ID="lblFYSelection" runat="server" LabelID="2011" FormatString="{0}:"  SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td style="width: 40%">
                <asp:DropDownList ID="ddlFySelection" runat="server" SkinID="DropDownList150" ValidationGroup="grpFY">
                </asp:DropDownList>
            </td>
            <td style="width: 5%">
                <webControls:ExRequiredFieldValidator ID="rfvFY" runat="server" InitialValue="-2"
                    Font-Bold="true" Font-Size="Medium" Display="None" ControlToValidate="ddlFySelection"
                    ></webControls:ExRequiredFieldValidator>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 5%">
            </td>
            <td style="width: 55%">
            </td>
            <td style="width: 40%; text-align: right;">
                <webControls:ExButton ID="btnSetFY" runat="server" LabelID="2017" SkinID="Black11Arial"
                    OnClick="btnSetFY_Click"   />
            </td>
        </tr>
    </table>

    <script type="text/javascript" language="javascript">
        function ValidateControl() {
      
            var valSummaryObj = GetValidationSummaryElement();
            valSummaryObj.validationGroup = 'grpFY';
            Page_ClientValidate('grpFY');
            valSummaryObj.validationGroup = '';

        }
    </script>

</asp:Content>
