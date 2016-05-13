using System;
using System.Data;
using Adapdev.Data;
    
namespace SkyStem.ART {
    
    public class ConnectionFactory {
        
        public static IDbConnection CreateConnection(){
			IDbConnection connection = Adapdev.Data.DbProviderFactory.CreateConnection(DbConstants.DatabaseProviderType);
			connection.ConnectionString = DbConstants.ConnectionString;
			return connection;        
        }
   }
}
