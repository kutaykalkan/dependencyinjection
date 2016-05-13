using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.App.DAO.Matching.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Matching
{
    public class MatchingStatusMstDAO : MatchingStatusMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MatchingStatusMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
    }
}