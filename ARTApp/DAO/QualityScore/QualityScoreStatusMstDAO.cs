

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.App.DAO.QualityScore.Base;
using SkyStem.ART.Client.Model.QualityScore;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.QualityScore
{
    public class QualityScoreStatusMstDAO : QualityScoreStatusMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public QualityScoreStatusMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        /// <summary>
        /// Gets the quality score status MST info list.
        /// </summary>
        /// <returns></returns>
        public List<QualityScoreStatusMstInfo> GetAllQualityScoreStatuses()
        {
            IDbCommand cmd = null;
            IDbConnection cnn = null;
            IDataReader dr = null;
            List<QualityScoreStatusMstInfo> oQualityScoreStatusMstInfoList = null;
            try
            {
                cmd = CreateSelectCommandQualityScoreStatusMstInfoList();
                cnn = this.CreateConnection();
                cnn.Open();
                cmd.Connection = cnn;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oQualityScoreStatusMstInfoList = new List<QualityScoreStatusMstInfo>();
                QualityScoreStatusMstInfo oQualityScoreStatusMstInfo;
                while (dr.Read())
                {
                    oQualityScoreStatusMstInfo = this.MapObject(dr);
                    oQualityScoreStatusMstInfoList.Add(oQualityScoreStatusMstInfo);
                }
            }
            finally
            {
                if (dr != null)
                    dr.ClearColumnHash();
                if (cmd != null)
                    cmd.Dispose();
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return oQualityScoreStatusMstInfoList;
        }

        /// <summary>
        /// Creates the select command quality score status MST info list.
        /// </summary>
        /// <returns></returns>
        private IDbCommand CreateSelectCommandQualityScoreStatusMstInfoList()
        {
            IDbCommand cmd = this.CreateCommand("[QualityScore].[usp_SEL_QualityScoreStatus]");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }
    }
}