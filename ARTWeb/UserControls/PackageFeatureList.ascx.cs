using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using SkyStem.ART.App.Services;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using System.Collections.Generic;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;

namespace SkyStem.ART.Web.UserControls
{

    public partial class UserControls_PackageFeatureList : System.Web.UI.UserControl
    {
        List<PackageMstInfo> lstPackageMstInfo;
        int iPackageCount = 0;
        int iMatrixColumnCount = 0;
        int? iPackageId;
        Int16 iCustomizedPackage;
        DataSet dsFeaturesPackageAvailabilityMatrix;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblSerialNo.Text = LanguageUtil.GetValue(2081);
            lblFeatureName.Text = LanguageUtil.GetValue(2143);
            lblFeatureAvailable.Text= LanguageUtil.GetValue(2082);
            lblFeatureNotAvailable.Text = LanguageUtil.GetValue(2083);
            lblFeatureAvailablePartial.Text = LanguageUtil.GetValue(2084); 
            if (Request.QueryString[QueryStringConstants.POSTBACK_PACKAGEID] != "" && Request.QueryString[QueryStringConstants.POSTBACK_PACKAGEID] != string.Empty)
            {
                iPackageId = Convert.ToInt32(Request.QueryString[QueryStringConstants.POSTBACK_PACKAGEID]) ;
            }

            if (Request.QueryString[QueryStringConstants.POSTBACK_ISCUSTOMIZED_PACKAGE] != "" && Request.QueryString[QueryStringConstants.POSTBACK_ISCUSTOMIZED_PACKAGE] != string.Empty)
            {
                iCustomizedPackage = Convert.ToInt16(Request.QueryString[QueryStringConstants.POSTBACK_ISCUSTOMIZED_PACKAGE]);
            }
            
            lstPackageMstInfo = SessionHelper.GetAllPackage();
            iPackageCount = lstPackageMstInfo.Count;
            rptPackageHeader.DataSource = lstPackageMstInfo;
            rptPackageHeader.DataBind();
            Package oPackage = (Package)RemotingHelper.GetPackageObject();
            string str = oPackage.GetFeaturesPackageAvailabilityMatrix( Helper.GetAppUserInfo());
            dsFeaturesPackageAvailabilityMatrix = XmlStringToDataSet(str);
            iMatrixColumnCount = dsFeaturesPackageAvailabilityMatrix.Tables[0].Columns.Count;
            rptFeatures.DataSource = dsFeaturesPackageAvailabilityMatrix;
            rptFeatures.DataBind();


            //******************  this is working *******************
            //Package oPackage=(Package)RemotingHelper.GetPackageObject();
            //string str = oPackage.GetFeaturesPackageAvailabilityMatrix();
            //gvPackageFeatureList.DataSource = XmlStringToDataSet(str);
            //gvPackageFeatureList.DataBind();
            //FeaturesPackageRepeater.DataSource = XmlStringToDataSet(str);
            //FeaturesPackageRepeater.DataBind();
            //******************  this is working *******************  
            //Response.Write(str);
            
        }

        protected void rptPackageHeader_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (iPackageId != null)
            {
                PackageMstInfo currentItem = (PackageMstInfo)e.Item.DataItem;
                if (currentItem.PackageID == iPackageId)
                {
                    //ExLabel lblPackageName = ((ExLabel)e.Item.FindControl("lblPackageName"));
                    //lblPackageName.BackColor = System.Drawing.Color.DarkOrange;



                     System.Web.UI.HtmlControls.HtmlGenericControl currentCell = ( System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("tdPackageName");
                     currentCell.Attributes.Add("class", "SelectedPackage");
                   //currentCell.Attributes.Add("class", "SelectedPackage");
                    if (iCustomizedPackage == 1)
                    {
                        //((ExImageButton)e.Item.FindControl("imgIsCutomizedPackage")).Visible = true;
                        ((ExLabel)e.Item.FindControl("lblIsCutomizedPackage")).Visible = true;
                        lblFootNotes.Text = String.Format(LanguageUtil.GetValue(2088), "*");
                        lblFootNotes.Visible = true;
                    }
                }
              
            }
        }

       

