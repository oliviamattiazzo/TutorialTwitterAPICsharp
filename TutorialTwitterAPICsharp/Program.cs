                                 using System;
using System.Globalization;

namespace TutorialTwitterAPICsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            TwitterApi twitterApi = new TwitterApi();
            dynamic tweets = twitterApi.GetTweets();

            foreach(dynamic t in tweets)
            {
                Console.WriteLine($"{t.user.name} (@{t.user.screen_name}) em {AjustaDataHora(t.created_at)}\n" +
                                  $"{t.full_text}\n");

                Console.WriteLine("*********************************************\n");
            }
        }
        private static string AjustaDataHora(dynamic dynamicDate)
        {
            //Date time format example: Thu Oct 17 20:06:45 + 0000 2019
            DateTime dataHora = DateTime.ParseExact(dynamicDate.ToString(), "ddd MMM dd HH:mm:ss K yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
            return dataHora.ToString("g", CultureInfo.CreateSpecificCulture("pt-BR"));
        }
    }
}
