<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_Dashboard_ReconciliationTracking" Codebehind="ReconciliationTracking.ascx.cs" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<table style="width: 100%" cellpadding="0" cellspacing="0" id="tblMessage" runat="server"
    visible="false">
    <tr>
        <td colspan="2">
            <webControls:ExLabel ID="lblMessage" runat="server" SkinID="ErrorLabel"></webControls:ExLabel>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="upnlAccountOwnershipStatistics" runat="server">
    <ContentTemplate>
        <table style="width: 100%" cellpadding="0" cellspacing="0" id="tblContent" runat="server">
            <tr class="BlankRow">
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 2px; vertical-align: bottom; width: 30%;">
                    <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="1663" FormatString="{0} :  "
                        SkinID="Black11Arial"></webControls:ExLabel>
                </td>
                <td>
                    <webControls:ExLabel ID="lblTotalAccounts" runat="server" SkinID="Black11ArialValignMiddle"></webControls:ExLabel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center; vertical-align: top">
                    <asp:Chart ID="chrtReconciliationTracking" BorderlineColor="Blue" runat="server"
                        Width="450px" Height="240px" EnableViewState="true">
                    </asp:Chart>
                </td>
            </tr>
            <tr class="BlankRow">
                <td>
                </td>
            </tr>
            
            
            <tr>
                <td colspan="2">
                   
                        <telerikWebControls:ExRadGrid ID="rgReconciliationTrackingStatus" runat="server" EntityNameLabelID="2294"
                        AllowSorting="True" AllowExportToPDF="true" AllowExportToExcel="True" OnItemDataBound="rgReconciliationTrackingStatus_ItemDataBound"
                        OnItemCreated="rgReconciliationTrackingStatus_ItemCreated" OnItemCommand="rgReconciliationTrackingStatus_OnItemCommand"
                        OnSortCommand="rgReconciliationTrackingStatus_SortCommand" Width="100%">
                            <MasterTableView DataKeyNames="ReconciliationStatusID">
                                <Columns>
                                
                                    <telerikWebControls:ExGridBoundColumn DataField="ReconciliationStatusID" UniqueName="ReconciliationStatusID" Visible="false">
                                    </telerikWebControls:ExGridBoundColumn>
                                
                                
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="RecStatusLinkButtonColumn" LabelID="1370" SortExpression="ReconciliationStatus"
                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="40%">
                                        <ItemTemplate>
                                            <webControls:ExLinkButton ID="lnkRecStatus" runat="server" OnCommand="SendToAccountViewer" SkinID="GridLinkButton" ></webControls:ExLinkButton>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    
                                     <telerikWebControls:ExGridTemplateColumn UniqueName="RecStatusLabelColumn" LabelID="1370" SortExpression="ReconciliationStatus"
                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5%" Visible="false">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblRecStatus" runat="server"  ></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    
                                    
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1510" UniqueName="AmountLinkButtonColumn" SortExpression="Amount"
                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="40%">
                                        <ItemTemplate>
                                            <webControls:ExLinkButton ID="lnkAmount" runat="server" OnCommand="SendToAccountViewer" SkinID="GridLinkButton" ></webControls:ExLinkButton>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    
                                    
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1510" UniqueName="AmountLabelColumn" SortExpression="Amount"
                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="5%" Visible="false">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblAmount" runat="server"  ></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    
                                    
                                    

                                </Columns>
                            </MasterTableView>
                        </telerikWebControls:ExRadGrid>
                    
                </td>
            </tr>
            
            
            <tr class="BlankRow">
                <td>
                </td>
            </tr>
            
            
           
            <tr>
                <td align="left" colspan="2">
                    <table>
                        <tr>
                            <td>
                               <webControls:ExLabel ID="lblNote" FormatString ="{0}:" runat="server" LabelID="2005" SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblMsg" runat="server" LabelID="1899" SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                             
                        </tr>
                         <tr>
                            <td>
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblMsg2" runat="server" LabelID="2004" SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                             
                        </tr>
                    </table>
                </td>
            </tr>
            
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<UserControls:ProgressBar ID="upHome" runat="server" EnableTheming="true" AssociatedUpdatePanelID="upnlAccountOwnershipStatistics"
    Visible="true" />
