<%@ Page Language="C#" MasterPageFile="~/MasterPages/MatchingMaster.master" AutoEventWireup="true" Theme="SkyStemBlueBrown" Inherits="Pages_Matching_MatchSourceDataImport"
    Title="Untitled Page" Codebehind="MatchSourceDataImport.aspx.cs" %>

<%@ Register Src="~/UserControls/LegendOnAccountSearch.ascx" TagName="LegendOnAccountSearch"
    TagPrefix="UserControl" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMatching" runat="Server">
    <asp:UpdatePanel ID="upnlMatchSourceDataImport" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <asp:Panel ID="pnlMatching" runat="server">
                    <tr>
                        <td>
                            <telerikWebControls:ExRadGrid ID="radMatchSourceDataImport" runat="server" AllowInster="false"
                                AllowExportToExcel="false" AllowExportToPDF="false" AllowPrint="false" AllowPrintAll="false"
                                GridLines="None" EnableAjaxSkinRendering="false" Width="100%" EnableViewState="true"
                                OnItemDataBound="radMatchSourceDataImport_GridItemDataBound">
                                <MasterTableView>
                                    <Columns>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2190" HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Font-Bold="true" HeaderStyle-Width="100px" DataType="System.String">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlMatchingSourceType" runat="server" SkinID="DropDownList200"
                                                    AutoPostBack="false">
                                                    <asp:ListItem Text="Select One" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1287" HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Font-Bold="true" HeaderStyle-Width="100px" DataType="System.String">
                                            <ItemTemplate>
                                                <%--    <webControls:ExTextBox ID="txtName" runat="server" SkinID="ExTextBox200" IsRequired="false"
                                            EnableViewState="true" MaxLength="50"  />--%>
                                                <asp:TextBox ID="txtName" runat="server" MaxLength="50" EnableViewState="true"></asp:TextBox>
                                                <webControls:ExRegularExpressionValidator ID="revName" runat="server" ControlToValidate="txtName"
                                                    Text="!" ValidationExpression="^[a-zA-Z0-9-_\s]{1,50}$" LabelID="5000304"></webControls:ExRegularExpressionValidator>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2087" HeaderStyle-HorizontalAlign="Left"
                                            HeaderStyle-Font-Bold="true" HeaderStyle-Width="100px" DataType="System.String">
                                            <ItemTemplate>
                                                <telerikWebControls:ExRadUpload LabelID="2494" ID="RadFileUpload" runat="server" ControlObjectsVisibility="none" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerikWebControls:ExRadGrid>
                        </td>
                    </tr>
                </asp:Panel>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1" ClientEvents-OnRequestStart="conditionalPostback">
                            <webControls:ExButton ID="btnUploadNewFiles" runat="server" LabelID="1478" OnClick="btnUploadNewFiles_Click"
                                CausesValidation="true" />
                            <webControls:ExButton ID="btnBack" LabelID="1239" CausesValidation="false" runat="server"
                                OnClick="btnBack_Click" />
                        </telerik:RadAjaxPanel>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<UserControl:LegendOnAccountSearch ID="LegendOnAccountSearch" runat="server" />--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <UserControls:ProgressBar ID="ucAccountProfileMassAndBulkUpdate" runat="server" EnableTheming="true"
                            AssociatedUpdatePanelID="upnlMatchSourceDataImport" Visible="true" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function conditionalPostback(sender, args) {
            if (args.EventTarget == "<%= btnUploadNewFiles.UniqueID%>") {
                args.EnableAjax = false;
            }
        }
    </script>

</asp:Content>
