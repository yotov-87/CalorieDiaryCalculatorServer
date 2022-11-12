using System.ComponentModel.DataAnnotations;

namespace CalorieDiaryCalculator.Server.Features.Identity.Models
{
    public class RegisterRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
