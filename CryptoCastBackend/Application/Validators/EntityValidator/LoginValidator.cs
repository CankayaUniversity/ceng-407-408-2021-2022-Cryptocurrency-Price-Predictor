using Entities.User;
using Shared.Validators;
using FluentValidation;

namespace Application.Validators.EntityValidator
{
    public partial class LoginValidator: BaseValidator<LoginEntity>
    {
        public LoginValidator()
        {
            RuleFor(r => r.UserName).Must(IsNotNullOrNotWhiteSpace).WithMessage("Kullanıcı adı boş bırakılamaz.");
            RuleFor(r => r.Password).Must(IsNotNullOrNotWhiteSpace).WithMessage("Şifre boş bırakılamaz.");
        }
    }
}
