<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" Inherits="Pages_ReportActivity" Title="Untitled Page"
    Theme="SkyStemBlueBrown" Codebehind="ReportActivity.aspx.cs" %>

<%@ Register Src="~/UserControls/Report/ParameterViewer.ascx" TagPrefix="ucRpt" TagName="ParamViewer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" colspan="2">
                <webControls:ExLabel ID="lblCompanyName" runat="server" SkinID="BlueBold13Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <webControls:ExLabel ID="lblReportName" runat="server" SkinID="ReportTitle"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <webControls:ExLabel ID="lblReportDescription" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
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
    </table>
    <telerikWebControls:ExRadGrid ID="rgMain" runat="server" EntityNameLabelID="1229"
        AllowPaging="true" AllowSorting="true" AllowMultiRowSelection="true" OnItemDataBound="rgMain_ItemDataBound"
        AllowPrint="true" AllowPrintAll="true" OnNeedDataSource="rgMain_NeedDataSource">
        <MasterTableView DataKeyNames="ReportArchiveID, ReportUrl, ReportArchiveParameterByRptArchiveID">
            <Columns>
                <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="">
                    <ItemTemplate>
                        <webControls:ExLinkButton ID="lnkbtnArchiveDate" runat="server" SkinID="GridLinkButton"
                            OnCommand="LinkButton_Command"></webControls:ExLinkButton>
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1579 " SortExpression="">
                    <ItemTemplate>
                        <webControls:ExLinkButton ID="lnkbtnActionPerformed" runat="server" SkinID="GridLinkButton"
                            OnCommand="LinkButton_Command"></webControls:ExLinkButton>
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1563" ItemStyle-HorizontalAlign="Left"
                    HeaderStyle-Width="30%">
                    <ItemTemplate>
                        <ucRpt:ParamViewer ID="ucParamViewer" runat="server" ParamDisplayNameField="ParamDisplayName"
                            ParamValueField="ParamDisplayValue" OnbtnLinkClick="linkParamClick" />
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1848">
                    <ItemTemplate>
                        <webControls:ExLinkButton ID="lnkbtnComments" runat="server" SkinID="GridLinkButton"
                            OnCommand="LinkButton_Command"></webControls:ExLinkButton>
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerikWebControls:ExRadGrid>
    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr class="BlankRow">
        </tr>
        <tr>
            <td align="right">
                <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClick="btnCancel_Click"
                    SkinID="ExButton100" />
            </td>
        </tr>
    </table>
</asp:Content>
