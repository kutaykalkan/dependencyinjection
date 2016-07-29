<%@ Page Language="C#" AutoEventWireup="true" Inherits="Pages_CreateTask"
    MasterPageFile="~/MasterPages/PopUpMasterPage.master" Theme="SkyStemBlueBrown" Codebehind="CreateTask.aspx.cs" %>

<%@ Register TagPrefix="Popup" TagName="RecFrequency" Src="~/UserControls/PopupRecFrequency.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="RiskRatingDDL" Src="~/UserControls/RiskRatingDropDown.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<%@ Register Src="~/UserControls/AccountSearchControl.ascx" TagName="AccountSearchControl"
    TagPrefix="UserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="5%" />
        <col width="20%" />
        <col width="30%" />
        <col width="5%" />
        <col width="15%" />
        <col width="30%" />
        <asp:Panel ID="pnlCreatedBy" runat="server">
            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
            <tr>
                <td class="ManadatoryField"></td>
                <td>
                    <webControls:ExLabel ID="lblRecPeriod" runat="server" LabelID="1420" FormatString="{0}:"
                        SkinID="Black11Arial"></webControls:ExLabel>
                </td>
                <td>
                    <webControls:ExLabel ID="lblRecPeriodValue" runat="server" SkinID="Black11Arial"
                        Text=""></webControls:ExLabel>
                </td>
                <td></td>
                <td>
                    <webControls:ExLabel ID="lblTaskNumber" runat="server" LabelID="2544" SkinID="Black11Arial"
                        FormatString="{0}:"></webControls:ExLabel>
                </td>
                <td>
                    <webControls:ExLabel ID="lblTaskNumberVal" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                </td>
            </tr>
            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
            <tr>
                <td class="ManadatoryField"></td>
                <td>
                    <webControls:ExLabel ID="lblCreatedBy" runat="server" LabelID="2556" FormatString="{0}:"
                        SkinID="Black11Arial"></webControls:ExLabel>
                </td>
                <td>
                    <webControls:ExLabel ID="lblCreatedByVal" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                </td>
                <td></td>
                <td>
                    <webControls:ExLabel ID="lblDateCreated" runat="server" LabelID="2557" SkinID="Black11Arial"
                        FormatString="{0}:"></webControls:ExLabel>
                </td>
                <td>
                    <webControls:ExLabel ID="lblDateCreatedVal" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                </td>
            </tr>
        </asp:Panel>
        <tr class="BlankRow">
            <td colspan="6"></td>
        </tr>
        <tr>
            <td class="ManadatoryField">*
            </td>
            <td>
                <webControls:ExLabel ID="lblTaskListName" runat="server" LabelID="2584" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <asp:DropDownList ID="ddlTaskListName" runat="server" SkinID="DropDownList200" OnSelectedIndexChanged="ddlTaskListName_SelectedIndexChanged"
                    CausesValidation="false" AutoPostBack="true">
                </asp:DropDownList>
                <webControls:ExRequiredFieldValidator ID="rfvTaskListName" runat="server" InitialValue="-2"
                    ControlToValidate="ddlTaskListName"></webControls:ExRequiredFieldValidator>
            </td>
            <td></td>
            <td>
                <webControls:ExLabel ID="lblNewTaskListName" runat="server" LabelID="2579" SkinID="Black11Arial"
                    FormatString="{0}:"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExTextBox ID="txtNewTaskListName" MaxLength="100" IsRequired="true"
                    runat="server" SkinID="ExTextBox200" />
                <asp:CustomValidator ID="cvTaskListName" runat="server" Text="!" Font-Bold="true"
                    OnServerValidate="cvTaskListName_OnServerValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="6"></td>
        </tr>
        <tr>
            <td class="ManadatoryField"></td>
            <td>
                <webControls:ExLabel ID="lblTaskSubListName" runat="server" LabelID="2954" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <asp:DropDownList ID="ddlTaskSubListName" runat="server" SkinID="DropDownList200" OnSelectedIndexChanged="ddlTaskSubListName_SelectedIndexChanged"
                    CausesValidation="false" AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td></td>
            <td>
                <webControls:ExLabel ID="lblNewTaskSubListName" runat="server" LabelID="2955" SkinID="Black11Arial"
                    FormatString="{0}:"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExTextBox ID="txtNewTaskSubListName" MaxLength="100" runat="server" SkinID="ExTextBox200" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="6"></td>
        </tr>
        <tr>
            <td class="ManadatoryField">
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>*
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table width="100%" cellpadding="0" cellspacing="0" height="85PX" border="0">
                    <tr>
                        <td valign="top">
                            <webControls:ExLabel ID="lblTaskName" runat="server" LabelID="2545" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="BlankRow"></td>
                    </tr>
                    <tr>
                        <td>
                            <webControls:ExLabel ID="lblCustomField1" runat="server" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="BlankRow"></td>
                    </tr>
                    <tr>
                        <td>
                            <webControls:ExLabel ID="lblCustomField2" runat="server" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table width="100%" cellpadding="0" cellspacing="0" height="85PX" border="0">

                    <tr>
                        <td valign="top">
                            <webControls:ExTextBox ID="txtTaskName" runat="server" MaxLength="500" IsRequired="true"
                                SkinID="ExTextBox200" />
                            <webControls:ExLabel ID="lblTaskNameValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="BlankRow"></td>
                    </tr>
                    <tr>
                        <td>
                            <webControls:ExTextBox ID="txtCustomField1" runat="server" MaxLength="500"
                                SkinID="ExTextBox200" />
                            <webControls:ExLabel ID="lblCustomField1Value" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="BlankRow"></td>
                    </tr>
                    <tr>
                        <td>
                            <webControls:ExTextBox ID="txtCustomField2" runat="server" MaxLength="500"
                                SkinID="ExTextBox200" />
                            <webControls:ExLabel ID="lblCustomField2Value" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
            <td valign="top">
                <webControls:ExLabel ID="lblDescription" runat="server" LabelID="1408" SkinID="Black11Arial"
                    FormatString="{0}:"></webControls:ExLabel>
            </td>
            <td valign="top">
                <webControls:ExTextBox ID="txtDescription" runat="server" MaxLength="500" SkinID="ExMulitilineTextBox200" />
                <webControls:ExLabel ID="lblDescriptionValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="6"></td>
        </tr>


        <tr>
            <td class="ManadatoryField">*</td>
            <td>
                <webControls:ExLabel ID="lblAssignedTo" runat="server" LabelID="2564" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <asp:TextBox ID="txtAssignedTo" runat="server" SkinID="TextBox150" />
                <webControls:ExHyperLink ID="hlAssignedTo" LabelID="1607" Visible="true" runat="server"
                    ToolTipLabelID="1607" SkinID="AddUser" />
                <webControls:ExLabel ID="lblAssignedToValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                <asp:CustomValidator ID="cvAssignedTo" runat="server" Text="!" Font-Bold="true" ClientValidationFunction="ValidateAssignedTo"
                    OnServerValidate="cvAssignedTo_OnServerValidate"></asp:CustomValidator>
                <asp:CustomValidator ID="cvTaskAccountsAssignedTo" runat="server" Font-Size="Medium"
                    OnServerValidate="cvTaskAccountsAssignedTo_OnServerValidate"></asp:CustomValidator>
                <asp:CustomValidator ID="cvTaskUser" ClientValidationFunction="ValidateTaskUsers" runat="server" Text="!" Font-Bold="true" ControlToValidate="txtAssignedTo"
                    OnServerValidate="cvTaskUser_OnServerValidate"></asp:CustomValidator>
                <webControls:ExImageButton CommandName="ClearAssignee" ID="btnClearAssignedTo" ToolTipLabelID="2956"
                    runat="server" SkinID="ClearNotify" CausesValidation="false" OnClick="btnClearAssignedTo_Click" />
            </td>
            <td></td>
            <td>
                <webControls:ExLabel ID="lblReviewerLabel" runat="server" LabelID="1131" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <asp:TextBox ID="txtReviewer" runat="server" SkinID="TextBox150"></asp:TextBox>
                <webControls:ExHyperLink ID="hlReviewer" LabelID="1607" Visible="true" runat="server"
                    ToolTipLabelID="1607" SkinID="AddUser" />
                <webControls:ExLabel ID="lblReviewerValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                <asp:CustomValidator ID="cvTaskAccountsReviewer" runat="server" Font-Size="Medium"
                    OnServerValidate="cvTaskAccountsReviewer_OnServerValidate"></asp:CustomValidator>
                <%--       <asp:CustomValidator ID="cvTaskUserReviewer" runat="server" Text="!" Font-Bold="true" ControlToValidate="txtReviewer"
                    ClientValidationFunction="ValidateTaskUsers" OnServerValidate="cvTaskUser_OnServerValidate"></asp:CustomValidator>--%>
                <webControls:ExImageButton CommandName="ClearReviewer" ID="btnClearReviewer" ToolTipLabelID="2957"
                    runat="server" SkinID="ClearNotify" CausesValidation="false" OnClick="btnClearReviewer_Click" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="6"></td>
        </tr>
        <tr>
            <td class="ManadatoryField"></td>
            <td>
                <webControls:ExLabel ID="lblApproverLabel" runat="server" LabelID="1132" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <asp:TextBox ID="txtApprover" runat="server" SkinID="TextBox150"></asp:TextBox>
                <webControls:ExHyperLink ID="hlApprover" LabelID="1607" Visible="true" runat="server"
                    ToolTipLabelID="1607" SkinID="AddUser" />
                <webControls:ExLabel ID="lblApproverValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                <asp:CustomValidator ID="cvApprover" runat="server" Text="!" Font-Bold="true" ControlToValidate="txtApprover" ClientValidationFunction="ValidateApprover"
                    OnServerValidate="cvApprover_OnServerValidate"></asp:CustomValidator>
                <asp:CustomValidator ID="cvTaskAccountsApprover" runat="server" Font-Size="Medium"
                    OnServerValidate="cvTaskAccountsApprover_OnServerValidate"></asp:CustomValidator>
                <%--       <asp:CustomValidator ID="cvTaskUserApprover" runat="server" Text="!" Font-Bold="true" ControlToValidate="txtApprover"
                    ClientValidationFunction="ValidateTaskUsers" OnServerValidate="cvTaskUser_OnServerValidate"></asp:CustomValidator>--%>
                <webControls:ExImageButton CommandName="ClearApprover" ID="btnClearApprover" ToolTipLabelID="2958"
                    runat="server" SkinID="ClearNotify" CausesValidation="false" OnClick="btnClearApprover_Click" />

            </td>
            <td></td>
            <td>
                <webControls:ExLabel ID="lblNotify" runat="server" LabelID="2525" SkinID="Black11Arial"
                    FormatString="{0}:"></webControls:ExLabel>
            </td>
            <td>
                <asp:TextBox ID="txtNotify" runat="server" SkinID="TextBox150"></asp:TextBox>
                <webControls:ExHyperLink ID="hlNotify" LabelID="1607" Visible="true" runat="server"
                    ToolTipLabelID="1607" SkinID="AddUser" />
                <webControls:ExLabel ID="lblNotifyValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                <%--       <asp:CustomValidator ID="cvTaskUser" runat="server" Text="!" Font-Bold="true" ControlToValidate="txtNotify"
                    ClientValidationFunction="ValidateTaskUsers" OnServerValidate="cvTaskUser_OnServerValidate"></asp:CustomValidator>--%>
                <asp:CustomValidator ID="cvTaskAccountsNotify" runat="server" Font-Size="Medium"
                    OnServerValidate="cvTaskAccountsNotify_OnServerValidate"></asp:CustomValidator>
                <webControls:ExImageButton CommandName="ClearNotify" ID="btnClearNotify" ToolTipLabelID="2663"
                    runat="server" SkinID="ClearNotify" CausesValidation="false" OnClick="btnClearNotify_Click" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="6"></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <webControls:ExLabel ID="lblAttDocs" runat="server" LabelID="1392" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td colspan="4">
                <webControls:ExHyperLink ID="hlAttachment" runat="server" SkinID="ShowDocumentPopupHyperLink" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="6"></td>
        </tr>
        <tr>
            <td class="ManadatoryField">*
            </td>
            <td>
                <webControls:ExLabel ID="lblRecurrence" runat="server" LabelID="1861" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td colspan="4">
                <asp:DropDownList ID="ddlRecurrence" runat="server" SkinID="DropDownList200" OnSelectedIndexChanged="ddlRecurrence_SelectedIndexChanged"
                    CausesValidation="false" AutoPostBack="true">
                </asp:DropDownList>
            </td>
        </tr>
        <asp:Panel ID="pnlCustomRecurrenceType" runat="server">
            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
            <tr>
                <td class="ManadatoryField">*
                </td>
                <td valign="top">
                    <webControls:ExLabel ID="ExLabel2" runat="server" LabelID="1978" FormatString="{0}:"
                        SkinID="Black11Arial"></webControls:ExLabel>
                </td>
                <td>
                    <div id="Div1" runat="server" class="CheckBoxListDiv" style="width: 200px; height: 150px; overflow-y: scroll;">
                        <asp:CheckBoxList ID="cblRecPeriodsCustom" OnDataBound="cblRecPeriodsCustom_DataBinding"
                            runat="server" Height="80px" />
                    </div>
                </td>
                <td colspan="3">
                    <webControls:ExCustomValidator runat="server" OnServerValidate="cvcblRecPeriods_ServerValidate"
                        ID="cvcblRecPeriods">!</webControls:ExCustomValidator>
                </td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="pnlRecurrenceQuarterlyType" runat="server">
            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
            <tr>
                <td class="ManadatoryField">*
                </td>
                <td valign="top">
                    <webControls:ExLabel ID="ExLabel3" runat="server" LabelID="2735" FormatString="{0}:"
                        SkinID="Black11Arial"></webControls:ExLabel>
                </td>
                <td>
                    <div id="Div2" runat="server" class="CheckBoxListDiv" style="width: 200px; height: 150px; overflow-y: scroll;">
                        <asp:CheckBoxList ID="cblRecurrencePeriodNumber" runat="server" Height="80px" />
                    </div>
                </td>
                <td colspan="3">
                    <webControls:ExCustomValidator runat="server" OnServerValidate="cvcblRecPeriodNumbers_ServerValidate"
                        ID="cvcblRecPeriodNumbers">!</webControls:ExCustomValidator>
                </td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="pnlRecurrenceYearlyType" runat="server">
            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
            <tr>
                <td class="ManadatoryField">*
                </td>
                <td valign="top">
                    <webControls:ExLabel ID="ExLabel4" runat="server" LabelID="2735" FormatString="{0}:"
                        SkinID="Black11Arial"></webControls:ExLabel>
                </td>
                <td colspan="4">
                    <asp:DropDownList ID="ddlRecurrencePeriodNumber" runat="server" SkinID="DropDownList200">
                    </asp:DropDownList>
                    <webControls:ExRequiredFieldValidator ID="rfvRecurrencePeriodNumber" ControlToValidate="ddlRecurrencePeriodNumber"
                        runat="server" InitialValue="-2"></webControls:ExRequiredFieldValidator>
                </td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="pnlRecurrenceMRType" runat="server">
            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
            <tr>
                <td class="ManadatoryField">*
                </td>
                <td valign="top">
                    <webControls:ExLabel ID="ExLabel5" runat="server" LabelID="2735" FormatString="{0}:"
                        SkinID="Black11Arial"></webControls:ExLabel>
                </td>
                <td>
                    <div id="Div3" runat="server" class="CheckBoxListDiv" style="width: 200px; height: 150px; overflow-y: scroll;">
                        <asp:CheckBoxList ID="cblMRRecurrencePeriodNumber" runat="server" Height="80px" />
                    </div>
                </td>
                <td colspan="3" align="left">
                    <webControls:ExCustomValidator runat="server" OnServerValidate="cvcblMRRecPeriodNumbers_ServerValidate"
                        ID="cvcblMRRecPeriodNumbers">!</webControls:ExCustomValidator>
                </td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="PanelDueDate" runat="server">
            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
            <tr>
                <td class="ManadatoryField">*
                </td>
                <td>
                    <webControls:ExLabel ID="lblTaskDueDate" runat="server" LabelID="2566" SkinID="Black11Arial"
                        FormatString="{0}:"></webControls:ExLabel>
                </td>
                <td>
                    <webControls:ExCalendar ID="calTaskDueDate" runat="server" SkinID="ExCalendar100"></webControls:ExCalendar>
                    <webControls:ExRequiredFieldValidator ID="rfvCalenderTaskDueDate" runat="server"
                        ControlToValidate="calTaskDueDate"></webControls:ExRequiredFieldValidator>
                    <webControls:ExCustomValidator ID="cvcalTaskDueDate" runat="server" Text="!" ControlToValidate="calTaskDueDate"
                        ClientValidationFunction="ValidateIsDate" OnServerValidate="cvTaskDueDate_OnServerValidate"></webControls:ExCustomValidator>
                    <webControls:ExCustomValidator runat="server" OnServerValidate="cvIsOnHoliday_ServerValidate"
                        ID="cvTaskDueDateHoliday" ControlToValidate="calTaskDueDate" LabelID="5000085">!</webControls:ExCustomValidator>
                    <asp:CustomValidator ID="cvComparecalTaskDueDateWithCurrentDate" runat="server" Text="!"
                        Font-Bold="true" ControlToValidate="calTaskDueDate" ClientValidationFunction="CompareDateNotBeforeCurrentDate" OnServerValidate="cvComparecalTaskDueDateWithCurrentDate_OnServerValidate"></asp:CustomValidator>
                </td>
                <td class="ManadatoryField">
                    <%-- *--%>
                </td>
                <td>
                    <webControls:ExLabel ID="lblAssigneeDueDate" runat="server" LabelID="2567" FormatString="{0}:"
                        SkinID="Black11Arial"></webControls:ExLabel>
                </td>
                <td>
                    <webControls:ExCalendar ID="calAssigneeDueDate" runat="server" SkinID="ExCalendar100"></webControls:ExCalendar>
                    <%-- <webControls:ExRequiredFieldValidator ID="rfvCalenderAssigneeDueDate" runat="server"
                        ControlToValidate="calAssigneeDueDate"></webControls:ExRequiredFieldValidator>--%>
                    <webControls:ExCustomValidator ID="cvcalAssigneeDueDate" runat="server" Text="!"
                        ControlToValidate="calAssigneeDueDate" ClientValidationFunction="ValidateIsDate"
                        OnServerValidate="cvAssigneeDueDate_OnServerValidate"></webControls:ExCustomValidator>
                    <asp:CustomValidator ID="cvAssigneeDD" runat="server" Text="!" Font-Bold="true" OnServerValidate="cvAssigneeDD_OnServerValidate"></asp:CustomValidator>
                    <webControls:ExCustomValidator runat="server" OnServerValidate="cvIsOnHoliday_ServerValidate"
                        ID="cvAssigneeDueDateHoliday" ControlToValidate="calAssigneeDueDate" LabelID="5000085">!</webControls:ExCustomValidator>
                    <asp:CustomValidator ID="cvComparecalAssigneeDueDateWithCurrentDate" runat="server"
                        Text="!" Font-Bold="true" ControlToValidate="calAssigneeDueDate" ClientValidationFunction="CompareDateNotBeforeCurrentDate" OnServerValidate="cvCompareAssigneeDueDateWithCurrentDate_OnServerValidate"></asp:CustomValidator>

                </td>
            </tr>
            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
            <tr>
                <td class="ManadatoryField"></td>
                <td>
                    <webControls:ExLabel ID="lblReviewerDueDate" runat="server" LabelID="1418" SkinID="Black11Arial"
                        FormatString="{0}:"></webControls:ExLabel>
                </td>
                <td>
                    <webControls:ExCalendar ID="calReviewerDueDate" runat="server" SkinID="ExCalendar100"></webControls:ExCalendar>

                    <webControls:ExCustomValidator ID="cvReviewerDueDate" runat="server" Text="!" ControlToValidate="calReviewerDueDate"
                        ClientValidationFunction="ValidateIsDate" OnServerValidate="cvReviewerDueDate_OnServerValidate"></webControls:ExCustomValidator>
                    <webControls:ExCustomValidator runat="server" OnServerValidate="cvIsOnHoliday_ServerValidate"
                        ID="cvReviewerDueDateHoliday" ControlToValidate="calReviewerDueDate" LabelID="5000085">!</webControls:ExCustomValidator>
                    <asp:CustomValidator ID="cvComparecalReviewerDueDateWithCurrentDate" runat="server" Text="!"
                        Font-Bold="true" ControlToValidate="calReviewerDueDate" ClientValidationFunction="CompareDateNotBeforeCurrentDate" OnServerValidate="cvComparecalReviewerDueDateWithCurrentDate_OnServerValidate"></asp:CustomValidator>
                    <asp:CustomValidator ID="cvValidateTaskDates" ClientValidationFunction="ValidateTaskDates"
                        runat="server" Font-Size="Medium" OnServerValidate="cvValidateTaskDates_OnServerValidate"></asp:CustomValidator>
                     <asp:CustomValidator ID="cvReviewerDD" runat="server" Text="!" Font-Bold="true" OnServerValidate="cvReviewerDD_OnServerValidate"></asp:CustomValidator>
                </td>
                <td class="ManadatoryField"></td>
                <td></td>
                <td></td>
            </tr>
            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
        </asp:Panel>
        <asp:Panel ID="PanelDueDays" runat="server">
            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
            <tr>
                <td class="ManadatoryField">*
                </td>
                <td>
                    <webControls:ExLabel ID="lblTaskDueDayaBasis" runat="server" LabelID="2777" SkinID="Black11Arial"
                        FormatString="{0}:"></webControls:ExLabel>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTaskDueDaysBasis" runat="server" SkinID="DropDownList125">
                    </asp:DropDownList>
                    <webControls:ExRequiredFieldValidator ID="rfvTaskDueDaysBasis" runat="server" InitialValue="-2"
                        ControlToValidate="ddlTaskDueDaysBasis"></webControls:ExRequiredFieldValidator>
                    &nbsp; 
                    <webControls:ExLabel ID="lblTaskSkip" runat="server" LabelID="2781" SkinID="Black11Arial" FormatString="{0}:"></webControls:ExLabel>
                    <webControls:ExTextBox ID="txtSkipTaskDueDays" runat="server" SkinID="ExTextBox30" />
                    <webControls:ExLabel runat="server" ID="lblTaskSkipDueDays" SkinID="Black11Arial"></webControls:ExLabel>
                    <asp:CustomValidator ID="cvSkipTaskDueDays" runat="server" Font-Size="Medium" OnServerValidate="cvSkipTaskDueDays_OnServerValidate"></asp:CustomValidator>
                </td>
                <td class="ManadatoryField"></td>
                <td>
                    <webControls:ExLabel ID="lblAssigneeDueDaysBasis" runat="server" LabelID="2778" SkinID="Black11Arial"
                        FormatString="{0}:"></webControls:ExLabel>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAssigneeDueDaysBasis" runat="server" SkinID="DropDownList125">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="cvAssigneeDueDaysBasis" runat="server" Font-Size="Medium" OnServerValidate="cvAssigneeDueDaysBasis_OnServerValidate"></asp:CustomValidator>
                    &nbsp; 
                    <webControls:ExLabel ID="lblAssigneeDueDaysSkip" runat="server" LabelID="2781" SkinID="Black11Arial" FormatString="{0}:"></webControls:ExLabel>
                    <webControls:ExTextBox ID="txtSkipAssigneeDueDays" runat="server"
                        SkinID="ExTextBox30" />
                    <webControls:ExLabel runat="server" ID="lblSkipAssigneeDueDays" SkinID="Black11Arial"></webControls:ExLabel>
                    <asp:CustomValidator ID="cvSkipAssigneeDueDays" runat="server" Font-Size="Medium" OnServerValidate="cvSkipAssigneeDueDays_OnServerValidate"></asp:CustomValidator>
                </td>
            </tr>

            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
            <tr>
                <td class="ManadatoryField">*
                </td>
                <td>
                    <webControls:ExLabel ID="lblCustomTaskDueDate" runat="server" LabelID="2582" SkinID="Black11Arial"
                        FormatString="{0}:"></webControls:ExLabel>
                </td>
                <td>
                    <webControls:ExTextBox ID="txtCustomTaskDueDays" runat="server" IsRequired="true"
                        SkinID="ExTextBox100" />
                    <webControls:ExLabel ID="lblTaskDueDaysValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                    <webControls:ExLabel ID="lblDays" runat="server" LabelID="1245" SkinID="Black11Arial"></webControls:ExLabel>
                    <%--<asp:CustomValidator ID="cvValidateTaskDueDays" runat="server" Text="!" Font-Bold="true"
                        ClientValidationFunction="ValidateDueDays_Task"></asp:CustomValidator>--%>
                    <asp:CustomValidator ID="cvTaskDueDays"
                        runat="server" Font-Size="Medium" OnServerValidate="cvTaskDueDays_OnServerValidate"></asp:CustomValidator>
                </td>
                <td class="ManadatoryField">
                    <%--  *--%>
                </td>
                <td>
                    <webControls:ExLabel ID="lblCustomAssigneeDueDate" runat="server" LabelID="2570"
                        FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel>
                </td>
                <td>
                    <webControls:ExTextBox ID="txtCustomAssigneeDueDays" runat="server" SkinID="ExTextBox100" />
                    <webControls:ExLabel ID="lblAssigneeDueDaysValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                    <webControls:ExLabel ID="lblDays1" runat="server" LabelID="1245" SkinID="Black11Arial"></webControls:ExLabel>
                    <%--   <asp:CustomValidator ID="cvValidateAssigneeDueDays" runat="server" Text="!" Font-Bold="true"
                        ClientValidationFunction="ValidateDueDays_Assignee"></asp:CustomValidator>--%>
                    <asp:CustomValidator ID="cvAssigneeDueDays" runat="server" Text="!" Font-Bold="true"
                        OnServerValidate="cvAssigneeDueDays_OnServerValidate"></asp:CustomValidator>
                </td>
            </tr>
            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>

            <tr>
                <td class="ManadatoryField"></td>
                <td>
                    <webControls:ExLabel ID="lblReviewerDueDaysBasis" runat="server" LabelID="2947" SkinID="Black11Arial"
                        FormatString="{0}:"></webControls:ExLabel>
                </td>
                <td>
                    <asp:DropDownList ID="ddlReviewerDueDaysBasis" runat="server" SkinID="DropDownList125">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="cvReviewerDueDaysBasis" runat="server" Font-Size="Medium" OnServerValidate="cvReviewerDueDaysBasis_OnServerValidate"></asp:CustomValidator>
                    &nbsp; 
                    <webControls:ExLabel ID="lblReviewerDueDaysSkip" runat="server" LabelID="2781" SkinID="Black11Arial" FormatString="{0}:"></webControls:ExLabel>
                    <webControls:ExTextBox ID="txtSkipReviewerDueDays" runat="server" SkinID="ExTextBox30" />
                    <webControls:ExLabel runat="server" ID="lblSkipReviewerDueDays" SkinID="Black11Arial"></webControls:ExLabel>
                    <asp:CustomValidator ID="cvSkipReviewerDueDays" runat="server" Font-Size="Medium" OnServerValidate="cvSkipTaskDueDays_OnServerValidate"></asp:CustomValidator>
                     <asp:CustomValidator ID="cvTaskSkipDueDaysOrder" runat="server" Font-Size="Medium" OnServerValidate="cvTaskSkipDueDaysOrder_OnServerValidate"></asp:CustomValidator>
                </td>
                <td class="ManadatoryField"></td>
                <td></td>
                <td></td>
            </tr>

            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
            <tr>
                <td class="ManadatoryField"></td>
                <td>
                    <webControls:ExLabel ID="lblCustomReviewerDueDays" runat="server" LabelID="2753" SkinID="Black11Arial"
                        FormatString="{0}:"></webControls:ExLabel>
                </td>
                <td>
                    <webControls:ExTextBox ID="txtCustomReviewerDueDays" runat="server"
                        SkinID="ExTextBox100" />
                    <webControls:ExLabel ID="lblReviewerDueDaysValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                    <webControls:ExLabel ID="lblDays2" runat="server" LabelID="1245" SkinID="Black11Arial"></webControls:ExLabel>
                     <asp:CustomValidator ID="cvReviewerDueDays" runat="server" Text="!" Font-Bold="true"
                        OnServerValidate="cvReviewerDueDays_OnServerValidate"></asp:CustomValidator>
                    <asp:CustomValidator ID="cvTaskDueDaysOrder"
                        runat="server" Font-Size="Medium" OnServerValidate="cvTaskDueDaysOrder_OnServerValidate"></asp:CustomValidator>
                </td>
                <td class="ManadatoryField"></td>
                <td></td>
                <td></td>
            </tr>

             <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
            <tr>
                <td class="ManadatoryField"> * </td>
                <td>
                    <webControls:ExLabel ID="lblDaysType" runat="server" LabelID="2963" SkinID="Black11Arial"
                        FormatString="{0}:"></webControls:ExLabel>
                </td>
                <td>
                  <asp:DropDownList ID="ddlDaysType" runat="server" SkinID="DropDownList200" CausesValidation="false" >
                </asp:DropDownList>
                <webControls:ExRequiredFieldValidator ID="rfvDaysType" runat="server" InitialValue="-2"
                    ControlToValidate="ddlDaysType"></webControls:ExRequiredFieldValidator>
                </td>
                <td class="ManadatoryField"></td>
                <td></td>
                <td></td>
            </tr>

        </asp:Panel>
        <asp:Panel ID="pnlTaskAccounts" runat="server">
            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
            <tr>
                <td></td>
                <td colspan="5">
                    <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="2581" FormatString="{0}:"
                        SkinID="Black11Arial"></webControls:ExLabel>
                    <asp:CustomValidator ID="cvTaskAccounts" runat="server" Font-Size="Medium" OnServerValidate="cvTaskAccounts_OnServerValidate"></asp:CustomValidator>
                </td>
            </tr>
            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:Panel ID="pnlTaskAccountGridScroll" runat="server" SkinID="RadGridScrollPanel">
                        <UserControl:SkyStemARTGrid ID="ucSkyStemARTGridAccountsAdded" runat="server" Grid-AllowPaging="false"
                            Grid-AllowSorting="true" Grid-EntityNameLabelID="2571" Grid-AllowCauseValidationExportToExcel="false"
                            Grid-AllowExportToExcel="True">
                            <SkyStemGridColumnCollection>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1357" HeaderStyle-Width="20%" SortExpression="AccountNumber"
                                    DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblAccountNumberAddedGrid" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1346" HeaderStyle-Width="15%" SortExpression="AccountName"
                                    DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblAccountNameAddedGrid" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1256" SortExpression="ZeroBalance"
                                    DataType="System.String" UniqueName="ZeroBalance" Visible="false">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblZeroBalanceAddedGrid" runat="server" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1014" SortExpression="KeyAccount"
                                    DataType="System.String" UniqueName="KeyAccount" Visible="false">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblKeyAccountAddedGrid" runat="server" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1013" SortExpression="RiskRating"
                                    DataType="System.String" UniqueName="RiskRating" Visible="false">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblRiskRatingAddedGrid" runat="server" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1426" HeaderStyle-Width="15%" SortExpression="ReconciliationTemplate"
                                    DataType="System.String">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblReconciliationTemplateAddedGrid" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1130" SortExpression="PreparerFullName"
                                    DataType="System.String" UniqueName="Preparer" Visible="true">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblPreparerAddedGrid" runat="server" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2501" SortExpression="BackupPreparerFullName"
                                    DataType="System.String" UniqueName="BackupPreparer">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblBackupPreparerAddedGrid" runat="server" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1131" SortExpression="ReviewerFullName"
                                    DataType="System.String" UniqueName="Reviewer" Visible="true">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblReviewerAddedGrid" runat="server" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2502" SortExpression="BackupReviewerFullName"
                                    DataType="System.String" UniqueName="BackupReviewer">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblBackupReviewerAddedGrid" runat="server" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1132" SortExpression="ApproverFullName"
                                    DataType="System.String" UniqueName="Approver" Visible="false">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblApproverAddedGrid" runat="server" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2503" SortExpression="BackupApproverFullName"
                                    DataType="System.String" UniqueName="BackupApprover" Visible="false">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblBackupApproverAddedGrid" runat="server" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                            </SkyStemGridColumnCollection>
                            <ClientSettings>
                                <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                            </ClientSettings>
                        </UserControl:SkyStemARTGrid>
                    </asp:Panel>
                </td>
            </tr>
        </asp:Panel>
        <tr class="BlankRow">
            <td colspan="6"></td>
        </tr>
        <tr>
            <td align="right" colspan="6">
                <webControls:ExButton ID="btnRemoveAccount" runat="server" LabelID="2170" CausesValidation="false"
                    OnClick="btnRemoveAccount_OnClick" />
                <webControls:ExButton ID="btnUpdate" runat="server" LabelID="1315" OnClick="btnUpdate_Click" OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true; this.value = 'Saving...';" UseSubmitBehavior="false" />
                <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClick="btnCancel_Click"
                    CausesValidation="false" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="6"></td>
        </tr>
        <asp:Panel ID="pnlSearchGrid" runat="server">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Panel ID="pnlHeader" runat="server">
                            <table class="InputRequrementsHeading" width="100%">
                                <tr>
                                    <td width="2%">&nbsp;
                                    </td>
                                    <td width="20%">
                                        <webControls:ExLabel ID="lblAddMoreAccounts" runat="server" LabelID="1356" SkinID="BlueBold11Arial" />
                                    </td>
                                    <td width="20%">&nbsp;
                                    </td>
                                    <td width="10%">&nbsp;
                                    </td>
                                    <td width="20%">&nbsp;
                                    </td>
                                    <td width="28%" align="right">
                                        <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlAccountSearch" runat="server">
                            <table width="100%" border="0" class="InputRequrementsTextNoBackColor">
                                <tr>
                                    <td>
                                        <UserControl:AccountSearchControl ID="ucAccountSearchControl" runat="server" IsOnPopup="true" />
                                    </td>
                                </tr>
                                <tr class="BlankRow">
                                    <td colspan="6"></td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">
                                            <UserControl:SkyStemARTGrid ID="ucSkyStemARTGridAccountSearchResult" runat="server"
                                                Grid-AllowPaging="true" Grid-AllowSorting="true" Grid-EntityNameLabelID="1356"
                                                Grid-AllowExportToExcel="True" Grid-AllowCauseValidationExportToExcel="false">
                                                <SkyStemGridColumnCollection>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1357" HeaderStyle-Width="20%" UniqueName="AccountNumber"
                                                        SortExpression="AccountNumber" DataType="System.String">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblAccountNumber" runat="server"></webControls:ExLabel>
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1346" HeaderStyle-Width="15%" UniqueName="AccountName"
                                                        SortExpression="AccountName" DataType="System.String">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblAccountName" runat="server"></webControls:ExLabel>
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1256" SortExpression="ZeroBalance"
                                                        DataType="System.String" UniqueName="ZeroBalance" Visible="false">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblZeroBalance" runat="server" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1014" SortExpression="KeyAccount"
                                                        DataType="System.String" UniqueName="KeyAccount" Visible="false">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblKeyAccount" runat="server" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1013" SortExpression="RiskRating"
                                                        DataType="System.String" UniqueName="RiskRating" Visible="false">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblRiskRating" runat="server" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1426" HeaderStyle-Width="15%" SortExpression="ReconciliationTemplate"
                                                        DataType="System.String">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblReconciliationTemplate" runat="server"></webControls:ExLabel>
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1130" SortExpression="PreparerFullName"
                                                        DataType="System.String" UniqueName="Preparer" Visible="true">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblPreparer" runat="server" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="2501" SortExpression="BackupPreparerFullName"
                                                        DataType="System.String" UniqueName="BackupPreparer" Visible="false">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblBackupPreparer" runat="server" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1131" SortExpression="ReviewerFullName"
                                                        DataType="System.String" UniqueName="Reviewer" Visible="true">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblReviewer" runat="server" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="2502" SortExpression="BackupReviewerFullName"
                                                        DataType="System.String" UniqueName="BackupReviewer" Visible="false">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblBackupReviewer" runat="server" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="1132" SortExpression="ApproverFullName"
                                                        DataType="System.String" UniqueName="Approver" Visible="false">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblApprover" runat="server" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="2503" SortExpression="BackupApproverFullName"
                                                        DataType="System.String" UniqueName="BackupApprover" Visible="false">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblBackupApprover" runat="server" />
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="2752" HeaderStyle-Width="10%" UniqueName="PreparerDueDays"
                                                        SortExpression="PreparerDueDays" DataType="System.Int32">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblPreparerDueDays" runat="server"></webControls:ExLabel>
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="2753" HeaderStyle-Width="10%" UniqueName="ReviewerDueDays"
                                                        SortExpression="ReviewerDueDays" DataType="System.Int32">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblReviewerDueDays" runat="server"></webControls:ExLabel>
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                    <telerikWebControls:ExGridTemplateColumn LabelID="2754" HeaderStyle-Width="10%" UniqueName="ApproverDueDays"
                                                        SortExpression="ApproverDueDays" DataType="System.Int32">
                                                        <ItemTemplate>
                                                            <webControls:ExLabel ID="lblApproverDueDays" runat="server"></webControls:ExLabel>
                                                        </ItemTemplate>
                                                    </telerikWebControls:ExGridTemplateColumn>
                                                </SkyStemGridColumnCollection>
                                                <ClientSettings>
                                                    <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                                                </ClientSettings>
                                            </UserControl:SkyStemARTGrid>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="right">
                                        <webControls:ExButton ID="btnAdd" runat="server" CausesValidation="false" LabelID="1560"
                                            OnClick="btnAdd_OnClick" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <ajaxToolkit:CollapsiblePanelExtender ID="cpeAccountSearch" TargetControlID="pnlAccountSearch"
                            ImageControlID="imgCollapse" CollapseControlID="pnlHeader" ExpandControlID="pnlHeader"
                            runat="server" SkinID="CollapsiblePanel">
                        </ajaxToolkit:CollapsiblePanelExtender>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td></td>
                </tr>
            </table>
        </asp:Panel>
    </table>
    <asp:HiddenField ID="hdnAssignedTo" runat="server" />
    <asp:HiddenField ID="hdnReviewer" runat="server" />
    <asp:HiddenField ID="hdnApprover" runat="server" />
    <asp:HiddenField ID="hdnNotify" runat="server" />
    <asp:HiddenField ID="hdnDateToCompare" runat="server" />

    <script type="text/javascript">
        function setUsersInfo(AddUsersIDList, AddUsersNameList, TaskUserRoleVal) {
            // alert(TaskUserRoleVal);
            // alert(AddUsersIDList);

            var hdnUser = null;
            var txtUser = null;
            if (TaskUserRoleVal == 4)//AssignedTo
            {
                hdnUser = $get("<%=hdnAssignedTo.ClientID %>");
                txtUser = $get('<%=txtAssignedTo.ClientID %>');
            }
            else if (TaskUserRoleVal == 5)//Reviewer
            {
                hdnUser = $get("<%=hdnReviewer.ClientID %>");
                txtUser = $get('<%=txtReviewer.ClientID %>');
            }
            else if (TaskUserRoleVal == 24)//Approver
            {
                hdnUser = $get("<%=hdnApprover.ClientID %>");
                txtUser = $get('<%=txtApprover.ClientID %>');
            }
            else if (TaskUserRoleVal == 6)//Notify
            {
                hdnUser = $get("<%=hdnNotify.ClientID %>");
                txtUser = $get('<%=txtNotify.ClientID %>');
            }
    if (hdnUser != null)
        hdnUser.value = AddUsersIDList;
    if (txtUser != null)
        txtUser.value = AddUsersNameList;
            //            alert(hdnNotify.value);
}

