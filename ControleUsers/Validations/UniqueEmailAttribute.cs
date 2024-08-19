using ControlerUsers.Data;
using System.ComponentModel.DataAnnotations;

namespace ControlerUsers.Validations
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) {
                return ValidationResult.Success;
            }   

            var dbContext = validationContext.GetService(serviceType: typeof(UserContext)) as UserContext;
            var email = value.ToString();

            return dbContext.Users.Any(u => u.Email == email)
                ? new ValidationResult("Email já cadastrado no banco de dados")
                : ValidationResult.Success;
        }
    }
}
