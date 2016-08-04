using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace TestTabs
{
    [DataContract]
    public class Trade
    {
        public static List<Trade> trades = new List<Trade>();
        [DataMember]
        public int trade_id { get; set; }
        public DateTime Timestamp { get; set; }
        [DataMember]
        public string isin { get; set; }
        [DataMember]
        public string buySell { get; set; }
        [DataMember]
        public int day { get; set; }
        [DataMember]
        public int month { get; set; }
        [DataMember]
        public int year { get; set; }
        [DataMember]
        public int hour { get; set; }
        [DataMember]
        public int minutes { get; set; }
        [DataMember]
        public int seconds { get; set; }
        [DataMember]
        public int noOfBonds { get; set; }
        [DataMember]
        public double price { get; set; }
        public double? askPrice { get; set; }
        public double? bidPrice { get; set; }
        [DataMember]
        public string tradeStatus { get; set; }

        public void setPrices()
        {
            if(buySell == "buy")
            {
                bidPrice = price;
                askPrice = null;
            }
            else
            {
                bidPrice = null;
                askPrice = price;
            }
        }

        public void FormatDate()
        {
            Timestamp = new DateTime(year, month, day, hour, minutes, seconds);
        }
    }
}
