namespace Adfenix.Services.Interface
{
    public interface ILogService
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(Exception ex, string message);
    }
}
