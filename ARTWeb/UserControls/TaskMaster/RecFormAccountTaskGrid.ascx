<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecFormAccountTaskGrid.ascx.cs"
    Inherits="UserControls_TaskMaster_RecFormAccountTaskGrid" %>
<%@ Register Src="~/UserControls/TaskMaster/GeneralTaskGrid.ascx" TagName="GeneralTaskGrid"
    TagPrefix="UserControl" %>
<div id="divMainContent" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
        
            <td width="2px">
            </td>
            <td>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <col width="2px" />
                    <col width="82px" />
                    <col width="1210px" />
                    <col width="2px" />
                    <tr>
                        <td class="ExpandPanelTopLeft" height="16" align="left">
                            <asp:Image ID="Image1" SkinID="BorderTopLeft" runat="server" />
                        </td>
                        <td height="16" align="right">
                            <asp:Image ID="imgArrowTop" SkinID="ArrowTop" runat="server" />
                        </td>
                        <td class="ExpandPanelTopBorder" height="16" align="left">
                            <asp:Image ID="Image5" SkinID="BorderHorizontalTop" runat="server" />
                        </td>
                        <td class="ExpandPanelTopLeft" height="16">
                            <asp:Image ID="Image3" SkinID="BorderTopLeft" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ExpandPanelLeftBorder">
                            <asp:Image ID="Image2" SkinID="BorderVerticalLeft" runat="server" />
                        </td>
                        <td colspan="2">
                            <!-- Start - User Control Content here --->
                            <asp:UpdatePanel ID="updpnlMain" runat="server">
                                <ContentTemplate>
                                    <table id="tblMainContent" width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr class="BlankRow">
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlGrid" runat="server">
                                                    <UserControl:GeneralTaskGrid ID="ucGeneralTaskGrid" runat="server" AllowActionMenu="true"
                                                        ShowSelectCheckBoxColum="false" AllowExportToExcel="true" AllowExportToPDF="true"
                                                        AllowCustomPaging="true" AllowCustomApprove="true" AllowCustomReject="true" AllowCustomDone="true"
                                                        AllowSelectionPersist="false" GridApplyApproveOnClientClick="ShowApprovePageForATPendingGrid(); return false;"
                                                        GridApplyRejectOnClientClick="ShowRejectPageForATPendingGrid(); return false;"
                                                        GridApplyDoneOnClientClick="ShowDonePageForATPendingGrid(); return false;" ShowEditColumn="true"  ShowByDefaultViewIcon="true"
                                                        />
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <!-- End - User Control Content here  --->
                        </td>
                        <td class="ExpandPanelLeftBorder">
                            <asp:Image ID="Image4" SkinID="BorderVerticalLeft" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="ExpandPanelBottomBorder">
                            <asp:Image ID="Image6" SkinID="BorderHorizontalBottom" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="2px">
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField runat="server" ID="hdIsRefreshData" Value="0" />
<asp:HiddenField runat="server" ID="hdIsExpanded" Value="0" />
