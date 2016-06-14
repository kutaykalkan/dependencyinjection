<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master"
    Theme="SkyStemBlueBrown" AutoEventWireup="true"
    Inherits="Pages_RecForm_EditExchangeRate" Codebehind="EditExchangeRate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <asp:Panel ID="pnlBCCY" runat="server">
            <tr>
                <td>
                    <webControls:ExLabel ID="lblLCCYtoBCCY" runat="server" LabelID="2048" FormatString="{0}:"
                        SkinID="Black11Arial" />
                </td>
                <td>
                    <asp:TextBox ID="txtLCCYtoBCCY" runat="server" SkinID="TextBox70" />
                    <asp:RequiredFieldValidator ID="rfvLCCYtoBCCY" runat="server" ControlToValidate="txtLCCYtoBCCY">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvLCCYtoBCCY" runat="server" ControlToValidate="txtLCCYtoBCCY"
                        ClientValidationFunction="ValidateNumbers">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr class="BlankRow">
                <td colspan="2">
                </td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="pnlRCCY" runat="server">
            <tr>
                <td>
                    <webControls:ExLabel ID="lblLCCYtoRCCY" runat="server" LabelID="2049" FormatString="{0}:"
                        SkinID="Black11Arial" />
                </td>
                <td>
                    <asp:TextBox ID="txtLCCYtoRCCY" runat="server" SkinID="TextBox70" />
                    <asp:RequiredFieldValidator ID="rfvLCCYtoRCCY" runat="server" ControlToValidate="txtLCCYtoRCCY">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvLCCYtoRCCY" runat="server" ControlToValidate="txtLCCYtoRCCY"
                        ClientValidationFunction="ValidateNumbers">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr class="BlankRow">
                <td colspan="2">
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td>
            </td>
            <td>
                <webControls:ExButton ID="btnOK" runat="server" LabelID="1742" SkinID="ExButton100"
                    OnClientClick="javascript:SubmitAndClose();" />
            </td>
        </tr>
    </table>

    <script type="text/javascript" language="javascript">
        function SubmitAndClose() {
            var txtLCCYtoBCCY = $get('<%= txtLCCYtoBCCY.ClientID %>');
            var txtLCCYtoRCCY = $get('<%= txtLCCYtoRCCY.ClientID %>');
            Page_ClientValidate();
            var LccyToBccy = null;
            var LccyToRccy = null;
            if(txtLCCYtoBCCY != null)
                LccyToBccy = txtLCCYtoBCCY.value;
            if(txtLCCYtoRCCY != null)
                RccyToBccy = txtLCCYtoRCCY.value;
            if (Page_IsValid) {
                var wnd1 = GetRadWindow();
                var mgr = wnd1.get_windowManager();
                var wnd2 = mgr.getWindowByName("EditRecItemWindow");
                wnd2.get_contentFrame().contentWindow.OverrideExchangeRate(LccyToBccy, RccyToBccy);
                wnd1.close();
            }
        }
    </script>

</asp:Content>
