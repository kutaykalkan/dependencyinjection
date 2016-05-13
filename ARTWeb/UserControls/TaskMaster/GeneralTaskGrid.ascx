<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GeneralTaskGrid.ascx.cs"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_GeneralTaskGrid" %>
<table width="100%" cellpadding="0px" cellspacing="0px">
    <tr>
        <td style="width: 98%">
            <telerikWebControls:ExRadGrid ID="rgGeneralTasks" Width="100%" runat="server" OnItemDataBound="rgGeneralTasks_ItemDataBound"
                OnNeedDataSource="rgGeneralTasks_NeedDataSource" ClientSettings-Selecting-AllowRowSelect="true"
                AllowMultiRowSelection="true" AllowSorting="true" AllowPaging="true" OnItemCommand="rgGeneralTasks_ItemCommand"
                OnPageIndexChanged="rgGeneralTasks_PageIndexChanged" OnItemCreated="rgGeneralTasks_ItemCreated"
                OnPageSizeChanged="rgGeneralTasks_PageSizeChanged" AllowCauseValidationExportToExcel="false"
                OnPdfExporting="rgGeneralTasks_PdfExporting"  
                AllowCauseValidationExportToPDF="false">
                <ClientSettings>
                    <Selecting UseClientSelectColumnOnly="true" />
                </ClientSettings>
                <MasterTableView Width="100%" CellPadding="0" CellSpacing="0" ShowFooter="true" TableLayout="Auto"
                    Name="TasksGridView" DataKeyNames="TaskID,TaskDetailID,RecPeriodID,TaskStatusID">
                    <PagerTemplate>
                        <asp:Panel ID="PagerPanel" runat="server">
                            <asp:Panel runat="server" ID="pnlPageSizeDDL">
                                <div style="float: left; margin-right: 10px;">
                                    <webControls:ExLabel ID="lblPageSize" runat="server" LabelID="2493"></webControls:ExLabel>
                                    <asp:DropDownList ID="ddlPageSize" SkinID="DropDownList50" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="NumericPagerPlaceHolder" />
                        </asp:Panel>
                    </PagerTemplate>
                    <GroupHeaderTemplate>
                        <%--   <table>
                            <tr>
                                <td>--%>
<%--                        <webControls:ExLabel runat="server" ID="lblTaskListName" LabelID="2584" FormatString="{0}:"></webControls:ExLabel>--%>
                        <webControls:ExLabel runat="server" ID="lblTaskListNameVal" SkinID="Black9Arial"></webControls:ExLabel>
                        <%--</td>
                                <td>--%>
                        <webControls:ExHyperLink ID="hlEditTaskList" SkinID="GridHyperLinkImageEdit" runat="server"></webControls:ExHyperLink>
                        <%-- </td>
                            </tr>
                        </table>--%>
                    </GroupHeaderTemplate>
                    <Columns>
                        <%--Task Status Image--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="ImageColumn">
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td style="width: 22px">
                                            <webControls:ExHyperLink ID="hlCompletedStatus" runat="server" ToolTipLabelID="2559"
                                                SkinID="GridHyperLinkImageWithCompletedStatus"></webControls:ExHyperLink>
                                            <webControls:ExHyperLink ID="hlPendingStatus" runat="server" ToolTipLabelID="2561"
                                                SkinID="GridHyperLinkImageWithPendingStatus"></webControls:ExHyperLink>
                                            <webControls:ExHyperLink ID="hlOverDueStatus" runat="server" ToolTipLabelID="2562"
                                                SkinID="GridHyperLinkImageWithOverDueStatus"></webControls:ExHyperLink>
                                            <webControls:ExHyperLink ID="hlHiddenStatus" runat="server" ToolTipLabelID="2617"
                                                SkinID="GridHyperLinkImageWithHiddenStatus"></webControls:ExHyperLink>
                                            <webControls:ExHyperLink ID="hlDeletedStatus" runat="server" ToolTipLabelID="2646"
                                                SkinID="GridHyperLinkImageWithDeleteStatus"></webControls:ExHyperLink>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" Visible="true"
                            HeaderStyle-Width="5%">
                        </telerikWebControls:ExGridClientSelectColumn>
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
                            DataType="System.Int16">
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
                        <%--   <telerikWebControls:ExGridTemplateColumn UniqueName="TaskDuration" LabelID="2548"
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
                            <HeaderStyle HorizontalAlign="Left" Width="15%" BorderWidth="0" />
                            <ItemStyle HorizontalAlign="Left" BorderWidth="0" />
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
                                    <webControls:ExHyperLink ID="hlAttachment" runat="server" SkinID="ShowDocumentPopupHyperLink"></webControls:ExHyperLink>
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
                        <telerikWebControls:ExGridTemplateColumn UniqueName="TaskCustomField1"  SortExpression="TaskCustomField1"
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
                        <%--  <telerikWebControls:ExGridTemplateColumn UniqueName="Edit" DataType="System.String">
                            <ItemTemplate>
                                <webControls:ExHyperLink ID="hlEdit" SkinID="GridHyperLinkImageEdit" runat="server"></webControls:ExHyperLink>
                                <webControls:ExHyperLink ID="hlReadOnly" SkinID="GridHyperLinkImageReadOnly" runat="server"></webControls:ExHyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="15%" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerikWebControls:ExGridTemplateColumn>--%>
                        <%--Delete Task Load--%>
                        <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="5%" DataType="System.String"
                            ItemStyle-HorizontalAlign="Center" UniqueName="DeleteTaskLoad">
                            <ItemTemplate>
                                <webControls:ExImageButton CommandName="DeleteTaskLoad" ToolTipLabelID="2680" ID="btnDeleteTaskLoad" runat="server"
                                    SkinID="DeleteTaskLoadIcon" />
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                    </Columns>
                    <GroupByExpressions>
                        <telerik:GridGroupByExpression>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="TaskListName" />
                                <telerik:GridGroupByField FieldName="TaskListID" />
                                <telerik:GridGroupByField FieldName="TaskListAddedBy" />
                            </GroupByFields>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="TaskListName" />
                            </SelectFields>
                        </telerik:GridGroupByExpression>
                        <telerik:GridGroupByExpression>
                            <GroupByFields>
                                <telerik:GridGroupByField FieldName="TaskSubListName" />
                                <telerik:GridGroupByField FieldName="TaskSubListID" />
                                <telerik:GridGroupByField FieldName="TaskSubListAddedBy" />
                            </GroupByFields>
                            <SelectFields>
                                <telerik:GridGroupByField FieldName="TaskSubListName" />
                            </SelectFields>
                        </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                </MasterTableView>
                <FooterStyle HorizontalAlign="Right" />
            </telerikWebControls:ExRadGrid>
        </td>
    </tr>
</table>
<script language="javascript" type="text/javascript">
    function ConfirmDeletion(msg) {
        var answer = confirm(msg);
        if (answer) {
            return true;
        }
        else {
            return false;
        }
    }

</script>
