using System;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.AppService.Fluent.Models;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
// using Microsoft.Azure.Management.Samples.Common;
using System.IO;
using System.Net.Http;

// App service management example 
// https://github.com/Azure-Samples/app-service-dotnet-manage-web-apps-with-custom-domains/blob/master/Program.cs

namespace AppServiceManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var creds = SdkContext.AzureCredentialsFactory.FromFile(Environment.GetEnvironmentVariable("AZURE_AUTH_LOCATION"));

                var azure = Azure.Configure().WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic).Authenticate(creds).WithDefaultSubscription();

                Console.WriteLine($"Selected subscription is: {azure.SubscriptionId}");

                ConfigureAppService(azure);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Creates a web app under a new app service plan
        public static void ConfigureAppService(IAzure azure)
        {

        }
    }
}
