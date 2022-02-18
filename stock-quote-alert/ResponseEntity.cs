using System.Text.Json.Serialization;

namespace stock_quote_alert; 

public class ResponseEntity {
    
    [JsonPropertyName("status")]
    private bool status;

    [JsonPropertyName("code")] 
    private int code;

    [JsonPropertyName("msg")] 
    private string message;

    [JsonPropertyName("response")] 
    private Dictionary<string, string>[] response;

    public ResponseEntity(bool status, int code, string message, Dictionary<string, string>[] response) {
        this.status = status;
        this.code = code;
        this.message = message;
        this.response = response;
    }


    public bool Status {
        get => status;
        set => status = value;
    }

    public int Code {
        get => code;
        set => code = value;
    }

    public string Message {
        get => message;
        set => message = value;
    }

    public Dictionary<string, string>[] Response {
        get => response;
        set => response = value;
    }
}