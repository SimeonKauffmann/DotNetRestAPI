using System;
using System.Data.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading;
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
using MongoDB.Driver;
using MongoDB.Driver.Support;
using Xunit.Abstractions;


namespace TestOnboarding.MyServiceTest

{
  public class MyServiceTests
  {
    private Mock<IMongoClient> mongoClient;
    private Mock<IMongoDatabase> mongodb;
    private Mock<IMongoCollection<User>> userCollection;
    private List<User> userList;
    private Mock<IAsyncCursor<User>> userCursor;
    public MyServiceTests()
    {
      this.mongoClient = new Mock<IMongoClient>();
      this.userCollection = new Mock<IMongoCollection<User>>();
      this.mongodb = new Mock<IMongoDatabase>();
      this.userCursor = new Mock<IAsyncCursor<User>>();
      var user = new User()
      {
        Created = new DateTime(),
        UserId = "123",
        Name = "Bob",
        CompletedSteps = new List<Step>() { new() { Completed = true, StepName = "StepName" } },
      };
      this.userList = new List<User>()
            {
                user
            };

    }



    private void InitializeMongoDb()
    {
      this.mongodb.Setup(x => x.GetCollection<User>("onboardingUsers",
          default)).Returns(this.userCollection.Object);
      this.mongoClient.Setup(x => x.GetDatabase(It.IsAny<string>(),
          default)).Returns(this.mongodb.Object);
    }


    private void InitializeMongoUserCollection()
    {
      this.userCursor.Setup(x => x.Current).Returns(this.userList);
      this.userCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);
      this.userCursor.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
          .Returns(Task.FromResult(true)).Returns(Task.FromResult(false));
      this.userCollection.Setup(x => x.AggregateAsync(It.IsAny<PipelineDefinition<User, User>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(this.userCursor.Object);
      this.InitializeMongoDb();
    }





    private IOptions<DbModel> Options { get; set; }

    private readonly ITestOutputHelper output;

    private MyServiceTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    /*
    [Fact]


    public async Task GetUserData()
    {
        this.InitializeMongoUserCollection();


        var config = new Mock<IOptions<DbModel>>();
        config.SetupGet(o => o.Value).Returns(new DbModel()
        {
            mongoDB =
                "mongodb"
        });




        var mongoDbService = new UserService(this.mongoClient.Object);
        var response = await mongoDbService.GetAll();
        output.WriteLine(response[0].Name);

        response.Should().NotBeNull();
        response[0].Name.Should().Be("Bob");
    }


    [Fact]
    public async Task TestMockOption()
    {
        var config = new Mock<IOptions<DbModel>>();
        config.SetupGet(o => o.Value).Returns(new DbModel()
        {
            mongoDB =
                "hi"
        });


        config.Should().NotBeNull();
        config.Object.Value.mongoDB.Should().Be("hi");


    }
*/
  }
}