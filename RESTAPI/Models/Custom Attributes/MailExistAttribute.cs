using System.ComponentModel.DataAnnotations;

namespace Models.CustomAttribute
{
    public class MailExistAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var owner = validationContext.ObjectInstance as OwnerData;
            //if (owner == null) return new ValidationResult("Model is empty");
            //DbContext db = new DbContext();
            //var user = db.Users.FirstOrDefault(u => u.email == (string)value && u.id != owner.Id);

            if ( null == null)
                return ValidationResult.Success;
            else
                return new ValidationResult("Mail already exists");
        }
    }
}