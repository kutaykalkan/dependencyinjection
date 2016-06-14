<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="SkyStem.ART.Web.UserControls.Matching.Wizard.RecItemColumnMapping" Codebehind="RecItemColumnMapping.ascx.cs" %>
<%@ Register TagPrefix="usc" TagName="MatchingCombinationSelection" Src="~/UserControls/Matching/Wizard/MatchingCombinationSelection.ascx" %>
<%@ Import Namespace="SkyStem.Language.LanguageUtility" %>
<%@ Register TagPrefix="usc" TagName="MatchSetInfo" Src="~/UserControls/Matching/MatchSetInfo.ascx" %>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td colspan="3" valign="middle">
            <usc:MatchSetInfo ID="uscMatchSetInfo" runat="server" />
        </td>
    </tr>
    <tr id="trDataSourceName" runat="server">
        <td colspan="3" valign="middle">
            <usc:MatchingCombinationSelection ID="ddlMatchingCombinationSelection" runat="server" />
            <webControls:ExCustomValidator runat="server" ClientValidationFunction="validateUnMatchedItem"
                ID="cvDuplicateUnMatchedItemColumn" LabelID="5000298" Display="None"></webControls:ExCustomValidator>
        </td>
    </tr>
    <tr class="BlankRow">
        <td colspan="3">
            <asp:HiddenField ID="hdnMatchSetSubSetCombinationID" runat="server" />
            <asp:HiddenField ID="hdnDuplicateMatchingConfigurationID" runat="server" />
        </td>
    </tr>
    <asp:Panel ID="GridPnl" runat="server">
        <tr>
            <td colspan="3">
                <telerikWebControls:ExRadGrid ID="rgMappingColumns" runat="server" EntityNameLabelID="2101"
                    OnItemDataBound="rgMappingColumns_ItemDataBound" AllowPaging="false" OnNeedDataSource="rgMappingColumns_NeedDataSource"
                    OnSortCommand="rgMappingColumns_SortCommand">
                    <MasterTableView AllowSorting="true" DataKeyNames="RecItemColumnID">
                        <Columns>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2273" HeaderStyle-Width="250px"
                                UniqueName="RecItemColumn">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblRecItemColumnName" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2274" UniqueName="UnMatchedItemColumn">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlUnMatchedItemColumn" runat="server" SkinID="DropDownList200">
                                    </asp:DropDownList>
                                    <%--  <webControls:ExCustomValidator runat="server" ClientValidationFunction="UnMatchedItemColumnValidateSave"
                                        ID="ExCustomValidator1">!</webControls:ExCustomValidator>--%>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                    </ClientSettings>
                </telerikWebControls:ExRadGrid>
            </td>
        </tr>
    </asp:Panel>
</table>

<script type="text/javascript" language="javascript">
    function checkUnMatchedItemUnique() {

        var grid = $find('<%= rgMappingColumns.ClientID %>');
        var MasterTable = grid.get_masterTableView();

        for (var i = 0; i < MasterTable.get_dataItems().length; i++) {
            var ddlUnMatchedItemColumn1 = MasterTable.get_dataItems()[i].findElement("ddlUnMatchedItemColumn").value;
            if (ddlUnMatchedItemColumn1 != "-2") {
                for (var j = i + 1; j < MasterTable.get_dataItems().length; j++) {
                    var ddlUnMatchedItemColumn2 = MasterTable.get_dataItems()[j].findElement("ddlUnMatchedItemColumn").value;
                    if (ddlUnMatchedItemColumn2 != "-2" && ddlUnMatchedItemColumn1 == ddlUnMatchedItemColumn2) {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    function validateUnMatchedItem(source, args) {
        args.IsValid = checkUnMatchedItemUnique();
    }
</script>

<%--<script type="text/javascript" language="javascript">


    function ValidatePage() {
              if (typeof (Page_ClientValidate) == 'function') {
                  Page_ClientValidate();
                }  
          if (Page_IsValid)
           { 
                       // do something 
              alert('Page is valid!'); 
          } 
        else {
                alert('Page is not valid!');
            }  
     } 
  
    
    
    
    function UnMatchedItemColumnValidateSave(sender, args) {

     
        ValidateSave(sender, args);

    }
    Array.prototype.contains = function(needle) {
        for (i in this) {
            if (this[i] == needle) return true;
        } return false;
    }

    function ValidateSave(sender, args) {


        debugger;
        var index = sender.parentNode.parentNode.sectionRowIndex;
        // Get the corresponding index
        var grid = $find("<%=rgMappingColumns.ClientID %>");

        var hdnDuplicateMatchingConfigurationID = document.getElementById("<%=hdnDuplicateMatchingConfigurationID.ClientID %>");
        var MasterTable = grid.get_masterTableView();
        var ddlUnMatchedItemColumn = MasterTable.get_dataItems()[index].findElement("ddlUnMatchedItemColumn"); //accessing standard asp.net server controls
        var ddlUnMatchedItemColumnValue = ddlUnMatchedItemColumn.value;
        var x = hdnDuplicateMatchingConfigurationID.value.split(',');
        if (ddlUnMatchedItemColumnValue != "-2") {

            if (x.contains(ddlUnMatchedItemColumnValue)) {
                args.IsValid = false;
                hdnDuplicateMatchingConfigurationID.value = "";
                sender.errormessage = '<% =LanguageUtil.GetValue(5000272) %>';
                return false;
            }
            else {

                if (hdnDuplicateMatchingConfigurationID.value == "")
                    hdnDuplicateMatchingConfigurationID.value = ddlUnMatchedItemColumnValue;
                    else
                        hdnDuplicateMatchingConfigurationID.value +=","+ ddlUnMatchedItemColumnValue;
            
            }          

        }
    }

   
        
</script> --%>