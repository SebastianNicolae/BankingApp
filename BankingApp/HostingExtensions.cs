using BankingApp.Domain.TransactionDomain;
using BankingApp.Domain.TransactionValidators;
using BankingApp.Extensions.ApiResponse;
using BankingApp.Extensions.Middlewares;
using BankingApp.Models.DatabaseModels;
using BankingApp.Swagger;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BankingApp
{
    internal static class HostingExtensions
    {

        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IBankingService,BankingService>();
            builder.Services.AddScoped<IBANValidator>();
            builder.Services.AddScoped<AvailableFundsValidator>();
            builder.Services.AddScoped<IValidationsManager>(sp=> new ValidationsManager(
                new List<ITransactionValidator>
                {
                    sp.GetService<IBANValidator>(),
                    sp.GetService<AvailableFundsValidator>()
                }));
            return builder;
        }

        public static WebApplicationBuilder ConfigureConfigurationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IResponseFactory, ResponseFactory>();
            return builder;
        }

        public static WebApplicationBuilder ConfigureSwaggerServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Swagger", xmlFile);

                c.EnableAnnotations();
            });

            builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

            return builder;
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            
            app.MapControllers();
            app.UseSwagger();
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });

            return app;
        }

        public static WebApplication SeedDataBase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var context = scope.ServiceProvider.GetService<BankingAppDbContext>();

            context.Account.Add(new Account
            {
                Id = "1231-2121",
                Name = "William",
                Balance = 12,
                Created = DateTime.UtcNow,
                Iban = "RO49AAAA1B31007593840000"
            });

            context.Account.Add(new Account
            {
                Id = "2212-1212",
                Name = "John",
                Balance = 15,
                Created = DateTime.UtcNow,
                Iban = "12SAD-212AS-12SDA-ASD321"
            });

            context.SaveChanges();

            return app;
        }

        public static WebApplicationBuilder ConfigureDataBaseServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<BankingAppDbContext>(options =>
            options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("BankingAppDb")));

            return builder;
        }

        public static WebApplicationBuilder ConfigureApiVersioningServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("x-api-version"),
                    new MediaTypeApiVersionReader("x-api-version"));
            });

            builder.Services.AddEndpointsApiExplorer();

            // Add ApiExplorer to discover versions
            builder.Services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            return builder;
        }
    }
}
