<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Pages_UserAccountAssociation" MasterPageFile="~/MasterPages/ARTMasterPage.master"
    Theme="SkyStemBlueBrown" CodeBehind="UserAccountAssociation.aspx.cs" %>

<%@ Register Src="~/UserControls/OrganizationalHierarchyDropdown.ascx" TagName="OrganizationalHierarchyDropdown"
    TagPrefix="UserControl" %>
<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<%@ Register Src="~/UserControls/AccountSearchControl.ascx" TagName="AccountSearchControl"
    TagPrefix="UserControl" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnlAccountProfile" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlHidePage" runat="server" Width="100%">
                <asp:Panel ID="pnlSaveData" runat="server" Width="100%">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="padding-left: 2px">
                                <webControls:ExLabel ID="lblFirstName" LabelID="1267" FormatString="{0}:" runat="server"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblFirstNameValue" runat="server" SkinID="Black11ArialNormal" />
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblLastName" LabelID="1268" FormatString="{0}:" runat="server"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblLastNameValue" runat="server" SkinID="Black11ArialNormal" />
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td style="padding-left: 2px">
                                <webControls:ExLabel ID="LblLoginID" LabelID="1269" FormatString="{0}:" runat="server"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExLabel ID="LblLoginIDValue" runat="server" SkinID="Black11ArialNormal" />
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblEmailID" LabelID="1270" FormatString="{0}:" runat="server"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblEmailIDValue" runat="server" SkinID="Black11ArialNormal" />
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr id="trUser" runat="server">
                            <td colspan="4" class="SubSectionHeading">
                                <webControls:ExLabel ID="lblExistingAssociation" runat="server" LabelID="1639" SkinID="SubSectionHeading" />
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <webControls:ExLabel ID="ExLabel1" LabelID="2175" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                &nbsp; &nbsp;
                                <asp:DropDownList ID="ddlRole" runat="server" SkinID="DropDownList200" EnableViewState="true"
                                    OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td>
                                <webControls:ExLabel ID="lblAllAccounts" LabelID="2176" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="optAllAccountYes" runat="server" GroupName="AllAccounts" LabelID="1252" SkinID="OptBlack11Arial"/>
                                <webControls:ExRadioButton ID="optAllAccountNo" runat="server" Checked="true" GroupName="AllAccounts" LabelID="1251" SkinID="OptBlack11Arial" />
                                <webControls:ExCustomValidator ID="cvAllAccounts" runat="server" ClientValidationFunction="validateAllAccounts">!</webControls:ExCustomValidator>
                            </td>
                            <td colspan="2"></td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table id="tblAssociationDisplay" width="100%" cellpadding="0" cellspacing="0" runat="server">
                                    <tr>
                                        <td colspan="4">
                                            <UserControl:SkyStemARTGrid ID="ucSkyStemARTAccountOwnershipGrid" runat="server" grid-width="100%" 
                                                OnGridItemDataBound="ucSkyStemARTAccountOwnershipGrid_GridItemDataBound" Grid-AllowExportToExcel="True" Grid-AllowExportToPDF="True">
                                                <SkyStemGridColumnCollection>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1357" HeaderStyle-Width="40%" SortExpression="AccountNumber"
                                                        DataType="System.String">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblAccountNumberOwnershipGrid" runat="server"></webControls:ExLabel>
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1346" HeaderStyle-Width="40%" SortExpression="AccountName"
                                                        DataType="System.String">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblAccountNameOwnershipGrid" runat="server"></webControls:ExLabel>
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                </SkyStemGridColumnCollection>
                                            </UserControl:SkyStemARTGrid>
                                            <%--   <asp:Panel ID="pnlOwnershipGrid" runat="server" SkinID="RadGridScrollPanel">
                                    <UserControl:SkyStemARTGrid ID="ucSkyStemARTOwnershipGridForAccount" runat="server"
                                        OnGridItemDataBound="ucSkyStemARTOwnershipGridForAccount_GridItemDataBound">
                                        <SkyStemGridColumnCollection>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1357" HeaderStyle-Width="40%" SortExpression="AccountNumber"
                                                DataType="System.String">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblAccountNumber" runat="server"></webControls:ExLabel>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1346" HeaderStyle-Width="40%" SortExpression="AccountName"
                                                DataType="System.String">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblAccountName" runat="server"></webControls:ExLabel>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                        </SkyStemGridColumnCollection>
                                    </UserControl:SkyStemARTGrid>
                                </asp:Panel>--%>
                                        </td>
                                    </tr>
                                    <tr class="BlankRow">
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="right">
                                            <webControls:ExButton ID="btnDelete" runat="server" LabelID="1564" OnClick="btnDelete_OnClick"
                                                Visible="false" SkinID="ExButton100" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table id="tblUserRoleAssociationDisplay" width="100%" cellpadding="0" cellspacing="0" runat="server">
                                    <tr>
                                        <td colspan="4">
                                            <telerikWebControls:ExRadGrid ID="rgUserRoleSelected" runat="server" EntityNameLabelID="1341"
                                                AllowPaging="false" AllowSorting="true" OnItemDataBound="rgUserRoleSelected_GridItemDataBound"
                                                AllowMultiRowSelection="true" AllowExportToExcel="true" AutoGenerateColumns="false" 
                                                OnItemCreated="rgUserRoleSelected_ItemCreated" OnNeedDataSource="rgUserRoleSelected_NeedDataSource"
                                                AllowExportToPDF="true" AllowPrint="false" AllowPrintAll="false" OnItemCommand="rgUserRoleSelected_ItemCommand"
                                                AllowCustomPaging="false">
                                                <MasterTableView ClientDataKeyNames="ChildUserID, ChildRoleID" >
                                                    <Columns>
                                                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" HeaderStyle-Width="5%" />
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1267" SortExpression="FirstName"
                                                            HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblAddedGridFirstName" runat="server" SkinID="Black11ArialNormal" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1268" SortExpression="LastName"
                                                            HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblAddedGridLastName" runat="server" SkinID="Black11ArialNormal" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1269" HeaderStyle-Width="20%" SortExpression="LoginID">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblAddedGridLoginID" runat="server" SkinID="Black11ArialNormal" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1278" HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblAddedGridRole" runat="server" SkinID="Black11ArialNormal" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1311" HeaderStyle-Width="20%" SortExpression="EmailID">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblAddedGridEmailID" runat="server" SkinID="Black11ArialNormal" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerikWebControls:ExRadGrid>
                                        </td>
                                    </tr>
                                    <tr class="BlankRow">
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="right">
                                            <webControls:ExButton ID="btnDeleteUser" runat="server" LabelID="1564" OnClick="btnDeleteUser_OnClick"
                                                Visible="false" SkinID="ExButton100" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                <webControls:ExButton ID="btnSave" runat="server" LabelID="1315 " OnClick="btnSave_OnClick"
                                    Visible="false" SkinID="ExButton100" />
                                <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClick="btnCancel_Click"
                                    CausesValidation="false" SkinID="ExButton100" />
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlAccountSearchHeader" runat="server" Width="100%">
                    <table class="InputRequrementsHeading" width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="2%">&nbsp;
                            </td>
                            <td width="20%">
                                <webControls:ExLabel ID="lblInputRequirementsAccountSearch" runat="server" LabelID="1356" SkinID="Black11Arial" />
                            </td>
                            <td width="20%">&nbsp;
                            </td>
                            <td width="10%">&nbsp;
                            </td>
                            <td width="20%">&nbsp;
                            </td>
                            <td width="28%" align="right">
                                <webControls:ExImage ID="imgCollapseAccountSearch" runat="server" SkinID="CollapseIcon" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlAccountSearchContent" runat="server" Width="100%">
                    <table width="100%" class="InputRequrementsTextNoBackColor" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="2%">&nbsp;
                            </td>
                            <td width="20%">&nbsp;
                            </td>
                            <td width="20%">&nbsp;
                            </td>
                            <td width="10%">&nbsp;
                            </td>
                            <td width="20%">&nbsp;
                            </td>
                            <td width="28%">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblOrganizationalHiearachy" LabelID="1596" runat="server"
                                    SkinID="Black11Arial"></webControls:ExLabel>:
                            </td>
                            <td>
                                <UserControl:OrganizationalHierarchyDropdown ID="ucOrganizationalHierarchyDropdown"
                                    runat="server" />
                            </td>
                            <td>
                                <asp:TextBox Width="100" MaxLength="250" ID="txtOrganizationalHiearachy" runat="server" />
                            </td>
                            <%--  <td>
                                <asp:DropDownList AutoPostBack="true" ID="ddlUserAccountAssocition" runat="server"
                                    Width="80%" OnSelectedIndexChanged="ddlUserAccountAssocition_OnSelectedIndexChanged">
                                    <asp:ListItem Text="Region" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Entity" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Account" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>--%>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblAccount" LabelID="1712" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                &nbsp;
                                <webControls:ExLabel ID="lblFromAcNu" LabelID="1336" runat="server" FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExTextBox ID="txtAcNumber" runat="server" SkinID="ExTextBox150" MaxLength="20" />
                            </td>
                            <td style="padding-left: 1cm">
                                <webControls:ExLabel ID="lblToAcNu" LabelID="1345" runat="server" FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExTextBox ID="txtToAcNumber" runat="server" SkinID="ExTextBox150" MaxLength="20" />
                            </td>
                            <td></td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblFsCaption" LabelID="1337" runat="server" FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFsCaption" runat="server" SkinID="ExTextBox150" MaxLength="50" />
                                <img id="imgFSCaptionProgress" style="visibility: hidden" alt="imgProgress" src="../App_Themes/SkyStemBlueBrown/Images/progress_small.gif" />
                                <ajaxToolkit:AutoCompleteExtender TargetControlID="txtFsCaption" ServiceMethod="AutoCompleteFSCaption"
                                    runat="server" ID="aceFSCaption" OnClientPopulating="ShowFSCaptionProgressIcon"
                                    OnClientPopulated="ShowFSCaptionProgressIcon">
                                </ajaxToolkit:AutoCompleteExtender>
                            </td>
                            <td style="padding-left: 1cm">
                                <webControls:ExLabel ID="lblAccname" runat="server" LabelID="1346" FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExTextBox ID="txtAcName" runat="server" SkinID="ExTextBox150" MaxLength="100" />
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td colspan="5">&nbsp;
                            </td>
                            <td align="right">
                                <webControls:ExButton ID="btnSearch" runat="server" LabelID="1340 " OnClick="btnSearch_OnClick"
                                    SkinID="ExButton100" />&nbsp;
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td colspan="6">
                                <UserControl:SkyStemARTGrid ID="ucSkyStemARTGrid" runat="server" OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound">
                                    <SkyStemGridColumnCollection>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1357" HeaderStyle-Width="10%" SortExpression="AccountNumber"
                                            DataType="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblAccountNumber" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1346" HeaderStyle-Width="15%" SortExpression="AccountName"
                                            DataType="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblAccountName" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </SkyStemGridColumnCollection>
                                </UserControl:SkyStemARTGrid>
                                &nbsp;
                                <%--<asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">--%>
                                <%-- <UserControl:SkyStemARTGrid ID="ucSkyStemARTGridForAccounts" runat="server" OnGridItemDataBound="ucSkyStemARTGridForAccounts_GridItemDataBound">
                                        <SkyStemGridColumnCollection>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1357" HeaderStyle-Width="10%" SortExpression="AccountNumber"
                                                DataType="System.String">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblAccountNumber" runat="server"></webControls:ExLabel>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1346" HeaderStyle-Width="15%" SortExpression="AccountName"
                                                DataType="System.String">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblAccountName" runat="server"></webControls:ExLabel>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                        </SkyStemGridColumnCollection>
                                    </UserControl:SkyStemARTGrid>--%>
                                <%--</asp:Panel>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="right">&nbsp;<webControls:ExButton ID="btnAdd" runat="server" LabelID="1560" OnClick="btnAdd_OnClick"
                                Visible="false" SkinID="ExButton100" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <UserControls:ProgressBar ID="ProgressBar1" runat="server" AssociatedUpdatePanelID="upnlAccountProfile" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpeAccountSearch" TargetControlID="pnlAccountSearchContent"
                    ImageControlID="imgCollapseAccountSearch" CollapseControlID="pnlAccountSearchHeader" ExpandControlID="pnlAccountSearchHeader"
                    runat="server" SkinID="CollapsiblePanel">
                </ajaxToolkit:CollapsiblePanelExtender>
                <br />
                <asp:Panel ID="pnlUserSearchHeader" runat="server" Width="100%">
                    <table class="InputRequrementsHeading" width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="2%">&nbsp;
                            </td>
                            <td width="20%">
                                <webControls:ExLabel ID="lblInputRequirementUserSearch" runat="server" LabelID="1737" SkinID="Black11Arial" />
                            </td>
                            <td width="20%">&nbsp;
                            </td>
                            <td width="10%">&nbsp;
                            </td>
                            <td width="20%">&nbsp;
                            </td>
                            <td width="28%" align="right">
                                <webControls:ExImage ID="imgCollapseUserSearch" runat="server" SkinID="CollapseIcon" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlUserSearchContent" runat="server" Width="100%">
                    <table width="100%" class="InputRequrementsTextNoBackColor" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="2%">&nbsp;
                            </td>
                            <td width="20%">&nbsp;
                            </td>
                            <td width="20%">&nbsp;
                            </td>
                            <td width="10%">&nbsp;
                            </td>
                            <td width="20%">&nbsp;
                            </td>
                            <td width="28%">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblUSFirstName" runat="server" FormatString="{0}:" LabelID="1267"
                                    SkinID="Black11Arial">
                                </webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExTextBox ID="txtFirstName" runat="server" SkinID="ExTextBox150" />
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblUSLastName" runat="server" FormatString="{0}:" LabelID="1268"
                                    SkinID="Black11Arial">
                                </webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExTextBox ID="txtLastName" runat="server" SkinID="ExTextBox150" />
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblEmail" runat="server" FormatString="{0}:" LabelID="1270"
                                    SkinID="Black11Arial">
                                </webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExTextBox ID="txtEmail" runat="server" SkinID="ExTextBox150" />
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblRole" runat="server" FormatString="{0}:" LabelID="1278"
                                    SkinID="Black11Arial">
                                </webControls:ExLabel>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRoleUserSearch" runat="server" SkinID="DropDownList150">
                                </asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td colspan="5">&nbsp;
                            </td>
                            <td align="right">
                                <webControls:ExButton ID="btnUserSearch" runat="server" LabelID="1340 " OnClick="btnUserSearch_OnClick"
                                    SkinID="ExButton100" />&nbsp;
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td colspan="6">
                                <telerikWebControls:ExRadGrid ID="rgUserSearchList" runat="server" EntityNameLabelID="1341"
                                    AllowPaging="false" AllowSorting="true" OnItemDataBound="rgUserSearchGrid_GridItemDataBound"
                                    AllowMultiRowSelection="true" AllowExportToExcel="false" OnSortCommand="rgUserSearchList_SortCommand"
                                    OnNeedDataSource="rgUserSearchList_NeedDataSource" AllowExportToPDF="false" AllowPrint="false" AllowPrintAll="false"
                                    AllowCustomPaging="false" AutoGenerateColumns="false">
                                    <MasterTableView ClientDataKeyNames="ChildUserID, ChildRoleID" TableLayout="Auto">
                                        <Columns>
                                            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" HeaderStyle-Width="5%" />
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1267" SortExpression="FirstName"
                                                HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblSearchGridFirstName" runat="server" SkinID="Black11ArialNormal" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1268" SortExpression="LastName"
                                                HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblSearchGridLastName" runat="server" SkinID="Black11ArialNormal" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1269" HeaderStyle-Width="20%" SortExpression="LoginID">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblSearchGridLoginID" runat="server" SkinID="Black11ArialNormal" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1278" HeaderStyle-Width="15%">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblSearchGridRole" runat="server" SkinID="Black11ArialNormal" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1311" HeaderStyle-Width="20%" SortExpression="EmailID">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblSearchGridEmailID" runat="server" SkinID="Black11ArialNormal" />
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerikWebControls:ExRadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="right">&nbsp;<webControls:ExButton ID="btnAddUserRole" runat="server" LabelID="1560" OnClick="btnAddUserRole_OnClick"
                                Visible="false" SkinID="ExButton100" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>
            <ajaxToolkit:CollapsiblePanelExtender ID="cpeUserSearch" TargetControlID="pnlUserSearchContent"
                ImageControlID="imgCollapseUserSearch" CollapseControlID="pnlUserSearchHeader" ExpandControlID="pnlUserSearchHeader"
                runat="server" SkinID="CollapsiblePanel">
            </ajaxToolkit:CollapsiblePanelExtender>
            <asp:Panel ID="pnlADDUsers" runat="server" Visible="false" Width="100%">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <webControls:ExButton ID="btnAddMore" runat="server" LabelID="1531" OnClick="btnAddMore_Click"
                                CausesValidation="false" SkinID="ExButton200" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <webControls:ExButton ID="btnHome" runat="server" LabelID="1532" OnClick="btnHome_Click"
                                CausesValidation="false" SkinID="ExButton200" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">
