using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpertPdf.HtmlToPdf;
using System.Text;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Utility;
using System.IO;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Data;
using System.IO.Compression;

/// <summary>
/// Summary description for ExportHelper
/// </summary>
public class ExportHelper
{
    public ExportHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static PdfConverter GetPdfConverter(string pdfTitle, bool bShowCurrentRecPeriodInFooter, bool isHeaderFooterRequired)
    {
        PdfConverter oPdfConverter = new PdfConverter();
        // set the license key - required [Pick from Web.Config]
        string licenseKey = AppSettingHelper.GetAppSettingValue(AppSettingConstants.HTML_2_PDF_LICENSE_KEY);
        if (!string.IsNullOrEmpty(licenseKey))
        {
            oPdfConverter.LicenseKey = licenseKey;
        }

        oPdfConverter.AvoidImageBreak = true;
        oPdfConverter.AvoidTextBreak = true;
        oPdfConverter.PdfDocumentOptions.LiveUrlsEnabled = false;

        if (isHeaderFooterRequired)
        {
            oPdfConverter.PdfDocumentOptions.LeftMargin = 10;
            oPdfConverter.PdfDocumentOptions.RightMargin = 10;
            oPdfConverter.PdfDocumentOptions.TopMargin = 20;
            oPdfConverter.PdfDocumentOptions.BottomMargin = 20;

            // Header Options for the PDF being generated
            oPdfConverter.PdfDocumentOptions.ShowHeader = true;
            oPdfConverter.PdfHeaderOptions.DrawHeaderLine = false;
            oPdfConverter.PdfHeaderOptions.HeaderText = Helper.GetDisplayDateTime(DateTime.Now);
            oPdfConverter.PdfHeaderOptions.HeaderTextFontSize = 10;
            oPdfConverter.PdfHeaderOptions.HeaderTextAlign = HorizontalTextAlign.Right;

            // Footer Options for the PDF being generated
            oPdfConverter.PdfDocumentOptions.ShowFooter = true;
            oPdfConverter.PdfFooterOptions.DrawFooterLine = false;
            oPdfConverter.PdfFooterOptions.PageNumberingFormatString = string.Format(LanguageUtil.GetValue(2007), "&p;", "&P;");
            oPdfConverter.PdfFooterOptions.ShowPageNumber = true;
            oPdfConverter.PdfFooterOptions.FooterTextFontSize = 10;
            oPdfConverter.PdfFooterOptions.FooterText = ExportHelper.GetRecFormNamePeriodText(pdfTitle, bShowCurrentRecPeriodInFooter);
        }
        oPdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
        oPdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
        oPdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;

        oPdfConverter.PdfDocumentInfo.Title = pdfTitle;
        oPdfConverter.PdfDocumentInfo.Subject = pdfTitle;

        // set to generate selectable pdf or a pdf with embedded image - optional
        oPdfConverter.PdfDocumentOptions.GenerateSelectablePdf = true;

        // set the embedded fonts option - optional, by default is false
        oPdfConverter.PdfDocumentOptions.EmbedFonts = false;

        // set PDF security options - optional
        oPdfConverter.PdfSecurityOptions.CanPrint = true;
        oPdfConverter.PdfSecurityOptions.CanEditContent = false;

        //set PDF document description - optional
        oPdfConverter.PdfDocumentInfo.AuthorName = "SkyStem - ART";
        oPdfConverter.PdfDocumentOptions.FitWidth = true;
        return oPdfConverter;
    }

