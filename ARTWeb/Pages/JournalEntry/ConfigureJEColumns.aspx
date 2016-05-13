<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigureJEColumns.aspx.cs"
    Inherits="Pages_ConfigureJEColumns" MasterPageFile="~/MasterPages/ARTMasterPage.master"
    Theme="SkyStemBlueBrown" %>

<%@ Register TagPrefix="UserControl" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Import Namespace="SkyStem.Language.LanguageUtility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <asp:Panel ID="GridPnl" runat="server">
                    <tr>
                        <td>
                            <telerikWebControls:ExRadGrid ID="rgJEColumns" runat="server" EntityNameLabelID="2101"
                                OnNeedDataSource="rgJEColumns_NeedDataSource" OnItemCommand="rgJEColumns_OnItemCommand"
                                OnItemDataBound="rgJEColumns_ItemDataBound" AllowAddNewRow="true">
                                <MasterTableView>
                                    <Columns>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2081" UniqueName="col3">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblSNo" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                                                <webControls:ExCustomValidator runat="server" ClientValidationFunction="JEColumnValidate"
                                                    ID="cvColumnName">!</webControls:ExCustomValidator>
                                                <webControls:ExCustomValidator runat="server" ValidationGroup="SAVE" ClientValidationFunction="JEColumnValidateSave"
                                                    ID="ExCustomValidator1">!</webControls:ExCustomValidator>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2104" UniqueName="col1">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtColumnName" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2105" UniqueName="col2">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLenth" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerikWebControls:ExRadGrid>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td align="right" style="padding-right: 50px">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-right: 30px">
                        <webControls:ExButton ID="btnSave" ValidationGroup="SAVE" runat="server" LabelID="1315"
                            SkinID="ExButton100" OnClick="btnSave_OnClick" />&nbsp;
                        <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" CausesValidation="false"
                            SkinID="ExButton100" OnClick="btnCancel_OnClick" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hdnNoOFfBlankRow" runat="server" Value="0" />
                        <UserControl:ProgressBar ID="ucProgressBar" runat="server" AssociatedUpdatePanelID="upnlMain" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">




        function JEColumnValidate(sender, args) {


            var valSummaryObj = GetValidationSummaryElement();
            valSummaryObj.validationGroup = '';
            var index = sender.parentNode.parentNode.sectionRowIndex;
            // Get the corresponding index
            var grid = $find("<%=rgJEColumns.ClientID %>");

            var hdnNoOFfBlankRow = document.getElementById("<%=hdnNoOFfBlankRow.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var textColumnName = MasterTable.get_dataItems()[index].findElement("txtColumnName"); //accessing standard asp.net server controls
            var txtLenth = MasterTable.get_dataItems()[index].findElement("txtLenth");
            var txtColumnNameValue = textColumnName.value;
            var txtLenthValue = txtLenth.value;

            if (txtColumnNameValue == "" && txtLenthValue == "") {
                hdnNoOFfBlankRow.value = parseInt(hdnNoOFfBlankRow.value) + 1;
                if (hdnNoOFfBlankRow.value > 0) {
                    args.IsValid = false;
                    sender.errormessage = '<% =LanguageUtil.GetValue(5000229) %>';
                    return;
                }

            }
        }


        function JEColumnValidateSave(sender, args) {

            var valSummaryObj = GetValidationSummaryElement();
            valSummaryObj.validationGroup = 'SAVE';
            ValidateSave(sender, args);

        }

        function ValidateSave(sender, args) {



            var index = sender.parentNode.parentNode.sectionRowIndex;
            // Get the corresponding index
            var grid = $find("<%=rgJEColumns.ClientID %>");

            var hdnNoOFfBlankRow = document.getElementById("<%=hdnNoOFfBlankRow.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var textColumnName = MasterTable.get_dataItems()[index].findElement("txtColumnName"); //accessing standard asp.net server controls
            var txtLenth = MasterTable.get_dataItems()[index].findElement("txtLenth");
            var txtColumnNameValue = textColumnName.value;
            var txtLenthValue = txtLenth.value;



            if (txtColumnNameValue != "") {
                if (txtLenthValue == null || txtLenthValue == "") {
                    args.IsValid = false;

                    sender.errormessage = '<% =LanguageUtil.GetValue(5000232) %>';
                    return;

                }
            }
            if (txtLenthValue != "") {
                if (txtColumnNameValue == null || txtColumnNameValue == "") {
                    args.IsValid = false;
                    sender.errormessage = '<% =LanguageUtil.GetValue(5000231) %>';
                    return;

                }
            }
            if (txtLenthValue != "") {
                if (IsPositiveInteger(txtLenth)) {
                    args.IsValid = true;
                }
                else {
                    args.IsValid = false;
                    sender.errormessage = '<% =LanguageUtil.GetValue(5000230) %>';

                }
            }


        }
        
    </script>

</asp:Content>
