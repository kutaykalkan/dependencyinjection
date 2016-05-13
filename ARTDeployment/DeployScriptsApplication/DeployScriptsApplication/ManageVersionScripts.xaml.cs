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
using SkyStem.ART.Client.Model;
using DeployScriptsApplication.APP.BLL;
using Microsoft.Windows.Controls;
using System.Configuration;

namespace DeployScriptsApplication
{
    /// <summary>
    /// Interaction logic for ManageVersionScripts.xaml
    /// </summary>
    public partial class ManageVersionScripts : Window
    {

        public ManageVersionScripts()
        {
            InitializeComponent();
            BindVersionType();
            BindVersions();

        }
        List<VersionScriptInfo> oVersionScriptInfoList;
        List<VersionMstInfo> oVersionMstInfoList;
        private void BindVersions()
        {
            oVersionMstInfoList = Helper.GetAllVersionList();
            VersionMstInfo oVersionMstInfo = new VersionMstInfo();
            oVersionMstInfo.VersionNumber = "Select One";
            oVersionMstInfo.VersionID = -2;
            oVersionMstInfoList.Add(oVersionMstInfo);
            oVersionMstInfo = new VersionMstInfo();
            oVersionMstInfo.VersionNumber = "Create New";
            oVersionMstInfo.VersionID = -1;
            oVersionMstInfoList.Add(oVersionMstInfo);

            cbVersions.ItemsSource = oVersionMstInfoList;
            cbVersions.DisplayMemberPath = "VersionNumber";
            cbVersions.SelectedValuePath = "VersionID";
            cbVersions.SelectedValue = "-2";

        }
        private void BindVersionType()
        {
            int DeploymentEnvironmentID = Convert.ToInt32(ConfigurationManager.AppSettings["DeploymentEnvironmentID"]);
            List<VersionTypeMstInfo> oVersionTypeMstInfoList;
            List<VersionTypeMstInfo> oAllVersionTypeMstInfoList = Helper.GetAllVersionTypeList();
            oVersionTypeMstInfoList = oAllVersionTypeMstInfoList.FindAll(obj => obj.VersionTypeID.Value == DeploymentEnvironmentID).ToList();
            VersionTypeMstInfo oVersionTypeMstInfo = new VersionTypeMstInfo();
            oVersionTypeMstInfo.VersionType = "Select One";
            oVersionTypeMstInfo.VersionTypeID = -2;
            oVersionTypeMstInfoList.Add(oVersionTypeMstInfo);

            cbVersionType.ItemsSource = oVersionTypeMstInfoList;
            cbVersionType.DisplayMemberPath = "VersionType";
            cbVersionType.SelectedValuePath = "VersionTypeID";
            cbVersionType.SelectedValue = "-2";

        }
        private void cbVersions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string SelectedVal = string.Empty;

            if (cbVersions.SelectedValue != null)
                SelectedVal = cbVersions.SelectedValue.ToString();
            if (SelectedVal == "-1")
            {
                txtVersionNumber.Visibility = Visibility.Visible;
                btnAddVersion.Visibility = Visibility.Visible;
                BinddgAddedVersionScript(null);
            }
            else if (!string.IsNullOrEmpty(SelectedVal) && SelectedVal != "-2")
            {
                int SelVal = 0;
                if (cbVersions.SelectedValue != null)
                    SelVal = Convert.ToInt32(cbVersions.SelectedValue);
                txtVersionNumber.Visibility = Visibility.Hidden;
                btnAddVersion.Visibility = Visibility.Hidden;
                oVersionScriptInfoList = Helper.GetVersionScriptList(SelVal);
                BinddgAddedVersionScript(oVersionScriptInfoList);
                //bool IsVersionScriptExecuted = oVersionMstInfoList.Find(o => o.VersionID.Value == SelVal).IsVersionScriptExecuted;
                ShowHideBtn();
            }
            else
            {
                txtVersionNumber.Visibility = Visibility.Hidden;
                btnAddVersion.Visibility = Visibility.Hidden;
                BinddgAddedVersionScript(null);
            }
        }
        //private void ShowHideControls(bool IsVersionScriptExecuted)
        //{
        //    if (IsVersionScriptExecuted)
        //    {
        //        //lblSelectFile.Visibility = Visibility.Hidden;
        //        //FileNameTextBox.Visibility = Visibility.Hidden;
        //        //btnBrowse.Visibility = Visibility.Hidden;
        //        //btnAdd.Visibility = Visibility.Hidden;
        //        //btnSave.Visibility = Visibility.Hidden;
        //        //ShowHideBtnDel(Visibility.Hidden);

