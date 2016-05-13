

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

	public abstract class CompanyAlertDetailUserDAOBase : CustomAbstractDAO<CompanyAlertDetailUserInfo> 
	{
        
		/// <summary>
		/// A static representation of column CompanyAlertDetailID
		/// </summary>
		public static readonly string COLUMN_COMPANYALERTDETAILID = "CompanyAlertDetailID";
		/// <summary>
		/// A static representation of column CompanyAlertDetailUserID
		/// </summary>
		public static readonly string COLUMN_COMPANYALERTDETAILUSERID = "CompanyAlertDetailUserID";
		/// <summary>
		/// A static representation of column DateRevised
		/// </summary>
		public static readonly string COLUMN_DATEREVISED = "DateRevised";
		/// <summary>
		/// A static representation of column IsRead
		/// </summary>
		public static readonly string COLUMN_ISREAD = "IsRead";
		/// <summary>
		/// A static representation of column MailSentDate
		/// </summary>
		public static readonly string COLUMN_MAILSENTDATE = "MailSentDate";
		/// <summary>
		/// A static representation of column RevisedBy
		/// </summary>
		public static readonly string COLUMN_REVISEDBY = "RevisedBy";
		/// <summary>
		/// A static representation of column RoleID
		/// </summary>
		public static readonly string COLUMN_ROLEID = "RoleID";
		/// <summary>
		/// A static representation of column UserID
		/// </summary>
		public static readonly string COLUMN_USERID = "UserID";
		/// <summary>
		/// Provides access to the name of the primary key column (CompanyAlertDetailUserID)
		/// </summary>
		public static readonly string TABLE_PRIMARYKEY = "CompanyAlertDetailUserID";

		/// <summary>
		/// Provides access to the name of the table
		/// </summary>
		public static readonly string TABLE_NAME = "CompanyAlertDetailUser";

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
        public CompanyAlertDetailUserDAOBase(AppUserInfo oAppUserInfo) :
            base(DbConstants.DatabaseProviderType, DbConstants.DatabaseType, "CompanyAlertDetailUser", oAppUserInfo.ConnectionString)
        {
            CurrentAppUserInfo  = oAppUserInfo;
	        }
        
	        /// <summary>
	        /// Maps the IDataReader values to a CompanyAlertDetailUserInfo object
	        /// </summary>
	        /// <param name="r">The IDataReader to map</param>
	        /// <returns>CompanyAlertDetailUserInfo</returns>
	        protected override CompanyAlertDetailUserInfo MapObject(System.Data.IDataReader r) {

	            CompanyAlertDetailUserInfo entity = new CompanyAlertDetailUserInfo();
	            
	
				try{
					int ordinal = r.GetOrdinal("CompanyAlertDetailUserID");
					if (!r.IsDBNull(ordinal)) entity.CompanyAlertDetailUserID = ((System.Int64)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("CompanyAlertDetailID");
					if (!r.IsDBNull(ordinal)) entity.CompanyAlertDetailID = ((System.Int64)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("UserID");
					if (!r.IsDBNull(ordinal)) entity.UserID = ((System.Int32)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("RoleID");
					if (!r.IsDBNull(ordinal)) entity.RoleID = ((System.Int16)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("MailSentDate");
					if (!r.IsDBNull(ordinal)) entity.MailSentDate = ((System.DateTime)(r.GetValue(ordinal)));
				}
				catch(Exception){}
	
				try{
					int ordinal = r.GetOrdinal("IsRead");
					if (!r.IsDBNull(ordinal)) entity.IsRead = ((System.Boolean)(r.GetValue(ordinal)));
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
	
	            return entity;
	        }
 
		/// <summary>
		/// Creates the sql insert command, using the values from the passed
		/// in CompanyAlertDetailUserInfo object
		/// </summary>
		/// <param name="o">A CompanyAlertDetailUserInfo object, from which the insert values are pulled</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateInsertCommand(CompanyAlertDetailUserInfo entity) {
	
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_INS_CompanyAlertDetailUser");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;

		
				System.Data.IDbDataParameter parCompanyAlertDetailID = cmd.CreateParameter();
				parCompanyAlertDetailID.ParameterName = "@CompanyAlertDetailID";
				if(!entity.IsCompanyAlertDetailIDNull)
					parCompanyAlertDetailID.Value = entity.CompanyAlertDetailID;
				else
					parCompanyAlertDetailID.Value = System.DBNull.Value;
				cmdParams.Add(parCompanyAlertDetailID);
			
				System.Data.IDbDataParameter parCompanyAlertDetailUserID = cmd.CreateParameter();
				parCompanyAlertDetailUserID.ParameterName = "@CompanyAlertDetailUserID";
				if(!entity.IsCompanyAlertDetailUserIDNull)
					parCompanyAlertDetailUserID.Value = entity.CompanyAlertDetailUserID;
				else
					parCompanyAlertDetailUserID.Value = System.DBNull.Value;
				cmdParams.Add(parCompanyAlertDetailUserID);
			
				System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
				parDateRevised.ParameterName = "@DateRevised";
				if(!entity.IsDateRevisedNull)
					parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
				else
					parDateRevised.Value = System.DBNull.Value;
				cmdParams.Add(parDateRevised);
			
				System.Data.IDbDataParameter parIsRead = cmd.CreateParameter();
				parIsRead.ParameterName = "@IsRead";
				if(!entity.IsIsReadNull)
					parIsRead.Value = entity.IsRead;
				else
					parIsRead.Value = System.DBNull.Value;
				cmdParams.Add(parIsRead);
			
				System.Data.IDbDataParameter parMailSentDate = cmd.CreateParameter();
				parMailSentDate.ParameterName = "@MailSentDate";
				if(!entity.IsMailSentDateNull)
					parMailSentDate.Value = entity.MailSentDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
				else
					parMailSentDate.Value = System.DBNull.Value;
				cmdParams.Add(parMailSentDate);
			
				System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
				parRevisedBy.ParameterName = "@RevisedBy";
				if(!entity.IsRevisedByNull)
					parRevisedBy.Value = entity.RevisedBy;
				else
					parRevisedBy.Value = System.DBNull.Value;
				cmdParams.Add(parRevisedBy);
			
				System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
				parRoleID.ParameterName = "@RoleID";
				if(!entity.IsRoleIDNull)
					parRoleID.Value = entity.RoleID;
				else
					parRoleID.Value = System.DBNull.Value;
				cmdParams.Add(parRoleID);
			
				System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
				parUserID.ParameterName = "@UserID";
				if(!entity.IsUserIDNull)
					parUserID.Value = entity.UserID;
				else
					parUserID.Value = System.DBNull.Value;
				cmdParams.Add(parUserID);
					
				return cmd;
		
	        }

		/// <summary>
		/// Creates the sql update command, using the values from the passed
		/// in CompanyAlertDetailUserInfo object
		/// </summary>
		/// <param name="o">A CompanyAlertDetailUserInfo object, from which the update values are pulled</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateUpdateCommand(CompanyAlertDetailUserInfo entity) {
	
          
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_UPD_CompanyAlertDetailUser");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
		
				System.Data.IDbDataParameter parCompanyAlertDetailID = cmd.CreateParameter();
				parCompanyAlertDetailID.ParameterName = "@CompanyAlertDetailID";
				if(!entity.IsCompanyAlertDetailIDNull)
					parCompanyAlertDetailID.Value = entity.CompanyAlertDetailID;
				else
					parCompanyAlertDetailID.Value = System.DBNull.Value;
				cmdParams.Add(parCompanyAlertDetailID);
					
				System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
				parDateRevised.ParameterName = "@DateRevised";
				if(!entity.IsDateRevisedNull)
					parDateRevised.Value = entity.DateRevised.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
				else
					parDateRevised.Value = System.DBNull.Value;
				cmdParams.Add(parDateRevised);
			
				System.Data.IDbDataParameter parIsRead = cmd.CreateParameter();
				parIsRead.ParameterName = "@IsRead";
				if(!entity.IsIsReadNull)
					parIsRead.Value = entity.IsRead;
				else
					parIsRead.Value = System.DBNull.Value;
				cmdParams.Add(parIsRead);
			
				System.Data.IDbDataParameter parMailSentDate = cmd.CreateParameter();
				parMailSentDate.ParameterName = "@MailSentDate";
				if(!entity.IsMailSentDateNull)
					parMailSentDate.Value = entity.MailSentDate.Value.Subtract(new TimeSpan(2,0,0,0)).ToOADate();
				else
					parMailSentDate.Value = System.DBNull.Value;
				cmdParams.Add(parMailSentDate);
			
				System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
				parRevisedBy.ParameterName = "@RevisedBy";
				if(!entity.IsRevisedByNull)
					parRevisedBy.Value = entity.RevisedBy;
				else
					parRevisedBy.Value = System.DBNull.Value;
				cmdParams.Add(parRevisedBy);
			
				System.Data.IDbDataParameter parRoleID = cmd.CreateParameter();
				parRoleID.ParameterName = "@RoleID";
				if(!entity.IsRoleIDNull)
					parRoleID.Value = entity.RoleID;
				else
					parRoleID.Value = System.DBNull.Value;
				cmdParams.Add(parRoleID);
			
				System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
				parUserID.ParameterName = "@UserID";
				if(!entity.IsUserIDNull)
					parUserID.Value = entity.UserID;
				else
					parUserID.Value = System.DBNull.Value;
				cmdParams.Add(parUserID);
		
				System.Data.IDbDataParameter pkparCompanyAlertDetailUserID = cmd.CreateParameter();
				pkparCompanyAlertDetailUserID.ParameterName = "@CompanyAlertDetailUserID";
				pkparCompanyAlertDetailUserID.Value = entity.CompanyAlertDetailUserID;
				cmdParams.Add(pkparCompanyAlertDetailUserID);
	
            
				return cmd;
		
	        }

		/// <summary>
		/// Creates the sql delete command, using the passed in primary key
		/// </summary>
		/// <param name="id">The primary key of the object to delete</param>
		/// <returns>An IDbCommand</returns>
	        protected override System.Data.IDbCommand CreateDeleteOneCommand(object id) {
	
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_DEL_CompanyAlertDetailUser");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@CompanyAlertDetailUserID";
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
		
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_GET_CompanyAlertDetailUser");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@CompanyAlertDetailUserID";
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
		public IList<CompanyAlertDetailUserInfo> SelectAllByCompanyAlertDetailID(object id)
		{
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyAlertDetailUserByCompanyAlertDetailID");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@CompanyAlertDetailID";
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
		public IList<CompanyAlertDetailUserInfo> SelectAllByUserID(object id)
		{
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyAlertDetailUserByUserID");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@UserID";
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
		public IList<CompanyAlertDetailUserInfo> SelectAllByRoleID(object id)
		{
								
				System.Data.IDbCommand cmd = this.CreateCommand("gsp_SEL_CompanyAlertDetailUserByRoleID");
				cmd.CommandType = CommandType.StoredProcedure;
								
				IDataParameterCollection cmdParams = cmd.Parameters;
            
				System.Data.IDbDataParameter par = cmd.CreateParameter();
				par.ParameterName = "@RoleID";
				par.Value = id;
				cmdParams.Add(par);
            
	            return this.Select(cmd);
		}

				
	
																											
		
	
		
	
                            }
}
