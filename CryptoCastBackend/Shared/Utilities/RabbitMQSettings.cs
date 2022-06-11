using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities
{
    public static class RabbitMQSettings
    {
        public const string Auth_Settings_ForgotPasswordMailSendEventQueue = "auth-settings-forgotPasswordMailSend-queue";

        public const string Settings_Auth_ForgotPasswordMailSendCompletedEventQueue = "settings-auth-forgotPasswordMailSendCompleted-queue";

        public const string Auth_Settings_EmailVerificationMailSendEventQueue = "auth-settings-emailVerificationMailSend-queue";

        public const string Settings_Auth_EmailVerificationMailSendCompletedEventQueue = "settings-auth-emailVerificationMailSendCompleted-queue";
    }
}
