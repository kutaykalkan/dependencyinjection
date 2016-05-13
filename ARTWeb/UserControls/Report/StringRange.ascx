<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StringRange.ascx.cs" Inherits="StringRange" %>
<div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="5%" />
        <col width="20%" />
        <col width="70%" />
        <col width="5%" />
        <tr class="BlankRow">
        </tr>
        <tr>
            <td class="ManadatoryField">
                <asp:PlaceHolder ID="phMandatory" runat="server" Visible="false">*</asp:PlaceHolder>
            </td>
            <td valign="top">
                <webControls:ExLabel ID="lblCriteriaName" SkinID="Black11Arial" FormatString="{0}:"
                    LabelID="1491" runat="server"></webControls:ExLabel>
            </td>
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
                            <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblTo" SkinID="Black11Arial" FormatString="{0}:" LabelID="1345"
                                runat="server"></webControls:ExLabel>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <webControls:ExCustomValidator ID="rfv" runat="server" ClientValidationFunction="validateAccountRange"
                    Display="Dynamic" ErrorMessage="" Text="!"></webControls:ExCustomValidator>
            </td>
        </tr>
    </table>
</div>

<script type="text/javascript" language="javascript">
    function validateAccountRange(source, arguments) {
        //alert("test");
        var txtFrom = $get('<% =this.txtFrom.ClientID %>');
        var txtTo = $get('<% =this.txtTo.ClientID %>');
        if (txtFrom != null && txtTo != null)
            var val = txtFrom.value + txtTo.value;
        if (val == null || val == '')
            arguments.IsValid = false;
    }
</script>

