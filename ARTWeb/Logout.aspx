<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="Logout"
    Theme="SkyStemBlueBrown" MasterPageFile="~/MasterPages/LoginMasterPage.master" %>

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
                        <td colspan="3" width="870" style="background: url(App_Themes/SkyStemBlueBrown/Images/TopBorderForgotPwd.gif) repeat-x">
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
                            <img src="App_Themes/SkyStemBlueBrown/Images/SessionExpiredImage.gif" height="270" alt="" />
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
                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                            <tr>
                                                <td colspan="2">
                                                    <UserControls:ARTLogoLogin runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="Black11Arial"align="center">
                                                    <webControls:ExLabel ID="lblLogoutMessage" runat="server" LabelID="5000009" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="BrownArial13" align="center">
                                                    <webControls:ExLabel ID="lblLoginLinkMessage" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
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
                                    <td height="15" valign="bottom" class="GreenLoginBoxBackground" width="380">
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
