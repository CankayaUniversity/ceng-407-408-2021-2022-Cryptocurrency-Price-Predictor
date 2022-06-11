using Shared.Caching;
using Shared.Entities.Common;
using Shared.Extentions;
using FluentValidation;

namespace Shared.Validators
{
    public abstract class BaseValidator<TModel> : AbstractValidator<TModel> where TModel : class
    {
        #region Ctor

        protected BaseValidator()
        {
            PostInitialize();
        }

        #endregion

        #region Utilities

        protected virtual void PostInitialize()
        {

        }

        #endregion

        #region Extentions

        protected bool IsNotNullOrNotWhiteSpace(string str1)
        {
            var temp1 = !string.IsNullOrWhiteSpace(str1) ? str1.Trim() : null;
            return !string.IsNullOrWhiteSpace(temp1);
        }

        protected bool IsNotNull(object objectToCall)
        {
            return !objectToCall.IsNull();
        }

        protected bool IsNotNullOrEmpty<T>(IEnumerable<T> value)
        {
            return !value.IsNullOrEmpty();
        }

        #endregion
    }
}
