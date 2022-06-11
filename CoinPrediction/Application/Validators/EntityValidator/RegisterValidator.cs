using Entities.User;
using Shared.Validators;
using FluentValidation;

namespace Application.Validators.EntityValidator
{
    public partial class RegisterValidator : BaseValidator<RegisterEntity>
    {
        public RegisterValidator()
        {
            RuleFor(r => r.UserName).Must(IsNotNullOrNotWhiteSpace).WithMessage("Kullanıcı adı boş bırakılamaz.");
            RuleFor(r => r.Password).Must(IsNotNullOrNotWhiteSpace).WithMessage("Şifre boş bırakılamaz.");
            RuleFor(r => r.Email).Must(IsNotNullOrNotWhiteSpace).WithMessage("Şifre boş bırakılamaz.").EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
        }
    }
}
