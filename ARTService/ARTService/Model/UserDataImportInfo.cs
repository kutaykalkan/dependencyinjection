using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.Model
{
    public class UserDataImportInfo: DataImportHdrInfo 
    {
        public List<UserHdrInfo> UpdatedUserList { get; set; }
        public List<UserHdrInfo> CreatedUserList { get; set; }
        public List<CompanyUserInfo> CompanyUserInfoList { get; set; }
    }
}
