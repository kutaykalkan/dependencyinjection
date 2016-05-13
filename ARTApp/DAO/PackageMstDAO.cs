using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;

namespace SkyStem.ART.App.DAO
{
    public class PackageMstDAO : PackageMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PackageMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public PackageMstInfo GetComapanyPackageInfo(int companyId)
        {
            IDbCommand oDbCommand = this.CreateGetComapanyPackageInfoCommand(companyId);
            oDbCommand.Connection = this.CreateConnection();
            PackageMstInfo oPackageMstInfo = null;
            return oPackageMstInfo;
        }

        private IDbCommand CreateGetComapanyPackageInfoCommand(int companyId)
        {
            IDbCommand oDbCommand = this.CreateCommand("");
            oDbCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oDbCommand.Parameters;
            IDataParameter cmdParamCompanyId = oDbCommand.CreateParameter();
            cmdParamCompanyId.ParameterName = "@CompanyId";
            cmdParamCompanyId.DbType = System.Data.DbType.Int32;
            cmdParamCompanyId.Value = companyId;
            cmdParams.Add(cmdParamCompanyId);
            return oDbCommand;
        }

        /// <summary>
        /// GetAllPackage() is used to get all packages from db.
        /// </summary>
        /// <returns></returns>
        public List<PackageMstInfo> GetAllPackage()
        {
            List<PackageMstInfo> oPackageMstInfoCollection = null;

            try
            {
                IDbCommand cmd = this.CreateCommand("usp_SEL_AllPackageMst");
                cmd.CommandType = CommandType.StoredProcedure;
                oPackageMstInfoCollection = this.Select(cmd);
                return oPackageMstInfoCollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string GetFeaturesPackageAvailabilityMatrix()
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_FeaturesPackageAvailabilityMatrix");
            cmd.Connection = CreateConnection();
            if (cmd.Connection != null && cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            IDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds.GetXml();
        }


        //public string GetFeaturesPackageAvailabilityMatrix()
        //{
        //    IDbCommand cmd = this.CreateCommand("usp_GET_FeaturesPackageAvailabilityMatrix");
        //    cmd.Connection = CreateConnection();
        //    if (cmd.Connection != null && cmd.Connection.State != ConnectionState.Open)
        //        cmd.Connection.Open();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    IDataReader dr = cmd.ExecuteReader();
        //    string strXml = string.Empty;
        //    while (dr.Read())
        //    {
        //        strXml += dr[0].ToString();
        //    }

        //    return "<NewDataSet>" + strXml + "</NewDataSet>";
        //}

        protected DataSet DataReaderToDataSet(IDataReader reader)
        {
            DataTable table = new DataTable();
            int fieldCount = reader.FieldCount;
            for (int i = 0; i < fieldCount; i++)
            {
                table.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
            }
            table.BeginLoadData();
            object[] values = new object[fieldCount];
            while (reader.Read())
            {
                reader.GetValues(values);
                table.LoadDataRow(values, true);
            }
            table.EndLoadData();
            DataSet ds = new DataSet();
            ds.Tables.Add(table);
            return ds;
        }


        /// <summary>
        /// InsertPackageFeatureReportRole() is used to Insert CompanyFeature + CompanyRole + CompanyReport.
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="packageID"></param>
        /// <param name="connection"></param>
        /// <param name="oTransaction"></param>
        public void InsertPackageFeatureReportRole(Int32? companyID, Int16? packageID, string addedBy, DateTime? dateAdded, string revisedBy, DateTime? dateRevised)
        {
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateInsertFeatureReportRoleCommand(companyID, packageID, addedBy, dateAdded, revisedBy, dateRevised))
                {
                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePackageFeatureReportRole(Int32? companyID, Int16? packageID, IDbConnection connection, IDbTransaction oTransaction)
        {
            try
            {
                IDbCommand cmd = CreateUpdateFeatureReportRoleCommand(companyID, packageID);
                cmd.Connection = connection;
                cmd.Transaction = oTransaction;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// create insert command fro CompanyFeature + CompanyRole + CompanyReport.
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="packageID"></param>
        /// <returns></returns>
        protected System.Data.IDbCommand CreateInsertFeatureReportRoleCommand(Int32? companyID, Int16? packageID, string addedBy, DateTime? dateAdded, string revisedBy, DateTime? dateRevised)
        {
            IDbCommand cmd = this.CreateCommand("usp_SAV_CompanyFeature");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parCompanyId = cmd.CreateParameter();
            parCompanyId.ParameterName = "@CompanyID";
            if (companyID != null)
                parCompanyId.Value = companyID;
            else
                parCompanyId.Value = System.DBNull.Value;

            cmdParams.Add(parCompanyId);

            System.Data.IDbDataParameter parPackageId = cmd.CreateParameter();
            parPackageId.ParameterName = "@PackageID";
            if (packageID != null)
                parPackageId.Value = packageID;
            else
                parPackageId.Value = System.DBNull.Value;

            cmdParams.Add(parPackageId);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            parAddedBy.Value = addedBy;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            parDateAdded.Value = dateAdded.Value;
            cmdParams.Add(parDateAdded);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            parRevisedBy.Value = revisedBy;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            parDateRevised.Value = dateRevised.Value;
            cmdParams.Add(parDateRevised);

            return cmd;
        }

        protected System.Data.IDbCommand CreateUpdateFeatureReportRoleCommand(Int32? companyID, Int16? packageID)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_PackageFeature_Report_Role");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parCompanyId = cmd.CreateParameter();
            parCompanyId.ParameterName = "@CompanyID";
            parCompanyId.Value = companyID;

            cmdParams.Add(parCompanyId);

            System.Data.IDbDataParameter parPackageId = cmd.CreateParameter();
            parPackageId.ParameterName = "@PackageID";

            parPackageId.Value = packageID;

            cmdParams.Add(parPackageId);

            return cmd;
        }
    }
}