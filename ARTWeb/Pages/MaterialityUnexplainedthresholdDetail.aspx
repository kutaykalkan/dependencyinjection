<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true" Inherits="Pages_MaterialityUnexplainedthresholdDetail"
    Theme="SkyStemBlueBrown" Codebehind="MaterialityUnexplainedthresholdDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <col width="2%" />
                    <col width="96%" />
                    <col width="2%" />
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <col width="40%" />
                                <col width="60%" />
                                <tr>
                                    <td colspan="2">
                                        <webControls:ExLabel ID="lblRecPeriod" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr class="BlankRow">
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                       <%-- <webControls:ExLabel ID="ExLabel1" LabelID="1349" runat="server" SkinID="BlueBold11ArialUnderline"></webControls:ExLabel>--%>
                                    </td>
                                </tr>
                                <tr class="BlankRow">
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <webControls:ExLabel ID="lblUnexplainedThreshold" LabelID="1349" FormatString="{0}:"
                                            runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                    </td>
                                    <td align="right">
                                        <webControls:ExLabel ID="lblUnexplainedThresholdValue" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="LegendHeading" colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr class="BlankRow">
                                    <td>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr1">
                                    <td>
                                        <webControls:ExLabel ID="ExLabel2" LabelID="1253" FormatString="{0}" runat="server"
                                            SkinID="BlueBold11ArialUnderline"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr class="BlankRow">
                                    <td>
                                    </td>
                                </tr>
                                <tr runat="server" id="rowCompanyWideMateriality">
                                    <td>
                                        <webControls:ExLabel ID="lblCompanyWideMateriality" LabelID="1070" FormatString="{0}:"
                                            runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                    </td>
                                    <td align="right">
                                        <webControls:ExLabel ID="lblCompanyWideMaterialityValue" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr class="BlankRow">
                                    <td>
                                    </td>
                                </tr>
                                <tr id="trNoMateriality" runat="server">
                                    <td colspan="2">
                                        <webControls:ExLabel ID="lblMateriality" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <telerikWebControls:ExRadGrid ID="rdFSCaptionwideMateriality" runat="server" EntityNameLabelID="1422"
                                            AllowSorting="false" OnItemDataBound="rdFSCaptionwideMateriality_ItemDataBound" >
                                            <MasterTableView DataKeyNames="FSCaptionID">
                                                <Columns>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1337" SortExpression="FSCaption"
                                                        HeaderStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblFSCaptionName" runat="server" FormatString="{0}:" SkinID="Black11Arial" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1259 " SortExpression="MaterialityThreshold">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblFSCaptionValueDetail" runat="server" SkinID="Black11Arial" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                            <%-- <ClientSettings>
                                                        <Scrolling AllowScroll="true" SaveScrollPosition="true" ScrollHeight="400" />
                                                    </ClientSettings>--%>
                                        </telerikWebControls:ExRadGrid>
                                    </td>
                                </tr>
                                <tr class="BlankRow">
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
