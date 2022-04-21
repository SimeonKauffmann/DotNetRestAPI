using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace OnboardingBackend.Models
{
    public class IceContactModel
    {
        
        public ObjectId Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Relation { get; set; }

        [Required]
        public string Phone { get; set; }

        public string Name2 { get; set; }

        public string Relation2 { get; set; }

        public string Phone2 { get; set; }
        
    }
}
