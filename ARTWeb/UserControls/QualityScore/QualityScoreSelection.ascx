<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QualityScoreSelection.ascx.cs"
    Inherits="SkyStem.ART.Web.UserControls.QualityScoreSelection" %>
<%@ Register TagPrefix="UserControls" TagName="InputRequirements" Src="~/UserControls/InputRequirements.ascx" %>
<asp:Panel ID="pnlHeader" runat="server">
    <table class="InputRequrementsHeading" width="100%">
        <tr>
            <td width="70%">
                <webControls:ExLabel ID="lblQualityScore" runat="server" LabelID="2423" SkinID="BlueBold11Arial" />
            </td>
            <td width="30%" align="right">
                <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlQualityScore" runat="server">
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
                            <telerikWebControls:ExRadGrid ID="rgQualityScoreSelection" runat="server" AllowMultiRowSelection="true"
                                GroupHeaderItemStyle-Height="25px" ClientSettings-Selecting-AllowRowSelect="true"
                                OnItemDataBound="rgQualityScoreSelection_ItemDataBound" NoMasterRecordsLabelID="2423">
                                <MasterTableView DataKeyNames="CompanyQualityScoreID,RowNumber">
                                    <Columns>
                                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="4%" />
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="Description" LabelID="2418">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblDescription" runat="server" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="QualityScoreNumber" LabelID="2419"
                                            HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblQualityScoreNumber" runat="server" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                                </ClientSettings>
                            </telerikWebControls:ExRadGrid>
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
<ajaxToolkit:CollapsiblePanelExtender ID="cpeQualityScore" TargetControlID="pnlQualityScore"
    ImageControlID="imgCollapse" CollapseControlID="pnlHeader" ExpandControlID="pnlHeader"
    runat="server" SkinID="CollapsiblePanel" Collapsed="false">
</ajaxToolkit:CollapsiblePanelExtender>

<script language="javascript" type="text/javascript">

    function ConfirmAndSubmit(msg) {
        var answer = confirm(msg);
        if (answer) {
            return true;
        }
        else {
            return false;
        }
    }

</script>