    public static string GetRecFormNamePeriodText(string pageTitle, bool bShowCurrentRecPeriodInFooter)
    {
        string footerText = "";
        if (bShowCurrentRecPeriodInFooter)
        {
            footerText = string.Format("{0}. {1}: {2}", pageTitle, LanguageUtil.GetValue(1420), Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate));
        }
        else
        {
            footerText = pageTitle;
        }
        return footerText;
    }


    public static string GetHTMLStringForPDF(string htmlToConvert, string pageTitle)
    {
        StringBuilder oHTML = new StringBuilder();
        oHTML.Append("<HTML>");
        oHTML.Append("<HEAD>");
        oHTML.Append("<link href='../App_Themes/SkyStemBlueBrown/Default.css' type='text/css' rel='stylesheet' />");
        oHTML.Append("<link href='../App_Themes/SkyStemBlueBrown/Splitter.SkyStemBlueBrown.css' type='text/css' rel='stylesheet' />");
        oHTML.Append("<style>.HideInPdf{display:none}</style>");
        oHTML.Append("</HEAD>");
        oHTML.Append("<BODY style='background-color:white;'>");
        oHTML.Append("<table cellpadding='0' cellspacing='0' style='vertical-align: top; width: 1020px;background-color: white; text-align: left;' border='0'>");
        oHTML.Append("<tr class='PageTitleBg'>");
        oHTML.Append("<td>");
        oHTML.Append(pageTitle);
        oHTML.Append("</td>");
        oHTML.Append("</tr>");
        oHTML.Append("<tr>");
        oHTML.Append("<td>&nbsp;</td>");
        oHTML.Append("</tr>");
        oHTML.Append("<tr>");
        oHTML.Append("<td>");
        oHTML.Append(htmlToConvert);
        oHTML.Append("</td>");
        oHTML.Append("</tr>");
        oHTML.Append("</table>");
        oHTML.Append("</BODY>");
        oHTML.Append("</HTML>");

        return oHTML.ToString();
    }

    public static string RemoveInvalidFileNameChars(string fileName)
    {
        char[] oInvalidCharsArray = Path.GetInvalidFileNameChars();

        if (fileName.IndexOfAny(oInvalidCharsArray) != -1)
        {
            // means file name contains an invalid char
            // Replace each of the Invalid Char with a "_"
            for (int i = 0; i < oInvalidCharsArray.Length; i++)
            {
                fileName = fileName.Replace(oInvalidCharsArray[i], '_');
            }
        }

        // Also replace space in file name with "_", otherwise FireFox will not recognize the file
        fileName = fileName.Replace(' ', '_');
        return fileName;
    }

    public static void GeneratePDFAndRender(string pageTitle, string fileName, string htmlToConvert, bool bShowCurrentRecPeriodInFooter, bool isHeaderFooterRequired)
    {
        PdfConverter pdfConverter = ExportHelper.GetPdfConverter(pageTitle, bShowCurrentRecPeriodInFooter, isHeaderFooterRequired);
        SendPDFByteStreamForDownload(fileName, ConvertHtmlToPDFBytes(pdfConverter, htmlToConvert),false);
    }

    public static void GeneratePDFAndRender(string pageTitle, string fileName, string htmlToConvert, bool bShowCurrentRecPeriodInFooter, bool isHeaderFooterRequired, bool isLandscape)
    {
        PdfConverter pdfConverter = ExportHelper.GetPdfConverter(pageTitle, bShowCurrentRecPeriodInFooter, isHeaderFooterRequired);
        if (isLandscape)
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape;
        SendPDFByteStreamForDownload(fileName, ConvertHtmlToPDFBytes(pdfConverter, htmlToConvert),false);
    }

    public static byte[] ConvertHtmlToPDFBytes(PdfConverter pdfConverter, string htmlToConvert)
    {
        byte[] oByteCollection = null;
        string baseUri = AppSettingHelper.GetAppSettingValue(AppSettingConstants.BASE_SYSTEM_URL_FOR_PDF) + HttpContext.Current.Request.Url.AbsolutePath;
        oByteCollection = pdfConverter.GetPdfBytesFromHtmlString(htmlToConvert, baseUri);
        return oByteCollection;
    }

    public static void SendPDFByteStreamForDownload(string fileName, byte[] oByteCollection, bool IsGetOriginalFileName)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ClearContent();
        //HttpContext.Current.Response.AddHeader("Content-Type", "binary/octet-stream");
        HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");
        if (IsGetOriginalFileName)
            HttpContext.Current.Response.AddHeader("Content-Disposition",
                "attachment; filename=" + GetOriginalFileName(fileName) + "; size=" + oByteCollection.Length.ToString());
        else
            HttpContext.Current.Response.AddHeader("Content-Disposition",
           "attachment; filename=" + fileName + "; size=" + oByteCollection.Length.ToString());
        //HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.BinaryWrite(oByteCollection);
        //HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }

    public static void GeneratePDFAndSendMail(string htmlToConvert, string mailBody, string toAddress, string fromEmailAddress, string subject, string pageTitle, string fileName)
    {
        string baseUri = string.Empty;
        fileName = ExportHelper.RemoveInvalidFileNameChars(fileName);
        string filePath = SharedDataImportHelper.GetFolderForTemporaryFilesForExport() + fileName;
        PdfConverter oPdfConverter = ExportHelper.GetPdfConverter(pageTitle, true, true);

        baseUri = AppSettingHelper.GetAppSettingValue(AppSettingConstants.BASE_SYSTEM_URL_FOR_PDF) + HttpContext.Current.Request.Url.AbsolutePath;
        oPdfConverter.SavePdfFromHtmlStringToFile(htmlToConvert, filePath, baseUri);
        List<String> oFilePathCollection = new List<string>();
        oFilePathCollection.Add(filePath);


        StringBuilder sb = new StringBuilder();
        sb.Append(mailBody);
        sb.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendByUser, fromEmailAddress));

        MailHelper.SendEmail(fromEmailAddress, toAddress, subject, sb.ToString(), oFilePathCollection);
    }
    public static void GeneratePDFAndSendMail(string htmlToConvert, string mailBody, string toAddress, string fromEmailAddress, string subject, string pageTitle, string fileName, List<string> oFilePathList)
    {
        string baseUri = string.Empty;
        fileName = ExportHelper.RemoveInvalidFileNameChars(fileName);
        string filePath = SharedDataImportHelper.GetFolderForTemporaryFilesForExport() + fileName;
        PdfConverter oPdfConverter = ExportHelper.GetPdfConverter(pageTitle, true, true);

        baseUri = AppSettingHelper.GetAppSettingValue(AppSettingConstants.BASE_SYSTEM_URL_FOR_PDF) + HttpContext.Current.Request.Url.AbsolutePath;
        oPdfConverter.SavePdfFromHtmlStringToFile(htmlToConvert, filePath, baseUri);
        List<String> oFilePathCollection = new List<string>();
        oFilePathCollection.Add(filePath);
        oFilePathCollection.AddRange(oFilePathList);


        StringBuilder sb = new StringBuilder();
        sb.Append(mailBody);
        sb.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendByUser, fromEmailAddress));

        MailHelper.SendEmail(fromEmailAddress, toAddress, subject, sb.ToString(), oFilePathCollection);
    }

    #region Rec Form Pdf

    public static void GeneratePDFAndRender(string pageTitle, string pageFooter, string fileName, string htmlToConvert, bool bShowCurrentRecPeriodInFooter)
    {
        byte[] oByteCollection = GeneratePDFBytes(pageTitle, pageFooter, htmlToConvert, bShowCurrentRecPeriodInFooter);
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("Content-Type", "binary/octet-stream");
        HttpContext.Current.Response.AddHeader("Content-Disposition",
            "attachment; filename=" + fileName + "; size=" + oByteCollection.Length.ToString());
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.BinaryWrite(oByteCollection);
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }

    public static byte[] GeneratePDFBytes(string pageTitle, string pageFooter, string htmlToConvert, bool bShowCurrentRecPeriodInFooter)
    {
        byte[] oByteCollection = null;
        PdfConverter pdfConverter = ExportHelper.GetPdfConverter(pageTitle, pageFooter, bShowCurrentRecPeriodInFooter);
        string baseUri = AppSettingHelper.GetAppSettingValue(AppSettingConstants.BASE_SYSTEM_URL_FOR_PDF) + HttpContext.Current.Request.Url.AbsolutePath;
        oByteCollection = pdfConverter.GetPdfBytesFromHtmlString(htmlToConvert, baseUri);
        return oByteCollection;
    }

    public static PdfConverter GetPdfConverter(string pdfTitle, string pageFooter, bool bShowCurrentRecPeriodInFooter)
    {
        PdfConverter oPdfConverter = new PdfConverter();
        // set the license key - required [Pick from Web.Config]
        string licenseKey = AppSettingHelper.GetAppSettingValue(AppSettingConstants.HTML_2_PDF_LICENSE_KEY);
        if (!string.IsNullOrEmpty(licenseKey))
        {
            oPdfConverter.LicenseKey = licenseKey;
        }

        oPdfConverter.AvoidImageBreak = true;
        oPdfConverter.AvoidTextBreak = true;
        oPdfConverter.PdfDocumentOptions.LiveUrlsEnabled = false;

        // Header Options for the PDF being generated
        oPdfConverter.PdfDocumentOptions.ShowHeader = true;
        oPdfConverter.PdfHeaderOptions.DrawHeaderLine = false;
        oPdfConverter.PdfHeaderOptions.HeaderText = Helper.GetDisplayDateTime(DateTime.Now);
        oPdfConverter.PdfHeaderOptions.HeaderTextFontSize = 10;
        oPdfConverter.PdfHeaderOptions.HeaderTextAlign = HorizontalTextAlign.Right;

        // Footer Options for the PDF being generated
        oPdfConverter.PdfDocumentOptions.ShowFooter = true;
        oPdfConverter.PdfFooterOptions.DrawFooterLine = false;
        oPdfConverter.PdfFooterOptions.PageNumberingFormatString = string.Format(LanguageUtil.GetValue(2007), "&p;", "&P;");
        oPdfConverter.PdfFooterOptions.ShowPageNumber = true;
        oPdfConverter.PdfFooterOptions.FooterTextFontSize = 10;
        oPdfConverter.PdfFooterOptions.FooterText = pageFooter;// ExportHelper.GetRecFormNamePeriodText(pdfTitle, bShowCurrentRecPeriodInFooter);

        oPdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
        oPdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
        oPdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;
        oPdfConverter.PdfDocumentOptions.LeftMargin = 10;
        oPdfConverter.PdfDocumentOptions.RightMargin = 10;
        oPdfConverter.PdfDocumentOptions.TopMargin = 20;
        oPdfConverter.PdfDocumentOptions.BottomMargin = 20;
        oPdfConverter.PdfDocumentInfo.Title = pdfTitle;
        oPdfConverter.PdfDocumentInfo.Subject = pdfTitle;

        // set to generate selectable pdf or a pdf with embedded image - optional
        oPdfConverter.PdfDocumentOptions.GenerateSelectablePdf = true;

        // set the embedded fonts option - optional, by default is false
        oPdfConverter.PdfDocumentOptions.EmbedFonts = false;

        // set PDF security options - optional
        oPdfConverter.PdfSecurityOptions.CanPrint = true;
        oPdfConverter.PdfSecurityOptions.CanEditContent = false;

        //set PDF document description - optional
        oPdfConverter.PdfDocumentInfo.AuthorName = "SkyStem - ART";
        return oPdfConverter;
    }

    #endregion

    public static string GenerateTempFilePath(string prefix, string ext)
    {
        return SharedDataImportHelper.GetFolderForTemporaryFilesForExport() + GenerateTempFileName(prefix, ext);
    }

    public static string GenerateTempFilePath(string fileName)
    {
        return SharedDataImportHelper.GetFolderForTemporaryFilesForExport() + fileName;
    }

    public static string GenerateTempFileName(string fileName, string ext)
    {
        return RemoveInvalidFileNameChars(fileName) + "_" + Guid.NewGuid().ToString().ToUpper() + "." + ext;
    }

    /// <summary>
    /// Downloads the attachment.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <param name="downloadfileName">Name of the downloadfile.</param>
    /// <param name="bDeleteAfterDownload">if set to <c>true</c> [b delete after download].</param>
    public static void DownloadAttachment(string fileName, string saveAsFileName, bool bDeleteAfterDownload)
    {
        string downloadfileName = saveAsFileName;
        FileInfo oFileInfo = new FileInfo(fileName);
        if (saveAsFileName.Substring(saveAsFileName.Length - oFileInfo.Extension.Length) != oFileInfo.Extension)
            saveAsFileName = saveAsFileName + "." + oFileInfo.Extension;
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + downloadfileName);
        HttpContext.Current.Response.AddHeader("Content-Length", oFileInfo.Length.ToString());
        HttpContext.Current.Response.ContentType = GetContentType(oFileInfo.Extension);
        HttpContext.Current.Response.WriteFile(fileName);
        if (bDeleteAfterDownload)
            File.Delete(oFileInfo.Name);
        HttpContext.Current.Response.End();
    }

    /// <summary>
    /// Gets the type of the content.
    /// </summary>
    /// <param name="ext">The ext.</param>
    /// <returns></returns>
    public static string GetContentType(string ext)
    {
        string ContentType = string.Empty;
        switch (ext.Trim())
        {
            case ".htm":
            case ".html":
                ContentType = "text/HTML";
                break;

            case ".txt":
                ContentType = "text/Plain";
                break;

            case ".doc":
            case ".docx":
            case ".rtf":
                ContentType = "Application/msword";
                break;

            case ".xls":
            case ".xlsx":
            case ".xlss":
            case ".csv":
                ContentType = "Application/x-msexcel";
                break;

            case ".pdf":
                ContentType = "Application/pdf";
                break;

            case ".ppt":
            case ".pptx":
                ContentType = "Application/ms-powerpoint";
                break;

            case ".mpeg":
                ContentType = "Video/mpeg";
                break;

            case ".jpg":
                ContentType = "image/jpg";
                break;

            case ".gif":
                ContentType = "image/gif";
                break;

            case ".bmp":
                ContentType = "image/bmp";
                break;

            default:
                ContentType = "application/octet-stream";
                break;
        }
        return ContentType;
    }

    public static string CreateZipFromFiles(string zipFileName, List<string> strfiles)
    {
        try
        {
            if (!string.IsNullOrEmpty(zipFileName) && strfiles != null && strfiles.Count > 0)
            {
                using (ZipArchive oZipArchive = ZipFile.Open(zipFileName, ZipArchiveMode.Create))
                {
                    foreach (string strFile in strfiles)
                    {
                        if (File.Exists(strFile))
                        {
                            oZipArchive.CreateEntryFromFile(strFile, ExportHelper.GetFileName(strFile));
                        }
                    }
                }
                return zipFileName;
            }
            return null;
        }
        catch (Exception)
        {
            throw new ARTException(5000381);
        }
    }
    /// <summary>
    /// Get the file name from full path and remove timestamp/suffix added by the application during upload
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetOriginalFileName(string fileName)
    {
        string actFileName = string.Empty;
        int fileNameStartIndex = fileName.LastIndexOf(@"\") + 1;
        int fileNameEndIndex = fileName.LastIndexOf("_");
        if (fileNameStartIndex >= 0 && fileNameEndIndex >= 0)
        {
            actFileName = fileName.Substring(fileNameStartIndex, fileNameEndIndex - fileNameStartIndex);
            int dotIndex = fileName.LastIndexOf(".");
            if (dotIndex >= 0)
                actFileName += fileName.Substring(dotIndex);
        }
        return actFileName.Replace(',', '_');
    }

    /// <summary>
    /// Get the file name from full path 
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetFileName(string fileName)
    {
        string actFileName = fileName;
        int fileNameStartIndex = fileName.LastIndexOf(@"\") + 1;
        if (fileNameStartIndex >= 0)
        {
            actFileName = fileName.Substring(fileNameStartIndex);
        }
        return actFileName;
    }

}
