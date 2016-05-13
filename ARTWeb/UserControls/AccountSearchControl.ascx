<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AccountSearchControl.ascx.cs"
    Inherits="UserControls_AccountSearchControl"  %>
<%@ Register TagPrefix="OrganizationalHierarchy" TagName="DropDownList" Src="~/UserControls/OrganizationalHierarchyDropdown.ascx" %>
<%@ Register TagPrefix="RiskRating" TagName="DropDownList" Src="~/UserControls/RiskRatingDropDown.ascx" %>

<table width="100%" cellpadding="0" border="0" cellspacing="0">
    <tr id="trOrgHierarchy" runat="server">
        <td width="1%">&nbsp;
        </td>
        <td width="15%">
            <webControls:ExLabel ID="lblGeography" LabelID="1596" runat="server" FormatString="{0}:"
                SkinID="Black11Arial"></webControls:ExLabel>
        </td>
        <td colspan="2">
            <OrganizationalHierarchy:DropDownList ID="ucGeography" runat="server"></OrganizationalHierarchy:DropDownList>
            &nbsp;
            <asp:TextBox ID="txtGeography" runat="server" Width="100" MaxLength="250" />
            &nbsp;
            <webControls:ExImageButton ID="btnAddMore" runat="server" AlternateText="Add More"
                ImageUrl="~/App_Themes/SkyStemBlueBrown/Images/AddMoreFilter.gif" />
            <input type="hidden" id="lstElements" name="ddlElements" runat="server" />
        </td>
        <td width="22%">&nbsp;
        </td>
        <td>&nbsp;
        </td>
        <td rowspan="7">
            <asp:Panel ID="pnlSearchCommand" runat="server" Width="100%">
                <table width="100%">
                    <tr valign="top">
                        <td height="160px" width="145px">
                            <table id="tblSearchCommand" style="font-size: small; vertical-align: top" runat="server">
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr class="BlankRow">
        <td colspan="6"></td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td>
            <webControls:ExLabel ID="lblAccount" LabelID="1712" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
            &nbsp;
            <webControls:ExLabel ID="lblFromAcNu" LabelID="1336" runat="server" FormatString="{0}:"
                SkinID="Black11Arial"></webControls:ExLabel>
        </td>
        <td>
            <webControls:ExTextBox ID="txtAcNumber" runat="server" SkinID="ExTextBox150" MaxLength="20" />
        </td>
        <td style="padding-left: 1cm">
            <webControls:ExLabel ID="lblToAcNu" LabelID="1345" runat="server" FormatString="{0}:"
                SkinID="Black11Arial"></webControls:ExLabel>
        </td>
        <td>
            <webControls:ExTextBox ID="txtToAcNumber" runat="server" SkinID="ExTextBox150" MaxLength="20" />
        </td>
        <td></td>
    </tr>
    <tr class="BlankRow">
        <td colspan="6"></td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td>
            <webControls:ExLabel ID="lblFsCaption" LabelID="1337" runat="server" FormatString="{0}:"
                SkinID="Black11Arial"></webControls:ExLabel>
        </td>
        <td>
            <asp:TextBox ID="txtFsCaption" runat="server" SkinID="ExTextBox150" MaxLength="50" />
            <img id="imgFSCaptionProgress" style="visibility: hidden" alt="imgProgress" src="<%= ResolveClientUrlPath("~/App_Themes/SkyStemBlueBrown/Images/progress_small.gif") %>" />
            <ajaxToolkit:AutoCompleteExtender TargetControlID="txtFsCaption" ServiceMethod="AutoCompleteFSCaption"
                runat="server" ID="aceFSCaption" OnClientPopulating="ShowFSCaptionProgressIcon"
                OnClientPopulated="ShowFSCaptionProgressIcon">
            </ajaxToolkit:AutoCompleteExtender>
        </td>
        <td style="padding-left: 1cm">
            <webControls:ExLabel ID="lblRiskRating" LabelID="1013" runat="server" FormatString="{0}:"
                SkinID="Black11Arial"></webControls:ExLabel>
        </td>
        <td>
            <RiskRating:DropDownList ID="ddlRiskRating" runat="server" IsUsedOnSearchPage="true"></RiskRating:DropDownList>
        </td>
        <td>&nbsp;
        </td>
    </tr>
    <tr class="BlankRow">
        <td colspan="6"></td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td>
            <webControls:ExLabel ID="lblUsername" LabelID="1869" runat="server" FormatString="{0}:"
                SkinID="Black11Arial"></webControls:ExLabel>
        </td>
        <td>
            <asp:TextBox ID="txtUsername" runat="server" SkinID="ExTextBox150" MaxLength="201" />
            <img id="imgUserNameProgress" style="visibility: hidden" alt="imgProgress" src="<%= ResolveClientUrlPath("~/App_Themes/SkyStemBlueBrown/Images/progress_small.gif") %>" />
            <ajaxToolkit:AutoCompleteExtender TargetControlID="txtUsername" ServiceMethod="AutoCompleteUserName"
                runat="server" ID="aceUserName" OnClientPopulating="ShowUserNameProgressIcon"
                OnClientPopulated="ShowUserNameProgressIcon">
            </ajaxToolkit:AutoCompleteExtender>
        </td>
        <td style="padding-left: 1cm">
            <webControls:ExLabel ID="lblAccname" runat="server" LabelID="1346" FormatString="{0}:"
                SkinID="Black11Arial"></webControls:ExLabel>
        </td>
        <td>
            <webControls:ExTextBox ID="txtAcName" runat="server" SkinID="ExTextBox150" MaxLength="100" />
        </td>
        <td>&nbsp;
        </td>
    </tr>
    <tr class="BlankRow">
        <td colspan="6"></td>
    </tr>
    <tr id="trDueDays" runat="server" visible="false">
        <td>&nbsp;
        </td>
        <td>
            <webControls:ExLabel ID="lblDueDays" LabelID="2760" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
            &nbsp;
            <webControls:ExLabel ID="lblFromDueDays" LabelID="1336" runat="server" FormatString="{0}:"
                SkinID="Black11Arial"></webControls:ExLabel>
        </td>
        <td>
            <webControls:ExTextBox ID="txtFromDueDays" runat="server" SkinID="ExTextBox150" MaxLength="20" />
        </td>
        <td style="padding-left: 1cm">
            <webControls:ExLabel ID="lblToDueDays" LabelID="1345" runat="server" FormatString="{0}:"
                SkinID="Black11Arial"></webControls:ExLabel>
        </td>
        <td>
            <webControls:ExTextBox ID="txtToDueDays" runat="server" SkinID="ExTextBox150" MaxLength="20" />
        </td>
        <td></td>
    </tr>
    <tr class="BlankRow">
        <td colspan="6"></td>
    </tr>
    <tr>
        <td>&nbsp;
        </td>
        <td colspan="2">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr id="trKeyAccount" runat="server">
                    <td width="145px">
                        <webControls:ExLabel ID="lblIsKeyAccount" LabelID="1339" runat="server" FormatString="{0}:"
                            SkinID="Black11Arial"></webControls:ExLabel>
                    </td>
                    <td>
                        <webControls:ExRadioButton ID="optIsKeyAccountYes" runat="server" GroupName="IsKeyAccount"
                            TextAlign="Right" LabelID="1252" CssClass="Black11Arial" />
                        &nbsp;
                        <webControls:ExRadioButton ID="optIsKeyAccountNo" runat="server" GroupName="IsKeyAccount"
                            TextAlign="Right" LabelID="1251" CssClass="Black11Arial" />
                        &nbsp;
                        <webControls:ExRadioButton ID="optIsKeyAccountAll" runat="server" GroupName="IsKeyAccount"
                            TextAlign="Right" LabelID="1262" CssClass="Black11Arial" Checked="true" />
                    </td>
                </tr>
            </table>
        </td>
        <td style="padding-left: 1cm">
            <webControls:ExLabel ID="lblZeroBalanceAccount" runat="server" LabelID="1256" FormatString="{0}:"
                SkinID="Black11Arial"></webControls:ExLabel>
        </td>
        <td>
            <webControls:ExRadioButton ID="optZeroBalanceAccountYes" runat="server" GroupName="IsZeroBalanceAccount"
                TextAlign="Right" LabelID="1252" CssClass="Black11Arial" />
            &nbsp;
            <webControls:ExRadioButton ID="optZeroBalanceAccountNo" runat="server" GroupName="IsZeroBalanceAccount"
                TextAlign="Right" LabelID="1251" CssClass="Black11Arial" />
            &nbsp;
            <webControls:ExRadioButton ID="optZeroBalanceAccountAll" runat="server" GroupName="IsZeroBalanceAccount"
                TextAlign="Right" LabelID="1262" CssClass="Black11Arial" Checked="true" />
        </td>
        <td>&nbsp;
        </td>
    </tr>
    <tr class="BlankRow">
        <td colspan="6"></td>
    </tr>
    <%--IsReconcilable--%>
    <tr>
        <td>&nbsp;
        </td>
        <td colspan="2">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr id="trIsReconcilable" runat="server">
                    <td width="145px">
                        <webControls:ExLabel ID="lblIsReconcilable" LabelID="2401" runat="server" FormatString="{0}:"
                            SkinID="Black11Arial"></webControls:ExLabel>
                    </td>
                    <td>
                        <webControls:ExRadioButton ID="optIsReconcilableYes" runat="server" GroupName="IsReconcilable"
                            TextAlign="Right" LabelID="1252" CssClass="Black11Arial" />
                        &nbsp;
                        <webControls:ExRadioButton ID="optIsReconcilableNo" runat="server" GroupName="IsReconcilable"
                            TextAlign="Right" LabelID="1251" CssClass="Black11Arial" />
                        &nbsp;
                        <webControls:ExRadioButton ID="optIsReconcilableAll" runat="server" GroupName="IsReconcilable"
                            TextAlign="Right" LabelID="1262" CssClass="Black11Arial" Checked="true" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr class="BlankRow">
        <td colspan="6"></td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <webControls:ExCheckBoxWithLabel ID="chkShowMissing" runat="server" SkinID="CheckboxWithLabelBold" />
        </td>
        <td colspan="3" style="padding-left: 1cm">
            <webControls:ExCheckBoxWithLabel ID="chkShowMissingBackupOwners" runat="server" LabelID="2505"
                Visible="false" SkinID="CheckboxWithLabelBold" />
        </td>
    </tr>
    <tr class="BlankRow">
        <td colspan="6"></td>
    </tr>
    <tr runat="server">
        <td colspan="7" align="right">
            <table>
                <tr>
                    <td id="pnlSearch" runat="server">
                        <webControls:ExButton ID="ExButton1" runat="server" LabelID="1340" OnClick="btnSearch_Click"
                            CausesValidation="false" OnClientClick="return HideValidationSummary()" />&nbsp;
                    </td>
                    <td id="pnlSearchAndMassUpdate" runat="server">
                        <webControls:ExButton ID="ExButton2" runat="server" LabelID="1592" OnClick="btnSearchMassUpdate_Click"
                            CausesValidation="false" />&nbsp; &nbsp;&nbsp;&nbsp;
                        <webControls:ExButton ID="ExButton3" runat="server" LabelID="1593" OnClick="btnSearchBulkUpdate_Click"
                            CausesValidation="false" />&nbsp;
                    </td>
                    <td id="pnlSearchAndMail" runat="server" visible="false">
                        <webControls:ExButton ID="ExButton4" runat="server" LabelID="2485" OnClick="btnSearchAndMail_Click"
                            CausesValidation="false" />&nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<ajaxToolkit:RoundedCornersExtender Corners="All" BorderColor="AntiqueWhite" Radius="6"
    TargetControlID="pnlSearchCommand" ID="RoundedCornersExtender1" runat="server">
</ajaxToolkit:RoundedCornersExtender>
<input type="hidden" id="hdnOrganizationalHierarchy" runat="server" />
<input type="hidden" id="hdnTableInnerHTML" runat="server" />
