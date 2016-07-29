<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Pages_AddUserMailOnDataImport" Theme="SkyStemBlueBrown" MasterPageFile="~/MasterPages/PopUpMasterPage.master" Codebehind="AddUserMailOnDataImport.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="padding-left: 10px; padding-right: 10px">
                <%--User Search Criteria--%>
                <table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col width="15%" />
                        <col width="35%" />
                        <col width="15%" />
                        <col width="35%" />
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
                            <td>
                            </td>
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
                            <td>
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td>
                            </td>
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
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <telerikWebControls:ExRadGrid ID="rgUserList" runat="server" EntityNameLabelID="1229"
                    AllowPaging="true" AllowSorting="false" OnItemDataBound="rgUserList_ItemDataBound"
                    AllowMultiRowSelection="true" OnNeedDataSource="rgUserList_NeedDataSource" ClientSettings-Selecting-AllowRowSelect="true">
                    <MasterTableView ClientDataKeyNames="EmailID,UserID,FirstName,LastName" DataKeyNames="UserID">
                        <Columns>
                            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" HeaderStyle-Width="10%" />
                            <telerikWebControls:ExGridTemplateColumn LabelID="1267" SortExpression="FirstName"
                                HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <webControls:ExHyperLink ID="hlFirstName" runat="server" SkinID="GridHyperLink" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1268" SortExpression="LastName"
                                HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <webControls:ExHyperLink ID="hlLastName" runat="server" SkinID="GridHyperLink" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1311" HeaderStyle-Width="15%" SortExpression="EmailID">
                                <ItemTemplate>
                                    <webControls:ExHyperLink ID="hlEmailID" runat="server" SkinID="GridHyperLink" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1269" HeaderStyle-Width="15%" SortExpression="LoginID">
                                <ItemTemplate>
                                    <webControls:ExHyperLink ID="hlLoginID" runat="server" SkinID="GridHyperLink" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1271" HeaderStyle-Width="15%" SortExpression="JobTitle">
                                <ItemTemplate>
                                    <webControls:ExHyperLink ID="hlJobTitle" runat="server" SkinID="GridHyperLink" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1272" HeaderStyle-Width="15%" SortExpression="WorkPhone">
                                <ItemTemplate>
                                    <webControls:ExHyperLink ID="hlWorkPhone" runat="server" SkinID="GridHyperLink" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1273" HeaderStyle-Width="15%" SortExpression="Phone">
                                <ItemTemplate>
                                    <webControls:ExHyperLink ID="hlPhone" runat="server" SkinID="GridHyperLink" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerikWebControls:ExRadGrid>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right">
                <webControls:ExButton ID="AddUsers" runat="server" LabelID="1737" Visible="false" />
                <asp:HiddenField ID="AddUsersList" runat="server" />
                <webControls:ExButton ID="btnOK" runat="server" LabelID="1742" Visible="false" SkinID="ExButton100"
                    OnClientClick="javascript:SubmitAndClose(); return false;" />
                <asp:HiddenField ID="hdnTaskUserRole" runat="server" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table class="LegendTable" id="tblLegend" runat="server">
                    <tr>
                        <td class="LegendHeading" colspan="2">
                            <webControls:ExLabel ID="lblHeading" FormatString="{0}:" runat="server" LabelID="1383"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <webControls:ExImage ID="ExImage2" runat="server" LabelID="1534" SkinID="InactiveIcon" />
                            &nbsp;
                            <webControls:ExLabel ID="ExLabel4" runat="server" LabelID="1534" SkinID="LegendLabel"></webControls:ExLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript" language="javascript">
        function SubmitAndClose() {
            var AddUsersIDList = "";
            var AddUsersNameList = "";
            var hdnTaskUserRole = $get('<%= hdnTaskUserRole.ClientID %>');
            var TaskUserRoleVal = "";
            if (hdnTaskUserRole != null && grid != 'undefined')
                TaskUserRoleVal = hdnTaskUserRole.value;
            var grid = $find('<%= rgUserList.ClientID %>');
            if (grid != null && grid != 'undefined') {
                var MasterTable = grid.get_masterTableView();
                var selectedRows = MasterTable.get_selectedItems();
                for (var i = 0; i < selectedRows.length; i++) {
                    var row = selectedRows[i];
                    var cell = row.getDataKeyValue("UserID");
                    var cellName = row.getDataKeyValue("FirstName") + " " + row.getDataKeyValue("LastName");
                    if (i >= 1) {
                        AddUsersIDList = AddUsersIDList + ",";
                        AddUsersNameList = AddUsersNameList + "; ";
                    }
                    AddUsersIDList = AddUsersIDList + cell;
                    AddUsersNameList = AddUsersNameList + cellName;
                }
            }
            //            alert(AddUsersListHidden);
            if (Page_IsValid) {
                var wnd1 = GetRadWindow();
                var mgr = wnd1.get_windowManager();
                var wnd2 = mgr.getWindowByName("EditAddTaskWindow");
                if (wnd2 == null) {
                    wnd2 = mgr.getWindowByName("BulkEditTasks");
                }
                if (wnd2 != null) {
                    wnd2.get_contentFrame().contentWindow.setUsersInfo(AddUsersIDList, AddUsersNameList, TaskUserRoleVal);
                    wnd1.close();
                }
            }

        }
    </script>

</asp:Content>
