<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/PopUpMasterPage.master"
    CodeFile="PopupViewAccounts.aspx.cs" Inherits="Pages_Matching_PopupViewAccounts"
    Theme="SkyStemBlueBrown" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
      <tr>
      <webControls:ExLabel ID="lblDataSourceName" SkinID="Black11Arial" runat="server">
      </webControls:ExLabel>
      </tr>
    
    
        <tr>
            <td align="center">
                <telerikWebControls:ExRadGrid ID="radViewAccounts" runat="server" ShowHeader="false"
                    AllowInster="false" OnItemDataBound="radViewAccounts_ItemDataBound" 
                    AutoGenerateColumns="false" >
                    <MasterTableView>
                        <Columns>
                            
                            <telerikWebControls:ExGridTemplateColumn LabelID="2242" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAccountNumber" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            
                            <telerikWebControls:ExGridTemplateColumn LabelID="2243"  ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="35%" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAccountName" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            
                            <telerikWebControls:ExGridTemplateColumn LabelID="1382" ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-Width="35%" HeaderStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblGLBalance" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerikWebControls:ExRadGrid>
            </td>
        </tr>
    </table>
</asp:Content>
