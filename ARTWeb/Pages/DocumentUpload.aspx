<%@ Page Language="C#" AutoEventWireup="true" Inherits="Pages_DocumentUpload"
    Theme="SkyStemBlueBrown" MasterPageFile="~/MasterPages/PopUpMasterPage.master" Codebehind="DocumentUpload.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="AccountHierarchyDetail" Src="~/UserControls/AccountHierarchyDetail.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnItemId" runat="server" />
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <asp:Panel ID="pnlHideOnEdit" runat="server">
            <tr>
                <td colspan="7">
                    <UserControls:AccountHierarchyDetail ID="ucAccountHierarchyDetail" runat="server" />
                </td>
            </tr>
            <tr class="BlankRow">
                <td colspan="7"></td>
            </tr>
            <tr>
                <td style="width: 2%"></td>
                <%--RecPeriod--%>
                <td style="width: 20%">
                    <webControls:ExLabel ID="lblInputFormRecPeriod" runat="server" LabelID="1420" FormatString="{0}:"
                        SkinID="Black11Arial"></webControls:ExLabel>
                </td>
                <td style="width: 2%"></td>
                <td style="width: 27%">
                    <webControls:ExLabel ID="lblInputFormRecPeriodValue" runat="server" SkinID="ReadOnlyValue"
                        Text=""></webControls:ExLabel>
                </td>
                <td style="width: 2%"></td>
                <td style="width: 20%">
                    <webControls:ExLabel ID="lblEnteredDate" runat="server" LabelID="1399" SkinID="Black11Arial"
                        FormatString="{0}:"></webControls:ExLabel>
                </td>
                <td style="width: 27%">
                    <webControls:ExLabel ID="lblEnteredDateValue" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
                </td>
            </tr>
            <tr class="BlankRow">
                <td colspan="7"></td>
            </tr>
            <tr>
                <td></td>
                <%--Entered By--%>
                <td>
                    <webControls:ExLabel ID="lblItemInputEnteredBy" runat="server" LabelID="1679" SkinID="Black11Arial"
                        FormatString="{0}:"></webControls:ExLabel>
                </td>
                <td />
                <td>
                    <webControls:ExLabel ID="lblEnteredByValue" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
                </td>
                <td />
                <td colspan="2">
                    <webControls:ExLabel ID="lblMaxFileSize" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
                <td colspan="5">
                    <webControls:ExLabel ID="ExLabel2" runat="server" SkinID="Black9ArialItalic"></webControls:ExLabel>
                </td>
            </tr>
            <tr class="BlankRow">
            </tr>
            <tr>
                <td colspan="7">
                    <asp:UpdatePanel ID="rapFileUpload" runat="server">
                        <ContentTemplate>
                            <asp:Repeater ID="rptFileUpload" runat="server" OnItemDataBound="rptFileUpload_ItemDataBound"
                                OnItemCommand="rptFileUpload_ItemCommand">
                                <HeaderTemplate>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td class="ManadatoryField">*
                                            </td>
                                            <td width="36%">
                                                <webControls:ExLabel ID="lblDocumentName" runat="server" LabelID="1680" FormatString="{0}:"
                                                    SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <td class="ManadatoryField">*
                                            </td>
                                            <td width="24%">
                                                <webControls:ExLabel ID="lblDocument" runat="server" LabelID="1309" FormatString="{0}:"
                                                    SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <asp:Panel ID="pnlFileTypeHdr" runat="server">
                                                <td class="ManadatoryField">*
                                                </td>
                                                <td width="25%">
                                                    <webControls:ExLabel ID="lblFileType" LabelID="2028" runat="server" FormatString="{0}:"
                                                        SkinID="Black11Arial" />
                                                </td>
                                                  <td />
                                            </asp:Panel>
                                          
                                            <td width="5%">&nbsp;</td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td />
                                        <td>
                                            <webControls:ExTextBox ID="txtDocumentName" runat="server" SkinID="ExTextBox250"
                                                Text='<%# Bind("DocumentName") %>'
                                                IsRequired="true" MaxLength="50" />
                                            <asp:HiddenField ID="hdnRowNumber" runat="server" Value='<%# Bind("RowNumber") %>' />
                                        </td>
                                        <td />
                                        <td style="white-space: nowrap;">
                                            <%--<asp:FileUpload ID="fileUpload" runat="server" SkinID="FileUpload200" />--%>
                                            <%--<input type="file" runat="server" id="inputFile" />--%>
                                            <table cellpadding="0" cellspacing="0" border="0">
                                                <colgroup>
                                                    <col width="80%" />
                                                    <col width="20%" />
                                                </colgroup>
                                                <tr>
                                                    <td>
                                                        <%--<telerik:RadProgressManager ID="Radprogressmanager1" runat="server" />--%>
                                                        <telerikWebControls:ExRadUpload LabelID="2494" ID="ruRadFileUpload" runat="server" Width="250px" Height="21px"
                                                            ControlObjectsVisibility="None" InitialFileInputsCount="1" InputSize="26" MaxFileInputsCount="1" />
                                                        <%--<telerik:RadProgressArea ID="RadProgressArea1" runat="server" />--%>
                                                    </td>
                                                    <td>
                                                        <webControls:ExCustomValidator ID="cvRadUpload" runat="server" Display="Static" LabelID="5000035" Text="!"
                                                            ClientValidationFunction="ValidateFileSelection" OnServerValidate="cvRadUpload_OnServerValidate"></webControls:ExCustomValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--ValidationGroup="SaveFileToServer"--%>
                                        </td>
                                     
                                        <asp:Panel ID="pnlFileType" runat="server">
                                               <td />
                                            <td>
                                                <asp:DropDownList ID="ddlFileType" runat="server" SkinID="DropDownList150" />
                                            </td>
                                            <td>
                                                <webControls:ExRequiredFieldValidator ID="rfvFileType" runat="server" ControlToValidate="ddlFileType" Text="!" Display="Static"></webControls:ExRequiredFieldValidator>
                                            </td>
                                        </asp:Panel>
                                        <td >
                                            <webControls:ExImageButton ID="ImgBtnRemove" SkinID="DeleteIcon" runat="server" CausesValidation="false" LabelID="2227"
                                                CommandName="Remove" CommandArgument='<%# Bind("RowNumber") %>' />
                                        </td>
                                    </tr>
                                    <tr class="BlankRow">
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr class="BlankRow">
                                    </tr>
                                    <tr>
                                        <td />
                                        <td colspan="3" class="LinkButtonSpecial">
                                            <webControls:ExLinkButton ID="lnkBtnAttachMore" Visible="false" runat="server" LabelID="2790" CausesValidation="false" OnClick="lnkBtnAttachMore_Click" />
                                        </td>
                                        <td colspan="4"></td>
                                    </tr>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr class="BlankRow">
            </tr>
            <%--            <tr>
                <td></td>
                <td valign="top">
                    <webControls:ExLabel ID="lblComments" runat="server" LabelID="1408" SkinID="Black11Arial"
                        FormatString="{0}:"></webControls:ExLabel>
                </td>
                <td colspan="5">
                    <webControls:ExTextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="4"
                        Width="98%" Text='<%# Bind("Comments") %>' MaxLength="250" />
                </td>
            </tr>
            <tr class="BlankRow">
            </tr>--%>
            <tr>
                <td align="right" colspan="7">
                    <webControls:ExButton ID="btnUpload" runat="server" LabelID="1478" OnClick="btnUpload_Click" />
                </td>
            </tr>
            <tr class="BlankRow">
            </tr>
        </asp:Panel>
        <tr>
            <td></td>
            <td colspan="6">
                <telerikWebControls:ExRadGrid ID="rgGLAdjustments" runat="server" ShowHeader="false"
                    OnItemDataBound="rgGLAdjustments_ItemDataBound" AllowInster="false" OnDeleteCommand="rgGLAdjustments_DeleteCommand"
                    AllowExportToExcel="false" AllowExportToPDF="false" AllowPrint="false" AllowPrintAll="false"
                    EntityNameLabelID="1540">
                    <MasterTableView DataKeyNames="AttachmentID" EditMode="InPlace">
                        <Columns>
                            <%--Date--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="Date" LabelID="1399 ">
                                <ItemTemplate>
                                    <%#SkyStem.ART.Web.Utility.Helper.GetDisplayDate ((DateTime)Eval("Date"))%>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <%--Uploaded by--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="UploadedBy" LabelID="1679 ">
                                <ItemTemplate>
                                    <%--<%# Eval("UserID") %>--%>
                                    <%# Eval("UserFullName") %>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <%--Document Name--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="DocumentName" LabelID="1680 ">
                                <ItemTemplate>
                                    <webControls:ExHyperLink ID="hlDocumentName" runat="server" Text='<%#Bind("DocumentName") %>'></webControls:ExHyperLink>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <%--Comments/Description--%>
                            <%--                            <telerikWebControls:ExGridTemplateColumn UniqueName="Description" LabelID="1408">
                                <ItemTemplate>
                                    <%# Eval("Comments") %>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>--%>
                            <%--IsPermanentOrTemporary--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="IsPermanentOrTemporary" LabelID="1810">
                                <ItemTemplate>
                                    <%-- <%# Eval("IsPermanentOrTemporary")%>--%>
                                    <webControls:ExLabel ID="lblIsPermanentOrTemporary" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <%--Delete--%>
                            <telerikWebControls:ExGridButtonColumn ConfirmDialogType="Classic" ConfirmTextLabelID="1781"
                                ConfirmTextFormatString="{0}?" ButtonCssClass="DeleteButton" ButtonType="ImageButton"
                                UniqueName="DeleteColumn" CommandName="Delete">
                            </telerikWebControls:ExGridButtonColumn>
                        </Columns>
                    </MasterTableView>
                </telerikWebControls:ExRadGrid>
            </td>
        </tr>
    </table>

    <script type="text/javascript" language="javascript">

        function ValidateFileExtension(source, arguments) {
            var flag = 0;
            var uploadControl = getRadUpload('RadFileUpload.ClientID');
            if (uploadControl != null) {
                var fileInputs = uploadControl.getFileInputs();
                if (fileInputs[0].value == "") {
                    //source.errormessage = "Source file location cannot be empty.";
                    source.errormessage = source.fileNameErrorMessage;
                    arguments.IsValid = false;
                }
                if (!uploadControl.validateExtensions()) {
                    //source.errormessage = "Invalid file extension.";
                    source.errormessage = source.fileExtensionErrorMessage;
                    arguments.IsValid = false;
                }
            }
        }

        function ValidateFileSelection(src, args) {
            var uploadID = src.getAttribute("RadUploadControlID");
            var uploadControl = getRadUpload(uploadID);
            if (uploadControl != null) {
                var fileInputs = uploadControl.getFileInputs();
                if (fileInputs.length > 0) {
                    var fileInput = fileInputs[0];
                    if (fileInput.value == "") {
                        args.IsValid = false;
                        return false;
                    }
                }
            }
        }
    </script>

</asp:Content>
