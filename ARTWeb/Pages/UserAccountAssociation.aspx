<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Pages_UserAccountAssociation" MasterPageFile="~/MasterPages/ARTMasterPage.master"
    Theme="SkyStemBlueBrown" Codebehind="UserAccountAssociation.aspx.cs" %>

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
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td colspan="4">
                                <UserControl:SkyStemARTGrid ID="ucSkyStemARTAccountOwnershipGrid" runat="server"
                                    OnGridItemDataBound="ucSkyStemARTAccountOwnershipGrid_GridItemDataBound" Grid-AllowExportToExcel="True" Grid-AllowExportToPDF="True"  >
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
                                <webControls:ExButton ID="btnSave" runat="server" LabelID="1315 " OnClick="btnSave_OnClick"
                                    Visible="false" SkinID="ExButton100" />&nbsp;
                                <webControls:ExButton ID="btnDelete" runat="server" LabelID="1564" OnClick="btnDelete_OnClick"
                                    Visible="false" SkinID="ExButton100" />
                                <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClick="btnCancel_Click"
                                    CausesValidation="false" SkinID="ExButton100" />
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlHeader" runat="server" Width="100%">
                    <table class="InputRequrementsHeading" width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="2%">
                                &nbsp;
                            </td>
                            <td width="20%">
                                <webControls:ExLabel ID="lblInputRequirements" runat="server" LabelID="1356" SkinID="Black11Arial" />
                            </td>
                            <td width="20%">
                                &nbsp;
                            </td>
                            <td width="10%">
                                &nbsp;
                            </td>
                            <td width="20%">
                                &nbsp;
                            </td>
                            <td width="28%" align="right">
                                <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlContent" runat="server" Width="100%">
                    <table width="100%" class="InputRequrementsTextNoBackColor" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="2%">
                                &nbsp;
                            </td>
                            <td width="20%">
                                &nbsp;
                            </td>
                            <td width="20%">
                                &nbsp;
                            </td>
                            <td width="10%">
                                &nbsp;
                            </td>
                            <td width="20%">
                                &nbsp;
                            </td>
                            <td width="28%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
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
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
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
                            <td>
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
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
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                        <tr>
                            <td colspan="5">
                                &nbsp;
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
                                <UserControl:SkyStemARTGrid ID="ucSkyStemARTGrid" runat="server" OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound" >
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
                            <td colspan="6" align="right">
                                &nbsp;<webControls:ExButton ID="btnAdd" runat="server" LabelID="1560" OnClick="btnAdd_OnClick"
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
            </asp:Panel>
            <asp:Panel ID="pnlADDUsers" runat="server" Visible="false" Width="100%">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
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
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:CollapsiblePanelExtender ID="cpeInputRequirements" TargetControlID="pnlContent"
                ImageControlID="imgCollapse" CollapseControlID="pnlHeader" ExpandControlID="pnlHeader"
                runat="server" SkinID="CollapsiblePanel">
            </ajaxToolkit:CollapsiblePanelExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    
     <script language="javascript" type="text/javascript">

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
