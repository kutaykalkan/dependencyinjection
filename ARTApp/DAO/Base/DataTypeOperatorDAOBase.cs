

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO.Base 
{

	public abstract class DataTypeOperatorDAOBase : CustomAbstractDAO<DataTypeOperatorInfo> 
	{
        
		/// <summary>
		/// A static representation of column DataTypeID
		/// </summary>
		public static readonly string COLUMN_DATATYPEID = "DataTypeID";
		/// <summary>
		/// A static representation of column DataTypeOperatorID
		/// </summary>
		public static readonly string COLUMN_DATATYPEOPERATORID = "DataTypeOperatorID";
		/// <summary>
		/// A static representation of column OperatorID
		/// </summary>
		public static readonly string COLUMN_OPERATORID = "OperatorID";
		/// <summary>
		/// Provides access to the name of the primary key column (DataTypeOperatorID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "DataTypeOperatorID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "DataTypeOperator";

		/// <summary>
		/// Provides access to the name of the database
		/// </summary>
		public static readonly string DATABASE_NAME = "SkyStemART";

        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }
		
		/// <summary>
		/// Constructor
		/// </summary>
        public DataTypeOperatorDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "DataTypeOperator", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
	        }
        
	        /// <summary>
	        /// Maps the IDataReader values to a DataTypeOperatorInfo object
	        /// </summary>
	        /// <param name="r">The IDataReader to map</param>
	        /// <returns>DataTypeOperatorInfo</returns>
	        protected override DataTypeOperatorInfo MapObject(System.Data.IDataReader r) {

	            DataTypeOperatorInfo entity = new DataTypeOperatorInfo();
	            
																						entity.DataTypeOperatorID = r.GetInt16Value("DataTypeOperatorID");
																																						entity.DataTypeID = r.GetInt16Value("DataTypeID");
																																						entity.OperatorID = r.GetInt16Value("OperatorID");
																	
	            return entity;
	        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in DataTypeOperatorInfo object
		/// </summary>
		/// <param name="o">A DataTypeOperatorInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateInsertCommand(DataTypeOperatorInfo entity) {
	
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_DataTypeOperator");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;

		
				System.Data.IDbDataParameter parDataTypeID = cmd.CreateParameter();
				parDataTypeID.ParameterName = "@DataTypeID";
				if(entity != null)
					parDataTypeID.Value = entity.DataTypeID;
				else
					parDataTypeID.Value = System.DBNull.Value;
				cmdParams.Add(parDataTypeID);
					
				System.Data.IDbDataParameter parOperatorID = cmd.CreateParameter();
				parOperatorID.ParameterName = "@OperatorID";
				if(entity != null)
					parOperatorID.Value = entity.OperatorID;
				else
					parOperatorID.Value = System.DBNull.Value;
				cmdParams.Add(parOperatorID);
					
				return cmd;
		
	        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in DataTypeOperatorInfo object
		/// </summary>
		/// <param name="o">A DataTypeOperatorInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateUpdateCommand(DataTypeOperatorInfo entity) {
	
          
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_DataTypeOperator");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
		
				System.Data.IDbDataParameter parDataTypeID = cmd.CreateParameter();
				parDataTypeID.ParameterName = "@DataTypeID";
				if(entity != null)
					parDataTypeID.Value = entity.DataTypeID;
				else
					parDataTypeID.Value = System.DBNull.Value;
				cmdParams.Add(parDataTypeID);
					
				System.Data.IDbDataParameter parOperatorID = cmd.CreateParameter();
				parOperatorID.ParameterName = "@OperatorID";
				if(entity != null)
					parOperatorID.Value = entity.OperatorID;
				else
					parOperatorID.Value = System.DBNull.Value;
				cmdParams.Add(parOperatorID);
		
				System.Data.IDbDataParameter pkparDataTypeOperatorID = cmd.CreateParameter();
				pkparDataTypeOperatorID.ParameterName = "@DataTypeOperatorID";
				pkparDataTypeOperatorID.Value = entity.DataTypeOperatorID;
				cmdParams.Add(pkparDataTypeOperatorID);
	
            
				return cmd;
		
	        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) {
	
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_DataTypeOperator");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@DataTypeOperatorID";
				par.Value = id;
				cmdParams.Add(par);
	            
	            return cmd;
		
	        }
	
       
		/// <summary>
		/// Creates the sql select command, using the passed in primary key
		/// </summary>
		/// <param name="o">The primary key of the object to select</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateSelectOneCommand(object id) {
		
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_DataTypeOperator");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@DataTypeOperatorID";
				par.Value = id;
				cmdParams.Add(par);
            
	            return cmd;
		
	        }

			
		/// <summary>
		/// Creates the sql select command, using the passed in foreign key.  This will return an
		/// IList of all objects that have that foreign key.
		/// </summary>
		/// <param name="o">The foreign key of the objects to select</param>
		/// <returns>An IList</returns>
		public IList<DataTypeOperatorInfo> SelectAllByDataTypeID(object id)
		{
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_DataTypeOperatorByDataTypeID");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@DataTypeID";
				par.Value = id;
				cmdParams.Add(par);
            
	            return this.Select(cmd);
		}

					
		/// <summary>
		/// Creates the sql select command, using the passed in foreign key.  This will return an
		/// IList of all objects that have that foreign key.
		/// </summary>
		/// <param name="o">The foreign key of the objects to select</param>
		/// <returns>An IList</returns>
		public IList<DataTypeOperatorInfo> SelectAllByOperatorID(object id)
		{
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_DataTypeOperatorByOperatorID");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@OperatorID";
				par.Value = id;
				cmdParams.Add(par);
            
	            return this.Select(cmd);
		}

				
	
												
		
	
		
		protected override void CustomSave(DataTypeOperatorInfo o, IDbConnection connection){
						
			string query = QueryHelper.GetSqlServerLastInsertedCommand(DataTypeOperatorDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);

						
		}
		
		protected override void CustomSave(DataTypeOperatorInfo o, IDbConnection connection, IDbTransaction transaction){
						
			string query = QueryHelper.GetSqlServerLastInsertedCommand(DataTypeOperatorDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
			
						
		}		
		
		private void MapIdentity(DataTypeOperatorInfo entity, object id){
			entity.DataTypeOperatorID = Convert.ToInt16(id);
		}
		
			
		
	
             }
}
