<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="SkyStem.ART.Web.UserControls.Matching.MatchingCombinationSelection" Codebehind="MatchingCombinationSelection.ascx.cs" %>
<webControls:ExLabel ID="lblDataSource" runat="server" LabelID="2308" FormatString="{0} :"
    SkinID="Black11Arial"></webControls:ExLabel>
&nbsp;
<asp:DropDownList ID="ddlMatchingCombination" runat="server" CausesValidation="true"
    AutoPostBack="true" SkinID="DropDownList450" 
    OnSelectedIndexChanged="ddlMatchingCombination_SelectedIndexChanged">
</asp:DropDownList>
<asp:HiddenField ID="hdnPreviousMatchSetSubSetCombinationID" runat="server" />
<asp:RequiredFieldValidator ID="vldMatchingCombination" runat="server" Visible="false"
    ControlToValidate="ddlMatchingCombination" Text="!" Font-Bold="true" Font-Size="Medium"
    Enabled="false">
</asp:RequiredFieldValidator>

<script language="javascript" type="text/javascript">
    function ValidatePageData() {
        if (typeof (Page_ClientValidate) == 'function')
            Page_ClientValidate();
        var hdnPrev = document.getElementById("<%=hdnPreviousMatchSetSubSetCombinationID.ClientID%>");
        if (typeof (Page_IsValid) != 'undefined' && !Page_IsValid) {
            $("#<%=ddlMatchingCombination.ClientID%>").val(hdnPrev.value);
        }
    }
</script>

