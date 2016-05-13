<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RoleSelection.ascx.cs"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_RoleSelection" %>
<div style="width: 500px; height: 200px;">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" style="padding-right: 78px; vertical-align: top;">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="text-align: left; vertical-align: top;">
                            <webControls:ExLabel ID="lblAvailableRolesCaption" runat="server" LabelID="1199"
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; vertical-align: top;">
                            <asp:ListBox runat="server" ID="lstUserRoles" SelectionMode="Multiple" Rows="5" SkinID="lstSelection200">
                            </asp:ListBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td align="center">
                <div>
                    <table >
                        <tr>
                            <td align="center" >
                                <input type="button" id="btnSelect" runat="server" style="width:120px"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" >
                            <input type="button" id="btnSelectAll" runat="server" style="width:120px" />
                              
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <input type="button" id="btnUnSelect" runat="server"  style="width:120px"/>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" >
                                 <input type="button" id="btnUnSelectAll" runat="server" style="width:120px" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td align="right" style="padding-left: 78px; vertical-align: top;">
                <div align="left">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="text-align: left; vertical-align: top;">
                                <webControls:ExLabel ID="lblSelectedRolesCaption" runat="server" LabelID="1255" SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: top;">
                                <asp:ListBox runat="server" ID="lstSelectedUserRoles" SelectionMode="Multiple" Rows="5" SkinID="lstSelection200" >
                                </asp:ListBox>
                                <webControls:ExCustomValidator runat="server" ID="cvSelectedUserRoles" ControlToValidate="lstSelectedUserRoles"
                                    LabelID="1725" 
                                    EnableClientScript="true" ClientValidationFunction="ValidateListBox"
                                    ValidateEmptyText="true" Display="Dynamic" >!</webControls:ExCustomValidator>
                            </td>
                        </tr>
                        
                    </table>
                </div>
            </td>
            
        </tr>
        <tr><td><webControls:ExLabel ID="lblSelectMultiple" runat="server" LabelID="1718" FormatString="({0})" SkinID="Black9ArialItalic"></webControls:ExLabel></td></tr>
        
    </table>
</div>

<script language="javascript" type="text/javascript">
    function ValidateListBox(source, args) {
        var opt = document.getElementById ('<% =this.lstSelectedUserRoles.ClientID %>');
        if (opt != null) {
            if (opt.options.length > 0) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }
    }
</script>
