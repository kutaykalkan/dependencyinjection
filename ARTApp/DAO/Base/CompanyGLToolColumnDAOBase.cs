

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

	public abstract class CompanyGLToolColumnDAOBase : CustomAbstractDAO<CompanyGLToolColumnInfo> 
	{
        
		/// <summary>
		/// A static representation of column CompanyGLToolColumnID
		/// </summary>
		public static readonly string COLUMN_COMPANYGLTOOLCOLUMNID = "CompanyGLToolColumnID";
		/// <summary>
		/// A static representation of column CompanyRecPeriodSetID
		/// </summary>
		public static readonly string COLUMN_COMPANYRECPERIODSETID = "CompanyRecPeriodSetID";
		/// <summary>
		/// A static representation of column GLToolColumnLength
		/// </summary>
		public static readonly string COLUMN_GLTOOLCOLUMNLENGTH = "GLToolColumnLength";
		/// <summary>
		/// A static representation of column GLToolColumnName
		/// </summary>
		public static readonly string COLUMN_GLTOOLCOLUMNNAME = "GLToolColumnName";
		/// <summary>
		/// Provides access to the name of the primary key column (CompanyGLToolColumnID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "CompanyGLToolColumnID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "CompanyGLToolColumn";

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
        public CompanyGLToolColumnDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CompanyGLToolColumn", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
	        }
        
	        /// <summary>
	        /// Maps the IDataReader values to a CompanyGLToolColumnInfo object
	        /// </summary>
	        /// <param name="r">The IDataReader to map</param>
	        /// <returns>CompanyGLToolColumnInfo</returns>
	        protected override CompanyGLToolColumnInfo MapObject(System.Data.IDataReader r) {

	            CompanyGLToolColumnInfo entity = new CompanyGLToolColumnInfo();
	            
	
				try{
					int ordinal = r.GetOrdinal("CompanyGLToolColumnID");
					if (!r.IsDBNull(ordinal)) entity.CompanyGLToolColumnID = ((System.Int32)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("CompanyRecPeriodSetID");
					if (!r.IsDBNull(ordinal)) entity.CompanyRecPeriodSetID = ((System.Int32)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("GLToolColumnName");
					if (!r.IsDBNull(ordinal)) entity.GLToolColumnName = ((System.String)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("GLToolColumnLength");
					if (!r.IsDBNull(ordinal)) entity.GLToolColumnLength = ((System.Int16)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
	            return entity;
	        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in CompanyGLToolColumnInfo object
		/// </summary>
		/// <param name="o">A CompanyGLToolColumnInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateInsertCommand(CompanyGLToolColumnInfo entity) {
	
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CompanyGLToolColumn");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;

				
				System.Data.IDbDataParameter parCompanyRecPeriodSetID = cmd.CreateParameter();
				parCompanyRecPeriodSetID.ParameterName = "@CompanyRecPeriodSetID";
				if(entity != null)
					parCompanyRecPeriodSetID.Value = entity.CompanyRecPeriodSetID;
				else
					parCompanyRecPeriodSetID.Value = System.DBNull.Value;
				cmdParams.Add(parCompanyRecPeriodSetID);
			
				System.Data.IDbDataParameter parGLToolColumnLength = cmd.CreateParameter();
				parGLToolColumnLength.ParameterName = "@GLToolColumnLength";
                if (entity != null)
					parGLToolColumnLength.Value = entity.GLToolColumnLength;
				else
					parGLToolColumnLength.Value = System.DBNull.Value;
				cmdParams.Add(parGLToolColumnLength);
			
				System.Data.IDbDataParameter parGLToolColumnName = cmd.CreateParameter();
				parGLToolColumnName.ParameterName = "@GLToolColumnName";
                if (entity != null)
					parGLToolColumnName.Value = entity.GLToolColumnName;
				else
					parGLToolColumnName.Value = System.DBNull.Value;
				cmdParams.Add(parGLToolColumnName);
					
				return cmd;
		
	        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in CompanyGLToolColumnInfo object
		/// </summary>
		/// <param name="o">A CompanyGLToolColumnInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateUpdateCommand(CompanyGLToolColumnInfo entity) {
	
          
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CompanyGLToolColumn");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				
				System.Data.IDbDataParameter parCompanyRecPeriodSetID = cmd.CreateParameter();
				parCompanyRecPeriodSetID.ParameterName = "@CompanyRecPeriodSetID";
                if (entity != null)
					parCompanyRecPeriodSetID.Value = entity.CompanyRecPeriodSetID;
				else
					parCompanyRecPeriodSetID.Value = System.DBNull.Value;
				cmdParams.Add(parCompanyRecPeriodSetID);
			
				System.Data.IDbDataParameter parGLToolColumnLength = cmd.CreateParameter();
				parGLToolColumnLength.ParameterName = "@GLToolColumnLength";
                if (entity != null)
					parGLToolColumnLength.Value = entity.GLToolColumnLength;
				else
					parGLToolColumnLength.Value = System.DBNull.Value;
				cmdParams.Add(parGLToolColumnLength);
			
				System.Data.IDbDataParameter parGLToolColumnName = cmd.CreateParameter();
				parGLToolColumnName.ParameterName = "@GLToolColumnName";
                if (entity != null)
					parGLToolColumnName.Value = entity.GLToolColumnName;
				else
					parGLToolColumnName.Value = System.DBNull.Value;
				cmdParams.Add(parGLToolColumnName);
		
				System.Data.IDbDataParameter pkparCompanyGLToolColumnID = cmd.CreateParameter();
				pkparCompanyGLToolColumnID.ParameterName = "@CompanyGLToolColumnID";
				pkparCompanyGLToolColumnID.Value = entity.CompanyGLToolColumnID;
				cmdParams.Add(pkparCompanyGLToolColumnID);
	
            
				return cmd;
		
	        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) {
	
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CompanyGLToolColumn");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@CompanyGLToolColumnID";
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
		
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CompanyGLToolColumn");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@CompanyGLToolColumnID";
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
		public IList<CompanyGLToolColumnInfo> SelectAllByCompanyRecPeriodSetID(object id)
		{
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyGLToolColumnByCompanyRecPeriodSetID");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@CompanyRecPeriodSetID";
				par.Value = id;
				cmdParams.Add(par);
            
	            return this.Select(cmd);
		}

				
	
															
		
	
		
		protected override void CustomSave(CompanyGLToolColumnInfo o, IDbConnection connection){
						
			string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyGLToolColumnDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);

						
		}
		
		protected override void CustomSave(CompanyGLToolColumnInfo o, IDbConnection connection, IDbTransaction transaction){
						
			string query = QueryHelper.GetSqlServerLastInsertedCommand(CompanyGLToolColumnDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
			
						
		}		
		
		private void MapIdentity(CompanyGLToolColumnInfo entity, object id){
			entity.CompanyGLToolColumnID = Convert.ToInt32(id);
		}
		
			
		
	
                }
}
