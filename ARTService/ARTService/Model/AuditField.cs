using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyStem.ART.Service.Model
{
    public class AuditField
    {
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateRevised { get; set; }
        public string RevisedBy { get; set; }
    }
}
