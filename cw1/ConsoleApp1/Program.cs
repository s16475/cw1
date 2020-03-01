using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentNullException("No argument!");
            }
            else
            {
                var emails = await GetEmails(args[0]);

                foreach (var a in args)
                {
                    Console.WriteLine(a);
                }
                foreach (var email in emails)
                {
                    Console.WriteLine(email);
                }
            }        
        }

        static async Task<IList<string>> GetEmails(string url)
        {
            var httpclient = new HttpClient();
            var listOfEmails = new List<string>();
            var response = await httpclient.GetAsync(url);
            // podczas pobierania wystapil blad

            Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
            RegexOptions.IgnoreCase);
            //find items that matches with our pattern
            MatchCollection emailMatches = emailRegex.Matches(response.Content.ReadAsStringAsync().Result);

            response.Dispose();
            foreach (var emailMatch in emailMatches)
            {
                listOfEmails.Add(emailMatch.ToString());
            }

            if (listOfEmails.Count == 0)
            {
                Console.WriteLine("Brak adresów email");
            }
            
            return listOfEmails;

        }

    }
}
