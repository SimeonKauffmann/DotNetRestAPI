using System;

namespace OnboardingBackend.Models
{
    public class HardwareFormModel
    {
        public string _id { get; set; }
        public DateTime Created { get; set; }
        public string Phone { get; set; }
        public string Headphones { get; set; }
        public string Keyboard { get; set; }
        public string Mouse { get; set; }
        public string Computer { get; set; }
        public string ComputerColor { get; set; }
        public string CurrentNumber { get; set; }
        
        public string NewNumber { get; set; }
        public string PCModel { get; set; }
        
        public string PhoneNumber { get; set; }
        public string ClothingSize { get; set; }


    }
}
