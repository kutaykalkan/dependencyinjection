<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" CodeFile="DataImportConfiguration.aspx.cs" Inherits="Pages_DataImportConfiguration" Theme="SkyStemBlueBrown" %>

<%@ Register TagPrefix="UserControls" TagName="InputRequirements" Src="~/UserControls/InputRequirements.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <asp:Panel ID="pnlHeader" runat="server">
                    <table class="InputRequrementsHeading" width="100%">
                        <tr>
                            <td width="70%">
                                <webControls:ExLabel ID="lblSRARuleSelection" runat="server" LabelID="2847" SkinID="BlueBold11Arial" />
                            </td>
                            <td width="30%" align="right">
                                <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlSRARuleSelection" runat="server">
                    <table width="100%" cellspacing="0" cellpadding="0" class="blueBorder">
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">
                                            <UserControls:InputRequirements ID="ucInputRequirements" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="BlankRow">
                                        <td />
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <webControls:ExLabel ID="lblDataImportType" runat="server" SkinID="Black11Arial" LabelID="1307" />
                                            &nbsp;
                                <asp:DropDownList ID="ddlDataImportType" runat="server" OnSelectedIndexChanged="ddlDataImportType_SelectedIndexChanged" AutoPostBack="true" SkinID="DropDownList200"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="BlankRow">
                                        <td />
                                    </tr>
                                    <tr>
                                        <td>
                                            <telerikWebControls:ExRadGrid ID="rgDataImportWarning" Width="100%" runat="server" OnItemDataBound="rgDataImportWarning_ItemDataBound"
                                                SkinID="SkyStemBlueBrownRecItems" AllowMultiRowSelection="true" ClientSettings-Selecting-AllowRowSelect="true">
                                                <ClientSettings>
                                                    <Selecting UseClientSelectColumnOnly="true" />
                                                </ClientSettings>
                                                <MasterTableView ClientDataKeyNames="DataImportMessageID"
                                                    DataKeyNames="DataImportMessageID" Width="100%" ShowFooter="false" TableLayout="Auto"
                                                    Name="MappingItemsGridView">
                                                    <Columns>
                                                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" Visible="true"
                                                            HeaderStyle-Width="4%">
                                                        </telerikWebControls:ExGridClientSelectColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="2891" HeaderStyle-Width="30%" SortExpression="DataImportWarning"
                                                            DataType="System.String" UniqueName="DataImportWarning">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblDataImportWarning" runat="server"></webControls:ExLabel>
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1859" UniqueName="Condition"
                                                            HeaderStyle-Width="60%">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblCondition" runat="server"></webControls:ExLabel>
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1408" UniqueName="DataImportWarningPreferencesID"
                                                            Visible="false">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="lblDataImportWarningPreferencesID" runat="server" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <FooterStyle HorizontalAlign="Right" />
                                            </telerikWebControls:ExRadGrid>
                                        </td>
                                    </tr>
                                    <tr class="BlankRow">
                                        <td />
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <webControls:ExButton ID="btnSave" runat="server" LabelID="1315" SkinID="ExButton100"
                                                OnClick="btnSave_Click" />&nbsp;
                            <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" SkinID="ExButton100"
                                OnClick="btnCancel_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" cellspacing="0" cellpadding="0">
                        <tr class="BlankRow">
                            <td></td>
                        </tr>
                        <tr class="HideInPdf">
                            <td align="center">
                                <webControls:ExHyperLink ID="hlAuditTrail" runat="server" LabelID="1380" NavigateUrl="~/Pages/DataImportConfigurationAuditTrail.aspx"></webControls:ExHyperLink>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpeSRARuleSelection" TargetControlID="pnlSRARuleSelection"
                    ImageControlID="imgCollapse" CollapseControlID="pnlHeader" ExpandControlID="pnlHeader"
                    runat="server" SkinID="CollapsiblePanel" Collapsed="false">
                </ajaxToolkit:CollapsiblePanelExtender>
            </td>
        </tr>
        <tr class="BlankRow">
            <td />
        </tr>
        <tr id="trFTPConfiguration" runat="server">
            <td>
                <asp:Panel ID="pnlFTPConfiguration" runat="server">
                    <table class="InputRequrementsHeading" width="100%">
                        <tr>
                            <td width="70%">
                                <webControls:ExLabel ID="lblFTPConfiguration" runat="server" LabelID="2851" SkinID="BlueBold11Arial" />
                            </td>
                            <td width="30%" align="right">
                                <webControls:ExImage ID="imgCollapseFTPC" runat="server" SkinID="CollapseIcon" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlFTPConfigurationGrid" runat="server">
                    <table width="100%" cellspacing="0" cellpadding="0" class="blueBorder">
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td colspan="2">
                                            <UserControls:InputRequirements ID="InputRequirementsFTPConfig" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="BlankRow">
                                        <td />
                                    </tr>
                                    <tr>
                                        <td>
                                            <webControls:ExLabel ID="lblFTPServer" SkinID="Black11Arial" LabelID="2903" runat="server" FormatString="{0}:"></webControls:ExLabel>
                                            &nbsp;
                                            <webControls:ExLabel ID="lblFTPServerVal" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                        </td>
                                    </tr>
                                    <tr class="BlankRow">
                                        <td />
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerikWebControls:ExRadGrid ID="rgFTPConfig" Width="100%" runat="server" OnItemDataBound="rgFTPConfig_ItemDataBound"
                                                OnNeedDataSource="rgFTPConfig_NeedDataSource" SkinID="SkyStemBlueBrownRecItems" AllowMultiRowSelection="true" ClientSettings-Selecting-AllowRowSelect="true">
                                                <ClientSettings>
                                                    <Selecting UseClientSelectColumnOnly="true" />
                                                </ClientSettings>
                                                <MasterTableView ClientDataKeyNames="UserFTPConfigurationID,DataImportTypeID"
                                                    DataKeyNames="UserFTPConfigurationID,DataImportTypeID" Width="100%" ShowFooter="false" TableLayout="Auto"
                                                    Name="MappingItemsGridView">
                                                    <Columns>
                                                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" Visible="true"
                                                            HeaderStyle-Width="2%">
                                                        </telerikWebControls:ExGridClientSelectColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1307" HeaderStyle-Width="10%"
                                                            DataType="System.String" UniqueName="DataImportType">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblDataImportType" runat="server"></webControls:ExLabel>
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1308" UniqueName="ProfileName"
                                                            HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <webControls:ExTextBox ID="txtProfileName" runat="server"></webControls:ExTextBox>
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1278" UniqueName="FTPUploadRole"
                                                            HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlFTPUploadRole" runat="server" SkinID="DropDownList125" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="2870" UniqueName="DataImportTemplate"
                                                            HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlDataImportTemplate" runat="server" SkinID="DropDownList125" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="2924" HeaderStyle-Width="25%"
                                                            DataType="System.String" UniqueName="FTPPath">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblFTPPath" runat="server"></webControls:ExLabel>
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="5%" AllowFiltering="false" LabelID="2923"
                                                            UniqueName="imgStatus">
                                                            <ItemTemplate>
                                                                <webControls:ExImage ID="imgSuccess" runat="server" LabelID="2923" SkinID="SuccessIcon"
                                                                    Visible="false" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <FooterStyle HorizontalAlign="Right" />
                                            </telerikWebControls:ExRadGrid>
                                        </td>
                                    </tr>
                                    <tr class="BlankRow">
                                        <td />
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <webControls:ExButton ID="btnSaveFTPConfig" ValidationGroup="FTPConfig" runat="server" LabelID="1315" SkinID="ExButton100"
                                                OnClick="btnSaveFTPConfig_Click" />
                                            <asp:CustomValidator ID="cvSaveFTPConfig" ValidationGroup="FTPConfig" runat="server" Text="!" OnServerValidate="cvSaveFTPConfig_ServerValidate"
                                                Font-Bold="true" Font-Size="Medium"></asp:CustomValidator>
                                            &nbsp;
                            <webControls:ExButton ID="btnCancelFTP" runat="server" LabelID="1239"  SkinID="ExButton100"
                                OnClick="btnCancel_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpeFTPConfiguration" TargetControlID="pnlFTPConfigurationGrid"
                    ImageControlID="imgCollapseFTPC" CollapseControlID="pnlFTPConfiguration" ExpandControlID="pnlFTPConfiguration"
                    runat="server" SkinID="CollapsiblePanel" Collapsed="false">
                </ajaxToolkit:CollapsiblePanelExtender>
            </td>
        </tr>
    </table>
</asp:Content>

