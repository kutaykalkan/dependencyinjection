<%@ Page Language="C#" MasterPageFile="~/MasterPages/CertificationMasterPage.master"
    AutoEventWireup="true" CodeFile="CertificationException.aspx.cs" Inherits="Pages_CertificationException"
    Theme="SkyStemBlueBrown" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<%@ Register Src="~/UserControls/Signature.ascx" TagName="Signature" TagPrefix="UserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCertification" runat="Server">
    <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">
                            <UserControl:SkyStemARTGrid ID="ucSkyStemARTGrid" runat="server" OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound"
                                OnGrid_NeedDataSourceEventHandler="ucSkyStemARTGrid_NeedDataSourceEventHandler"
                                ShowFooter="true" Grid-AllowExportToExcel="true" Grid-AllowPaging="true" Grid-AllowExportToPDF="False">
                                <SkyStemGridColumnCollection>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1357 " SortExpression="AccountNumber">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlAccountNumber" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1346 " SortExpression="AccountName">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlAccountName" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn ItemStyle-Wrap="false"   UniqueName="AmountReportingCurrency"  SortExpression="GLBalanceReportingCurrency">
                                        <ItemTemplate >
                                            <webControls:ExHyperLink ID="hlGLBalance" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1013 " UniqueName="RiskRating"
                                        SortExpression="RiskRating">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlRiskRating" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1433 " UniqueName="Materiality"
                                        SortExpression="IsMaterial">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlIsMaterial" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1014  " UniqueName="KeyAccount"
                                        SortExpression="IsKeyAccount">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlIsKeyAccount" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1130  " UniqueName="Preparer" FooterStyle-Wrap="false" SortExpression="PreparerFullName" FooterStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlPreparer" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <%--<webControls:ExLabel ID="lblGrandTotal" LabelID="2002" CssClass="GridFooterRightAlignement" runat="server" SkinID="Black11Arial" />--%>
                                            <webControls:ExLabel ID="lblGrandTotal" LabelID="2309" runat="server" SkinID="Black11Arial" />
                                        </FooterTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1131  " UniqueName="Reviewer" SortExpression="ReviewerFullName">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlReviewer" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1132  " UniqueName="Approver" SortExpression="ApproverFullName">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlApprover" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1678 " ItemStyle-Wrap="false" DataType="System.Decimal"
                                        SortExpression="UnexplainedVarianceReportingCurrency">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlUnexplainedVariance" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <webControls:ExLabel ID="lblTotalUnexplainedVariance" runat="server" SkinID="Black11Arial" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right"/>
                                        
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1425" ItemStyle-Wrap="false" SortExpression="WriteOnOffAmountReportingCurrency">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlWriteOffOn" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <webControls:ExLabel ID="lblTotalWriteOffOn" runat="server" SkinID="Black11Arial" />
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right"/>
                                    </telerikWebControls:ExGridTemplateColumn>
                                </SkyStemGridColumnCollection>
                            </UserControl:SkyStemARTGrid>
                        </asp:Panel>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table style="width: 96%" cellpadding="0" cellspacing="0" class="DataImportStatusMessage">
                            <tr class="BlankRow">
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <webControls:ExLabel ID="lblCertificationVerbiage" runat="server" SkinID="Black11ArialNormal"
                                        Width="100%" />
                                </td>
                            </tr>
                            <tr class="BlankRow">
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table style="width: 96%" cellpadding="0" cellspacing="0">
                            <col width="20%" />
                            <col width="80%" />
                            <tr class="BlankRow">
                                <td>
                                </td>
                            </tr>
                            <tr class="BlankRow">
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <webControls:ExLabel ID="lblAdditionalComments" runat="server" LabelID="1468 " FormatString="{0}:"
                                        SkinID="Black11Arial" />
                                </td>
                                <td align="left">
                                    <webControls:ExTextBox ID="txtAdditionalComments" runat="server" SkinID="ExTextBoxAdditionalComments" />
                                    <webControls:ExLabel ID="lblAdditionalCommentsValue" runat="server" FormatString="{0}:"
                                        SkinID="Black11ArialNormal" />
                                </td>
                            </tr>
                            <tr class="BlankRow">
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <webControls:ExButton ID="btnAgree" runat="server" LabelID="1465 " OnClick="btnAgree_Click"
                                        SkinID="ExButton100" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <UserControl:Signature ID="ucSignature" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <UserControls:ProgressBar ID="ucProgressBarMain" runat="server" AssociatedUpdatePanelID="upnlMain" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
