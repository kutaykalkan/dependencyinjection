<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true" Inherits="Pages_GridCustomization" Theme="SkyStemBlueBrown" Codebehind="GridCustomization.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <webControls:ExLabel ID="lblSelectColumns" runat="server" LabelID="1766" SkinID="Black11Arial"></webControls:ExLabel>:
                <webControls:ExCustomValidator ID="cvColumns" runat="server" ClientValidationFunction="ValidateSelectedColumns"
                    runat="server" LabelID="5000091">!</webControls:ExCustomValidator>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:checkboxlist id="cblColumns" runat="server" repeatcolumns="2" repeatdirection="Horizontal"
                    skinid="cblDefault">
                </asp:checkboxlist>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td align="right">
                <webControls:ExButton ID="btnSave" runat="server" OnClick="btnSave_Click" SkinID="ExButton100"
                    LabelID="1315" />
            </td>
        </tr>
    </table>

    <script type="text/javascript" language="javascript">
        function ValidateSelectedColumns(source, args) 
        {
            var cblColumns = document.getElementById('<%= this.cblColumns.ClientID %>');
            if (IsItemSelectedInCheckBoxList(cblColumns))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
    </script>

</asp:content>
