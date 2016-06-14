<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Pages_ReportSignOffArchive" Theme="SkyStemBlueBrown" MasterPageFile="~/MasterPages/PopUpMasterPage.master" Codebehind="ReportSignOffArchive.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="Label1" runat="server" Width="410px"></asp:Label>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="30%" />
        <col width="70%" />
        <tr>
            <td>
                <webControls:ExLabel ID="lblReportPeriod" LabelID="1846" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblReportPeriodValue" SkinID="Black11ArialNormal" runat="server"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td>
                <webControls:ExRadioButton ID="optArchive" runat="server" LabelID="1583" GroupName="Active"
                    SkinID="OptBlack11Arial" />
            </td>
            <td>
                <webControls:ExRadioButton ID="optSignOff" runat="server" LabelID="1377" GroupName="Active"
                    SkinID="OptBlack11Arial" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;&nbsp;
                <webControls:ExLabel ID="lblSharedWith" SkinID="Black11ArialNormal" LabelID="1847"
                    runat="server"></webControls:ExLabel>
            </td>
            <td>
                <asp:TextBox ID="txtSharedWith" runat="server" SkinID="TextBox200" MaxLength="128"></asp:TextBox>
                <webControls:ExRegularExpressionValidator ID="revEmailId" runat="server" ControlToValidate="txtSharedWith"
                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*"
                    LabelID="1751"></webControls:ExRegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 2px">&nbsp;
            </td>
            <td style="padding-top: 2px">
                <webControls:ExLabel ID="lblEmailToolTip" SkinID="Black9ArialItalic" runat="server"
                    LabelID="1976" FormatString="({0})"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px; vertical-align: top;">
                <webControls:ExLabel ID="lblComments" SkinID="Black11ArialNormal" LabelID="1848"
                    FormatString="{0}:" runat="server"></webControls:ExLabel>
            </td>
            <td style="vertical-align: top;">
                <webControls:ExTextBox ID="txtComments" TextMode="MultiLine" runat="server" SkinID="ExMulitilineTextBoxSignoffComment" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <webControls:ExButton ID="btnOk" runat="server" Width="50" Height="25" LabelID="1742"
                    OnClick="btnOk_Click" OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true; this.value = 'Ok...';" UseSubmitBehavior="false"/>
                &nbsp;
                <webControls:ExButton ID="btnCancel" runat="server" Width="70" LabelID="1239" Height="25" OnClick="btnCancel_Click"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
