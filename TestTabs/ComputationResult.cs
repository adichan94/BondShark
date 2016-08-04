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
    public class ComputationResult
    {
        [DataMember]
        public double desiredYield { get; set; }
        [DataMember]
        public int quantity { get; set; }
        [DataMember]
        public double cleanPrice { get; set; }
        [DataMember]
        public double dirtyPrice { get; set; }
        [DataMember]
        public double settlementAmount { get; set; }
        [DataMember] 
        public double accruedAmount { get; set; }
    }
}
