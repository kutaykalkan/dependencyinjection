


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class GeographyObjectHdrDAO : GeographyObjectHdrDAOBase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public GeographyObjectHdrDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public void MapObjectWithOrganisationalHierarchyInfo(IDataReader reader, OrganizationalHierarchyInfo oOrganizationalHierarchyInfo)
        {
            oOrganizationalHierarchyInfo.Key2 = reader.GetStringValue("Key2");
            oOrganizationalHierarchyInfo.Key3 = reader.GetStringValue("Key3");
            oOrganizationalHierarchyInfo.Key4 = reader.GetStringValue("Key4");
            oOrganizationalHierarchyInfo.Key5 = reader.GetStringValue("Key5");
            oOrganizationalHierarchyInfo.Key6 = reader.GetStringValue("Key6");
            oOrganizationalHierarchyInfo.Key7 = reader.GetStringValue("Key7");
            oOrganizationalHierarchyInfo.Key8 = reader.GetStringValue("Key8");
            oOrganizationalHierarchyInfo.Key9 = reader.GetStringValue("Key9");
            oOrganizationalHierarchyInfo.FSCaption = reader.GetStringValue("FSCaption");
            oOrganizationalHierarchyInfo.AccountType = reader.GetStringValue("AccountType");

            oOrganizationalHierarchyInfo.Key2LabelID = reader.GetInt32Value("Key2LabelID");
            oOrganizationalHierarchyInfo.Key3LabelID = reader.GetInt32Value("Key3LabelID");
            oOrganizationalHierarchyInfo.Key4LabelID = reader.GetInt32Value("Key4LabelID");
            oOrganizationalHierarchyInfo.Key5LabelID = reader.GetInt32Value("Key5LabelID");
            oOrganizationalHierarchyInfo.Key6LabelID = reader.GetInt32Value("Key6LabelID");
            oOrganizationalHierarchyInfo.Key7LabelID = reader.GetInt32Value("Key7LabelID");
            oOrganizationalHierarchyInfo.Key8LabelID = reader.GetInt32Value("Key8LabelID");
            oOrganizationalHierarchyInfo.Key9LabelID = reader.GetInt32Value("Key9LabelID");
            oOrganizationalHierarchyInfo.FSCaptionLabelID = reader.GetInt32Value("FSCaptionLabelID");
            oOrganizationalHierarchyInfo.AccountTypeLabelID = reader.GetInt32Value("AccountTypeLabelID");

        }


    }
}