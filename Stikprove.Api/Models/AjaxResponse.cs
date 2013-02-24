namespace Stikprove.Api.Models
{
    public class AjaxResponse
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        static public AjaxResponse Create(string statusCode, string message)
        {
            return new AjaxResponse()
            {
                Message = message,
                StatusCode = statusCode
            };
        }

        static public AjaxResponse Create(string statusCode, string message, object data)
        {
            return new AjaxResponse()
            {
                Message = message,
                StatusCode = statusCode,
                Data = data
            };
        }
    }
}