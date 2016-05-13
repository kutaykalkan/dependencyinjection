using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using System.Timers;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Globalization;
using SkyStem.ART.Service.APP.BLL;

namespace ServiceDummy
{
    class Program
    {
        static void Main(string[] args)
        {
            DataProcessingControlBoard.DataProcessingControl();
            //Alert.RaiseAlerts();
        }
    }
}
