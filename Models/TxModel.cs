using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIBPayment
{
    public class TxModel
    {
        public string Url { get; set; }
        public string ResponseSuccessUrl { get; set; }
        public string ResponseFailUrl { get; set; }
        public string TxnType { get; set; }                                                   
        public string Timezone { get; set; }
        public string TxnDatetime { get; set; }
        public string HashAlgorithm { get; set; }
        public string Hash { get; set; }
        public string StoreName { get; set; }
        public string Mode { get; set; }
        public string OId { get; set; }
        public string ChargeTotal { get; set; }
        public string Currency { get; set; }
        public string PaymentMethod { get; set; }
        public string SharedSecret { get; set; }

        public string GetSourceHashString() => StoreName + TxnDatetime + ChargeTotal + Currency + SharedSecret;


    }
}
