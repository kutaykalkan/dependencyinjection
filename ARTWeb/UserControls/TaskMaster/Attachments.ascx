<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_TaskMaster_Attachments" Codebehind="Attachments.ascx.cs" %>
<table width="100%">
    <col width="100%" />
    <tr>
        <td>
            <asp:Panel ID="pnlFileUpload" runat="server">
                <table width="100%">
                    <colgroup>
                        <col width="2%" />
                        <col width="38%" />
                        <col width="60%" />
                    </colgroup>
                    <tr>
                        <td colspan="2"></td>
                        <td>
                            <webControls:ExLabel ID="lblMaxFileSize" runat="server" SkinID="Black11Arial">
                            </webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:UpdatePanel ID="rapFileUpload" runat="server">
                                <ContentTemplate>
                                    <asp:Repeater ID="rptFileUpload" runat="server" OnItemDataBound="rptFileUpload_ItemDataBound"
                                        OnItemCommand="rptFileUpload_ItemCommand">
                                        <HeaderTemplate>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td class="ManadatoryField">*
                                                    </td>
                                                    <td width="54%">
                                                        <webControls:ExLabel ID="lblDocumentName" runat="server" LabelID="1680" FormatString="{0}:"
                                                            SkinID="Black11Arial"></webControls:ExLabel>
                                                    </td>
                                                    <td class="ManadatoryField">*
                                                    </td>
                                                    <td width="37%">
                                                        <webControls:ExLabel ID="lblDocument" runat="server" LabelID="1309" FormatString="{0}:"
                                                            SkinID="Black11Arial"></webControls:ExLabel>
                                                    </td>
                                                    <%--                                                <td class="ManadatoryField">*
                                                </td>
                                                <td width="30%">
                                                    <webControls:ExLabel ID="lblComments" runat="server" LabelID="1408" SkinID="Black11Arial"
                                                        FormatString="{0}:"></webControls:ExLabel>
                                                </td>--%>
                                                    <td width="5%">&nbsp;</td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td />
                                                <td>
                                                    <webControls:ExTextBox ID="txtDocumentName" runat="server" SkinID="ExTextBox400"
                                                        Text='<%# Bind("DocumentName") %>'
                                                        IsRequired="true" MaxLength="50" />
                                                    <asp:HiddenField ID="hdnRowNumber" runat="server" Value='<%# Bind("RowNumber") %>' />
                                                </td>
                                                <td />
                                                <td style="white-space: nowrap;">
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <colgroup>
                                                            <col width="98%" />
                                                            <col width="2%" />
                                                        </colgroup>
                                                        <tr>
                                                            <td>
                                                                <telerikWebControls:ExRadUpload LabelID="2494" ID="ruRadFileUpload" runat="server" Width="310px" Height="21px"
                                                                    ControlObjectsVisibility="None" InitialFileInputsCount="1" InputSize="37" MaxFileInputsCount="1" />
                                                            </td>
                                                            <td>
                                                                <webControls:ExCustomValidator ID="cvRadUpload" runat="server" Display="Dynamic" LabelID="5000035"
                                                                    ClientValidationFunction="ValidateFileSelection" OnServerValidate="cvRadUpload_OnServerValidate">!</webControls:ExCustomValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <%--                                            <td />
                                            <td>
                                                <webControls:ExTextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="4"
                                                    Width="100%" MaxLength="250" Text='<%# Bind("Comments") %>' />
                                            </td>--%>
                                                <td>
                                                    <webControls:ExImageButton ID="ImgBtnRemove" runat="server" SkinID="DeleteIcon" CausesValidation="false" LabelID="2227"
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
                                                <td colspan="3">
                                                    <webControls:ExLinkButton ID="lnkBtnAttachMore" Visible="false" runat="server" LabelID="2790" CausesValidation="false" OnClick="lnkBtnAttachMore_Click" />
                                                </td>
                                                <td colspan="3"></td>
                                            </tr>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                        <td align="right">
                            <webControls:ExButton ID="btnAdd" runat="server" LabelID="2583" OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
            <telerikWebControls:ExRadGrid ID="rgAttachments" Width="100%" runat="server" OnItemDataBound="rgAttachments_ItemDataBound"
                OnNeedDataSource="rgAttachments_NeedDataSource" ClientSettings-Selecting-AllowRowSelect="true"
                AutoGenerateColumns="false" AllowMultiRowSelection="true" AllowSorting="true"
                OnDeleteCommand="rgGLAdjustments_DeleteCommand" AllowExportToExcel="false" AllowExportToPDF="false"
                AllowPrint="false" EntityNameLabelID="1392" AllowPrintAll="false" AllowPaging="true">
                <MasterTableView Width="100%" TableLayout="Auto" DataKeyNames="AttachmentID" EditMode="InPlace">
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
                        <%--                        <telerikWebControls:ExGridTemplateColumn UniqueName="Description" LabelID="1408">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblDescription" runat="server">
                                </webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>--%>

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
        var uploadID = src.getAttribute("RadUploadControlID");
        var uploadControl = getRadUpload(uploadID);
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

