


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class GridMstDAO : GridMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GridMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
    }
}