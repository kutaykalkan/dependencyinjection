﻿<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="AcctFltrRadioButtonListYesNoAll" Codebehind="AcctFltrRadioButtonListYesNoAll.ascx.cs" %>
<div>
    <div id="checkboxlistMaindiv" runat="server" style="width: auto; float: left;">
        <asp:RadioButtonList ID="rblCriteria" runat="server" RepeatDirection="Horizontal"
            CssClass="Black11Arial" CausesValidation="false" BorderColor="Gray" BorderStyle="Solid"
            BorderWidth="1px">
        </asp:RadioButtonList>
    </div>
    <webControls:ExRequiredFieldValidator ID="rfv" ControlToValidate="rblCriteria" runat="server"
        Enabled="false"></webControls:ExRequiredFieldValidator>
</div>
