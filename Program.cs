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

        // Creates a web app under a new app service plan and cleans up those resources when done
        public static void ConfigureAppService(IAzure azure)
        {
            string appName = "MyConfiguredApp";
            string rgName = "MyConfiguredRG";

            Console.WriteLine("Creating resource group, app service plan and web app.");

            // Create Web app in a new resource group with a new app service plan
            var webApp = azure.WebApps.Define(appName).WithRegion(Region.USWest).WithNewResourceGroup(rgName).WithNewWindowsPlan(PricingTier.BasicB1).Create();
            var plan = webApp.AppServicePlanId;

            Console.WriteLine("Resources created. Check portal.azure.com");
            Console.WriteLine("Press any key to clean up resources.");
            Console.ReadLine();
            Console.WriteLine("Cleaning up resources.");

            // Delete resources one-by-one
            // Note: might be most efficient to delete by resource group, but wanted to learn code for each piece. (This is a learning exercise afterall!)
            azure.WebApps.DeleteById(webApp.Id);

            azure.AppServices.AppServicePlans.DeleteById(plan);

            azure.ResourceGroups.DeleteByName(rgName);

            Console.WriteLine("Resources cleaned.");
        }
    }
}
