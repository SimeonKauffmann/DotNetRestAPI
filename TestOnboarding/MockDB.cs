using System;
using System.Data.Common;
using System.Collections.Generic;  
using System.ComponentModel.DataAnnotations;  
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Logging;
using OnboardingBackend.Models; 
using OnboardingBackend.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Xunit;
using Moq;
using Newtonsoft.Json;
using OnboardingBackend.Services;
using System.Threading.Tasks;
using MongoDB.Driver;
using OnboardingBackend.Models;
using OnboardingBackend;
using TestOnboarding.MyServiceTest;

namespace TestOnboarding
{
    public class MockDB
    {
        /*
            public class AppTestFixture : WebApplicationFactory<Startup>
            {
                //override methods here as needed
            }
            public class BooksServiceIntegrationTests : IClassFixture<AppTestFixture>
            {
                readonly AppTestFixture _fixture;
                readonly HttpClient _client;
                public BooksServiceIntegrationTests(AppTestFixture fixture)
                {
                    _fixture = fixture;
                    _client = _fixture.CreateClient();
                }
        
                [Fact]
                
                
                public async Task GetProductsValidataData() {
                    this.InitializeMongoProductCollection();
                    var mongoDBService = new MongoDBService(this.settings, this.mongoClient.Object);
                    var response = await mongoDBService.GetProducts(new Models.DTO.Request.SearchRequestDTO() {
                        Department = "any",
                        Categories = new List < string > () {
                            "any"
                        },
                        AttributeFilter = new List < Models.DTO.Request.AttributeFilterDTO > () {
                            new Models.DTO.Request.AttributeFilterDTO() {
                                K = "size",
                                V = "3"
                            }
                        }
                    });
                    
                    
                [Fact]
                public async Task GET_retrieves_test_data()
                {
                    // _factory.WithWebHostBuilder(builder); 
                    // Arrange
        
        
                    // Act
                    var response = await _client.GetAsync("/test");
        
                    // Assert
                    response.StatusCode.Should().Be(HttpStatusCode.OK);
        
                    var message = JsonConvert.DeserializeObject<TestModel[]>(await response.Content.ReadAsStringAsync());
                    message[0].Message.Should().Be("Hi from Atlas!");
                }
            }*/
    }
}