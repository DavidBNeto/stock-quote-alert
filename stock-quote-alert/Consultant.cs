using System.Net.Http.Json;
using System.Text;

namespace stock_quote_alert;

public class Consultant {
    
    private readonly string key; //"F0c7R4uKDqHXotBMzHlNZnAuS";

    private readonly HttpClient client = new HttpClient();

    private readonly string url;

    private readonly string asset;

    private readonly double floor;

    private readonly double ceil;

    public Consultant(string asset, string ceil, string floor, string key) {
        this.key = key;
        this.asset = asset.ToUpper();
        this.ceil = Convert.ToDouble(ceil.Replace('.',','));
        this.floor = Convert.ToDouble(floor.Replace('.',','));
        url = FormatUrlString();
    }

    public async Task<AssetOperation> Consult(TimeSpanBetweenRequests timeSpan) {
        try {
            while (isBusinessHours()) {
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                ResponseEntity? response = responseMessage.Content.ReadFromJsonAsync<ResponseEntity>().Result;
                if (response is not null && response.Code == 200) {
                    double current = Convert.ToDouble(response.Response[0]["c"].Replace('.', ','));
                    Console.Write(new StringBuilder()
                        .Append("Valor atual do ativo ")
                        .Append(response.Response[0]["s"])
                        .Append(": ")
                        .Append(current)
                        .Append(" --> Orientação: ")
                        .ToString()
                    );
                    if (current > ceil) {
                        Console.Write("Vender\n");
                        return AssetOperation.Sell;
                    }
                    if (current < floor) {
                        Console.Write("Comprar\n");
                        return AssetOperation.Buy;
                    }
                    Console.Write("Esperar \n");
                } else {
                    return AssetOperation.InvalidAsset;
                }
                Console.Write("Esperando atualização do valor do ativo...\n");
                Thread.Sleep((int)timeSpan * 60 * 1000);
            }
        } catch (Exception e) {
            Console.WriteLine(e); 
            return AssetOperation.Error;
        }
        return AssetOperation.Error;
    }

    private string FormatUrlString() {
        return new StringBuilder().Append("https://fcsapi.com/api-v3/stock/latest")
            .Append("?symbol=")
            .Append(asset)
            .Append("&access_key=")
            .Append(key)
            .Append("&exchange=BM%26FBovespa")
            .ToString();
    }
    
    //debugging purposes
    public override string ToString() {
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

    private bool isBusinessHours() {
        int currHour = DateTime.Now.Hour;
        int currMinutes = DateTime.Now.Minute;
        if ((currHour == 9 && currMinutes > 29) ||
            (currHour > 9 && currHour < 18)) {
            return true;
        } else {
            Thread.Sleep(((24 - currHour) + 9) * 60 * 60 * 1000);
            return true;
        }
    }
}