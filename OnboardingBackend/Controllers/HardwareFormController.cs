using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnboardingBackend.Models;
using OnboardingBackend.Services;
//using Slack.Webhooks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using SlackAPI;

namespace OnboardingBackend.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class HardwareFormController : ControllerBase
  {
    static readonly HttpClient Client = new();



    private readonly UserService _userService;

    public HardwareFormController(UserService userService)
    {
      _userService = userService;
    }


    [HttpPut]
    public async Task<IActionResult> Put(HardwareFormModel newHardware)
    {
      string auth = Request.Headers.Authorization;
      string office = Request.Headers["office"];
      var id = Request.Query["id"];

      if (auth is null)
      {
        SlackErrorMessage.SlackError(auth, office, id, "Hardware");
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
            SlackErrorMessage.SlackError(auth, office, id, "Hardware");
            return Unauthorized();
          }
        }
      }
      catch (HttpRequestException e)
      {
        Console.WriteLine("\nException Caught!");
        Console.WriteLine("Message :{0} ", e.Message);
      }


      newHardware.Created = DateTime.Now;

      var response = await _userService.UpdateHardwareAsync(id, newHardware);

      var user = await _userService.GetAsync(id);


      string Computer() => newHardware.Computer.Length > 0 ? newHardware.Computer : "None";
      string Phone() => newHardware.Phone.Length > 0 ? newHardware.Phone : "None";
      string Number() => newHardware.CurrentNumber.Length > 0 ? newHardware.CurrentNumber : "New Number";
      string ComputerColor() => newHardware.Computer == "PC" ? newHardware.PCModel : newHardware.ComputerColor;
      string Mouse() => newHardware.Mouse.Length > 0 ? newHardware.Mouse : "None";
      string Headphones() => newHardware.Headphones.Length > 0 ? newHardware.Headphones : "None";
      string Keyboard() => newHardware.Keyboard.Length > 0 ? newHardware.Keyboard : "None";
      string Clothing() => newHardware.ClothingSize.Length > 0 ? newHardware.ClothingSize : "Not Given";
      Console.WriteLine(newHardware.PCModel);

      // Slack 
      const string TOKEN = "secret";

      var slackClient = new SlackTaskClient(TOKEN);

      var slackMessage = $"Hi! {user.Name} would like to request the following hardware.";

      var slackAttachment = new IBlock[]
      {
                new Block()
                {
                    type = "section",
                    text = new Text() {text = slackMessage}
                },
                new Block()
                {
                    type = "section",
                    text = new Text() {text = $"Phone: {Phone()}"}
                },
                new Block()
                {
                type = "section",
                text = new Text() {text = $"Phone Number: {Number()}"}
                },
                new Block()
                {
                    type = "section",
                    text = new Text() {text = $"Computer: {Computer()}, {ComputerColor()}"}
                },
                new Block()
                {
                    type = "section",
                    text = new Text() {text = $"Mouse/Trackpad: {Mouse()}"}
                },
                new Block()
                {
                    type = "section",
                    text = new Text() {text = $"Headphones: {Headphones()}"}
                },
                new Block()
                {
                    type = "section",
                    text = new Text() {text = $"Keyboard: {Keyboard()}"}
                },
                new Block()
                {
                    type = "section",
                    text = new Text() {text = $"Clothing Size: {Clothing()}"}
                }
      };







      if (newHardware.Phone != "RESET")
      {
        var slackresponse = await slackClient.PostMessageAsync("#hardware-requests", slackMessage,
            "New Hardware Request", null, false, slackAttachment);

        Console.WriteLine(slackresponse.error);
      }

      return CreatedAtAction(nameof(Put), response);

    }
  }
}
