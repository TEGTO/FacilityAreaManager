namespace FacilityAreaManagerApi
{
    public static class Configuration
    {
        public static string DATABASE_CONNECTION_STRING { get; } = "FacilityContractsDb";
        public static string USE_CORS { get; } = "UseCORS";
        public static string ALLOWED_CORS_ORIGINS { get; } = "AllowedCORSOrigins";
    }
}
