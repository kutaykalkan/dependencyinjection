<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_LegendOnTaskViewer" Codebehind="LegendOnTaskViewer.ascx.cs" %>
<table class="LegendTable" width="100" cellpadding="0" cellspacing="0">
    <tr>
        <td class="LegendHeading" colspan="12">
            <webControls:ExLabel ID="lblHeading" FormatString="{0}:" runat="server" LabelID="1383"></webControls:ExLabel>
        </td>
    </tr>
    <tr>
        <%-- <td style="width: 10%">
            <webControls:ExImage ID="imgImportItem" runat="server" SkinID="ImportItem" />
            &nbsp;
            <webControls:ExLabel ID="lblImportItem" runat="server" LabelID="1497 " SkinID="LegendLabel"></webControls:ExLabel>
        </td>--%>
        <td style="width: 8%">
            <webControls:ExImage ID="imgPending" runat="server" SkinID="Pending" />
            &nbsp;
            <webControls:ExLabel ID="lblPending" runat="server" LabelID="2561 " SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td style="width: 9%">
            <webControls:ExImage ID="imgCompleted" runat="server" SkinID="Completed" />
            &nbsp;
            <webControls:ExLabel ID="lblCompleted" runat="server" LabelID="2559" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td style="width: 8%">
            <webControls:ExImage ID="imgOverdue" runat="server" SkinID="Overdue" />
            &nbsp;
            <webControls:ExLabel ID="lblOverdue" runat="server" LabelID="2562" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td style="width: 8%">
            <webControls:ExImage ID="ExImage1" runat="server" SkinID="Delete" />
            &nbsp;
            <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="2646" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td style="width: 8%">
            <webControls:ExImage ID="imgEdit" runat="server" SkinID="Edit" />
            &nbsp;
            <webControls:ExLabel ID="lblEdit" runat="server" LabelID="1429" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td style="width: 8%">
            <webControls:ExImage ID="imgReadOnly" runat="server" SkinID="ReadOnly" />
            &nbsp;
            <webControls:ExLabel ID="lblReadOnly" runat="server" LabelID="1470" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <%-- 
       <td style="width: 8%">
            <webControls:ExImage ID="imgHidden" runat="server" SkinID="Hidden" />
            &nbsp;
            <webControls:ExLabel ID="lblHidden" runat="server" LabelID="2107" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td style="width: 8%">
            <webControls:ExImage ID="imgUnhide" runat="server" SkinID="Unhide" />
            &nbsp;
            <webControls:ExLabel ID="lblUnhide" runat="server" LabelID="2108" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td style="width: 8%">
            <webControls:ExImage ID="imgAdd" runat="server" SkinID="Add" />
            &nbsp;
            <webControls:ExLabel ID="lblAdd" runat="server" LabelID="1560" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td style="width: 8%">
            <webControls:ExImage ID="imgDel" runat="server" SkinID="Del" />
            &nbsp;
            <webControls:ExLabel ID="lblDel" runat="server" LabelID="1564" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td style="width: 8%">
            <webControls:ExImage ID="imgApprove" runat="server" SkinID="Approve" />
            &nbsp;
            <webControls:ExLabel ID="lblApprove" runat="server" LabelID="1483" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td style="width: 8%">
            <webControls:ExImage ID="imgReject" runat="server" SkinID="Reject" />
            &nbsp;
            <webControls:ExLabel ID="lblReject" runat="server" LabelID="1482" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td style="width: 8%">
            <webControls:ExImage ID="imgDone" runat="server" SkinID="Done" />
            &nbsp;
            <webControls:ExLabel ID="lblDone" runat="server" LabelID="2590" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        <td style="width: 8%">
            <webControls:ExImage ID="imgBulkEdit" runat="server" SkinID="BulkEdit" />
            &nbsp;
            <webControls:ExLabel ID="lblBulkEdit" runat="server" LabelID="2572" SkinID="LegendLabel"></webControls:ExLabel>
        </td>
        --%>
    </tr>
</table>