function ValidateTaskDates(sender, args) {

    var TaskDateArray = new Array();

    var txtCalAssigneeDueDate = $get("<%=calAssigneeDueDate.ClientID %>")
    if (txtCalAssigneeDueDate != null && txtCalAssigneeDueDate != 'undefined') {
        var TCalAssigneeDueDate = removeSpaces(txtCalAssigneeDueDate.value);
        if (TCalAssigneeDueDate != '') {
            TaskDateArray.push(TCalAssigneeDueDate);
        }
    }

    var txtCalReviewerDueDate = $get("<%=calReviewerDueDate.ClientID %>")
    if (txtCalReviewerDueDate != null && txtCalReviewerDueDate != 'undefined') {
        var TCalReviewerDueDate = removeSpaces(txtCalReviewerDueDate.value);
        if (TCalReviewerDueDate != '') {
            TaskDateArray.push(TCalReviewerDueDate);
        }
    }

    var txtCalTaskDueDate = $get("<%=calTaskDueDate.ClientID %>")
    if (txtCalTaskDueDate != null && txtCalTaskDueDate != 'undefined') {
        var TCalTaskDueDate = removeSpaces(txtCalTaskDueDate.value);
        if (TCalTaskDueDate != '') {
            TaskDateArray.push(TCalTaskDueDate);
        }
    }
    if (CheckDates(TaskDateArray) == false) {
        sender.setAttribute('ErrorMessage', sender.getAttribute("TaskDateErrorMessage"));
        args.IsValid = false;
        return;
    }
    return false;
}
function CheckDates(datesArray) {
    if (datesArray != null && datesArray != 'undefined') {
        for (var i = 1; i < datesArray.length; i++) {
            var minValue = datesArray[i - 1];
            var maxValue = datesArray[i];
            var result = CompareDates(maxValue, minValue);
            if (result == -1) {
                return false;
            }

        }
    }
}
function removeSpaces(string) {
    return string.split(' ').join('');
}
function ValidateIsDate(sender, args) {
    var dateCal = document.getElementById(sender.controltovalidate).value;
    var TdateCal = removeSpaces(dateCal);
    if (TdateCal != '') {
        args.IsValid = IsDate(dateCal);
        return;
    }
    return false;
}
function CompareDateNotBeforeCurrentDate(sender, args) {
    var dateToCompare = $get("<%=hdnDateToCompare.ClientID %>").value;;
    var dateCal = document.getElementById(sender.controltovalidate).value;
    if (dateToCompare != dateCal) {
        var result = CompareDates(dateCal, dateToCompare);
        if (result == -1) //what about <0
        {
            args.IsValid = false;
        }
        else {
            args.IsValid = true;
        }
    }
}
function ValidateTaskDays(sender, args) {

    var TaskDaysArray = new Array();
    var txtAssigneeDueDays = $get("<%=txtCustomAssigneeDueDays.ClientID %>")
    if (txtAssigneeDueDays != null && txtAssigneeDueDays != 'undefined') {
        var TAssigneeDueDays = removeSpaces(txtAssigneeDueDays.firstChild.value);
        if (TAssigneeDueDays != '') {
            TaskDaysArray.push(TAssigneeDueDays);
        }
    }

    var txtTaskDueDays = $get("<%=txtCustomTaskDueDays.ClientID %>")
    if (txtTaskDueDays != null && txtTaskDueDays != 'undefined') {
        var TaskDueDays = removeSpaces(txtTaskDueDays.firstChild.value);
        if (TaskDueDays != '') {
            TaskDaysArray.push(TaskDueDays);
        }
    }
    if (CheckDueDays(TaskDaysArray) == false) {
        args.IsValid = false;
        return;
    }
    return false;
}
function ValidateDueDays_Assignee(sender, args) {
    var TaskDaysArray = new Array();
    var txtAssigneeDueDays = $get("<%=txtCustomAssigneeDueDays.ClientID %>")
    if (txtAssigneeDueDays != null && txtAssigneeDueDays != 'undefined') {
        var TAssigneeDueDays = removeSpaces(txtAssigneeDueDays.firstChild.value);
        if (TAssigneeDueDays != '' && parseInt(TAssigneeDueDays) < 1) {
            args.IsValid = false;
            return;
        }
    }
}
function ValidateDueDays_Task(sender, args) {
    var txtTaskDueDays = $get("<%=txtCustomTaskDueDays.ClientID %>")
    if (txtTaskDueDays != null && txtTaskDueDays != 'undefined') {
        var TaskDueDays = removeSpaces(txtTaskDueDays.firstChild.value);
        if (TaskDueDays != '' && parseInt(TaskDueDays) < 1) {
            args.IsValid = false;
            return;
        }
    }
}
function CheckDueDays(daysArray) {
    if (daysArray != null && daysArray != 'undefined') {
        for (var i = 1; i < daysArray.length; i++) {
            var minValue = parseInt(daysArray[i - 1]);
            var maxValue = parseInt(daysArray[i]);
            if (minValue > maxValue) {
                return false;
            }
        }
    }
}

