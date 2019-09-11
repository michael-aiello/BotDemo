using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotDemo.Bots;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BotDemo
{
    public class Startup
    {
        // Inject the IHostingEnvironment into constructor
        public Startup(IHostingEnvironment env)
        {
            // Set the root path
            ContentRootPath = env.ContentRootPath;
        }

        // Track the root path so that it can be used to setup the app configuration
        public string ContentRootPath { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Set up the service configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            services.AddSingleton(configuration);

            // Add your SimpleBot to your application
            services.AddBot<SimpleBot>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(configuration);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            // Tell your application to use Bot Framework
            app.UseBotFramework();
        }
    }
}
