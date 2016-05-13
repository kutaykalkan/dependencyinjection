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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DeployScriptsApplication.APP.BLL;
using SkyStem.ART.Client.Model;
using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Primitives;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace DeployScriptsApplication
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CompanyVersion : Window
    {
        public CompanyVersion()
        {
            InitializeComponent();
            BindVersions();
        }
        public void SetVersionID(int VersionID)
        {
            cbVersions.SelectedValue = VersionID.ToString();
            BindVersionScripts(VersionID);

        }
        private void BindVersionScripts(int VersionID)
        {
            oAllVersionScriptInfoList = Helper.GetVersionScriptList(VersionID);
            dgVersionScript.ItemsSource = oAllVersionScriptInfoList;
            oAllServerCompanyInfoList = Helper.GetAllServerCompanyList(VersionID);
            dgServerCompany.ItemsSource = oAllServerCompanyInfoList;
            ShowHideChk();
        }

        List<VersionScriptInfo> oAllVersionScriptInfoList;
        List<ServerCompanyInfo> oAllServerCompanyInfoList;
        List<CurrentDBVersion> oAllCurrentDBVersion;
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string msg;
            // List<long> VersionScriptIDList = SelectedVersionScriptIDList();
            //List<int> CompanyIDList = SelectedCompanyIDList();
            //if (VersionScriptIDList.Count > 0 && CompanyIDList.Count > 0)
            //{
            //    List<CompanyVersionScriptInfo> oCompanyVersionScriptInfolist = Helper.GetCompanyVersionScriptInfoListForSelectedRows(VersionScriptIDList, CompanyIDList);
            //    if (oCompanyVersionScriptInfolist.Count > 0)
            //    {
            //        string Msg = "This Version Scripts have already been run on following companie`s databases:";
            //        for (int i = 0; i < oCompanyVersionScriptInfolist.Count; i++)
            //        {
            //            Msg = Msg + "\n" + oCompanyVersionScriptInfolist[i].CompanyName;
            //        }
            //        Msg = Msg + "\n Please Deselect these companies.";
            //        MessageBox.Show(Msg);
            //    }
            //    else
            //    {

            //        List<VersionScriptInfo> oVersionScriptInfoList = SelectedVersionScriptInfoList(VersionScriptIDList);
            //        List<ServerCompanyInfo> oServerCompanyInfoList = SelectedServerCompanyInfoList(CompanyIDList);
            //        msg = Helper.RunScriptServerCompany(oVersionScriptInfoList, oServerCompanyInfoList, chkDbBackup.IsChecked);
            //        MessageBox.Show(msg);
            //    }
            //}
            //else
            //{
            //    if (VersionScriptIDList.Count <= 0)
            //        MessageBox.Show("No Script exist in selected Version.");
            //    else if (CompanyIDList.Count <= 0)
            //        MessageBox.Show("Please Select Atleast one company.");


            //}
            List<int> CompanyIDList = SelectedCompanyIDList();
            if (CompanyIDList.Count > 0)
            {
                //List<VersionScriptInfo> oVersionScriptInfoList = SelectedVersionScriptInfoList(VersionScriptIDList);
                List<ServerCompanyInfo> oServerCompanyInfoList = SelectedServerCompanyInfoList(CompanyIDList);
                CurrentDBVersion oNewCurrentDBVersion = new CurrentDBVersion();
                oNewCurrentDBVersion.CurrentDBVersionID = Convert.ToInt32(cbVersions.SelectedValue);
                oNewCurrentDBVersion.CurrentDBVersionNumber = cbVersions.Text;
                oNewCurrentDBVersion.DBVersionDate = System.DateTime.Now;
                msg = Helper.RunScriptServerCompany(oServerCompanyInfoList, chkDbBackup.IsChecked, oNewCurrentDBVersion, oAllCurrentDBVersion);
                ShowHideChk();
                MessageBox.Show(msg);
            }
            else
            {
                if (CompanyIDList.Count <= 0)
                    MessageBox.Show("Please Select Atleast one company.");
            }
        }
        private List<long> SelectedVersionScriptIDList()
        {
            List<long> SelectedVersionScriptIDList = new List<long>();
            for (int i = 0; i < dgVersionScript.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)dgVersionScript.ItemContainerGenerator.ContainerFromIndex(i);
                if (row == null)
                {
                    dgVersionScript.UpdateLayout();
                    row = (DataGridRow)dgVersionScript.ItemContainerGenerator.ContainerFromIndex(i);
                }
                //CheckBox checkBox = FindChild<CheckBox>(row, "Chk");
                //if (checkBox != null && checkBox.IsChecked == true)
                //{
                //TextBlock TextBlock = FindChild<TextBlock>(row, "txtVersionScriptID");
                //if (TextBlock != null)
                //{
                //    long VersionScriptID = Convert.ToInt64(TextBlock.Text);
                //    SelectedVersionScriptIDList.Add(VersionScriptID);
                //}
                //}
                if (row != null)
                {
                    VersionScriptInfo oVersionScriptInfo = (VersionScriptInfo)row.Item;
                    if (oVersionScriptInfo != null && oVersionScriptInfo.VersionScriptID.HasValue)
                        SelectedVersionScriptIDList.Add(oVersionScriptInfo.VersionScriptID.Value);
                }
            }
            return SelectedVersionScriptIDList;
        }
        private List<VersionScriptInfo> SelectedVersionScriptInfoList(List<long> VersionScriptIDList)
        {
            List<VersionScriptInfo> oVersionScriptInfoList = new List<VersionScriptInfo>();
            if (VersionScriptIDList != null && VersionScriptIDList.Count > 0 && oAllVersionScriptInfoList != null)
            {
                oVersionScriptInfoList = (from oVersionScriptInfo in oAllVersionScriptInfoList
                                          from VersionScriptID in VersionScriptIDList
                                          where oVersionScriptInfo.VersionScriptID == VersionScriptID
                                          select oVersionScriptInfo).ToList();
            }
            return oVersionScriptInfoList;
        }
        private List<int> SelectedCompanyIDList()
        {
            List<int> SelectedCompanyIDList = new List<int>();
            for (int i = 0; i < dgServerCompany.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)dgServerCompany.ItemContainerGenerator.ContainerFromIndex(i);
                if (row == null)
                {
                    dgServerCompany.UpdateLayout();
                    row = (DataGridRow)dgServerCompany.ItemContainerGenerator.ContainerFromIndex(i);
                }
                CheckBox checkBox = FindChild<CheckBox>(row, "Chk");
                if (checkBox != null && checkBox.IsChecked == true && checkBox.Visibility != Visibility.Hidden)
                {
                    ServerCompanyInfo oServerCompanyInfo = (ServerCompanyInfo)row.Item;
                    if (oServerCompanyInfo != null && oServerCompanyInfo.CompanyID.HasValue)
                        SelectedCompanyIDList.Add(oServerCompanyInfo.CompanyID.Value);

                    //TextBlock TextBlock = FindChild<TextBlock>(row, "txtServerCompanyID");
                    //if (TextBlock != null)
                    //{
                    //    int CompanyID = Convert.ToInt32(TextBlock.Text);
                    //    SelectedCompanyIDList.Add(CompanyID);
                    //}
                }
            }
            return SelectedCompanyIDList;
        }
        private List<ServerCompanyInfo> SelectedServerCompanyInfoList(List<int> CompanyIDList)
        {
            List<ServerCompanyInfo> SelectedServerCompanyInfoList = new List<ServerCompanyInfo>();
            if (CompanyIDList != null && CompanyIDList.Count > 0 && oAllServerCompanyInfoList != null)
            {
                SelectedServerCompanyInfoList = (from oServerCompanyInfo in oAllServerCompanyInfoList
                                                 from CompanyID in CompanyIDList
                                                 where oServerCompanyInfo.CompanyID == CompanyID
                                                 select oServerCompanyInfo).ToList();
            }
            return SelectedServerCompanyInfoList;
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
        private void cbVersions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int SelectedVal = 0;
            if (cbVersions.SelectedValue != null)
                SelectedVal = Convert.ToInt32(cbVersions.SelectedValue);
            if (SelectedVal > 0)
            {
                BindVersionScripts(SelectedVal);
            }
            else
            {
                dgVersionScript.ItemsSource = null;
                dgServerCompany.ItemsSource = null;
            }

        }
        private void HeadCheck(object sender, RoutedEventArgs e, bool IsChecked, DataGrid dg)
        {
            for (int i = 0; i < dg.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)dg.ItemContainerGenerator.ContainerFromIndex(i);
                if (row == null)
                {
                    dg.UpdateLayout();
                    row = (DataGridRow)dg.ItemContainerGenerator.ContainerFromIndex(i);
                }
                CheckBox checkBox = FindChild<CheckBox>(row, "Chk");
                if (checkBox != null && checkBox.Visibility != Visibility.Hidden)
                {
                    checkBox.IsChecked = IsChecked;
                }
            }
        }
        private void ShowHideChk()
        {
            oAllCurrentDBVersion = new List<CurrentDBVersion>();
            CurrentDBVersion oCurrentDBVersion = null;
            for (int i = 0; i < dgServerCompany.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)dgServerCompany.ItemContainerGenerator.ContainerFromIndex(i);
                if (row == null)
                {
                    dgServerCompany.UpdateLayout();
                    row = (DataGridRow)dgServerCompany.ItemContainerGenerator.ContainerFromIndex(i);
                }

                CheckBox checkBox = FindChild<CheckBox>(row, "Chk");

                //CurrentVersion
                TextBlock txtCurrentVersion = FindChild<TextBlock>(row, "CurrentVersion");
                if (txtCurrentVersion != null)
                {
                    if (row != null)
                    {
                        ServerCompanyInfo oServerCompanyInfo = (ServerCompanyInfo)row.Item;
                        oCurrentDBVersion = Helper.GetCurrentDBVersion(oServerCompanyInfo);
                        if (!string.IsNullOrEmpty(oCurrentDBVersion.CurrentDBVersionNumber))
                        {
                            oCurrentDBVersion.CompanyID = oServerCompanyInfo.CompanyID;
                            oCurrentDBVersion.ServerCompanyID = oServerCompanyInfo.ServerCompanyID;
                            oAllCurrentDBVersion.Add(oCurrentDBVersion);
                            txtCurrentVersion.Text = oCurrentDBVersion.CurrentDBVersionNumber;
                        }
                        else
                        {
                            txtCurrentVersion.Text = "Database does not exists!!";
                            if (checkBox != null)
                                checkBox.Visibility = Visibility.Hidden;
                        }
                    }
                }

                if (checkBox != null)
                {
                    //if (row != null)
                    //{
                    //    ServerCompanyInfo oServerCompanyInfo = (ServerCompanyInfo)row.Item;
                    //    if (oServerCompanyInfo != null && oServerCompanyInfo.IsVersionAlreadyRun.HasValue && oServerCompanyInfo.IsVersionAlreadyRun.Value == true)
                    //        checkBox.Visibility = Visibility.Hidden;
                    //}
                    int ddlVersionID = Convert.ToInt32(cbVersions.SelectedValue);
                    if (oCurrentDBVersion != null && oCurrentDBVersion.CurrentDBVersionID.HasValue && oCurrentDBVersion.CurrentDBVersionID.Value >= ddlVersionID)
                        checkBox.Visibility = Visibility.Hidden;
                }
            }
        }

        //private void CheckBox_Checked(object sender, RoutedEventArgs e)
        //{
        //    HeadCheck(sender, e, true, dgVersionScript);
        //}
        //private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    HeadCheck(sender, e, false, dgVersionScript);
        //}
        private void CheckBoxCompany_Checked(object sender, RoutedEventArgs e)
        {
            HeadCheck(sender, e, true, dgServerCompany);
        }
        private void CheckBoxCompany_Unchecked(object sender, RoutedEventArgs e)
        {
            HeadCheck(sender, e, false, dgServerCompany);
        }
        public static T FindChild<T>(DependencyObject parent, string childName)
          where T : DependencyObject
        {
            // Confirm parent is valid.  
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child 
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree 
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child.  
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search 
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name 
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found. 
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }
        private void btnReleaseStatus_Click(object sender, RoutedEventArgs e)
        {
            int SelectedVal = -1;
            if (cbVersions.SelectedValue != null)
                SelectedVal = Convert.ToInt32(cbVersions.SelectedValue);
            ReleaseStatus ownd = new ReleaseStatus();
            ownd.Show();
            this.Close();
            ownd.BindReleaseStatusByVersionID(SelectedVal);
        }

        private void btnAddVersionScript_Click(object sender, RoutedEventArgs e)
        {
            ManageVersionScripts ownd = new ManageVersionScripts();
            ownd.Show();
            this.Close();
        }

    }
}
