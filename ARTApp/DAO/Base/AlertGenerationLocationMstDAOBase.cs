

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

	public abstract class AlertGenerationLocationMstDAOBase : CustomAbstractDAO<AlertGenerationLocationMstInfo> 
	{
        
		/// <summary>
		/// A static representation of column AddedBy
		/// </summary>
		public static readonly string COLUMN_ADDEDBY = "AddedBy";
		/// <summary>
		/// A static representation of column AlertGenerationLocation
		/// </summary>
		public static readonly string COLUMN_ALERTGENERATIONLOCATION = "AlertGenerationLocation";
		/// <summary>
		/// A static representation of column AlertGenerationLocationDesc
		/// </summary>
		public static readonly string COLUMN_ALERTGENERATIONLOCATIONDESC = "AlertGenerationLocationDesc";
		/// <summary>
		/// A static representation of column AlertGenerationLocationID
		/// </summary>
		public static readonly string COLUMN_ALERTGENERATIONLOCATIONID = "AlertGenerationLocationID";
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
		/// Provides access to the name of the primary key column (AlertGenerationLocationID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "AlertGenerationLocationID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "AlertGenerationLocationMst";

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
        public AlertGenerationLocationMstDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "AlertGenerationLocationMst", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
	        }
        
	        /// <summary>
	        /// Maps the IDataReader values to a AlertGenerationLocationMstInfo object
	        /// </summary>
	        /// <param name="r">The IDataReader to map</param>
	        /// <returns>AlertGenerationLocationMstInfo</returns>
	        protected override AlertGenerationLocationMstInfo MapObject(System.Data.IDataReader r) {

	            AlertGenerationLocationMstInfo entity = new AlertGenerationLocationMstInfo();
	            
	
				try{
					int ordinal = r.GetOrdinal("AlertGenerationLocationID");
					if (!r.IsDBNull(ordinal)) entity.AlertGenerationLocationID = ((System.Int16)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("AlertGenerationLocation");
					if (!r.IsDBNull(ordinal)) entity.AlertGenerationLocation = ((System.String)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("AlertGenerationLocationDesc");
					if (!r.IsDBNull(ordinal)) entity.AlertGenerationLocationDesc = ((System.String)(r.GetValue(ordinal)));
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
		/// in AlertGenerationLocationMstInfo object
		/// </summary>
		/// <param name="o">A AlertGenerationLocationMstInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateInsertCommand(AlertGenerationLocationMstInfo entity) {
	
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_AlertGenerationLocationMst");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;

		
				System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
				parAddedBy.ParameterName = "@AddedBy";
				if(!entity.IsAddedByNull)
					parAddedBy.Value = entity.AddedBy;
				else
					parAddedBy.Value = System.DBNull.Value;
				cmdParams.Add(parAddedBy);
			
				System.Data.IDbDataParameter parAlertGenerationLocation = cmd.CreateParameter();
				parAlertGenerationLocation.ParameterName = "@AlertGenerationLocation";
				if(!entity.IsAlertGenerationLocationNull)
					parAlertGenerationLocation.Value = entity.AlertGenerationLocation;
				else
					parAlertGenerationLocation.Value = System.DBNull.Value;
				cmdParams.Add(parAlertGenerationLocation);
			
				System.Data.IDbDataParameter parAlertGenerationLocationDesc = cmd.CreateParameter();
				parAlertGenerationLocationDesc.ParameterName = "@AlertGenerationLocationDesc";
				if(!entity.IsAlertGenerationLocationDescNull)
					parAlertGenerationLocationDesc.Value = entity.AlertGenerationLocationDesc;
				else
					parAlertGenerationLocationDesc.Value = System.DBNull.Value;
				cmdParams.Add(parAlertGenerationLocationDesc);
			
				System.Data.IDbDataParameter parAlertGenerationLocationID = cmd.CreateParameter();
				parAlertGenerationLocationID.ParameterName = "@AlertGenerationLocationID";
				if(!entity.IsAlertGenerationLocationIDNull)
					parAlertGenerationLocationID.Value = entity.AlertGenerationLocationID;
				else
					parAlertGenerationLocationID.Value = System.DBNull.Value;
				cmdParams.Add(parAlertGenerationLocationID);
			
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
		/// in AlertGenerationLocationMstInfo object
		/// </summary>
		/// <param name="o">A AlertGenerationLocationMstInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateUpdateCommand(AlertGenerationLocationMstInfo entity) {
	
          
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_AlertGenerationLocationMst");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
		
				System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
				parAddedBy.ParameterName = "@AddedBy";
				if(!entity.IsAddedByNull)
					parAddedBy.Value = entity.AddedBy;
				else
					parAddedBy.Value = System.DBNull.Value;
				cmdParams.Add(parAddedBy);
			
				System.Data.IDbDataParameter parAlertGenerationLocation = cmd.CreateParameter();
				parAlertGenerationLocation.ParameterName = "@AlertGenerationLocation";
				if(!entity.IsAlertGenerationLocationNull)
					parAlertGenerationLocation.Value = entity.AlertGenerationLocation;
				else
					parAlertGenerationLocation.Value = System.DBNull.Value;
				cmdParams.Add(parAlertGenerationLocation);
			
				System.Data.IDbDataParameter parAlertGenerationLocationDesc = cmd.CreateParameter();
				parAlertGenerationLocationDesc.ParameterName = "@AlertGenerationLocationDesc";
				if(!entity.IsAlertGenerationLocationDescNull)
					parAlertGenerationLocationDesc.Value = entity.AlertGenerationLocationDesc;
				else
					parAlertGenerationLocationDesc.Value = System.DBNull.Value;
				cmdParams.Add(parAlertGenerationLocationDesc);
					
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
		
				System.Data.IDbDataParameter pkparAlertGenerationLocationID = cmd.CreateParameter();
				pkparAlertGenerationLocationID.ParameterName = "@AlertGenerationLocationID";
				pkparAlertGenerationLocationID.Value = entity.AlertGenerationLocationID;
				cmdParams.Add(pkparAlertGenerationLocationID);
	
            
				return cmd;
		
	        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) {
	
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_AlertGenerationLocationMst");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@AlertGenerationLocationID";
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
		
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_AlertGenerationLocationMst");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@AlertGenerationLocationID";
				par.Value = id;
				cmdParams.Add(par);
            
	            return cmd;
		
	        }

		
	
																														
		
	
		
	
               
          
        
		
		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<AlertGenerationLocationMstInfo> SelectAlertGenerationLocationMstDetailsAssociatedToAlertTypeMstByAlertMst(AlertTypeMstInfo entity)
		{
			return this.SelectAlertGenerationLocationMstDetailsAssociatedToAlertTypeMstByAlertMst(entity.AlertTypeID);		
		}

		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<AlertGenerationLocationMstInfo> SelectAlertGenerationLocationMstDetailsAssociatedToAlertTypeMstByAlertMst(object id)
		{							
				
				System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AlertGenerationLocationMst] INNER JOIN [AlertMst] ON [AlertGenerationLocationMst].[AlertGenerationLocationID] = [AlertMst].[AlertGenerationLocationID] INNER JOIN [AlertTypeMst] ON [AlertMst].[AlertTypeID] = [AlertTypeMst].[AlertTypeID]  WHERE  [AlertTypeMst].[AlertTypeID] = @AlertTypeID ");
				IDataParameterCollection cmdParams = cmd.Parameters;
            	System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@AlertTypeID";
				par.Value = id;
								
				cmdParams.Add(par);
                List<AlertGenerationLocationMstInfo> objAlertGenerationLocationMstEntityColl = new List<AlertGenerationLocationMstInfo>(this.Select(cmd));
	            return objAlertGenerationLocationMstEntityColl;
	    }

		 		       
        		       
        
		
		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<AlertGenerationLocationMstInfo> SelectAlertGenerationLocationMstDetailsAssociatedToAlertResponseTypeMstByAlertMst(AlertResponseTypeMstInfo entity)
		{
			return this.SelectAlertGenerationLocationMstDetailsAssociatedToAlertResponseTypeMstByAlertMst(entity.AlertResponseTypeID);		
		}

		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<AlertGenerationLocationMstInfo> SelectAlertGenerationLocationMstDetailsAssociatedToAlertResponseTypeMstByAlertMst(object id)
		{							
				
				System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [AlertGenerationLocationMst] INNER JOIN [AlertMst] ON [AlertGenerationLocationMst].[AlertGenerationLocationID] = [AlertMst].[AlertGenerationLocationID] INNER JOIN [AlertResponseTypeMst] ON [AlertMst].[AlertResponseTypeID] = [AlertResponseTypeMst].[AlertResponseTypeID]  WHERE  [AlertResponseTypeMst].[AlertResponseTypeID] = @AlertResponseTypeID ");
				IDataParameterCollection cmdParams = cmd.Parameters;
            	System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@AlertResponseTypeID";
				par.Value = id;
								
				cmdParams.Add(par);
                List<AlertGenerationLocationMstInfo> objAlertGenerationLocationMstEntityColl = new List<AlertGenerationLocationMstInfo>(this.Select(cmd));
	            return objAlertGenerationLocationMstEntityColl;
	    }

		 		       
        		                       }
}
