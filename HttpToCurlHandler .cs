using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RefitLogCalls
{
    internal class HttpToCurlHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid().ToString();

            await RequestLogger.Log(request, id);

            var response = await base.SendAsync(request, cancellationToken);

            await ResponseLogger.Log(response, id);

            return response;
        }
    }
}
