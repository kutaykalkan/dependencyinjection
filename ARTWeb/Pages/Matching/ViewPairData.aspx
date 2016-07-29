<%@ Page Language="C#" AutoEventWireup="true" Inherits="Pages_Matching_ViewPairData"
    MasterPageFile="~/MasterPages/PopUpMasterPage.master" Theme="SkyStemBlueBrown" Codebehind="ViewPairData.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlHeader" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%" class="blueBorder">
            <tr class="blueRow">
                <td width="96%">
                    <webControls:ExLabel ID="lblContentTitle" runat="server" LabelID="2239" SkinID="Black11Arial" />
                </td>
                <td width="4%" align="right">
                    <webControls:ExImage ID="imgCollapseContent" runat="server" SkinID="CollapseIcon" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlSourec1Content" runat="server" CssClass="blueBorderWithMargin">
        <table cellpadding="0px" width="100%" cellspacing="0px">
            <%--<tr>
                <td>
                    <webControls:ExLabel ID="lblSourec1" runat="server" SkinID="Black11Arial" LabelID="2239"
                        FormatString="{0} :"></webControls:ExLabel>
                </td>
                <td>
                </td>
            </tr>--%>
            <tr>
                <td colspan="2">
                    <telerikWebControls:ExRadGrid ID="rgSource1" AllowMultiRowSelection="true" runat="server"
                        AllowPaging="true" OnNeedDataSource="rgSource1_NeedDataSource">
                        <MasterTableView EnableColumnsViewState="false">
                            <Columns>
                            </Columns>
                        </MasterTableView>
                    </telerikWebControls:ExRadGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeContent" TargetControlID="pnlSourec1Content"
        ImageControlID="imgCollapseContent" CollapseControlID="imgCollapseContent" ExpandControlID="imgCollapseContent"
        runat="server" SkinID="CollapsiblePanel" Collapsed="true">
    </ajaxToolkit:CollapsiblePanelExtender>
    <br />
    <asp:Panel ID="pnlSourec2ContentHdr" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%" class="blueBorder">
            <tr class="blueRow">
                <td width="96%">
                    <webControls:ExLabel ID="lblSourec2" runat="server" LabelID="2240" SkinID="Black11Arial" />
                </td>
                <td width="4%" align="right">
                    <webControls:ExImage ID="imgCollapseSourec2" runat="server" SkinID="CollapseIcon" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlSourec2Content" runat="server" CssClass="blueBorderWithMargin">
        <table cellpadding="0px" width="100%" cellspacing="0px">
           <%-- <tr>
                <td>
                    <webControls:ExLabel ID="ExLabel1" runat="server" SkinID="Black11Arial" LabelID="2240"
                        FormatString="{0} :"></webControls:ExLabel>
                </td>
                <td>
                </td>
            </tr>--%>
            <tr>
                <td colspan="2">
                    <telerikWebControls:ExRadGrid ID="rgSource2" AllowMultiRowSelection="true" runat="server"
                        AllowPaging="true" OnNeedDataSource="rgSource2_NeedDataSource">
                        <MasterTableView EnableColumnsViewState="false">
                            <Columns>
                            </Columns>
                        </MasterTableView>
                    </telerikWebControls:ExRadGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeContent2" TargetControlID="pnlSourec2Content"
        ImageControlID="imgCollapseSourec2" CollapseControlID="imgCollapseSourec2" ExpandControlID="imgCollapseSourec2"
        runat="server" SkinID="CollapsiblePanel" Collapsed="true">
    </ajaxToolkit:CollapsiblePanelExtender>
</asp:Content>
