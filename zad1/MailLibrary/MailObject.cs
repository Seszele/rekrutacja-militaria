namespace MailLibrary;
using System.Collections.Generic;

public class MailObject
{
    public string From { get; set; } = "";
    public List<string> To { get; set; } = new List<string>();
    public List<string>? Cc { get; set; }
    public List<string>? Bcc { get; set; }
    public string Subject { get; set; } = "";
    public string Body { get; set; } = "";
    public bool IsHtml { get; set; }
    public SmtpConfiguration? SmtpConfiguration { get; set; }
}

public class SmtpConfiguration
{
    public string Server { get; set; } = "";
    public int Port { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool UseSsl { get; set; }
}
