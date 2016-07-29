<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true"
    Theme="SkyStemBlueBrown" Inherits="Pages_RecPeriodStatusHistoryPopup" Codebehind="RecPeriodStatusHistoryPopup.aspx.cs" %>
<%@ OutputCache Duration="1" VaryByParam="none" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">

        <tr class="BlankRow">
            <td></td>
        </tr>

        <tr>
            <td> <webControls:ExLabel ID="lblRecPeriodStatusHistory" LabelID="2985" FormatString="{0} :" SkinID="Black11Arial" 
                    runat="server"></webControls:ExLabel></td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td>
                <telerikwebcontrols:exradgrid id="rgAuditTrail" runat="server" entitynamelabelid="1380"
                    allowpaging="false" allowsorting="true" allowexporttoexcel="true" allowexporttopdf="true"
                    onneeddatasource="rgAuditTrail_NeedDataSource" onitemdatabound="rgAuditTrail_ItemDataBound"
                    onitemcreated="rgAuditTrail_ItemCreated" onitemcommand="rgAuditTrail_OnItemCommand">                    
                    <MasterTableView>
                        <Columns>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="StatusDate">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDate" runat="server"/>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1533 " SortExpression="FullName">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblUser" runat="server" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2986" SortExpression="ReconciliationPeriodStatus">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblStatus" runat="server" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerikwebcontrols:exradgrid>
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td align="right" colspan="2" style="padding-right: 30px; padding-top: 20px">

                <webControls:ExButton ID="btnCancel" runat="server" Width="70" LabelID="1239" Height="25"
                    OnClientClick="GetRadWindow().Close();" CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
