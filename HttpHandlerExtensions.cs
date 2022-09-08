using Microsoft.Extensions.DependencyInjection;

namespace RefitLogCalls
{
    public static class HttpHandlerExtension
    {
        public static IHttpClientBuilder LogCurl(this IHttpClientBuilder builder)
        {
            builder.AddHttpMessageHandler<HttpToCurlHandler>();
            return builder;
        }

        public static IServiceCollection RegisterLogCurl(this IServiceCollection services, LogOptions logOptions = null)
        {
            LogConfiguration.Options = logOptions ?? new LogOptions();
            services.AddTransient<HttpToCurlHandler>();
            return services;
        }
    }
}