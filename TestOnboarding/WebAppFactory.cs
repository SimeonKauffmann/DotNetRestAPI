using System;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Moq;
using Newtonsoft.Json;
using OnboardingBackend.Services;
using OnboardingBackend;



namespace TestOnboarding

{
    public class WebAppFactory : WebApplicationFactory<Startup>
    {
        private readonly Action<IServiceCollection> _configureTestServices;

        public WebAppFactory(Action<IServiceCollection> configureTestServices)
        {
            _configureTestServices = configureTestServices;
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices(collection => { _configureTestServices?.Invoke(collection); });
        }
    }
}