<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CertificationMasterPagePrint.master"
    AutoEventWireup="true" CodeFile="CertificationSignOffPrint.aspx.cs" Inherits="Pages_CertificationPrint_CertificationSignOffPrint"
    Theme="SkyStemBlueBrown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCertification" runat="Server">
    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <table style="width: 96%" cellpadding="0" cellspacing="0" class="DataImportStatusMessagePrintTable">
                    <tr>
                        <td style="padding-right:1px;padding-bottom:1px;">
                            <table style="width: 100%" cellpadding="0" cellspacing="0" class="DataImportStatusMessagePrintContent">
                                <tr>
                                    <td align="center" class="DataImportStatusMessageTitleFirstRow">
                                        <webControls:ExLabel ID="lblCertificationHeader" LabelID="1702 " runat="server" SkinID="Black11Arial"
                                            Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="DataImportStatusMessageTitle">
                                        <webControls:ExLabel ID="lblCertificationDate" runat="server" SkinID="Black11Arial"
                                            Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <webControls:ExLabel ID="lblCertificationVerbiage" runat="server" SkinID="Black11ArialNormal"
                                            Width="100%" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
