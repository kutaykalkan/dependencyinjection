<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DateRange.ascx.cs" Inherits="DateRange" %>
<div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="5%" />
        <col width="20%" />
        <col width="75%" />
        <tr class="BlankRow">
        </tr>
        <tr>
            <td class="ManadatoryField">
            </td>
            <td>
                <webControls:ExLabel ID="lblCriteriaName" SkinID="Black11Arial" FormatString="{0}:"
                    LabelID="1411" runat="server"></webControls:ExLabel>
            </td>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <col width="10%" />
                    <col width="40%" />
                    <col width="10%" />
                    <col width="40%" />
                    <tr>
                        <td>
                            <webControls:ExLabel ID="lblFrom" SkinID="Black11Arial" FormatString="{0}:" LabelID="1336"
                                runat="server"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExCalendar ID="calFrom" runat="server"></webControls:ExCalendar>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblTo" SkinID="Black11Arial" FormatString="{0}:" LabelID="1345"
                                runat="server"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExCalendar ID="calTo" runat="server" on></webControls:ExCalendar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>

<script type="text/javascript" language="javascript">
    function validate(source, arguments) {
        arguments.IsValid = true;
    }
</script>

