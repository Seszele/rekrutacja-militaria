using System.Text;
using System.Text.Json;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using MailLibrary.Utils;

namespace MailLibrary;
public class Consumer : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly EventingBasicConsumer _consumer;
    private string? _consumerTag;

    public Consumer(string hostname, string username, string password)
    {
        var factory = new ConnectionFactory()
        {
            HostName = hostname,
            UserName = username,
            Password = password,
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "email_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

        _consumer = new EventingBasicConsumer(_channel);
        _consumer.Received += OnReceived;
    }

    public void Start()
    {
        _consumerTag = _channel.BasicConsume(queue: "email_queue", autoAck: true, consumer: _consumer);
    }

    public void Stop()
    {
        if (!string.IsNullOrEmpty(_consumerTag))
        {
            _channel.BasicCancel(_consumerTag);
            _consumerTag = null;
        }
    }

    private void OnReceived(object? sender, BasicDeliverEventArgs e)
    {
        var body = e.Body.ToArray();
        var mailObjectJson = Encoding.UTF8.GetString(body);
        var mailObject = JsonSerializer.Deserialize<MailObject>(mailObjectJson);

        if (mailObject != null && mailObject.SmtpConfiguration != null)
        {
            SendEmail(mailObject);
        }
    }

    private void SendEmail(MailObject mailObject)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(mailObject.From.GetNameFromEmail(), mailObject.From));
        message.To.AddRange(mailObject.To.ConvertAll(x => new MailboxAddress(x.GetNameFromEmail(), x)));
        if (mailObject.Cc != null)
        {
            message.Cc.AddRange(mailObject.Cc.ConvertAll(x => new MailboxAddress(x.GetNameFromEmail(), x)));
        }
        if (mailObject.Bcc != null)
        {
            message.Bcc.AddRange(mailObject.Bcc.ConvertAll(x => new MailboxAddress(x.GetNameFromEmail(), x)));
        }
        message.Subject = mailObject.Subject ?? "";

        message.Body = new TextPart(mailObject.IsHtml ? "html" : "plain")
        {
            Text = mailObject.Body ?? ""
        };

        var smtpConfig = mailObject.SmtpConfiguration;
        using (var client = new SmtpClient())
        {
            if (smtpConfig == null)
            {
                Console.WriteLine("SMTP configuration is null");
                return;
            }
            try
            {
                client.Connect(smtpConfig.Server, smtpConfig.Port, smtpConfig.UseSsl ? SecureSocketOptions.Auto : SecureSocketOptions.None);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error connecting to SMTP server: {ex.Message}");
                return;
            }

            if (!string.IsNullOrEmpty(smtpConfig.Username) && !string.IsNullOrEmpty(smtpConfig.Password))
            {
                client.Authenticate(smtpConfig.Username, smtpConfig.Password);
            }
            client.Send(message);
            client.Disconnect(true);
        }
    }

    public void Dispose()
    {
        Stop();
        _channel?.Close();
        _connection?.Close();
    }
}
