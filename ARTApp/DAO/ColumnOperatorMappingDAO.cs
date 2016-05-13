


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
    public class ColumnOperatorMappingDAO : ColumnOperatorMappingDAOBase
    {


        /// <summary>
        /// Constructor
        /// </summary>
        public ColumnOperatorMappingDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }



    }
}