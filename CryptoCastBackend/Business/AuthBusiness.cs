using System.Data;
using System.Net.Mail;
using Application.Api;
using Application.Domain;
using Application.Interfaces.Business;
using Application.Interfaces.Data;
using Application.Mapping;
using Data.Context;
using Entities.User;
using Shared.Entities.Common;
using Shared.Extentions;
using Shared.Utilities;

namespace Business
{
    public class AuthBusiness : IAuthBusiness
    {
        #region Fields
        private readonly IRepository<User> _userRepository;
        private readonly IDbContext _context;
        private readonly byte _activeStatus = Enums.TableStatus.Active.ToByte();
        #endregion

        #region Ctor
        public AuthBusiness(IRepository<User> userRepository, IDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }
        #endregion

        #region Methods

        public async Task<ServiceResponse<CurrentUserEntity>> Login(LoginEntity request)
        {
            return await Task.Run(() =>
            {
                using var tx = _context.BeginTransaction(IsolationLevel.ReadUncommitted).Result;
                try
                {
                    User userControl;
                    if (MailAddress.TryCreate(request.UserName, out var mailAddress))
                        userControl = _userRepository.GetListNoTracking().Where(x => x.Email == request.UserName.Trim() && x.Status == _activeStatus).AsEnumerable().FirstOrDefault();
                    else
                        userControl = _userRepository.GetListNoTracking().Where(x => x.UserName == request.UserName.Trim() && x.Status == _activeStatus).AsEnumerable().FirstOrDefault();

                    if (userControl.IsNull())
                    {
                        string? message = GetTranslationMessage("UserNotFound");
                        return new ServiceResponse<CurrentUserEntity>(Constants.UserNotFoundCode, message ?? "Kayıtlı kullanıcı bulunamadı.");
                    }

                    var passwordEncrypt = Encriptions.EncryptText(request.Password);

                    var user = _userRepository.GetListNoTracking().Where(x => (x.Email == request.UserName.Trim() || x.UserName == request.UserName) && x.Password == passwordEncrypt && x.Status == _activeStatus).AsEnumerable().FirstOrDefault();
                    if (user.IsNull())
                    {
                        string? message = GetTranslationMessage("FailLogin");
                        return new ServiceResponse<CurrentUserEntity>(Constants.ErrorCode, message ?? "Kullanıcı e-posta adresi veya şifre yanlış, lütfen tekrar deneyiniz.");
                    }

                    string? succesMessage = GetTranslationMessage("TransactionSuccessfulMessage");
                    return new ServiceResponse<CurrentUserEntity>(user.CurrentUserToModel(), succesMessage);
                }
                catch (Exception ex)
                {
                    string? message = GetTranslationMessage("ExceptionErrorMessage");
                    return new ServiceResponse<CurrentUserEntity>(Constants.ErrorCode, message ?? Constants.ExceptionErrorMessage);
                }
            });
        }

        public async Task<ServiceResponse<CurrentUserEntity>> Register(RegisterEntity request)
        {
            return await Task.Run(() =>
            {
                using var tx = _context.BeginTransaction(IsolationLevel.ReadUncommitted).Result;
                {
                    try
                    {
                        var userControl = _userRepository.GetListNoTracking().Where(x => (x.Email == request.Email.Trim() || x.UserName == request.UserName.Trim()) && x.Status == _activeStatus).AsEnumerable().FirstOrDefault();
                        if (userControl.IsNotNull())
                        {
                            if (request.UserName.Trim() == userControl.UserName)
                            {
                                string? message = GetTranslationMessage("RegisteredUsernameMessage");
                                return new ServiceResponse<CurrentUserEntity>(Constants.UserNotFoundCode, message ?? "Girilen kullanıcı adı ile kayıtlı kullanıcı bulunmaktadır. Başka bir kullanıcı adı girerek tekrar deneyiniz.");
                            }
                            else if (request.Email.Trim() == userControl.Email)
                            {
                                string? message = GetTranslationMessage("RegisteredEmailMessage");
                                return new ServiceResponse<CurrentUserEntity>(Constants.UserNotFoundCode, message ?? "Girilen E-Posta adresi ile kayıtlı kullanıcı bulunmaktadır. Başka bir e-posta adresi girerek tekrar deneyiniz.");
                            }

                        }

                        if (request.Password.Contains(request.UserName))
                        {
                            string? message = GetTranslationMessage("UsernameInYourPasswordMessage");
                            return new ServiceResponse<CurrentUserEntity>(Constants.ErrorCode, message ?? "Şifrenizin içinde kullanıcı adı bilgisi olmamalıdır.");
                        }

                        var passwordEncrypt = Encriptions.EncryptText(request.Password);
                        var user = request.ToModel();
                        var dateTimeNow = DateTime.UtcNow;
                        user.Password = passwordEncrypt;
                        user.RoleId = Enums.UserRole.Customer.ToInt();

                        if (user.LanguageCode.IsNullOrWhiteSpace())
                            user.LanguageCode = Constants.TRLanguage;

                        user.CreateDate = dateTimeNow;
                        user.CreateUser = default;
                        user.Status = _activeStatus;
                        var insertUser = _userRepository.InsertT(user).Result;


                        tx.CommitAsync();

                        string? succesMessage = GetTranslationMessage("TransactionSuccessfulMessage");
                        return new ServiceResponse<CurrentUserEntity>(insertUser.CurrentUserToModel(), succesMessage);

                    }
                    catch (Exception ex)
                    {
                        tx.RollbackAsync();
                        string? message = GetTranslationMessage("ExceptionErrorMessage");
                        return new ServiceResponse<CurrentUserEntity>(Constants.ErrorCode, message ?? Constants.ExceptionErrorMessage);
                    }
                }
            });
        }

        private string? GetTranslationMessage(string key)
        {
            string? message = null;
            string languageCode = ProjectConfiguration.CurrentUser.LanguageCode ?? Constants.TRLanguage;
            //var serviceResponse = _settingsApiIntegration.GetTranslations().Result;
            //if (serviceResponse.Result.ResultCode == Constants.SuccessCode)
            //    message = serviceResponse.ReturnObject.FirstOrDefault(x => x.Code == key && x.Culture == languageCode)?.Value;
            return message;
        }

        #endregion
    }
}
