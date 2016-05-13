<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="SkyStem.ART.Web.ForgotPassword"
    EnableTheming="true" Theme="SkyStemBlueBrown" MasterPageFile="~/MasterPages/LoginMasterPage.master" %>

<%@ Register TagPrefix="UserControls" TagName="ARTLogoLogin" Src="~/UserControls/ARTLogoLogin.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphContents">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td>
                <table border="0" cellspacing="0" cellpadding="0" style="background-color: White;
                    width: 910px">
                    <tr>
                        <td align="left" valign="bottom" height="20" width="20">
                            <img src="App_Themes/SkyStemBlueBrown/Images/Cor3.gif" style="vertical-align: bottom"
                                width="20" height="20" alt="" />
                        </td>
                        <td colspan="3" style="width: 870px; background: url(App_Themes/SkyStemBlueBrown/Images/TopBorderForgotPwd.gif) repeat-x">
                            &nbsp;
                        </td>
                        <td align="right" valign="bottom" height="20" width="20">
                            <img src="App_Themes/SkyStemBlueBrown/Images/Cor4.gif" style="vertical-align: bottom"
                                width="20" height="20" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td style="background: url(App_Themes/SkyStemBlueBrown/Images/LtBorderForgotPwd.gif) repeat-y">
                        </td>
                        <td width="410">
                            <img src="App_Themes/SkyStemBlueBrown/Images/Lock.jpg" width="353" height="270" alt="" />
                        </td>
                        <td width="50">
                        </td>
                        <td width="410">
                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td height="15" align="left" valign="bottom" class="LeftBorderBlueLogin" width="15">
                                        <img src="app_themes/skystembluebrown/images/LtCornerForgotPwd.gif" width="15" height="15"
                                            alt="" />
                                    </td>
                                    <td height="15" valign="top" class="GreenLoginBoxBackground" width="380">
                                        <img src="App_Themes/SkyStemBlueBrown/Images/TopBor.gif" style="vertical-align: top"
                                            width="380" height="2" alt="" />
                                    </td>
                                    <td height="15" align="right" valign="bottom" class="RightBorderBlueLogin" width="15">
                                        <img src="app_themes/skystembluebrown/images/RtCornerForgotPwd.gif" width="15" height="15"
                                            alt="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="bottom" class="LeftBorderBlueLogin">
                                        &nbsp;
                                    </td>
                                    <td class="GreenLoginBoxBackground">
                                        <asp:Panel ID="pnlForgotPassword" runat="server">
                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                <tr>
                                                    <td colspan="2">
                                                        <UserControls:ARTLogoLogin runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="left">
                                                        <asp:ValidationSummary ID="valSummary" runat="server" />
                                                        <webControls:ExLabel ID="lblErrorMessage" runat="server" SkinID="ErrorLabel" Visible="false"></webControls:ExLabel>
                                                    </td>
                                                </tr>
                                                <tr class="BlankRow">
                                                    <td colspan="2">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="Black11Arial" align="left">
                                                        <webControls:ExLabel ID="lblInfo" runat="server" LabelID="1561"></webControls:ExLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="BrownArial13" width="25%">
                                                        <webControls:ExLabel ID="lblUserName" runat="server" LabelID="1003"></webControls:ExLabel>
                                                    </td>
                                                    <td width="75%">
                                                        <asp:TextBox ID="txtUserName" runat="server" AutoCompleteType="Disabled" autocomplete="off" SkinID="TextBox200"/>
                                                        <webControls:ExRequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" />
                                                        <webControls:ExRegularExpressionValidator ID="revUserName" runat="server" ControlToValidate="txtUserName" LabelID="5000004" 
                                                            ValidationExpression="^([a-zA-Z0-9_\-\.]+)@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$"></webControls:ExRegularExpressionValidator>
                                                        <asp:CustomValidator ID="cvUserName" runat="server" ControlToValidate="txtUserName" OnServerValidate="cvUserName_ServerValidate" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="25%">
                                                    </td>
                                                    <td align="right" width="75%">
                                                        <webControls:ExButton ID="btnGetPassword" runat="server" LabelID="1562" OnClick="btnGetPassword_Click"
                                                            SkinID="ExButton150" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td width="15" class="RightBorderBlueLogin">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td height="15" align="left" valign="bottom" class="LeftBorderBlueLogin">
                                        <img src="app_themes/skystembluebrown/images/BtLtCornerForgotPwd.gif" width="15"
                                            height="15" alt="" style="vertical-align: bottom" />
                                    </td>
                                    <td height="15" valign="bottom" class="GreenLoginBoxBackground">
                                        <img src="App_Themes/SkyStemBlueBrown/Images/BottomBorder.gif" width="380" height="2"
                                            alt="" style="vertical-align: bottom" />
                                    </td>
                                    <td height="15" align="right" valign="bottom" class="RightBorderBlueLogin">
                                        <img src="app_themes/skystembluebrown/images/BtRtCornerForgotPwd.gif" width="15"
                                            height="15" alt="" style="vertical-align: bottom" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="background: url(App_Themes/SkyStemBlueBrown/Images/RtBorderForgotPwd.gif) repeat-y">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="bottom" height="20">
                            <img src="App_Themes/SkyStemBlueBrown/Images/Cor1.gif" style="vertical-align: bottom"
                                width="20" height="20" alt="" />
                        </td>
                        <td colspan="3" style="background: url(App_Themes/SkyStemBlueBrown/Images/BotBorderForgotPwd.gif) repeat-x">
                            &nbsp;
                        </td>
                        <td align="right" valign="bottom" height="20">
                            <img src="App_Themes/SkyStemBlueBrown/Images/Cor2.gif" style="vertical-align: bottom"
                                width="20" height="20" alt="" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
