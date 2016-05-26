<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master"
    AutoEventWireup="true" Inherits="Pages_GridApplyFilter"
    Theme="SkyStemBlueBrown" Codebehind="GridApplyFilter.aspx.cs" %>

<%@ Register Src="../UserControls/AccountViewerFilter/StringRange.ascx" TagName="StringRange"
    TagPrefix="acctFilter" %>
<%@ Register Src="~/UserControls/AccountViewerFilter/DateRange.ascx" TagName="DateRange"
    TagPrefix="acctFilter" %>
<%@ Register Src="~/UserControls/AccountViewerFilter/CheckBoxListWithSelectAll.ascx"
    TagName="CheckBoxList" TagPrefix="acctFilter" %>
<%@ Register Src="~/UserControls/AccountViewerFilter/AcctFltrRadioButtonListYesNoAll.ascx"
    TagName="YesNoAllRBtnLst" TagPrefix="acctFilter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updAutoComplete" runat="server">
        <ContentTemplate>
            <table id="tblMain" cellpadding="0" cellspacing="0" width="100%">
                <asp:Label ID="Label1" runat="server" Width="410px"></asp:Label>
                <tr>
                    <td>
                        <webControls:ExLabel ID="lblSelectColumns" runat="server" LabelID="1973" SkinID="Black11Arial"></webControls:ExLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlCriteria" runat="server">
                            <table id="tblCriteria" cellpadding="0" cellspacing="0" width="100%" border="0" class="TableSameAsGrid">
                                <colgroup>
                                    <col width="20%" />
                                    <col width="20%" />
                                    <col width="55%" />
                                    <col width="5%" />
                                </colgroup>
                                <tr class="TableHeaderSameAsGrid">
                                    <td>
                                        <webControls:ExLabel ID="lblFieldName" runat="server" LabelID="1942"></webControls:ExLabel>
                                    </td>
                                    <td>
                                        <webControls:ExLabel ID="lblOperator" runat="server" LabelID="1943"></webControls:ExLabel>
                                    </td>
                                    <td colspan="2">
                                        <webControls:ExLabel ID="lblValue" runat="server" LabelID="1944"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr class="TableRowSameAsGrid" style="height: 3px">
                                    <td colspan="4">
                                    </td>
                                </tr>
                                <tr class="TableRowSameAsGrid" align="left">
                                    <td valign="top">
                                        <asp:DropDownList ID="ddlFieldName" runat="server" AutoPostBack="true" CausesValidation="false"
                                            OnSelectedIndexChanged="ddlFieldName_SelectedIndexChanged" SkinID="DropDownList150">
                                        </asp:DropDownList>
                                    </td>
                                    <td valign="top">
                                        <asp:DropDownList ID="ddlOperatorName" runat="server" AutoPostBack="true" CausesValidation="false"
                                            OnSelectedIndexChanged="ddlOperatorName_SelectedIndexChanged" SkinID="DropDownList150">
                                        </asp:DropDownList>
                                    </td>
                                    <td valign="top">
                                        <webControls:ExTextBox ID="acctfltrEqual" runat="server" ErrorPhraseID="5000191"
                                            IsRequired="true" SkinID="ExTextBox150" />
                                        <acctFilter:StringRange ID="acctfltrStringRange" runat="server" ErrorLabelID="5000191"
                                            isRequired="true" />
                                        <acctFilter:DateRange ID="acctfltrDateRange" runat="server" />
                                        <acctFilter:CheckBoxList ID="acctfltrCheckBoxList" runat="server" ErrorLabelID="5000191"
                                            isRequired="true" />
                                        <acctFilter:YesNoAllRBtnLst ID="acctfltrYesNoAll" runat="server" isRequired="true"
                                            ErrorLabelID="5000191" />
                                        <asp:TextBox ID="acctfltrAutoSuggest" runat="server" SkinID="ExTextBox150" MaxLength="50" />
                                        <webControls:ExRequiredFieldValidator ID="rfvFSCaption" runat="server" ControlToValidate="acctfltrAutoSuggest"
                                            LabelID="5000191" Display="Dynamic"></webControls:ExRequiredFieldValidator>
                                        <ajaxToolkit:AutoCompleteExtender TargetControlID="acctfltrAutoSuggest" ServiceMethod="AutoCompleteFSCaption"
                                            runat="server" ID="aceFSCaption" OnClientPopulating="ShowFSCaptionProgressIcon"
                                            OnClientPopulated="ShowFSCaptionProgressIcon">
                                        </ajaxToolkit:AutoCompleteExtender>
                                        <webControls:ExCalendar ID="calFlterDateEqual" runat="server" Width="100" IsRequired="true"
                                            ErrorPhraseID="5000191"></webControls:ExCalendar>
                                        <asp:CustomValidator ID="cvStringRangeDataType" runat="server" Text="!" Font-Bold="true"
                                            OnServerValidate="cvStringRangeDataType_OnServerValidate"></asp:CustomValidator>
                                    </td>
                                    <td valign="top">
                                        <img id="imgFSCaptionProgress" style="visibility: hidden" alt="imgProgress" src="../App_Themes/SkyStemBlueBrown/Images/progress_small.gif" />
                                    </td>
                                </tr>
                                <tr class="TableRowSameAsGrid" style="height: 3px">
                                    <td colspan="4">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="3">
                        <webControls:ExButton ID="btnAddMore" runat="server" CausesValidation="true" LabelID="1344"
                            OnClick="btnAddMore_Click" SkinID="ExButton100" />
                        <webControls:ExButton ID="btnApplyFilter" runat="server" CausesValidation="false"
                            LabelID="1936" OnClick="btnApplyFilter_Click" SkinID="ExButton100" />
                    </td>
                </tr>
                <tr>
                    <td><%--<asp:Panel ID="pnlShowCriteria" runat="server" ScrollBars="Auto" Visible="true" Width="99%">--%>
                        <table id="tblFltrCriteria" runat="server" border="0" cellpadding="0" cellspacing="0" class="TableSameAsGrid" width="100%">
                            <tr>
                                <td colspan="4">
                                    <webControls:ExLabel ID="lblSelectedCriteria" runat="server" LabelID="1974" SkinID="SubSectionHeading"></webControls:ExLabel>
                                </td>
                            </tr>
                            <tr class="TableHeaderSameAsGrid">
                                <td width="20%">
                                    <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="1942"></webControls:ExLabel>
                                </td>
                                <td width="20%">
                                    <webControls:ExLabel ID="ExLabel2" runat="server" LabelID="1943"></webControls:ExLabel>
                                </td>
                                <td width="50%">
                                    <webControls:ExLabel ID="ExLabel3" runat="server" LabelID="1944"></webControls:ExLabel>
                                </td>
                                <td width="10%">&nbsp; </td>
                            </tr>
                        </table>
                        <%--</asp:Panel>--%></td>
                </tr>
            </table>
            <asp:Button ID="btnDummy" runat="server" OnClick="btnDummy_Click" CausesValidation="false" />
            <asp:HiddenField ID="hdnField" runat="server" />
            <%--<ajaxToolkit:RoundedCornersExtender Corners="All" BorderColor="AntiqueWhite" Radius="6"
        TargetControlID="pnlShowCriteria" ID="RoundedCornersExtender1" runat="server">
    </ajaxToolkit:RoundedCornersExtender>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function deleteClick(btnDelete) {
            var btn = $get('<% =this.btnDummy.ClientID %>');
            var hdn = $get('<% =this.hdnField.ClientID %>');
            if (hdn != null) {
                //hdn.value = btnDelete.paramID;
                hdn.value = btnDelete.getAttribute('paramID');
            }
            btn.click();
            return false;
        }
    </script>

</asp:Content>
