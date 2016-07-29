<%@ Page Language="C#" MasterPageFile="~/MasterPages/MatchingMaster.master" AutoEventWireup="true" Inherits="Pages_Matching_MatchSourceDataTypeMapping"
    Theme="SkyStemBlueBrown" Codebehind="MatchSourceDataTypeMapping.aspx.cs" %>

<%@ Register TagPrefix="UserControl" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Import Namespace="SkyStem.Language.LanguageUtility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMatching" runat="Server">
    <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <asp:Panel ID="GridPnl" runat="server">
                    <tr class="BlankRow">
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr id="trDataSourceName" runat="server">
                        <td colspan="3" valign="middle">
                            &nbsp;
                            <webControls:ExLabel ID="lblDataSourceName" runat="server" LabelID="2308" FormatString="{0} :"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            &nbsp;
                            <asp:DropDownList ID="ddlDataSourceName" runat="server" SkinID="DropDownList200"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlDataSourceName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <telerikWebControls:ExRadGrid ID="rgMappingColumns" runat="server" EntityNameLabelID="2101"
                                OnItemDataBound="rgMappingColumns_ItemDataBound">
                                <MasterTableView>
                                    <Columns>
                                        <telerikWebControls:ExGridTemplateColumn Visible="false" HeaderStyle-Width="10px">
                                            <ItemTemplate>
                                                <%-- <webControls:ExLabel ID="lblGridColumnNumber" SkinID="Black11Arial" runat="server"></webControls:ExLabel>--%>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2104" HeaderStyle-Width="250px"
                                            UniqueName="ColumnName">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblColumnName" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2193" UniqueName="DataType">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlDataType" runat="server" SkinID="DropDownList200">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="ID" Visible="false" />
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="GridColumnID" Visible="false" />
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="MatchingSourceDataImportID"
                                            Visible="false" />
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                                </ClientSettings>
                            </telerikWebControls:ExRadGrid>
                        </td>
                    </tr>
                </asp:Panel>
                <tr class="BlankRow">
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <webControls:ExButton ID="btnContinueLater" runat="server" LabelID="2206" SkinID="ExButton150"
                            OnClick="btnContinueLater_OnClick" />
                        <webControls:ExButton ID="btnSubmit" runat="server" LabelID="1238" SkinID="ExButton100"
                            OnClick="btnSubmit_OnClick" />&nbsp;
                        <webControls:ExButton ID="btnSubmitAll" runat="server" LabelID="2211" SkinID="ExButton100"
                            OnClick="btnSubmitAll_OnClick" />&nbsp;
                        <webControls:ExButton ID="btnStatus" OnClick="btnStatus_Click" runat="server" LabelID="2271" />&nbsp;
                        <webControls:ExButton ID="btnBack" LabelID="1545" CausesValidation="false" runat="server"
                        OnClick="btnBack_Click" />&nbsp;
                         <webControls:ExButton ID="btnCancel" LabelID="1239" CausesValidation="false" runat="server" 
                         OnClick="btnCancel_Click" />&nbsp;
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






        function JEColumnValidateSave(sender, args) {

            var valSummaryObj = GetValidationSummaryElement();
            valSummaryObj.validationGroup = 'SAVE';
            ValidateSave(sender, args);

        }

        function ValidateSave(sender, args) {
            var index = sender.parentNode.parentNode.sectionRowIndex;
            // Get the corresponding index
            var grid = $find("<%=rgMappingColumns.ClientID %>");
            var MasterTable = grid.get_masterTableView();
            var ddlDataType = MasterTable.get_dataItems()[index].findElement("ddlDataType"); //accessing standard asp.net server controls
            var ddlDataTypeValue;

            if (ddlDataType != null) {
                ddlDataTypeValue = ddlDataType.options[ddlDataType.selectedIndex].text;
            }


            if (ddlDataTypeValue != "") {
                if (ddlDataTypeValue == null || ddlDataTypeValue == "Select One") {
                    args.IsValid = false;

                    sender.errormessage = '<% =LanguageUtil.GetValue(5000254) %>';
                    return;

                }
            }



        }
        
    </script>

</asp:Content>
