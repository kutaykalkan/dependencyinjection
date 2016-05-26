<%@ Page Language="C#" MasterPageFile="~/MasterPages/RecProcessMasterPage.master"
    AutoEventWireup="true" Inherits="Pages_UnexplainedVariance"
    Title="Untitled Page" Theme="SkyStemBlueBrown" Codebehind="UnexplainedVariance.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="AccountHierarchyDetail" Src="~/UserControls/AccountHierarchyDetail.ascx" %>
<%@ Register TagPrefix="userControl" TagName="DocumentUpload" Src="~/UserControls/DocumentUploadButton.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<asp:content id="Content1" contentplaceholderid="cphRecProcess" runat="Server">
    <%--    <div id="divMainContent">--%>
    
     
    <asp:updatepanel id="updpnlMain" runat="server">
    
 
        <contenttemplate>
                <table id="tblMainContent" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">
                                <telerikWebControls:ExRadGrid ID="rgUnExpectedVariance" runat="server" ShowHeader="true"
                                    OnNeedDataSource="rgUnExpectedVariance_NeedDataSource"
                                    OnItemCommand="rgUnExpectedVariance_ItemCommand" 
                                    OnItemCreated="rgUnExpectedVariance_ItemCreated"
                                    
                                    OnItemDataBound="rgUnExpectedVariance_ItemDataBound"
                                    OnDeleteCommand="rgUnExpectedVariance_DeleteCommand"
                                    AllowExportToExcel="true" AllowExportToPDF="true" AllowPrint="true" AllowPrintAll="true">
                                     <%-- OnInsertCommand="rgUnExpectedVariance_InsertCommand"  OnUpdateCommand="rgUnExpectedVariance_UpdateCommand"--%>
                                    <MasterTableView DataKeyNames="GLDataUnexplainedVarianceID">
                                        <Columns>
                                            <%--Added By--%>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="AddedBy" LabelID="1508" HeaderStyle-Width="15%">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblAddedBy" runat="server"></webControls:ExLabel>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                           <%--DateAdded--%>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="DateAdded" LabelID="1399  " SortExpression="DateAdded" DataType="System.DateTime"
                                             HeaderStyle-Width="15%">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblDateAdded" runat="server"></webControls:ExLabel>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                             <%--Amount In Base Currency--%>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="AmountInBaseCurrency" LabelID="1673 "
                                                FormatString="{0}" SortExpression="AmountBaseCurrency" DataType="System.Decimal" HeaderStyle-Width="15%">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblAmountBaseCurrency" runat="server" Text='<%#Helper.GetDisplayDecimalValue((Decimal?)Eval("AmountBaseCurrency"))%>'></webControls:ExLabel>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <%--Blank Space--%>
                                            <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="50">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblSpacer" Width="50" runat="server" ></webControls:ExLabel>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <%--Comments--%>
                                            <telerikWebControls:ExGridTemplateColumn ItemStyle-Width="30%" UniqueName="Comments" LabelID="1408" HeaderStyle-Width="50%">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblComments"  runat="server" Text='<%# Eval("Comments")%>'></webControls:ExLabel>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <%--Edit Button--%>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="ShowInputForm">
                                                <ItemTemplate>
                                                    <webControls:ExHyperLink ID="hlShowItemInputForm" runat="server" SkinID="ShowItemInputPopup"
                                                        /><%--CommandName="ShowInputForm" CommandArgument='<%# Eval("GLReconciliationItemInputID") %>'--%>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <%--Delete Button Column--%>
                                            <telerikWebControls:ExGridButtonColumn ConfirmDialogType="Classic" ConfirmTextLabelID="1781" ConfirmTextFormatString="{0}?" ButtonCssClass="DeleteButton"  ButtonType="ImageButton" UniqueName="DeleteColumn" CommandName="Delete">
                                            </telerikWebControls:ExGridButtonColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerikWebControls:ExRadGrid>
                            </asp:Panel>
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="right">
                           <webControls:ExButton ID="btnAdd" runat="server" LabelID="1560" SkinID="ExButton100"/>
                                
                            <webControls:ExButton ID="btnCancel" runat="server" LabelID="1545" SkinID="ExButton100" />
                         </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="updateProgerssDiv">
                                <UserControls:ProgressBar ID="ucProgressBar" runat="server" EnableTheming="true"
                                     AssociatedUpdatePanelID="updpnlMain" />
                            </div>
                        </td>
                    </tr>
                </table>
            </contenttemplate>
    </asp:updatepanel>
    <%--</div>--%>
    <%--Input Form--%>
    <telerik:RadWindow ID="rwRecItemInput" VisibleOnPageLoad="false" runat="server" OpenerElementID="<%btnAdd.ClientID %>"
        Modal="true" Width="850px" Height="400px" Top="50px">
    </telerik:RadWindow>
    <telerik:RadWindow ID="rwBulkClose" VisibleOnPageLoad="false" runat="server" OpenerElementID="<%btnClose.ClientID %>"
        Modal="true" Width="850px" Height="400px" Top="50px">
    </telerik:RadWindow>

    <script language="javascript" type="text/javascript">

        function ShowRecItemInput(queryString) {

            var oWnd = $find('<%=rwRecItemInput.ClientID%>');
            oWnd.setUrl('EditItemUnexplainedVariance.aspx?' + queryString);
            oWnd.show();
            return false;
        }
        
    </script>

</asp:content>