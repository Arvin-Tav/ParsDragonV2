using Learning.Domain.DTO.ReCaptcha;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace Learning.Application.Utils
{
    public static class MethodRepo
    {

        public static bool CheckRechapcha(IFormCollection form)
        {
            string urlToPost = "https://www.google.com/recaptcha/api/siteverify";
            string secretKey = "6LeLwHMeAAAAAO9jwdHLl48anzKCxKAXqCgX70wp"; // change this
            string gRecaptchaResponse = form["g-recaptcha-response"];
            var postData = "secret=" + secretKey + "&response=" + gRecaptchaResponse;
            // send post data
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlToPost);
            request.Method = "POST";
            request.ContentLength = postData.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(postData);
            }

            // receive the response now
            string result = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }

            // validate the response from Google reCaptcha
            var captChaesponse = JsonConvert.DeserializeObject<reCaptchaResponse>(result);
            return captChaesponse.Success;
            //!!!
            //return true;
        }
    }
}
