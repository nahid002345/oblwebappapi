using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace OBLMobileAppApi.Class
{
    public class CustomHttpRequest
    {

        public CustomHttpRequest()
        {
        }
        public void PostJSONRequest(string url,NameValueCollection parameter)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json ="{"+ string.Join(",", Array.ConvertAll(parameter.AllKeys, key => string.Format(@"""{0}"":""{1}""", HttpUtility.UrlEncode(key), parameter[key])))+"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
    }
}