<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" Inherits="Pages_CreateDataImportTemplate" Theme="SkyStemBlueBrown" Codebehind="CreateDataImportTemplate.aspx.cs" %>

<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <%-- code starts from here--%>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td style="padding-left: 15px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="ManadatoryField">*
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblTemplateName" runat="server" SkinID="Black11Arial" LabelID="2860"
                                FormatString="{0}:"></webControls:ExLabel>
                        </td>
                        <td colspan="2">
                            <webControls:ExTextBox ID="txtTemplateName" runat="server" SkinID="ExTextBox200" IsRequired="true"
                                EnableViewState="true" MaxLength="50" />
                            &nbsp;&nbsp;
                        </td>
                        <td class="ManadatoryField">*
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblLanguage" runat="server" SkinID="Black11Arial" LabelID="2486"
                                FormatString="{0}:" />
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlLanguage" runat="server" SkinID="DropDownList200">
                            </asp:DropDownList>
                            <webControls:ExRequiredFieldValidator ID="rfvImportType" runat="server" ControlToValidate="ddlLanguage"
                                Display="Static">!</webControls:ExRequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td></td>
                    </tr>
                    <tr>
                        <td class="ManadatoryField">*
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblSheetName" runat="server" SkinID="Black11Arial" LabelID="2861"
                                FormatString="{0}:" />
                        </td>
                        <td colspan="2" style="white-space: nowrap;">

                            <webControls:ExTextBox ID="txtSheetName" runat="server" SkinID="ExTextBox200" IsRequired="true"
                                EnableViewState="true" MaxLength="50" />
                        </td>

                        <td class="ManadatoryField">*
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblDataImportType" runat="server" SkinID="Black11Arial" LabelID="1307"
                                FormatString="{0}:" />
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlDataImportType" runat="server" SkinID="DropDownList200" />
                            <webControls:ExRequiredFieldValidator ID="rfvData" runat="server" ControlToValidate="ddlDataImportType"
                                Display="Static">!</webControls:ExRequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td></td>
                    </tr>

                    <tr>
                        <td class="ManadatoryField">*
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblTemplateFile" runat="server" SkinID="Black11Arial" LabelID="2862"
                                FormatString="{0}:" />
                        </td>
                        <td colspan="0">
                            <telerikWebControls:ExRadUpload LabelID="2494" ID="RadFileUpload" runat="server" ControlObjectsVisibility="None"
                                InitialFileInputsCount="1" MaxFileInputsCount="1" InputSize="35" />
                            <asp:CustomValidator ID="cvFileUpload" runat="server" ClientValidationFunction="ValidateFileExtension"
                                Display="Dynamic" ErrorMessage="" OnServerValidate="cvFileUpload_ServerValidate" Text="!"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td></td>
                    </tr>
                    <tr id="trButtons" runat="server">
                        <td align="right" colspan="7">
                            <webControls:ExButton ID="btnImport" runat="server" LabelID="1315" SkinID="ExButton100"
                                OnClick="btnImport_Click" CausesValidation="true" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="2">
                            <telerikWebControls:ExRadGrid ID="ucSkyStemARTGrid" runat="server" OnItemDataBound="ucSkyStemARTGrid_ItemDataBound"
                                 AllowMultiRowSelection="true" ClientSettings-Selecting-AllowRowSelect="true" OnItemCommand="ucSkyStemARTGrid_ItemCommand" OnPageIndexChanged="ucSkyStemARTGrid_PageIndexChanged"
                                AllowExportToExcel="true" AllowExportToPDF="true" AllowCauseValidationExportToExcel="false" AllowCauseValidationExportToPDF="false" OnItemCreated="ucSkyStemARTGrid_ItemCreated">
                                <ClientSettings>
                                    <Selecting UseClientSelectColumnOnly="true" />
                                </ClientSettings>
                                <MasterTableView ClientDataKeyNames="ImportTemplateID"
                                    DataKeyNames="ImportTemplateID" Width="100%" TableLayout="Auto"
                                    Name="MappingItemsGridView" AllowPaging="true">
                                    <Columns>
                                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" Visible="true"
                                            HeaderStyle-Width="1%">
                                        </telerikWebControls:ExGridClientSelectColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2860" HeaderStyle-Width="10%" SortExpression="TemplateName"
                                            DataType="System.String" UniqueName="TemplateName">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblTemplateName" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2486" HeaderStyle-Width="10%" SortExpression="Language"
                                            DataType="System.String" UniqueName="Language">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblLanguage" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2861" HeaderStyle-Width="10%" SortExpression="SheetName"
                                            DataType="System.String" UniqueName="SheetName">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblSheetName" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1307" HeaderStyle-Width="10%" SortExpression="DataImportType"
                                            DataType="System.String" UniqueName="DataImportType">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblDataImportType" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2027" HeaderStyle-Width="10%" SortExpression="FileName"
                                            DataType="System.String" UniqueName="FileName">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblFileName" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2087" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <webControls:ExImageButton ID="imgFileType" runat="server" SkinID="FileDownloadIcon" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2556" HeaderStyle-Width="10%" SortExpression="CreatedBy"
                                            DataType="System.String" UniqueName="CreatedBy">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblCreatedBy" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2557" HeaderStyle-Width="10%" SortExpression="DateCreated"
                                            DataType="System.String" UniqueName="DateCreated">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblDateCreated" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1552" HeaderStyle-Width="10%" SortExpression="DateRevised"
                                            DataType="System.String" UniqueName="DateRevised">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblDateRevised" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1543" HeaderStyle-Width="10%" SortExpression="RevisedBy"
                                            DataType="System.String" UniqueName="RevisedBy">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblRevisedBy" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2906" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <webControls:ExImage ID="imgSuccess" runat="server" SkinID="SuccessIcon" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="EditRecord"
                                            HeaderStyle-Width="9%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnEdit" runat="server" ImageUrl="~/App_Themes/SkyStemBlueBrown/Images/dataTypeMapping.gif" CommandName="EDIT" CausesValidation="false" Visible="false" ToolTip="Update Mapping" />
                                                <asp:ImageButton ID="imgbtnViewRecord" runat="server" ImageUrl="~/App_Themes/SkyStemBlueBrown/Images/ReadOnly.gif" CommandName="VIEW" CausesValidation="false" Visible="false" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerikWebControls:ExRadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td align="right" colspan="7">
                <webControls:ExButton ID="btnDelete" runat="server" LabelID="1564" SkinID="ExButton100"
                    OnClick="btnDelete_Click" CausesValidation="false" />
            </td>
        </tr>
    </table>
    <iframe id="ifDownloader" runat="server" style="display:none;" />
    <script type="text/javascript">
        function ValidateFileExtension(source, arguments) {
            var flag = 0;
            var uploadControl = getRadUpload('<%= RadFileUpload.ClientID %>');
            if (uploadControl != null) {
                var fileInputs = uploadControl.getFileInputs();
                if (fileInputs[0].value == "") {
                    source.errormessage = '<%= Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField,2862) %>';
                    arguments.IsValid = false;
                }
                if (!uploadControl.validateExtensions()) {
                    source.errormessage = '<%= SkyStem.Language.LanguageUtility.LanguageUtil.GetValue(5000036) %>';
                    arguments.IsValid = false;
                }
            }
        }
    </script>
</asp:Content>

