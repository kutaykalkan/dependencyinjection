<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GLProcessError.aspx.cs" Inherits="GLProcessError"
    Theme="SkyStemBlueBrown" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Skystem ART</title>
    <script type="text/javascript" src="../JavaScript/jquery-1.4.2.min.js"></script>
    <link href="../App_Themes/SkyStemBlueBrown/default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="_scriptManager" EnablePartialRendering="true" runat="server"
        EnablePageMethods="true">
    </asp:ScriptManager>    
    <table id="tblMain" cellspacing="0" cellpadding="0" border="0" width="75%" class="centered">
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
            <td align="center" width="75%">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="outerTable">
                    <tr>
                        <td colspan="2" align="center" class="topBg">
                            <asp:Image ID="imgLogo" runat="server" SkinID="SkyStemLogo" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                        </td>
                    </tr>
                    <tr>
                        <td width="37%">
                            &nbsp;
                        </td>
                        <td width="63%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Image ID="imgClock" runat="server" SkinID="ClockImage" />
                        </td>
                        <td align="left" valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
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
                                        <asp:Image ID="imgWait" runat="server" SkinID="WaitImage" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="text">
                                        <webControls:ExLabel ID="lblErrorMsg" runat="server"></webControls:ExLabel>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td class="text">
                            <webControls:ExButton ID="btnRefresh" runat="server" SkinID="ExButton100" LabelID="2097"
                                OnClick="btnRefresh_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Black11Arial" colspan="2" align="left">
                            <webControls:ExLabel ID="lblSeconds" LabelID="2529" runat="server" FormatString="{0}:"/>&nbsp;<span id="spanTimer"></span>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        var timeLeft = 60000;
        function reloadPage() {
            $get("<%= btnRefresh.ClientID %>").click();
        }
        function ShowTimeLeft() {
            if (timeLeft > 1000) {
                timeLeft -= 1000;
                $("#spanTimer").html(timeLeft / 1000);
            }
        }
        setInterval(reloadPage, timeLeft);
        setInterval(ShowTimeLeft, 1000);
        ShowTimeLeft();
    </script>

    </form>
</body>
</html>
