<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CapabilityRiskRating.ascx.cs"
    Inherits="UserControls_CapabilityRiskRating" %>
<asp:Panel ID="pnlRiskRatingFrequency" runat="server" Width="100%">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="2%" />
        <col width="35%" />
        <col width="25%" />
        <col width="38%" />
        <tr>
            <td class="ManadatoryField">
            </td>
            <td>
                <webControls:ExLabel ID="lblRiskRating" runat="server" LabelID="1013" FormatString="{0}:"
                    SkinID="Black11Arial" />
            </td>
            <td>
                <webControls:ExLabel ID="lblRiskRatingHeading" runat="server" SkinID="Black11Arial" />
            </td>
            <td>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td class="ManadatoryField">
            </td>
            <td>
                <webControls:ExLabel ID="lblRiskRatingFrequency" runat="server" LabelID="1427  "
                    FormatString="{0}:" SkinID="Black11Arial" />
            </td>
            <td>
                <asp:DropDownList ID="ddlRiskRatingFrequency" OnSelectedIndexChanged="ddlRiskRatingFrequency_SelectedIndexChanged"
                    runat="server" AutoPostBack="true" SkinID="DropDownList200" />
            </td>
            <td>
                <webControls:ExImage ID="imgStatusRiskRatingFrequencyForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                <webControls:ExImage ID="imgStatusRiskRatingFrequencyForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="pnlRiskRatingCustom" runat="server" Width="100%">
                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                        <col width="2%" />
                        <col width="35%" />
                        <col width="25%" />
                        <col width="38%" />
                        <tr>
                            <td class="ManadatoryField">
                                &nbsp;
                            </td>
                            <td>
                                <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="2011" FormatString="{0}:"
                                    SkinID="Black11Arial" />
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlFinancialYear" SkinID="DropDownList200" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <div id="Div1" runat="server" class="CheckBoxListDiv" style="width: 200px; height: 150px;
                                    overflow-y: scroll;">
                                    <asp:CheckBoxList ID="cblRiskRatingPeriodsCustom" OnDataBound="cblRiskRatingPeriodsCustom_DataBinding"
                                        runat="server" Height="80px" />
                                </div>
                            </td>
                            <td>
                                <webControls:ExCustomValidator runat="server" ID="cvRiskRating" EnableClientScript="true"
                                    ClientValidationFunction="validateAtLeastOneItemInCBL" Font-Bold="true" Font-Size="Medium">!</webControls:ExCustomValidator>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Panel>
