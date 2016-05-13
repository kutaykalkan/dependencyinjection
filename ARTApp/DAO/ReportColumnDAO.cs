


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Params;

namespace SkyStem.ART.App.DAO
{
    public class ReportColumnDAO : ReportColumnDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReportColumnDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public List<ReportColumnInfo> SelectAllReportColumnsByReportID(ReportParamInfo oReportParamInfo)
        {
            List<ReportColumnInfo> oReportColumnInfoList = new List<ReportColumnInfo>(); 
            using (IDbConnection cnn = this.CreateConnection())
            {
                using (IDbCommand cmd = this.CreateCommandSelectAllReportColumnsByReportID(oReportParamInfo))
                {
                    cnn.Open();
                    cmd.Connection = cnn;
                    IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {
                        oReportColumnInfoList.Add(MapObject(dr));
                    }
                    dr.ClearColumnHash();
                }
            }
            return oReportColumnInfoList;
        }

        private IDbCommand CreateCommandSelectAllReportColumnsByReportID(ReportParamInfo oReportParamInfo)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_ReportColumns");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (oReportParamInfo.CompanyID.HasValue)
                parCompanyID.Value = oReportParamInfo.CompanyID.Value;
            else
                parCompanyID.Value = DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parReportID = cmd.CreateParameter();
            parReportID.ParameterName = "@ReportID";
            if (oReportParamInfo.ReportID.HasValue)
                parReportID.Value = oReportParamInfo.ReportID.Value;
            else
                parReportID.Value = DBNull.Value;
            cmdParams.Add(parReportID);

            System.Data.IDbDataParameter parIsOptional = cmd.CreateParameter();
            parIsOptional.ParameterName = "@IsOptional";
            if (oReportParamInfo.IsOptional.HasValue)
                parIsOptional.Value = oReportParamInfo.IsOptional.Value;
            else
                parIsOptional.Value = DBNull.Value;
            cmdParams.Add(parIsOptional);

            return cmd;
        }

        protected override ReportColumnInfo MapObject(IDataReader r)
        {
            ReportColumnInfo oReportColumnInfo = base.MapObject(r);
            oReportColumnInfo.ColumnUniqueName = r.GetStringValue("ColumnUniqueName");
            oReportColumnInfo.FeatureID = r.GetInt16Value("FeatureID");
            oReportColumnInfo.CapabilityID = r.GetInt16Value("CapabilityID");
            oReportColumnInfo.ColumnTypeID = r.GetInt16Value("ColumnTypeID");
            return oReportColumnInfo;
        }
    }
}