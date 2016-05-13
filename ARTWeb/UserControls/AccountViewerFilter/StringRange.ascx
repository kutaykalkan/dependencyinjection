<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StringRange.ascx.cs" 
Inherits="AcctFltrStringRange" %>
<div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="95%" />        
        <col width="5%" />
        <tr>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <col width="10%" />
                    <col width="38%" />
                    <col width="10%" />
                    <col width="38%" />
                    <tr>
                        <td>
                            <webControls:ExLabel ID="lblFrom" SkinID="Black11Arial" FormatString="{0}:" LabelID="1336"
                                runat="server"></webControls:ExLabel>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblTo" SkinID="Black11Arial" FormatString="{0}:" LabelID="1345"
                                runat="server"></webControls:ExLabel>
                        </td>
                        <td>
                            &nbsp;
                            <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <webControls:ExCustomValidator ID="rfv" runat="server" ClientValidationFunction="AcctFltrValidateAccountRange"
                    Display="Dynamic" ErrorMessage="" Text="!"></webControls:ExCustomValidator>
            </td>
        </tr>
    </table>
</div>

