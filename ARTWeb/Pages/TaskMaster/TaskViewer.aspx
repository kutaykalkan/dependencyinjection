<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" Inherits="Pages_TaskMaster_TaskViewer" Title="Untitled Page"
    Theme="SkyStemBlueBrown" ValidateRequest="false" Codebehind="TaskViewer.aspx.cs" %>

<%@ Register TagName="GeneralTaskGrid" TagPrefix="userControl" Src="~/UserControls/TaskMaster/GeneralTaskGrid.ascx" %>
<%@ Register TagName="AccountTaskGrid" TagPrefix="userControl" Src="~/UserControls/TaskMaster/AccountTaskGrid.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="Legend" Src="~/UserControls/TaskMaster/LegendOnTaskViewer.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<asp:Content ID="cphTask" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table Width="100%" style="table-layout: fixed;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right" style="padding: 10px">
                <webControls:ExLabel ID="lblTaskCategory" runat="server" LabelID="2610" SkinID="Black11Arial"
                    FormatString="{0}:"></webControls:ExLabel>
                <asp:DropDownList ID="ddlTaskCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTaskCategory_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="BlankRow">
        </tr>
        <tr>
            <td class="TDTabEmptyArea">
                <telerikWebControls:ExRadTabStrip Skin="SkyStemBlueBrown" EnableEmbeddedSkins="false"
                    ID="rtsTabs" runat="server" ReorderTabsOnSelect="true" Align="Justify" MultiPageID="rmpTabPages"
                    Width="500px">
                    <Tabs>
                        <telerikWebControls:ExRadTab LabelID="2547" Width="300px" PageViewID="rpvGeneralTask">
                        </telerikWebControls:ExRadTab>
                        <telerikWebControls:ExRadTab LabelID="2546" Width="300px" PageViewID="rpvAccountTask">
                        </telerikWebControls:ExRadTab>
                    </Tabs>
                </telerikWebControls:ExRadTabStrip>
            </td>
            <td class="TDTabEmptyArea">
                &nbsp;
            </td>
        </tr>
        <tr class="BlankRow">
        </tr>
        <tr>
            <td colspan="2" class="TDTabPageArea">
                <telerikWebControls:ExRadMultiPage Width="100%" Height="100%" ID="rmpTabPages" runat="server"
                    SelectedIndex="0">
                    <telerikWebControls:ExRadPageView ID="rpvGeneralTask" runat="server">
                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="InputRequrementsHeading" width="100%"
                                        border="0">
                                        <%--Pending/Overdue Tasks--%>
                                        <tr>
                                            <td align="left">
                                                <webControls:ExLabel ID="lblGeneralPendingOverdueTasks" runat="server" LabelID="2574"
                                                    SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <td align="right">
                                                <webControls:ExImage ID="imgCollapseGeneralPendingOverdue" runat="server" SkinID="CollapseIcon" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlGeneralPendingOverdueTasks" runat="server" ScrollBars="Auto" >
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="center">
                                                    <webControls:ExCheckBox ID="chkShowAllPendingGeneralTask" SkinID="CheckboxWithLabelBold"
                                                        LabelID="2725" runat="server" AutoPostBack="true" OnCheckedChanged="chkShowAllPendingGeneralTask_OnCheckedChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <userControl:GeneralTaskGrid ID="ucGeneraltaskGridPending" runat="server" AllowCustomFilter="true"
                                                        AllowCustomPaging="true" AllowExportToExcel="true" AllowExportToPDF="true" AllowSelectionPersist="false"
                                                        BasePageTitleLabelID="2547" EditMode="Edit" IsOnPage="true" IsPrintMode="false"
                                                        OnGridCommand="ucGeneraltaskGridPending_GridCommand" Visible="true" GridApplyFilterOnClientClick="ShowFilterPageGeneralTaskGridPending(); return false;"
                                                        AllowCustomization="true" GridCustomizeOnClientClick="ShowCustomizationPageForGTPendingGrid(); return false;"
                                                        GridType="GeneralTaskPending" ShowEditColumn="true" AllowActionMenu="true" AllowCustomImport="true"
                                                        GridApplyImportOnClientClick="ShowGeneralTaskImportPage();return false;" AllowCustomAdd="true"
                                                        AllowCustomEdit="true" AllowCustomDelete="true" AllowCustomApprove="true" AllowCustomReject="true" AllowCustomReview="true"
                                                        AllowCustomDone="true" GridApplyAddOnClientClick="ShowAddPageForGTPendingGrid(); return false;"
                                                        GridApplyEditOnClientClick="ShowEditPageForGTPendingGrid(); return false;" GridApplyDeleteOnClientClick="ShowDeletePageForGTPendingGrid(); return false;"
                                                        GridApplyApproveOnClientClick="ShowApprovePageForGTPendingGrid(); return false;"
                                                        GridApplyRejectOnClientClick="ShowRejectPageForGTPendingGrid(); return false;"
                                                        GridApplyDoneOnClientClick="ShowDonePageForGTPendingGrid(); return false;" 
                                                        GridApplyReviewOnClientClick="ShowReviewPageForGTPendingGrid(); return false;" 
                                                        ShowSelectCheckBoxColum="false"
                                                        DeleteTaskLoad="true">
                                                    </userControl:GeneralTaskGrid>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr class="BlankRow">
                            </tr>
                            <tr>
                                <td>
                                    <%--Completed Tasks--%>
                                    <table cellpadding="0" cellspacing="0" class="InputRequrementsHeading" width="100%">
                                        <tr>
                                            <td align="left">
                                                <webControls:ExLabel ID="lblGeneralCompletedTasks" runat="server" LabelID="2575"
                                                    SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <td align="right">
                                                <webControls:ExImage ID="imgCollapseGeneralCompletedTask" runat="server" SkinID="CollapseIcon" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="BlankRow">
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlGeneralCompletedTasks" runat="server" ScrollBars="Auto" >
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="center">
                                                    <webControls:ExCheckBox ID="chkShowGeneralHiddenTask" runat="server" LabelID="2611"
                                                        SkinID="CheckboxWithLabelBold" AutoPostBack="true" OnCheckedChanged="chkShowHiddenTask_OnCheckedChanged">
                                                    </webControls:ExCheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <userControl:GeneralTaskGrid ID="ucGeneralTaskGridCompleted" runat="server" AllowCustomPaging="true"
                                                        AllowExportToExcel="true" AllowExportToPDF="true" AllowSelectionPersist="true"
                                                        BasePageTitleLabelID="2547" EditMode="Edit" IsOnPage="true" IsPrintMode="false"
                                                        Visible="true" OnGridCommand="ucGeneralTaskGridCompleted_GridCommand" AllowCustomFilter="true"
                                                        GridApplyFilterOnClientClick="ShowFilterPageGeneralTaskGridCompleted(); return false;"
                                                        GridType="GeneralTaskCompleted" AllowCustomization="true" 
                                                        GridCustomizeOnClientClick="ShowCustomizationPageForGTCompletedGrid(); return false;"                                                     
                                                        AllowActionMenu="false" Grid-AllowCustomHide="true" Grid-AllowCustomUnhide="true" AllowCustomReopen="true"
                                                        ShowEditColumn="true" AllowCustomDelete="true"
                                                        GridApplyDeleteOnClientClick="javascript:">
                                                    </userControl:GeneralTaskGrid>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <ajaxToolkit:CollapsiblePanelExtender ID="cpePendingOverdueTask" TargetControlID="pnlGeneralPendingOverdueTasks"
                            ImageControlID="imgCollapseGeneralPendingOverdue" CollapseControlID="imgCollapseGeneralPendingOverdue"
                            ExpandControlID="imgCollapseGeneralPendingOverdue" runat="server" SkinID="CollapsiblePanel">
                        </ajaxToolkit:CollapsiblePanelExtender>
                        <ajaxToolkit:CollapsiblePanelExtender ID="cpeAccountTask" TargetControlID="pnlGeneralCompletedTasks"
                            ImageControlID="imgCollapseGeneralCompletedTask" CollapseControlID="imgCollapseGeneralCompletedTask"
                            ExpandControlID="imgCollapseGeneralCompletedTask" runat="server" SkinID="CollapsiblePanel">
                        </ajaxToolkit:CollapsiblePanelExtender>
                    </telerikWebControls:ExRadPageView>
                    <telerikWebControls:ExRadPageView ID="rpvAccountTask" runat="server">
                        <%--Pending/Overdue Account Tasks--%>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="InputRequrementsHeading" width="100%">
                                        <tr>
                                            <td align="left">
                                                <webControls:ExLabel ID="lblAccountPendingOverdueTask" runat="server" LabelID="2574"
                                                    SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <td align="right">
                                                <webControls:ExImage ID="imgCollapseAccountPendingOverdueTask" runat="server" SkinID="CollapseIcon" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlAccountPendingOverdueTasks" runat="server" ScrollBars="Auto" Style="padding-left: 2px;
                                        padding-right: 2px" Width="1150px">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td align="center">
                                                    <webControls:ExCheckBox ID="chkShowAllPendingAccountTask" SkinID="CheckboxWithLabelBold" LabelID="2725"
                                                        runat="server" AutoPostBack="true" OnCheckedChanged="chkShowAllPendingAccountTask_OnCheckedChanged" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <userControl:AccountTaskGrid ID="ucAccountTaskGridPending" runat="server" AllowCustomFilter="true"
                                                        AllowCustomPaging="true" AllowExportToExcel="true" AllowExportToPDF="true" AllowSelectionPersist="false"
                                                        BasePageTitleLabelID="2547" EditMode="Edit" IsOnPage="true" IsPrintMode="false"
                                                        AllowCustomization="true" Visible="true" GridType="AccountTaskPending" ShowEditColumn="true"
                                                        AllowActionMenu="true" CustomGridApplyFilterOnClientClick="ShowFilterPageAccountTaskGridPending(); return false;"
                                                        OnCustomGridCommand="ucAccountTaskGridPending_GridCommand" AllowCustomAdd="true"
                                                        AllowCustomDelete="true" AllowCustomApprove="true" AllowCustomReject="true" AllowCustomDone="true" AllowCustomReview="true"
                                                        GridApplyAddOnClientClick="ShowAddPageForATPendingGrid(); return false;" GridApplyDeleteOnClientClick="ShowDeletePageForATPendingGrid(); return false;"
                                                        GridApplyApproveOnClientClick="ShowApprovePageForATPendingGrid(); return false;"
                                                        GridApplyRejectOnClientClick="ShowRejectPageForATPendingGrid(); return false;"
                                                        GridApplyDoneOnClientClick="ShowDonePageForATPendingGrid(); return false;" 
                                                        GridApplyReviewOnClientClick="ShowReviewPageForATPendingGrid(); return false;" 
                                                        ShowSelectCheckBoxColum="false">
                                                    </userControl:AccountTaskGrid>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <%--Completed Account Tasks--%>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="InputRequrementsHeading" width="100%">
                                        <tr>
                                            <td align="left">
                                                <webControls:ExLabel ID="lblCompletedTask" runat="server" LabelID="2575" SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <td align="right">
                                                <webControls:ExImage ID="imgCollapseAccountCompletedTask" runat="server" SkinID="CollapseIcon" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="center">
                                                <webControls:ExCheckBox ID="chkShowAccountHiddenTask" runat="server" LabelID="2611"
                                                    SkinID="CheckboxWithLabelBold" AutoPostBack="true" OnCheckedChanged="chkShowHiddenTask_OnCheckedChanged">
                                                </webControls:ExCheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlAccountCompletedTasks" runat="server" ScrollBars="Auto" Style="padding-left: 2px;
                                                    padding-right: 2px" Width="1150px">
                                                    <userControl:AccountTaskGrid ID="ucAccountTaskGridCompleted" runat="server" AllowCustomFilter="true"
                                                        AllowCustomPaging="true" AllowExportToExcel="true" AllowExportToPDF="true" AllowSelectionPersist="true"
                                                        BasePageTitleLabelID="2547" EditMode="Edit" IsOnPage="true" IsPrintMode="false"
                                                        AllowCustomization="true" Visible="true" GridType="AccountTaskCompleted" AllowActionMenu="false"
                                                        CustomGridApplyFilterOnClientClick="ShowFilterPageAccountTaskGridCompleted(); return false;"
                                                        OnCustomGridCommand="ucAccountTaskGridCompleted_GridCommand" Grid-AllowCustomHide="true"                                                       
                                                        Grid-AllowCustomUnhide="true" ShowEditColumn="true" AllowCustomReopen="true" 
                                                        GridApplyDeleteOnClientClick="javascript:" AllowCustomDelete="true" 
                                                        ShowSelectCheckBoxColum="true">
                                                     
                                                    </userControl:AccountTaskGrid>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" TargetControlID="pnlAccountPendingOverdueTasks"
                            ImageControlID="imgCollapseAccountPendingOverdueTask" CollapseControlID="imgCollapseAccountPendingOverdueTask"
                            ExpandControlID="imgCollapseAccountPendingOverdueTask" runat="server" SkinID="CollapsiblePanel">
                        </ajaxToolkit:CollapsiblePanelExtender>
                        <ajaxToolkit:CollapsiblePanelExtender ID="cpeAccountPendingOverdueTask" TargetControlID="pnlAccountCompletedTasks"
                            ImageControlID="imgCollapseAccountCompletedTask" CollapseControlID="imgCollapseAccountCompletedTask"
                            ExpandControlID="imgCollapseAccountCompletedTask" runat="server" SkinID="CollapsiblePanel">
                        </ajaxToolkit:CollapsiblePanelExtender>
                    </telerikWebControls:ExRadPageView>
                </telerikWebControls:ExRadMultiPage>
            </td>
        </tr>
        <tr class="BlankRow">
        </tr>
        <tr>
            <td colspan="2">
                <UserControls:Legend ID="ucLegend" runat="server" />
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hdIsRefreshData" Value="1" />

    <script language="javascript" type="text/javascript">
        function SetElementValue(docElementId, val) {

            document.getElementById(docElementId).value = val;
        }
    </script>

    <script type="text/javascript" language="javascript">
        function OnClientAddButtonClickedGeneralTask(sender, args) {
            var button = args.get_item();
            //alert(button.get_text());
            var funcName = button.get_attributes("<%=TaskViewerConstants.TOOLBAR_COMMAND_ADD_ATTRIBUTE_NAME_GENERAL_TASK %>").getAttribute("<%=TaskViewerConstants.TOOLBAR_COMMAND_ADD_ATTRIBUTE_NAME_GENERAL_TASK %>");
            eval(funcName);
        }
        function OnClientAddButtonClickedAccountTask(sender, args) {
            var button = args.get_item();
            //alert(button.get_text());
            var funcName = button.get_attributes("<%=TaskViewerConstants.TOOLBAR_COMMAND_ADD_ATTRIBUTE_NAME_ACCOUNT_TASK %>").getAttribute("<%=TaskViewerConstants.TOOLBAR_COMMAND_ADD_ATTRIBUTE_NAME_ACCOUNT_TASK %>");
            eval(funcName);
        }
        function ShowFilterPageGeneralTaskGridPending() {
            OpenRadWindowForHyperlink("<%=this.FilterPageURLGeneraltaskGridPending %>", 480, 600);
        }
        function ShowFilterPageGeneralTaskGridCompleted() {
            OpenRadWindowForHyperlink("<%=this.FilterPageURLGeneraltaskGridCompleted %>", 480, 600);
        }
        function ShowCustomizationPageForGTPendingGrid() {
            OpenRadWindowForHyperlink("<%=this.CustomizationURLForGTPending %>", 480, 600);
        }
        function ShowCustomizationPageForGTCompletedGrid() {
            OpenRadWindowForHyperlink("<%=this.CustomizationURLForGTCompleted %>", 480, 600);
        }
        function ShowFilterPageAccountTaskGridPending() {
            OpenRadWindowForHyperlink("<%=this.FilterPageURLAccounttaskGridPending %>", 480, 600);
        }
        function ShowFilterPageAccountTaskGridCompleted() {
            OpenRadWindowForHyperlink("<%=this.FilterPageURLAccounttaskGridCompleted %>", 480, 600);
        }
        function ShowGeneralTaskImportPage() {
            location.href = "GeneralTaskImport.aspx";
        }
        var tableView = null;
        function ddlPageSize_SelectedIndexChanged(ID, SourceID) {

            if ($find(SourceID) != null) {
                tableView = $find(SourceID).get_masterTableView();
            }
            if (tableView != null)
                tableView.set_pageSize(document.getElementById(ID).value);
        }
        //General Task
        function ShowAddPageForGTPendingGrid() {
            OpenRadWindowWithName("<%=this.AddPageURLGeneraltaskGridPending%>", 'EditAddTaskWindow', '580', '1050');
        }
        function ShowEditPageForGTPendingGrid() {
            OpenRadWindowWithName("<%=this.EditPageURLGeneraltaskGridPending%>", 'BulkEditTasks', '580', '1050');
        }
        function ShowDeletePageForGTPendingGrid() {
            OpenRadWindowWithName("<%=this.DeletePageURLGeneraltaskGridPending%>", 'BulkDeleteTasks', '580', '1050');
        }
        function ShowApprovePageForGTPendingGrid() {
            OpenRadWindowWithName("<%=this.ApprovePageURLGeneraltaskGridPending%>", 'BulkApproveTasks', '580', '1050');
        }
        function ShowRejectPageForGTPendingGrid() {
            OpenRadWindowWithName("<%=this.RejectPageURLGeneraltaskGridPending%>", 'BulkRejectTasks', '580', '1050');
        }
        function ShowDonePageForGTPendingGrid() {
            OpenRadWindowWithName("<%=this.DonePageURLGeneraltaskGridPending%>", 'BulkCompleteTasks', '580', '1050');
        }
        function ShowReviewPageForGTPendingGrid() {
            OpenRadWindowWithName("<%=this.ReviewPageURLGeneraltaskGridPending%>", 'BulkReviewTasks', '580', '1050');
        }
        //Account Task
        function ShowAddPageForATPendingGrid() {
            OpenRadWindowWithName("<%=this.AddPageURLAccounttaskGridPending%>", 'EditAddTaskWindow', '580', '1050');
        }
        function ShowDeletePageForATPendingGrid() {
            OpenRadWindowWithName("<%=this.DeletePageURLAccounttaskGridPending%>", 'BulkDeleteTasks', '580', '1050');
        }
        function ShowApprovePageForATPendingGrid() {
            OpenRadWindowWithName("<%=this.ApprovePageURLAccounttaskGridPending%>", 'BulkApproveTasks', '580', '1050');
        }
        function ShowRejectPageForATPendingGrid() {
            OpenRadWindowWithName("<%=this.RejectPageURLAccounttaskGridPending%>", 'BulkRejectTasks', '580', '1050');
        }
        function ShowDonePageForATPendingGrid() {
            OpenRadWindowWithName("<%=this.DonePageURLAccounttaskGridPending%>", 'BulkCompleteTasks', '580', '1050');
        }                      
        function ShowReviewPageForATPendingGrid() {
            OpenRadWindowWithName("<%=this.ReviewPageURLAccounttaskGridPending%>", 'BulkReviewTasks', '580', '1050');
        }
    </script>

</asp:Content>
