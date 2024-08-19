using System.ComponentModel.DataAnnotations;

namespace ControleUsers.DTOs
{
    public class UserPost
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty!;
        [Range(18, 120)]
        public int Idade { get; set; } = default!;
    }
}
