<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true"
    CodeFile="ReportRun.aspx.cs" Inherits="Pages_ReportRun" Title="Untitled Page"
    Theme="SkyStemBlueBrown" %>

<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <%--<tr class="blueRow">
            <td align="left">
                <webControls:ExLabel ID="lblReportDetails" runat="server" Text="ENTITY - ACCT# - ACCT NAME"
                    SkinID="BlueBold11Arial"></webControls:ExLabel>
            </td>
        </tr>--%>
                <tr>
                    <td align="right" style="padding-right: 10px">
                        <webControls:ExHyperLink ID="hlReselectParameters" runat="server"></webControls:ExHyperLink>
                    </td>
                </tr>
            </table>
            <%--<div id="divReport" runat="server">
        <UserControls:Report ID="rpt1" runat="server" />
    </div>--%>
    
    
            <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">
                <UserControl:SkyStemARTGrid ID="ucSkyStemARTGrid" runat="server" OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound">
                    <SkyStemGridColumnCollection>
                        <telerikWebControls:ExGridTemplateColumn LabelID="1357" SortExpression="AccountNumber"
                            DataType="System.String" UniqueName="AccountNumber" Visible="false">
                            <ItemTemplate>
                                <webControls:ExHyperLink ID="hlAccountNumber" runat="server" SkinID="GridHyperLink" />
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn LabelID="1346" SortExpression="AccountName"
                            DataType="System.String" UniqueName="AccountName" Visible="false">
                            <ItemTemplate>
                                <webControls:ExHyperLink ID="hlAccountName" runat="server" SkinID="GridHyperLink" />
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn LabelID="1013 ">
                            <ItemTemplate>
                                <webControls:ExHyperLink ID="hlRiskRating" runat="server" SkinID="GridHyperLink" />
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn LabelID="1433 ">
                            <ItemTemplate>
                                <webControls:ExHyperLink ID="hlIsMaterial" runat="server" SkinID="GridHyperLink" />
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        
                        <telerikWebControls:ExGridTemplateColumn LabelID="1504  ">
                            <ItemTemplate>
                                <webControls:ExHyperLink ID="hlReason" runat="server" SkinID="GridHyperLink" />
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn LabelID="1014  ">
                            <ItemTemplate>
                                <webControls:ExHyperLink ID="hlIsKeyAccount" runat="server" SkinID="GridHyperLink" />
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn LabelID="1382" SortExpression="GLBalanceReportingCurrency"
                            DataType="System.Decimal" UniqueName="GLBalance" Visible="false">
                            <ItemTemplate>
                                <webControls:ExHyperLink ID="hlGLBalance" runat="server" SkinID="GridHyperLink" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn LabelID="1130" SortExpression="PreparerFullName"
                            DataType="System.String" UniqueName="Preparer" Visible="false">
                            <ItemTemplate>
                                <webControls:ExHyperLink ID="hlPreparer" runat="server" SkinID="GridHyperLink" />
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                    </SkyStemGridColumnCollection>
                </UserControl:SkyStemARTGrid>
            </asp:Panel>
            
            
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr class="BlankRow">
                </tr>
                <tr>
                    <td align="right">
                        <webControls:ExButton ID="btnReportSignOff" runat="server" LabelID="1582 " OnClick="btnReportSignOff_Click"
                            SkinID="ExButton200" />
                        <webControls:ExButton ID="btnArchive" runat="server" LabelID="1583 " OnClick="btnArchive_Click"
                            CausesValidation="false" SkinID="ExButton100" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <div id="divComment" runat="server" align="center">
                <table style="width: 90%;" cellpadding="0" cellspacing="0">
                    <col width="20%" />
                    <col width="80%" />
                    <tr align="left">
                        <td>
                            <webControls:ExLabel ID="lblReportPeriodText" runat="server" LabelID="1584   " SkinID="Black11Arial" />
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblReportPeriodValue" runat="server" SkinID="Black11ArialNormal" />
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                            <webControls:ExLabel ID="lblShareWith" runat="server" LabelID="1585" SkinID="Black11Arial" />
                        </td>
                        <%-- <td>
                <asp:DropDownList ID="ddlShareWith" runat="server" SkinID="DropDownList200" />
            </td>--%>
                        <td>
                            <webControls:ExTextBox ID="txtShareWithUserName" runat="server" SkinID="ExTextBox150" />
                            &nbsp;
                            <webControls:ExImage ID="imgShareWithUserName" runat="server" SkinID="AddUser" LabelID="1607" />
                        </td>
                    </tr>
                    <tr colspan="2" align="left">
                        <td>
                            <webControls:ExLabel ID="lblComment" runat="server" LabelID="1468  " SkinID="Black11Arial"
                                FormatString="{0}:" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <webControls:ExTextBox ID="txtComment" runat="server" Width="100%" TextMode="MultiLine"
                                SkinID="ExTextBoxAdditionalComments" />
                        </td>
                    </tr>
                    <tr class="BlankRow">
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <webControls:ExButton ID="btnSignOff" runat="server" LabelID="1377 " OnClick="btnSignOff_Click"
                                SkinID="ExButton100" />
                            <webControls:ExButton ID="btnSave" runat="server" LabelID="1315  " OnClick="btnSave_Click"
                                SkinID="ExButton100" />
                            <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239  " OnClick="btnCancel_Click"
                                SkinID="ExButton100" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <UserControls:ProgressBar ID="ucProgressBarMain" runat="server" AssociatedUpdatePanelID="upnlMain" />
</asp:Content>
