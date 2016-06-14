<%@ Control Language="C#" AutoEventWireup="true" Inherits="AcctFltrDateRange" Codebehind="DateRange.ascx.cs" %>
<div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="5%" />
        <col width="95%" />
        <tr class="BlankRow">
        </tr>
        <tr>
            <td class="ManadatoryField">
            </td>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <col width="8%" />
                    <col width="40%" />
                    <col width="5%" />
                    <col width="47%" />
                    <tr>
                        <td>
                            <webControls:ExLabel ID="lblFrom" SkinID="Black11Arial" FormatString="{0}:" LabelID="1336"
                                runat="server"></webControls:ExLabel>&nbsp;
                        </td>
                        <td style="white-space: nowrap;">
                            <webControls:ExCalendar ID="calFrom" runat="server" Width="100"></webControls:ExCalendar>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblTo" SkinID="Black11Arial" FormatString="{0}:" LabelID="1345"
                                runat="server"></webControls:ExLabel>&nbsp;
                        </td>
                        <td style="white-space: nowrap;">
                            <webControls:ExCalendar ID="calTo" runat="server" Width="100"></webControls:ExCalendar>
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

