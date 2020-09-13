using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace TutorialTwitterAPICsharp
{
    public class Authorization
    {
        private static string GetConsumerKey()
        {
            var xml = XDocument.Load("../../../AuthorizationKeys.xml");
            return xml.Root.Element("ConsumerKey").Value;
        }

        private static string GetConsumerSecret()
        {
            var xml = XDocument.Load("../../../AuthorizationKeys.xml");
            return xml.Root.Element("ConsumerSecret").Value;
        }

        public string GetAccessToken()
        {
            string accessToken = "";
            string consumerKey = GetConsumerKey();
            string consumerSecret = GetConsumerSecret();
            var credentials = Convert.ToBase64String(new UTF8Encoding().GetBytes(GetConsumerKey() + ":" + GetConsumerSecret()));

            var request = WebRequest.Create("https://api.twitter.com/oauth2/token") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;

            var reqBody = Encoding.UTF8.GetBytes("grant_type=client_credentials");
            request.ContentLength = reqBody.Length;

            using (var req = request.GetRequestStream())
            {
                req.Write(reqBody, 0, reqBody.Length);
            }

            try
            {
                string respbody = null;
                using (var resp = request.GetResponse().GetResponseStream())
                {
                    var respR = new StreamReader(resp);
                    respbody = respR.ReadToEnd();
                }

                accessToken = respbody.Substring(respbody.IndexOf("access_token\":\"") + "access_token\":\"".Length, respbody.IndexOf("\"}") - (respbody.IndexOf("access_token\":\"") + "access_token\":\"".Length));
            }
            catch
            {
                throw new Exception("Error getting Twitter Access Token!");
            }

            return accessToken;
        }
    }
}
