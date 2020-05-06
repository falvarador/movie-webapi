namespace MovieWeb.WebApi.Common
{
    public static class ContentTypes
    {
        public const string Json = "application/json";
        public const string Xml = "application/xml";
    }

    public static class Headers
    {
        public const string Pagination = "X-Pagination";
    }

    public static class HttpMethods
    {
        public const string All = "*";
        public const string Delete = "DELETE";
        public const string Get = "GET";
        public const string Head = "HEAD";
        public const string Options = "OPTIONS";
        public const string Patch = "PATCH";
        public const string Post = "POST";
        public const string Put = "PUT";
        public const string Methods = "GET, POST, PUT, PATCH, DELETE";
    }

    public class Versions
    {
        public const string v1 = "1";
        public const string v2 = "2";
        public const string v3 = "3";
        public const string v4 = "4";
        public const string v5 = "5";
        public const string v6 = "6";
        public const string v7 = "7";
        public const string v8 = "8";
        public const string v9 = "9";
    }

    public class Compression 
    {
        public const string ContentEncodingHeader = "Content-Encoding";
        public const string ContentEncodingGzip = "gzip";
        public const string ContentEncodingDeflate = "deflate";
    }
}
