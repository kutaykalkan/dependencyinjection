<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_Report_AccountRange" Codebehind="AccountRange.ascx.cs" %>
<div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="5%" />
        <col width="20%" />
        <col width="75%" />
        <tr class="BlankRow">
            </tr>
        <tr>
            <td class="ManadatoryField">
            </td>
            <td>
                <webControls:ExLabel ID="lblRiskRating" SkinID="Black11Arial" FormatString="{0}:"
                    LabelID="1491" runat="server"></webControls:ExLabel>
            </td>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <webControls:ExLabel ID="ExLabel1" SkinID="Black11Arial" FormatString="{0}:" LabelID="1336"
                                runat="server"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExTextBox ID="txtFromAccount" runat="server" />
                        </td>
                        <td>
                            <webControls:ExLabel ID="ExLabel2" SkinID="Black11Arial" FormatString="{0}:" LabelID="1345"
                                runat="server"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExTextBox ID="txtToAccount" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
