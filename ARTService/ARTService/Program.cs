using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace SkyStem.ART.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] oServiceBaseCollection;
            oServiceBaseCollection = new ServiceBase[] 
			{ 
				new DataProcessingService()
			};
            ServiceBase.Run(oServiceBaseCollection);
        }
    }
}
