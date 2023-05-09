using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace MailLibrary;
public class Producer : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public Producer(string hostname, string username, string password)
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
    }

    public void Send(MailObject mailObject)
    {
        if (mailObject == null)
        {
            throw new ArgumentNullException(nameof(mailObject));
        }
        if (mailObject.SmtpConfiguration == null)
        {
            throw new ArgumentNullException(nameof(mailObject.SmtpConfiguration));
        }
        if (mailObject.To.Count == 0)
        {
            throw new System.ComponentModel.WarningException("No recipients specified");
        }
        var mailObjectJson = JsonSerializer.Serialize(mailObject);
        var body = Encoding.UTF8.GetBytes(mailObjectJson);

        _channel.BasicPublish(exchange: "", routingKey: "email_queue", basicProperties: null, body: body);
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}
