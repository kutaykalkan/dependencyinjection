<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MatchingMaster.master"
    AutoEventWireup="true" Theme="SkyStemBlueBrown"
    Inherits="Pages_Matching_MatchingResults" Codebehind="MatchingResults.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="AccountHierarchyDetail" Src="~/UserControls/AccountHierarchyDetail.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="Matched" Src="~/UserControls/Matching/MatchSetResults/Matched.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="UnMatched" Src="~/UserControls/Matching/MatchSetResults/UnMatched.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="PartialMatched" Src="~/UserControls/Matching/MatchSetResults/PartialMatched.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="MatchingCombinationSelection" Src="~/UserControls/Matching/Wizard/MatchingCombinationSelection.ascx" %>
<%@ Register TagPrefix="usc" TagName="MatchSetInfo" Src="~/UserControls/Matching/MatchSetInfo.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMatching" runat="Server">
    <asp:Panel ID="pnlMessage" runat="server">
        <table width="1003px">
            <tr>
                <td width="1003px" align="center">
                    <webControls:ExLabel ID="lblError" runat="server" LabelID="1051" FormatString="{0}:"
                        SkinID="Black11Arial"></webControls:ExLabel>&nbsp;&nbsp;
                    <webControls:ExLabel ID="lblErrorMessage" LabelID="2352" SkinID="Black11ArialNormal"
                        runat="server"></webControls:ExLabel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlResult" runat="server">
        &nbsp;<usc:MatchSetInfo ID="uscMatchSetInfo" runat="server" />
        <table width="1003px">
            <tr>
                <td>
                    <UserControls:MatchingCombinationSelection ID="ucMatchingCombinationSelection" runat="server" EnabledValidatePageData="false" />
                </td>
            </tr>
            <tr>
                <td valign="bottom">
                    <table width="1003px" style="table-layout: fixed" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="600px">
                                <telerikWebControls:ExRadTabStrip Skin="SkyStemBlueBrown" EnableEmbeddedSkins="false"
                                    ID="rtsTabs" runat="server" ReorderTabsOnSelect="true" Align="Justify" Width="600px"
                                    SelectedIndex="0" MultiPageID="rmpTabPages">
                                    <Tabs>
                                        <telerikWebControls:ExRadTab Width="200px" LabelID="2333">
                                        </telerikWebControls:ExRadTab>
                                        <telerikWebControls:ExRadTab Width="200px" LabelID="2332">
                                        </telerikWebControls:ExRadTab>
                                        <telerikWebControls:ExRadTab Width="200px" LabelID="2334">
                                        </telerikWebControls:ExRadTab>
                                    </Tabs>
                                </telerikWebControls:ExRadTabStrip>
                            </td>
                            <td width="403px" class="TDTabEmptyArea">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="TDTabPageArea">
                                <telerikWebControls:ExRadMultiPage Width="99.8%" Height="100%" ID="rmpTabPages" runat="server"
                                    SelectedIndex="0">
                                    <telerikWebControls:ExRadPageView ID="rpvUnMatched" runat="server">
                                        <UserControls:UnMatched ID="ucUnMatched" runat="server" />
                                    </telerikWebControls:ExRadPageView>
                                    <telerikWebControls:ExRadPageView ID="rpvMatched" runat="server">
                                        <UserControls:Matched ID="ucMatched" runat="server" />
                                    </telerikWebControls:ExRadPageView>
                                    <telerikWebControls:ExRadPageView ID="rpvPartialMatched" runat="server">
                                        <UserControls:PartialMatched ID="ucPartialMatched" runat="server" />
                                    </telerikWebControls:ExRadPageView>
                                </telerikWebControls:ExRadMultiPage>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <webControls:ExButton ID="btnBack" LabelID="1239" CausesValidation="false" runat="server"
                        OnClick="btnBack_Click" />
                    
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
