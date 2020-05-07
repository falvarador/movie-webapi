namespace MovieWeb.WebApi.Common
{    
    using System.Threading.Tasks;

    public static class IntegerExtension
    {
        public static bool ToBoolean(this int request)
        {
            return request > 0 ? true : false;
        }

        public static async Task<bool> ToBooleanAsync(this Task<int> request)
        {
            return await request > 0 ? true : false;
        }
    }
}
