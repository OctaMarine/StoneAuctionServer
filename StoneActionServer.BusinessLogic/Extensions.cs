using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoneActionServer.BusinessLogic.Services;

namespace StoneActionServer.BusinessLogic;

public static class Extensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection,IConfiguration configuration)
    {
        serviceCollection.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));
        serviceCollection.AddScoped<JwtService>();
        serviceCollection.AddScoped<IAuthService,AuthService>();
        return serviceCollection;
    }
}