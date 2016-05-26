<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="SimpleScrollableCheckBoxList" Codebehind="SimpleScrollableCheckBoxList.ascx.cs" %>
<div id="checkboxlistMaindiv" runat="server" class="CheckBoxListDiv" style="width: auto;
    float: left;">
    <div id="divSelectAll" runat="server" class="CheckBoxListSelectALL" style="width: 200px;">
        <webControls:ExCheckBox ID="chkSelectAll" runat="server" LabelID="1262" />
    </div>
    <div id="divCheckBoxList" runat="server" class="CheckBoxList" style="width: 200px;
        height: 100px; overflow-y: scroll;">
        <asp:CheckBoxList ID="cblOptions" style="width: 200px;" runat="server" AutoPostBack="false" OnDataBound="cblOptions_DataBound">
        </asp:CheckBoxList>
    </div>
</div>
<webControls:ExCustomValidator ID="rfv" runat="server" ClientValidationFunction="validate"
    Enabled="false"></webControls:ExCustomValidator>

<script type="text/javascript" language="javascript">
    function validate() {
        arguments.IsValid = false;
    }
</script>

