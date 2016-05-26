<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_SessionExpiryNotification" Codebehind="SessionExpiryNotification.ascx.cs" %>
<div>

    <script type="text/javascript" language="javascript">
        // ----- Statrt----- script for Session Expiry Notification 

        var timeLeftCounter = null;
        var seconds = 60;

        //start the main label counter when the page loads
        function pageLoad() {
            var xmlPanel = $find("<%= RadNotification1.ClientID %>")._xmlPanel;
            xmlPanel.set_enableClientScriptEvaluation(true);
            seconds = GetWarningTime();
        };

        //stop timers for UI 
        function stopTimer(timer) {
            clearInterval(this[timer]);
            this[timer] = null;
        };

        //reset timers for UI
        function resetTimer(timer, func, interval) {
            this.stopTimer(timer);
            this[timer] = setInterval(Function.createDelegate(this, func), interval);
        };

        function OnClientShowing(sender, args) {
            var hdnDisplaySessionWarning = $get("<%= hdnDisplaySessionTimeoutWarning.ClientID %>");
            if (hdnDisplaySessionWarning.value != "1") {
                ContinueSession();
                args.set_cancel(true);
            }
            else
                resetTimer("timeLeftCounter", UpdateTimeLabel, 1000);
        }

        function UpdateTimeLabel(toReset) {
            var sessionExpired = (seconds == 0);
            if (sessionExpired) {
                stopTimer("timeLeftCounter");
                //redirect to session expired page - simply take the url which RadNotification sent from the server to the client as value
                window.location.href = $find("<%= RadNotification1.ClientID %>").get_value();
            }
            else {
                var timeLbl = $get('<%= timeLbl.ClientID %>');
                timeLbl.innerHTML = seconds--;
            }
        }

        function ContinueSession() {
            var notification = $find("<%= RadNotification1.ClientID %>");
            //we need to contact the server to restart the Session - the fastest way is via callback
            //calling update() automatically performs the callback, no need for any additional code or control
            notification.update();
            notification.hide();

            //resets the showInterval for the scenario where the Notification is not disposed (e.g. an AJAX request is made)
            //You need to inject a call to the ContinueSession() function from the code behind in such a request
            var showIntervalStorage = notification.get_showInterval(); //store the original value
            notification.set_showInterval(0); //change the timer to avoid untimely showing, 0 disables automatic showing
            notification.set_showInterval(showIntervalStorage); //sets back the original interval which will start counting from its full value again

            stopTimer("timeLeftCounter");
            seconds = GetWarningTime();
            return false;
        }

        function GetWarningTime() {
            var hdnSessionWarning = $get("<%= hdnSessionTimeoutWarningInterval.ClientID %>");
            return hdnSessionWarning.value * 1;
        }

        // ----- End----- Script for Session Expiry Notification       
    </script>

    <asp:HiddenField ID="hdnSessionTimeoutWarningInterval" runat="server" />
    <asp:HiddenField ID="hdnDisplaySessionTimeoutWarning" runat="server" />
    <telerikWebControls:ExRadNotification ID="RadNotification1" runat="server" Position="Center"
        Width="375" Height="140" LabelID="2542" EnableRoundedCorners="true" ShowCloseButton="true"
        OnCallbackUpdate="OnCallbackUpdate" OnClientShowing="OnClientShowing" LoadContentOn="PageLoad"
        EnableShadow="true" KeepOnMouseOver="true" TitleIcon="~/App_Themes/SkyStemBlueBrown/Images/info_small.gif">
        <ContentTemplate>
            <div class="notificationContent">
                <table>
                    <tr>
                        <td class="infoIcon">
                            <asp:Image ID="imgInfo" runat="server" SkinID="InfoBig" alt="info icon" />
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblTimeRemaining" runat="server" FormatString="{0} " LabelID="2538"></webControls:ExLabel>
                        </td>
                        <td>
                            <%-- <span id="timeLbl"></span>--%>
                            <webControls:ExLabel ID="timeLbl" runat="server"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblSeconds" runat="server" FormatString=" {0}." LabelID="2539"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;
                        </td>
                        <td colspan="3" align="left">
                            <webControls:ExLabel ID="lblMessage" runat="server" FormatString="{0}." LabelID="2543"></webControls:ExLabel>
                        </td>
                        <tr>
                            <td colspan="4" align="center">
                                <webControls:ExButton ID="btnContinueSession" runat="server" LabelID="1742" Style="margin: 10px;"
                                    UseSubmitBehavior="false" OnClientClick="return ContinueSession();" SkinID="ExButton100">
                                </webControls:ExButton>
                            </td>
                        </tr>
                </table>
            </div>
        </ContentTemplate>
    </telerikWebControls:ExRadNotification>
</div>
