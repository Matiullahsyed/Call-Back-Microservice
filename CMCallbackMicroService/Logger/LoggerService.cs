namespace CallbackMicroService.Logger
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;
        private readonly IConfiguration _configuration;

        public LoggerService(ILogger<LoggerService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void LogInformation(params object[] logParams)
        {
            if (_configuration.GetValue<string>("EnableLog:Information") == "1")
            {
                _logger.LogInformation(string.Join(", ", logParams));
            }
        }
        public void LogDebug(params object[] logParams)
        {
            if (_configuration.GetValue<string>("EnableLog:Debug") == "1")
            {
                _logger.LogDebug(string.Join(", ", logParams));
            }
        }
        public void LogError(Exception ex, params object[] logParams)
        {
            if (_configuration.GetValue<string>("EnableLog:Error") == "1")
            {
                _logger.LogError(ex, string.Join(", ", logParams));
            }
        }
    }
}
