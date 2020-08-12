namespace MovieWeb.WebApi.Common
{
    using System.Linq;
    using System.Threading.Tasks;

    public static class StringExtension
    {
        public static string ToSnakeCase(this string source)
        {
            return string.Concat(source.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }

        public static async Task<string> ToSnakeCaseAsync(this Task<string> source)
        {
            return string.Concat((await source).Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
    }
}
