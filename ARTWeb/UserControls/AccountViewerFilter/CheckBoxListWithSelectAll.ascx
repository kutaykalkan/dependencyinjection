<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheckBoxListWithSelectAll.ascx.cs"
    Inherits="CheckBoxListWithSelectAll" %>
<div id="checkboxlistMaindiv" runat="server" class="CheckBoxListDiv"  style="width: auto; float: left;">
    <div id="divSelectAll" runat="server" class="CheckBoxListSelectALL" style="width: 225px;">
        <webControls:ExCheckBox ID="chkSelectAll" runat="server" LabelID="1262" />
    </div>
    <div id="divCheckBoxList" runat="server" class="CheckBoxList" style="width: 225px;
        height: 100px; overflow-y: scroll;">
        <asp:CheckBoxList ID="cblOptions" style="width: 225px;" runat="server" AutoPostBack="false" OnDataBound="cblOptions_DataBound">
        </asp:CheckBoxList>
    </div>
</div>
<webControls:ExCustomValidator ID="rfv" runat="server" ClientValidationFunction="validateOptions"
    Display="Dynamic" ErrorMessage="" Text="!" CssClass="validator"></webControls:ExCustomValidator>
