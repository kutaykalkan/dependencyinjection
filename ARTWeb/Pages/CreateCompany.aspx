<%@ Page Language="C#" AutoEventWireup="true" Inherits="CreateCompany"
    MasterPageFile="~/MasterPages/ARTMasterPage.master" Theme="SkyStemBlueBrown"
    ValidateRequest="false" Codebehind="CreateCompany.aspx.cs" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Import Namespace="SkyStem.Language.LanguageUtility" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnlCreateCompany" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td colspan="7">
                        <u>
                            <webControls:ExLabel ID="lblCompanyInfo" LabelID="1286" SkinID="BlueBold11Arial"
                                runat="server"></webControls:ExLabel></u>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td colspan="8">
                    </td>
                </tr>
                <%--Name And Display Name--%>
                <tr>
                    <td width="5%">
                        &nbsp;
                    </td>
                    <td class="ManadatoryField">
                        *
                    </td>
                    <td width="20%">
                        <webControls:ExLabel ID="lblName" LabelID="1287" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                    </td>
                    <td>
                        <webControls:ExTextBox ID="txtName" SkinID="ExTextBox200" IsRequired="true" runat="server" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td class="ManadatoryField">
                        *
                    </td>
                    <td>
                        <webControls:ExLabel ID="lblDisplayName" LabelID="1288" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                    </td>
                    <td>
                        <webControls:ExTextBox ID="txtDisplayName" SkinID="ExTextBox200" runat="server" IsRequired="true" />
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td colspan="8">
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td class="ManadatoryField">
                        *
                    </td>
                    <td>
                        <webControls:ExLabel ID="lblCompanyAlias" LabelID="2936" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                    </td>
                    <td>
                        <webControls:ExTextBox ID="txtCompanyAlias" SkinID="ExTextBox200" runat="server" MaxLength="15" IsRequired="true" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <%--<webControls:ExLabel ID="lblShowLogo" LabelID="2100" SkinID="Black11Arial" runat="server"></webControls:ExLabel>--%>
                        <webControls:ExLabel ID="lblLogo" LabelID="1290" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                    </td>
                    <td>
                        <telerikWebControls:ExRadUpload LabelID="2494" ID="RadFileUpload" runat="server" ControlObjectsVisibility="None"
                            InitialFileInputsCount="1" MaxFileInputsCount="1" Width="250px" />
                    </td>
                </tr>
                
                <%--Website And Logo--%>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <webControls:ExLabel ID="lblWebsite" LabelID="1289" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWebSite" SkinID="TextBox200" runat="server" />&nbsp;
                        <webControls:ExImage src="../App_Themes/SkyStemBlueBrown/Images/SearchIconButton.gif"
                            ID="imgWebsite" runat="server" LabelID="1318" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                         
                    </td>
                    <td>
                        <webControls:ExCheckBox ID="chkShowLogo" runat="server" SkinID= "CheckboxWithLabelBold" LabelID="2100" />
                        <asp:CustomValidator ID="cvCompanyLogo" runat="server"  OnServerValidate="cvCompanyLogo_OnServerValidate" Text="!" ></asp:CustomValidator>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td colspan="8">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td colspan="7">
                        <u>
                            <webControls:ExLabel ID="lblAddressDetail" LabelID="1291" SkinID="BlueBold11Arial"
                                runat="server"></webControls:ExLabel></u>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td colspan="8">
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <webControls:ExLabel ID="lblAddress" LabelID="1292" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                    </td>
                    <td>
                        <webControls:ExTextBox ID="txtAddress" SkinID="ExTextBox200" runat="server" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <webControls:ExLabel ID="lblCity" LabelID="1293" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                    </td>
                    <td>
                        <webControls:ExTextBox ID="txtCity" SkinID="ExTextBox200" runat="server" />
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td colspan="8">
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <webControls:ExLabel ID="lblState" LabelID="1294" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                    </td>
                    <td>
                        <webControls:ExTextBox ID="txtState" SkinID="ExTextBox200" runat="server" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <webControls:ExLabel ID="lblZip" LabelID="1295" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                    </td>
                    <td>
                        <webControls:ExTextBox ID="txtZip" SkinID="ExTextBox200" runat="server" />
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td colspan="8">
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <webControls:ExLabel ID="lblCountry" LabelID="1296" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                    </td>
                    <td colspan="5">
                        <webControls:ExTextBox ID="txtCountry" SkinID="ExTextBox200" runat="server" />
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td colspan="8">
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td class="ManadatoryField">
                        *
                    </td>
                    <td>
                        <webControls:ExLabel ID="ExLabel1" LabelID="1274" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                    </td>
                    <td>
                        <webControls:ExRadioButton ID="optYes" runat="server" LabelID="1252" GroupName="Active"
                            SkinID="OptBlack11Arial" />
                        &nbsp;&nbsp;&nbsp;
                        <webControls:ExRadioButton ID="optNo" runat="server" LabelID="1251" GroupName="Active"
                            SkinID="OptBlack11Arial" />
                        <webControls:ExCustomValidator runat="server" ClientValidationFunction="validateIsActive"
                            ID="cvIsActive">!</webControls:ExCustomValidator>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td colspan="8">
                    </td>
                </tr>
                <asp:Panel ID="pnlConfiguration" runat="server">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td colspan="7">
                            <u>
                                <webControls:ExLabel ID="lblConfiguration" LabelID="1297" SkinID="BlueBold11Arial"
                                    runat="server"></webControls:ExLabel></u>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8">
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td class="ManadatoryField">
                            *
                        </td>
                        <td>
                            <webControls:ExLabel ID="ExLabel2" LabelID="2172" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                        </td>
                        <td colspan="5">
                            <asp:DropDownList ID="ddlPackage" runat="server" SkinID="DropDownList200" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPackagea_SelectedIndexChanged">
                            </asp:DropDownList>
                            <webControls:ExHyperLink ID="hlPackageMatrix" runat="server"><webControls:ExImage  ID="imgPackageMatrix" runat="server" SkinID="PackageMatrixImage" Visible="true" /></webControls:ExHyperLink>
                            <webControls:ExRequiredFieldValidator ID="rfvPackage" runat="server" InitialValue="-2" ControlToValidate="ddlPackage"></webControls:ExRequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8">
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td class="ManadatoryField">
                            *
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblSubscription" LabelID="1298" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                        </td>
                        <td colspan="5">
                            <webControls:ExCalendar ID="calSubscriptionStartDate" runat="server" SkinID="ExCalendar200"></webControls:ExCalendar>
                            <webControls:ExRequiredFieldValidator ID="rfvCalenderStartDate" runat="server" ControlToValidate="calSubscriptionStartDate"></webControls:ExRequiredFieldValidator>
                            <webControls:ExCustomValidator ID="cvCalenderStartDate" runat="server" ControlToValidate="calSubscriptionStartDate"
                                Text="!" ClientValidationFunction="ValidateStartDate"></webControls:ExCustomValidator>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblSubscriptionEndDate" LabelID="1299" SkinID="Black11Arial"
                                runat="server"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExCalendar ID="calSubscriptionEndDate" runat="server" SkinID="ExCalendar200"></webControls:ExCalendar>
                            <%--<webControls:ExRequiredFieldValidator ID="rfvSubscriptionEnddate" runat="server"
                            ControlToValidate="calSubscriptionEndDate"></webControls:ExRequiredFieldValidator>--%>
                            <asp:CustomValidator ID="cvCompareWithSubscriptionStartDate" runat="server" Text="!"
                                ClientValidationFunction="CompareSubscriptionStartAndEndDates">  </asp:CustomValidator>
                            <asp:CustomValidator ID="cvMandatoryEndDateAndEndDates" runat="server" Text="!" 
                                ErrorMessage="abcd" ClientValidationFunction="MandatoryEndDateAndEndDates"></asp:CustomValidator>
                            <webControls:ExCustomValidator ID="cvcalEndDate" runat="server" Text="!" 
                                ControlToValidate="calSubscriptionEndDate" ClientValidationFunction="ValidateEndDate"></webControls:ExCustomValidator>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblOR" LabelID="1301" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                        </td>
                        <td>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblNumOfDays" LabelID="1300" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNumOfDays" MaxLength ="4" SkinID="TextBox200" runat="server" />
                            <webControls:ExCustomValidator ID="cvNumOfDays" runat="server" Text="!" 
                                ControlToValidate="txtNumOfDays" ClientValidationFunction="ValidateNoOfDays"></webControls:ExCustomValidator>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td class="ManadatoryField">
                            *
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblLicencedUser" LabelID="1302" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                        </td>
                        <td>
                            <%--  <webControls:ExTextBox ID="txtLicencedUser" SkinID="ExTextBox200" IsRequired="true"
                                runat="server" />--%>
                            <asp:TextBox ID="txtLicencedUser" SkinID="TextBox200" runat="server"></asp:TextBox>
                            <webControls:ExRequiredFieldValidator ID="requiredFieldLicencedUser" runat="server"
                                ControlToValidate="txtLicencedUser"></webControls:ExRequiredFieldValidator>
                            <webControls:ExCompareValidator Operator="DataTypeCheck" ControlToValidate="txtLicencedUser"
                                ID="cmpvldNumericValidator" Type="Integer" runat="server"></webControls:ExCompareValidator>
                            <asp:CustomValidator ID="cvLicencedUser" runat="server" Text="!" 
                                ClientValidationFunction="CompareNoOfLicensedUser">  </asp:CustomValidator>
                        </td>
                        <td>
                        </td>
                        <td class="ManadatoryField">
                            *
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblDataStorage" LabelID="1303" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataStorage" SkinID="TextBox200" runat="server"></asp:TextBox>
                            <webControls:ExLabel ID="lblInMBs" SkinID="Black11Arial" LabelID="1352" runat="server"
                                FormatString="({0})"></webControls:ExLabel>
                            <webControls:ExRequiredFieldValidator ID="requiredFieldDataStorage" runat="server"
                                ControlToValidate="txtDataStorage"></webControls:ExRequiredFieldValidator>
                            <asp:CustomValidator ID="cvDataStorage" runat="server" Text="!" 
                                ClientValidationFunction="CompareDataStorage"> </asp:CustomValidator>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8">
                        </td>
                    </tr>
                    <tr id="trUseSeparateDatabase" runat="server">
                        <td>
                            &nbsp;
                        </td>
                        <td class="ManadatoryField">
                            *
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblUseSeparateDatabase" LabelID="2659" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExRadioButton ID="optSeparateDatabaseYes" runat="server" LabelID="1252" GroupName="IsSeparateDatabase"
                                SkinID="OptBlack11Arial" />
                            &nbsp;&nbsp;&nbsp;
                            <webControls:ExRadioButton ID="optSeparateDatabaseNo" runat="server" LabelID="1251" GroupName="IsSeparateDatabase"
                                SkinID="OptBlack11Arial" />
                            <webControls:ExCustomValidator runat="server" ClientValidationFunction="validateIsStandAlone"
                                ID="cvUseSeparateDatabase">!</webControls:ExCustomValidator>
                        </td>
                         <td>
                            &nbsp;
                        </td>
                        <td class="ManadatoryField">
                            *
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblEnableFTP" LabelID="2902" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExRadioButton ID="optEnableFTPYes" runat="server" LabelID="1252" GroupName="IsFTPEnabled"
                                SkinID="OptBlack11Arial" />
                            &nbsp;&nbsp;&nbsp;
                            <webControls:ExRadioButton ID="optEnableFTPNo" runat="server" LabelID="1251" GroupName="IsFTPEnabled"
                                SkinID="OptBlack11Arial" />
                            <webControls:ExCustomValidator runat="server" ClientValidationFunction="validateIsFTPEnabled"
                                ID="cvEnableFTP">!</webControls:ExCustomValidator>
                        </td>
                    </tr>
                </asp:Panel>
                <tr class="BlankRow">
                    <td colspan="8">
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td colspan="7">
                        <u>
                            <webControls:ExLabel ID="lblContactsDetail" LabelID="1304" SkinID="BlueBold11Arial"
                                runat="server"></webControls:ExLabel></u>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td colspan="7">
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td class="ManadatoryField">
                        *
                    </td>
                    <td>
                        <webControls:ExLabel ID="lblContactName" LabelID="1287" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                    </td>
                    <td>
                        <webControls:ExTextBox ID="txtContactName" SkinID="ExTextBox200" runat="server" IsRequired="true" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        <webControls:ExLabel ID="lblEmail" LabelID="1305" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" SkinID="TextBox200" MaxLength="128"></asp:TextBox>
                        <webControls:ExRegularExpressionValidator ID="revEmailId" runat="server" ControlToValidate="txtEmail"
                            ValidationExpression="^([a-zA-Z0-9_\-\.]+)@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$"
                            LabelID="1751"></webControls:ExRegularExpressionValidator>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td colspan="8">
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <webControls:ExLabel ID="lblTelephone" LabelID="1306" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                    </td>
                    <td colspan="5">
                        <webControls:ExTextBox ID="txtTelephone" SkinID="ExTextBox200" runat="server" />
                        <%--<webControls:ExRegularExpressionValidator ID="revEmail" runat="server" ></webControls:ExRegularExpressionValidator>--%>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td colspan="8">
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                    <td colspan="7" align="right">
                        <webControls:ExButton ID="btnSave" runat="server" Width="90" Height="25" LabelID="1315"
                            OnClick="btnSave_Click" />
                        <webControls:ExButton ID="btnCancel" runat="server" Width="90" LabelID="1239" Height="25"
                            CausesValidation="false" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        function ValidateNoOfDays(sender, args) {
            var noOfDays = document.getElementById('<%=txtNumOfDays.ClientID%>');
            args.IsValid = IsPositiveInteger(noOfDays);
        }

        function ValidateStartDate(sender, args) {
            var SubscriptionEndDate = document.getElementById('<%=calSubscriptionStartDate.ClientID%>');
            args.IsValid = IsDate(SubscriptionEndDate.value);
        }

        function ValidateEndDate(sender, args) {
            var SubscriptionEndDate = document.getElementById('<%=calSubscriptionEndDate.ClientID%>');
            args.IsValid = IsDate(SubscriptionEndDate.value);
        }

        function CallbackFromCalendar() {
            var txtNumOfDaysID = '<%=this.txtNumOfDays.ClientID %>';
            var calSubscriptionEndDateID = '<%=this.calSubscriptionEndDate.ClientID %>';
            var calSubscriptionStartDateID = '<%=this.calSubscriptionStartDate.ClientID %>';
            calNoDays(txtNumOfDaysID, calSubscriptionEndDateID, calSubscriptionStartDateID);
        }

        function validateIsActive(source, args) {
            var groupValue = false;
            var rb1 = document.getElementById('<%=this.optYes.ClientID %>');
            var rb2 = document.getElementById('<%=this.optNo.ClientID %>');
            if ((rb1 != null) && (rb1 != null)) {
                if ((rb1.checked == false) && (rb2.checked == false)) {
                    args.IsValid = false;
                }
                else
                    args.IsValid = true;
            }
        }

        function validateIsStandAlone(source, args) {
            var groupValue = false;
            var rb1 = document.getElementById('<%=this.optSeparateDatabaseYes.ClientID %>');
            var rb2 = document.getElementById('<%=this.optSeparateDatabaseNo.ClientID %>');
            if ((rb1 != null) && (rb1 != null)) {
                if ((rb1.checked == false) && (rb2.checked == false)) {
                    args.IsValid = false;
                }
                else
                    args.IsValid = true;
            }
        }
        function CompareSubscriptionStartAndEndDates(sender, args) {
            var SubscriptionStartDate = document.getElementById('<%=calSubscriptionStartDate.ClientID%>');
            var SubscriptionEndDate = document.getElementById('<%=calSubscriptionEndDate.ClientID%>');
            var StartDate;
            var EndDate = SubscriptionEndDate.value;
            StartDate = SubscriptionStartDate.value;

            var result = CompareDates(StartDate, EndDate);

            if (result > 0) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }


        function MandatoryEndDateAndEndDates(sender, args) {
            var SubscriptionNumOfDays = document.getElementById('<%=txtNumOfDays.ClientID%>');
            var SubscriptionEndDate = document.getElementById('<%=calSubscriptionEndDate.ClientID%>');
            var NumOfDays;
            var EndDate = SubscriptionEndDate.value;
            NumOfDays = SubscriptionNumOfDays.value;
            if (EndDate.length <= 0 && NumOfDays.length <= 0) {
                //            alert(EndDate + '-' + NumOfDays);
                args.IsValid = false;
            }
            else {
                //           alert(EndDate +'-' + NumOfDays);
                args.IsValid = true;
            }
        }

        function CompareNoOfLicensedUser(sender, args) {
            var NoofLicensedUser = document.getElementById('<%=this.txtLicencedUser.ClientID %>');
            var LicensedUser;
            LicensedUser = NoofLicensedUser.value;

            if (LicensedUser == null) {
                args.IsValid = true;
            }
            else {

                if (IsInteger(NoofLicensedUser) && LicensedUser <= 0) {
                    args.IsValid = false;
                }
                else {
                    args.IsValid = true;
                }

            }


        }


        function CompareDataStorage(sender, args) {
            var DataStorage = document.getElementById('<%=this.txtDataStorage.ClientID %>');
            var DataStorageValue;
            DataStorageValue = DataStorage.value;

            if (DataStorageValue == null || DataStorageValue == "") {
                args.IsValid = true;
            }
            else {
                if (IsPositiveDecimal(DataStorage)) {
                    args.IsValid = true;
                }
                else {

                    args.IsValid = false;
                }
            }
        }


        function CheckWebsite(obj) {
            var text = document.getElementById(obj);
            if (text != null && text.value != "") {
                window.open("http://" + text.value + "/");
                //window.setTimeout("delayit()", 3000);
            }
            else {
                var msg = '<% =LanguageUtil.GetValue(5000248) %>';
                alert(msg);
            }     
        }
       
        function validateIsFTPEnabled(source, args) {
            var groupValue = false;
            var rb1 = document.getElementById('<%=this.optEnableFTPYes.ClientID %>');
            var rb2 = document.getElementById('<%=this.optEnableFTPNo.ClientID %>');
            if ((rb1 != null) && (rb1 != null)) {
                if ((rb1.checked == false) && (rb2.checked == false)) {
                    args.IsValid = false;
                }
                else
                    args.IsValid = true;
            }
        }
        
        
    </script>

</asp:Content>
