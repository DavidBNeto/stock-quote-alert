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
        Holidays = new string[13] {
            "2022-01-01",
            "2022-02-28",
            "2022-03-01",
            "2022-03-02",
            "2022-03-03",
            "2022-04-15",
            "2022-04-21",
            "2022-06-16",
            "2022-09-07",
            "2022-10-12",
            "2022-11-02",
            "2022-11-15",
            "2022-12-30",
        };
    }

    public string ApiKey { get; }
    public TimeSpanBetweenRequests TimeSpan { get; }

    public string[] Holidays { get; }

}