<%@ Page Title="Matching Wizard" Language="C#" Theme="SkyStemBlueBrown" MasterPageFile="~/MasterPages/MatchingMaster.master"
    AutoEventWireup="true" Inherits="Pages_Matching_MatchingWizard"
    EnableEventValidation="true" Codebehind="MatchingWizard.aspx.cs" %>

<%@ Register TagPrefix="wzSteps" TagName="MatchingSourceSelection" Src="~/UserControls/Matching/Wizard/MatchingSourceSelection.ascx" %>
<%@ Register TagPrefix="wzSteps" TagName="MatchingSourceFilter" Src="~/UserControls/Matching/Wizard/MatchingSourceFilter.ascx" %>
<%@ Register TagPrefix="wzSteps" TagName="MatchingConfiguration" Src="~/UserControls/Matching/Wizard/MatchingConfiguration.ascx" %>
<%@ Register TagPrefix="wzSteps" TagName="DisplayColumnSelection" Src="~/UserControls/Matching/Wizard/DisplayColumnSelection.ascx" %>
<%@ Register TagPrefix="wzSteps" TagName="RecItemColumnMapping" Src="~/UserControls/Matching/Wizard/RecItemColumnMapping.ascx" %>
<%@ Register TagPrefix="wzSteps" TagName="PreviewConfirm" Src="~/UserControls/Matching/Wizard/PreviewConfirm.ascx" %>
<%@ Import Namespace="SkyStem.Language.LanguageUtility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMatching" runat="Server">
    <asp:Panel ID="pnlMatchingWizard" runat="server">
        <table width="100%">
            <tr>
                <td>
                    <asp:Wizard ID="wzMatching" OnActiveStepChanged="wzMatching_ActiveStepChanged" OnNextButtonClick="wzMatching_NextButtonClick"
                        OnPreviousButtonClick="wzMatching_OnPreviousButtonClick" OnSideBarButtonClick="wzMatching_OnSideBarButtonClick"
                        OnFinishButtonClick="wzMatching_FinishButtonClick" runat="server" CssClass="WizardStyle">
                        <StartNavigationTemplate>
                            <%--<i>StartNavigationTemplate</i><br />--%>
                            <webControls:ExButton ID="btnUploadNewDataSources" CausesValidation="false" OnClick="btnUploadNewDataSources_OnClick"
                                LabelID="2189" runat="server" />
                            <webControls:ExButton ID="btnNext" LabelID="2204" CommandName="MoveNext" runat="server" />
                        </StartNavigationTemplate>
                        <StepNavigationTemplate>
                            <webControls:ExButton ID="btnPrevious" LabelID="2205" CommandName="MovePrevious"
                                runat="server" />
                            <webControls:ExButton ID="btnNext" LabelID="2204" CommandName="MoveNext" runat="server" />
                            <webControls:ExButton ID="btnContinueLater" OnClick="btnContinueLater_OnClick" LabelID="2206"
                                runat="server" />
                            <webControls:ExButton ID="btnDiscard" LabelID="2207" OnClientClick="return showConfirmation();"
                                runat="server" OnClick="btnDiscard_OnClick" />
                        </StepNavigationTemplate>
                        <FinishNavigationTemplate>
                            <webControls:ExButton ID="btnPrevious" LabelID="2205" CommandName="MovePrevious"
                                runat="server" />
                            <webControls:ExButton ID="btnSubmit" Enabled="false" LabelID="1238" runat="server"
                                OnClick="btnSubmit_OnClick" />
                            <webControls:ExButton ID="btnDiscard" LabelID="2207" runat="server" OnClientClick="return showConfirmation();"
                                OnClick="btnDiscard_OnClick" />
					     </FinishNavigationTemplate>
                        <WizardSteps>
                            <asp:WizardStep StepType="Start" ID="wzStepMatchingSourceSelection">
                                <wzSteps:MatchingSourceSelection runat="server" ID="ucMatchingSourceSelection" />
                            </asp:WizardStep>
                            <asp:WizardStep StepType="Step" ID="wzStepMatchingSourceFilter">
                                <wzSteps:MatchingSourceFilter runat="server" ID="ucMatchingSourceFilter" />
                            </asp:WizardStep>
                            <asp:WizardStep StepType="Step" ID="wzStepMatchingConfiguration">
                                <wzSteps:MatchingConfiguration runat="server" ID="ucMatchingConfiguration" />
                            </asp:WizardStep>
                            <asp:WizardStep StepType="Step" ID="wzStepDisplayColumnSelection">
                                <wzSteps:DisplayColumnSelection runat="server" ID="ucDisplayColumnSelection" />
                            </asp:WizardStep>
                            <asp:WizardStep StepType="Step" ID="wzStepRecItemColumnMapping">
                                <wzSteps:RecItemColumnMapping runat="server" ID="ucRecItemColumnMapping" />
                            </asp:WizardStep>
                            <asp:WizardStep StepType="Finish" ID="wzStepPreviewConfirm">
                                <wzSteps:PreviewConfirm runat="server" ID="PreviewConfirm" />
                            </asp:WizardStep>
                        </WizardSteps>
                        <SideBarStyle CssClass="SideBarStyle" />
                        <StepStyle CssClass="StepStyle" />
                        <NavigationStyle CssClass="NavigationStyle" HorizontalAlign="Right" />
                        <SideBarButtonStyle CssClass="SideBarButtonStyle" />
                        <SideBarTemplate>
                            <asp:DataList runat="server" ID="SideBarList">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CssClass="SideBarButtonStyle" ID="SideBarButton" />
                                </ItemTemplate>
                                <SelectedItemTemplate>
                                    <asp:LinkButton runat="server" CssClass="SideBarActiveButtonStyle" ID="SideBarButton" />
                                </SelectedItemTemplate>
                            </asp:DataList>
                            <td class="WizardSeparatorVertical" rowspan="<%=wzMatching.WizardSteps.Count%>">
                            </td>
                        </SideBarTemplate>
                    </asp:Wizard>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <webControls:ExButton ID="btnBack" LabelID="1545" CausesValidation="false" runat="server"
                        OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <script language="javascript" type="text/javascript">
        function showConfirmation() {
            if (!confirm('<% =LanguageUtil.GetValue(5000297) %>'))
                return false;
            return true;
        }
    </script>

</asp:Content>
