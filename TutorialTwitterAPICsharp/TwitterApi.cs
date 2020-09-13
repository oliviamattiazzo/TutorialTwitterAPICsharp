using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;

namespace TutorialTwitterAPICsharp
{
    public class TwitterApi
    {
        public dynamic GetTweets()
        {
            Authorization authorization = new Authorization();
            string accessToken = authorization.GetAccessToken();

            var getTimeline = WebRequest.Create(@"https://api.twitter.com/1.1/statuses/user_timeline.json?" +
                                                "screen_name=oliviamattiazzo&" +
                                                "count=10&" +
                                                "include_rts=false&"+
                                                "tweet_mode=extended") as HttpWebRequest;
            getTimeline.Method = "GET";
            getTimeline.Headers[HttpRequestHeader.Authorization] = "Bearer " + accessToken;

            dynamic responseItems;
            try
            {
                string respBody = null;
                using (var resp = getTimeline.GetResponse().GetResponseStream())
                {
                    var respR = new StreamReader(resp);
                    respBody = respR.ReadToEnd();
                }

                responseItems = JsonConvert.DeserializeObject(respBody);
            }
            catch
            {
                throw new Exception("Error getting tweets!");
            }

            return responseItems;
        }
    }
}
