<%@ Page Title="Untitled Page" Language="C#" MasterPageFile="~/MasterPages/ReportViewerPrint.master"
    AutoEventWireup="true" CodeFile="TaskCompletionReportPrint.aspx.cs" Inherits="Pages_Reports_TaskCompletionReportPrint"
    Theme="SkyStemBlueBrown" %>

<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphRptViewer" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">
                    <UserControl:SkyStemARTGrid ID="ucSkyStemARTGrid" runat="server" OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound"
                        ShowSerialNumberColumn="false">
                        <skystemgridcolumncollection>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1357" SortExpression="AccountNumber"
                                DataType="System.String" UniqueName="AccountNumber">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAccountNumber" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1346" SortExpression="AccountName"
                                DataType="System.String" UniqueName="AccountName">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAccountName" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2584" SortExpression="TaskList.TaskListName"
                                UniqueName="TaskListID">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblTaskListName" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2544" SortExpression="TaskNumber"
                                UniqueName="TaskNumber">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblTaskNumber" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2545" SortExpression="TaskName"
                                UniqueName="TaskName">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblTaskName" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1408" SortExpression="TaskDescription"
                                UniqueName="Description">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblTaskDescription" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="DateCreated" LabelID="2557"
                                SortExpression="DateAdded" DataType="System.DateTime">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDateCreated" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="CreatedBy" LabelID="2556" SortExpression="TaskDetailAddedByUser.Name"
                                DataType="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblCreatedBy" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
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
                            <telerikWebControls:ExGridTemplateColumn UniqueName="TaskStatus" LabelID="2576" SortExpression="TaskStatus"
                                DataType="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblTaskStatus" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="TaskOwner" LabelID="2550" SortExpression="TaskOwner"
                                DataType="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAssignedTo" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                              <telerikWebControls:ExGridTemplateColumn UniqueName="TaskReviewer" LabelID="1131" SortExpression="TaskReviewer"
                                DataType="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblTaskReviewer" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="TaskApprover" LabelID="1132"
                                SortExpression="TaskApprover" DataType="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblTaskApprover" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="DateDone" LabelID="2722" SortExpression="DateDone"
                                DataType="System.DateTime">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDateDone" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="DoneBy" LabelID="2724" SortExpression="TaskDetailDoneByUser.Name"
                                DataType="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDoneBy" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>

                            <telerikWebControls:ExGridTemplateColumn UniqueName="DateReviewed" LabelID="2641" SortExpression="DateReviewed"
                                DataType="System.DateTime">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDateReviewed" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="ReviewedBy" LabelID="2640" SortExpression="TaskDetailReviewedByUser.Name"
                                DataType="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblReviewedBy" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>

                            <telerikWebControls:ExGridTemplateColumn UniqueName="DateApproved" LabelID="1435"
                                SortExpression="DateApproved" DataType="System.DateTime">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDateApproved" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="ApprovedBy" LabelID="2642" SortExpression="TaskDetailApprovedByUser.Name"
                                DataType="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblApprovedBy" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </skystemgridcolumncollection>
                    </UserControl:SkyStemARTGrid>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