function ValidateTaskUsers(sender, args) {  
    var cloneArray = new Array();
    var AssignedToArray;
    var ReviewerArray;
    var ApproverArray;
    var NotifyArray;
    var hdnAssignedTo = $get("<%=hdnAssignedTo.ClientID %>")
    if (hdnAssignedTo != null && hdnAssignedTo != 'undefined') {
        var hdnAssignedToVal = removeSpaces(hdnAssignedTo.value);
        AssignedToArray = hdnAssignedToVal.split(",");
    }
    var hdnReviewer = $get("<%=hdnReviewer.ClientID %>")
            if (hdnReviewer != null && hdnReviewer != 'undefined') {
                var hdnReviewerVal = removeSpaces(hdnReviewer.value);
                if (hdnReviewerVal != null && hdnReviewerVal != '' && hdnReviewerVal != "")
                ReviewerArray = hdnReviewerVal.split(",");
            }
            var hdnApprover = $get("<%=hdnApprover.ClientID %>")
            if (hdnApprover != null && hdnApprover != 'undefined') {
                var hdnApproverVal = removeSpaces(hdnApprover.value);
                if (hdnApproverVal != null && hdnApproverVal != '' && hdnApproverVal != "")
                ApproverArray = hdnApproverVal.split(",");
            }
            var hdnNotify = $get("<%=hdnNotify.ClientID %>")
            if (hdnNotify != null && hdnNotify != 'undefined') {
                var hdnNotifyVal = removeSpaces(hdnNotify.value);
                if (hdnNotifyVal != null && hdnNotifyVal != '' && hdnNotifyVal != "")
                NotifyArray = hdnNotifyVal.split(",");
            }
            if (AssignedToArray != null && AssignedToArray != 'undefined' && AssignedToArray.length > 0) {
                for (var i = 0; i < AssignedToArray.length; i++) {
                    if (contains(AssignedToArray[i], cloneArray) != -1) {
                        args.IsValid = false;
                        return;
                    }
                    cloneArray.push(AssignedToArray[i]);
                }
            }
            if (ReviewerArray != null && ReviewerArray != 'undefined' && ReviewerArray.length > 0) {
                if (cloneArray == null || cloneArray == 'undefined') {
                    for (var i = 0; i < ReviewerArray.length; i++) {
                        if (contains(ReviewerArray[i], cloneArray) != -1) {
                            args.IsValid = false;
                            return;
                        }
                        cloneArray.push(ReviewerArray[i]);
                    }
                }
                else {
                    for (var i = 0; i < ReviewerArray.length; i++) {
                        if (contains(ReviewerArray[i], cloneArray) != -1) {
                            args.IsValid = false;
                            return;
                        }
                        cloneArray.push(ReviewerArray[i]);
                    }

                }
            }
            if (ApproverArray != null && ApproverArray != 'undefined' && ApproverArray.length > 0) {
                if (cloneArray == null || cloneArray == 'undefined') {
                    for (var i = 0; i < ApproverArray.length; i++) {
                        if (contains(ApproverArray[i], cloneArray) != -1) {
                            args.IsValid = false;
                            return;
                        }
                        cloneArray.push(ApproverArray[i]);
                    }
                }
                else {
                    for (var i = 0; i < ApproverArray.length; i++) {
                        if (contains(ApproverArray[i], cloneArray) != -1) {
                            args.IsValid = false;
                            return;
                        }
                        cloneArray.push(ApproverArray[i]);
                    }

                }
            }

            if (NotifyArray != null && NotifyArray != 'undefined' && NotifyArray.length > 0) {
                if (cloneArray == null || cloneArray == 'undefined') {
                    for (var i = 0; i < NotifyArray.length; i++) {
                        if (contains(NotifyArray[i], cloneArray) != -1) {
                            args.IsValid = false;
                            return;
                        }
                        cloneArray.push(NotifyArray[i]);
                    }
                }
                else {
                    for (var i = 0; i < NotifyArray.length; i++) {
                        if (contains(NotifyArray[i], cloneArray) != -1) {
                            args.IsValid = false;
                            return;
                        }
                        cloneArray.push(NotifyArray[i]);
                    }

                }
            }

            return false;
        }
        function contains(val, arr) {
            return jQuery.inArray(val, arr);
            //for (var i = 0; i < a.length; i++) {
            //    if (a[i] === obj) {
            //        return true;
            //    }
            //}
            //return false;
        }
        function ValidateAssignedTo(sender, args) {

            var txtAssignedTo = $get("<%=txtAssignedTo.ClientID %>").value;
            if (txtAssignedTo == "" || txtAssignedTo == '')
                $get("<%=hdnAssignedTo.ClientID %>").value = "";

            var AssignedTo = $get("<%=hdnAssignedTo.ClientID %>").value;
            if (AssignedTo == "" || AssignedTo == '') {
                args.IsValid = false;
                return;
            }
            return false;
        }

        function ValidateApprover(sender, args) {

            var txtApproverVal = $get("<%=txtApprover.ClientID %>").value;
            var txtReviewerVal = $get("<%=txtReviewer.ClientID %>").value;
            if (txtApproverVal == "" || txtApproverVal == '') {
                $get("<%=hdnApprover.ClientID %>").value = "";
            }
            var ApproverVal = $get("<%=hdnApprover.ClientID %>").value;
            var ReviewerVal = $get("<%=hdnReviewer.ClientID %>").value;
            if ((txtApproverVal != "" || ApproverVal != "") && (txtApproverVal == "" || ReviewerVal == "" || ReviewerVal == '')) {
                args.IsValid = false;
                return;
            }

            return false;
        }
        function ConfirmDeletion(msg, TaskUserRoleVal) {
            var answer = confirm(msg);
            if (answer) {
                setUsersInfo("", "", TaskUserRoleVal);
                return false;
            }
            else {
                return false;
            }
        }
        function OpenNewWindow(url, QueryStringConstants, TaskUserRole) {

            var hdnNotify = null
            if (TaskUserRole == 4)//AssignedTo
                hdnNotify = $get("<%=hdnAssignedTo.ClientID %>")
            else if (TaskUserRole == 5)//Reviewer
                hdnNotify = $get("<%=hdnReviewer.ClientID %>")
            else if (TaskUserRole == 24)//Approver
                hdnNotify = $get("<%=hdnApprover.ClientID %>")
            else if (TaskUserRole == 6)//Notify
                hdnNotify = $get("<%=hdnNotify.ClientID %>")

    if (hdnNotify != null && hdnNotify != 'undefined') {
        var hdnNotifyVal = removeSpaces(hdnNotify.value);
        if (hdnNotifyVal != null && hdnNotifyVal != "")
            url = url + "&" + QueryStringConstants + "=" + hdnNotifyVal;
    }
    OpenRadWindowFromRadWindow(url, '400', '650');
}



    </script>

</asp:Content>
