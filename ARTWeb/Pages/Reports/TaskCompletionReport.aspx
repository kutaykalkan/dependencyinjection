<%@ Page Title="Untitled Page" Language="C#" MasterPageFile="~/MasterPages/ReportViewer.master"
    AutoEventWireup="true" Inherits="Pages_Reports_TaskCompletionReport"
    Theme="SkyStemBlueBrown" Codebehind="TaskCompletionReport.aspx.cs" %>

<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphRptViewer" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">
                    <UserControl:SkyStemARTGrid ID="ucSkyStemARTGrid" runat="server" OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound"
                        OnGrid_NeedDataSourceEventHandler="ucSkyStemARTGrid_NeedDataSourceEventHandler"
                        Grid-AllowPaging="True" Grid-AllowExportToExcel="True" Grid-AllowExportToPDF="False"
                        ShowSerialNumberColumn="false">
                        <SkyStemGridColumnCollection>
                            <telerikwebcontrols:exgridtemplatecolumn labelid="1357" sortexpression="AccountNumber"
                                datatype="System.String" uniquename="AccountNumber">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAccountNumber" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>
                            <telerikwebcontrols:exgridtemplatecolumn labelid="1346" sortexpression="AccountName"
                                datatype="System.String" uniquename="AccountName">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAccountName" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>
                            <telerikwebcontrols:exgridtemplatecolumn labelid="2584" sortexpression="TaskList.TaskListName"
                                uniquename="TaskListID">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblTaskListName" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>
                            <telerikwebcontrols:exgridtemplatecolumn labelid="2544" sortexpression="TaskNumber"
                                uniquename="TaskNumber">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblTaskNumber" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>
                            <telerikwebcontrols:exgridtemplatecolumn labelid="2545" sortexpression="TaskName"
                                uniquename="TaskName">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblTaskName" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>
                            <telerikwebcontrols:exgridtemplatecolumn labelid="1408" sortexpression="TaskDescription"
                                uniquename="Description">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblTaskDescription" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>
                            <telerikwebcontrols:exgridtemplatecolumn uniquename="DateCreated" labelid="2557"
                                sortexpression="DateAdded" datatype="System.DateTime">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDateCreated" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>
                            <telerikwebcontrols:exgridtemplatecolumn uniquename="CreatedBy" labelid="2556" sortexpression="TaskDetailAddedByUser.Name"
                                datatype="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblCreatedBy" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>
                            <%--Task Custom Field 1--%>
                            <telerikwebcontrols:exgridtemplatecolumn uniquename="TaskCustomField1" sortexpression="TaskCustomField1"
                                datatype="System.String">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblTaskCustomField1" runat="server" SkinID="GridReportLabel"></webControls:ExLabel>  
                            </ItemTemplate>                      
                            </telerikwebcontrols:exgridtemplatecolumn>
                            <%--Task Custom Field 2--%>
                            <telerikwebcontrols:exgridtemplatecolumn uniquename="TaskCustomField2" sortexpression="TaskCustomField2"
                                datatype="System.String">
                            <ItemTemplate>
                               <webControls:ExLabel ID="lblTaskCustomField2" runat="server" SkinID="GridReportLabel"></webControls:ExLabel>
                            </ItemTemplate>                         
                            </telerikwebcontrols:exgridtemplatecolumn>
                            <telerikwebcontrols:exgridtemplatecolumn uniquename="TaskStatus" labelid="2576" sortexpression="TaskStatus"
                                datatype="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblTaskStatus" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>
                            <telerikwebcontrols:exgridtemplatecolumn uniquename="TaskOwner" labelid="2550" sortexpression="TaskOwner"
                                datatype="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAssignedTo" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>
                            <telerikwebcontrols:exgridtemplatecolumn uniquename="TaskReviewer" labelid="1131" sortexpression="TaskReviewer"
                                datatype="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblTaskReviewer" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>
                            <telerikwebcontrols:exgridtemplatecolumn uniquename="TaskApprover" labelid="1132"
                                sortexpression="TaskApprover" datatype="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblTaskApprover" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>
                            <telerikwebcontrols:exgridtemplatecolumn uniquename="DateDone" labelid="2722" sortexpression="DateDone"
                                datatype="System.DateTime">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDateDone" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>
                            <telerikwebcontrols:exgridtemplatecolumn uniquename="DoneBy" labelid="2724" sortexpression="TaskDetailDoneByUser.Name"
                                datatype="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDoneBy" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>

                            <telerikwebcontrols:exgridtemplatecolumn uniquename="DateReviewed" labelid="2641" sortexpression="DateReviewed"
                                datatype="System.DateTime">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDateReviewed" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>
                            <telerikwebcontrols:exgridtemplatecolumn uniquename="ReviewedBy" labelid="2640" sortexpression="TaskDetailReviewedByUser.Name"
                                datatype="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblReviewedBy" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>

                            <telerikwebcontrols:exgridtemplatecolumn uniquename="DateApproved" labelid="1435"
                                sortexpression="DateApproved" datatype="System.DateTime">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDateApproved" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>
                            <telerikwebcontrols:exgridtemplatecolumn uniquename="ApprovedBy" labelid="2642" sortexpression="TaskDetailApprovedByUser.Name"
                                datatype="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblApprovedBy" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikwebcontrols:exgridtemplatecolumn>
                        </SkyStemGridColumnCollection>
                    </UserControl:SkyStemARTGrid>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
