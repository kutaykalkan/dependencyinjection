<%@ Page Language="C#" AutoEventWireup="true" Inherits="SkyStem.ART.Web.Login"
    Theme="SkyStemBlueBrown" MasterPageFile="~/MasterPages/LoginMasterPage.master" Codebehind="Login.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ARTLogoLogin" Src="~/UserControls/ARTLogoLogin.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphContents">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td class="LoginBg">
                <table width="910" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td colspan="6">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">&nbsp;
                        </td>
                        <td width="15%">&nbsp;
                        </td>
                        <td width="15%">&nbsp;
                        </td>
                        <td width="15%">&nbsp;
                        </td>
                        <td width="36%" height="246" valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="5%" height="16" align="left" valign="bottom" class="LeftBorderBlueLogin">
                                        <img src="App_Themes/SkyStemBlueBrown/Images/LoginTopLt.gif" width="16" height="16"
                                            alt="" />
                                    </td>
                                    <td width="91%" height="16" valign="top" class="GreenLoginBoxBackground">
                                        <img src="App_Themes/SkyStemBlueBrown/Images/TopBor.gif" style="vertical-align: top"
                                            width="352" height="2" alt="" />
                                    </td>
                                    <td width="4%" height="16" align="right" valign="bottom" class="RightBorderBlueLoginModified">
                                        <img src="App_Themes/SkyStemBlueBrown/Images/LoginTopRt.gif" width="16" height="16"
                                            alt="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="bottom" class="LeftBorderBlueLogin">&nbsp;
                                    </td>
                                    <td valign="top" class="GreenLoginBoxBackground">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td colspan="2">
                                                    <UserControls:ARTLogoLogin runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:ValidationSummary ID="valSummary" runat="server" />
                                                    <webControls:ExLabel ID="lblErrorMessage" runat="server" SkinID="ErrorLabel" Visible="false"></webControls:ExLabel>
                                                </td>
                                            </tr>
                                            <tr class="BlankRow">
                                                <td colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td class="BrownArial13" width="25%">
                                                    <webControls:ExLabel ID="lblUserName" runat="server" LabelID="1003"></webControls:ExLabel>
                                                </td>
                                                <td width="75%">
                                                    <asp:TextBox ID="txtUserName" runat="server" AutoCompleteType="Disabled" autocomplete="off" SkinID="TextBox200"/>
                                                    <webControls:ExRequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" />
                                                    <webControls:ExRegularExpressionValidator ID="revUserName" runat="server" LabelID="5000004" ControlToValidate="txtUserName"
                                                    ValidationExpression="^([a-zA-Z0-9_\-\.]+)@[a-zA-Z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$" />
                                                    <asp:CustomValidator ID="cvUserName" runat="server" ControlToValidate="txtUserName" OnServerValidate="cvUserName_ServerValidate" />
                                                </td>
                                            </tr>
                                            <tr class="BlankRow">
                                                <td colspan="2"></td>
                                            </tr>
                                            <tr>
                                                <td class="BrownArial13">
                                                    <webControls:ExLabel ID="lblPassword" runat="server" LabelID="1004"></webControls:ExLabel>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPassword" AutoCompleteType="Disabled" autocomplete="off" runat="server" SkinID="TextBox200" TextMode="Password"/>
                                                    <webControls:ExRequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" />
                                                    <webControls:ExRegularExpressionValidator ID="revPassword" LabelID="5000195" runat="server" ControlToValidate="txtPassword"
                                                        ValidationExpression="^([a-zA-Z0-9@#\$%&\!\?]*)$" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="WhiteArial12" colspan="2">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="WhiteArial12">&nbsp;
                                                </td>
                                                <td class="Black11Arial">
                                                    <webControls:ExHyperLink ID="hlForgotPassword" runat="server" LabelID="1005" NavigateUrl="ForgotPassword.aspx"></webControls:ExHyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="WhiteArial12" colspan="2">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="WhiteArial12">&nbsp;
                                                </td>
                                                <td align="right">
                                                    <webControls:ExButton ID="btnLogin" runat="server" LabelID="1002" OnClick="btnLogin_Click"
                                                        SkinID="ExButton100" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="16" class="RightBorderBlueLoginModified">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td height="16" align="left" valign="bottom" class="LeftBorderBlueLogin">
                                        <img src="App_Themes/SkyStemBlueBrown/Images/LoginBotLt.gif" width="16" height="16"
                                            alt="" style="vertical-align: bottom" />
                                    </td>
                                    <td height="16" valign="bottom" class="GreenLoginBoxBackground">
                                        <img src="App_Themes/SkyStemBlueBrown/Images/BottomBorder.gif" width="352" height="2"
                                            alt="" style="vertical-align: bottom" />
                                    </td>
                                    <td height="16" align="right" valign="bottom" class="RightBorderBlueLoginModified">
                                        <img src="App_Themes/SkyStemBlueBrown/Images/LoginBotRt.gif" width="16" height="16"
                                            alt="" style="vertical-align: bottom" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="4%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
