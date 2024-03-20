namespace CallbackMicroService.Logger
{
    public interface ILoggerService
    {
        void LogInformation(params object[] logParams);
        void LogDebug(params object[] logParams);
        void LogError(Exception ex, params object[] logParams);
    }
}
