using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PaymentSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static string MakeUrlWithQuery(string baseUrl, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl));

            if (parameters == null || parameters.Count() == 0)
                return baseUrl;

            return parameters.Aggregate(baseUrl,
                (accumulated, kvp) => string.Format($"{accumulated}{kvp.Key}={kvp.Value}&"));
        }

        public async Task<IActionResult> TestPayment()
        {
            //var parameters = new Dictionary<string, string>
            //{
            //    ["key"] = "AIzaSyCnTuwwBHuw3P6k0uXINIeexg5L9aic_a8", ////ConfigurationManager.AppSettings["APIKey"],
            //    ["part"] = "statistics",
            //    ["id"] = videos[i].Videoid
            //};
            //var baseUrl = "https://testoauth.homebank.kz/epay2/oauth2/token?";
            //var fullUrl = MakeUrlWithQuery(baseUrl, parameters);

            //HttpClientHandler clientHandler = new HttpClientHandler();
            //clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            //HttpClient client = new HttpClient(clientHandler);
            //client.BaseAddress = new Uri(fullUrl.Substring(0, fullUrl.Length - 1));
            //client.DefaultRequestHeaders
            //      .Accept
            //      .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header


            //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            //client.DefaultRequestHeaders.ConnectionClose = true;

            //var result = await client.PostAsync(fullUrl.Substring(0, fullUrl.Length - 1),);

            var values = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "scope", "payment" },
                { "client_id", "test" },
                { "client_secret", "yF587AV9Ms94qN2QShFzVR3vFnWkhjbAK3sG" },
                { "invoiceID", "000000001" },
                { "amount", "100" },
                { "currency", "KZT" },
                { "terminal", "67e34d63-102f-4bd1-898e-370781d0074d" },
                { "postLink", "" },
                { "failurePostLink", "" }
            };

            var content = new FormUrlEncodedContent(values);
            HttpClient client = new HttpClient();
            var response = await client.PostAsync("https://testoauth.homebank.kz/epay2/oauth2/token", content);

            var responseString = await response.Content.ReadAsStringAsync();
            ViewBag.Response = responseString;
            ViewBag.Invoice = "000000001";
            ViewBag.Amount = "100";

            return View();
        }

        public IActionResult Fail()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Failure()
        {
            return View();
        }
    }
}
