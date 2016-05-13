using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using Telerik.Web.UI;
using System.Text;

public partial class UserControls_TextEditor : UserControlBase
{
    #region Properties
    public bool IsRequired
    {
        get { return rfvMultiline.Enabled; }
        set { rfvMultiline.Enabled = value; }
    }

    public string EditorSkinID
    {
        get { return rdMultiline.SkinID; }
        set { rdMultiline.SkinID = value; }
    }

    public int LabelID { get; set; }

    public RadEditor EditorControl
    {
        get { return rdMultiline; }
        set { rdMultiline = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        SetValidationMessages();
        RegisterJavaScript();
    }

    protected void cvMultiline_OnServerValidation(object sendor, ServerValidateEventArgs args)
    {
        if (rdMultiline.Content.Length > WebConstants.MAX_LENGTH_FOR_ATTRIBUTE_VALUE)
        {
            args.IsValid = false;
        }
    }

    private void SetValidationMessages()
    {
        cvMultiline.ErrorMessage = Helper.GetMaxLengthErrorMessage(this.LabelID, WebConstants.MAX_LENGTH_FOR_ATTRIBUTE_VALUE);
        rfvMultiline.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.LabelID);
    }

    private void RegisterJavaScript()
    {
        rdMultiline.Attributes.Remove("lblCharCount");
        rdMultiline.Attributes.Add("lblCharCount", lblCharCount.ClientID);
        if (!this.Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "EditorFunctions"))
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" var editorMaxLength = " + WebConstants.MAX_LENGTH_FOR_ATTRIBUTE_VALUE.ToString() + ";");

            sb.AppendLine(" function ValidateMultiline(sender, args) {");
            sb.AppendLine("     var rdMultiline = $find(sender.controltovalidate);");
            sb.AppendLine("     var htmlContent = rdMultiline.get_html();");
            sb.AppendLine("     if (htmlContent.length > editorMaxLength)");
            sb.AppendLine("     {    ");
            sb.AppendLine("         args.IsValid = false;");
            sb.AppendLine("         return false;");
            sb.AppendLine("     }");
            sb.AppendLine(" }");

            sb.AppendLine(" function OnClientLoad(editor, args)");
            sb.AppendLine(" {");
            sb.AppendLine("     UpdateCharCount(editor, args);");
            sb.AppendLine("     editor.attachEventHandler('onkeydown', function(e)");
            sb.AppendLine("     {");
            sb.AppendLine("         UpdateCharCount(editor, args);");
            sb.AppendLine("     });");
            sb.AppendLine(" }        ");

            sb.AppendLine(" function UpdateCharCount(editor, args)");
            sb.AppendLine(" {");
            sb.AppendLine("     var remainingChars = editorMaxLength - editor.get_html().length;");
            sb.AppendLine("     var lblCharCount = document.getElementById(editor.get_element().getAttribute('lblCharCount'));");
            sb.AppendLine("     lblCharCount.textContent = remainingChars;");
            sb.AppendLine(" }");
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "EditorFunctions", sb.ToString(), true);
        }
    }
}
