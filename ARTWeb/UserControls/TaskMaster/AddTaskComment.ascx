<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_TaskMaster_AddTaskComment" Codebehind="AddTaskComment.ascx.cs" %>

<script type="text/javascript" language="javascript">
    function checkLength(txtBox) {
        //        if (txtBox) {
        //            if (txtBox.value.length <= (200-1))
        //                return true;
        //            else
        //                return false;
        //        }        
    }
</script>

<table width="100%">
    <col width="30%" />
    <col width="10%" />
    <col width="60%" />
    <tr>
        <td align="left">
            <%--<webControls:ExTextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="100%"
                MaxLength="200" Rows="2">
                <TextBox TextMode="MultiLine" ID="txtComments_TextBox"></TextBox>
            </webControls:ExTextBox>--%>
            <webControls:ExTextBox ID="txtComments" TextMode="MultiLine" MaxLength="200" Rows="3"
                runat="server" onkeypress="javascript:return checkLength(this);" SkinID="ExMulitilineTextBoxSignoffComment" ></webControls:ExTextBox>
        </td>
        <td>
            &nbsp;
        </td>
        <td align="left">
            <asp:Panel ID="pnlAttachmnet" runat="server">
                <table>
                    <col width="65%" valign="middle" />
                    <col width="5%" valign="middle" />
                    <col width="30%" valign="middle" />
                    <tr>
                        <td align="center">
                            <webControls:ExLabel runat="server" ID="lblDocuments" FormatString="{0}:" LabelID="1392"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <%--<webControls:ExLabel runat="server" ID="lblCountAttachedDocument" SkinID="ItemCount"></webControls:ExLabel>--%>
                        </td>
                        <td>
                        &nbsp;
                        </td>
                        <td align="left" valign="top">
                            <webControls:ExHyperLink ID="hlAttachment" runat="server" SkinID="ShowDocumentPopupHyperLink" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
