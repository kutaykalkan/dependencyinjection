<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true" Inherits="Pages_PopupUserLockdownDetail" Theme="SkyStemBlueBrown" Codebehind="PopupUserLockdownDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="center">

                <telerikWebControls:ExRadGrid ID="rgUserLockdownDetail" runat="server" ShowHeader="false"
                    AllowInster="false" AllowExportToExcel="false" AllowExportToPDF="false" AllowPrint="false"
                    AllowPrintAll="false" EntityNameLabelID="2943" AutoGenerateColumns="false"
                    OnItemDataBound="rgUserLockdownDetail_ItemDataBound">
                    <MasterTableView>
                        <Columns>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2941" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="50%" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblLockoutDateAndTime" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>

                            <telerikWebControls:ExGridTemplateColumn LabelID="2942" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="50%" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblResetDateAndTime" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerikWebControls:ExRadGrid>

            </td>
        </tr>
        <%--<tr>
            <td>
                <webControls:ExButton ID="btnOKPopup" runat="server" LabelID="1742" CausesValidation="false"
                    OnClientClick="ClosePage()" />&nbsp;
            </td>
        </tr>--%>
    </table>

    <script type="text/javascript" language="javascript">
        function ClosePage() {
            GetRadWindow().Close();
        }
    </script>

</asp:Content>
