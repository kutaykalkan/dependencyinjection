<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true"
    CodeFile="ConfigureJEWriteOnOffApprover.aspx.cs" Inherits="Pages_JournalEntry_ConfigureJEWriteOnOffApprover"
    Theme="SkyStemBlueBrown" Title="Untitled Page" %>

<%@ Register TagPrefix="UserControl" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Import Namespace="SkyStem.Language.LanguageUtility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <asp:Panel ID="GridPnl" runat="server">
                    <tr>
                        <td>
                            <telerikWebControls:ExRadGrid ID="rgConfigureJEWriteOffOnApprover" runat="server"
                                EntityNameLabelID="2101" OnNeedDataSource="rgConfigureJEWriteOffOnApprover_NeedDataSource"
                                OnItemDataBound="rgConfigureJEWriteOffOnApprover_ItemDataBound" OnItemCommand="rgConfigureJEWriteOffOnApprover_OnItemCommand"
                                AllowAddNewRow="true">
                                <MasterTableView>
                                    <Columns>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2081" UniqueName="col3">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblSNo" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                                                <webControls:ExCustomValidator runat="server" ClientValidationFunction="JEColumnValidate"
                                                    ID="cvColumnName">!</webControls:ExCustomValidator>
                                                <webControls:ExCustomValidator runat="server" ValidationGroup="SAVE" ClientValidationFunction="JEColumnValidateSave"
                                                    ID="ExCustomValidator1">!</webControls:ExCustomValidator>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2109" UniqueName="col1">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtFromAmount" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2110" UniqueName="col2">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtToAmount" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2111" UniqueName="col4">
                                            <ItemTemplate>
                                                <asp:DropDownList SkinID="DropDownList150" ID="ddlPrimaryApprover" runat="server">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2112" UniqueName="col5">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlSecondaryApprover" SkinID="DropDownList150" runat="server">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerikWebControls:ExRadGrid>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td align="right" style="padding-right: 50px">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-right: 30px">
                        <webControls:ExButton ID="btnSave"  ValidationGroup="SAVE" runat="server" LabelID="1315"
                            SkinID="ExButton100" OnClick="btnSave_OnClick" />&nbsp;
                        <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" CausesValidation="false"
                            SkinID="ExButton100" OnClick="btnCancel_OnClick" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hdnNoOFfBlankRow" runat="server" Value="0" />
                        <UserControl:ProgressBar ID="ucProgressBar" runat="server" AssociatedUpdatePanelID="upnlMain" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        var minFromAmount =  parseFloat(0);
        var maxToAmount = parseFloat(0);
        function JEColumnValidate(sender, args) {
        
            var valSummaryObj = GetValidationSummaryElement();
            valSummaryObj.validationGroup = '';
            var index = sender.parentNode.parentNode.sectionRowIndex;            
            // Get the corresponding index
            var grid = $find("<%=rgConfigureJEWriteOffOnApprover.ClientID %>");

            var hdnNoOFfBlankRow = document.getElementById("<%=hdnNoOFfBlankRow.ClientID %>");
            var MasterTable = grid.get_masterTableView();
           
            var textFromAmount = MasterTable.get_dataItems()[index].findElement("txtFromAmount"); //accessing standard asp.net server controls
            var txtToAmount = MasterTable.get_dataItems()[index].findElement("txtToAmount");
            var textFromAmountValue = textFromAmount.value;
            var txtToAmountValue = txtToAmount.value;
            if (textFromAmountValue == "" && txtToAmountValue == "") {
                hdnNoOFfBlankRow.value = parseInt(hdnNoOFfBlankRow.value) + 1;
                if (hdnNoOFfBlankRow.value > 0) {
                    args.IsValid = false;
//                    sender.errormessage = 'One blank row already exist';
                    sender.errormessage = '<% =LanguageUtil.GetValue(5000229) %>';
                    return;
                }

            }
        }


        function JEColumnValidateSave(sender, args) {          

            var valSummaryObj = GetValidationSummaryElement();
            valSummaryObj.validationGroup = 'SAVE';
            ValidateSave(sender, args);

        }

        function ValidateSave(sender, args) {
        
            var index = sender.parentNode.parentNode.sectionRowIndex;
            
            if (index == 0) {
                 minFromAmount = parseFloat(0);
                 maxToAmount = parseFloat(0);
            
            }
            // Get the corresponding index
            var grid = $find("<%=rgConfigureJEWriteOffOnApprover.ClientID %>");          
            var hdnNoOFfBlankRow = document.getElementById("<%=hdnNoOFfBlankRow.ClientID %>");
            var MasterTable = grid.get_masterTableView();         
            var textFromAmount = MasterTable.get_dataItems()[index].findElement("txtFromAmount"); //accessing standard asp.net server controls
            var txtToAmount = MasterTable.get_dataItems()[index].findElement("txtToAmount");
            var ddlPrimaryApprover = MasterTable.get_dataItems()[index].findElement("ddlPrimaryApprover"); //accessing standard asp.net server controls
            var ddlSecondaryApprover = MasterTable.get_dataItems()[index].findElement("ddlSecondaryApprover");
            var textFromAmountValue = parseFloat(textFromAmount.value);
            var txtToAmountValue = parseFloat( txtToAmount.value);
            var ddlPrimaryApproverValue = ddlPrimaryApprover.value;
            var ddlSecondaryApproverValue = ddlSecondaryApprover.value;
           

            if (txtToAmount.value != "") {
                if (textFromAmount.value == null || textFromAmount.value == "") {
                    args.IsValid = false;
                    //sender.errormessage = 'From Amount is mandatory if To Amount has value';
                    sender.errormessage = '<% =LanguageUtil.GetValue(5000233) %>';
                    return;

                }
                if (ddlPrimaryApproverValue == "-2") {
                    args.IsValid = false;
                    //sender.errormessage = 'Primary Approver is mandatory if To Amount has value';
                    sender.errormessage = '<% =LanguageUtil.GetValue(5000234) %>';
                    return;

                }
                if (ddlSecondaryApproverValue != "-2" && (ddlPrimaryApproverValue == ddlSecondaryApproverValue)) {
                    args.IsValid = false;
                    //sender.errormessage = 'Primary Approver and Secondary Approver should not be same';
                    sender.errormessage = '<% =LanguageUtil.GetValue(5000235) %>';
                    return;

                }
            }
            
            
            
            
            
            if (textFromAmountValue != "" && minFromAmount == 0) {
                minFromAmount = parseFloat(textFromAmountValue);
            }
            if (txtToAmountValue != "" && maxToAmount == 0) {              
                    maxToAmount = parseFloat(txtToAmountValue);               
            }
            if (textFromAmountValue != "")
             {
                if (validateMinRange(textFromAmountValue)) {
                    args.IsValid = false;
                    // sender.errormessage = 'From Amount Causes Over Lap Ranges';
                       sender.errormessage = '<% =LanguageUtil.GetValue(5000217) %>';
                    return;

                }
             }
             if (txtToAmountValue != "") {
                 if (validateMaxRange(txtToAmountValue)) {

                     args.IsValid = false;                   
                     //sender.errormessage = 'To Amount Causes Over Lap Ranges';
                     sender.errormessage = '<% =LanguageUtil.GetValue(5000218) %>';
                     return;
                 }

                 if (textFromAmountValue < minFromAmount)
                     minFromAmount = textFromAmountValue;
                 if (txtToAmountValue > maxToAmount)
                     maxToAmount = txtToAmountValue;
             }                  
          
                    
        }

        function validateMinRange(val) {

            if (val > minFromAmount && val < maxToAmount)                   
                    return true ;
             else  
                return false;  
            }
      
            
         function validateMaxRange(valMax)
         {
             if (valMax > minFromAmount && valMax < maxToAmount)
                  return true ;
              else 
                return false;
        }
        
        
    </script>

</asp:Content>
