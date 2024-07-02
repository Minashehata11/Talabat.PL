using System.ComponentModel.DataAnnotations;

namespace Talabate.PL.Dtos
{
    public class ReqgisterOrLoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
    }
}
