<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" CodeFile="ScheduleDataImport.aspx.cs" Inherits="Pages_ScheduleDataImport" Theme="SkyStemBlueBrown" %>

<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlHeader" runat="server">
        <table class="InputRequrementsHeading">
            <tr>
                <td width="2%">
                    <webControls:ExImage ID="imgInputRequirementsIcon" runat="server" SkinID="NotesIcon" />
                </td>
                <td width="94%">
                    <webControls:ExLabel ID="lblInputRequirements" runat="server" LabelID="2886" />
                </td>
                <td width="4%" align="right">
                    <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlContent" runat="server">
        <table border="0" cellpadding="0" cellspacing="6" width="100%" class="InputRequrementsText">
            <tr>
                <td colspan="">
                    <telerikWebControls:ExRadGrid ID="ucSkyStemARTGridScheduleDataImport" Width="100%" runat="server" OnItemDataBound="ucSkyStemARTGridScheduleDataImport_ItemDataBound"
                        SkinID="SkyStemBlueBrownRecItems" AllowMultiRowSelection="true" ClientSettings-Selecting-AllowRowSelect="true">
                        <ClientSettings>
                            <Selecting UseClientSelectColumnOnly="true" />
                        </ClientSettings>
                        <MasterTableView ClientDataKeyNames="DataImportTypeID"
                            DataKeyNames="DataImportTypeID" Width="100%" ShowFooter="true" TableLayout="Auto"
                            Name="MappingItemsGridView">
                            <Columns>
                                <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" Visible="true"
                                    HeaderStyle-Width="1%">
                                </telerikWebControls:ExGridClientSelectColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1307" HeaderStyle-Width="15%" SortExpression="DataImportType"
                                    DataType="System.String" UniqueName="DataImportType">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblDataImportType" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1408" UniqueName="Description"
                                    HeaderStyle-Width="35%">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblDescription" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                   <telerikWebControls:ExGridTemplateColumn LabelID="2893" UniqueName="DefaultValue"
                                    HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblDefaultValue" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="Recurrence">
                                    <ItemTemplate>
                                        <table>
                                            <col width="25%" align="right" />
                                            <col width="5%" align="center" />
                                            <col width="50%" align="left" />
                                            <tr>
                                                <td>
                                                    <webControls:ExLabel ID="lblEvery" runat="server"></webControls:ExLabel>
                                                </td>
                                                <td>
                                                    <table>
                                                        <col width="97%" align="left" />
                                                        <col width="3%" align="left" />
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtEvery" runat="server" Width="30"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <webControls:ExCustomValidator ID="vldEvery" ControlToValidate="txtEvery"
                                                                    runat="server" Enabled="true" Text="!" Font-Bold="true" Font-Size="Medium" ClientValidationFunction="validateForNonEmptyAndPositive" OnServerValidate="NonEmptyAndPositive_OnServerValidate"></webControls:ExCustomValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <webControls:ExLabel ID="lblHours" runat="server"></webControls:ExLabel>
                                                </td>
                                                <td>
                                                    <webControls:ExLabel ID="lblAnd" runat="server"></webControls:ExLabel>
                                                </td>
                                                <td>
                                                    <table>
                                                        <col width="97%" align="left" />
                                                        <col width="3%" align="left" />
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtMinutes" runat="server" Width="30"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <webControls:ExCustomValidator ID="vldMinutes" ControlToValidate="txtMinutes"
                                                                    runat="server" Enabled="true" Text="!" Font-Bold="true" Font-Size="Medium" ClientValidationFunction="validateForNonEmptyAndPositive" OnServerValidate="NonEmptyAndPositive_OnServerValidate"></webControls:ExCustomValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <webControls:ExLabel ID="lblMinutes" runat="server"></webControls:ExLabel>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1408" UniqueName="DataImportScheduleID"
                                    HeaderStyle-Width="35%" Visible="false">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblDataImportScheduleID" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <FooterStyle HorizontalAlign="Right" />
                    </telerikWebControls:ExRadGrid>
                </td>
            </tr>
            <tr class="BlankRow">
                <td></td>
            </tr>
            <tr>
                <td colspan="0" align="right">
                    <webControls:ExButton ID="btnReset" runat="server" LabelID="2482" OnClick="btnReset_Click" />
                    <webControls:ExButton ID="btnSave" runat="server" LabelID="1315" OnClick="btnSave_Click" />&nbsp;
                        <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239"
                            CausesValidation="false" OnClick="btnCancel_Click" />&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeScheduleDataImport" TargetControlID="pnlContent"
        ImageControlID="imgCollapse" CollapseControlID="imgCollapse" ExpandControlID="imgCollapse"
        runat="server" SkinID="CollapsiblePanel" Collapsed="true">
    </ajaxToolkit:CollapsiblePanelExtender>
    <script type="text/javascript" language="javascript">
        function validateForNonEmptyAndPositive(sender, args) {
            var ValidationText = args.Value;
            if (ValidationText.match('^[1-9]+[0-9]*$'))
                args.IsValid = true;
            else
                args.IsValid = false;
        }
    </script>
</asp:Content>

