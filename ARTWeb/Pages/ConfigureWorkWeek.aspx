<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigureWorkWeek.aspx.cs"
    Inherits="Pages_ConfigureWorkWeek" MasterPageFile="~/MasterPages/ARTMasterPage.master"
    Theme="SkyStemBlueBrown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="padding-left: 110px">
                <webControls:ExLabel ID="lblSelect" runat="server" LabelID="2136" SkinID="Black11Arial"
                    FormatString="{0}:"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <asp:Panel ID ="pnlWorkWeek" runat ="server" >
        <tr>
            <td>
                <webControls:ExCustomValidator ID="cvWeekDaySelection" runat="server" ClientValidationFunction="validateAtLeastOneItemInCBL"
                    Text="" Display="None" LabelID="2075"></webControls:ExCustomValidator>
                <asp:Panel ID="pnlGrid" runat="server">
                    <asp:Repeater ID="rptWorkWeek" runat="server" OnItemDataBound="rptWorkWeek_ItemDataBound">
                        <ItemTemplate>
                            <table width="100%">
                                <tr>
                                    <td style="width: 10%">
                                        &nbsp;
                                    </td>
                                    <td style="width: 80%">
                                        <webControls:ExCheckBoxWithLabel ID="chkWeekDay" runat="server" SkinID="CheckboxWithLabelBold" />
                                        <input type="hidden" id="hdnWeekDayID" runat="server" />
                                    </td>
                                    <td style="width: 10%">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        </asp:Panel>     
        <tr>
            <td style="padding-left: 250px">
                <webControls:ExButton ID="btnSave" runat="server" LabelID="1315" SkinID="ExButton100"
                    OnClick="btnSave_OnClick" />&nbsp;  <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" SkinID="ExButton100"
                    OnClick="btnCancel_OnClick" />
            </td>
        </tr>
        <tr>
            <td style="padding-left: 110px">
                &nbsp;
            </td>
        </tr>
    </table>
    <input type="text" id="Sel" runat="server" style="display: none" />

    <script type="text/javascript" language="javascript">
  function validateAtLeastOneItemInCBL(source, args)
             {              
              if (!IsCBLAtLeastOneSelected())
                 {
                    args.IsValid = false;
                 }
                 else
                    args.IsValid = true;             
         }

         function IsCBLAtLeastOneSelected() {
             var isAnySelected = false;
             var ctrls = document.getElementsByTagName('input');
             for (var i = 0; i < ctrls.length; i++) {
                 var cbox = ctrls[i];
                 if (cbox.type == "checkbox") {                    
                     if (cbox.checked) {
                         isAnySelected = true;
                     }
                 }
             } 
             return isAnySelected;
         }                                                              
    </script>

</asp:Content>
