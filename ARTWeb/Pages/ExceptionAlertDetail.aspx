<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true" Inherits="Pages_ExceptionAlertDetail" Title="Untitled Page" 
    Theme="SkyStemBlueBrown" Codebehind="ExceptionAlertDetail.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
       
        <tr>
            <td align="center" >
                <table width="96%" border="0" cellpadding="0" cellspacing="0">
                    <%--<tr>
                        <td align="left">
                            <webControls:ExLabel ID="lblExceptionType" runat="server" SkinID="SubSectionHeading"
                                LabelID="1011"></webControls:ExLabel>
                        </td>
                    </tr>--%>
                    
                    <tr>
                        <td align="center">
                            <webControls:ExCheckBoxWithLabel ID="chkShowExceptionMsg"   runat="server" SkinID="CheckboxWithLabel"
                                LabelID="1855" AutoPostBack="True" 
                                oncheckedchanged="chkShowExceptionMsg_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <telerikWebControls:ExRadGrid ID="rgException" runat="server" EntityNameLabelID="1011"
                                AllowMultiRowSelection="true" OnNeedDataSource="rgException_NeedDataSource" 
                                OnItemDataBound="rgException_ItemDataBound" 
                                 OnSortCommand="rgException_SortCommand"  >
                                <MasterTableView DataKeyNames="CompanyAlertDetailUserID" AllowSorting="true"   AllowPaging="true">
                                    <Columns>
                                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn_Exception" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="4%" />
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1857" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="66%">
                                            <ItemTemplate>
                                            <webControls:ExLabel ID="lblAlertDescription" runat ="server" SkinID="Black9Arial"></webControls:ExLabel> 
                                     
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        
                                         <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="DateAdded" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                            <ItemTemplate>
                                            <webControls:ExLabel ID="lblAlertDateAdded" runat ="server" SkinID="Black9Arial"></webControls:ExLabel> 
                                     
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
                            <webControls:ExButton ID="btnSaveException" runat="server" SkinID="ExButton100" 
                                LabelID="1856 " onclick="btnSaveException_Click" />
                 
                        </td>
                    </tr>
                </table>
                
                
                
            </td>
        </tr>
    </table>
<asp:HiddenField ID="hdnIsRead" Value="yes" runat="server" />
    <script type="text/javascript" language="javascript">
    function RowSelecting(rowObject)
 { 

    return false; //cancel event
 }
 
    </script>

</asp:Content>
