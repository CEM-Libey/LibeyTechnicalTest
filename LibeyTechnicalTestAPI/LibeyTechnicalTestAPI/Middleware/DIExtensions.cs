using LibeyTechnicalTestDomain.LibeyUserAggregate.Application;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure;
namespace LibeyTechnicalTestAPI.Middleware
{
    public static class DIExtensions
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services)
        {
            services.AddTransient<ILibeyUserAggregate, LibeyUserAggregate>();
            services.AddTransient<ILibeyUserRepository, LibeyUserRepository>();

            services.AddTransient<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddTransient<IDocumentTypeAggregate, DocumentTypeAggregate>();

            services.AddTransient<IUbigeoAggregate, UbigeoAggregate>();
            services.AddTransient<IUbigeoRepository, UbigeoRepository>();


            return services;
        }
    }
}
