<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RadioButtonListAllYesNo.ascx.cs"
    Inherits="RadioButtonListAllYesNo" %>
<div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="5%" />
        <col width="20%" />
        <col width="75%" />
        <tr class="BlankRow">
        </tr>
        <tr>
            <td class="ManadatoryField" >
                <asp:PlaceHolder ID="phMandatoryField" runat="server" Visible="false">* </asp:PlaceHolder>
            </td>
            <td>
                <webControls:ExLabel ID="lblCriteriaName" SkinID="Black11Arial" FormatString="{0}:"
                    LabelID="1014" runat="server"></webControls:ExLabel>
            </td>
            <td>
                <asp:RadioButtonList ID="rblCriteria" runat="server" RepeatDirection="Horizontal"
                    CssClass="Black11Arial" CausesValidation="false">
                </asp:RadioButtonList>
                <webControls:ExRequiredFieldValidator ID="rfv" ControlToValidate="rblCriteria" runat="server" Enabled="false"></webControls:ExRequiredFieldValidator>
            </td>
        </tr>
    </table>
</div>
