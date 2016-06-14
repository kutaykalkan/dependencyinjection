<%@ Page Language="C#" MasterPageFile="~/MasterPages/CertificationMasterPage.master"
    AutoEventWireup="true" Inherits="Pages_CertificationHome"
    Title="Untitled Page" Theme="SkyStemBlueBrown" Codebehind="CertificationHome.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCertification" runat="Server">
    <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Panel ID="pnlMyStatus" runat="server">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr class="BlankRow">
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <webControls:ExLabel ID="lblStatusHeading" SkinID="StatusHeading" runat="server"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr class="BlankRow">
                                </tr>
                                <tr>
                                    <td>
                                        <table class="TableSameAsGrid" width="100%" cellpadding="0" cellspacing="0">
                                            <col width="40%" />
                                            <col width="30%" />
                                            <col width="30%" />
                                            <tr class="TableHeaderSameAsGrid">
                                                <td>
                                                    <webControls:ExLabel ID="lblStep" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                                </td>
                                                <td>
                                                    <div style="text-align: center">
                                                        <webControls:ExLabel ID="lblStatus" LabelID="1456" runat="server" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                                                    </div>
                                                </td>
                                                <td>
                                                    <!-- Apoorv - Added Div to do center alignment //-->
                                                    <div style="text-align: center">
                                                        <webControls:ExLabel ID="lblDate" LabelID="1399" runat="server" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr id="trMandatoryReport" runat="server" class="TableRowSameAsGrid">
                                                <td>
                                                    <webControls:ExHyperLink ID="hlMandatoryReportSignoff" runat="server" LabelID="1016"
                                                        SkinID="" />
                                                </td>
                                                <td align="center">
                                                    <webControls:ExLabel ID="lblMandatoryReportSignoffStatus" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                                </td>
                                                <td align="center">
                                                    <webControls:ExLabel ID="lblMandatoryReportSignoffDate" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                                </td>
                                            </tr>
                                            <tr id="trCertificationBalances" runat="server" class="TableAlternateRowSameAsGrid">
                                                <td>
                                                    <webControls:ExHyperLink ID="hlCertificationBalances" runat="server" LabelID="1462"
                                                        SkinID="" />
                                                </td>
                                                <td align="center">
                                                    <webControls:ExLabel ID="lblCertificationBalancesStatus" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                                </td>
                                                <td align="center">
                                                    <webControls:ExLabel ID="lblCertificationBalancesDate" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                                </td>
                                            </tr>
                                            <tr id="trExceptionCertification" runat="server" class="TableRowSameAsGrid">
                                                <td>
                                                    <webControls:ExHyperLink ID="hlExceptionCertification" runat="server" LabelID="1211"
                                                        SkinID="" />
                                                </td>
                                                <td align="center">
                                                    <webControls:ExLabel ID="lblExceptionCertificationStatus" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                                </td>
                                                <td align="center">
                                                    <webControls:ExLabel ID="lblExceptionCertificationDate" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                                </td>
                                            </tr>
                                            <tr id="trCertification" runat="server" class="TableAlternateRowSameAsGrid">
                                                <td>
                                                    <webControls:ExHyperLink ID="hlCertification" runat="server" LabelID="1072" SkinID="" />
                                                </td>
                                                <td align="center">
                                                    <webControls:ExLabel ID="lblCertificationStatus" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                                </td>
                                                <td align="center">
                                                    <webControls:ExLabel ID="lblCertificationDate" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                        </asp:Panel>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlJuniorStatus" runat="server">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <webControls:ExLabel ID="lblStatusLinkHeading" runat="server" SkinID="SubSectionHeading"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr class="BlankRow">
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerikWebControls:ExRadGrid ID="rgStatus" runat="server" EntityNameLabelID="1046"
                                            AllowPaging="false" AllowSorting="false" OnItemDataBound="rgStatus_ItemDataBound"
                                            OnDetailTableDataBind="rgStatus_DetailTableDataBind" AllowExportToExcel="True"
                                            OnItemCreated="rgStatus_ItemCreated" OnItemCommand="rgStatus_OnItemCommand" AllowExportToPDF="False"
                                            AllowPrint="true" AllowPrintAll="true">
                                            <MasterTableView Name="lavel1" ExpandCollapseColumn-Display="true" DataKeyNames="UserID">
                                                <DetailTables>
                                                    <telerik:GridTableView runat="server" Name="lavel2" CssClass="GridTableView" DataKeyNames="UserID">
                                                        <DetailTables>
                                                            <telerik:GridTableView runat="server" Name="lavel3" CssClass="GridTableView" DataKeyNames="UserID">
                                                                <Columns>
                                                                    <telerikWebControls:ExGridTemplateColumn UniqueName="UserName" ItemStyle-Width="20%"
                                                                        HeaderStyle-Width="20%">
                                                                        <ItemTemplate>
                                                                            <webControls:ExLabel ID="lblUserName" runat="server" SkinID="" />
                                                                        </ItemTemplate>
                                                                    </telerikWebControls:ExGridTemplateColumn>
                                                                    <telerikWebControls:ExGridTemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="3%"
                                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3%">
                                                                        <ItemTemplate>
                                                                            <webControls:ExImage ID="imgNoAccess" runat="server" SkinID="UserUnderYouNoAccess" />
                                                                            <webControls:ExImage ID="imgSharedAccess" runat="server" SkinID="UserUnderYouSharedAccess" />
                                                                        </ItemTemplate>
                                                                    </telerikWebControls:ExGridTemplateColumn>
                                                                    <%-- LabelID="1130 "--%>
                                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1600 " SortExpression="NoOfMissingPreparer"
                                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <webControls:ExHyperLink ID="hlCertificationBalancesStatus" runat="server" SkinID="" />
                                                                        </ItemTemplate>
                                                                    </telerikWebControls:ExGridTemplateColumn>
                                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="DateCertificationBalances"
                                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <webControls:ExHyperLink ID="hlDateCertificationBalances" runat="server" SkinID="" />
                                                                        </ItemTemplate>
                                                                    </telerikWebControls:ExGridTemplateColumn>
                                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1601 " SortExpression="NoOfMissingPreparer"
                                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <webControls:ExHyperLink ID="hlCertificationExceptionStatus" runat="server" SkinID="" />
                                                                        </ItemTemplate>
                                                                    </telerikWebControls:ExGridTemplateColumn>
                                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="DateCertificationException"
                                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <webControls:ExHyperLink ID="hlDateCertificationException" runat="server" SkinID="" />
                                                                        </ItemTemplate>
                                                                    </telerikWebControls:ExGridTemplateColumn>
                                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1602 " SortExpression="NoOfMissingPreparer"
                                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <webControls:ExHyperLink ID="hlCertificationStatus" runat="server" SkinID="" />
                                                                        </ItemTemplate>
                                                                    </telerikWebControls:ExGridTemplateColumn>
                                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="CertificationDate"
                                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                                        <ItemTemplate>
                                                                            <webControls:ExHyperLink ID="hlDateCertification" runat="server" SkinID="" />
                                                                        </ItemTemplate>
                                                                    </telerikWebControls:ExGridTemplateColumn>
                                                                </Columns>
                                                            </telerik:GridTableView>
                                                        </DetailTables>
                                                        <Columns>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="UserName" ItemStyle-HorizontalAlign="left"
                                                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblUserName" runat="server" SkinID="" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="BackupUserName" ItemStyle-HorizontalAlign="Left"
                                                                ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblBackupUserName" runat="server" SkinID="" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>                                                    
                                                            <telerikWebControls:ExGridTemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%"
                                                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <webControls:ExImage ID="imgNoAccess" runat="server" SkinID="UserUnderYouNoAccess" />
                                                                    <webControls:ExImage ID="imgSharedAccess" runat="server" SkinID="UserUnderYouSharedAccess" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <%-- LabelID="1130 "--%>
                                                            <telerikWebControls:ExGridTemplateColumn LabelID="1600 " SortExpression="NoOfMissingPreparer"
                                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <webControls:ExHyperLink ID="hlCertificationBalancesStatus" runat="server" SkinID="" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="DateCertificationBalances"
                                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <webControls:ExHyperLink ID="hlDateCertificationBalances" runat="server" SkinID="" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn LabelID="1601 " SortExpression="NoOfMissingPreparer"
                                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <webControls:ExHyperLink ID="hlCertificationExceptionStatus" runat="server" SkinID="" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="DateCertificationException"
                                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <webControls:ExHyperLink ID="hlDateCertificationException" runat="server" SkinID="" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn LabelID="1602 " SortExpression="NoOfMissingPreparer"
                                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <webControls:ExHyperLink ID="hlCertificationStatus" runat="server" SkinID="" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="CertificationDate"
                                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                                <ItemTemplate>
                                                                    <webControls:ExHyperLink ID="hlDateCertification" runat="server" SkinID="" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                        </Columns>
                                                    </telerik:GridTableView>
                                                </DetailTables>
                                                <Columns>
                                                    <telerikWebControls:ExGridTemplateColumn UniqueName="UserName" ItemStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblUserName" runat="server" SkinID="" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn UniqueName="BackupUserName" ItemStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblBackupUserName" runat="server" SkinID="" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>                                                    
                                                    <telerikWebControls:ExGridTemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3%">
                                                        <ItemTemplate>
                                                            <webControls:ExImage ID="imgNoAccess" runat="server" SkinID="UserUnderYouNoAccess" />
                                                            <webControls:ExImage ID="imgSharedAccess" runat="server" SkinID="UserUnderYouSharedAccess" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <%--LabelID="1130 "--%>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1600 " SortExpression="NoOfMissingPreparer"
                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <webControls:ExHyperLink ID="hlCertificationBalancesStatus" runat="server" SkinID="" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="DateCertificationBalances"
                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <webControls:ExHyperLink ID="hlDateCertificationBalances" runat="server" SkinID="" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1601 " SortExpression="NoOfMissingPreparer"
                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <webControls:ExHyperLink ID="hlCertificationExceptionStatus" runat="server" SkinID="" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="DateCertificationException"
                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <webControls:ExHyperLink ID="hlDateCertificationException" runat="server" SkinID="" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1602 " SortExpression="NoOfMissingPreparer"
                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <webControls:ExHyperLink ID="hlCertificationStatus" runat="server" SkinID="" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="CertificationDate"
                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <webControls:ExHyperLink ID="hlDateCertification" runat="server" SkinID="" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerikWebControls:ExRadGrid>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                
                   <tr class="BlankRow">
                    <td>
                    </td>
                 </tr>
                 
                 
                  <tr>
                    <td>
                        <asp:Panel ID="pnlOtherThanPRAStatus" runat="server">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <webControls:ExLabel ID="ExLabel1" LabelID="2171" runat="server" SkinID="SubSectionHeading"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr class="BlankRow">
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerikWebControls:ExRadGrid ID="rgOtherUsers" runat="server" EntityNameLabelID="1046"
                                            AllowPaging="false" AllowSorting="false" OnItemDataBound="rgOtherUsers_ItemDataBound"
                                            AllowExportToExcel="True" OnItemCreated="rgOtherUsers_ItemCreated" OnItemCommand="rgOtherUsers_OnItemCommand" AllowExportToPDF="False"
                                            AllowPrint="true" AllowPrintAll="true">
                                            <MasterTableView Name="lavel1" ExpandCollapseColumn-Display="true" DataKeyNames="UserID">
                                                <Columns>
                                                    <telerikWebControls:ExGridTemplateColumn UniqueName="UserName" ItemStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblUserName" runat="server" SkinID="" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                      <telerikWebControls:ExGridTemplateColumn UniqueName="UserRole" ItemStyle-HorizontalAlign="Left"
                                                        ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblUserRole" runat="server" SkinID="" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    
                                                    
                                                    
                                                    
                                                    
                                                    <telerikWebControls:ExGridTemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%"
                                                        HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="3%">
                                                        <ItemTemplate>
                                                            <webControls:ExImage ID="imgNoAccess" runat="server" SkinID="UserUnderYouNoAccess" />
                                                            <webControls:ExImage ID="imgSharedAccess" runat="server" SkinID="UserUnderYouSharedAccess" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <%--LabelID="1130 "--%>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1600 " SortExpression="NoOfMissingPreparer"
                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <webControls:ExHyperLink ID="hlCertificationBalancesStatus" runat="server" SkinID="" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="DateCertificationBalances"
                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <webControls:ExHyperLink ID="hlDateCertificationBalances" runat="server" SkinID="" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1601 " SortExpression="NoOfMissingPreparer"
                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15%" HeaderStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <webControls:ExHyperLink ID="hlCertificationExceptionStatus" runat="server" SkinID="" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="DateCertificationException"
                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <webControls:ExHyperLink ID="hlDateCertificationException" runat="server" SkinID="" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1602 " SortExpression="NoOfMissingPreparer"
                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <webControls:ExHyperLink ID="hlCertificationStatus" runat="server" SkinID="" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="CertificationDate"
                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <webControls:ExHyperLink ID="hlDateCertification" runat="server" SkinID="" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerikWebControls:ExRadGrid>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                
                
                
                
                
                
            </table>
            <div id="updateProgerssDiv">
                <UserControls:ProgressBar ID="ucProgressBar" runat="server" EnableTheming="true"
                    AssociatedUpdatePanelID="upnlMain" Visible="true" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
