using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Adapdev.Data;

namespace SkyStem.ART.App.DAO.Base
{
    public abstract class CustomAbstractDAO<T> : Adapdev.Data.AbstractDAO<T>
    {
        public CustomAbstractDAO(DbProviderType providerType, Adapdev.Data.DbType databaseType, string tableName, string connectionString)
            : base(providerType, databaseType, tableName, connectionString)
        {

        }
        public new IDbCommand CreateCommand(string commandName)
        {
            IDbCommand oCmd;
            oCmd = base.CreateCommand(commandName);
            if (DbConstants.CommandTimeOut.HasValue)
                oCmd.CommandTimeout = DbConstants.CommandTimeOut.Value;
            return oCmd;
        }

        protected abstract override IDbCommand CreateDeleteOneCommand(object id);


        protected abstract override IDbCommand CreateInsertCommand(T o);


        protected abstract override IDbCommand CreateSelectOneCommand(object id);


        protected abstract override IDbCommand CreateUpdateCommand(T o);


        protected abstract override T MapObject(IDataReader dr);

    }
}
