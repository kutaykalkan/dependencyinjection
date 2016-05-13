

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Base
{

    public abstract class UtilityDAOBase : CustomAbstractDAO<DummyInfo>
    {
        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public UtilityDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "Dummy", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
        }

        protected override IDbCommand CreateDeleteOneCommand(object id)
        {
            throw new NotImplementedException();
        }

        protected override IDbCommand CreateInsertCommand(DummyInfo o)
        {
            throw new NotImplementedException();
        }

        protected override IDbCommand CreateSelectOneCommand(object id)
        {
            throw new NotImplementedException();
        }

        protected override IDbCommand CreateUpdateCommand(DummyInfo o)
        {
            throw new NotImplementedException();
        }

        protected override DummyInfo MapObject(IDataReader dr)
        {
            throw new NotImplementedException();
        }
    }
}
