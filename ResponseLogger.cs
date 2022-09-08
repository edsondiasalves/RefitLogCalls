using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RefitLogCalls
{
    internal static class ResponseLogger
    {
        public static async Task Log(HttpResponseMessage Response, string responseId)
        {
            if (LogConfiguration.Options.LogMode != LogModeOptions.Both && LogConfiguration.Options.LogMode != LogModeOptions.Response)
            {
                return;
            }

            LogResponseSeparator(responseId, "START");

            await LogResponseContent(Response);

            LogResponseSeparator(responseId, "END");
        }

        private static async Task LogResponseContent(HttpResponseMessage response)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Status code: {response.StatusCode}");

            if (response.Content != null)
            {
                var content = await response.Content.ReadAsStringAsync();
                sb.AppendLine($"Body: {content}");
            }

            Debug.WriteLine(sb.ToString());
        }

        private static void LogResponseSeparator(string responseId, string startOrEnd)
        {
            if (LogConfiguration.Options.IncludeLogTextSeparators)
            {
                var sb = new StringBuilder();

                sb.Append(LogConfiguration.Options.LogResponseSeparatorText);

                if (LogConfiguration.Options.IncludeLogIds)
                {
                    sb.Append($"- {responseId}");
                }

                sb.Append($" - {startOrEnd} ********");
                sb.AppendLine(string.Empty);
                Debug.WriteLine(sb.ToString());
            }
        }
    }
}