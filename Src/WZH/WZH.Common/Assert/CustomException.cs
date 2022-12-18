namespace WZH.Common.Assert
{
    /// <summary>
    /// 自定义异常
    /// </summary>
    public class CustomException : Exception
    {
        public HttpStatusType HttpStatusCode { get; private set; }

        public CustomException(HttpStatusType httpStatusCode, string message) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}