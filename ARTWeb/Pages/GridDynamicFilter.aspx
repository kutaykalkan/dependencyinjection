<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master"
    AutoEventWireup="true" Inherits="GridDynamicFilter"
    Theme="SkyStemBlueBrown" Codebehind="GridDynamicFilter.aspx.cs" %>

<%--<%@ Register Src="../UserControls/AccountViewerFilter/StringRange.ascx" TagName="StringRange"
    TagPrefix="acctFilter" %>
--%>
<%@ Register Src="~/UserControls/AccountViewerFilter/DateRange.ascx" TagName="DateRange"
    TagPrefix="acctFilter" %>
<%@ Register Src="~/UserControls/AccountViewerFilter/CheckBoxListWithSelectAll.ascx"
    TagName="CheckBoxList" TagPrefix="acctFilter" %>
<%--<%@ Register Src="~/UserControls/AccountViewerFilter/AcctFltrRadioButtonListYesNoAll.ascx"
    TagName="YesNoAllRBtnLst" TagPrefix="acctFilter" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function deleteClick(btnDelete) {
            var btn = $get('<% =this.btnDummy.ClientID %>');
            var hdn = $get('<% =this.hdnField.ClientID %>');
            if (hdn != null) {
                hdn.value = btnDelete.getAttribute('columnID');
            }
            btn.click();
            return false;
        }
    </script>

    <script type="text/javascript" language="javascript">
        function CallApplyFilterAndClose() {
            alert('GDF');
            //            if (Page_IsValid) {
            //                debugger;
            //                var wnd1 = GetRadWindow();
            //                var mgr = wnd1.get_windowManager();
            //                var wnd2 = mgr.getWindowByName("BCPopupWindow");
            //                wnd2.get_contentFrame().contentWindow.CallParentFunction();
            //                wnd1.close();
            //            }
        }
    </script>

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
                                    <col width="59%" />
                                    <col width="1%" />
                                </colgroup>
                                <tr class="TableHeaderSameAsGrid">
                                    <td align="center">
                                        <webControls:ExLabel ID="lblFieldName" runat="server" LabelID="1942"></webControls:ExLabel>
                                    </td>
                                    <td align="center">
                                        <webControls:ExLabel ID="lblOperator" runat="server" LabelID="1943"></webControls:ExLabel>
                                    </td>
                                    <td colspan="2" align="left" style="padding-left:60px;">
                                        <webControls:ExLabel ID="lblValue" runat="server" LabelID="1944"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr class="TableRowSameAsGrid" style="height: 3px">
                                    <td colspan="4">
                                    </td>
                                </tr>
                                <tr class="TableRowSameAsGrid" align="center">
                                    <td valign="top" align="center">
                                        <asp:DropDownList ID="ddlFieldName" runat="server" AutoPostBack="true" CausesValidation="false"
                                            OnSelectedIndexChanged="ddlFieldName_SelectedIndexChanged" SkinID="DropDownList150">
                                        </asp:DropDownList>
                                    </td>
                                    <td valign="top" align="center">
                                        <asp:DropDownList ID="ddlOperatorName" runat="server" AutoPostBack="true" CausesValidation="false"
                                            OnSelectedIndexChanged="ddlOperatorName_SelectedIndexChanged" SkinID="DropDownList150">
                                        </asp:DropDownList>
                                    </td>
                                    <td valign="top" align="left" style="padding-left:20px;">
                                        <webControls:ExTextBox ID="acctfltrEqual" runat="server" ErrorPhraseID="5000191"
                                            IsRequired="true" SkinID="ExTextBox150" />
                                        <%--<acctFilter:StringRange ID="acctfltrStringRange" runat="server" ErrorLabelID="5000191"
                                            isRequired="true" --%>
                                        <acctFilter:DateRange ID="acctfltrDateRange" runat="server" />
                                        <%-- <acctFilter:CheckBoxList ID="acctfltrCheckBoxList" runat="server" ErrorLabelID="5000191"
                                            isRequired="true" />--%>
                                        <%-- <acctFilter:YesNoAllRBtnLst ID="acctfltrYesNoAll" runat="server" isRequired="true"
                                            ErrorLabelID="5000191" />--%>
                                        <asp:TextBox ID="acctfltrAutoSuggest" runat="server" SkinID="ExTextBox150" MaxLength="50"
                                            Visible="false"></asp:TextBox>
                                        <webControls:ExRequiredFieldValidator ID="rfvFSCaption" runat="server" ControlToValidate="acctfltrAutoSuggest"
                                            Visible="false" LabelID="5000191" Display="Dynamic"></webControls:ExRequiredFieldValidator>
                                        <ajaxToolkit:AutoCompleteExtender TargetControlID="acctfltrAutoSuggest" ServiceMethod="AutoCompleteFSCaption"
                                            runat="server" ID="aceFSCaption" OnClientPopulating="ShowFSCaptionProgressIcon"
                                            OnClientPopulated="ShowFSCaptionProgressIcon">
                                        </ajaxToolkit:AutoCompleteExtender>
                                    </td>
                                    <td valign="top">
                                        <!-- <img id="imgFSCaptionProgress" style="visibility: hidden" alt="imgProgress" src="../App_Themes/SkyStemBlueBrown/Images/progress_small.gif" /> -->
                                        <img id="img1" style="visibility: hidden" alt="imgProgress" />
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
                    <td>
                        <%--<asp:Panel ID="pnlShowCriteria" runat="server" ScrollBars="Auto" Visible="true" Width="99%">--%>
                        <table id="tblFltrCriteria" runat="server" border="0" cellpadding="0" cellspacing="0"
                            class="TableSameAsGrid" width="100%">
                            <tr>
                                <td colspan="4">
                                    <webControls:ExLabel ID="lblSelectedCriteria" runat="server" LabelID="1974" SkinID="SubSectionHeading">
                                    </webControls:ExLabel>
                                </td>
                            </tr>
                            <tr class="TableHeaderSameAsGrid">
                                <td width="20%">
                                    <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="1942">
                                    </webControls:ExLabel>
                                </td>
                                <td width="20%">
                                    <webControls:ExLabel ID="ExLabel2" runat="server" LabelID="1943">
                                    </webControls:ExLabel>
                                </td>
                                <td width="50%">
                                    <webControls:ExLabel ID="ExLabel3" runat="server" LabelID="1944">
                                    </webControls:ExLabel>
                                </td>
                                <td width="10%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <%--</asp:Panel>--%>
                    </td>
                </tr>
                <%--<tr>
                    <td colspan="4">
                        <webControls:ExLabel ID="lblClause" runat="server" LabelID="2224" SkinID="SubSectionHeading">
                        </webControls:ExLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <webControls:ExLabel ID="lblSelectedClause" runat="server" SkinID="StatusHeading"></webControls:ExLabel>
                    </td>
                </tr>--%>
                </caption>
            </table>
            <asp:Button ID="btnDummy" runat="server" OnClick="btnDummy_Click" CausesValidation="false" />
            <asp:HiddenField ID="hdnField" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
