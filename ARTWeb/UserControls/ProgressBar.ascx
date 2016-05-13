<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProgressBar.ascx.cs" Inherits="UserControls_ProgressBar" %>
<asp:UpdateProgress ID="upProgress" runat="server" DisplayAfter="20">
    <ProgressTemplate>
        <iframe frameborder="0" src="about:blank" class="ProgressBarBackground"></iframe>
        <div class="ProgressBar" id="divProgressBar" runat="server">
            <webControls:ExImage ID="imgProgress" runat="server" SkinID="ProgressIcon" />
            <webControls:ExLabel ID="lblProgress" runat="server" LabelID="1266" />
        </div>
        <ajaxToolkit:AlwaysVisibleControlExtender ID="avceProgressBar" runat="server" TargetControlID="divProgressBar"
            VerticalSide="Middle" HorizontalSide="Left" ScrollEffectDuration="0.1">
        </ajaxToolkit:AlwaysVisibleControlExtender>
    </ProgressTemplate>
</asp:UpdateProgress>
