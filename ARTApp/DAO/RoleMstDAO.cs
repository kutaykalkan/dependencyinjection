


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class RoleMstDAO : RoleMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RoleMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public List<RoleMstInfo> GetAllRole(int? companyID, DateTime? periodEndDate)
        {
            List<RoleMstInfo> oRoleMstInfoCollection = null;

            IDbCommand cmd = CreateGetAllRolesByCompanyID(companyID, periodEndDate);
            cmd.CommandType = CommandType.StoredProcedure;
            oRoleMstInfoCollection = new List<RoleMstInfo>(this.Select(cmd));
            return oRoleMstInfoCollection;
        }

        private IDbCommand CreateGetAllRolesByCompanyID(int? companyID, DateTime? periodEndDate)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AllRoleByCompanyID");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();

            parCompanyID.ParameterName = "@CompanyID";
            if (companyID.HasValue)
                parCompanyID.Value = companyID;
            else
                parCompanyID.Value = System.DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parPeriodEndDate = cmd.CreateParameter();
            parPeriodEndDate.ParameterName = "@PeriodEndDate";
            if (periodEndDate.HasValue)
                parPeriodEndDate.Value = periodEndDate;
            else
                parPeriodEndDate.Value = System.DBNull.Value;
            cmdParams.Add(parPeriodEndDate);

            return cmd;
        }

        public List<RoleMstInfo> GetAllRoles()
        {
            List<RoleMstInfo> oRoleMstInfoList = null;
            IDbCommand cmd = CreateGetAllRolesCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            oRoleMstInfoList = new List<RoleMstInfo>(this.Select(cmd));
            return oRoleMstInfoList;
        }

        private IDbCommand CreateGetAllRolesCommand()
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_AllRoles");
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        protected override RoleMstInfo MapObject(IDataReader r)
        {
            RoleMstInfo entity =  base.MapObject(r);
            entity.IsVisibleForAccountAssociationByUserRole = r.GetBooleanValue("IsVisibleForAccountAssociationByUserRole");
            return entity;
        }
    }
}