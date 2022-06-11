using Application.Interfaces.Business;
using Entities.User;
using Authorization.Authorization;
using Shared.Entities.Common;
using Shared.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        #region Fields

        private IAuthBusiness _authBusiness;

        #endregion

        #region Ctor

        public AuthController(IAuthBusiness authBusiness)
        {
            _authBusiness = authBusiness;
        }

        #endregion

        #region Methods

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginEntity request)
        {
            var tokenModel = _authBusiness.Login(request).Result;

            if (tokenModel.Result.ResultCode != Constants.SuccessCode)
                return Ok(new ServiceResponse<UserTokenEntity>(tokenModel.Result.ResultCode, tokenModel.Result.ResultMessage));

            var tokenResult = JwtToken.GetToken(tokenModel.ReturnObject);
            return Ok(new ServiceResponse<UserTokenEntity>(tokenResult));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public IActionResult Register([FromBody] RegisterEntity request)
        {
            var tokenModel = _authBusiness.Register(request).Result;

            if (tokenModel.Result.ResultCode != Constants.SuccessCode)
                return Ok(new ServiceResponse<UserTokenEntity>(tokenModel.Result.ResultCode, tokenModel.Result.ResultMessage));

            var tokenResult = JwtToken.GetToken(tokenModel.ReturnObject);
            return Ok(new ServiceResponse<UserTokenEntity>(tokenResult));
        }

        #endregion
    }
}
