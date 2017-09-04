<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" Inherits="Pages_CreateUser" Title="Untitled Page"
    Theme="SkyStemBlueBrown" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" Codebehind="CreateUser.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register TagPrefix="UserControl" TagName="RoleSelection" Src="~/UserControls/RoleSelection.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding-left: 40px; padding-right: 40px">
        <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <asp:ValidationSummary ID="valSummaryLocal" ValidationGroup="LocalGroup" runat="server" />
                <table style="width: 100%">
                    <tr id="trUser" runat="server">
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <webControls:ExLabel ID="lblGeneralProfile" LabelID="1263" FormatString="{0}:" runat="server"
                                            Font-Underline="true" SkinID="BlueBold11Arial"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        <table style="width: 100%;">
                                            <colgroup>
                                                <col width="1%" />
                                                <col width="15%" />
                                                <col width="34%" />
                                                <col width="1%" />
                                                <col width="15%" />
                                                <col width="34%" />
                                                <tr>
                                                    <td class="ManadatoryField">* </td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblFirstName" runat="server" FormatString="{0}:" LabelID="1267" SkinID="Black11Arial"></webControls:ExLabel>
                                                    </td>
                                                    <td>
                                                        <webControls:ExTextBox ID="txtFirstName" runat="server" IsRequired="true" MaxLength="100" SkinID="ExTextBox200" />
                                                    </td>
                                                    <td class="ManadatoryField">* </td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblLastName" runat="server" FormatString="{0}:" LabelID="1268" SkinID="Black11Arial"></webControls:ExLabel>
                                                    </td>
                                                    <td>
                                                        <webControls:ExTextBox ID="txtLastName" runat="server" IsRequired="true" MaxLength="100" SkinID="ExTextBox200" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ManadatoryField">* </td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblLoginId" runat="server" FormatString="{0}:" LabelID="1269" SkinID="Black11Arial"></webControls:ExLabel>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtLoginId" runat="server" MaxLength="128" SkinID="TextBox200" />
                                                        <webControls:ExLabel ID="lblLoginText" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                                        <webControls:ExRegularExpressionValidator ID="revLoginID" runat="server" ControlToValidate="txtLoginID" LabelID="5000195"
                                                            ValidationExpression="^([a-zA-Z0-9_\-\.]+)@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$"></webControls:ExRegularExpressionValidator>
                                                        <webControls:ExRequiredFieldValidator ID="rfvLoginID" runat="server" ControlToValidate="txtLoginId"></webControls:ExRequiredFieldValidator>
                                                        <asp:HiddenField ID="hdnLoginID" runat="server" />
                                                    </td>
                                                    <td class="ManadatoryField">* </td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblEmailId" runat="server" FormatString="{0}:" LabelID="1270" SkinID="Black11Arial"></webControls:ExLabel>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmailId" runat="server" MaxLength="128" SkinID="TextBox200"></asp:TextBox>
                                                        <webControls:ExRegularExpressionValidator ID="revEmailId" runat="server" ControlToValidate="txtEmailId" LabelID="1751" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$"></webControls:ExRegularExpressionValidator>
                                                        <webControls:ExRequiredFieldValidator ID="rfvEmailId" runat="server" ControlToValidate="txtEmailId"></webControls:ExRequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblJobTitle" runat="server" FormatString="{0}:" LabelID="1271" SkinID="Black11Arial"></webControls:ExLabel>
                                                    </td>
                                                    <td>
                                                        <webControls:ExTextBox ID="txtJobTitle" runat="server" MaxLength="100" SkinID="ExTextBox200" />
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblWorkPhone" runat="server" FormatString="{0}:" LabelID="1272" SkinID="Black11Arial"></webControls:ExLabel>
                                                    </td>
                                                    <td>
                                                        <webControls:ExTextBox ID="txtWorkPhone" runat="server" MaxLength="50" SkinID="ExTextBox200" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblPhone" runat="server" FormatString="{0}:" LabelID="1273" SkinID="Black11Arial"></webControls:ExLabel>
                                                    </td>
                                                    <td>
                                                        <webControls:ExTextBox ID="txtPhone" runat="server" MaxLength="50" SkinID="ExTextBox200" />
                                                    </td>
                                                    <td class="ManadatoryField">* </td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblActive" runat="server" FormatString="{0}:" LabelID="1274" SkinID="Black11Arial"></webControls:ExLabel>
                                                    </td>
                                                    <td>
                                                        <webControls:ExRadioButton ID="optActiveYes" runat="server" GroupName="IsActive" LabelID="1252" SkinID="OptBlack11Arial" />
                                                        <webControls:ExRadioButton ID="optActiveNo" runat="server" GroupName="IsActive" LabelID="1251" SkinID="OptBlack11Arial" />
                                                        <asp:HiddenField ID="hdnCurrentStatus" runat="server" />
                                                        <webControls:ExCustomValidator ID="cvIsActive" runat="server" ClientValidationFunction="validateIsActive" LabelID="1720">!</webControls:ExCustomValidator>
                                                        <webControls:ExCustomValidator ID="cvUserRoleCheckForInActive" runat="server" OnServerValidate="cvUserRoleCheckForInActive_OnServerValidate" ValidationGroup="LocalGroup">!</webControls:ExCustomValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ManadatoryField">* </td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblDefaultLanguage" runat="server" FormatString="{0}:" LabelID="2486" SkinID="Black11Arial"></webControls:ExLabel>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="false" SkinID="DropDownList200" />
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblIsUserLocked" runat="server" FormatString="{0}:" LabelID="2939" SkinID="Black11Arial"></webControls:ExLabel>
                                                    </td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblIsUserLockedValue" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
                                                    </td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <webControls:ExLabel ID="lblRole" LabelID="1278" runat="server" FormatString="{0}:"
                                            Font-Underline="true" SkinID="BlueBold11Arial"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 20px">
                                        <table style="width: 100%;">
                                            <colgroup>
                                                <col width="1%" />
                                                <col width="15%" />
                                                <tr id="rowPotentialRole" runat="server">
                                                    <td class="ManadatoryField">* </td>
                                                    <td style="vertical-align: top;">
                                                        <webControls:ExLabel ID="lblPotentialRole" runat="server" FormatString="{0}:" LabelID="1275" SkinID="Black11Arial"></webControls:ExLabel>
                                                    </td>
                                                    <td style="vertical-align: top;">
                                                        <UserControl:RoleSelection ID="UserRoleSelection" runat="server" AvailableListLableId="1254" DefaultWidth="100" DisplayAvailableRows="10" DisplaySelectedRows="10" IsRequired="false" OnSelectedListItemsChange="UserRoleSelection_RoleSelectionChanged" SelectedListLableId="1255" />
                                                    </td>
                                                    <td>
                                                        <webControls:ExCustomValidator ID="cvUserRoleCheckForRoleRemoval" runat="server" OnServerValidate="cvUserRoleCheckForRoleRemoval_OnServerValidate" ValidationGroup="LocalGroup">!</webControls:ExCustomValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblDefaultRole" runat="server" FormatString="{0}:" LabelID="1276" SkinID="Black11Arial"></webControls:ExLabel>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDefaultRole" runat="server" EnableViewState="true" onChange="SetHiddenValue();" SkinID="DropDownList200">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnSelectItem" runat="server" />
                                                        <asp:HiddenField ID="hdnSelectValue" runat="server" />
                                                        <asp:HiddenField ID="hdnDefaultRole" runat="server" />
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trEnableFTPLabel" runat="server">
                                    <td>
                                        <webControls:ExLabel ID="lblFTPDetails" LabelID="2901" FormatString="{0}:" runat="server"
                                            Font-Underline="true" SkinID="BlueBold11Arial"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr id="trEnableFTP" runat="server">
                                    <td style="padding-left: 29px">
                                        <table style="width: 100%;">
                                            <colgroup>
                                                <col width="1%" />
                                                <col width="15%" />
                                                <col width="34%" />
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblEnableFTP" runat="server" FormatString="{0}:" LabelID="2902" SkinID="Black11Arial"></webControls:ExLabel>
                                                    </td>
                                                    <td>
                                                        <webControls:ExRadioButton ID="rbYes" runat="server" GroupName="IsEnable" LabelID="1252"
                                                            SkinID="OptBlack11Arial" />
                                                        <webControls:ExRadioButton ID="rbNo" runat="server" GroupName="IsEnable" LabelID="1251"
                                                            SkinID="OptBlack11Arial" />
                                                    </td>
                                                    <td class="ManadatoryField">* </td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblFTPServer" runat="server" FormatString="{0}:" LabelID="2903" SkinID="Black11Arial"></webControls:ExLabel>
                                                    </td>
                                                    <td style="margin-left: 50px; float: left;">
                                                        <asp:DropDownList ID="ddlServerFTP" runat="server" AutoPostBack="false" SkinID="DropDownList200" />
                                                        <webControls:ExRequiredFieldValidator ID="rfvServerFTP" runat="server" ControlToValidate="ddlServerFTP" Enabled="false"
                                                            Display="Static">!</webControls:ExRequiredFieldValidator>
                                                        <asp:CustomValidator ID="cvEnableFTP" runat="server" ValidationGroup="LocalGroup" ControlToValidate="ddlServerFTP"
                                                            OnServerValidate="cvEnableFTP_ServerValidate" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ManadatoryField">* </td>
                                                    <td>
                                                        <webControls:ExLabel ID="lblFTPLoginID" runat="server" FormatString="{0}:" LabelID="2925" SkinID="Black11Arial"></webControls:ExLabel>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFTPLoginID" runat="server" MaxLength="20" SkinID="TextBox200" />
                                                        <webControls:ExLabel ID="lblFTPLoginIDValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                                        <webControls:ExRegularExpressionValidator ID="revFTPLoginID" runat="server" ControlToValidate="txtFTPLoginID" LabelID="5000195"
                                                            ValidationExpression="^[A-Za-z0-9\._-]{10,20}$"></webControls:ExRegularExpressionValidator>
                                                        <webControls:ExRequiredFieldValidator ID="rfvFTPLoginID" runat="server" Enabled="false" ControlToValidate="txtFTPLoginId"></webControls:ExRequiredFieldValidator>
                                                        <asp:HiddenField ID="hdnFTPLoginID" runat="server" />
                                                    </td>
                                                    <td colspan="3"></td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <webControls:ExButton LabelID="1315" runat="server" ID="btnSave" OnClick="btnSave_Click" />
                                        <webControls:ExButton LabelID="1239" runat="server" ID="btnCancel" OnClick="btnCancel_Click"
                                            CausesValidation="false" />
                                        <webControls:ExButton LabelID="1924" runat="server" ID="btnAccountAssociation" OnClick="btnAccountAssociation_Click"
                                            SkinID="ExButton150" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trButtons" runat="server" visible="false">
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <webControls:ExButton ID="btnAddMore" runat="server" LabelID="1531" OnClick="btnAddMore_Click"
                                            CausesValidation="false" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <webControls:ExButton ID="btnHome" runat="server" LabelID="1532" OnClick="btnHome_Click"
                                            CausesValidation="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <UserControls:ProgressBar ID="ucAccountViewer" runat="server" EnableTheming="true"
                                AssociatedUpdatePanelID="upnlMain" Visible="true" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript" language="javascript">

        function FTPEnableChanged() {
            var rbYes = document.getElementById('<%= this.rbYes.ClientID %>');
            var rfvFTPLoginID = document.getElementById('<%= this.rfvFTPLoginID.ClientID %>');
            var ddlServerFTP = document.getElementById('<%=this.ddlServerFTP.ClientID%>');
            var rfvServerFTP = document.getElementById('<%=this.rfvServerFTP.ClientID%>');
            if (rbYes != 'undefined' && rbYes != null && rbYes.checked == true) {
                ValidatorEnable(rfvFTPLoginID, true);
                ValidatorEnable(rfvServerFTP, true);
            }
            else {
                ValidatorEnable(rfvFTPLoginID, false);
                ValidatorEnable(rfvServerFTP, false);
            }
        }

        function validateIsActive(source, args) {
            var groupValue = false;
            var rb1 = document.getElementById('<%=this.optActiveYes.ClientID %>');
            var rb2 = document.getElementById('<%=this.optActiveNo.ClientID %>');
            if ((rb1 != null) && (rb1 != null)) {
                if ((rb1.checked == false) && (rb2.checked == false)) {
                    args.IsValid = false;
                }
                else
                    args.IsValid = true;
            }
        }

        $(document).ready(function () {
            $(document.getElementById('<%=this.hdnSelectValue.ClientID %>')).change(
                function EnableDisableFTPControls(source, args) {
                    var hdnSelectValue = document.getElementById('<%= this.hdnSelectValue.ClientID %>');
                    var ddlServerFTP = document.getElementById('<%= this.ddlServerFTP.ClientID %>');
                    var txtFTPLoginID = document.getElementById('<%= this.txtFTPLoginID.ClientID %>');
                    var rbYes = document.getElementById('<%= this.rbYes.ClientID %>');
                    var rbNo = document.getElementById('<%= this.rbNo.ClientID %>');
                    var revFTPLoginID = document.getElementById('<%= this.revFTPLoginID.ClientID %>');
                    var rfvFTPLoginID = document.getElementById('<%= this.rfvFTPLoginID.ClientID %>');
                    if (rbYes != 'undefined' && rbNo != 'undefined' && ddlServerFTP != 'undefined' && txtFTPLoginID != 'undefined'
                        && rbYes != null && rbNo != null && ddlServerFTP != null && txtFTPLoginID != null) {
                        var serverID = $(ddlServerFTP).val();
                        var arr = hdnSelectValue.value.split(",");
                        var roleFound = false;
                        for (i = 0; i < arr.length; i++) {
                            if (arr[i] == '<%= ((short)SkyStem.ART.Client.Data.ARTEnums.UserRole.SYSTEM_ADMIN).ToString() %>'
                                || arr[i] == '<%= ((short)SkyStem.ART.Client.Data.ARTEnums.UserRole.BUSINESS_ADMIN).ToString() %>') {
                                roleFound = true;
                            }
                        }
                        if (roleFound == false) {
                            rbYes.checked = false;
                            rbNo.checked = false;
                            $(ddlServerFTP).val('<%= SkyStem.ART.Web.Data.WebConstants.SELECT_ONE %>');
                            $(txtFTPLoginID).val('');
                        }
                        rbYes.disabled = !roleFound;
                        rbNo.disabled = !roleFound;
                        ddlServerFTP.disabled = !roleFound;
                        txtFTPLoginID.disabled = !roleFound || rbNo.checked;
                    }
                }
            );
        });
    </script>

</asp:Content>
