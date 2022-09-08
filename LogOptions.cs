namespace RefitLogCalls
{
    public class LogOptions
    {
        private const string LOG_REQUEST_TEXT_DEFAULT = " ********** REFIT LOG REQUEST ";

        private const string LOG_RESPONSE_TEXT_DEFAULT = " ********** REFIT LOG RESPONSE ";

        public LogModeOptions LogMode { get; set; } = LogModeOptions.Both;

        public bool IncludeLogTextSeparators { get; set; } = true;

        public bool IncludeLogIds { get; set; } = true;

        public string LogRequestSeparatorText { get; set; } = LOG_REQUEST_TEXT_DEFAULT;

        public string LogResponseSeparatorText { get; set; } = LOG_RESPONSE_TEXT_DEFAULT;
    }

    public enum LogModeOptions { Request, Response, Both }
}