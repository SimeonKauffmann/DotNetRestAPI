using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using OnboardingBackend.Controllers;
using OnboardingBackend.Models;


namespace OnboardingBackend.Models
{
    public class Step
    {
        public string StepName { get; set; }

        public bool Completed { get; set; }
    }

    
    public class User
    {
        public ObjectId _id { get; set; }
        
        [Required] public DateTime Created { get; set; }
        [Required] public string UserId { get; set; }

        [Required] public string Name { get; set; }
        
        [Required] public List<Step> CompletedSteps { get; set; }

        public IceContactModel IceContacts { get; set; }

        public HardwareFormModel RequestedHardware { get; set; }

        public PersonalDetailsModel Data { get; set; }
        
        public string GithubUsername { get; set; }
    }
}

