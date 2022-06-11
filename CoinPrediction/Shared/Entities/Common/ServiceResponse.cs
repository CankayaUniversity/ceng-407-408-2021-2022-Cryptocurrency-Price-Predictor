
namespace Shared.Entities.Common
{
    public class ServiceResponse<T>
    {
        private const string SuccessMessage = "İşlem Başarılı";
        private const int ErrorCode = -1;
        private const int SuccessCode = 0;
        public ServiceResult Result { get; set; }
        public T ReturnObject { get; set; }
        public int TotalCount { get; set; }
        public ServiceResponse()
        {
            this.Result = new ServiceResult(SuccessCode, SuccessMessage);
            this.TotalCount = 0;
        }

        public ServiceResponse(string? message = null)
        {
            this.Result = new ServiceResult(SuccessCode, message ?? SuccessMessage);
            this.TotalCount = 0;
        }
        
        public ServiceResponse(Exception ex)
        {
            if (ex != null)
            {
                string message = string.Empty;
                message = message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += string.Format("Message: {0}", ex.Message);
                message += Environment.NewLine;
                message += string.Format("StackTrace: {0}", ex.StackTrace);
                message += Environment.NewLine;
                message += string.Format("Source: {0}", ex.Source);
                message += Environment.NewLine;
                message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                message += string.Format("Inner Exception: {0}", ex.InnerException?.Message.ToString());
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                this.Result = new ServiceResult(ErrorCode, message);
            }
        }
        public ServiceResponse(T returnObject, string? message,int totalCount=0)
        {
            this.ReturnObject = returnObject;
            this.Result = new ServiceResult(SuccessCode, message ?? SuccessMessage);
            this.TotalCount = totalCount;
        }

        public ServiceResponse(T returnObject,int totalCount = 0)
        {
            this.ReturnObject = returnObject;
            this.Result = new ServiceResult(SuccessCode, SuccessMessage);
            this.TotalCount = totalCount;
        }

        public ServiceResponse(int resultCode, string resultMessage,int totalCount=0)
        {
            this.Result = new ServiceResult(resultCode, resultMessage);
            this.TotalCount = totalCount;
        }

    }

    [Serializable]
    public class ServiceResponse
    {
        private const string SuccessMessage = "İşlem Başarılı";
        private const int ErrorCode = -1;
        private const int SuccessCode = 0;
        public ServiceResult Result { get; set; }
        public int TotalCount { get; set; }

        public ServiceResponse()
        {
            this.Result = new ServiceResult(SuccessCode, SuccessMessage);
            this.TotalCount = 0;
        }

        public ServiceResponse(string? message = null)
        {
            this.Result = new ServiceResult(SuccessCode, message ?? SuccessMessage);
            this.TotalCount = 0;
        }
        public ServiceResponse(Exception ex)
        {
            if (ex != null)
            {
                string message = string.Empty;
                message = message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += string.Format("Message: {0}", ex.Message);
                message += Environment.NewLine;
                message += string.Format("StackTrace: {0}", ex.StackTrace);
                message += Environment.NewLine;
                message += string.Format("Source: {0}", ex.Source);
                message += Environment.NewLine;
                message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                message += string.Format("Inner Exception: {0}", ex.InnerException?.Message.ToString());
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                this.Result = new ServiceResult(ErrorCode, message);
            }
        }
        public ServiceResponse(int resultCode, string resultMessage, int totalCount = 0)
        {
            this.Result = new ServiceResult(resultCode, resultMessage);
            this.TotalCount = totalCount;
        }
    }


}
