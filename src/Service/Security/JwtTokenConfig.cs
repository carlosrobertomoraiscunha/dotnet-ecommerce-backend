namespace EcommerceAPI.Service.Security
{
    public class JwtTokenConfig
    {
        public static string Secret { get; set; }
        public static int AccessTokenExpiration { get; set; }
    }
}
