namespace SampleApp.Services;

public interface IEmailSender
{
    void Send(string to, string body);
}
