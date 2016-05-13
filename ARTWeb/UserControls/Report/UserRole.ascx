<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserRole.ascx.cs" Inherits="UserRole" %>
<%@ Register Src="~/UserControls/Report/ScrollableCheckboxListWithSelectAll.ascx"
    TagPrefix="ucRpt" TagName="scrollableCheckboxList" %>
<%@ Register src="SimpleScrollableCheckBoxList.ascx" tagname="SimpleScrollableCheckBoxList" tagprefix="rptCriteria" %>
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
            <td valign="top">
                <webControls:ExLabel ID="lblCriteriaName" SkinID="Black11Arial" FormatString="{0}:"
                    LabelID="1278" runat="server"></webControls:ExLabel>
            </td>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <webControls:ExLabel ID="lblRole" SkinID="Black11Arial" FormatString="{0}:" LabelID="1278"
                                            runat="server"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <rptCriteria:SimpleScrollableCheckBoxList ID="ucRoles" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <webControls:ExButton ID="btnFetchUser" runat="server" LabelID="1867" 
                                onclick="btnFetchUser_Click" Width="90px" CausesValidation="false"/>
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <webControls:ExLabel ID="lblUser" SkinID="Black11Arial" FormatString="{0}:" LabelID="1204"
                                            runat="server"></webControls:ExLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <rptCriteria:SimpleScrollableCheckBoxList ID="ucUsers" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
