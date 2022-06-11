using System.Text;

namespace Shared.Extentions
{
    public static class ErrorHelper
    {
        public static string GetExceptionString(Exception exception)
        {
            string completeException;
            if (exception.InnerException != null && exception.InnerException.Message.Contains("Envelope"))
                completeException = exception.InnerException.ToString("");
            else
                completeException = exception.ToString("");
            return completeException;
        }
        public static string ToString(this Exception exception, string prefix, string suffix = "")
        {
            if (exception == null)
                return string.Empty;
            var builder = new StringBuilder();
            builder.AppendLine(prefix);
            try
            {
                builder.AppendLineFormat("Exception: {0}", exception.Message);
                builder.AppendLineFormat("Exception Type: {0}", exception.GetType().FullName);
                foreach (var data in exception.Data)
                    builder.AppendLineFormat("Data: {0}:{1}", data, exception.Data[data]);
                builder.AppendLineFormat("StackTrace: {0}", exception.StackTrace);
                builder.AppendLineFormat("Source: {0}", exception.Source);
                builder.AppendLineFormat("TargetSite: {0}", exception.TargetSite);
                builder.Append(exception.InnerException.ToString(prefix, suffix));
            }
            catch { }
            builder.AppendLine(suffix);
            return builder.ToString();
        }
    }
}
