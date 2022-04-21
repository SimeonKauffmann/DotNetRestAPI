using System;
using System.Net;
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
    public class HealthController : ControllerBase
    {

        [HttpGet]
        public HttpStatusCode HealthCheck ()
        {
           return HttpStatusCode.OK;
        }
    }
}