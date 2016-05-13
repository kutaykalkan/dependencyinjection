<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true"
    CodeFile="DueDates.aspx.cs" Inherits="Pages_DueDates" Theme="SkyStemBlueBrown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" class="DueDatesPanelContent">
        <tr>
            <td>
                <webControls:ExLabel ID="lblRecPeriod" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" class="TableSameAsGrid">
                    <asp:Panel ID="pnlPRADueDates" runat="server">
                        <tr class="TableRowSameAsGrid">
                            <td style="width: 75%">
                                <webControls:ExLabel ID="Label1" runat="server" LabelID="1417" SkinID="Black11Arial"
                                    FormatString="{0}:"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblPreparerDueDate" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                            </td>
                        </tr>
                        <tr class="TableAlternateRowSameAsGrid">
                            <td>
                                <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="1418" SkinID="Black11Arial"
                                    FormatString="{0}:"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblReviewerDueDate" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                            </td>
                        </tr>
                        <tr class="TableRowSameAsGrid">
                            <td>
                                <webControls:ExLabel ID="ExLabel2" runat="server" LabelID="1738" SkinID="Black11Arial"
                                    FormatString="{0}:"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblApproverDueDate" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr class="TableAlternateRowSameAsGrid" id="trCertStartDate" runat="server">
                        <td>
                            <webControls:ExLabel ID="ExLabel3" runat="server" LabelID="1453" SkinID="Black11Arial"
                                FormatString="{0}:"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblCertificationStartDate" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr class="TableRowSameAsGrid">
                        <td>
                            <webControls:ExLabel ID="lblRecCloseOrCertDueDateTitle" runat="server" SkinID="Black11Arial"
                                FormatString="{0}:"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblRecCloseOrCertDueDate" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
