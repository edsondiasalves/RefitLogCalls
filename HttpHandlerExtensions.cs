using Microsoft.Extensions.DependencyInjection;

namespace refit_log_calls
{
    public static class HttpHandlerExtension
    {
        public static IHttpClientBuilder LogCurl(this IHttpClientBuilder builder)
        {
            builder.AddHttpMessageHandler<HttpToCurlHandler>();
            return builder;
        }

        public static IServiceCollection RegisterLogCurl(this IServiceCollection services)
        {
            services.AddTransient<HttpToCurlHandler>();
            return services;
        }
    }
}