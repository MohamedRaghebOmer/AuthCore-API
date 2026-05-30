namespace AuthCore_API
{
    public static class Constants
    {
        public const string SecretKey = "ThisIsASecretKeyForJwtTokenGeneration";
        public const string Issuer = "API";
        public const string Audience = "APIAudience";
        public const int AccessTokenDurationInMinutes = 15;
        public const int RefreshTokenDurationInDays = 7;
    }
}
