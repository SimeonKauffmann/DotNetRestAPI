using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using OnboardingBackend.Models;
using OnboardingBackend.Services;


namespace OnboardingBackend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UserController : ControllerBase
  {
    static readonly HttpClient Client = new();

    private readonly UserService _userService;

    public UserController(UserService userService) =>
        _userService = userService;

    [HttpGet]
    public async Task<IActionResult> Get()

    {
      string auth = Request.Headers.Authorization;
      string office = Request.Headers["office"];
      string id = Request.Query["id"];

      if (auth is null)
      {
        SlackErrorMessage.SlackError(auth, office, id, "User");
        return Unauthorized();
      }

      try
      {
        using (var request = new HttpRequestMessage(HttpMethod.Get, "https://oauth2.secure.com"))
        {
          request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", auth.Split(' ')[1]);
          request.Headers.Add("office", office);
          var response = await Client.SendAsync(request);

          if (!response.IsSuccessStatusCode)
          {
            SlackErrorMessage.SlackError(auth, office, id, "User");
            return Unauthorized();
          }
        }
      }
      catch (HttpRequestException e)
      {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
      }

      var user = await _userService.GetAsync(id);
      return CreatedAtAction(nameof(Get), user);

    }

    [HttpPost]


    public async Task<IActionResult> Post(User newUser)
    {

      string auth = Request.Headers.Authorization;
      string office = Request.Headers["office"];
      string id = Request.Query["id"];

      try
      {
        using (var request = new HttpRequestMessage(HttpMethod.Get, "https://oauth2.secure.com"))
        {
          request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", auth.Split(' ')[1]);
          request.Headers.Add("office", office);
          var response = await Client.SendAsync(request);

          if (!response.IsSuccessStatusCode)
          {
            SlackErrorMessage.SlackError(auth, office, id, "User");
            return Unauthorized();
          }
        }
      }
      catch (HttpRequestException e)
      {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
      }

      await _userService.CreateAsync(newUser);
      return CreatedAtAction(nameof(Post), newUser);
    }

  }
}
