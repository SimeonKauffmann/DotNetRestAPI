using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnboardingBackend.Models;
using OnboardingBackend.Services;
using System.Net.Http;
using System.Net.Http.Headers;



namespace OnboardingBackend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CompletedStepsController : ControllerBase
  {
    private readonly UserService _userService;
    static readonly HttpClient Client = new();

    public CompletedStepsController(UserService userService) =>
        _userService = userService;



    [HttpPut]
    public async Task<IActionResult> Put(Step stepName)
    {
      string auth = Request.Headers.Authorization;
      string office = Request.Headers["office"];
      string id = Request.Query["id"];


      if (auth is null)
      {
        SlackErrorMessage.SlackError(auth, office, id, "CompletedSteps");
        return Unauthorized();
      }


      try
      {
        using (var request = new HttpRequestMessage(HttpMethod.Get, "https://oauth2.secure.com"))
        {
          request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", auth.Split(' ')[1]);
          request.Headers.Add("office", office);
          var toolsResponse = await Client.SendAsync(request);

          if (!toolsResponse.IsSuccessStatusCode)
          {
            SlackErrorMessage.SlackError(auth, office, id, "CompletedSteps");
            return Unauthorized();
          }
        }
      }
      catch (HttpRequestException e)
      {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
      }



      var response = await _userService.UpdateStepsAsync(id, stepName);

      return CreatedAtAction(nameof(Put), response);
    }

  }
}

