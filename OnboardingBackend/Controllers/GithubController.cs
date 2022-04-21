using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnboardingBackend.Models;
using OnboardingBackend.Services;
using Slack.Webhooks;
using System.Net.Http;
using System.Net.Http.Headers;
using SlackAPI;


namespace OnboardingBackend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class GithubController : ControllerBase
  {
    private readonly UserService _userService;

    static readonly HttpClient Client = new();

    public GithubController(UserService userService) =>
        _userService = userService;



    [HttpPut]
    public async Task<IActionResult> Put(Github githubUser)
    {
      string auth = Request.Headers.Authorization;
      string office = Request.Headers["office"];
      var id = Request.Query["id"];

      if (auth is null)
      {
        SlackErrorMessage.SlackError(auth, office, id, "Github");
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
            SlackErrorMessage.SlackError(auth, office, id, "Github");
            return Unauthorized();
          }
        }
      }
      catch (HttpRequestException e)
      {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
      }


      var response = await _userService.UpdateGithubAsync(id, githubUser);

      var user = await _userService.GetAsync(id);

      const string TOKEN = "secret";
      var slackClient = new SlackTaskClient(TOKEN);

      var slackresponse = await slackClient.PostMessageAsync("#github-invitations", $"Hi! Can you add {user.Name} as a collaborator on Github. The username is {githubUser.Username}");


      return CreatedAtAction(nameof(Put), response);
    }

  }
}