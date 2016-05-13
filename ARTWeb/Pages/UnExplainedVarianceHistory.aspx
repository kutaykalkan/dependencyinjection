<%@ Page Language="C#" MasterPageFile="~/MasterPages/RecProcessMasterPage.master" AutoEventWireup="true"
    CodeFile="UnExplainedVarianceHistory.aspx.cs" Inherits="Pages_UnExplainedVarianceHistory"
    Title="Untitled Page" Theme="SkyStemBlueBrown" %>

<%@ Register TagPrefix="UserControls" TagName="AccountHierarchyDetail" Src="~/UserControls/AccountHierarchyDetail.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphRecProcess" runat="Server">
    <div id="divMainContent">
        <asp:UpdatePanel ID="updpnlMain" runat="server">
            <ContentTemplate>
                <table id="tblMainContent" width="100%" cellpadding="0" cellspacing="0">
                    <tr class="blueRow">
                        <td align="left">
                            <UserControls:AccountHierarchyDetail ID="ucAccountHierarchyDetail" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">
                                <telerikWebControls:ExRadGrid ID="rgUnExpectedVarianceHistory" runat="server" ShowHeader="true"
                                    OnNeedDataSource="rgUnExpectedVarianceHistory_NeedDataSource"  OnItemDataBound="rgUnExpectedVariance_ItemDataBound"
                                      EntityNameLabelID="1391">
                                    <MasterTableView DataKeyNames="GLDataUnexplainedVarianceID">
                                        <Columns>
                                            <%--Entered By--%>
                                            <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="25%" UniqueName="AddedBy" LabelID="1508">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblAddedBy" runat="server"></webControls:ExLabel>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <webControls:ExLabel ID="lblAddedBy" runat="server"></webControls:ExLabel>
                                                </EditItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <%--Reconcilliation Period--%>
                                            <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="10%" UniqueName="ReconciliationPeriod" LabelID="1420">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblPeriodEndDate" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <%--Comment Date--%>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="CommentDate" HeaderStyle-Width="10%" LabelID="1399">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblDateAdded" runat="server" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Amount" HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblAmount" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="10px">
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <%--Comments--%>
                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Comments" HeaderStyle-Width="35%" LabelID="1408">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblComments" runat="server"></webControls:ExLabel>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerikWebControls:ExRadGrid>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <webControls:ExButton ID="btnCancel" runat="server" LabelID="1545" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="updateProgerssDiv">
                                <UserControls:ProgressBar ID="ucProgressBar" runat="server" EnableTheming="true"
                                    Visible="true" AssociatedUpdatePanelID="updpnlMain" />
                            </div>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
