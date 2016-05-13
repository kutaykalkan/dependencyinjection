<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true"
    CodeFile="DataImportGLMapping.aspx.cs" Inherits="Pages_DataImportGLMapping" Theme="SkyStemBlueBrown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding-left: 40px; padding-right: 40px">
        <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <table style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr class="BlankRow">
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <webControls:ExLabel ID="lblExistingMapping" LabelID="1566" SkinID="SubSectionHeading"
                                runat="server"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="4">
                        </td>
                    </tr>
                    <%--Static table row--%>
                    <tr>
                        <td style="width: 2%">
                            &nbsp;
                        </td>
                        <td colspan="3" align="center">
                            <table cellpadding="0" cellspacing="0" class="TableSameAsGrid">
                                <tr class="TableHeaderSameAsGrid">
                                    <td>
                                        <webControls:ExLabel LabelID="1570" runat="server"></webControls:ExLabel>
                                    </td>
                                    <td>
                                        <webControls:ExLabel ID="ExLabel2" LabelID="1571" runat="server"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr class="TableRowSameAsGrid" align="left">
                                    <td>
                                        <webControls:ExLabel ID="ExLabel3" LabelID="1059" runat="server"></webControls:ExLabel>
                                    </td>
                                    <td>
                                        <webControls:ExLabel ID="ExLabel4" LabelID="1229" runat="server"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr class="TableAlternateRowSameAsGrid" align="left">
                                    <td>
                                        <webControls:ExLabel ID="ExLabel5" LabelID="1337" runat="server"></webControls:ExLabel>
                                    </td>
                                    <td>
                                        <webControls:ExLabel ID="ExLabel6" LabelID="1337" runat="server"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr class="TableRowSameAsGrid" align="left">
                                    <td>
                                        <webControls:ExLabel ID="ExLabel7" LabelID="1363" runat="server"></webControls:ExLabel>
                                    </td>
                                    <td>
                                        <webControls:ExLabel ID="ExLabel8" LabelID="1363" runat="server"></webControls:ExLabel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <webControls:ExLabel ID="ExLabel1" LabelID="1567" SkinID="SubSectionHeading" runat="server"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 2%">
                            &nbsp;
                        </td>
                        <td align="center">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 2%">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <webControls:ExLabel ID="lblOrganizationalHierarchyKeyDB" LabelID="1570" runat="server"
                                            SkinID="Black11Arial"></webControls:ExLabel>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <webControls:ExLabel ID="lblOrganizationalHierarchyKeyName" LabelID="1571" runat="server"
                                            SkinID="Black11Arial"></webControls:ExLabel>
                                        <br />
                                        <webControls:ExLabel ID="ExLabel9" LabelID="1576" FormatString="({0})" runat="server"
                                            SkinID="Black9ArialItalic"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 2%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 30%">
                                        <asp:ListBox runat="server" ID="lstHierarchyKeys" SelectionMode="Single" Rows="6"
                                            SkinID="lstSelection200"></asp:ListBox>
                                    </td>
                                    <td align="center" style="width: 15%">
                                        <input type="button" id="btnMap" runat="server" />
                                    </td>
                                    <td style="width: 30%">
                                        <asp:ListBox runat="server" ID="lstHierarchyName" SelectionMode="Single" Rows="6"
                                            SkinID="lstSelection200"></asp:ListBox>
                                    </td>
                                </tr>
                                <tr class="BlankRow">
                                </tr>
                                <tr>
                                    <td style="width: 2%">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <colgroup>
                                                <col width="80%" />
                                                <col width="20%" />
                                                <tr>
                                                    <td>
                                                        <asp:ListBox runat="server" ID="lstMapping" SelectionMode="Multiple" Rows="6" SkinID="ListBox300">
                                                        </asp:ListBox>
                                                        <asp:HiddenField ID="hdnKeyNameMapping" runat="server" />
                                                    </td>
                                                    <td>
                                                        <asp:CustomValidator ID="cvKeyMapping" runat="server" ClientValidationFunction="KeymappingValidation"
                                                            EnableClientScript="true" ErrorMessage="Invalid Selection">!</asp:CustomValidator>
                                                    </td>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 2%">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td align="right">
                                        <input type="button" id="btnDeleteMapping" runat="server" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <webControls:ExButton ID="btnSave" runat="server" SkinID="ExButton100" LabelID="1315"
                                OnClick="btnSave_Click" />
                            &nbsp;&nbsp;
                            <webControls:ExButton ID="btnCancel" runat="server" SkinID="ExButton100" LabelID="1239"
                                CausesValidation="false" />
                        </td>
                    </tr>
                </table>
                </td> </tr>
                <tr class="BlankRow">
                </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript" language="javascript">
        function KeymappingValidation(source, args) {
            var y = 0;
            var txt = "";
            var lstKyMapping = $get('<% =this.lstMapping.ClientID %>');
            var hdnCtrl = $get('<% = this.hdnKeyNameMapping.ClientID %>');
            if (lstKyMapping != null) {
                if (ValidateSelection()) {
                    //If selection is valid, prepare a string and save it in hiddenfield.
                    var lstOptions = lstKyMapping.options;
                    hdnCtrl.value = '';
                    for (y = 0; y < lstOptions.length; y++) {
                        //txt = lstOptions[y].text.split(' ').join('');
                        txt = lstOptions[y].text.replace(/\s+$/, "").replace(/^\s+/, ""); //Remove spaces from left and right
                        hdnCtrl.value = hdnCtrl.value + lstOptions[y].value + '^' + txt + ',';
                    }
                    args.IsValid = true;
                }
                else {
                    args.IsValid = false;
                }
            }
        }


        function ValidateSelection() {

            var lstHKeys = $get('<% =this.lstHierarchyKeys.ClientID %>');
            var lstMapping = $get('<% =this.lstMapping.ClientID %>');
            var validator = $get('<% =this.cvKeyMapping.ClientID %>');
            var HKeyOptions;
            var MapedOptions;
            var x = 0;
            var y = 0;
            if (lstHKeys != null && lstMapping != null) {
                HKeyOptions = lstHKeys.options;
                MapedOptions = lstMapping.options;
                if (HKeyOptions.length == 0)
                    return true;
                if (MapedOptions.length == 0) {
                    if (validator != null)
                        validator.setAttribute('ErrorMessage', '<% =this.InvalidSelection_NoSelection %>');
                    return false;
                }
                    
                for (x = 0; x < MapedOptions.length; x++) {
                    for (y = 0; y < HKeyOptions.length; y++) {
                        if (parseInt(MapedOptions[x].value) >= parseInt(HKeyOptions[y].value)) {
                            if (validator != null)
                                validator.setAttribute('ErrorMessage', '<% =this.InvalidSelection_WrongOrder %>');
                            return false;
                        }
                           
                    }
                }
                return true;
            }
        }
    
    </script>

</asp:Content>
