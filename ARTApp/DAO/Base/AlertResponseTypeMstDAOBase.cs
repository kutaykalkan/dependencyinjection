

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

    public abstract class AlertResponseTypeMstDAOBase : CustomAbstractDAO<AlertResponseTypeMstInfo> 
	{
        
		/// <summary>
		/// A static representation of column AddedBy
		/// </summary>
		public static readonly string COLUMN_ADDEDBY = "AddedBy";
		/// <summary>
		/// A static representation of column AlertResponseType
		/// </summary>
		public static readonly string COLUMN_ALERTRESPONSETYPE = "AlertResponseType";
		/// <summary>
		/// A static representation of column AlertResponseTypeDesc
		/// </summary>
		public static readonly string COLUMN_ALERTRESPONSETYPEDESC = "AlertResponseTypeDesc";
		/// <summary>
		/// A static representation of column AlertResponseTypeID
		/// </summary>
		public static readonly string COLUMN_ALERTRESPONSETYPEID = "AlertResponseTypeID";
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
		/// Provides access to the name of the primary key column (AlertResponseTypeID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "AlertResponseTypeID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "AlertResponseTypeMst";

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
        public AlertResponseTypeMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "AlertResponseTypeMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
	        }
        
	        /// <summary>
	        /// Maps the IDataReader values to a AlertResponseTypeMstInfo object
	        /// </summary>
	        /// <param name="r">The IDataReader to map</param>
	        /// <returns>AlertResponseTypeMstInfo</returns>
	        protected override AlertResponseTypeMstInfo MapObject(System.Data.IDataReader r) {

	            AlertResponseTypeMstInfo entity = new AlertResponseTypeMstInfo();
	            
	
				try{
					int ordinal = r.GetOrdinal("AlertResponseTypeID");
					if (!r.IsDBNull(ordinal)) entity.AlertResponseTypeID = ((System.Int16)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("AlertResponseType");
					if (!r.IsDBNull(ordinal)) entity.AlertResponseType = ((System.String)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("AlertResponseTypeDesc");
					if (!r.IsDBNull(ordinal)) entity.AlertResponseTypeDesc = ((System.String)(r.GetValue(ordinal)));
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
		/// in AlertResponseTypeMstInfo object
		/// </summary>
		/// <param name="o">A AlertResponseTypeMstInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateInsertCommand(AlertResponseTypeMstInfo entity) {
	
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_AlertResponseTypeMst");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;

		
				System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
				parAddedBy.ParameterName = "@AddedBy";
				if(!entity.IsAddedByNull)
					parAddedBy.Value = entity.AddedBy;
				else
					parAddedBy.Value = System.DBNull.Value;
				cmdParams.Add(parAddedBy);
			
				System.Data.IDbDataParameter parAlertResponseType = cmd.CreateParameter();
				parAlertResponseType.ParameterName = "@AlertResponseType";
				if(!entity.IsAlertResponseTypeNull)
					parAlertResponseType.Value = entity.AlertResponseType;
				else
					parAlertResponseType.Value = System.DBNull.Value;
				cmdParams.Add(parAlertResponseType);
			
				System.Data.IDbDataParameter parAlertResponseTypeDesc = cmd.CreateParameter();
				parAlertResponseTypeDesc.ParameterName = "@AlertResponseTypeDesc";
				if(!entity.IsAlertResponseTypeDescNull)
					parAlertResponseTypeDesc.Value = entity.AlertResponseTypeDesc;
				else
					parAlertResponseTypeDesc.Value = System.DBNull.Value;
				cmdParams.Add(parAlertResponseTypeDesc);
			
				System.Data.IDbDataParameter parAlertResponseTypeID = cmd.CreateParameter();
				parAlertResponseTypeID.ParameterName = "@AlertResponseTypeID";
				if(!entity.IsAlertResponseTypeIDNull)
					parAlertResponseTypeID.Value = entity.AlertResponseTypeID;
				else
					parAlertResponseTypeID.Value = System.DBNull.Value;
				cmdParams.Add(parAlertResponseTypeID);
			
				System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
				parDateAdded.ParameterName = "@DateAdded";
				if(!entity.IsDateAddedNull)
					parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
				else
					parDateAdded.Value = System.DBNull.Value;
				cmdParams.Add(parDateAdded);
			
				System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
				parDateRevised.ParameterName = "@DateRevised";
				if(!entity.IsDateRevisedNull)
					parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
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
		/// in AlertResponseTypeMstInfo object
		/// </summary>
		/// <param name="o">A AlertResponseTypeMstInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateUpdateCommand(AlertResponseTypeMstInfo entity) {
	
          
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_AlertResponseTypeMst");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
		
				System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
				parAddedBy.ParameterName = "@AddedBy";
				if(!entity.IsAddedByNull)
					parAddedBy.Value = entity.AddedBy;
				else
					parAddedBy.Value = System.DBNull.Value;
				cmdParams.Add(parAddedBy);
			
				System.Data.IDbDataParameter parAlertResponseType = cmd.CreateParameter();
				parAlertResponseType.ParameterName = "@AlertResponseType";
				if(!entity.IsAlertResponseTypeNull)
					parAlertResponseType.Value = entity.AlertResponseType;
				else
					parAlertResponseType.Value = System.DBNull.Value;
				cmdParams.Add(parAlertResponseType);
			
				System.Data.IDbDataParameter parAlertResponseTypeDesc = cmd.CreateParameter();
				parAlertResponseTypeDesc.ParameterName = "@AlertResponseTypeDesc";
				if(!entity.IsAlertResponseTypeDescNull)
					parAlertResponseTypeDesc.Value = entity.AlertResponseTypeDesc;
				else
					parAlertResponseTypeDesc.Value = System.DBNull.Value;
				cmdParams.Add(parAlertResponseTypeDesc);
					
				System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
				parDateAdded.ParameterName = "@DateAdded";
				if(!entity.IsDateAddedNull)
					parDateAdded.Value = entity.DateAdded.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
				else
					parDateAdded.Value = System.DBNull.Value;
				cmdParams.Add(parDateAdded);
			
				System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
				parDateRevised.ParameterName = "@DateRevised";
				if(!entity.IsDateRevisedNull)
					parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
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
		
				System.Data.IDbDataParameter pkparAlertResponseTypeID = cmd.CreateParameter();
				pkparAlertResponseTypeID.ParameterName = "@AlertResponseTypeID";
				pkparAlertResponseTypeID.Value = entity.AlertResponseTypeID;
				cmdParams.Add(pkparAlertResponseTypeID);
	
            
				return cmd;
		
	        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) {
	
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_AlertResponseTypeMst");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@AlertResponseTypeID";
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
		
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_AlertResponseTypeMst");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@AlertResponseTypeID";
				par.Value = id;
				cmdParams.Add(par);
            
	            return cmd;
		
	        }

		
	
																														
		
	
		
	
               
          
        
		
		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<AlertResponseTypeMstInfo> SelectAlertResponseTypeMstDetailsAssociatedToAlertTypeMstByAlertMst(AlertTypeMstInfo entity)
		{
			return this.SelectAlertResponseTypeMstDetailsAssociatedToAlertTypeMstByAlertMst(entity.AlertTypeID);		
		}

		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<AlertResponseTypeMstInfo> SelectAlertResponseTypeMstDetailsAssociatedToAlertTypeMstByAlertMst(object id)
		{							
				
				System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AlertResponseTypeMst] INNER JOIN [AlertMst] ON [AlertResponseTypeMst].[AlertResponseTypeID] = [AlertMst].[AlertResponseTypeID] INNER JOIN [AlertTypeMst] ON [AlertMst].[AlertTypeID] = [AlertTypeMst].[AlertTypeID]  WHERE  [AlertTypeMst].[AlertTypeID] = @AlertTypeID ");
				IDataParameterCollection cmdParams = cmd.Parameters;
            	System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@AlertTypeID";
				par.Value = id;
								
				cmdParams.Add(par);
                List<AlertResponseTypeMstInfo> objAlertResponseTypeMstEntityColl = new List<AlertResponseTypeMstInfo>(this.Select(cmd));
	            return objAlertResponseTypeMstEntityColl;
	    }

		 		       
        		       
        
		
		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<AlertResponseTypeMstInfo> SelectAlertResponseTypeMstDetailsAssociatedToAlertGenerationLocationMstByAlertMst(AlertGenerationLocationMstInfo entity)
		{
			return this.SelectAlertResponseTypeMstDetailsAssociatedToAlertGenerationLocationMstByAlertMst(entity.AlertGenerationLocationID);		
		}

		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<AlertResponseTypeMstInfo> SelectAlertResponseTypeMstDetailsAssociatedToAlertGenerationLocationMstByAlertMst(object id)
		{							
				
				System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AlertResponseTypeMst] INNER JOIN [AlertMst] ON [AlertResponseTypeMst].[AlertResponseTypeID] = [AlertMst].[AlertResponseTypeID] INNER JOIN [AlertGenerationLocationMst] ON [AlertMst].[AlertGenerationLocationID] = [AlertGenerationLocationMst].[AlertGenerationLocationID]  WHERE  [AlertGenerationLocationMst].[AlertGenerationLocationID] = @AlertGenerationLocationID ");
				IDataParameterCollection cmdParams = cmd.Parameters;
            	System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@AlertGenerationLocationID";
				par.Value = id;
								
				cmdParams.Add(par);
                List<AlertResponseTypeMstInfo> objAlertResponseTypeMstEntityColl = new List<AlertResponseTypeMstInfo>(this.Select(cmd));
	            return objAlertResponseTypeMstEntityColl;
	    }

		 		       
        		                       }
}
