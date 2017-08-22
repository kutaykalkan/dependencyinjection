using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkyStem.ART.Web.Areas.mvc.Controllers
{
    public class AutoSaveController : Controller
    {
        // GET: mvc/AutoSave
        [HttpPost]
        public ActionResult Save(AutoSaveAttributeValueInfo objVal)
        {
            if (objVal.AutoSaveAttributeID.HasValue)
            {
                Helper.SaveAutoSaveAttributeValue((ARTEnums.AutoSaveAttribute)objVal.AutoSaveAttributeID, null, (objVal.Value == "1") ? "True" : "False", false);
            }
            ContentResult result = new ContentResult();
            result.Content = "State Saved";
            return result;
        }
    }
}