<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageAutoRefreshNotification.ascx.cs" Inherits="UserControls_Notification_PageAutoRefreshNotification" %>
<div>
    <asp:HiddenField ID="hdnPreviousPeriodStatusID" runat="server" />
    <telerikwebcontrols:exradnotification id="rnPageAutoRefresh" runat="server"
        width="375" height="140" labelid="2542" enableroundedcorners="true" showclosebutton="true"
        autoclosedelay="5000" onclienthidden="OnClientHidden" text=""
        enableshadow="true" keeponmouseover="true" titleicon="~/App_Themes/SkyStemBlueBrown/Images/info_small.gif">
    </telerikwebcontrols:exradnotification>
    <script type="text/javascript">
        function ShowNotification(title, msg) {
            var rnPageAutoRefresh = $find('<%=rnPageAutoRefresh.ClientID%>')
            rnPageAutoRefresh.set_title(title);
            rnPageAutoRefresh.set_text(msg);
            rnPageAutoRefresh.show();
        }
        function OnClientHidden(sender, eventArgs) {
            RefreshFullPage();
        }
        //alert("OnClientUpdated event fired by RadNotification with id: " + sender.get_id());
    </script>
</div>

