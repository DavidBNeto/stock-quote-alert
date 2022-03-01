namespace stock_quote_alert; 

public class SystemClock: IClock {
    DateTime IClock.Now { get { return DateTime.Now; } }
}