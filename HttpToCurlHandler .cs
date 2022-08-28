using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace refit_log_calls
{
    public class HttpToCurlHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestId = Guid.NewGuid().ToString();

            await LogRequest(request, requestId);
            var response = await base.SendAsync(request, cancellationToken);
            await LogResponse(response, requestId);

            return response;
        }

        private async Task<StringBuilder> LogRequest(HttpRequestMessage request, string requestId)
        {
            var method = request.Method.Method;
            var url = request.RequestUri?.ToString();

            var sb = new StringBuilder();

            sb.AppendLine($"********************** REFIT LOG REQUEST - {requestId} - START **********************");
            sb.AppendLine($"curl --location --request {method} '{url}'");

            foreach (var header in request.Headers)
            {
                sb.AppendLine($"--header '{header.Key}: {String.Join(",", header.Value)}' ");
            }

            if (request.Content != null)
            {
                var content = await request.Content.ReadAsStringAsync();
                sb.AppendLine($"--header 'Content-Type: application/json' ");
                sb.AppendLine($"--data-raw '{content}'");
            }

            sb.AppendLine($"********************** REFIT LOG REQUEST - {requestId} - END **********************");

            Debug.WriteLine(sb.ToString());

            return sb;
        }

        private async Task<StringBuilder> LogResponse(HttpResponseMessage response, string requestId)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"********************** REFIT LOG RESPONSE - {requestId} - START **********************");

            sb.AppendLine($"Status code: {response.StatusCode}");

            if (response.Content != null)
            {
                var content = await response.Content.ReadAsStringAsync();
                sb.AppendLine($"Body: {content}");
            }

            sb.AppendLine($"********************** REFIT LOG RESPONSE - {requestId} - END **********************");

            Debug.WriteLine(sb.ToString());
            return sb;
        }
    }
}
