using SkyStem.ART.App.DAO;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyStem.ART.App.BLL
{
    public class AttachmentBLL
    {
        private AttachmentBLL()
        {

        }
        /// <summary>
        /// Get attachments
        /// </summary>
        /// <param name="RecordID"></param>
        /// <param name="RecordTypeID"></param>
        /// <param name="RecPeriodID"></param>
        /// <param name="oAppUserInfo"></param>
        /// <returns></returns>
        public static List<AttachmentInfo> GetAttachment(long RecordID, int RecordTypeID, int? RecPeriodID, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            AttachmentDAO oAttachmentDAO = new AttachmentDAO(oAppUserInfo);
            return oAttachmentDAO.GetAttachmentByRecordIDandRecordTypeID(RecordID, RecordTypeID, RecPeriodID);
        }

        /// <summary>
        /// Get All attachments of GL
        /// </summary>
        /// <param name="GLDataID"></param>
        /// <param name="UserID"></param>
        /// <param name="RoleID"></param>
        /// <param name="oAppUserInfo"></param>
        /// <returns></returns>
        public static List<AttachmentInfo> GetAllAttachmentForGL(long? GLDataID, int? UserID, short? RoleID, AppUserInfo oAppUserInfo)
        {
            List<AttachmentInfo> oAttachmentInfoCollection = new List<AttachmentInfo>();
            ServiceHelper.SetConnectionString(oAppUserInfo);
            // Check permission on GL
            if (!GLDataBLL.CheckGLPermissions(GLDataID, UserID, RoleID, oAppUserInfo))
                return null;
            AttachmentDAO oAttachmentDAO = new AttachmentDAO(oAppUserInfo);
            oAttachmentInfoCollection = oAttachmentDAO.GetAllAttachmentForGL(GLDataID, UserID, RoleID);
            return oAttachmentInfoCollection;
        }

        public static List<AttachmentInfo> GetAllAttachmentForTask(long? taskID, ARTEnums.TaskType taskType, AppUserInfo oAppUserInfo)
        {
            List<AttachmentInfo> oAttachmentInfoCollection = new List<AttachmentInfo>();
            ServiceHelper.SetConnectionString(oAppUserInfo);
            // Check permission on GL
            if (!TaskBLL.CheckTaskPermissions(taskID, oAppUserInfo))
                return null;
            AttachmentDAO oAttachmentDAO = new AttachmentDAO(oAppUserInfo);
            oAttachmentInfoCollection = oAttachmentDAO.GetAllAttachmentForTask(taskID, (short)taskType, oAppUserInfo.RecPeriodID, oAppUserInfo.UserID, oAppUserInfo.RoleID);
            return oAttachmentInfoCollection;
        }
    }
}