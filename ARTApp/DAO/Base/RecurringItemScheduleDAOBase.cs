

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

	public abstract class RecurringItemScheduleDAOBase : CustomAbstractDAO<RecurringItemScheduleInfo> 
	{
        
		/// <summary>
		/// A static representation of column Balance
		/// </summary>
		public static readonly string COLUMN_BALANCE = "Balance";
		/// <summary>
		/// A static representation of column BeginDate
		/// </summary>
		public static readonly string COLUMN_BEGINDATE = "BeginDate";
		/// <summary>
		/// A static representation of column EndDate
		/// </summary>
		public static readonly string COLUMN_ENDDATE = "EndDate";
		/// <summary>
		/// A static representation of column GLReconciliationItemInputID
		/// </summary>
		public static readonly string COLUMN_GLRECONCILIATIONITEMINPUTID = "GLReconciliationItemInputID";
		/// <summary>
		/// A static representation of column RecurringItemScheduleID
		/// </summary>
		public static readonly string COLUMN_RECURRINGITEMSCHEDULEID = "RecurringItemScheduleID";
		/// <summary>
		/// A static representation of column ScheduleName
		/// </summary>
		public static readonly string COLUMN_SCHEDULENAME = "ScheduleName";
		/// <summary>
		/// Provides access to the name of the primary key column (RecurringItemScheduleID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "RecurringItemScheduleID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "RecurringItemSchedule";

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
        public RecurringItemScheduleDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "RecurringItemSchedule", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;   
        }
        
	        /// <summary>
	        /// Maps the IDataReader values to a RecurringItemScheduleInfo object
	        /// </summary>
	        /// <param name="r">The IDataReader to map</param>
	        /// <returns>RecurringItemScheduleInfo</returns>
	        protected override RecurringItemScheduleInfo MapObject(System.Data.IDataReader r) {

	            RecurringItemScheduleInfo entity = new RecurringItemScheduleInfo();
	            
	
				try{
					int ordinal = r.GetOrdinal("RecurringItemScheduleID");
					if (!r.IsDBNull(ordinal)) entity.RecurringItemScheduleID = ((System.Int32)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("GLReconciliationItemInputID");
					if (!r.IsDBNull(ordinal)) entity.GLReconciliationItemInputID = ((System.Int64)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("ScheduleName");
					if (!r.IsDBNull(ordinal)) entity.ScheduleName = ((System.String)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("BeginDate");
					if (!r.IsDBNull(ordinal)) entity.BeginDate = ((System.DateTime)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("EndDate");
					if (!r.IsDBNull(ordinal)) entity.EndDate = ((System.DateTime)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("Balance");
					if (!r.IsDBNull(ordinal)) entity.Balance = ((System.Decimal)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
	            return entity;
	        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in RecurringItemScheduleInfo object
		/// </summary>
		/// <param name="o">A RecurringItemScheduleInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateInsertCommand(RecurringItemScheduleInfo entity) {
	
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_RecurringItemSchedule");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;

		
				System.Data.IDbDataParameter parBalance = cmd.CreateParameter();
				parBalance.ParameterName = "@Balance";
				if(!entity.IsBalanceNull)
					parBalance.Value = entity.Balance;
				else
					parBalance.Value = System.DBNull.Value;
				cmdParams.Add(parBalance);
			
				System.Data.IDbDataParameter parBeginDate = cmd.CreateParameter();
				parBeginDate.ParameterName = "@BeginDate";
				if(!entity.IsBeginDateNull)
					parBeginDate.Value = entity.BeginDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
				else
					parBeginDate.Value = System.DBNull.Value;
				cmdParams.Add(parBeginDate);
			
				System.Data.IDbDataParameter parEndDate = cmd.CreateParameter();
				parEndDate.ParameterName = "@EndDate";
				if(!entity.IsEndDateNull)
					parEndDate.Value = entity.EndDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
				else
					parEndDate.Value = System.DBNull.Value;
				cmdParams.Add(parEndDate);
			
				System.Data.IDbDataParameter parGLReconciliationItemInputID = cmd.CreateParameter();
				parGLReconciliationItemInputID.ParameterName = "@GLReconciliationItemInputID";
				if(!entity.IsGLReconciliationItemInputIDNull)
					parGLReconciliationItemInputID.Value = entity.GLReconciliationItemInputID;
				else
					parGLReconciliationItemInputID.Value = System.DBNull.Value;
				cmdParams.Add(parGLReconciliationItemInputID);
					
				System.Data.IDbDataParameter parScheduleName = cmd.CreateParameter();
				parScheduleName.ParameterName = "@ScheduleName";
				if(!entity.IsScheduleNameNull)
					parScheduleName.Value = entity.ScheduleName;
				else
					parScheduleName.Value = System.DBNull.Value;
				cmdParams.Add(parScheduleName);
					
				return cmd;
		
	        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in RecurringItemScheduleInfo object
		/// </summary>
		/// <param name="o">A RecurringItemScheduleInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateUpdateCommand(RecurringItemScheduleInfo entity) {
	
          
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_RecurringItemSchedule");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
		
				System.Data.IDbDataParameter parBalance = cmd.CreateParameter();
				parBalance.ParameterName = "@Balance";
				if(!entity.IsBalanceNull)
					parBalance.Value = entity.Balance;
				else
					parBalance.Value = System.DBNull.Value;
				cmdParams.Add(parBalance);
			
				System.Data.IDbDataParameter parBeginDate = cmd.CreateParameter();
				parBeginDate.ParameterName = "@BeginDate";
				if(!entity.IsBeginDateNull)
					parBeginDate.Value = entity.BeginDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
				else
					parBeginDate.Value = System.DBNull.Value;
				cmdParams.Add(parBeginDate);
			
				System.Data.IDbDataParameter parEndDate = cmd.CreateParameter();
				parEndDate.ParameterName = "@EndDate";
				if(!entity.IsEndDateNull)
					parEndDate.Value = entity.EndDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
				else
					parEndDate.Value = System.DBNull.Value;
				cmdParams.Add(parEndDate);
			
				System.Data.IDbDataParameter parGLReconciliationItemInputID = cmd.CreateParameter();
				parGLReconciliationItemInputID.ParameterName = "@GLReconciliationItemInputID";
				if(!entity.IsGLReconciliationItemInputIDNull)
					parGLReconciliationItemInputID.Value = entity.GLReconciliationItemInputID;
				else
					parGLReconciliationItemInputID.Value = System.DBNull.Value;
				cmdParams.Add(parGLReconciliationItemInputID);
					
				System.Data.IDbDataParameter parScheduleName = cmd.CreateParameter();
				parScheduleName.ParameterName = "@ScheduleName";
				if(!entity.IsScheduleNameNull)
					parScheduleName.Value = entity.ScheduleName;
				else
					parScheduleName.Value = System.DBNull.Value;
				cmdParams.Add(parScheduleName);
		
				System.Data.IDbDataParameter pkparRecurringItemScheduleID = cmd.CreateParameter();
				pkparRecurringItemScheduleID.ParameterName = "@RecurringItemScheduleID";
				pkparRecurringItemScheduleID.Value = entity.RecurringItemScheduleID;
				cmdParams.Add(pkparRecurringItemScheduleID);
	
            
				return cmd;
		
	        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) {
	
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_RecurringItemSchedule");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@RecurringItemScheduleID";
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
		
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_RecurringItemSchedule");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@RecurringItemScheduleID";
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
		public IList<RecurringItemScheduleInfo> SelectAllByGLReconciliationItemInputID(object id)
		{
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_RecurringItemScheduleByGLReconciliationItemInputID");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@GLReconciliationItemInputID";
				par.Value = id;
				cmdParams.Add(par);
            
	            return this.Select(cmd);
		}

				
	
																					
		
	
		
		protected override void CustomSave(RecurringItemScheduleInfo o, IDbConnection connection){
						
			string query = QueryHelper.GetSqlServerLastInsertedCommand(RecurringItemScheduleDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);

						
		}
		
		protected override void CustomSave(RecurringItemScheduleInfo o, IDbConnection connection, IDbTransaction transaction){
						
			string query = QueryHelper.GetSqlServerLastInsertedCommand(RecurringItemScheduleDAO.TABLE_NAME);
			IDbCommand cmd = Adapdev.Data.DbProviderFactory.CreateCommand(DbConstants.DatabaseProviderType);
			cmd.CommandText = query;
			cmd.Transaction = transaction;
			cmd.Connection = connection;
			object id = cmd.ExecuteScalar();
			this.MapIdentity(o, id);
			
						
		}		
		
		private void MapIdentity(RecurringItemScheduleInfo entity, object id){
			entity.RecurringItemScheduleID = Convert.ToInt32(id);
		}
		
			
		
	
                  
          
        
		
		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<RecurringItemScheduleInfo> SelectRecurringItemScheduleDetailsAssociatedToReconciliationPeriodByRecurringItemScheduleReconciliationPeriod(ReconciliationPeriodInfo entity)
		{
			return this.SelectRecurringItemScheduleDetailsAssociatedToReconciliationPeriodByRecurringItemScheduleReconciliationPeriod(entity.ReconciliationPeriodID);		
		}

		/// <summary>
		/// Creates the Entity Collection of table related to present table by n-n relationship
		/// </summary>
		/// <param name="id">The entity object with its primary key set to select Collection of Entity related to it by n-n realtionship</param>
		/// <returns>A collection of related entities</returns>
		public IList<RecurringItemScheduleInfo> SelectRecurringItemScheduleDetailsAssociatedToReconciliationPeriodByRecurringItemScheduleReconciliationPeriod(object id)
		{							
				
				System.Data.IDbCommand cmd = this.CreateCommand("SELECT  *  FROM [RecurringItemSchedule] INNER JOIN [RecurringItemScheduleReconciliationPeriod] ON [RecurringItemSchedule].[RecurringItemScheduleID] = [RecurringItemScheduleReconciliationPeriod].[AccurableItemScheduleID] INNER JOIN [ReconciliationPeriod] ON [RecurringItemScheduleReconciliationPeriod].[ReconciliationPeriodID] = [ReconciliationPeriod].[ReconciliationPeriodID]  WHERE  [ReconciliationPeriod].[ReconciliationPeriodID] = @ReconciliationPeriodID ");
				IDataParameterCollection cmdParams = cmd.Parameters;
            	System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@ReconciliationPeriodID";
				par.Value = id;
								
				cmdParams.Add(par);
                List<RecurringItemScheduleInfo> objRecurringItemScheduleEntityColl = new List<RecurringItemScheduleInfo>(this.Select(cmd));
	            return objRecurringItemScheduleEntityColl;
	    }

		 		           }
}
