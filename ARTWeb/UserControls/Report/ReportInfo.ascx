<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportInfo.ascx.cs" Inherits="UserControls_Report_ReportInfo" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 150px; padding-left: 2px; height: 100px;">
                        <asp:Image ID="imgCompanyLogo" ImageAlign="AbsMiddle" runat="server" Width="150"
                            Height="100" />
                    </td>
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center" colspan="2">
                                    <webControls:ExLabel ID="lblCompanyName" runat="server" SkinID="BlueBold13Arial"></webControls:ExLabel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <webControls:ExLabel ID="lblReportName" runat="server" SkinID="ReportTitle"></webControls:ExLabel>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <webControls:ExLabel ID="lblReportDescription" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 150px">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr class="BlankRow">
        <td>
        </td>
    </tr>
    <tr>
        <td align="left">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <colgroup>
                    <col width="10%" />
                    <col width="28%" />
                    <col width="8%" />
                    <col width="20%" />
                    <col width="18%" />
                    <col width="16%" />
                    <tr>
                        <td style="padding-left: 5px">
                            <webControls:ExLabel ID="lblPreparedBy" runat="server" FormatString="{0}:" LabelID="1500"
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td align="left">
                            <webControls:ExLabel ID="lblPreparedByValue" runat="server" SkinID="UserName"></webControls:ExLabel>
                        </td>
                        <td align="right">
                            <webControls:ExLabel ID="lblRptPeriod" runat="server" FormatString="{0}:" LabelID="1420"
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td style="padding-left: 5px">
                            <webControls:ExLabel ID="lblRptPeriodValue" runat="server" SkinID="UserName"></webControls:ExLabel>
                        </td>
                        <td align="right">
                            <webControls:ExLabel ID="lblDateTime" runat="server" FormatString="{0}:" LabelID="1630"
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td style="padding-left: 5px">
                            <webControls:ExLabel ID="lblDateTimeValue" runat="server" SkinID="UserName"></webControls:ExLabel>
                        </td>
                    </tr>
                </colgroup>
            </table>
        </td>
    </tr>
</table>
