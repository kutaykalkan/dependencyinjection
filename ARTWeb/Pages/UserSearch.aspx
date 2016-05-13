<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true"
    CodeFile="UserSearch.aspx.cs" Inherits="Pages_UserSearch" Title="Untitled Page"
    Theme="SkyStemBlueBrown" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnlUserSearch" runat="server">
        <ContentTemplate>
            <script type="text/javascript">

                function toggleDropDownList(source) {
                    document.getElementById('<%= DDLActHist.ClientID %>').disabled = !source.checked;
                }

            </script>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="padding-left: 10px; padding-right: 10px">
                        <%--User Search Criteria--%>
                        <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                            <colgroup>
                                <col width="15%" />
                                <col width="35%" />
                                <col width="15%" />
                                <col width="40%" />
                                <tr>
                                    <td>
                                        <webControls:ExLabel ID="lblFirstName" runat="server" FormatString="{0}:" LabelID="1267"
                                            SkinID="Black11Arial">
                                        </webControls:ExLabel>
                                    </td>
                                    <td>
                                        <webControls:ExTextBox ID="txtFirstName" runat="server" SkinID="ExTextBox200" />
                                    </td>
                                    <td>
                                        <webControls:ExLabel ID="lblLastName" runat="server" FormatString="{0}:" LabelID="1268"
                                            SkinID="Black11Arial">
                                        </webControls:ExLabel>
                                    </td>
                                    <td>
                                        <webControls:ExTextBox ID="txtLastName" runat="server" SkinID="ExTextBox200" />
                                    </td>
                                </tr>
                                <tr class="BlankRow">
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <webControls:ExLabel ID="lblEmail" runat="server" FormatString="{0}:" LabelID="1270"
                                            SkinID="Black11Arial">
                                        </webControls:ExLabel>
                                    </td>
                                    <td>
                                        <webControls:ExTextBox ID="txtEmail" runat="server" SkinID="ExTextBox200" />
                                    </td>
                                    <td>
                                        <webControls:ExLabel ID="lblRole" runat="server" FormatString="{0}:" LabelID="1278"
                                            SkinID="Black11Arial">
                                        </webControls:ExLabel>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRole" runat="server" SkinID="DropDownList200">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="BlankRow">
                                    <td></td>
                                </tr>
                                <tr id="rowHideOnAddUser" runat="server">
                                    <td>
                                        <webControls:ExLabel ID="lblStatus" runat="server" FormatString="{0}:" LabelID="1338"
                                            SkinID="Black11Arial">
                                        </webControls:ExLabel>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlStatus" runat="server" SkinID="DropDownList200">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <webControls:ExLabel ID="lblActivationHistory" runat="server" FormatString="{0}:" LabelID="2803"
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
                                        <webControls:ExButton ID="btnSearch" runat="server" LabelID="2912" OnClick="btnSearch_Click" />
                                        <webControls:ExButton ID="btnSearchFTP" runat="server" LabelID="2913" OnClick="btnSearchFTP_Click" />
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
                        <asp:Panel ID="pnlUserGrid" runat="server" SkinID="RadGridScrollPanel">
                            <telerikWebControls:ExRadGrid ID="rgUserList" runat="server" EntityNameLabelID="1341"
                                AllowPaging="true" AllowSorting="true" OnItemDataBound="rgUserList_ItemDataBound"
                                OnItemCommand="rgUserList_ItemCommand" AllowMultiRowSelection="true" AllowExportToExcel="true"
                                AllowExportToPDF="true" AllowPrint="true" AllowPrintAll="true" OnSortCommand="rgUserList_SortCommand"
                                OnNeedDataSource="rgUserList_NeedDataSource" OnItemCreated="rgUserList_ItemCreated"
                                AllowCustomPaging="true" Width="1600px">
                                <MasterTableView ClientDataKeyNames="EmailID" TableLayout="Auto">
                                    <Columns>
                                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" HeaderStyle-Width="5%" />
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1267" SortExpression="FirstName"
                                            HeaderStyle-Width="14%">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlFirstName" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1268" SortExpression="LastName"
                                            HeaderStyle-Width="14%">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlLastName" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1269" HeaderStyle-Width="14%" SortExpression="LoginID">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlLoginID" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1278" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <%--<webControls:ExLabel ID="lblDefaultRole" runat="server" SkinID="Black11Arial" />--%>
                                                <webControls:ExLabel ID="lblRole" runat="server" SkinID="Black11ArialNormal" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1311" HeaderStyle-Width="16%" SortExpression="EmailID">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlEmailID" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>

                                        <telerikWebControls:ExGridTemplateColumn LabelID="1399" HeaderStyle-Width="8%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblDate" runat="server" SkinID="Black11ArialNormal" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2803" SortExpression="UserStatus" HeaderStyle-Width="8%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblStatus" runat="server" SkinID="Black11ArialNormal" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2801" SortExpression="AddedByUserName" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblBy" runat="server" SkinID="Black11ArialNormal" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2556" SortExpression="AddedBy" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblCreatedBy" runat="server" SkinID="Black11ArialNormal" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2557" SortExpression="DateAdded" HeaderStyle-Width="6%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblDateCreated" runat="server" SkinID="Black11ArialNormal" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2526">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlIsActive" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2939">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlIsUserLocked" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2940">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlLockdownCount" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="FTPDate" LabelID="2907" HeaderStyle-Width="8%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblFTPDate" runat="server" SkinID="Black11ArialNormal" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="FTPActive" LabelID="2910">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlIsFTPActive" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="IconColumn">
                                            <ItemTemplate>
                                                <webControls:ExImageButton LabelID="1536" ID="imgBtnResetPassword" runat="server"
                                                    CommandName="ResetPassword" SkinID="ResetPassword" CssClass="DeleteButton" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerikWebControls:ExRadGrid>
                        </asp:Panel>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4" align="right">
                        <webControls:ExButton ID="AddUsers" runat="server" LabelID="1737" Visible="false" />
                        <asp:HiddenField ID="AddUsersList" runat="server" />
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <telerikWebControls:ExRadGrid ID="rgFTP" runat="server" EntityNameLabelID="1341"
                            AllowPaging="true" AllowSorting="true" OnItemDataBound="rgFTP_ItemDataBound"
                            OnItemCommand="rgFTP_ItemCommand" AllowMultiRowSelection="true" AllowExportToExcel="true"
                            AllowExportToPDF="true" OnSortCommand="rgFTP_SortCommand" OnItemCreated="rgFTP_ItemCreated"
                            OnNeedDataSource="rgFTP_NeedDataSource" AllowCustomPaging="true">
                            <ClientSettings>
                                <Selecting AllowRowSelect="True"></Selecting>
                            </ClientSettings>
                            <MasterTableView ClientDataKeyNames="EmailID">
                                <Columns>
                                    <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" HeaderStyle-Width="5%" />
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1267" SortExpression="FirstName"
                                        HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlFirstName" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1268" SortExpression="LastName"
                                        HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlLastName" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1269" HeaderStyle-Width="10%" SortExpression="LoginID">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlLoginID" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1278" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblRole" runat="server" SkinID="Black11ArialNormal" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1311" HeaderStyle-Width="10%" SortExpression="EmailID">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlEmailID" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>

                                    <telerikWebControls:ExGridTemplateColumn LabelID="2907" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblDate" runat="server" SkinID="Black11ArialNormal" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2908" SortExpression="FTPActivationStatusId" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblActivationHistory" runat="server" SkinID="Black11ArialNormal" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2909" SortExpression="AddedByUserName" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblFTPBy" runat="server" SkinID="Black11ArialNormal" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2910">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlIsActive" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="IconColumn" LabelID="2911">
                                        <ItemTemplate>
                                            <webControls:ExImageButton LabelID="1536" ID="imgBtnResetPassword" runat="server"
                                                CommandName="ResetFTPPassword" SkinID="ResetPassword" CssClass="DeleteButton" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerikWebControls:ExRadGrid>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td></td>
                </tr>
                <tr class="BlankRow">
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table class="LegendTable" width="100%" id="tblLegend" runat="server">
                            <tr>
                                <td class="LegendHeading" colspan="3">
                                    <webControls:ExLabel ID="lblHeading" FormatString="{0}:" runat="server" LabelID="1383"></webControls:ExLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <webControls:ExImage ID="ExImage2" runat="server" LabelID="1534" SkinID="InactiveIcon" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel4" runat="server" LabelID="1534" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>
                                <td>
                                    <webControls:ExLabel ID="ExLabel2" runat="server" LabelID="2802" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>
                                <td>
                                    <webControls:ExImage ID="ExImage1" runat="server" SkinID="ResetPassword" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="1536" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <UserControls:ProgressBar ID="upUserSearch" runat="server" EnableTheming="true" AssociatedUpdatePanelID="upnlUserSearch"
                            Visible="true" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
