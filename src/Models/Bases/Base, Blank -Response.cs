namespace MovieWeb.WebApi.Model
{
     public class BaseResponse
    {
        public bool IsSuccessful { get; set; }

        public object Message { get; set; }

        public int HttpStatusCode { get; set; }
        
        public string MessageDetail { get; } = "For more information see the log file.";
    }

    public class BlankResponse
    {

    }
}
