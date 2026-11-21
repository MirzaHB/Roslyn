using System;

namespace SampleApp.Services;

public class SmtpEmailSender : IEmailSender
{
    public void Send(string to, string body)
    {
        Console.WriteLine($"To {to}: {body}");
    }
}
