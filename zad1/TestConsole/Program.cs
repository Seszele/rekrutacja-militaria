using MailLibrary;

Console.WriteLine("Hello, World!");
// Replace these values with your RabbitMQ server and email server credentials
string rabbitMqHostname = "localhost";
string rabbitMqUsername = "guest";
string rabbitMqPassword = "guest";
string smtpServer = "localhost";
int smtpPort = 1025;
string smtpUsername = "your_email@example.com";
string smtpPassword = "your_password";
bool useSsl = false;

// Create a Producer instance
var producer = new Producer(rabbitMqHostname, rabbitMqUsername, rabbitMqPassword);

// Create a Consumer instance
var consumer = new Consumer(rabbitMqHostname, rabbitMqUsername, rabbitMqPassword);

consumer.Start();
// Prepare a test email
var mailObject = new MailObject
{
    From = smtpUsername,
    To = { "recipient@example.com" },
    Subject = "Test email from RabbitMQ",
    Body = "<h1>Hello, world!</h1><p>This is a test email sent using RabbitMQ and MailKit.</p>",
    IsHtml = true,
    SmtpConfiguration = new SmtpConfiguration
    {
        Server = smtpServer,
        Port = smtpPort,
        UseSsl = useSsl,
        Username = smtpUsername,
        Password = smtpPassword,
    },
};

// Send the test email using the Producer
producer.Send(mailObject);

Console.WriteLine("Test email sent. Press any key to exit...");
Console.ReadKey();

consumer.Stop();

// Dispose of the Producer and Consumer instances
producer.Dispose();
consumer.Dispose();