<%--        function ShowHideAssociation() {
            var rb1 = document.getElementById('<%=this.optAllAccountYes.ClientID %>');
            var rb2 = document.getElementById('<%=this.optAllAccountNo.ClientID %>');
            var tblAssociationDisplay = document.getElementById('<%=this.tblAssociationDisplay.ClientID %>');
            var tblUserRoleAssociationDisplay = document.getElementById('<%=this.tblUserRoleAssociationDisplay.ClientID %>');
            var pnlAccountSearchHeader = document.getElementById('<%=this.pnlAccountSearchHeader.ClientID %>');
            var pnlAccountSearchContent = document.getElementById('<%=this.pnlAccountSearchContent.ClientID %>');
            var pnlUserSearchHeader = document.getElementById('<%=this.pnlUserSearchHeader.ClientID %>');
            var pnlUserSearchContent = document.getElementById('<%=this.pnlUserSearchContent.ClientID %>');
            if ((rb1 != null) && (rb1 != null)) {
                if (rb1.checked == true) {
                    $(tblAssociationDisplay).hide();
                    $(tblUserRoleAssociationDisplay).hide();
                    $(pnlAccountSearchHeader).hide();
                    $(pnlAccountSearchContent).hide();
                    $(pnlUserSearchHeader).hide();
                    $(pnlUserSearchContent).hide();
                }
                else {
                    $(tblAssociationDisplay).show();
                    $(tblUserRoleAssociationDisplay).show();
                    $(pnlAccountSearchHeader).show();
                    $(pnlAccountSearchContent).show();
                    $(pnlUserSearchHeader).show();
                    $(pnlUserSearchContent).show();
                }
            }
            return true;
        }--%>

        function validateAllAccounts(source, args) {
            var groupValue = false;
            var rb1 = document.getElementById('<%=this.optAllAccountYes.ClientID %>');
            var rb2 = document.getElementById('<%=this.optAllAccountNo.ClientID %>');
            if ((rb1 != null) && (rb1 != null)) {
                if ((rb1.checked == false) && (rb2.checked == false)) {
                    args.IsValid = false;
                }
                else
                    args.IsValid = true;
            }
        }
        function ConfirmDeleteUserAccountAssociation(msg) {
            var answer = confirm(msg);
            if (answer) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
