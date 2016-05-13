<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AccountTaskGrid.ascx.cs"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_AccountTaskGrid" %>
<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<table width="100%" cellpadding="0px" cellspacing="0px">
    <tr>
        <td style="width: 100%">
            <UserControl:SkyStemARTGrid ID="ucSkyStemARTAccountTaskGrid" runat="server" Grid-AllowRefresh="false"
                Grid-AllowPaging="True" ShowTaskStatusImage="true" OnGridCommand="ucAccountTaskGrid_GridCommand"
                BindSkipedRecords="true">
                <SkyStemGridMasterTableView>
                    <GroupHeaderTemplate>
                        <%--<table>
                            <tr>
                                <td>--%>
<%--                        <webControls:ExLabel runat="server" ID="lblTaskListName" LabelID="2584" FormatString="{0}:"></webControls:ExLabel>--%>
                        <webControls:ExLabel runat="server" ID="lblTaskListNameVal" SkinID="Black9Arial"></webControls:ExLabel>
                        <%-- </td>
                                <td>--%>
                        <webControls:ExHyperLink ID="hlEditTaskList" SkinID="GridHyperLinkImageEdit" runat="server"></webControls:ExHyperLink>
                        <%--  </td>
                            </tr>
                        </table>--%>
                    </GroupHeaderTemplate>
                </SkyStemGridMasterTableView>
                <SkyStemGridColumnCollection>
                    <telerikWebControls:ExGridTemplateColumn LabelID="1357" SortExpression="AccountNumber"
                        DataType="System.String" UniqueName="AccountNumber" Visible="false">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlAccountNumber" runat="server" SkinID="GridHyperLink" />
                        </ItemTemplate>
                        <HeaderStyle Width="6%" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <telerikWebControls:ExGridTemplateColumn LabelID="1346" SortExpression="AccountName"
                        DataType="System.String" UniqueName="AccountName" Visible="false">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlAccountName" runat="server" SkinID="GridHyperLink" />
                        </ItemTemplate>
                        <HeaderStyle Width="14%" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--TaskNumber--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="TaskNumber" LabelID="2544" SortExpression="TaskNumber"
                        DataType="System.String">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlTaskNumber" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Task Name--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="TaskName" LabelID="2545" SortExpression="TaskName"
                        DataType="System.String">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlTaskName" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Task Status --%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="TaskStatus" LabelID="2576" SortExpression="TaskStatus"
                        DataType="System.String">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlTaskStatus" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Task Description--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="Description" LabelID="1408"
                        SortExpression="TaskDescription" DataType="System.String">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlDescription" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Documents--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="AttachmentCount" LabelID="2631"
                        SortExpression="CreationDocCount" DataType="System.Int32">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlDocs" runat="server" SkinID="ShowDocumentPopupHyperLink"></webControls:ExHyperLink>
                            <webControls:ExHyperLink ID="hlAttachmentCount" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle Width="5%" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Start Date--%>
                    <%--  <telerikWebControls:ExGridTemplateColumn UniqueName="StartDate" LabelID="1449" SortExpression="TaskStartDate"
                        DataType="System.DateTime">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlStartDate" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle Width="10%" />
                    </telerikWebControls:ExGridTemplateColumn>--%>
                    <%--Due Date--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="AssigneeDueDate" LabelID="2567" SortExpression="AssigneeDueDate"
                        DataType="System.DateTime">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlAssigneeDueDate" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle Width="10%" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="TaskReviewerDueDate" LabelID="1418" SortExpression="ReviewerDueDate"
                        DataType="System.DateTime">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlReviewerDueDate" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle Width="10%" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="DueDate" LabelID="1499" SortExpression="TaskDueDate"
                        DataType="System.DateTime">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlDueDate" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle Width="10%" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%-- Task Duration --%>
                    <%--     <telerikWebControls:ExGridTemplateColumn UniqueName="TaskDuration" LabelID="2548"
                        SortExpression="TaskDueDays" DataType="System.Int32">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlTaskDuration" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle Width="5%" />
                    </telerikWebControls:ExGridTemplateColumn>--%>
                    <%--Recurrence Type--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="RecurrenceType" LabelID="2549"
                        SortExpression="RecurrenceType.RecurrenceType" DataType="System.String">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlRecurrenceType" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                            <webControls:ExHyperLink ID="hlTaskRecurrence" runat="server" SkinID="PopupRecFrequency"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Task Owner--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="TaskOwner" LabelID="2550"
                        DataType="System.String">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlTaskOwner" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Task Owner--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="TaskReviewer" LabelID="1131"
                        DataType="System.String">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlTaskReviewer" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Approver--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="TaskApprover" LabelID="1132"
                        DataType="System.String">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlApprover" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Comment--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="Comment" LabelID="2587" SortExpression="Comment"
                        DataType="System.String">
                        <ItemTemplate>
                            <webControls:ExLabel ID="lblComment" runat="server"></webControls:ExLabel>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="CommentIcon" DataType="System.String">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlComment" runat="server" SkinID="GridHyperLinkImageComment"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%-- Approval Duration --%>
                    <%--<telerikWebControls:ExGridTemplateColumn UniqueName="ApprovalDuration" LabelID="2552"
                        SortExpression="ApprovalDuration" DataType="System.Int32">
                        <ItemTemplate>
                            <webControls:ExLabel ID="lblApprovalDuration" runat="server"></webControls:ExLabel>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle Width="5%" />
                    </telerikWebControls:ExGridTemplateColumn>--%>
                    <%--CompletionDate--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="CompletionDate" LabelID="2553"
                        SortExpression="CompletionDate" DataType="System.DateTime">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlCompletionDate" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle Width="10%" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Completion Docs--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="CompletionDocs" LabelID="2554"
                        SortExpression="CompletionDocCount" DataType="System.Int32">
                        <ItemTemplate>
                            <span style="display: block; white-space: nowrap; vertical-align: inherit !important">
                                <webControls:ExHyperLink ID="hlAttachment" runat="server" SkinID="ShowDocumentPopupHyperLink" />
                                <webControls:ExHyperLink ID="hlCompletionDocs" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                            </span>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle Width="5%" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Created By--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="CreatedBy" LabelID="2556" SortExpression="TaskDetailAddedByUser.Name"
                        DataType="System.String">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlCreatedBy" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Date Created--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="DateCreated" LabelID="2557"
                        SortExpression="CompletionDate" DataType="System.DateTime">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlDateCreated" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle Width="10%" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Revised By--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="RevisedBy" LabelID="1543" SortExpression="TaskDetailRevisedByUser.Name"
                        DataType="System.String">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlRevisedBy" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Date Revised--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="DateRevised" LabelID="1552"
                        SortExpression="DateRevised" DataType="System.DateTime">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlDateRevised" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle Width="10%" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Task Custom Field 1--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="TaskCustomField1" SortExpression="TaskCustomField1"
                        DataType="System.String">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlTaskCustomField1" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Task Custom Field 1--%>
                    <telerikWebControls:ExGridTemplateColumn UniqueName="TaskCustomField2" SortExpression="TaskCustomField2"
                        DataType="System.String">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlTaskCustomField2" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerikWebControls:ExGridTemplateColumn>
                    <%--Edit--%>
                    <%-- <telerikWebControls:ExGridTemplateColumn UniqueName="Edit" DataType="System.String">
                        <ItemTemplate>
                            <webControls:ExHyperLink ID="hlEdit" SkinID="GridHyperLinkImageEdit" runat="server"></webControls:ExHyperLink>
                            <webControls:ExHyperLink ID="hlReadOnly" SkinID="GridHyperLinkImageReadOnly" runat="server"></webControls:ExHyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </telerikWebControls:ExGridTemplateColumn>--%>
                </SkyStemGridColumnCollection>
            </UserControl:SkyStemARTGrid>
        </td>
    </tr>
</table>
