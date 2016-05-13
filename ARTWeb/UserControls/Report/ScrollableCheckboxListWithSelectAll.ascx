<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ScrollableCheckboxListWithSelectAll.ascx.cs"
    Inherits="ScrollableCheckboxListWithSelectAll" %>
<div>
    <asp:Panel ID="pnlScrollableCheckboxListWithSelectAll" runat="server">
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <col width="5%" />
            <col width="20%" />
            <col width="75%" />
            <%--<col width="5%" />--%>
            <tr class="BlankRow">
            </tr>
            <tr>
                <td class="ManadatoryField">
                    <asp:PlaceHolder ID="phMandatory" runat="server" Visible="false">*</asp:PlaceHolder>
                </td>
                <td valign="top">
                    <webControls:ExLabel ID="lblParamName" SkinID="Black11Arial" FormatString="{0}:"
                        runat="server"></webControls:ExLabel>
                </td>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                        <%--<col width="50%" />
                    <col width="50%" />--%>
                        <tr>
                            <td>
                                <div id="checkboxlistMaindiv" runat="server" class="CheckBoxListDiv" style="width: auto;
                                    float: left;">
                                    <div id="divSelectAll" runat="server" class="CheckBoxListSelectALL">
                                        <webControls:ExCheckBox ID="chkSelectAll" runat="server" LabelID="1262" />
                                    </div>
                                    <div id="divCheckBoxList" class="CheckBoxList" runat="server" style="height: 100px;
                                        overflow-y: scroll;">
                                        <asp:CheckBoxList ID="cblOptions" Width="100%" runat="server" AutoPostBack="false"
                                            OnDataBound="cblOptions_DataBound">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                                <webControls:ExCustomValidator ID="rfv" runat="server" ClientValidationFunction="validateOptions"
                                    Display="Dynamic" ErrorMessage="" Text="!" CssClass="validator"></webControls:ExCustomValidator>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>

<script type='text/javascript' language='javascript'>
    
    
</script>

