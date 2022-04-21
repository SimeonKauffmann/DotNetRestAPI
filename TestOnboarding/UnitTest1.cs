using System;
using System.Data.Common;
using System.Collections.Generic;  
using System.ComponentModel.DataAnnotations;  
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Logging;
using OnboardingBackend.Models; 
using OnboardingBackend.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Xunit;
using Moq;
using Newtonsoft.Json;
using OnboardingBackend.Services;


namespace TestOnboarding
{
   /* public class OperationsControllerTests : IDisposable
    {
        private readonly WebAppFactory _webAppFactory;
        
    public class UnitTest1 : IClassFixture<WebApplicationFactory<OnboardingBackend.Startup>>
    {
        private readonly WebApplicationFactory<OnboardingBackend.Startup> _factory;
        

        public UnitTest1(WebApplicationFactory<OnboardingBackend.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GET_retrieves_test_data()
        {
             _factory.WithWebHostBuilder(builder); 
            // Arrange
           
            
            // Act
            var response = await _httpClient.GetAsync("/test");
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var message = JsonConvert.DeserializeObject<TestModel[]>(await response.Content.ReadAsStringAsync());
            message[0].Message.Should().Be("Hi from Atlas!");

        }
    }*/
    
}