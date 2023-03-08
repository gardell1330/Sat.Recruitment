using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Models
{
    public class UserRequestModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; } 
        public string UserType { get; set; } 
        public string Money { get; set; }
    }
}