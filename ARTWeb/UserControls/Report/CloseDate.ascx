<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Report_CloseDate" Codebehind="CloseDate.ascx.cs" %>
<div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="25%" />
        <col width="75%" />
        <tr>
            <td>
                <webControls:ExLabel ID="lblCriteriaName" SkinID="Black11Arial" FormatString="{0}:" LabelID="1411"
                    runat="server"></webControls:ExLabel>
            </td>
            <td>
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <webControls:ExLabel ID="lblFrom" SkinID="Black11Arial" FormatString="{0}:" LabelID="1336"
                                runat="server"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExCalendar ID="calFrom" runat="server"></webControls:ExCalendar>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblTo" SkinID="Black11Arial" FormatString="{0}:" LabelID="1345"
                                runat="server"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExCalendar ID="calTo" runat="server"></webControls:ExCalendar>
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
    </table>
</div>