using DatabaseControl;
using ExceptionHandling;
using FacilityAreaManagerApi;
using FacilityAreaManagerApi.BackgroundServices;
using FacilityAreaManagerApi.Infrastructure.Data;
using FacilityAreaManagerApi.Infrastructure.Repositories;
using FacilityAreaManagerApi.Infrastructure.Validators;
using FacilityAreaManagerApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

#region DB

builder.Services.AddDbContextFactory<FacilityAreaManagerDbContext>(builder.Configuration.GetConnectionString(Configuration.DATABASE_CONNECTION_STRING)!, "FacilityAreaManagerApi");
builder.Services.AddRepositoryWithResilience<FacilityAreaManagerDbContext>(builder.Configuration);

#endregion

#region Cors

bool.TryParse(builder.Configuration[Configuration.USE_CORS], out bool useCors);
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

if (useCors)
{
    builder.Services.AddApplicationCors(builder.Configuration, myAllowSpecificOrigins, builder.Environment.IsDevelopment());
}

#endregion

#region Project Services

builder.Services.AddSingleton<IFacilityAreaManagerRepository, FacilityAreaManagerRepository>();
builder.Services.AddSingleton<IContractProcessingBackgroundService, ContractProcessingBackgroundService>();

#endregion

builder.Services.ConfigureCustomInvalidModelStateResponseControllers();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Facility Area Manager API",
        Version = "v1",
        Description = "API documentation for Facility Area Manager."
    });

    options.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "X-Api-Key",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Description = "API Key authentication using the X-Api-Key header.",
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddSharedFluentValidation(typeof(AddEquipmentPlacementContractRequestValidator));

builder.Services.AddMediatR(conf =>
{
    conf.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddHostedService<ContractProcessingBackgroundService>();

var app = builder.Build();

if (app.Configuration[Configuration.EF_CREATE_DATABASE] == "true")
{
    await app.ConfigureDatabaseAsync<FacilityAreaManagerDbContext>(CancellationToken.None);
}

if (useCors)
{
    app.UseCors(myAllowSpecificOrigins);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseExceptionMiddleware();
app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

public partial class Program { }