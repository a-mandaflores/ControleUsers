using ControlerUsers.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControlerUsers.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty!;

        [Column("email")]
        [Required]
        [EmailAddress]
        [UniqueEmail]
        public string Email { get; set; } = string.Empty!;
        [Column("idade")]
        [Range(18, 120)]
        public int Idade { get; set; }
    }

    
}
