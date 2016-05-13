using ART.Integration.Utility.ARTIntegrationServices.FTPFileService;
using ART.Integration.Utility.ARTIntegrationServices.FTPUserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ART.Integration.Utility.Helpers
{
    internal class RemotingHelper
    {
        private RemotingHelper()
        {

        }
        public static IFTPUserService GetFTPUserServiceClientObject()
        {
            return new FTPUserServiceClient();
        }
        public static IFTPFileService GetFTPFileServiceClientObject()
        {
            return new FTPFileServiceClient();
        }
    }
}
