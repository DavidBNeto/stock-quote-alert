using System;
using stock_quote_alert;

namespace stock_quote_alert_test; 

public class StaticClock: IClock {
    DateTime IClock.Now { get { return new DateTime(2022, 02, 1, 10, 6, 13); } }
}