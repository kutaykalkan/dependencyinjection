<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SingleDate.ascx.cs" Inherits="UserControls_Report_SingleDate" %>
<div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="5%" />
        <col width="20%" />
        <col width="75%" />
        <tr class="BlankRow">
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <webControls:ExLabel ID="lblDate" SkinID="Black11Arial" FormatString="{0}:" runat="server"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExCalendar ID="clSingleDate" runat="server"></webControls:ExCalendar>
                <asp:CustomValidator ID="cvSingleDate" runat="server" Text="!" Font-Bold="true" ControlToValidate="clSingleDate"
                    ClientValidationFunction="ValidateDate">
                </asp:CustomValidator>
            </td>
        </tr>
    </table>
</div>

<script type="text/javascript" language="javascript">

    function ValidateDate(sender, args) {
        if (IsDate(document.getElementById(sender.controltovalidate).value)) {
            args.IsValid = true;
        }
        else {
            args.IsValid = false;
        }
    }
</script>