        protected void rptFeatures_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int? iCurrentRowFeatureId;
            ListItemCollection listItemCollection = new ListItemCollection();
            List<string> row = new List<string>();
            DataRowView currentItem = (DataRowView)e.Item.DataItem;
            ((ExLabel)e.Item.FindControl("lblSerialNo")).Text =(e.Item.ItemIndex+1).ToString();
            iCurrentRowFeatureId = Convert.ToInt32(currentItem.Row["FeatureId"]);
            foreach (PackageMstInfo oPackageMstInfo in lstPackageMstInfo)
            {
               //row.Add(currentItem.Row[oPackageMstInfo.PackageName].ToString());
               listItemCollection.Add(new ListItem(Convert.ToString(currentItem.Row[oPackageMstInfo.PackageName]),iCurrentRowFeatureId.ToString()));
            }
            Repeater objRepeater = ((Repeater)e.Item.FindControl("rptFeatureAvailable"));
            objRepeater.DataSource = listItemCollection;// row;
            objRepeater.DataBind();
        }

        protected void rptFeatureAvailable_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int iRowIndex=e.Item.ItemIndex;
            Label lblIsFeatureAvailable = ((Label)e.Item.FindControl("lblIsFeatureAvailable"));
            ListItem listItem = (ListItem)e.Item.DataItem;
            int? iCurrentRowFeatureId = Convert.ToInt32(listItem.Value);
            if (lblIsFeatureAvailable != null)
            {
                //lblIsFeatureAvailable.Text = e.Item.DataItem.ToString();
                lblIsFeatureAvailable.Text = listItem.Text;
            }
            ExImageButton img = ((ExImageButton)e.Item.FindControl("imgShowFeatureAvailablity"));
            img.PostBackUrl = "javascript:void(0);";
            //Display Text/Image/icon based on FeatureName/FeatureId condition
            if (e.Item.DataItem.ToString() == "0")
                // Feature not available 
                img.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/CrossIcon.gif";
            else if (e.Item.DataItem.ToString() == "1")
                // Feature not available and IsFullFeatureAvailable=True
                img.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/BlueTickIcon.gif";
            else
                // Feature not available and IsFullFeatureAvailable=True
                img.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/GreenTickIcon.gif";
            //Get list of Reports available in Current Feature with in specific Package.
            Repeater objRepeater = ((Repeater)e.Item.FindControl("rptAvailableReport"));
            int iIndexOfPackageColumnInMatrix = iMatrixColumnCount - iPackageCount + iRowIndex;
            string strPackageName = dsFeaturesPackageAvailabilityMatrix.Tables[0].Columns[iIndexOfPackageColumnInMatrix].ToString();
            //Get package Id
            PackageMstInfo oPackageMstInfo= lstPackageMstInfo.Find(obj => obj.PackageName == strPackageName);
            //Highlight columns in case, PackageId supplied to this control
            ////if (oPackageMstInfo.PackageID == iPackageId)
            ////{
            ////    HtmlTableCell currentCell = ((HtmlTableCell)e.Item.FindControl("tdIsFeatureAvailable"));
            ////    currentCell.Attributes.Add("class", "SelectedPackage");
            ////}

            if (oPackageMstInfo.PackageID == iPackageId)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl currentCell = ((System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("tdIsFeatureAvailable"));
                if (currentCell != null)
                    currentCell.Attributes.Add("class", "SelectedPackage");
                System.Web.UI.HtmlControls.HtmlGenericControl currentCell1 = ((System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("tdIsFeatureAvailable1"));
                if (currentCell1 != null)
                    currentCell1.Attributes.Add("class", "SelectedPackage");
            }


            //In case of Feature=Report load all report for specific PackageId
            if (iCurrentRowFeatureId == (int)WebEnums.Feature.Reports)
            {
                Report oReport = (Report)RemotingHelper.GetReportObject();
                objRepeater.DataSource = oReport.GetAllReportsByPackageId(oPackageMstInfo.PackageID, Helper.GetAppUserInfo());
                objRepeater.DataBind();
            }
            
            
        }

        protected void rptAvailableReport_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ReportMstInfo oReportMstInfo = (ReportMstInfo)e.Item.DataItem;
           ExLabel lblAvailableReport = ((ExLabel)e.Item.FindControl("lblAvailableReport"));
           ExLabel lblSerialNoAvailableReport = ((ExLabel)e.Item.FindControl("lblSerialNoAvailableReport"));
           ExLabel lblAvailableReport2 = ((ExLabel)e.Item.FindControl("lblAvailableReport2"));
           ExLabel lblSerialNoAvailableReport2 = ((ExLabel)e.Item.FindControl("lblSerialNoAvailableReport2"));
           if (oReportMstInfo != null)
           {
               if(lblAvailableReport != null)
               lblAvailableReport.LabelID = oReportMstInfo.ReportLabelID.Value;
               if (lblSerialNoAvailableReport != null)
               lblSerialNoAvailableReport.Text = (e.Item.ItemIndex + 1).ToString();
               if (lblAvailableReport2 != null)
                   lblAvailableReport2.LabelID = oReportMstInfo.ReportLabelID.Value;
               if (lblSerialNoAvailableReport2 != null)
                   lblSerialNoAvailableReport2.Text = (e.Item.ItemIndex + 1).ToString();
           }
        }

        private DataSet XmlStringToDataSet(string xmlString)
        {
            DataSet matrixDataSet = null;
            if (String.IsNullOrEmpty(xmlString))
            {
                return matrixDataSet;
            }

            try
            {
                using (System.IO.StringReader stringReader = new System.IO.StringReader(xmlString))
                {
                    matrixDataSet = new DataSet();
                    matrixDataSet.ReadXml(stringReader);
                }
            }
            catch
            {
                //return null
                matrixDataSet = null;
            }
            //return the DataSet
            return matrixDataSet;
        }
    }
}
