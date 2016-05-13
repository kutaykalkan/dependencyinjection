using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using System.Data;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "GLRecInputItem" here, you must also update the reference to "GLRecInputItem" in Web.config.
    public class GLDataRecItem : IGLDataRecItem
    {
        public void DoWork()
        {
        }

        #region "IGLRecItemInput Members"
        public List<GLDataRecItemInfo> GetRecItem(AppUserInfo oAppUserInfo)
        {
            //throw new NotImplementedException();
#if DEMO
            List<GLDataRecItemInfo> oGLRecItemInputCollection = new List<GLDataRecItemInfo>();
            GLDataRecItemInfo oGLRecItemInput1 = new GLDataRecItemInfo();
            oGLRecItemInput1.GLDataRecItemID = 1;
            oGLRecItemInput1.GLDataID = 10001;
            oGLRecItemInput1.ReconciliationCategoryID = 5;
            oGLRecItemInput1.ReconciliationCategoryTypeID = 7;
            oGLRecItemInput1.DataImportID = null;
            oGLRecItemInput1.IsAttachmentAvailable = false;
            oGLRecItemInput1.Amount = 275;
            oGLRecItemInput1.LocalCurrencyCode = "USD";
            oGLRecItemInput1.AmountBaseCurrency = 300;
            oGLRecItemInput1.AmountReportingCurrency = 275;
            oGLRecItemInput1.OpenDate = DateTime.Now;
            oGLRecItemInput1.CloseDate = DateTime.Now;
            oGLRecItemInput1.JournalEntryRef = 1001.ToString();
            oGLRecItemInput1.Comments = "Transaction Comments1";
            oGLRecItemInput1.CloseComments = "Resolution Comments1";
            oGLRecItemInput1.DateAdded = DateTime.Now;
            oGLRecItemInput1.AddedBy = "Test User";
            oGLRecItemInput1.ImportName = "Outstanding Checks";
            oGLRecItemInput1.Documents = "Document1";

            oGLRecItemInput1.AddedBy = "Tom Jones";
            oGLRecItemInput1.DateAdded = Convert.ToDateTime("2009-12-31 19:50:00.000");

            oGLRecItemInput1.BeginDate = Convert.ToDateTime("2008-12-31 19:50:00.000");
            oGLRecItemInput1.EndDate = Convert.ToDateTime("2009-12-31 19:50:00.000");
            oGLRecItemInput1.ScheduleName = "Prepaid Rent";
            oGLRecItemInput1.RecurringItemScheduleID = 1;
            oGLRecItemInput1.Balance = 12000;
            oGLRecItemInput1.CalculatedMonthlyAmount = 1000;


            GLDataRecItemInfo oGLRecItemInput2 = new GLDataRecItemInfo();
            oGLRecItemInput2.GLDataRecItemID = 2;
            oGLRecItemInput2.GLDataID = 10001;
            oGLRecItemInput2.ReconciliationCategoryID = 5;
            oGLRecItemInput2.ReconciliationCategoryTypeID = 7;
            oGLRecItemInput2.DataImportID = null;
            oGLRecItemInput2.IsAttachmentAvailable = false;
            oGLRecItemInput2.Amount = Convert.ToDecimal(375.00);
            oGLRecItemInput2.LocalCurrencyCode = "USD";
            oGLRecItemInput2.AmountBaseCurrency = 450;
            oGLRecItemInput2.AmountReportingCurrency = 375;
            oGLRecItemInput2.OpenDate = DateTime.Now;
            oGLRecItemInput2.OpenDate = DateTime.Now;
            oGLRecItemInput2.JournalEntryRef = 1002.ToString();
            oGLRecItemInput2.Comments = "Transaction Comments2";
            oGLRecItemInput2.CloseComments = "Resolution Comments2";
            oGLRecItemInput2.DateAdded = DateTime.Now;
            oGLRecItemInput2.AddedBy = "Test User1";
            oGLRecItemInput2.ImportName = "Outstanding Rent Receipts";
            oGLRecItemInput2.Documents = "Document2";

            oGLRecItemInput2.AddedBy = "Tom Jones";
            oGLRecItemInput2.DateAdded = Convert.ToDateTime("2009-11-19 19:50:00.000");

            oGLRecItemInput2.BeginDate = Convert.ToDateTime("2008-12-31 19:50:00.000");
            oGLRecItemInput2.EndDate = Convert.ToDateTime("2009-12-31 19:50:00.000");
            oGLRecItemInput2.ScheduleName = "Prepaid bills";
            oGLRecItemInput2.RecurringItemScheduleID = 2;
            oGLRecItemInput2.Balance = 12000;
            oGLRecItemInput2.CalculatedMonthlyAmount = 1000;

            oGLRecItemInputCollection.Add(oGLRecItemInput1);
            oGLRecItemInputCollection.Add(oGLRecItemInput2);
            return oGLRecItemInputCollection;
#else
            try
            {
                GLReconciliationItemInputDAO oGLRecItemInputDAO = new GLReconciliationItemInputDAO();
                return oGLRecItemInputDAO.SelectAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }

#endif


        }
        public List<GLDataRecItemInfo> GetRecItem(int RecPeriodID, AppUserInfo oAppUserInfo)
        {
            //throw new NotImplementedException();
#if DEMO
            List<GLDataRecItemInfo> oGLRecItemInputCollection = new List<GLDataRecItemInfo>();
            Random Rndm = new Random();
            for (int i = 0; i <= 4; i++)
            {
                GLDataRecItemInfo oGLRecItemInput1 = new GLDataRecItemInfo();
                oGLRecItemInput1.GLDataRecItemID = i + 1;
                oGLRecItemInput1.GLDataID = 10001;
                oGLRecItemInput1.ReconciliationCategoryID = 5;
                oGLRecItemInput1.ReconciliationCategoryTypeID = 7;
                oGLRecItemInput1.DataImportID = i + 1;
                oGLRecItemInput1.IsAttachmentAvailable = false;


                if (i == 0)
                {
                    oGLRecItemInput1.LocalCurrencyCode = "EUR";
                    oGLRecItemInput1.Amount = 12000;
                    oGLRecItemInput1.AmountBaseCurrency = 7000;
                    oGLRecItemInput1.AmountReportingCurrency = 8000;
                }
                if (i == 1)
                {
                    oGLRecItemInput1.LocalCurrencyCode = "USD";
                    oGLRecItemInput1.Amount = 12000;
                    oGLRecItemInput1.AmountBaseCurrency = 4000;
                    oGLRecItemInput1.AmountReportingCurrency = 5000;
                }
                if (i == 2)
                {
                    oGLRecItemInput1.LocalCurrencyCode = "INR";
                    oGLRecItemInput1.Amount = 12000;
                    oGLRecItemInput1.AmountBaseCurrency = 9000;
                    oGLRecItemInput1.AmountReportingCurrency = 10000;
                }
                if (i == 3)
                {
                    oGLRecItemInput1.LocalCurrencyCode = "EUR";
                    oGLRecItemInput1.Amount = 12000;
                    oGLRecItemInput1.AmountBaseCurrency = 3250;
                    oGLRecItemInput1.AmountReportingCurrency = 4250;
                }
                if (i == 4)
                {
                    oGLRecItemInput1.LocalCurrencyCode = "USD";
                    oGLRecItemInput1.Amount = 12000;
                    oGLRecItemInput1.AmountBaseCurrency = 6500;
                    oGLRecItemInput1.AmountReportingCurrency = 7500;
                }

                oGLRecItemInput1.OpenDate = DateTime.Now.Date;
                oGLRecItemInput1.CloseDate = DateTime.Now.Date;
                oGLRecItemInput1.JournalEntryRef = Rndm.Next(1, 9999).ToString();
                oGLRecItemInput1.Comments = "Transaction Comments" + i.ToString();
                oGLRecItemInput1.CloseComments = "Resolution Comments" + i.ToString();
                oGLRecItemInput1.DateAdded = DateTime.Now.Date;
                oGLRecItemInput1.AddedBy = "User" + i.ToString();
                oGLRecItemInput1.ImportName = "Outstanding Checks" + i.ToString();
                oGLRecItemInput1.Documents = "Document" + i.ToString();

                //Accural Schedule Properties.
                oGLRecItemInput1.Balance = 1200;
                oGLRecItemInput1.BeginDate = DateTime.Now;
                oGLRecItemInput1.EndDate = DateTime.Now;
                oGLRecItemInput1.RecurringItemScheduleID = i + 1;
                oGLRecItemInput1.ScheduleName = "Schedule" + (i + 1).ToString();
                oGLRecItemInput1.CalculatedMonthlyAmount = 1000;

                //

                oGLRecItemInputCollection.Add(oGLRecItemInput1);
            }
            return oGLRecItemInputCollection;
#else
            try
            {

                GLReconciliationItemInputDAO oGLRecItemInputDAO = new GLReconciliationItemInputDAO();
                return oGLRecItemInputDAO.SelectAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }

#endif


        }
        //        public List<GLDataRecItemInfo> GetRecInputItemByItemID(long GLRecInputItemID)
        //        {
        //            int i = GLRecInputItemID;
        //            Random Rndm = new Random();
        //            List<GLDataRecItemInfo> oGLRecItemInputCollection = new List<GLDataRecItemInfo>();
        //# if DEMO
        //            if (GLRecInputItemID > 0)
        //            {
        //                #region " In case of edit"
        //                GLDataRecItemInfo oGLRecItemInput1 = new GLDataRecItemInfo();
        //                oGLRecItemInput1.GLDataRecItemID = i;
        //                oGLRecItemInput1.GLDataID = 10001;
        //                oGLRecItemInput1.ReconciliationCategoryID = 5;
        //                oGLRecItemInput1.ReconciliationCategoryTypeID = 7;
        //                oGLRecItemInput1.DataImportID = i + 1;
        //                oGLRecItemInput1.IsAttachmentAvailable = false;


        //                if (i == 1)
        //                {
        //                    oGLRecItemInput1.Amount = 500;
        //                    oGLRecItemInput1.AmountBaseCurrency = 700;
        //                    oGLRecItemInput1.AmountReportingCurrency = 800;
        //                    oGLRecItemInput1.LocalCurrencyCode = "EUR";
        //                }
        //                if (i == 2)
        //                {
        //                    oGLRecItemInput1.Amount = 300;
        //                    oGLRecItemInput1.AmountBaseCurrency = 400;
        //                    oGLRecItemInput1.AmountReportingCurrency = 500;
        //                    oGLRecItemInput1.LocalCurrencyCode = "USD";
        //                }
        //                if (i == 3)
        //                {
        //                    oGLRecItemInput1.Amount = 800;
        //                    oGLRecItemInput1.AmountBaseCurrency = 900;
        //                    oGLRecItemInput1.AmountReportingCurrency = 1000;
        //                    oGLRecItemInput1.LocalCurrencyCode = "INR";
        //                }
        //                if (i == 4)
        //                {
        //                    oGLRecItemInput1.Amount = 225;
        //                    oGLRecItemInput1.AmountBaseCurrency = 325;
        //                    oGLRecItemInput1.AmountReportingCurrency = 425;
        //                    oGLRecItemInput1.LocalCurrencyCode = "EUR";
        //                }
        //                if (i == 5)
        //                {
        //                    oGLRecItemInput1.Amount = 550;
        //                    oGLRecItemInput1.AmountBaseCurrency = 650;
        //                    oGLRecItemInput1.AmountReportingCurrency = 750;
        //                    oGLRecItemInput1.LocalCurrencyCode = "USD";
        //                }

        //                oGLRecItemInput1.OpenDate = DateTime.Now.Date;
        //                oGLRecItemInput1.CloseDate = DateTime.Now.Date;
        //                oGLRecItemInput1.JournalEntryRef = Rndm.Next(1, 9999).ToString();
        //                oGLRecItemInput1.Comments = "Transaction Comments" + i.ToString();
        //                oGLRecItemInput1.CloseComments = "Resolution Comments" + i.ToString();
        //                oGLRecItemInput1.DateAdded = DateTime.Now.Date;
        //                oGLRecItemInput1.AddedBy = "User" + i.ToString();
        //                oGLRecItemInput1.ImportName = "Outstanding Checks" + i.ToString();
        //                oGLRecItemInput1.Documents = "Document" + i.ToString();

        //                //Accural Schedule Properties.
        //                oGLRecItemInput1.Balance = 1200;
        //                oGLRecItemInput1.BeginDate = DateTime.Now;
        //                oGLRecItemInput1.EndDate = DateTime.Now;
        //                oGLRecItemInput1.RecurringItemScheduleID = i + 1;
        //                oGLRecItemInput1.ScheduleName = "Schedule" + (i + 1).ToString();
        //                //

        //                oGLRecItemInputCollection.Add(oGLRecItemInput1);
        //                #endregion
        //            }
        //            else
        //            {
        //                #region "In case of New record"
        //                GLDataRecItemInfo oGLRecItemInput = new GLDataRecItemInfo();
        //                oGLRecItemInput.GLDataRecItemID = -1;
        //                oGLRecItemInputCollection.Add(oGLRecItemInput);
        //            }
        //            #endregion
        //#endif
        //            return oGLRecItemInputCollection;
        //        }

        //public void UpdateRecInputItem( string AddedBy, DateTime DateAdded, Decimal Amount
        //    , DateTime OpenDate, string Comments, int JournalEntryRef, DateTime ResolutionDate
        //    , string ResolutionComments, long GLReconciliationItemInputID)
        //{
        //}
        public void UpdateRecInputItem(GLDataRecItemInfo oGLRecItemInput, short recTemplateAttributeID, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecItemDAO oGLReconciliationItemInputDAO = new GLDataRecItemDAO(oAppUserInfo);
                oConnection = oGLReconciliationItemInputDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                oGLReconciliationItemInputDAO.UpdateRecItemInput(oGLRecItemInput, recTemplateAttributeID, oConnection, oTransaction);

                oTransaction.Commit();
            }
            catch (SqlException ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                {
                    oConnection.Close();
                    oConnection.Dispose();
                }
            }
        }

        public void InsertRecInputItem(GLDataRecItemInfo oGLRecItemInput, short recTemplateAttributeID, int recPeriodID, List<AttachmentInfo> oAttachmentInfoCollection, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecItemDAO oGLReconciliationItemInputDAO = new GLDataRecItemDAO(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                oConnection = oGLReconciliationItemInputDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                oGLReconciliationItemInputDAO.InsertRecInputItem(oGLRecItemInput, recTemplateAttributeID, oConnection, oTransaction);
                oReconciliationPeriodDAO.UpdateRecPeriodStatusAsInProgress(recPeriodID, oConnection, oTransaction);

                if (oAttachmentInfoCollection != null && oAttachmentInfoCollection.Count > 0)
                {
                    Array.ForEach(oAttachmentInfoCollection.ToArray(), a => a.RecordID = oGLRecItemInput.GLDataRecItemID);
                    DataTable dtAttachment = ServiceHelper.ConvertAttachmentInfoCollectionToDataTable(oAttachmentInfoCollection);
                    AttachmentDAO oAttachmentDAO = new AttachmentDAO(oAppUserInfo);
                    oAttachmentDAO.InsertAttachmentBulk(dtAttachment, recPeriodID,oGLRecItemInput.AddedBy, oGLRecItemInput.DateAdded, oConnection, oTransaction);
                }

                oTransaction.Commit();
            }
            catch (SqlException ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                {
                    oConnection.Close();
                    oConnection.Dispose();
                }
            }
        }

        public void DeleteRecInputItem(GLDataRecItemInfo oGLRecItemInput, short recTemplateAttributeID, DataTable dtGLDataParams, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecItemDAO oGLReconciliationItemInputDAO = new GLDataRecItemDAO(oAppUserInfo);
                oConnection = oGLReconciliationItemInputDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                oGLReconciliationItemInputDAO.DeleteRecItemInput(oGLRecItemInput, recTemplateAttributeID, dtGLDataParams, oConnection, oTransaction);

                oTransaction.Commit();
            }
            catch (SqlException ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                {
                    oConnection.Close();
                    oConnection.Dispose();
                }
            }
        }
        #endregion

        #region "RecurringItemScheduleReconciliationPeriod"
        //        public List<RecurringItemScheduleReconciliationPeriodInfo> GetItemScheduleDetail(int RecPeriodID, int ScheduleID)
        //        {
        //#if DEMO
        //            List<RecurringItemScheduleReconciliationPeriodInfo> oItemScheduleCollection = new List<RecurringItemScheduleReconciliationPeriodInfo>();
        //            Random oRand = new Random();
        //            for (int i = 1; i <= 5; i++)
        //            {
        //                RecurringItemScheduleReconciliationPeriodInfo oItemSchedule = new RecurringItemScheduleReconciliationPeriodInfo();
        //                oItemSchedule.RecurringItemScheduleReconciliationPeriodID = oRand.Next(1, 9999);
        //                oItemSchedule.AccurableItemScheduleID = ScheduleID;
        //                oItemSchedule.ReconciliationPeriodID = i;
        //                oItemSchedule.AccruedAmount = 1000 * i;
        //                oItemSchedule.ActualAmount = 1000;
        //                oItemSchedule.Date = DateTime.Now.AddMonths(i);
        //                oItemSchedule.ScheduleName = "Prepaid Rent";
        //                oItemScheduleCollection.Add(oItemSchedule);
        //                oItemSchedule = null;
        //            }
        //            return oItemScheduleCollection;
        //#else
        //            try
        //            {
        //                RecurringItemScheduleReconciliationPeriodDAO obj = new RecurringItemScheduleReconciliationPeriodDAO();
        //                return obj.SelectAll();
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //#endif
        //        }

        //        public List<RecurringItemScheduleReconciliationPeriodInfo> GetItemScheduleDetail(int RecPeriodID)
        //        {
        //#if DEMO
        //            List<RecurringItemScheduleReconciliationPeriodInfo> oItemScheduleCollection = new List<RecurringItemScheduleReconciliationPeriodInfo>();
        //            Random oRand = new Random();
        //            for (int i = 1; i <= 5; i++)
        //            {
        //                RecurringItemScheduleReconciliationPeriodInfo oItemSchedule = new RecurringItemScheduleReconciliationPeriodInfo();
        //                oItemSchedule.RecurringItemScheduleReconciliationPeriodID = oRand.Next(1, 9999);

        //                oItemSchedule.ReconciliationPeriodID = i;
        //                oItemSchedule.AccruedAmount = 1000 * i;
        //                oItemSchedule.ActualAmount = 1000;
        //                oItemSchedule.Date = DateTime.Now.AddMonths(i);
        //                oItemSchedule.ScheduleName = "Prepaid Rent";
        //                oItemScheduleCollection.Add(oItemSchedule);
        //                oItemSchedule = null;
        //            }
        //            return oItemScheduleCollection;
        //#else
        //            try
        //            {
        //                RecurringItemScheduleReconciliationPeriodDAO obj = new RecurringItemScheduleReconciliationPeriodDAO();
        //                return obj.SelectAll();
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //#endif
        //        }
        #endregion

        /// <summary>
        /// Gets All the Sum(Amount) order by ReconciliationCategoryTypeID, so can display sum on template forms for various CategoryTypes
        /// </summary>
        /// <param name="gLDataID"></param>
        /// <returns></returns>
        public List<GLDataRecItemInfo> GetTotalByReconciliationCategoryTypeID(long? gLDataID, AppUserInfo oAppUserInfo)
        {
            List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecItemDAO oGLReconciliationItemInputDAO = new GLDataRecItemDAO(oAppUserInfo);
                oGLReconciliationItemInputInfoCollection = oGLReconciliationItemInputDAO.GetTotalByReconciliationCategoryTypeID(gLDataID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oGLReconciliationItemInputInfoCollection;
        }

        public List<GLDataRecItemInfo> GetRecItem(long glDataID, int recPeriodID, short recCategoryTypeID, short glReconciliationItemInputRecordTypeID, short accountTemplateAttributeID, AppUserInfo oAppUserInfo)
        {
            List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecItemDAO oGLReconciliationItemInputDAO = new GLDataRecItemDAO(oAppUserInfo);
                oGLReconciliationItemInputInfoCollection = oGLReconciliationItemInputDAO.GetRecItem(glDataID, recPeriodID, recCategoryTypeID, glReconciliationItemInputRecordTypeID, accountTemplateAttributeID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oGLReconciliationItemInputInfoCollection;
        }

        public void UpdateGLRecItemCloseDate(long glDataID, List<long> glRecItemInputIDCollection, DateTime? closeDate, string closeComments, string journalEntryRef, string comments, short recCategoryTypeID, short recTemplateAttributeID, string revisedBy, DateTime? dateRevised, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable dtGLRecItemInputIDs = ServiceHelper.ConvertLongIDCollectionToDataTable(glRecItemInputIDCollection);
                GLDataRecItemDAO oGLReconciliationItemInputDAO = new GLDataRecItemDAO(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                oConnection = oGLReconciliationItemInputDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                oGLReconciliationItemInputDAO.UpdateGLRecItemCloseDate(glDataID, dtGLRecItemInputIDs, closeDate, closeComments, journalEntryRef, comments, recCategoryTypeID, recTemplateAttributeID, revisedBy, dateRevised, oConnection, oTransaction);
                oReconciliationPeriodDAO.UpdateRecPeriodStatusAsInProgress(recPeriodID, oConnection, oTransaction);

                oTransaction.Commit();
            }
            catch (SqlException ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                {
                    oConnection.Close();
                    oConnection.Dispose();
                }
            }
        }

        public List<ReconciliationCategoryMstInfo> GetAllReconciliationCategory(int? recPeriodID, long? AccountID, ref int? RecTemplateID, AppUserInfo oAppUserInfo)
        {
            List<ReconciliationCategoryMstInfo> oReconciliationCategoryMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationCategoryMstDAO oReconciliationCategoryMstDAO = new ReconciliationCategoryMstDAO(oAppUserInfo);
                oReconciliationCategoryMstInfoCollection = oReconciliationCategoryMstDAO.GetAllReconciliationCategory(recPeriodID, AccountID, ref RecTemplateID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oReconciliationCategoryMstInfoCollection;
        }
        public List<GLDataRecurringItemScheduleInfo> GetGLDataRecItemsListByMatchSetSubSetCombinationID(long? MatchSetSubSetCombinationID, AppUserInfo oAppUserInfo)
        {
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecurringItemScheduleDAO oGLDataRecurringItemScheduleDAO = new GLDataRecurringItemScheduleDAO(oAppUserInfo);
                oGLDataRecurringItemScheduleInfo = oGLDataRecurringItemScheduleDAO.GetGLDataRecItemsListByMatchSetSubSetCombinationID(MatchSetSubSetCombinationID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oGLDataRecurringItemScheduleInfo;
        }

        public List<GLDataRecItemInfo> GetClosedGLDataRecItem(long? GlDataID, long? MatchsetSubSetCombinationID, long? ExCelRowNumber, long? MatchSetMatchingSourceDataImportID, short? GLDataRecItemTypeID, AppUserInfo oAppUserInfo)
        {
            List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecItemDAO oGLReconciliationItemInputDAO = new GLDataRecItemDAO(oAppUserInfo);
                oGLReconciliationItemInputInfoCollection = oGLReconciliationItemInputDAO.GetClosedGLDataRecItem(GlDataID, MatchsetSubSetCombinationID, ExCelRowNumber, MatchSetMatchingSourceDataImportID, GLDataRecItemTypeID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oGLReconciliationItemInputInfoCollection;
        }
        public short? GetGLRecItemTypeID(long? MatchsetSubSetCombinationID, long? ExCelRowNumber, long? MatchSetMatchingSourceDataImportID, AppUserInfo oAppUserInfo)
        {
            short? GLRecItemTypeID = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecItemDAO oGLDataRecItemDAO = new GLDataRecItemDAO(oAppUserInfo);
                GLRecItemTypeID = oGLDataRecItemDAO.GetGLRecItemTypeID(MatchsetSubSetCombinationID, ExCelRowNumber, MatchSetMatchingSourceDataImportID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return GLRecItemTypeID;
        }

        public List<RecItemCommentInfo> GetRecItemCommentList(long? RecordID, short? RecordTypeID, AppUserInfo oAppUserInfo)
        {
            List<RecItemCommentInfo> oRecItemCommentInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RecItemCommentDAO oRecItemCommentDAO = new RecItemCommentDAO(oAppUserInfo);
                oRecItemCommentInfoCollection = oRecItemCommentDAO.GetRecItemCommentList(RecordID, RecordTypeID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oRecItemCommentInfoCollection;
        }
        public void SaveRecItemComment(RecItemCommentInfo oRecItemCommentInfo, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RecItemCommentDAO oRecItemCommentDAO = new RecItemCommentDAO(oAppUserInfo);
                oConnection = oRecItemCommentDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                oRecItemCommentDAO.SaveRecItemComment(oRecItemCommentInfo, oConnection, oTransaction);
                oTransaction.Commit();
            }
            catch (SqlException ex)
            {
                if (oTransaction != null)
                {
                    oTransaction.Rollback();
                }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oTransaction != null)
                {
                    oTransaction.Rollback();
                }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                {
                    oTransaction.Dispose();
                    oConnection.Close();
                    oConnection.Dispose();
                }

            }
        }
        public GLDataRecItemInfo GetGLDataRecItem(long? RecordID, short? RecordTypeID, AppUserInfo oAppUserInfo)
        {
            GLDataRecItemInfo oRecItemCommentInfo = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecItemDAO oGLDataRecItemDAO = new GLDataRecItemDAO(oAppUserInfo);
                oRecItemCommentInfo = oGLDataRecItemDAO.GetGLDataRecItemInfo(RecordID, RecordTypeID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oRecItemCommentInfo;
        }
        public List<AttachmentInfo> CopyRecInputItem(GLDataRecItemInfo oGLRecItemInput, DataTable dtGLDataParams, int? recPeriodID, bool CloseSourceRecItem, AppUserInfo oAppUserInfo)
        {
            List<AttachmentInfo> oAttachmentInfoList = null;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecItemDAO oGLReconciliationItemInputDAO = new GLDataRecItemDAO(oAppUserInfo);
                oConnection = oGLReconciliationItemInputDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                oAttachmentInfoList = oGLReconciliationItemInputDAO.CopyRecInputItem(oGLRecItemInput, dtGLDataParams, recPeriodID, CloseSourceRecItem, oConnection, oTransaction);
                oTransaction.Commit();
            }
            catch (SqlException ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                {
                    oConnection.Close();
                    oConnection.Dispose();
                }
            }

            return oAttachmentInfoList;
        }








    }//End of class
}//end of namespace
