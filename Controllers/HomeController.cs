using AIBPayment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AIBPayment;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Cors;

namespace AIBPayment.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        dynamic config;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            ReadConfig();
        }

        public IActionResult Payment()
        {

            return View();
        }

        public IActionResult ClientPayment()
        {
            TxModel model = new TxModel();

            model.Url = (string)config.url;

            model.ResponseFailUrl = (string)config.responseFailUrl;
            model.ResponseSuccessUrl = (string)config.responseSuccessUrl;
            model.TxnType = (string)config.txntype;
            model.Timezone = (string)config.timezone;
            model.TxnDatetime = DateTime.Now.ToString("yyyy:MM:dd-HH:mm:ss");
            model.HashAlgorithm = (string)config.hashalgorithm;
            model.Mode = (string)config.mode;
            model.OId = "OrderNo";
            model.StoreName = (string)config.storename;
            model.Currency = (string)config.currency;
            model.PaymentMethod = "M";
            model.ChargeTotal = "13.00";
            model.SharedSecret = (string)config.sharedSecret;

            return View(model);
        }

        [HttpPost]
        public IActionResult Send([FromBody] TxModel model)
        {
            model.Url = (string)config.url;

            model.ResponseFailUrl = Url.Action("Fail", "Home", null, Request.Scheme);
            model.ResponseSuccessUrl = Url.Action("Success", "Home", null, Request.Scheme);
            model.TxnType = (string)config.txntype;
            model.Timezone = (string)config.timezone;
            model.SharedSecret = (string)config.sharedSecret;
            model.HashAlgorithm = (string) config.hashalgorithm;
            model.Mode = (string) config.mode;
            model.OId = "OrderNo";
            model.StoreName = (string) config.storename;
            model.Currency = (string) config.currency;
            model.PaymentMethod = "M";

            if (string.IsNullOrEmpty(model.ChargeTotal))
                model.ChargeTotal = "13.00";
            
            model.TxnDatetime = DateTime.Now.ToString("yyyy:MM:dd-HH:mm:ss");
            model.Hash = AIBPayment.Payment.CreateHash(model);
                       
            var json = JsonConvert.SerializeObject(model);

            return new JsonResult(model);
        }

        public IActionResult Fail()
        {
            return View();  
        }

        public IActionResult Success()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void ReadConfig()
        {
            string json;

            using (StreamReader r = new StreamReader("aib.json"))
            {
                json = r.ReadToEnd();
            }

            config = Newtonsoft.Json.Linq.JObject.Parse(json);
        }
    }
}
