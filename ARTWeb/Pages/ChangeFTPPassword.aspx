<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" CodeFile="ChangeFTPPassword.aspx.cs" Inherits="Pages_ChangeFTPPassword" Theme="SkyStemBlueBrown" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--    <asp:UpdatePanel ID="upnlChangePassword" runat="server">
        <ContentTemplate>--%>
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="3" style="height: 30px">&nbsp;
                    </td>
                </tr>
                <tr style="height: auto">
                    <td width="25%">&nbsp;
                    </td>
                    <td width="50%">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ChangePasswordBg">
                            <tr class="BlankRow">
                                <td colspan="4"></td>
                            </tr>
                            <tr>
                                <td colspan="3" align="left" style="text-indent: 10px">
                                    <webControls:ExLabel ID="lblChangePassword" runat="server" LabelID="2917" SkinID="BlueBold11ArialUnderline"></webControls:ExLabel>
                                </td>
                                <td></td>
                            </tr>
                            <tr class="BlankRow">
                                <td colspan="4"></td>
                            </tr>
                            <tr>
                                <td class="ManadatoryField">*
                                </td>
                                <td width="38%">
                                    <webControls:ExLabel ID="lblOldPassword" runat="server" LabelID="1235" SkinID="Black11Arial"
                                        FormatString="{0}:"></webControls:ExLabel>
                                </td>
                                <td width="60%" align="left">
                                    <asp:TextBox ID="txtOldPassword" autocomplete="off" AutoCompleteType="Disabled" runat="server" SkinID="TextBox200" TextMode="Password" />
                                </td>
                                <td>
                                    <webControls:ExRequiredFieldValidator ID="rfvOldPassword" runat="server" ControlToValidate="txtOldPassword" />
                                    <webControls:ExRegularExpressionValidator ID="revOldPassword" runat="server" ControlToValidate="txtOldPassword"
                                        ValidationExpression="(?=^.{6,}$)(?=.*\d)(?=.*[A-Z])(?![.\n]).*$" />
                                </td>
                            </tr>
                            <tr class="BlankRow">
                                <td colspan="4"></td>
                            </tr>
                            <tr>
                                <td class="ManadatoryField">*
                                </td>
                                <td>
                                    <webControls:ExLabel ID="lblNewPassword" runat="server" LabelID="1236" SkinID="Black11Arial"
                                        FormatString="{0}:"></webControls:ExLabel>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNewPassword" autocomplete="off" AutoCompleteType="Disabled" SkinID="TextBox200" runat="server" TextMode="Password" /><webControls:ExRequiredFieldValidator
                                        ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword" />
                                </td>
                                <td>
                                    <webControls:ExRegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtNewPassword"
                                        ValidationExpression="(?=^.{6,}$)(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?![.\n]).*$" />
                                    <webControls:ExCompareValidator ID="cmpvNewPassword" Operator="NotEqual" runat="server" ControlToCompare="txtOldPassword"
                                        ControlToValidate="txtNewPassword" LabelID="5000374" />
                                    <asp:CustomValidator ID="cvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                                        OnServerValidate="cvNewPassword_ServerValidate" />
                                    <asp:CustomValidator ID="cvNewPassordFormat" runat="server" ControlToValidate="txtNewPassword"
                                        OnServerValidate="cvNewPassordFormat_ServerValidate" />
                                </td>
                            </tr>
                            <tr class="BlankRow">
                                <td colspan="4"></td>
                            </tr>
                            <tr>
                                <td class="ManadatoryField">*
                                </td>
                                <td>
                                    <webControls:ExLabel ID="lblConfirmPassword" runat="server" LabelID="1237" SkinID="Black11Arial"
                                        FormatString="{0}:"></webControls:ExLabel>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtConfirmPassword" autocomplete="off" AutoCompleteType="Disabled" runat="server" SkinID="TextBox200" TextMode="Password" /><webControls:ExRequiredFieldValidator
                                        ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" />
                                </td>
                                <td>
                                    <webControls:ExCompareValidator ID="cmpvPassword" runat="server" ControlToValidate="txtConfirmPassword"
                                        ControlToCompare="txtNewPassword" LabelID="5000005" />
                                </td>
                            </tr>
                            <tr class="BlankRow">
                                <td colspan="4"></td>
                            </tr>
                        </table>
                    </td>
                    <td width="25%">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td width="25%">&nbsp;
                    </td>
                    <td width="50%" align="right">
                        <webControls:ExButton ID="btnSave" runat="server" Width="90" Height="25"  OnClick="btnSave_Click"
                            LabelID="1315" />
                        <webControls:ExButton ID="btnCancel" runat="server" Width="90" LabelID="1239" Height="25" OnClick="btnCancel_Click"
                           CausesValidation="false" />
                    </td>
                    <td width="25%"></td>
                </tr>
                <tr>
                    <td colspan="3">
<%--                        <usercontrols:progressbar id="ucProgressBar" runat="server" associatedupdatepanelid="upnlChangePassword" />--%>
                    </td>
                </tr>
            </table>
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

