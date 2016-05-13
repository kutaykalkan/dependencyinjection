<%@ Page Language="C#" MasterPageFile="~/MasterPages/MatchingMaster.master" AutoEventWireup="true" 
CodeFile="MatchingDataImportStatusMessages.aspx.cs" 
Theme="SkyStemBlueBrown" Inherits="Pages_Matching_MatchingDataImportStatusMessages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMatching" Runat="Server">
 <table width="100%">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:panel id="pnlErrorUpload" runat="server">
                    <table width="60%" class="DataImportStatusMessage" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="DataImportStatusMessageTitle" style="width: 6%">
                                <webControls:ExImage ID="imgFailure" runat="server" SkinID="ExpireIcon" Visible="false" />
                                <webControls:ExImage ID="imgSuccess" runat="server" SkinID="SuccessIcon" Visible="false" />
                                <webControls:ExImage ID="imgWarning" runat="server" SkinID="WarningIcon" Visible="false" />
                                <webControls:ExImage ID="imgProcessing" runat="server" SkinID="ProgressIcon" Height="24px"
                                    Width="23px" Visible="false" />
                                <webControls:ExImage ID="imgToBeProcessed" runat="server" SkinID="ToBeProcessedIcon"
                                    Visible="false" />
                                <webControls:ExImage ID="imgDraft" runat="server" SkinID="Draft"
                                    Visible="false" />
                            </td>
                            <td class="DataImportStatusMessageTitle">
                                <webControls:ExLabel ID="lblStatusHeading" runat="server" SkinID="BlueBold11Arial"></webControls:ExLabel>
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <webControls:ExLabel ID="ExLabel2" runat="server" SkinID="Black11Arial" LabelID="1523"
                                    FormatString="{0}:"></webControls:ExLabel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-left: 20px;">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 30%">
                                            <webControls:ExLabel ID="lblDataImportType" runat="server" SkinID="Black11Arial"
                                                LabelID="1307" FormatString="{0}:"></webControls:ExLabel>
                                        </td>
                                        <td>
                                            <webControls:ExLabel ID="lblDataImportTypeValue" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                        </td>
                                    </tr>
                                    <tr class="BlankRow">
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <webControls:ExLabel ID="lblProfileName" runat="server" SkinID="Black11Arial" LabelID="1524"
                                                FormatString="{0}:"></webControls:ExLabel>
                                        </td>
                                        <td>
                                            <webControls:ExLabel ID="lblProfileNameValue" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                        </td>
                                    </tr>
                                    <tr class="BlankRow">
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <webControls:ExLabel ID="lblLoadDate" runat="server" SkinID="Black11Arial" FormatString="{0}:"
                                                LabelID="1528"></webControls:ExLabel>
                                        </td>
                                        <td>
                                            <webControls:ExLabel ID="lblLoadDateValue" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                        </td>
                                    </tr>
                                    <tr class="BlankRow">
                                        <td>
                                        </td>
                                    </tr>
                                    <asp:panel id="pnlSuccess" runat="Server">
                                        <tr>
                                            <td>
                                                <webControls:ExLabel ID="lblNoOfRecords" runat="server" SkinID="Black11Arial" FormatString="{0}:"
                                                    LabelID="1745"></webControls:ExLabel>
                                            </td>
                                            <td>
                                                <webControls:ExLabel ID="lblNoOfRecordsValue" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <webControls:ExLabel ID="lblForceCommitDate" runat="server" SkinID="Black11Arial"
                                                    FormatString="{0}:" LabelID="1736"></webControls:ExLabel>
                                            </td>
                                            <td>
                                                <webControls:ExLabel ID="lblForceCommitDateValue" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                            <td>
                                            </td>
                                        </tr>
                                    </asp:panel>
                                    <tr>
                                        <td colspan="2">
                                            <webControls:ExLabel ID="lblMessage" runat="server" SkinID="Black11Arial" FormatString="{0}:"></webControls:ExLabel>
                                        </td>
                                    </tr>
                                    <tr class="BlankRow">
                                        <td>
                                        </td>
                                    </tr>
                                    <asp:panel id="pnlFailureMessages" runat="server">
                                        <tr>
                                            <td colspan="2">
                                                <asp:panel ID="Panel1" runat="server" scrollbars="Both" height="200px" cssclass="DataImportErrorPanel">
                                                    <webControls:ExLabel ID="lblFailureMessages" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                                </asp:panel>
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                            <td>
                                            </td>
                                        </tr>
                                    </asp:panel>
                                    <asp:panel id="pnlWarning" runat="server">
                                        <tr>
                                            <td colspan="2">
                                                <webControls:ExLabel ID="lblConfirmUpload" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                            <td>
                                            </td>
                                        </tr>
                                    </asp:panel>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <webControls:ExButton ID="btnYes" runat="server" SkinID="ExButton100" LabelID="1252"
                                                OnClick="btnYes_Click" />&nbsp;
                                            <webControls:ExButton ID="btnBack" runat="server" SkinID="ExButton100" LabelID="1545"
                                                OnClick="btnBack_Click" />
                                        </td>
                                    </tr>
                                </table>
    </td> </tr> </table> </asp:panel> </td> </tr> </table>
</asp:Content>

