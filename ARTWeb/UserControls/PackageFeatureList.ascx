<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PackageFeatureList.ascx.cs"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_PackageFeatureList" %>
<%@ Import Namespace="SkyStem.Language.LanguageUtility" %>
<table border="0" cellspacing="0" cellpadding="0" width="100%">
    <tr>
        <td align="left">
            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                <tr class="TableHeaderSameAsGrid">
                    <td>
                        <webControls:ExLabel ID="lblSerialNo" runat="server"></webControls:ExLabel>
                    </td>
                    <td>
                        <webControls:ExLabel ID="lblFeatureName" runat="server"></webControls:ExLabel>
                    </td>
                    <asp:Repeater ID="rptPackageHeader" runat="server" OnItemDataBound="rptPackageHeader_ItemDataBound">
                        <ItemTemplate>
                            <td align="center" width="100px">
                                <div id="tdPackageName" runat="server" style="width: 100%; height: 100%; text-align: center;">
                                    <webControls:ExLabel ID="lblPackageName" runat="server" Text='<%#LanguageUtil.GetValue(Convert.ToInt32(Eval("PackageNameLabelID")))%>'></webControls:ExLabel>
                                    <webControls:ExLabel ID="lblIsCutomizedPackage" runat="server" Text="*" ForeColor="Red"
                                        Visible="false"></webControls:ExLabel>
                                </div>
                            </td>
                        </ItemTemplate>
                    </asp:Repeater>
                </tr>
                <asp:Repeater ID="rptFeatures" runat="server" OnItemDataBound="rptFeatures_ItemDataBound">
                    <ItemTemplate>
                        <tr class="TableRowSameAsGrid">
                            <td>
                                <webControls:ExLabel ID="lblSerialNo" runat="server"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblFeatureName" runat="server" Text='<%#LanguageUtil.GetValue(Convert.ToInt32(Eval("FeatureNameLabelID")))%>'></webControls:ExLabel>
                            </td>
                            <asp:Repeater ID="rptFeatureAvailable" runat="server" OnItemDataBound="rptFeatureAvailable_ItemDataBound">
                                <ItemTemplate>
                                    <td align="center">
                                        <div runat="server" id="tdIsFeatureAvailable1" style="width: 100%; height: 100%">
                                            <webControls:ExLabel ID="lblIsFeatureAvailable" runat="server" Visible="false"></webControls:ExLabel>
                                            <webControls:ExImageButton ID="imgShowFeatureAvailablity" runat="server" ImageAlign="AbsMiddle">
                                            </webControls:ExImageButton>
                                            <ajaxToolkit:HoverMenuExtender ID="hme1" runat="Server" TargetControlID="imgShowFeatureAvailablity"
                                                PopupControlID="PopupMenu" HoverCssClass="popupHover" PopupPosition="Right" />
                                            <asp:Panel ID="PopupMenu" runat="server" CssClass="popupMenu">
                                                <asp:Repeater ID="rptAvailableReport" runat="server" OnItemDataBound="rptAvailableReport_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr class="TableHeaderSameAsGrid">
                                                                <td>
                                                                    <webControls:ExLabel ID="lblSerialNoAR" LabelID="2081" runat="server"></webControls:ExLabel>
                                                                </td>
                                                                <td>
                                                                    <webControls:ExLabel ID="lblAvailableReportHeading" LabelID="2174" runat="server"></webControls:ExLabel>
                                                                </td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="TableRowSameAsGrid">
                                                            <td>
                                                                <webControls:ExLabel ID="lblSerialNoAvailableReport" runat="server"></webControls:ExLabel>
                                                            </td>
                                                            <td>
                                                                <webControls:ExLabel ID="lblAvailableReport" runat="server"></webControls:ExLabel>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr class="TableAlternateRowSameAsGrid">
                                                            <td>
                                                                <webControls:ExLabel ID="lblSerialNoAvailableReport2" runat="server"></webControls:ExLabel>
                                                            </td>
                                                            <td>
                                                                <webControls:ExLabel ID="lblAvailableReport2" runat="server"></webControls:ExLabel>
                                                            </td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    <%--  <tr>
                                                        <td class="HoverRow">
                                                            <webControls:ExLabel ID="lblAvailableReport" SkinID="Black9Arial" Text='<%#LanguageUtil.GetValue(Convert.ToInt32(Eval("ReportLabelID")))%>'
                                                                Width="250" runat="server"></webControls:ExLabel>
                                                        </td>
                                                    </tr>--%>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="TableAlternateRowSameAsGrid">
                            <td>
                                <webControls:ExLabel ID="lblSerialNo" runat="server"></webControls:ExLabel>
                            </td>
                            <td>
                                <asp:Label ID="lblFeatureName" runat="server" Text='<%#LanguageUtil.GetValue(Convert.ToInt32(Eval("FeatureNameLabelID")))%>'></asp:Label>
                            </td>
                            <asp:Repeater ID="rptFeatureAvailable" runat="server" OnItemDataBound="rptFeatureAvailable_ItemDataBound">
                                <ItemTemplate>
                                    <td align="center">
                                        <div runat="server" id="tdIsFeatureAvailable" style="width: 100%; height: 100%">
                                            <asp:Label ID="lblIsFeatureAvailable" runat="server" Visible="false"></asp:Label>
                                            <webControls:ExImageButton ID="imgShowFeatureAvailablity" runat="server"></webControls:ExImageButton>
                                            <ajaxToolkit:HoverMenuExtender ID="hme1" runat="Server" TargetControlID="imgShowFeatureAvailablity"
                                                PopupControlID="PopupMenu" HoverCssClass="popupHover" PopupPosition="Right" />
                                            <asp:Panel ID="PopupReportList" runat="server" CssClass="PopupReportList">
                                                <asp:Repeater ID="rptAvailableReport" runat="server" OnItemDataBound="rptAvailableReport_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <table border="1" cellpadding="0" cellspacing="1" width="100%">
                                                            <tr class="TableHeaderSameAsGrid">
                                                                <td>
                                                                    <webControls:ExLabel ID="lblSerialNoAR" LabelID="2081" runat="server"></webControls:ExLabel>
                                                                </td>
                                                                <td>
                                                                    <webControls:ExLabel ID="lblAvailableReportHeading" LabelID="2174" runat="server"></webControls:ExLabel>
                                                                </td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="TableRowSameAsGrid">
                                                            <td>
                                                                <webControls:ExLabel ID="lblSerialNoAvailableReport" runat="server"></webControls:ExLabel>
                                                            </td>
                                                            <td>
                                                                <webControls:ExLabel ID="lblAvailableReport" runat="server"></webControls:ExLabel>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr class="TableAlternateRowSameAsGrid">
                                                            <td>
                                                                <webControls:ExLabel ID="lblSerialNoAvailableReport2" runat="server"></webControls:ExLabel>
                                                            </td>
                                                            <td>
                                                                <webControls:ExLabel ID="lblAvailableReport2" runat="server"></webControls:ExLabel>
                                                            </td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    <%-- <tr class="HoverRow">
                                                            <td>
                                                                <webControls:ExLabel ID="lblAvailableReport" SkinID="Black9Arial" Text='<%#LanguageUtil.GetValue(Convert.ToInt32(Eval("ReportLabelID")))%>'
                                                                    Width="300" runat="server"></webControls:ExLabel>
                                                            </td>
                                                        </tr>--%>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left">
            <table class="LegendTable" border="0" cellpadding="0" cellspacing="0">
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="LegendHeading">
                        <webControls:ExLabel ID="lblHeading" FormatString="{0}:" runat="server" LabelID="1383"></webControls:ExLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="LegendTable" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left">
                                    <webControls:ExImage ID="imgFeatureAvailable" runat="server" ImageUrl="~/App_Themes/SkyStemBlueBrown/Images/BlueTickIcon.gif" />
                                </td>
                                <td align="left">
                                    <webControls:ExLabel ID="lblFeatureAvailable" runat="server" SkinID="LegendLabel"
                                        Text='<%#LanguageUtil.GetValue(Convert.ToInt32(Eval("PackageNameLabelID")))%>'></webControls:ExLabel>
                                </td>
                                <td align="left">
                                    <webControls:ExImage ID="imgFeatureNotAvailable" runat="server" SkinID="LegendLabel"
                                        ImageUrl="~/App_Themes/SkyStemBlueBrown/Images/CrossIcon.gif" />
                                </td>
                                <td align="left">
                                    <webControls:ExLabel ID="lblFeatureNotAvailable" runat="server" SkinID="LegendLabel"
                                        Text='<%#LanguageUtil.GetValue(Convert.ToInt32(Eval("PackageNameLabelID")))%>'></webControls:ExLabel>
                                </td>
                                <td align="left">
                                    <webControls:ExImage ID="imgFeatureAvailablePartial" runat="server" SkinID="LegendLabel"
                                        ImageUrl="~/App_Themes/SkyStemBlueBrown/Images/GreenTickIcon.gif" />
                                </td>
                                <td align="left">
                                    <webControls:ExLabel ID="lblFeatureAvailablePartial" runat="server" SkinID="LegendLabel"
                                        Text='<%#LanguageUtil.GetValue(Convert.ToInt32(Eval("PackageNameLabelID")))%>'></webControls:ExLabel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr class="BlankRow">
        <td>
        </td>
    </tr>
    <tr>
        <td align="left">
            <webControls:ExLabel ID="lblFootNotes" SkinID="LegendLabel" runat="server" Visible="false"></webControls:ExLabel>
        </td>
    </tr>
</table>
