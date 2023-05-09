namespace MailLibrary.Exceptions;
public class MailObjectException : Exception
{
    public MailObjectException(string message) : base(message)
    {
    }
}

public class SmtpConfigurationException : Exception
{
    public SmtpConfigurationException(string message) : base(message)
    {
    }
}