namespace Business.Domain.Services;

public interface ILogService
{
    void Write<T>(T? log) where T : class;
}