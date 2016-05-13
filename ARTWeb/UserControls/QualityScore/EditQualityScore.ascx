<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditQualityScore.ascx.cs"
    Inherits="SkyStem.ART.Web.UserControls.EditQualityScore" %>
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
                                                <telerikWebControls:ExRadGrid ID="rgEditQualityScore" runat="server" GroupHeaderItemStyle-Height="25px"
                                                    OnItemDataBound="rgEditQualityScore_ItemDataBound" NoMasterRecordsLabelID="2423">
                                                    <MasterTableView DataKeyNames="CompanyQualityScoreID,SystemQualityScoreStatusID"
                                                        ClientDataKeyNames="SystemQualityScoreStatusID">
                                                        <Columns>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="QualityScoreNumber" LabelID="2419"
                                                                HeaderStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblQualityScoreNumber" runat="server" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Description" LabelID="2418">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblDescription" runat="server" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="SystemQualityScoreStatusID"
                                                                LabelID="2444" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <webControls:ExLabel ID="lblSystemQualityScoreStatus" runat="server" />
                                                                    <asp:Image ID="imgInfo" runat="server" Visible="false" SkinID="InfoIcon" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="UserQualityScoreStatusID" LabelID="2445">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlUserQualityScoreStatus" runat="server" SkinID="DropDownList100" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                            <telerikWebControls:ExGridTemplateColumn UniqueName="Comments" LabelID="1848">
                                                                <ItemTemplate>
                                                                    <webControls:ExTextBox ID="txtComments" ErrorPhraseID="5000312" runat="server" SkinID="ExMulitilineTextBox150" />
                                                                </ItemTemplate>
                                                            </telerikWebControls:ExGridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerikWebControls:ExRadGrid>
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

