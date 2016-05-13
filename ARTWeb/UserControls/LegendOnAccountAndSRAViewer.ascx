<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LegendOnAccountAndSRAViewer.ascx.cs"
    Inherits="UserControls_LegendOnAccountAndSRAViewer" %>
<table class="LegendTable" width="100" cellpadding="0" cellspacing="0">
    <tr>
        <td class="LegendHeading" colspan="4">
            <webControls:ExLabel ID="lblHeading" FormatString="{0}:" runat="server" LabelID="1383"></webControls:ExLabel>
        </td>
    </tr>
    <tr>
        <td style="width: 25%">
            <webControls:ExImage ID="imgActionToReconcile" runat="server" SkinID="StartReconciliation" />
            &nbsp;
            <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="1636 " SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td style="width: 25%">
            <webControls:ExImage ID="imgActionToView" runat="server" SkinID="ReadOnlyMode" />
            &nbsp;
            <webControls:ExLabel ID="ExLabel2" runat="server" LabelID="1637 " SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td style="width: 25%">
            <webControls:ExImage ID="imgActionToEdit" runat="server" SkinID="EditMode" />
            &nbsp;
            <webControls:ExLabel ID="ExLabel3" runat="server" LabelID="1429" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td style="width: 25%">
            <webControls:ExImage ID="imgReconciled" runat="server" SkinID="InactiveIcon" />
            &nbsp;
            <webControls:ExLabel ID="lblReconciled" runat="server" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
    </tr>
</table>
