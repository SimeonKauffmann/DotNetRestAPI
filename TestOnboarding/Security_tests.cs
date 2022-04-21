
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using OnboardingBackend.Models;
using FluentAssertions;
using Xunit;
using System.Net.Http.Json;


namespace TestOnboarding
{
  public class MyServiceTests
  {
    private readonly HttpClient _httpClient = new();



    [Fact]
    public async Task SecurtiyGetUser()
    {
      using (var request = new HttpRequestMessage(HttpMethod.Get, "https://azurewebsite"))
      {
        HttpResponseMessage response = await _httpClient.SendAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
      }
    }

    [Fact]
    public async Task SecurtiyPutHardware()
    {

      HttpResponseMessage response = await _httpClient.PutAsJsonAsync("https://azurewebsite", new { });

      response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

    }


    [Fact]
    public async Task SecurtiyPutPersonalDetails()
    {

      var body =
          new PersonalDetailsModel()
          {
            Address = new Address() { LineOne = "line one", Zip = "000", City = "City" },
            Bank = new Bank() { Clearing = "000", Account = "000" },
            Phone = "0700000",
          };

      HttpResponseMessage response = await _httpClient.PutAsJsonAsync("https://azurewebsite", body);

      response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

    }

  }

}