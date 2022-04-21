
using System;
using System.ComponentModel.DataAnnotations;

namespace OnboardingBackend.Models
{
    public class Github
    {
        public string Username { get; set; }

    }
    public class Address
    {
        [Required]
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public string City { get; set; }
        public string Region { get; set; }
    }

    public class Bank
    {
        [Required]
        public string Clearing { get; set; }
        [Required]
        public string Account { get; set; }
    }
    public class PersonalDetailsModel
    {

        public Guid Id { get; set; }
        [Required]
        
        public string Phone { get; set; }

        [Required]
        public Address Address { get; set; }

        [Required]
        public Bank Bank { get; set; }

        public string Allergies { get; set; }

        public string OtherFoodPref { get; set; }

        public string GithubUsername { get; set; }
        
        public DateTime Created { get; set; }
    }
}