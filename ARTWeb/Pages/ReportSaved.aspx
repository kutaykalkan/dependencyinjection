<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" Inherits="Pages_ReportSaved" Title="Untitled Page"
    Theme="SkyStemBlueBrown" Codebehind="ReportSaved.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register Src="~/UserControls/Report/ParameterViewer.ascx" TagPrefix="ucRpt" TagName="ParamViewer"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <%--<table style="width: 100%" cellpadding="0" cellspacing="0">
            <tr class="blueRow">
                <td align="left">
                    <webControls:ExLabel ID="lblReportDetails" runat="server" Text="ENTITY - ACCT# - ACCT NAME"
                        SkinID="BlueBold11Arial"></webControls:ExLabel>
                </td>
            </tr>
        </table>--%>
            <telerikWebControls:ExRadGrid ID="rgMain" runat="server" EntityNameLabelID="1609"
                AllowPaging="true" AllowSorting="true"  AllowMultiRowSelection="true"
                OnItemDataBound="rgMain_ItemDataBound"
                 AllowPrint="true" AllowPrintAll="true" >
                <MasterTableView DataKeyNames="UserMyReportID,UserMyReportSavedReportID, UserMyReportSavedReportName,DateAdded" >
                    <Columns>
                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" HeaderText="CheckboxSelect column <br />" />
                        <telerikWebControls:ExGridTemplateColumn LabelID="1572" SortExpression="">
                            <ItemTemplate>
                               
                                <webControls:ExLinkButton ID="lbtnReportName" CommandName="SendToReportCommand" OnCommand="SendToReport" runat="server" SkinID="GridLinkButton" />
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn LabelID="1573" SortExpression="">
                            <ItemTemplate>
                               
                                <webControls:ExLinkButton ID="lbtnSavedReportName"  CommandName="SendToReportCommand" OnCommand="SendToReport" runat="server" SkinID="GridLinkButton" />
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn  LabelID="1563 " ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                            <ItemTemplate>
                               
                                <%--<webControls:ExLinkButton ID="lbtnParameter"  CommandName="SendToReportCommand" OnCommand="SendToReport" runat="server" SkinID="GridLinkButton" />--%>
                                  <ucRpt:ParamViewer ID="ucParamViewer" runat="server" OnbtnLinkClick="linkParamClick"  ParamDisplayNameField="ParamDisplayName" ParamValueField="ParamDisplayValue" />
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn LabelID="1574 ">
                            <ItemTemplate>
                               
                                <webControls:ExLinkButton ID="lbtnDateSaved"  CommandName="SendToReportCommand" OnCommand="SendToReport" runat="server" SkinID="GridLinkButton" />
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn LabelID="1575 " ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                              
                                <webControls:ExLinkButton ID="lbtnSavedBy"  CommandName="SendToReportCommand" OnCommand="SendToReport" runat="server" SkinID="GridLinkButton" />
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--<telerikWebControls:ExGridTemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                    >
                    <ItemTemplate>
                        <webControls:ExHyperLink ID="hlRunReport" runat="server" LabelID="1577 " SkinID="GridHyperLink" />
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>--%>
                    </Columns>
                </MasterTableView>
            </telerikWebControls:ExRadGrid>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr class="BlankRow">
                </tr>
                <tr>
                    <td align="right">
                        <webControls:ExButton ID="btnDelete" runat="server" LabelID="1564 " OnClick="btnDelete_Click"
                            CausesValidation="false" SkinID="ExButton100" />
                        <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClick="btnCancel_Click"
                            SkinID="ExButton100" />
                    </td>
                </tr>
            </table>
            <UserControls:ProgressBar ID="ucProgressBarMain" runat="server" AssociatedUpdatePanelID="upnlMain" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
