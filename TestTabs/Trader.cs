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
    public class Trader
    {
        [DataMember]
        public string traderId { get; set; }
        [DataMember]
        public string password { get; set; }

        public Trader(string id, string traderPassword)
        {
            traderId = id;
            password = traderPassword;
        }

        public override string ToString()
        {
            return traderId;
        }
    }
}
