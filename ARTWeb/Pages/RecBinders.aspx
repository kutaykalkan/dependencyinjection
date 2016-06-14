<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true"
    Theme="SkyStemBlueBrown" Inherits="Pages_RecBinders" Codebehind="RecBinders.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnlDataImportStatus" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%">

                <tr class="BlankRow">
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="pnlServiceDataImport" Width="1000px" ScrollBars="Horizontal">
                            <asp:HiddenField ID="hdnNewPageSize" runat="server" />
                            <telerikWebControls:ExRadGrid ID="rgRecBinders" OnItemDataBound="rgRecBinders_ItemDataBound"
                                OnItemCommand="rgRecBinders_ItemCommand" OnItemCreated="rgRecBinders_ItemCreated" OnSortCommand="rgRecBinders_SortCommand"
                                AllowExportToExcel="true" AllowExportToPDF="true" AllowRefresh="true" AllowSorting="true"
                                AllowPaging="true" AllowMultiRowSelection="false" AllowAddNewRow="false" AllowPrint="true"
                                AllowCustomPaging="true" OnPageSizeChanged="rgRecBinders_PageSizeChanged" OnPageIndexChanged="rgRecBinders_PageIndexChanged"
                                OnNeedDataSource="rgRecBinders_NeedDataSource" EntityNameLabelID="2826" AutoGenerateColumns="false" runat="server">
                                <MasterTableView ClientDataKeyNames="RequestID">
                                    <PagerTemplate>
                                        <asp:Panel ID="PagerPanel" runat="server">
                                            <asp:Panel runat="server" ID="pnlPageSizeDDL">
                                                <div style="float: left; margin-right: 10px;">
                                                    <webControls:ExLabel ID="lblPageSize" runat="server" LabelID="2493"></webControls:ExLabel>
                                                    <asp:DropDownList ID="ddlPageSize" SkinID="DropDownList50" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="NumericPagerPlaceHolder" />
                                        </asp:Panel>
                                    </PagerTemplate>
                                    <Columns>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2027" SortExpression="FileName">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblFileName" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2087" UniqueName="FileDownloadIconColumn" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <webControls:ExImageButton ID="imgFileTypeZip" runat="server" ImageAlign="Left" SkinID="FileDownloadIconZip" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1399" HeaderStyle-Width="20%" SortExpression="DateAdded">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblDate" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2180" HeaderStyle-Width="15%" SortExpression="AddedByUserName">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblAddedBy" runat="server" Width="100Px" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridButtonColumn UniqueName="DeleteColumn" CommandName="Delete"
                                            ConfirmDialogType="Classic" ConfirmTextLabelID="1932" ButtonType="ImageButton" ConfirmTextFormatString="{0}?"
                                            ButtonCssClass="DeleteButton" HeaderStyle-Width="5%">
                                        </telerikWebControls:ExGridButtonColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerikWebControls:ExRadGrid>
                        </asp:Panel>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <UserControls:ProgressBar ID="ucDataImportStatus" runat="server" AssociatedUpdatePanelID="upnlDataImportStatus" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="Sel" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>


    
</asp:Content>

