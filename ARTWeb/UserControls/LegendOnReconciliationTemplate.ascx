<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_LegendOnReconciliationTemplate" Codebehind="LegendOnReconciliationTemplate.ascx.cs" %>
<table class="LegendTable" cellpadding="0" cellspacing="0" width="100%">
    <tr class="HideInPdf">
        <td class="LegendHeading" colspan="6">
            <webControls:ExLabel ID="lblHeading" FormatString="{0}:" runat="server" LabelID="1383"></webControls:ExLabel>
        </td>
    </tr>
    <tr class="HideInPdf">
        <td>
            <webControls:ExImage ID="imgImportItem" runat="server" SkinID="ImportItem" />
            &nbsp;
            <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="1497" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td>
            <webControls:ExImage ID="imgViewItemGrid" runat="server" SkinID="ViewItemGrid" />
            &nbsp;
            <webControls:ExLabel ID="ExLabel2" runat="server" LabelID="1803" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td>
            <webControls:ExImage ID="imgLegendDocument" runat="server" SkinID="ShowDocumentPopup" />
            &nbsp;
            <webControls:ExLabel ID="lblLegendDocument" runat="server" LabelID="1804" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td>
            <webControls:ExImage ID="imgLegendReviewNotes" runat="server" SkinID="ShowCommentPopup" />
            &nbsp;
            <webControls:ExLabel ID="lblLegendReviewNotes" runat="server" LabelID="1394" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td>
            <webControls:ExImage ID="imgLegendUnexplainedVarianceHistory" runat="server" SkinID="History" />
            &nbsp;
            <webControls:ExLabel ID="lblLegendUnexplainedVarianceHistory" runat="server" LabelID="1391"
                SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td id="tdCarriedForwordItems" runat="server">
            <webControls:ExImage ID="imgCarriedForwordItems" runat="server" SkinID="ShowCarriedForwordItems" />
            &nbsp;
            <webControls:ExLabel ID="lblCarriedForwordItems" runat="server" LabelID="1930 " SkinID="LegendLabel"></webControls:ExLabel>
        </td>
    </tr>
</table>
