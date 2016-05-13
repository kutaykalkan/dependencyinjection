using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.Model
{
    public class TaskImportInfo : DataImportHdrInfo 
    {
        public List<TaskHdrInfo> CreatedTaskList { get; set; }
    }
}
