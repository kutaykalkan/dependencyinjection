<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataTypeMapping.ascx.cs"
    Inherits="UserControls_Matching_DataTypeMapping" %>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td width="1%">
            &nbsp;
        </td>
        <td width="5%">
            <webControls:ExLabel ID="lblDataSourceName" runat="server" SkinID="Black11Arial"
                LabelID="2191" FormatString="{0}:"></webControls:ExLabel>
        </td>
        <td width="10%">
            <asp:DropDownList ID="ddlDataSourceName" runat="server" SkinID="DropDownList200"
                AutoPostBack="true">
            </asp:DropDownList>
        </td>
    </tr>
    <tr class="BlankRow">
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Repeater ID="rptDataType" runat="server">
                <HeaderTemplate>
                    <table border="1" width="100%">
                        <tr>
                            <th>
                                Column Name
                            </th>
                            <th>
                                Data Type
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                           ColumnName
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDataType" runat="server" SkinID="DropDownList200" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </td>
    </tr>
</table>
