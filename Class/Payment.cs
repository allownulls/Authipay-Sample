using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Web;

namespace AIBPayment
{
    public static class Payment
    {
        public async static Task<string> SendTransaction(string url, TxModel model)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("chargetotal", model.ChargeTotal);
            parameters.Add("currency", model.Currency);
            parameters.Add("hash", model.Hash);
            parameters.Add("hash_algorithm", model.HashAlgorithm);
            parameters.Add("mode", model.Mode);
            parameters.Add("oid", model.OId);
            parameters.Add("paymentMethod", model.PaymentMethod);
            parameters.Add("responseFailURL", model.ResponseFailUrl);
            parameters.Add("responseSuccessURL", model.ResponseSuccessUrl);
            parameters.Add("sharedSecret", model.SharedSecret);
            parameters.Add("storename", model.StoreName);
            parameters.Add("timezone", model.Timezone);
            parameters.Add("txndatetime", model.TxnDatetime);
            parameters.Add("txntype", model.TxnType);

            var data = new FormUrlEncodedContent(parameters);

            using var client = new HttpClient();
                var response = await client.PostAsync(url, data);

            return await response.Content.ReadAsStringAsync();
        }

        public static string CreateHash(TxModel model) => GetHash(ToHexStringSimple(model.GetSourceHashString()));

        public static string GetHash(string source)
        {
            using (System.Security.Cryptography.HashAlgorithm sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(source));
                return string.Concat(bytes.Select(x => x.ToString("x2")));
            }
        }

        public static string ToHexStringSimple(string utf8String) => string.Concat(Encoding.UTF8.GetBytes(utf8String)
                                                                                         .Select(x => x.ToString("x2")));
    }
}
