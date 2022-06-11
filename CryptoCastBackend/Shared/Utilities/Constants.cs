namespace Shared.Utilities
{
    public class Constants
    {
        #region ResultCodes
        public static readonly int ServiceErrorCode = -3;
        public static readonly int ValidationErrorCode = -2;
        public static readonly int ErrorCode = -1;
        public static readonly int SuccessCode = 0;

        #endregion ResultCodes

        #region LoginResultCode
        public static readonly int UserNotFoundCode = -4;
        #endregion

        #region ErrorMessages
        public static readonly string EmptyErrorMessage = "Kayıt Bulunamadı.";
        public static readonly string EmptyRequestErrorMessage = "Request alanları boş bırakılamaz.";   
        public static readonly string ExceptionErrorMessage = "Beklenmedik bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";

        #endregion

        #region HttpClientServiceNames

        public static readonly string HtppClientSendGridServiceName = "SendGridService";
        public static readonly string HtppClientWalletServiceName = "WalletService";
        public static readonly string HtppClientSettingsApiServiceName = "SettingsApiService";
        public static readonly string HtppClientSettingsApiNoTokenServiceName = "SettingsApiNoTokenService";
        public static readonly string HtppClientPayzee = "HtppClientPayzee";
        public static readonly string HtppClientPayzeePayment = "HtppClientPayzeePayment";
        public static readonly string HtppClientAuthServiceName = "AuthService";
        public static readonly string HtppClientGatewayApiServiceName = "GatwayUrl";

        #endregion

        #region CultureCode

        public static readonly string TRLanguage = "tr-TR";
        public static readonly string ENLanguage = "en-US";

        #endregion

        #region PlatformCode

        public static readonly string AndroidPlatformCode = "android";
        public static readonly string IosPlatformCode = "ios";

        #endregion


        public static readonly string SetReadUncommited = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED";
        public static readonly string SetReadCommited = "SET TRANSACTION ISOLATION LEVEL READ COMMITTED";
        public static readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


    }
}
