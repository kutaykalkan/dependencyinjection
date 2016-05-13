using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SkyStem.ART.Client.Model.CompanyDatabase;
using DeployScriptsApplication.APP.BLL;
using SkyStem.ART.Client.Model;

namespace DeployScriptsApplication
{
    /// <summary>
    /// Interaction logic for ReleaseStatus.xaml
    /// </summary>
    public partial class ReleaseStatus : Window
    {
        List<CompanyVersionScriptInfo> oAllCompanyVersionScriptInfoList;
        public ReleaseStatus()
        {
            InitializeComponent();
            BindVersions();
        }
        private void cbVersions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int SelectedVal = 0;
            if (cbVersions.SelectedValue != null)
                SelectedVal = Convert.ToInt32(cbVersions.SelectedValue);
            BindReleaseStatus(SelectedVal);
        }
        private void BindVersions()
        {
            List<VersionMstInfo> oVersionMstInfoList = Helper.GetAllVersionList();
            VersionMstInfo oVersionMstInfo = new VersionMstInfo();
            oVersionMstInfo.VersionNumber = "Select One";
            oVersionMstInfo.VersionID = -2;
            oVersionMstInfoList.Add(oVersionMstInfo);
            cbVersions.ItemsSource = oVersionMstInfoList;
            cbVersions.DisplayMemberPath = "VersionNumber";
            cbVersions.SelectedValuePath = "VersionID";
            cbVersions.SelectedValue = "-2";

        }
        public void BindReleaseStatusByVersionID(int VersionID)
        {
            cbVersions.SelectedValue = VersionID.ToString();
            BindReleaseStatus(VersionID);
        }
        private void BindReleaseStatus(int VersionID)
        {
            if (VersionID > 0)
            {
                oAllCompanyVersionScriptInfoList = Helper.GetAllCompanyVersionScriptInfoList(VersionID);
                ListCollectionView oLcv = new ListCollectionView(oAllCompanyVersionScriptInfoList);
                oLcv.GroupDescriptions.Add(new PropertyGroupDescription("CompanyName"));
                // oLcv.GroupDescriptions.Add(new PropertyGroupDescription("VersionNumber"));                
                dgReleaseStatus.ItemsSource = oLcv;
            }
            else
            {
                dgReleaseStatus.ItemsSource = null;
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            int SelectedVal = -1;
            if (cbVersions.SelectedValue != null)
                SelectedVal = Convert.ToInt32(cbVersions.SelectedValue);
            CompanyVersion ownd = new CompanyVersion();
            ownd.Show();
            this.Close();
            ownd.SetVersionID(SelectedVal);

        }
      
    }
}
