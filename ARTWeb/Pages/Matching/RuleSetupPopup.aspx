<%@ Page Language="C#" AutoEventWireup="true" Inherits="RuleSetupPopup"
    MasterPageFile="~/MasterPages/PopUpMasterPage.master" Theme="SkyStemBlueBrown" Codebehind="RuleSetupPopup.aspx.cs" %>

<%@ Register Src="~/UserControls/Matching/LowerUpperBound.ascx" TagName="LowerUpperBoundControl"
    TagPrefix="RuleSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <asp:UpdatePanel ID="updAutoComplete" runat="server">
        <ContentTemplate>
            <div style="background: white;">
                <table id="tblMain" style="background: white;" runat="server" width="100%" cellspacing="0"
                    cellpadding="0" border="0">
                    <%--<colgroup>
                    <col width="5%" />
                    <col width="30%" />
                    <col width="60%" />
                    <col width="5%" />
                </colgroup>--%>
                    <tr>
                        <td style="width: 5%">
                            &nbsp
                            <!--Blank Column -->
                        </td>
                        <td style="width: 30%">
                            <webControls:ExLabel ID="lblDataSource1" runat="server" LabelID="2239" SkinID="Black11Arial"
                                FormatString="{0}:"></webControls:ExLabel>
                        </td>
                        <td style="width: 60%">
                            <webControls:ExLabel ID="lblDataSource1Value" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td style="width: 5%">
                            &nbsp<!--Blank Column -->
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                            <!--Blank Column -->
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblDataSource2" runat="server" LabelID="2240" SkinID="Black11Arial"
                                FormatString="{0}:"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblDataSource2Value" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                            <!--Blank Column -->
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblComparisonColumn" runat="server" LabelID="2238" SkinID="Black11Arial"
                                FormatString="{0}:"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblComparisonColumnValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            &nbsp
                            <!--Blank Column -->
                        </td>
                    </tr>
                    <tr id="blankRow1" class="BlankRow">
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr id="rowHint" visible="false">
                        <td>
                            &nbsp
                            <!--Blank Column -->
                        </td>
                        <td colspan="2">
                            <webControls:ExLabel ID="lblHintValue" LabelID="2281" runat="server" SkinID="Black11ArialNormal"
                                FormatString="{0}:"></webControls:ExLabel>
                        </td>
                        <td>
                            <!--Blank Column -->
                        </td>
                    </tr>
                    <tr id="blankRow2" class="BlankRow">
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr id="rowValueType" visible="false">
                        <td>
                            &nbsp
                        </td>
                        <td>
                            <webControls:ExLabel ib="lblValueType" runat="server" LabelID="1860" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlValueType" runat="server" AutoPostBack="false" CausesValidation="false"
                                SkinID="DropDownList100">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="blankRow3" class="BlankRow">
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr id="rowOperator" visible="false">
                        <td>
                            &nbsp
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblOperator" runat="server" LabelID="1943" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlOperator" runat="server" FormatString="{0}:" CausesValidation="false"
                                SkinID="DropDownList100">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr id="blankRow4" class="BlankRow">
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr id="rowStrType" visible="false">
                        <td class="ManadatoryField" align="right">
                            *
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblStringValue" runat="server" LabelID="1944" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <!-- LabelID needed to be changed later -->
                        </td>
                        <td>
                            <asp:TextBox ID="txtStringValue" runat="server" MaxLength="100"></asp:TextBox>
                            <webControls:ExRequiredFieldValidator ID="rfvStrValue" runat="server" ControlToValidate="txtStringValue"
                                Visible="true" LabelID="5000263" Display="Dynamic"></webControls:ExRequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="blankRow5" class="BlankRow">
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr id="rowBounds" visible="false">
                        <td>
                            &nbsp
                            <!--Blank Column -->
                        </td>
                        <td colspan="2">
                            <RuleSetup:LowerUpperBoundControl ID="luBoundCtrl" runat="server" />
                        </td>
                        <td>
                            &nbsp
                            <!--Blank Column  -->
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                            <!--Blank Column -->
                        </td>
                        <td colspan="2" align="right">
                            <webControls:ExButton ID="btnSet" runat="server" LabelID="2017" OnClick="btnSet_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
