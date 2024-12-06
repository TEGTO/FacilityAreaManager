namespace FacilityAreaManagerApi
{
    public static class Configuration
    {
        public static string DATABASE_CONNECTION_STRING { get; } = "FacilityContractsDb";
        public static string USE_CORS { get; } = "UseCORS";
        public static string ALLOWED_CORS_ORIGINS { get; } = "AllowedCORSOrigins";
        public static string API_KEY { get; } = "ApiKey";
        public static string EF_CREATE_DATABASE { get; } = "EFCreateDatabase";
    }
}
