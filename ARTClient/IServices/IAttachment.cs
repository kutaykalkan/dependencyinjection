using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IAttachment" here, you must also update the reference to "IAttachment" in Web.config.
    [ServiceContract]
    public interface IAttachment
    {
        [OperationContract]
        void DoWork();

        [OperationContract]

        List<AttachmentInfo> GetAttachment(long RecordID, int RecordTypeID, int? RecPeriodID, AppUserInfo oAppUserInfo);


        [OperationContract]
        void InsertAttachment(AttachmentInfo oAttachmentInfo, int recPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void DeleteAttachment(long AttachmentID, AppUserInfo oAppUserInfo);


        [OperationContract]
        int? DeleteAttachmentAndGetFileRefrenceCount(long AttachmentID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void InsertAttachmentBulk(List<AttachmentInfo> oAttachmentInfoList, DateTime? dateAdded, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<AttachmentInfo> GetAllAttachmentForGL(long? GLDataID, int? UserID, short? RoleID, AppUserInfo oAppUserInfo);


    }
}
