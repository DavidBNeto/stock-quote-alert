using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;

namespace stock_quote_alert;

public class Consultant {
    
    private const string Key = "F0c7R4uKDqHXotBMzHlNZnAuS";

    private static HttpClient Client = new HttpClient();

    private readonly string url;

    private readonly string asset;

    private readonly double floor;

    private readonly double ceil;

    public Consultant(string asset, double ceil, double floor) {
        this.asset = asset.ToUpper();
        this.ceil = ceil;
        this.floor = floor;
        url = FormatString();
    }

    public async Task<AssetOperation> Consult() {
        try {
            while (true) {
                HttpResponseMessage responseMessage = await Client.GetAsync(url);
                ResponseEntity? response = responseMessage.Content.ReadFromJsonAsync<ResponseEntity>().Result;
                if (response is not null && response.Code == 200) {
                    double current = Convert.ToDouble(response.Response[0]["c"]);
                    if (current > ceil) {
                        return AssetOperation.Sell;
                    }
                    if (current < floor) {
                        return AssetOperation.Buy;
                    }
                } else {
                    return AssetOperation.InvalidAsset;
                }
                Thread.Sleep(1800000);
            }
        } catch (Exception e) {
            Console.WriteLine(e); 
            return AssetOperation.Error;
        }
    }

    private string FormatString() {
        return new StringBuilder().Append("https://fcsapi.com/api-v3/stock/latest")
            .Append("?symbol=")
            .Append(asset)
            .Append("&access_key=")
            .Append(Key)
            .Append("&exchange=BM%26FBovespa")
            .ToString();
    }

    //debugging purposes
    private string ToString() {
        return new StringBuilder().Append("url: ")
            .Append(url)
            .Append("\nasset: ")
            .Append(asset)
            .Append("\nroof: ")
            .Append(floor)
            .Append("\nceil: ")
            .Append(ceil)
            .ToString();
    }
}