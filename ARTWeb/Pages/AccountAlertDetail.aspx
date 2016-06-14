<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true" Inherits="Pages_AccountAlertDetail" Title="Untitled Page"
    Theme="SkyStemBlueBrown" Codebehind="AccountAlertDetail.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <%--<tr>
            <td align="center">
                <table width="96%" border="0" cellpadding="0" cellspacing="0">
                    <td align="left">
                        <webControls:ExLabel ID="lblAlertType" runat="server" SkinID="SubSectionHeading"
                            LabelID="1010"></webControls:ExLabel>
                    </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>--%>
        
        <tr>
            <td align="center">
                <webControls:ExCheckBoxWithLabel ID="chkShowAlertMsg" runat="server" SkinID="CheckboxWithLabel"
                    LabelID="1855" AutoPostBack="True" OnCheckedChanged="chkShowAlertMsg_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <telerikWebControls:ExRadGrid ID="rgAlerts" runat="server" EntityNameLabelID="1010"
                    AllowPaging="false" AllowMultiRowSelection="true" OnNeedDataSource="rgAlerts_NeedDataSource"
                    OnItemDataBound="rgAlerts_ItemDataBound"
                     OnSortCommand="rgAlerts_SortCommand">
                    <MasterTableView DataKeyNames="CompanyAlertDetailUserID" AllowSorting="true"  AllowPaging="true">
                        <Columns>
                            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="4%" />
                            <telerikWebControls:ExGridTemplateColumn LabelID="1857" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="66%">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAlertDescription" runat="server" SkinID="Black9Arial"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="DateAdded" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAlertDateAdded" runat="server" SkinID="Black9Arial"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                </telerikWebControls:ExRadGrid>
            </td>
        </tr>
        <tr>
            <td align="right">
                <br />
                <webControls:ExButton ID="btnSaveAlert" runat="server" SkinID="ExButton150" LabelID="1856"
                    OnClick="btnSaveAlert_Click" />
            </td>
        </tr>
    </table>
    </td> </tr> </table>

    <script type="text/javascript" language="javascript">
        function RowSelecting(rowObject) {

            return false; //cancel event
        }
    </script>

</asp:Content>
