
using System.Net;
using Slack.Webhooks;

namespace OnboardingBackend.Services;

public class SlackErrorMessage
{
  public static void SlackError(string auth, string office, string user, string endpoint)
  {
    if (user != "PipelineTest")
    {
      var slackClient =
          new SlackClient("https://hooks.slack.com/services/someslack/somewhere");

      string hasAuth() => auth == null ? "No" : "Yes";

      var slackMessage = new SlackMessage
      {
        Username = "ERROR: Unauthorized",
        Text =
              $"AuthToken: {hasAuth()}, Office: {office}, Id: {user}, At: {endpoint}"
      };

      slackClient.Post(slackMessage);
    }
  }
}