namespace Shared.Entities.Common
{
    [Serializable]
    public class ServiceResult
    {
        public int ResultCode { get; set; }
        public string ResultMessage { get; set; }

        public ServiceResult()
        {
            
        }
        public ServiceResult(int resultCode, string resultMessage)
        {
            this.ResultCode = resultCode;
            this.ResultMessage = resultMessage;
        }
    }
}
