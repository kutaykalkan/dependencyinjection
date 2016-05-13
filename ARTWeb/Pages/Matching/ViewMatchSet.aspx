<%@ Page Language="C#" MasterPageFile="~/MasterPages/MatchingMaster.master" AutoEventWireup="true"
    CodeFile="ViewMatchSet.aspx.cs" Theme="SkyStemBlueBrown" Inherits="Pages_Matching_ViewMatchSet" %>

<%@ Register Src="~/UserControls/LegendOnAccountSearch.ascx" TagName="LegendOnAccountSearch"
    TagPrefix="UserControl" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMatching" runat="Server">
    <asp:UpdatePanel ID="upnlMatching" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <telerikWebControls:ExRadGrid ID="radMatching" runat="server" OnItemDataBound="radMatching_ItemDataBound"
                            OnNeedDataSource="radMatching_NeedDataSource" OnSortCommand="radMatching_SortCommand"
                            AllowExportToExcel="true" AllowExportToPDF="true" AllowPrint="true" AllowPrintAll="true"
                            AllowRefresh="True" OnItemCommand="radMatching_ItemCommand" OnItemCreated="radMatching_ItemCreated"
                            AllowPaging="true" AllowSorting="true" AllowMultiRowSelection="true">
                            <MasterTableView>
                                <Columns>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2237" Visible="false" SortExpression="MatchSetID"
                                        DataType="System.String">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblMatchSetID" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="5%" UniqueName="imgStatus">
                                        <ItemTemplate>
                                            <webControls:ExImage ID="imgSuccess" runat="server" LabelID="2371" SkinID="SuccessIcon"
                                                Visible="false" />
                                            <webControls:ExHyperLink ID="hlFailure" Visible="false" ToolTipLabelID="2372" runat="server"
                                                SkinID="MatchSetError"></webControls:ExHyperLink>
                                            <%--  <webControls:ExImage ID="imgFailure" runat="server" LabelID="2372" SkinID="ExpireIcon"
                                                Visible="false" />--%>
                                            <webControls:ExImage ID="imgWarning" runat="server" LabelID="1617" SkinID="WarningIcon"
                                                Visible="false" />
                                            <webControls:ExImage ID="imgProcessing" runat="server" LabelID="2373" SkinID="ProgressIcon"
                                                Height="24px" Width="23px" Visible="false" />
                                            <webControls:ExImage ID="imgToBeProcessed" runat="server" LabelID="2374" SkinID="ToBeProcessedIcon"
                                                Visible="false" />
                                            <webControls:ExImage ID="imgDraft" runat="server" LabelID="2370" SkinID="Draft" Visible="false" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2276" SortExpression="MatchSetRef"
                                        DataType="System.String" HeaderStyle-Width="5%" UniqueName="MatchSetRef">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlMatchSetRef" runat="server"></webControls:ExHyperLink>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2186" SortExpression="MatchSetName"
                                        UniqueName="MatchSetName" DataType="System.String">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlMatchSetName" runat="server"></webControls:ExHyperLink>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2187" SortExpression="MatchingType"
                                        DataType="System.String" HeaderStyle-Width="10%" UniqueName="MatchingType">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlMatchingType" runat="server"></webControls:ExHyperLink>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2184" SortExpression="AccountName"
                                        UniqueName="AccountName" DataType="System.String" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlAccount" runat="server"></webControls:ExHyperLink>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1370" SortExpression="ReconciliationStatus"
                                        DataType="System.String" UniqueName="ReconciliationStatus">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlReconciliationStatus" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1509" SortExpression="DateAdded"
                                        UniqueName="DateAdded" DataType="System.String" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlDateAdded" runat="server"></webControls:ExHyperLink>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2180" SortExpression="AddedBy"
                                        UniqueName="AddedBy" DataType="System.String" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlAddedBy" runat="server"></webControls:ExHyperLink>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <%--<telerikWebControls:ExGridTemplateColumn Visible="false">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlAddedByUserID" runat="server"></webControls:ExHyperLink>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1338" SortExpression="MatchingStatus" DataType="System.String" >
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlStatus" runat="server"></webControls:ExHyperLink>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>--%>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2351" DataType="System.String"
                                        HeaderStyle-Width="10%" UniqueName="ProcessedOn">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlProcessedOn" runat="server"></webControls:ExHyperLink>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="5%" DataType="System.String"
                                        ItemStyle-HorizontalAlign="Center" UniqueName="MatchingResult">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink CommandName="MatchingResult" CommandArgument="MatchSetID"
                                                ToolTipLabelID="2341" ID="hlMatchingResult" runat="server" SkinID="MatchSetResult" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="EditMatchSet">
                                        <ItemTemplate>
                                            <webControls:ExImageButton CommandName="EditMatchSet" CommandArgument="MatchSetID"
                                                ID="btnEditMatchSet" runat="server" SkinID="EditIcon" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="5%" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="5%" DataType="System.String"
                                        ItemStyle-HorizontalAlign="Center" UniqueName="Delete">
                                        <ItemTemplate>
                                            <webControls:ExImageButton CommandName="DeleteMatchSet"
                                                ID="btnDelete" runat="server" SkinID="DeleteIcon" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <%--<telerikWebControls:ExGridButtonColumn UniqueName="DeleteColumn" CommandName="Delete"

                                                                ConfirmDialogType="Classic" ConfirmTextLabelID="1781" ConfirmTextFormatString="{0}?"
                                                                ButtonCssClass="DeleteButton" HeaderStyle-Width="5%">
                                                            </telerikWebControls:ExGridButtonColumn>--%>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings>
                                <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                            </ClientSettings>
                        </telerikWebControls:ExRadGrid>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <webControls:ExButton ID="btnUploadStatus" OnClick="btnUploadStatus_Click" runat="server"
                            LabelID="2271" />&nbsp;
                        <webControls:ExButton ID="btnUploadNewFiles" OnClick="btnUploadNewFiles_Click" runat="server"
                            LabelID="2189" />&nbsp;
                        <webControls:ExButton ID="btnCreateNew" OnClick="btnCreateNew_Click" runat="server"
                            LabelID="1622" />&nbsp;
                        <webControls:ExButton ID="btnBack" LabelID="1545" CausesValidation="false" runat="server"
                            OnClick="btnBack_Click" />
                        <webControls:ExButton ID="btnBackToRecForm" LabelID="2388" CausesValidation="false"
                            runat="server" OnClick="btnBackToRecForm_Click" />
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="LegendTable">
                            <tr>
                                <td class="LegendHeading" colspan="5">
                                    <webControls:ExLabel ID="lblHeading" FormatString="{0}:" runat="server" LabelID="1383"></webControls:ExLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" valign="top">
                                    <webControls:ExImage ID="imgDraft" runat="server" LabelID="2421" SkinID="Draft" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel6" runat="server" LabelID="2421" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>
                                <td style="width: 20%" valign="top">
                                    <webControls:ExImage ID="imgSuccess" runat="server" LabelID="2371" SkinID="SuccessIcon" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="2371" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>
                                <td style="width: 20%" valign="top">
                                    <webControls:ExImage ID="imgFailure" runat="server" LabelID="2430" SkinID="ExpireIcon" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel2" runat="server" LabelID="2430" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>
                                <%-- <td style="width: 17%" valign="top">
                                    <webControls:ExImage ID="imgWarning" runat="server" LabelID="1617" SkinID="WarningIcon" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel3" runat="server" LabelID="1617" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>--%>
                                <td style="width: 20%" valign="top">
                                    <webControls:ExImage ID="imgProcessing" runat="server" LabelID="2431" SkinID="ProgressIcon"
                                        Height="24px" Width="23px" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel4" runat="server" LabelID="2431" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>
                                <td style="width: 20%" valign="top">
                                    <webControls:ExImage ID="ExImage1" runat="server" LabelID="2374" SkinID="ToBeProcessedIcon" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel5" runat="server" LabelID="2374" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <UserControls:ProgressBar ID="ucAccountProfileMassAndBulkUpdate" runat="server" EnableTheming="true"
                            AssociatedUpdatePanelID="upnlMatching" Visible="true" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">
        function ConfirmDeletion(msg) {
            var answer = confirm(msg);
            if (answer) {
                return true;
            }
            else {
                return false;
            }
        }
            
    </script>

</asp:Content>
