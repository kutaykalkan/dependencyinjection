

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

	public abstract class ColumnMstDAOBase : CustomAbstractDAO<ColumnMstInfo> 
	{
        
		/// <summary>
		/// A static representation of column AddedBy
		/// </summary>
		public static readonly string COLUMN_ADDEDBY = "AddedBy";
		/// <summary>
		/// A static representation of column ColumnDescription
		/// </summary>
		public static readonly string COLUMN_COLUMNDESCRIPTION = "ColumnDescription";
		/// <summary>
		/// A static representation of column ColumnID
		/// </summary>
		public static readonly string COLUMN_COLUMNID = "ColumnID";
		/// <summary>
		/// A static representation of column ColumnName
		/// </summary>
		public static readonly string COLUMN_COLUMNNAME = "ColumnName";
        /// <summary>
        /// A static representation of column ColumnName
        /// </summary>
        public static readonly string COLUMN_COLUMNUNIQUENAME = "ColumnUniqueName";
        /// <summary>
		/// A static representation of column ColumnNameLabelID
		/// </summary>
		public static readonly string COLUMN_COLUMNNAMELABELID = "ColumnNameLabelID";
		/// <summary>
		/// A static representation of column DateAdded
		/// </summary>
		public static readonly string COLUMN_DATEADDED = "DateAdded";
		/// <summary>
		/// A static representation of column DateRevised
		/// </summary>
		public static readonly string COLUMN_DATEREVISED = "DateRevised";
		/// <summary>
		/// A static representation of column HostName
		/// </summary>
		public static readonly string COLUMN_HOSTNAME = "HostName";
		/// <summary>
		/// A static representation of column IsActive
		/// </summary>
		public static readonly string COLUMN_ISACTIVE = "IsActive";
		/// <summary>
		/// A static representation of column RevisedBy
		/// </summary>
		public static readonly string COLUMN_REVISEDBY = "RevisedBy";
		/// <summary>
		/// Provides access to the name of the primary key column (ColumnID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "ColumnID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "ColumnMst";

		/// <summary>
		/// Provides access to the name of the database
		/// </summary>
		public static readonly string DATABASE_NAME = "SkyStemArt";

        /// <summary>
        ///  CurrentAppUserInfo  for further use
        /// </summary>
        public AppUserInfo CurrentAppUserInfo { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
        public ColumnMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "ColumnMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
	        }
        
	        /// <summary>
	        /// Maps the IDataReader values to a ColumnMstInfo object
	        /// </summary>
	        /// <param name="r">The IDataReader to map</param>
	        /// <returns>ColumnMstInfo</returns>
	        protected override ColumnMstInfo MapObject(System.Data.IDataReader r) {

	            ColumnMstInfo entity = new ColumnMstInfo();
	            
	
				try{
					int ordinal = r.GetOrdinal("ColumnID");
					if (!r.IsDBNull(ordinal)) entity.ColumnID = ((System.Int16)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("ColumnUniqueName");
					if (!r.IsDBNull(ordinal)) entity.ColumnUniqueName = ((System.String)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("ColumnNameLabelID");
					if (!r.IsDBNull(ordinal)) entity.ColumnNameLabelID = ((System.Int32)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("ColumnDescription");
					if (!r.IsDBNull(ordinal)) entity.ColumnDescription = ((System.String)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("IsActive");
					if (!r.IsDBNull(ordinal)) entity.IsActive = ((System.Boolean)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("DateAdded");
					if (!r.IsDBNull(ordinal)) entity.DateAdded = ((System.DateTime)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("AddedBy");
					if (!r.IsDBNull(ordinal)) entity.AddedBy = ((System.String)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("DateRevised");
					if (!r.IsDBNull(ordinal)) entity.DateRevised = ((System.DateTime)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("RevisedBy");
					if (!r.IsDBNull(ordinal)) entity.RevisedBy = ((System.String)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("HostName");
					if (!r.IsDBNull(ordinal)) entity.HostName = ((System.String)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
	            return entity;
	        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in ColumnMstInfo object
		/// </summary>
		/// <param name="o">A ColumnMstInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateInsertCommand(ColumnMstInfo entity) {
	
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_ColumnMst");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;

		
				System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
				parAddedBy.ParameterName = "@AddedBy";
				if(!entity.IsAddedByNull)
					parAddedBy.Value = entity.AddedBy;
				else
					parAddedBy.Value = System.DBNull.Value;
				cmdParams.Add(parAddedBy);
			
				System.Data.IDbDataParameter parColumnDescription = cmd.CreateParameter();
				parColumnDescription.ParameterName = "@ColumnDescription";
				if(!entity.IsColumnDescriptionNull)
					parColumnDescription.Value = entity.ColumnDescription;
				else
					parColumnDescription.Value = System.DBNull.Value;
				cmdParams.Add(parColumnDescription);
			
				System.Data.IDbDataParameter parColumnID = cmd.CreateParameter();
				parColumnID.ParameterName = "@ColumnID";
				if(!entity.IsColumnIDNull)
					parColumnID.Value = entity.ColumnID;
				else
					parColumnID.Value = System.DBNull.Value;
				cmdParams.Add(parColumnID);
			
				System.Data.IDbDataParameter parColumnName = cmd.CreateParameter();
				parColumnName.ParameterName = "@ColumnName";
				if(!entity.IsColumnNameNull)
					parColumnName.Value = entity.ColumnName;
				else
					parColumnName.Value = System.DBNull.Value;
				cmdParams.Add(parColumnName);
			
				System.Data.IDbDataParameter parColumnNameLabelID = cmd.CreateParameter();
				parColumnNameLabelID.ParameterName = "@ColumnNameLabelID";
				if(!entity.IsColumnNameLabelIDNull)
					parColumnNameLabelID.Value = entity.ColumnNameLabelID;
				else
					parColumnNameLabelID.Value = System.DBNull.Value;
				cmdParams.Add(parColumnNameLabelID);
			
				System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
				parDateAdded.ParameterName = "@DateAdded";
				if(!entity.IsDateAddedNull)
					parDateAdded.Value = entity.DateAdded.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
				else
					parDateAdded.Value = System.DBNull.Value;
				cmdParams.Add(parDateAdded);
			
				System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
				parDateRevised.ParameterName = "@DateRevised";
				if(!entity.IsDateRevisedNull)
					parDateRevised.Value = entity.DateRevised.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
				else
					parDateRevised.Value = System.DBNull.Value;
				cmdParams.Add(parDateRevised);
			
				System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
				parHostName.ParameterName = "@HostName";
				if(!entity.IsHostNameNull)
					parHostName.Value = entity.HostName;
				else
					parHostName.Value = System.DBNull.Value;
				cmdParams.Add(parHostName);
			
				System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
				parIsActive.ParameterName = "@IsActive";
				if(!entity.IsIsActiveNull)
					parIsActive.Value = entity.IsActive;
				else
					parIsActive.Value = System.DBNull.Value;
				cmdParams.Add(parIsActive);
			
				System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
				parRevisedBy.ParameterName = "@RevisedBy";
				if(!entity.IsRevisedByNull)
					parRevisedBy.Value = entity.RevisedBy;
				else
					parRevisedBy.Value = System.DBNull.Value;
				cmdParams.Add(parRevisedBy);
					
				return cmd;
		
	        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in ColumnMstInfo object
		/// </summary>
		/// <param name="o">A ColumnMstInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateUpdateCommand(ColumnMstInfo entity) {
	
          
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_ColumnMst");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
		
				System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
				parAddedBy.ParameterName = "@AddedBy";
				if(!entity.IsAddedByNull)
					parAddedBy.Value = entity.AddedBy;
				else
					parAddedBy.Value = System.DBNull.Value;
				cmdParams.Add(parAddedBy);
			
				System.Data.IDbDataParameter parColumnDescription = cmd.CreateParameter();
				parColumnDescription.ParameterName = "@ColumnDescription";
				if(!entity.IsColumnDescriptionNull)
					parColumnDescription.Value = entity.ColumnDescription;
				else
					parColumnDescription.Value = System.DBNull.Value;
				cmdParams.Add(parColumnDescription);
					
				System.Data.IDbDataParameter parColumnName = cmd.CreateParameter();
				parColumnName.ParameterName = "@ColumnName";
				if(!entity.IsColumnNameNull)
					parColumnName.Value = entity.ColumnName;
				else
					parColumnName.Value = System.DBNull.Value;
				cmdParams.Add(parColumnName);
			
				System.Data.IDbDataParameter parColumnNameLabelID = cmd.CreateParameter();
				parColumnNameLabelID.ParameterName = "@ColumnNameLabelID";
				if(!entity.IsColumnNameLabelIDNull)
					parColumnNameLabelID.Value = entity.ColumnNameLabelID;
				else
					parColumnNameLabelID.Value = System.DBNull.Value;
				cmdParams.Add(parColumnNameLabelID);
			
				System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
				parDateAdded.ParameterName = "@DateAdded";
				if(!entity.IsDateAddedNull)
					parDateAdded.Value = entity.DateAdded.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
				else
					parDateAdded.Value = System.DBNull.Value;
				cmdParams.Add(parDateAdded);
			
				System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
				parDateRevised.ParameterName = "@DateRevised";
				if(!entity.IsDateRevisedNull)
					parDateRevised.Value = entity.DateRevised.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
				else
					parDateRevised.Value = System.DBNull.Value;
				cmdParams.Add(parDateRevised);
			
				System.Data.IDbDataParameter parHostName = cmd.CreateParameter();
				parHostName.ParameterName = "@HostName";
				if(!entity.IsHostNameNull)
					parHostName.Value = entity.HostName;
				else
					parHostName.Value = System.DBNull.Value;
				cmdParams.Add(parHostName);
			
				System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
				parIsActive.ParameterName = "@IsActive";
				if(!entity.IsIsActiveNull)
					parIsActive.Value = entity.IsActive;
				else
					parIsActive.Value = System.DBNull.Value;
				cmdParams.Add(parIsActive);
			
				System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
				parRevisedBy.ParameterName = "@RevisedBy";
				if(!entity.IsRevisedByNull)
					parRevisedBy.Value = entity.RevisedBy;
				else
					parRevisedBy.Value = System.DBNull.Value;
				cmdParams.Add(parRevisedBy);
		
				System.Data.IDbDataParameter pkparColumnID = cmd.CreateParameter();
				pkparColumnID.ParameterName = "@ColumnID";
				pkparColumnID.Value = entity.ColumnID;
				cmdParams.Add(pkparColumnID);
	
            
				return cmd;
		
	        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) {
	
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_ColumnMst");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@ColumnID";
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
		
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_ColumnMst");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@ColumnID";
				par.Value = id;
				cmdParams.Add(par);
            
	            return cmd;
		
	        }

		
	
																																	
		
	
		
	
                                  }
}
