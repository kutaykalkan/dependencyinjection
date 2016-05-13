<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        //ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
        //    new ScriptResourceDefinition
        //    {
        //        Path = "~/JavaScript/jquery-1.4.2.min.js"
        //    }
        //);
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

        int maxRequestLength = Convert.ToInt32(SkyStem.ART.Web.Utility.AppSettingHelper.GetAppSettingValue(SkyStem.ART.Web.Data.AppSettingConstants.DEFAULTDATAIMPORTFILESIZE));
        //System.Web.Configuration.HttpRuntimeSection runTime = (System.Web.Configuration.HttpRuntimeSection)System.Web.Configuration.WebConfigurationManager.GetSection("system.web/httpRuntime");

        //Approx 100 Kb(for page content) size has been deducted because the maxRequestLength proprty is the page size, not only the file upload size

        //int maxRequestLength = (runTime.MaxRequestLength - 100) * 1024;

        //This code is used to check the request length of the page and if the request length is greater than 

        //MaxRequestLength then retrun to the same page with extra query string value action=exception

        HttpContext context = ((HttpApplication)sender).Context;

        if (context.Request.ContentLength > maxRequestLength)
        {

            IServiceProvider provider = (IServiceProvider)context;

            HttpWorkerRequest workerRequest = (HttpWorkerRequest)provider.GetService(typeof(HttpWorkerRequest));

            // Check if body contains data

            if (workerRequest.HasEntityBody())
            {

                // get the total body length

                int requestLength = workerRequest.GetTotalEntityBodyLength();

                // Get the initial bytes loaded

                int initialBytes = 0;

                if (workerRequest.GetPreloadedEntityBody() != null)

                    initialBytes = workerRequest.GetPreloadedEntityBody().Length;

                if (!workerRequest.IsEntireEntityBodyIsPreloaded())
                {

                    byte[] buffer = new byte[512000];

                    // Set the received bytes to initial bytes before start reading

                    int receivedBytes = initialBytes;
                    // Request needs to be completely read before doing any redirection
                    while (requestLength - receivedBytes >= initialBytes)
                    {

                        // Read another set of bytes

                        initialBytes = workerRequest.ReadEntityBody(buffer, buffer.Length);

                        // Update the received bytes

                        receivedBytes += initialBytes;

                    }
                    initialBytes = workerRequest.ReadEntityBody(buffer, requestLength - receivedBytes);
                }
            }

            string url = null;

            // Redirect the user to the Error page . 
            if (context.Request.Url.AbsolutePath.Contains("DocumentUpload.aspx"))
            {
                url = "~/Pages/ErrorHandlerPopup.aspx?";
            }
            else
            {
                url = "~/Pages/ErrorHandler.aspx?";
            }
            context.Server.ClearError();
            SkyStem.ART.Web.Utility.SessionHelper.TransferToUrl(url + SkyStem.ART.Web.Data.QueryStringConstants.ERROR_MESSAGE_LABEL_ID + "=" + 1975);
            //context.Server.Transfer(url + SkyStem.ART.Web.Data.QueryStringConstants.ERROR_MESSAGE_LABEL_ID + "=" + 1975);
            //context.Response.Redirect(url + SkyStem.ART.Web.Data.QueryStringConstants.ERROR_MESSAGE_LABEL_ID + "=" + 1975);
        }
        else
        {
            Exception objError = context.Server.GetLastError();
            if (objError != null)
            {
                objError = objError.GetBaseException();
                SkyStem.ART.Web.Utility.Helper.LogException(objError);
                context.Server.ClearError();
            }
            //context.Server.Transfer("~/Logout.aspx"); 
            SkyStem.ART.Web.Utility.SessionHelper.TransferToUrl("~/Logout.aspx");
        }
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void Application_PreSendRequestHeaders()
    {
        Response.Headers.Remove("Server");
        Response.Headers.Remove("X-AspNet-Version");
        Response.Headers.Remove("X-AspNetMvc-Version");
        Response.Headers.Remove("X-Powered-By");
    }
       
</script>

