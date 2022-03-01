namespace stock_quote_alert; 

public interface IClock {
    DateTime Now { get; } 
}