using Microsoft.Extensions.Configuration;

namespace stock_quote_alert; 

public class AppConfig {

    public AppConfig() {
        IConfigurationSection configuration = new ConfigurationBuilder()
            .SetBasePath(@"C:\Users\david.bertrand.iway\RiderProjects\stock-quote-alert\stock-quote-alert\")
            .AddJsonFile("appsettings.json", false, true)
            .Build()
            .GetSection("App");
        ApiKey = configuration["ApiKey"]!;
        TimeSpan = (TimeSpanBetweenRequests)Convert.ToInt32(configuration["Timespan"]);
    }

    public string ApiKey { get; }
    public TimeSpanBetweenRequests TimeSpan { get; }

}