        //    }
        //    else
        //    {
        //        lblSelectFile.Visibility = Visibility.Visible;
        //        FileNameTextBox.Visibility = Visibility.Visible;
        //        btnBrowse.Visibility = Visibility.Visible;
        //        btnSave.Visibility = Visibility.Visible;
        //        btnAdd.Visibility = Visibility.Visible;
        //        //ShowHideBtnDel(Visibility.Visible);
        //    }
        //}
        private void ShowHideBtn()
        {
            VersionScriptInfo oVersionScriptInfo = null;
            for (int i = 0; i < dgAddedVersionScript.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)dgAddedVersionScript.ItemContainerGenerator.ContainerFromIndex(i);
                if (row == null)
                {
                    dgAddedVersionScript.UpdateLayout();
                    row = (DataGridRow)dgAddedVersionScript.ItemContainerGenerator.ContainerFromIndex(i);
                   
                }
                oVersionScriptInfo = (VersionScriptInfo)row.Item;
                Button BtnDel = FindChild<Button>(row, "Del");
                if (BtnDel != null)
                {
                    if (oVersionScriptInfo.IsNew || oVersionScriptInfo.IsVersionScriptExecuted == false)
                        BtnDel.Visibility = Visibility.Visible;
                    else
                        BtnDel.Visibility = Visibility.Hidden;
                }
                TextBox txtScriptOrder = FindChild<TextBox>(row, "txtScriptOrder");
                if (txtScriptOrder != null)
                {
                    if (oVersionScriptInfo.IsNew || oVersionScriptInfo.IsVersionScriptExecuted == false)
                        txtScriptOrder.IsReadOnly  = false;
                    else
                        txtScriptOrder.IsReadOnly = true;
                }
            }
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
        private void BinddgAddedVersionScript(List<VersionScriptInfo> oVersionScriptInfoList)
        {
            dgAddedVersionScript.ItemsSource = oVersionScriptInfoList;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string StatusMsg = Helper.SaveVersionScript(oVersionScriptInfoList);
            if (StatusMsg == "VersionScript  saved to database successfully")
            {
                int SelVal = 0;
                if (cbVersions.SelectedValue != null)
                    SelVal = Convert.ToInt32(cbVersions.SelectedValue);
                //oVersionScriptInfoList = Helper.GetVersionScriptList(SelVal);
                CompanyVersion ownd = new CompanyVersion();
                ownd.Show();
                this.Close();
                ownd.SetVersionID(SelVal);
            }
            //MessageBox.Show(StatusMsg);
        }
        private void btnGoToCompanyversion_Click(object sender, RoutedEventArgs e)
        {
            CompanyVersion ownd = new CompanyVersion();
            ownd.Show();
            this.Close();
        }
        private void btnAddVersion_Click(object sender, RoutedEventArgs e)
        {
            string VersionTypeID = cbVersionType.SelectedValue.ToString();
            if (VersionTypeID != "-2")
            {
                if (!string.IsNullOrEmpty(txtVersionNumber.Text))
                {

                    int NewVersionID;
                    VersionMstInfo oVersionMstInfo = new VersionMstInfo();
                    oVersionMstInfo.VersionNumber = txtVersionNumber.Text;
                    oVersionMstInfo.AddedBy = "DeployApplication";
                    oVersionMstInfo.DateAdded = DateTime.Now;
                    oVersionMstInfo.VersionTypeID = Convert.ToInt32(VersionTypeID);
                    NewVersionID = Helper.SaveNewVersion(oVersionMstInfo);
                    if (NewVersionID > 0)
                    {
                        BindVersions();
                        cbVersions.SelectedValue = NewVersionID.ToString();
                        //ShowHideControls(false);
                        if (oVersionScriptInfoList != null && oVersionScriptInfoList.Count > 0)
                            oVersionScriptInfoList.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Version Number.");
                }
            }
            else
            {
                MessageBox.Show("Please select Version Type.");
            }
        }
        string FName = string.Empty;
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            string VersionID = cbVersions.SelectedValue.ToString();
            if (!(VersionID == "-1" || VersionID == "-2"))
            {
                // Create OpenFileDialog
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                // Set filter for file extension and default file extension
                dlg.DefaultExt = ".sql";
                dlg.Filter = "Text documents (.sql)|*.sql";
                // Display OpenFileDialog by calling ShowDialog method
                Nullable<bool> result = dlg.ShowDialog();
                // Get the selected file name and display in a TextBox
                if (result == true)
                {
                    string filename = dlg.FileName;
                    FileNameTextBox.Text = filename;
                    FName = dlg.SafeFileName;
                    //string fn = dlg.SafeFileName;
                    //string TargetFilePath = Helper.GetBaseFolderForVersionNumber(VersionID) + fn;
                    //System.IO.File.Copy(fn, TargetFilePath, true);
                    //// System.IO.File.Copy(fn, @"E:\TestFolder\" + fn);
                    //string VersionFolderPath = Helper.GetVersionNumberFolderPath(VersionID) + fn;

                    //VersionScriptInfo oVersionScriptInfo = new VersionScriptInfo();
                    //oVersionScriptInfo.ScriptName = fn;
                    //short SOrder = 0;
                    //if (oVersionScriptInfoList != null && short.TryParse((oVersionScriptInfoList.Count + 1).ToString(), out SOrder))
                    //{
                    //    oVersionScriptInfo.ScriptOrder = SOrder;
                    //    oVersionScriptInfo.VersionScriptID = SOrder * -1;
                    //}
                    //else
                    //{
                    //    SOrder = 1;
                    //    oVersionScriptInfo.ScriptOrder = SOrder;
                    //    oVersionScriptInfo.VersionScriptID = SOrder * -1;
                    //}
                    //oVersionScriptInfo.ScriptPath = VersionFolderPath;
                    //oVersionScriptInfo.VersionID = Convert.ToInt32(VersionID);
                    //oVersionScriptInfo.AddedBy = "DeployApplication";
                    //oVersionScriptInfo.DateAdded = DateTime.Now;
                    //oVersionScriptInfo.IsNew = true;
                    //if (oVersionScriptInfoList != null && oVersionScriptInfoList.Count > 0)
                    //{
                    //    var obj = (from o in oVersionScriptInfoList
                    //               where o.ScriptName.Trim() == fn.Trim()
                    //               select o).ToList();
                    //    if (obj != null && obj.Count == 0)
                    //    {
                    //        oVersionScriptInfoList.Add(oVersionScriptInfo);
                    //        BinddgAddedVersionScript(null);
                    //        BinddgAddedVersionScript(oVersionScriptInfoList);
                    //    }
                    //}
                    //else
                    //{
                    //    oVersionScriptInfoList = new List<VersionScriptInfo>();
                    //    oVersionScriptInfoList.Add(oVersionScriptInfo);
                    //    BinddgAddedVersionScript(null);
                    //    BinddgAddedVersionScript(oVersionScriptInfoList);

                    //}
                }
            }
        }
        private void Del_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult objResult;
            objResult = MessageBox.Show("Are You sure You want to delete ?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (objResult == MessageBoxResult.Yes)
            {
                long ID = Convert.ToInt64(((Button)sender).CommandParameter);
                if (ID > 0)
                {
                    VersionScriptInfo oVersionScriptInfo = new VersionScriptInfo();
                    oVersionScriptInfo.VersionScriptID = ID;
                    oVersionScriptInfo.DateRevised = DateTime.Now;
                    oVersionScriptInfo.RevisedBy = "DeployApplication";
                    Helper.DeleteVersionScript(oVersionScriptInfo);

                }
                RemoveFile(ID);
                oVersionScriptInfoList.RemoveAll(o => o.VersionScriptID == ID);
                BinddgAddedVersionScript(null);
                BinddgAddedVersionScript(oVersionScriptInfoList);
            }
        }
        private void RemoveFile(long VersionScriptID)
        {
            VersionScriptInfo DelVersionScriptInfo = (from obj in oVersionScriptInfoList
                                                      where obj.VersionScriptID == VersionScriptID
                                                      select obj).FirstOrDefault();
            if (DelVersionScriptInfo != null)
            {
                string DelFileName = DelVersionScriptInfo.ScriptName;
                string DelFilePath = DelVersionScriptInfo.ScriptPath;
                string VersionID = cbVersions.SelectedValue.ToString();
                string TargetFilePath = Helper.GetBaseFolder() + DelFilePath;
                System.IO.File.Delete(TargetFilePath);
            }

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(FName) && !string.IsNullOrEmpty(FileNameTextBox.Text))
            {
                string VersionID = cbVersions.SelectedValue.ToString();
                string VersionNumber = cbVersions.Text.ToString();
                string TargetFilePath = Helper.GetBaseFolderForVersionNumber(VersionNumber) + FName;
                System.IO.File.Copy(FName, TargetFilePath, true);
                // System.IO.File.Copy(fn, @"E:\TestFolder\" + fn);
                string VersionFolderPath = Helper.GetVersionNumberFolderPath(VersionNumber) + FName;

                VersionScriptInfo oVersionScriptInfo = new VersionScriptInfo();
                oVersionScriptInfo.ScriptName = FName;
                short SOrder = 0;
                if (oVersionScriptInfoList != null && short.TryParse((oVersionScriptInfoList.Count + 1).ToString(), out SOrder))
                {
                    oVersionScriptInfo.ScriptOrder = SOrder;
                    oVersionScriptInfo.VersionScriptID = SOrder * -1;
                }
                else
                {
                    SOrder = 1;
                    oVersionScriptInfo.ScriptOrder = SOrder;
                    oVersionScriptInfo.VersionScriptID = SOrder * -1;
                }
                oVersionScriptInfo.ScriptPath = VersionFolderPath;
                oVersionScriptInfo.VersionID = Convert.ToInt32(VersionID);
                oVersionScriptInfo.AddedBy = "DeployApplication";
                oVersionScriptInfo.DateAdded = DateTime.Now;
                oVersionScriptInfo.IsNew = true;
                if (oVersionScriptInfoList != null && oVersionScriptInfoList.Count > 0)
                {
                    var obj = (from o in oVersionScriptInfoList
                               where o.ScriptName.Trim() == FName.Trim()
                               select o).ToList();
                    if (obj != null && obj.Count == 0)
                    {
                        oVersionScriptInfoList.Add(oVersionScriptInfo);
                        BinddgAddedVersionScript(null);
                        BinddgAddedVersionScript(oVersionScriptInfoList);
                    }
                    ShowHideBtn();
                }
                else
                {
                    oVersionScriptInfoList = new List<VersionScriptInfo>();
                    oVersionScriptInfoList.Add(oVersionScriptInfo);
                    BinddgAddedVersionScript(null);
                    BinddgAddedVersionScript(oVersionScriptInfoList);
                }
            }
            FileNameTextBox.Text = "";

        }
    }
}
