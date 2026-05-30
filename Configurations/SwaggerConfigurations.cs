using Microsoft.OpenApi;

namespace AuthCore_API.Configurations
{
    public static class SwaggerConfigurations
    {
        public static IServiceCollection AddSwaggerConfigurations(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AuthCore-API v1",
                    Version = "v1",
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT Bearer token"
                });

                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecuritySchemeReference("Bearer", document),
                        new List<string>() // FIXED: Changed Array.Empty<string>() to new List<string>()
                    }
                });
            });

            return services;
        }
    }
}
