namespace STW.SubmissionApi.Presentation.Services;

public interface IMessagingService
{
    Task SendMessageAsync(string message);
}
