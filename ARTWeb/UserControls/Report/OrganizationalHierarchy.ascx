<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrganizationalHierarchy.ascx.cs"
    Inherits="OrganizationalHierarchy" %>
<div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <col width="5%" />
        <col width="20%" />
        <col width="75%" />
        <tr>
            <td class="ManadatoryField" >
                <asp:PlaceHolder ID="phMandatory" runat="server" Visible="false">*</asp:PlaceHolder>
            </td>
            <td>
                <webControls:ExLabel ID="lblOrgHierarchy" runat="server" LabelID="1596" SkinID="Black11Arial"
                    FormatString="{0}:"></webControls:ExLabel>
            </td>
            <td>
                <asp:DropDownList ID="ddlHierarchy" runat="server">
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:TextBox ID="txtHierarchy" runat="server"></asp:TextBox>
                &nbsp;&nbsp;
                <webControls:ExImageButton ID="btnAddMore" runat="server" AlternateText="Add More"
                    ImageUrl="~/App_Themes/SkyStemBlueBrown/Images/AddMoreFilter.gif" />
                <input id="hdnKey2" type="hidden" runat="server" />
                <input id="hdnKey3" type="hidden" runat="server" />
                <input id="hdnKey4" type="hidden" runat="server" />
                <input id="hdnKey5" type="hidden" runat="server" />
                <input id="hdnKey6" type="hidden" runat="server" />
                <input id="hdnKey7" type="hidden" runat="server" />
                <input id="hdnKey8" type="hidden" runat="server" />
                <input id="hdnKey9" type="hidden" runat="server" />
                <asp:TextBox ID="hndTextBox" runat="server" Style="display: none"></asp:TextBox>
                <webControls:ExRequiredFieldValidator ID="rvfEntity" ControlToValidate="hndTextBox"
                    runat="server" LabelID="5000168" Enabled="false"></webControls:ExRequiredFieldValidator>
            </td>
        </tr>
    </table>
</div>

<script type="text/javascript" language="javascript">
    function ValidateEntity(source, arguments) {
        //source.errormessage = source.fileNameErrorMessage;

        arguments.IsValid = false;

    }
</script>

