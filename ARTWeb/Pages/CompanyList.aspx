<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true"
    CodeFile="CompanyList.aspx.cs" Inherits="Pages_CompanyList" Theme="SkyStemBlueBrown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function toggleDropDownList(source) {
            document.getElementById('<%= DDLActHist.ClientID %>').disabled = !source.checked;
        }
    </script>
    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="padding-left: 10px; padding-right: 10px">
                <%--Company Search Criteria--%>
                <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col width="15%" />
                        <col width="35%" />
                        <col width="15%" />
                        <col width="35%" />
                        <tr>
                            <td>
                                <webControls:ExLabel ID="lblCompanyName" runat="server" FormatString="{0}:" LabelID="1320"
                                    SkinID="Black11Arial">
                                </webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExTextBox ID="txtCompanyName" runat="server" SkinID="ExTextBox200" />
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblDisplayName" runat="server" FormatString="{0}:" LabelID="1288"
                                    SkinID="Black11Arial">
                                </webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExTextBox ID="txtDisplayName" runat="server" SkinID="ExTextBox200" />
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <webControls:ExLabel ID="lblCompanyStatus" runat="server" FormatString="{0}:" LabelID="1338"
                                    SkinID="Black11Arial">
                                </webControls:ExLabel>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCompanyStatus" runat="server" SkinID="DropDownList200">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <webControls:ExLabel ID="lblFTPStatus" runat="server" FormatString="{0}:" LabelID="2915"
                                    SkinID="Black11Arial">
                                </webControls:ExLabel>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFTPStatus" runat="server" SkinID="DropDownList200">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblFTPActivationStatus" runat="server" FormatString="{0}:" LabelID="2908"
                                    SkinID="Black11Arial">
                                </webControls:ExLabel>
                            </td>
                            <td>
                                <asp:CheckBox ID="cbActHist" runat="server" onclick="toggleDropDownList(this);" />
                                <asp:DropDownList ID="DDLActHist" runat="server" SkinID="DropDownList175">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td></td>
                        </tr>
                        <tr>
                            <td align="right" colspan="4">
                                <webControls:ExButton ID="btnSearch" runat="server" LabelID="1340" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </colgroup>
                </table>
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td>
                <telerikWebControls:ExRadGrid ID="rgCompanyList" runat="server" EntityNameLabelID="1229"
                    OnItemDataBound="rgCompanyList_ItemDataBound"
                    OnNeedDataSource="rgCompanyList_NeedDataSource"
                    OnSortCommand="rgCompanyList_SortCommand"
                    OnItemCreated="rgCompanyList_ItemCreated" OnItemCommand="rgCompanyList_OnItemCommand"
                    AllowExportToExcel="true" AllowExportToPDF="true" AllowPrint="true" AllowPrintAll="true">
                    <MasterTableView AllowSorting="true" AllowPaging="true" DataKeyNames="CompanyID">
                        <Columns>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="IconColumn" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <webControls:ExImage ID="imgSubscriptionExpireMessage" runat="server" LabelID="5000020"
                                        SkinID="ExpireIcon" />
                                    <webControls:ExImage ID="imgSubscriptionWarningMessage" runat="server" LabelID="5000017"
                                        SkinID="WarningIcon" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1320" SortExpression="CompanyName" HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <webControls:ExHyperLink ID="hlCompanyName" runat="server" SkinID="GridHyperLink" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1288" SortExpression="DisplayName"
                                HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <webControls:ExHyperLink ID="hlDisplayName" runat="server" SkinID="GridHyperLink" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2936" SortExpression="CompanyAlias"
                                HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <webControls:ExHyperLink ID="hlCompanyAlias" runat="server" SkinID="GridHyperLink" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1292" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <webControls:ExHyperLink ID="hlAddress" runat="server" SkinID="GridHyperLink" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1321" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <webControls:ExHyperLink ID="hlSubscriptionPeriod" runat="server" SkinID="GridHyperLink" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1322" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <webControls:ExHyperLink ID="hlUsers" runat="server" SkinID="GridHyperLink" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1323" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <webControls:ExHyperLink ID="hlCapacity" runat="server" SkinID="GridHyperLink" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2907" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <webControls:ExLinkButton ID="lnkbtnFTPActivationDate" CommandName="DrilldownToUser" runat="server" SkinID="GridLinkButton" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2908" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <webControls:ExLinkButton ID="lnkbtnFTPActivationHistroy" CommandName="DrilldownToUser" runat="server" SkinID="GridLinkButton" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2905" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <webControls:ExLinkButton ID="lnkbtnFTPStatus" CommandName="DrilldownToUser" runat="server" SkinID="GridLinkButton" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerikWebControls:ExRadGrid>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table class="LegendTable">
        <tr>
            <td class="LegendHeading" colspan="4">
                <webControls:ExLabel ID="lblHeading" FormatString="{0}:" runat="server" LabelID="1383"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td>
                <webControls:ExImage ID="imgSubscriptionExpireLegend" runat="server" LabelID="5000020" SkinID="ExpireIcon" />
                &nbsp;
                <webControls:ExLabel runat="server" LabelID="5000020" SkinID="LegendLabel"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExImage ID="imgSubscriptionWarningLegend" runat="server" LabelID="5000017" SkinID="WarningIcon" />
                &nbsp;
                <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="5000017" SkinID="LegendLabel"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExImage ID="ExImage1" runat="server" LabelID="5000021" SkinID="LimitReachedIcon" />
                &nbsp;
                <webControls:ExLabel ID="ExLabel2" runat="server" LabelID="5000021" SkinID="LegendLabel"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExImage ID="ExImage2" runat="server" LabelID="1384" SkinID="InactiveIcon" />
                &nbsp;
                <webControls:ExLabel ID="ExLabel3" runat="server" LabelID="1384" SkinID="LegendLabel"></webControls:ExLabel>

            </td>
        </tr>
    </table>
</asp:Content>
