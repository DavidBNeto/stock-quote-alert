using Microsoft.Extensions.Configuration;

namespace stock_quote_alert; 

public class EmailConfig {

    public EmailConfig() {
        IConfigurationSection configuration = new ConfigurationBuilder()
            .SetBasePath(@"C:\Users\david.bertrand.iway\RiderProjects\stock-quote-alert\stock-quote-alert\")
            .AddJsonFile("appsettings.json", false, true)
            .Build()
            .GetSection("Email");
        Smtp = configuration["Smtp"]!;
        SenderEmail = configuration["SenderEmail"]!;
        SenderName = configuration["SenderName"]!;
        Password = configuration["Password"]!;
        RecipientEmail = configuration["RecipientEmail"]!;
        RecipientName = configuration["RecipientName"]!;
        Port = Convert.ToInt16(configuration["Port"]);
    }

    public string Smtp { get; }
    public string SenderEmail { get; }
    public string SenderName { get; }
    public string Password { get; }
    public string RecipientEmail { get; }
    public string RecipientName { get; }
    public int Port { get; }
    
    
}