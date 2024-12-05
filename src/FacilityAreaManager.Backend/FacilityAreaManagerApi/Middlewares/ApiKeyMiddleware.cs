namespace FacilityAreaManagerApi.Middlewares
{
    public class ApiKeyMiddleware
    {
        private const string API_KEY_HEADER_NAME = "X-Api-Key";

        private readonly RequestDelegate next;
        private readonly string validApiKey;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this.next = next;
            validApiKey = configuration[Configuration.API_KEY] ?? throw new InvalidOperationException("API Key is not configured.");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var extractedApiKey) || extractedApiKey != validApiKey)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Invalid API Key.");
                return;
            }

            await next(context);
        }
    }
}
