


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Matching.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Matching
{
    public class MatchingConfigurationRecItemColumnDAO : MatchingConfigurationRecItemColumnDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MatchingConfigurationRecItemColumnDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
    }
}