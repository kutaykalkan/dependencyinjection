<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="LowerUpperBound" Codebehind="LowerUpperBound.ascx.cs" %>
<%@ Import Namespace="SkyStem.Language.LanguageUtility" %>
<div>
    <table id="tblMain" width="75%" runat="server" cellpadding="0" cellspacing="0" border="0">
       
        <tr>
            <td class="ManadatoryField">
                *
            </td>
            <td>
                <webControls:ExLabel ID="lblLowerBound" runat="server" LabelID="2232" SkinID="Black11Arial"
                    FormatString="{0}:"></webControls:ExLabel>
            </td>
            <td>
                <asp:TextBox ID="txtLowerBound" runat="server" />
                <webControls:ExCustomValidator ID="rfv1" runat="server" ClientValidationFunction="LowerBoundValidationForRuleSetup"
                    Display="Dynamic" ErrorMessage="" Text="!"></webControls:ExCustomValidator>
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="ManadatoryField">
                *
            </td>
            <td>
                <webControls:ExLabel ID="lblUpperBound" runat="server" LabelID="2233" SkinID="Black11Arial"
                    FormatString="{0}:"></webControls:ExLabel>
            </td>
            <td>
                <asp:TextBox ID="txtUpperBound" runat="server" />
                <webControls:ExCustomValidator ID="rfv2" runat="server" ClientValidationFunction="UpperBoundValidationForRuleSetup"
                    Display="Dynamic" ErrorMessage="" Text="!"></webControls:ExCustomValidator>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnField" runat="server" />
</div>

<script language="javascript" type="text/javascript">
    function LowerBoundValidationForRuleSetup(source, arguments) {
        //var cvSearch = document.all ? document.all['" + rfv1.ClientID + "'] : document.getElementById('" + rfv1.ClientID + "');
        var cvSearch = $get('<%=rfv1.ClientID%>');
        var txtLower = document.getElementById(source.txtLowerBoundClientID);
        if (txtLower != null) {
            if (txtLower.value == '') {
                cvSearch.errormessage = '<%= LanguageUtil.GetValue(5000262) %>';
                arguments.IsValid = false;

            }
            else if (txtLower.value <= 0) {
                cvSearch.errormessage = '<%= LanguageUtil.GetValue(5000295) %>';
                arguments.IsValid = false;

            }
        }
        return false;
    }
    function UpperBoundValidationForRuleSetup(source, arguments) {
        //        var cvSearch = document.all ? document.all['" + rfv2.ClientID + "'] : document.getElementById('" + rfv2.ClientID + "');
        var cvSearch = $get('<%=rfv2.ClientID%>');
        var txtUpper = document.getElementById(source.txtUpperBoundClientID);
        if (txtUpper != null)
            if (txtUpper.value == '') {
            cvSearch.errormessage = '<%= LanguageUtil.GetValue(5000283) %>';
            arguments.IsValid = false;
        }
        else if (txtUpper.value <= 0) {
            cvSearch.errormessage = '<%= LanguageUtil.GetValue(5000296) %>';
            arguments.IsValid = false;
        }
        return false;
    }
    
</script>

