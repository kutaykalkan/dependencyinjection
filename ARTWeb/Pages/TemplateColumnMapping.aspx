<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" Inherits="Pages_TemplateColumnMapping" Theme="SkyStemBlueBrown" Codebehind="TemplateColumnMapping.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <%-- code starts from here--%>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td style="padding-left: 15px">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <webControls:ExLabel ID="lblTemplateName" runat="server" SkinID="Black11Arial" LabelID="2860"
                                FormatString="{0}:"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lbTemplateName" runat="server" CssClass="UserName"></webControls:ExLabel>
                            &nbsp;&nbsp;
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblDataImportType" runat="server" SkinID="Black11Arial" LabelID="1307"
                                FormatString="{0}:" />
                        </td>
                        <td>
                            <webControls:ExLabel ID="lbDataImport" runat="server" CssClass="UserName"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <webControls:ExLabel ID="lblCreatedBy" runat="server" SkinID="Black11Arial" LabelID="2556"
                                FormatString="{0}:" />
                        </td>
                        <td>
                            <webControls:ExLabel ID="lbCreatedBy" runat="server" CssClass="UserName"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblDateCreated" runat="server" SkinID="Black11Arial" LabelID="2557"
                                FormatString="{0}:" />
                        </td>
                        <td>
                            <webControls:ExLabel ID="lbDateCreated" runat="server" CssClass="UserName"></webControls:ExLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td>
              <%--  <asp:Panel ID="pnlTemplateFields" runat="server">--%>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="2">
                                <telerikWebControls:ExRadGrid ID="ucSkyStemARTGridMapping" Width="100%" runat="server" OnItemDataBound="ucSkyStemARTGridMapping_GridItemDataBound"
                                    SkinID="SkyStemBlueBrownRecItems" AllowMultiRowSelection="true" ClientSettings-Selecting-AllowRowSelect="true">
                                    <ClientSettings>
                                        <Selecting UseClientSelectColumnOnly="true" />
                                    </ClientSettings>
                                    <MasterTableView ClientDataKeyNames="ImportFieldID"
                                        DataKeyNames="ImportFieldID" Width="100%" ShowFooter="false" TableLayout="Auto"
                                        Name="MappingItemsGridView">
                                        <Columns>
                                         <%--   <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" Visible="true"
                                                HeaderStyle-Width="1%">
                                            </telerikWebControls:ExGridClientSelectColumn>--%>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="2873" HeaderStyle-Width="9%" SortExpression="ARTTemplateFields"
                                                DataType="System.String" UniqueName="ARTTemplateFields">
                                                <ItemTemplate>
                                                   <webControls:ExLabel ID="lblMandatory" runat="server" CssClass="ManadatoryField" Visible="false">*</webControls:ExLabel>
                                                  <webControls:ExLabel ID="lblTemplateFields" runat="server"></webControls:ExLabel>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="2874" UniqueName="TemplateFields"
                                                HeaderStyle-Width="8%">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlTemplateFields" runat="server"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvTemplateFields" runat="server" Enabled="false" ControlToValidate="ddlTemplateFields" ErrorMessage="Please select Column from dropdown" Text="!" InitialValue="-2">
                                                    </asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </telerikWebControls:ExGridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <FooterStyle HorizontalAlign="Right" />
                                </telerikWebControls:ExRadGrid>
                            </td>
                        </tr>
                    </table>
                <%--</asp:Panel>--%>
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td colspan="0" align="right">
                <webControls:ExButton ID="btnSave" runat="server" LabelID="1315" OnClick="btnSave_Click" />&nbsp;
                        <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClick="btnCancel_Click"
                            CausesValidation="false" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

