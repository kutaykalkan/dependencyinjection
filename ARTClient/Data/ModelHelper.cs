using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyStem.ART.Client.Data
{
    [Serializable]
    public class ModelHelper
    {
        public static string GetFullName(string FirstName, string LastName)
        {
            string name = FirstName;
            if (string.IsNullOrEmpty(name))
            {
                name = LastName;
            }
            else
            {
                name += " " + LastName;
            }
            return name;
        }

        internal static int GetAging(DateTime? dtOpen)
        {
            DateTime dtNow = DateTime.Now.Date;
            TimeSpan ts = dtNow.Date.Subtract(dtOpen.Value.Date);
            return (int)ts.TotalDays;
        }
    }


}
