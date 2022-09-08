using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RefitLogCalls
{
    internal static class RequestLogger
    {
        public static async Task Log(HttpRequestMessage request, string requestId)
        {
            if (LogConfiguration.Options.LogMode != LogModeOptions.Both && LogConfiguration.Options.LogMode != LogModeOptions.Request)
            {
                return;
            }

            LogRequestSeparator(requestId, "START");

            await LogRequestContent(request);

            LogRequestSeparator(requestId, "END");
        }

        private static async Task LogRequestContent(HttpRequestMessage request)
        {
            var method = request.Method.Method;
            var url = request.RequestUri?.ToString();
            var sb = new StringBuilder();

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

            Debug.WriteLine(sb.ToString());
        }

        private static void LogRequestSeparator(string requestId, string startOrEnd)
        {
            if (LogConfiguration.Options.IncludeLogTextSeparators)
            {
                var sb = new StringBuilder();

                sb.Append(LogConfiguration.Options.LogRequestSeparatorText);

                if (LogConfiguration.Options.IncludeLogIds)
                {
                    sb.Append($"- {requestId}");
                }

                sb.Append($" - {startOrEnd} ********");
                sb.AppendLine(string.Empty);
                Debug.WriteLine(sb.ToString());
            }
        }
    }
}