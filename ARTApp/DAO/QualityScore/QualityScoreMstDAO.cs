

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.App.DAO.QualityScore.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.QualityScore
{
    public class QualityScoreMstDAO : QualityScoreMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public QualityScoreMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
    }
}