<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LegendOnItemInputForm.ascx.cs" Inherits="UserControls_LegendOnItemInputForm" %>
<table class="LegendTable" width="100" cellpadding="0" cellspacing="0">
        <tr>
            <td id="tdLegendHeader"  runat="server" class="LegendHeading" colspan="2">
                <webControls:ExLabel ID="lblHeading" FormatString="{0}:" runat="server" LabelID="1383"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
        <td id="tdSchedule"  runat="server" >
                <webControls:ExImage ID="imgSchedule"  runat="server"  SkinID="ShowSchedulePopup" />
                &nbsp;
                <webControls:ExLabel ID="lblSchedule" runat="server"  SkinID="LegendLabel"></webControls:ExLabel>
            </td>
            <%--<td  id="tdDocument"  runat="server" >
                <webControls:ExImage ID="imgDocument" runat="server"  SkinID="ShowDocumentPopup" />
                &nbsp;
                <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="5000029 " SkinID="LegendLabel"></webControls:ExLabel>
            </td>--%>
            
              <td  id="tdCarriedForwordItems"  runat="server" >
                <webControls:ExImage ID="imgCarriedForwordItems" runat="server"  SkinID="ShowCarriedForwordItems" />
                &nbsp;
                <webControls:ExLabel ID="lblCarriedForwordItems" runat="server" LabelID="1930 " SkinID="LegendLabel"></webControls:ExLabel>
            </td>
           <%-- <td>
                <webControls:ExImage ID="imgEdit" runat="server"   SkinID="GridButtonEdit" />
                &nbsp;
                <webControls:ExLabel ID="ExLabel3" runat="server" LabelID="5000025 " SkinID="LegendLabel"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExImage ID="imgDelete" runat="server"   SkinID="GridButtonDelete" />
                &nbsp;
                <webControls:ExLabel ID="ExLabel4" runat="server" LabelID="5000026 " SkinID="LegendLabel"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExImage ID="imgCancel" runat="server"  SkinID="GridButtonCancel" />
                &nbsp;
                <webControls:ExLabel ID="ExLabel2" runat="server" LabelID="5000027 " SkinID="LegendLabel"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExImage ID="ExUpdate" runat="server"  SkinID="GridButtonUpdate" />
                &nbsp;
                <webControls:ExLabel ID="ExLabel6" runat="server" LabelID="5000028 " SkinID="LegendLabel"></webControls:ExLabel>
            </td>--%>
        </tr>
        </table>