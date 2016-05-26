<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_RecControlCheckList" Codebehind="RecControlCheckList.ascx.cs" %>
<%@ Register TagPrefix="UserControls" TagName="RecControlCheckListGrid" Src="~/UserControls/RecControlCheckList/RecControlCheckListGrid.ascx" %>
<div id="divMainContent" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="2px"></td>
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
                            <asp:Image SkinID="ArrowTop" runat="server" />
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
                            <table id="tblMainContent" width="100%" border="0" cellpadding="0" cellspacing="0">
                                <%-- <tr>
                                    <td class="GridHeaderPadding">
                                        <webControls:ExLabel ID="lblGridHeading" LabelID="1872" runat="server" SkinID="BlueBold11ArialUnderline"></webControls:ExLabel>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td>
                                        <UserControls:RecControlCheckListGrid ID="ucRecControlCheckListGrid" AllowExportToExcel="false"
                                            AllowExportToPDF="false" AllowCustomPaging="true" AllowSelectionPersist="false"
                                            ShowSelectCheckBoxColum="true" runat="server">
                                            <columncollection> 
                                                <telerikWebControls:ExGridButtonColumn UniqueName="DeleteColumn" CommandName="Delete"
                                                    ConfirmDialogType="Classic" ConfirmTextLabelID="1781" ConfirmTextFormatString="{0}?"
                                                    ButtonCssClass="DeleteButton" HeaderStyle-Width="5%">
                                                </telerikWebControls:ExGridButtonColumn>
                                            </columncollection>
                                        </UserControls:RecControlCheckListGrid>
                                    </td>
                                </tr>
                                <tr class="BlankRow">
                                    <td></td>
                                </tr>
                                <tr id="trOpenItemsButtonRow" runat="server">
                                    <td align="right">
                                        <webControls:ExButton ID="btnAdd" runat="server" LabelID="1560" SkinID="ExButton100" />
                                        <webControls:ExButton ID="btnDelete" runat="server" LabelID="1564" SkinID="ExButton100"
                                            OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                            </table>
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
            <td width="2px"></td>
        </tr>
    </table>
</div>
<asp:HiddenField runat="server" ID="hdIsRefreshData" />
<asp:HiddenField runat="server" ID="hdIsExpanded" Value="0" />
