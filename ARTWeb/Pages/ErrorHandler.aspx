<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true"
    CodeFile="ErrorHandler.aspx.cs" Inherits="ErrorHandler" Title="Untitled Page"
    Theme="SkyStemBlueBrown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="JavaScript">
		function showHide(targetName) 
		{
			if( document.getElementById ) 
			{ // NS6+
				target = document.getElementById(targetName);
			} 
			else if( document.all ) 
			{ // IE4+
				target = document.all[targetName];
			}
	        
			if( target ) 
			{
				if( target.style.display == "none" ) 
				{
					target.style.display = "inline";
				} 
				else 
				{
					target.style.display = "none";
				}
			}
		} 
    </script>

    <table width="80%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" colspan="2">
                <webControls:ExLabel ID="lblErrorOccured" runat="server" SkinID="BlueBold11ArialUnderline"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td class="BlankRow">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <webControls:ExLabel ID="lblErrorHeader" runat="server" LabelID="1724" SkinID="Black11ArialNormal"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td width="10%">
                <webControls:ExLabel ID="lblError" runat="server" LabelID="1051" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel runat="server" ID="lblMessage" SkinID="Black11ArialNormal"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
