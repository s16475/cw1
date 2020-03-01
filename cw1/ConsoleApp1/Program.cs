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
            try
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
            } catch (ArgumentNullException e)
            {
                Console.WriteLine("No argument:" + e); 
            }
        }

        static async Task<IList<string>> GetEmails(string url)
        {
            var httpclient = new HttpClient();
            var listOfEmails = new List<string>();
            var response = await httpclient.GetAsync(url);

            Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
            RegexOptions.IgnoreCase);
            //find items that matches with our pattern
            MatchCollection emailMatches = emailRegex.Matches(response.Content.ReadAsStringAsync().Result);
            
            foreach (var emailMatch in emailMatches)
            {
                listOfEmails.Add(emailMatch.ToString());
            }

            return listOfEmails;
        }

    }
}
