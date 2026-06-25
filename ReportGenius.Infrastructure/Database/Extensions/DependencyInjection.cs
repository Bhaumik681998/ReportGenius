using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportGenius.Application.Interfaces;
using ReportGenius.Infrastructure.AI;
using ReportGenius.Infrastructure.AI.Clients;
using ReportGenius.Infrastructure.AI.Schema;
using ReportGenius.Infrastructure.AI.Search;
using ReportGenius.Infrastructure.Configurations;
using ReportGenius.Infrastructure.Database.Connection;
using ReportGenius.Infrastructure.Repositories;

namespace ReportGenius.Infrastructure.Database.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Database Options
            services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.SectionName));

            // AI Options
            services.Configure<AIOptions>(configuration.GetSection(AIOptions.SectionName));

            // Cache Options
            services.Configure<CacheOptions>(configuration.GetSection(CacheOptions.SectionName));

            // JWT Options
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

            // CORS Options
            services.Configure<CorsOptions>(configuration.GetSection(CorsOptions.SectionName));

            // Swagger Options
            services.Configure<SwaggerOptions>(configuration.GetSection(SwaggerOptions.SectionName));

            // File Upload Options
            services.Configure<FileUploadOptions>(configuration.GetSection(FileUploadOptions.SectionName));

            // Logging Options
            services.Configure<LoggingOptions>(configuration.GetSection(LoggingOptions.SectionName));

            // Database Connection Factory
            services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

            // Generic Repository
            services.AddScoped<IGenericRepository, GenericRepository>();

            // AI Service
            services.AddScoped<IAIService, AIService>();

            // Ollama Client
            services.AddHttpClient<IOllamaClient, OllamaClient>();

            // SQL Execution Repository
            services.AddScoped<ISqlExecutionRepository, SqlExecutionRepository>();

            // Database Schema Service
            services.AddScoped<IDatabaseSchemaProvider, DatabaseSchemaProvider>();

            // Table Finder
            services.AddScoped<ITableFinder, TableFinder>();

            // Table Selector
            services.AddScoped<IAITableSelector, AITableSelector>();

            return services;
        }
    }
}
