using log4net;
using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;

namespace SED.Services
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {                
                Console.WriteLine("Web server on {0} starting.", baseAddress);
                DemoClientRequest(baseAddress);
                Console.ReadKey();
                Console.WriteLine("Web server on {0} stopping.", baseAddress);               
            }

            Console.ReadLine(); 
        }

        private static void DemoClientRequest(string baseAddress)
        {
            // Create HttpCient and make a request to api/SportEvents 
            HttpClient client = new HttpClient();
            var response = client.GetAsync(baseAddress + "api/SportEvents/GetUpcomming").Result;
            log.Debug(response);
            Console.WriteLine(response);
            log.Debug(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        }
    }
}
