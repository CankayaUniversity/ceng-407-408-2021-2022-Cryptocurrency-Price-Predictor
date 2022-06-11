using Entities.User;
using Shared.Entities.Common;

namespace Application.Interfaces.Business
{
    public interface IAuthBusiness
    {
        public Task<ServiceResponse<CurrentUserEntity>> Login(LoginEntity request);

        public Task<ServiceResponse<CurrentUserEntity>> Register(RegisterEntity request);

    }
}
