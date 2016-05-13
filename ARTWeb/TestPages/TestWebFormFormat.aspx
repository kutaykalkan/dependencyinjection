<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true"
    CodeFile="TestWebFormFormat.aspx.cs" Inherits="TestPages_TestWebFormFormat" Title="Untitled Page"
    Theme="SkyStemBlueBrown" %>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
<script language="javascript" type="text/javascript">
<!--

function ValidateDate()
{
    var dt = new Date();
    alert(ConvertJavascriptDateFormat(dt));
    var calTest = document.getElementById('<%= calTest.ClientID %>');
    //var v = dateFormat(calTest.value, "dd/MM/yyyy");
    //var v1 = ConvertDateFormat(calTest.value);
    //alert(v1);
//    var val = getDateFromFormat(calTest.value);
//    alert(getDateFromFormat(calTest.value));
    alert("returnes = " + IsDate(calTest.value));
//    alert("returnes val = " + IsDate(val));
}

function CalculateTotal()
{
    var txtNumber1 = document.getElementById('<%= txtNumber1.ClientID %>');
    var txtNumber2 = document.getElementById('<%= txtNumber2.ClientID %>');
    var lblTotal = document.getElementById('<%= lblTotal.ClientID %>');
    var lblTotalFormatted = document.getElementById('<%= lblTotalFormatted.ClientID %>');
    var txtResults = document.getElementById('<%= txtResults.ClientID %>');
    
    var result = 0;
    var number1 = 0;
    var number2 = 0;
    if (IsNumber(txtNumber1) && IsNumber(txtNumber2))
    {
        //alert("valid");
        number1 = Round(txtNumber1.value, 2);
//        alert("number1 ==" + number1);
//        var n1 = ConvertToEnglishNumber(txtNumber1.value);
//        alert("n1 ==" + n1);
        number2 = Round(txtNumber2.value, 2);
        
        result = number1 * number2;
        lblTotal.innerHTML = result;
//        alert("1");
        lblTotalFormatted.innerHTML = GetDisplayDecimalValue(result);
        alert(lblTotalFormatted.innerHTML + "=label=" + Round(lblTotalFormatted.innerHTML, 2));
//        alert("2");
        //txtResults.value = GetDisplayDecimalValueForTextBox(result);
//        alert("value = " + txtResults.value);
    }
    else
    {
        lblTotal.innerHTML = "";
        lblTotalFormatted.innerHTML = "";
        txtResults.value = "";
    }
    
    //alert(IsNumber(txtResults));
}
//-->
</script>
Current Number Format -
<webControls:ExLabel ID="lblNUmberFormat" runat="server"></webControls:ExLabel>
<br />
ExTextBox 1 -
<asp:textbox id="txtNumber1" runat="server" isrequired="true" />
<br />
ExTextBox 2 -
<asp:textbox id="txtNumber2" runat="server" />
<webControls:ExRequiredFieldValidator ID="rfvNumber2" runat="server" ControlToValidate="txtNumber2"
    ErrorMessage="Number 2 required">!</webControls:ExRequiredFieldValidator>
<br />
Result -
<webControls:ExLabel ID="lblTotal" runat="server"></webControls:ExLabel>
<br />
Result Formatted -
<webControls:ExLabel ID="lblTotalFormatted" runat="server"></webControls:ExLabel>
<br />
Result Formatted In TextBox -
<asp:TextBox ID="txtResults" runat="server" />
<br />
<br />
Compare Validator -
<asp:textbox id="txtNumber3" runat="server" />
<asp:comparevalidator id="cmpValid" runat="server" operator="DataTypeCheck" type="Double"
    controltovalidate="txtNumber2" errormessage="Number 3 Invalid">!</asp:comparevalidator>
<webcontrols:exbutton id="btn" runat="server" text="Click" OnClick="btn_Click" />

<br />
<br /><br /><br />
<hr />
<br />
<webcontrols:ExCalendar ID="calTest" runat="server"></webcontrols:ExCalendar>
<br />
Actual Value: <webcontrols:Exlabel ID="lblDateValue" runat="server"></webcontrols:Exlabel>
</asp:content>
