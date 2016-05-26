<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ARTMasterPage.master" Inherits="Pages_CertificationVerbiage"
    Theme="SkyStemBlueBrown" MaintainScrollPositionOnPostback="true" Codebehind="CertificationVerbiage.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding-left: 40px; padding-right: 40px">
        <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <asp:Panel ID="pnlMessagePane" Width="100%" runat="server" Visible="false">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="center">
                                <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="1051" FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                                &nbsp;&nbsp;
                                <webControls:ExLabel ID="lblError" LabelID="1811" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlContentPane" runat="server">
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Panel ID="pnlContent" runat="server">
                                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                                        <col width="2%" />
                                        <col width="20%" />
                                        <col width="40%" />
                                        <col width="38%" />
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <webControls:ExLabel ID="lblCertificationBalance" LabelID="1828" FormatString="{0}:"
                                                    runat="server" SkinID="SubSectionHeading"></webControls:ExLabel>
                                            </td>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <webControls:ExLabel ID="lblDynamicField" LabelID="1829" FormatString="{0}:" runat="server"
                                                    SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDynamicField" runat="server" SkinID="DropDownList150">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;<webControls:ExButton ID="btnCertificationAdd" runat="server" LabelID="1560"
                                                    OnClientClick="PasteTextInCertifcationEditor();return false" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td valign="top">
                                                <webControls:ExLabel ID="lblCertificationText" LabelID="1830 " FormatString="{0}:"
                                                    runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <td>
                                                <telerik:RadEditor runat="server" ID="txtDescriptionCertification" Height="150px"
                                                    EditModes="Design" Width="96%">
                                                    <FontNames>
                                                    </FontNames>
                                                    <Tools>
                                                        <telerik:EditorToolGroup>
                                                            <telerik:EditorTool Name="Italic" />
                                                            <telerik:EditorTool Name="Bold" />
                                                            <telerik:EditorTool Name="Underline" />
                                                        </telerik:EditorToolGroup>
                                                    </Tools>
                                                </telerik:RadEditor>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td colspan="3">
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td valign="top">
                                                <webControls:ExLabel ID="lblException" LabelID="1831" FormatString="{0}:" runat="server"
                                                    SkinID="SubSectionHeading"></webControls:ExLabel>
                                            </td>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td valign="top">
                                                <webControls:ExLabel ID="ExLabel2" LabelID="1829" FormatString="{0}:" runat="server"
                                                    SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlException" runat="server" SkinID="DropDownList150">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;<webControls:ExButton ID="btnExceptionAdd" runat="server" LabelID="1560"
                                                    OnClientClick="PasteTextInExceptionEditor();return false" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td valign="top">
                                                <webControls:ExLabel ID="ExLabel3" LabelID="1830 " FormatString="{0}:" runat="server"
                                                    SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <td>
                                                <telerik:RadEditor runat="server" ID="txtDescriptionException" Height="150px" EditModes="Design"
                                                    Width="96%">
                                                    <FontNames>
                                                    </FontNames>
                                                    <Tools>
                                                        <telerik:EditorToolGroup>
                                                            <telerik:EditorTool Name="Italic" />
                                                            <telerik:EditorTool Name="Bold" />
                                                            <telerik:EditorTool Name="Underline" />
                                                        </telerik:EditorToolGroup>
                                                    </Tools>
                                                </telerik:RadEditor>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td colspan="3">
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <webControls:ExLabel ID="ExLabel4" LabelID="1231" FormatString="{0}:" runat="server"
                                                     SkinID="SubSectionHeading"></webControls:ExLabel>
                                            </td>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <webControls:ExLabel ID="ExLabel5" LabelID="1829" FormatString="{0}:" runat="server"
                                                    SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlAccount" runat="server" SkinID="DropDownList150">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;<webControls:ExButton ID="ExButton1" runat="server" LabelID="1560"
                                                    OnClientClick="PasteTextInEditor();return false" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td valign="top">
                                                <webControls:ExLabel ID="ExLabel6" LabelID="1830 " FormatString="{0}:" runat="server"
                                                    SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <td>
                                                <telerik:RadEditor runat="server" ID="txtDescription" Height="150px" EditModes="Design"
                                                    Width="96%">
                                                    <FontNames>
                                                    </FontNames>
                                                    <Tools>
                                                        <telerik:EditorToolGroup>
                                                            <telerik:EditorTool Name="Italic" />
                                                            <telerik:EditorTool Name="Bold" />
                                                            <telerik:EditorTool Name="Underline" />
                                                        </telerik:EditorToolGroup>
                                                    </Tools>
                                                </telerik:RadEditor>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td colspan="3">
                                                <hr />
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <webControls:ExButton LabelID="1315" runat="server" ID="btnSave" OnClick="btnSave_Click" />
                                <webControls:ExButton LabelID="1239" runat="server" ID="btnCancel" OnClick="btnCancel_Click"
                                    CausesValidation="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <UserControls:ProgressBar ID="ucProgressBar" runat="server" AssociatedUpdatePanelID="upnlMain" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript" language="javascript">
        function PasteTextInCertifcationEditor() {
            var ddlDynamicField = document.getElementById("<%=ddlDynamicField.ClientID%>");
            var Text = ddlDynamicField.options[ddlDynamicField.selectedIndex].text;
            var editor = $find("<%=txtDescriptionCertification.ClientID%>"); //get a reference to RadEditor client object
            editor.pasteHtml(Text); //PasteHtml is a method from the editor client side API
            return false;
        }

        function PasteTextInExceptionEditor() {

            var ddlDynamicField = document.getElementById("<%=ddlException.ClientID%>");
            var Text = ddlDynamicField.options[ddlDynamicField.selectedIndex].text;
            var editor = $find("<%=txtDescriptionException.ClientID%>"); //get a reference to RadEditor client object
            editor.pasteHtml(Text); //PasteHtml is a method from the editor client side API
            return false;
        }

        function PasteTextInEditor() {


            var ddlDynamicField = document.getElementById("<%=ddlAccount.ClientID%>");
            var Text = ddlDynamicField.options[ddlDynamicField.selectedIndex].text;
            var editor = $find("<%=txtDescription.ClientID%>"); //get a reference to RadEditor client object
            editor.pasteHtml(Text);
            return false; //PasteHtml is a method from the editor client side API
        }

    </script>

</asp:Content>
