<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" Theme="SkyStemBlueBrown"
    AutoEventWireup="true" Inherits="Pages_TaskMaster_AddViewTaskComments" Codebehind="AddViewTaskComments.aspx.cs" %>

<%@ Register Src="~/UserControls/TaskMaster/ViewTaskComments.ascx" TagName="ViewTaskComments"
    TagPrefix="UserControls" %>
<%@ Register Src="~/UserControls/TaskMaster/AddTaskComment.ascx" TagName="AddTaskComments"
    TagPrefix="UserControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table> 
        <tr>
            <td>
                <table class="blueBorder" cellspacing="0" cellpadding="0">
                    <col width="500px" />
                    <tr class="blueRow">
                        <td align="center" style="padding-left: 2px;">
                            <webControls:ExLabel ID="lblPrevComents" LabelID="1848" runat="server" Style="font-weight: bold;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <UserControls:ViewTaskComments ID="ucViewTaskComments" runat="Server" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Panel ID="pnlAddComment" runat="server">
                    <table>
                        <colgroup>
                            <col width="30%" />
                            <col width="70%" />
                            <col width="10%" />
                            <tr>
                                <td align="left" valign="top">
                                    <webControls:ExLabel ID="ExLabel1" runat="server" FormatString="{0}:" LabelID="1848" />
                                </td>
                                <td colspan="2" align="left">
                                    <UserControls:AddTaskComments ID="ucAddTaskComments" runat="Server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="right">
                                    <webControls:ExButton ID="btnSave" runat="server" LabelID="1315" OnClick="btnSave_Click" />
                                </td>
                            </tr>
                        </colgroup>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
