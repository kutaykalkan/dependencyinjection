<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" Inherits="Pages_SignedReports" Title="Signed Reports"
    Theme="SkyStemBlueBrown" Codebehind="SignedReports.aspx.cs" %>

<%@ Register Src="~/UserControls/Report/ParameterViewer.ascx" TagPrefix="ucRpt" TagName="ParamViewer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">  
    
    <telerikWebControls:ExRadGrid ID="rgMain" runat="server" EntityNameLabelID="2805"
        AllowPaging="true" AllowSorting="true" AllowMultiRowSelection="true" OnItemDataBound="rgMain_ItemDataBound"
        AllowPrint="true" AllowPrintAll="true" AllowExportToExcel="true" AllowExportToPDF="true"
        OnItemCreated="rgMain_ItemCreated" OnItemCommand="rgMain_OnItemCommand" OnNeedDataSource="rgMain_NeedDataSource">
        <MasterTableView DataKeyNames="ReportID,ReportArchiveID, ReportUrl, ReportArchiveParameterByRptArchiveID">
            <Columns>
                <telerikWebControls:ExGridTemplateColumn LabelID="1572" UniqueName="ReportName" SortExpression="">
                    <ItemTemplate>
                        <webControls:ExLinkButton ID="lnkbtnReportName" runat="server" SkinID="GridLinkButton"
                            OnCommand="LinkButton_Command"></webControls:ExLinkButton>
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1572" UniqueName="ReportNameForExport" Visible="false" SortExpression="">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblReportName" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1628" UniqueName="SignedBy" SortExpression="">
                    <ItemTemplate>
                        <webControls:ExLinkButton ID="lnkbtnSignedBy" runat="server" SkinID="GridLinkButton"
                            OnCommand="LinkButton_Command"></webControls:ExLinkButton>
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1628" UniqueName="SignedByForExport" Visible="false" SortExpression="">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblSignedBy" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1399" UniqueName="ArchiveDate" SortExpression="">
                    <ItemTemplate>
                        <webControls:ExLinkButton ID="lnkbtnArchiveDate" runat="server" SkinID="GridLinkButton"
                            OnCommand="LinkButton_Command"></webControls:ExLinkButton>
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1399" UniqueName="ArchiveDateForExport" Visible="false" SortExpression="">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblArchiveDate" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1579 " UniqueName="ActionPerformed" SortExpression="">
                    <ItemTemplate>
                        <webControls:ExLinkButton ID="lnkbtnActionPerformed" runat="server" SkinID="GridLinkButton"
                            OnCommand="LinkButton_Command"></webControls:ExLinkButton>
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1579" UniqueName="ActionPerformedForExport" Visible="false" SortExpression="">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblActionPerformed" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1563" UniqueName="ParamViewer" ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-Width="30%">
                    <ItemTemplate>
                        <ucRpt:ParamViewer ID="ucParamViewer" runat="server" ParamDisplayNameField="ParamDisplayName"
                            ParamValueField="ParamDisplayValue" OnbtnLinkClick="linkParamClick" />
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1563" UniqueName="ParamViewerForExport" Visible="false" SortExpression="">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblParamViewer" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1848" UniqueName="Comments" SortExpression="">
                    <ItemTemplate>
                        <webControls:ExLinkButton ID="lnkbtnComments" runat="server" SkinID="GridLinkButton"
                            OnCommand="LinkButton_Command"></webControls:ExLinkButton>
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1848" UniqueName="CommentsForExport" Visible="false" SortExpression="">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblComments" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerikWebControls:ExRadGrid>
    
</asp:Content>
