<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master"
    AutoEventWireup="true" Inherits="Pages_RoleConfiguration"
    Theme="SkyStemBlueBrown" Codebehind="RoleConfiguration.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="InputRequirements" Src="~/UserControls/InputRequirements.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="SkyStem.Library.Controls.TelerikWebControls" Assembly="TelerikWebControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlHeader" runat="server">
        <table class="InputRequrementsHeading" width="100%">
            <tr>
                <td width="70%">
                    <webControls:ExLabel ID="lblRoleConfig" runat="server" LabelID="2532" SkinID="BlueBold11Arial" />
                </td>
                <td width="30%" align="right">
                    <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlMainRoleConfig" runat="server">
        <table width="100%" cellspacing="0" cellpadding="0" class="blueBorder">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td>
                                <UserControls:InputRequirements ID="ucInputRequirements" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlRoleConfiguration" runat="server">
                                    <telerikWebControls:ExRadGrid ID="rgRoleConfiguration" runat="server" AllowMultiRowSelection="true"
                                        GroupHeaderItemStyle-Height="25px" ClientSettings-Selecting-AllowRowSelect="true"
                                        NoMasterRecordsLabelID="1816" OnItemDataBound="rgRoleConfiguration_ItemDataBound">
                                        <MasterTableView DataKeyNames="AttributeID">
                                            <Columns>
                                                <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="4%" />
                                                <telerikWebControls:ExGridTemplateColumn UniqueName="RoleConfiguration" LabelID="2514">
                                                    <ItemTemplate>
                                                        <webControls:ExLabel ID="lblRoleConfiguration" runat="server" />
                                                    </ItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn UniqueName="FilterColumn1">
                                                    <ItemTemplate>
                                                        <asp:Panel ID="pnlFromRecPeriod" runat="server" Visible="false">
                                                            <webControls:ExLabel ID="lblFromPeriod" runat="server" />&nbsp;
                                                            <asp:DropDownList ID="ddlFromPeriod" runat="server" />
                                                            <webControls:ExRequiredFieldValidator ID="rfvFromPeriod" ControlToValidate="ddlFromPeriod" runat="server" Display="Static">!</webControls:ExRequiredFieldValidator>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn UniqueName="FilterColumn2">
                                                    <ItemTemplate>
                                                        <asp:Panel ID="pnlToRecPeriod" runat="server" Visible="false">
                                                            <webControls:ExLabel ID="lblToPeriod" runat="server" />&nbsp;
                                                            <asp:DropDownList ID="ddlToPeriod" runat="server" />
                                                            <webControls:ExRequiredFieldValidator ID="rfvToPeriod" ControlToValidate="ddlToPeriod" runat="server" Display="Static">!</webControls:ExRequiredFieldValidator>
                                                            <webControls:ExCustomValidator ID="cvToPeriod" runat="server" LabelID="5000383" ClientValidationFunction="ValidatePeriodDates">!</webControls:ExCustomValidator>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox"
                                                OnRowCreated="EnableDisableControls"
                                                OnRowSelected="EnableDisableControls"
                                                OnRowDeselected="EnableDisableControls" />
                                        </ClientSettings>
                                    </telerikWebControls:ExRadGrid>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td />
                        </tr>
                        <tr>
                            <td align="right">
                                <webControls:ExButton ID="btnSave" runat="server" LabelID="1315" SkinID="ExButton100"
                                    OnClick="btnSave_OnClick" />&nbsp;
                                <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" SkinID="ExButton100"
                                    OnClick="btnCancel_OnClick" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeRoleConfig" TargetControlID="pnlMainRoleConfig"
        ImageControlID="imgCollapse" CollapseControlID="pnlHeader" ExpandControlID="pnlHeader"
        runat="server" SkinID="CollapsiblePanel" Collapsed="false">
    </ajaxToolkit:CollapsiblePanelExtender>
    <script type="text/javascript">

        function ValidatePeriodDates(sender, args) {
            var ddlFromID = $(sender).attr("ddlFromPeriodID");
            var ddlToID = $(sender).attr("ddlToPeriodID");
            var fromPeriod = $('#'+ddlFromID+' :selected').text();
            var toPeriod = $('#' + ddlToID + ' :selected').text();
            if (CompareDates(fromPeriod, toPeriod) > 0)
                args.IsValid = false;
        }

        function EnableDisableControls(sender, args) {
            var item = args.get_item();
            var ddlFromPeriod = item.findElement("ddlFromPeriod");
            var rfvFromPeriod = item.findElement("rfvFromPeriod");
            var ddlToPeriod = item.findElement("ddlToPeriod");
            var rfvToPeriod = item.findElement("rfvToPeriod");
            var cvToPeriod = item.findElement("cvToPeriod");
            var isEnabled = item.get_selected();
            if(ddlFromPeriod != undefined)
                ddlFromPeriod.disabled = !isEnabled;
            if (rfvFromPeriod != undefined)
                ValidatorEnable(rfvFromPeriod, isEnabled);
            if (ddlToPeriod != undefined)
                ddlToPeriod.disabled = !isEnabled;
            if (rfvToPeriod != undefined)
                ValidatorEnable(rfvToPeriod, isEnabled);
            if (cvToPeriod != undefined)
                ValidatorEnable(cvToPeriod, isEnabled);
        }
    </script>
</asp:Content>
