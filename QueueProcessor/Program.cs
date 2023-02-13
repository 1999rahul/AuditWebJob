using Azure.Storage.Queues.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nancy.Json;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace QueueProcessor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder();
            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
            });
            builder.ConfigureLogging((context, b) =>
            {
                b.AddConsole();
            });
            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
                b.AddAzureStorageQueues();
            });
            var host = builder.Build();
            using (host)
            {
                await host.RunAsync();
            }
        }
        public static void ProcessQueueMessage([QueueTrigger("audit-queue")] string message, ILogger logger)
        {
            var fact= new CookBookContextFactory().CreateDbContext();
            fact.data.Add(new AuditEmployeeData() {Data= message });
            fact.SaveChanges();
        }
    }
}