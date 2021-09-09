using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Delivery.SelfServiceKioskApi.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Delivery.SelfServiceKioskApi.Concrete
{
    public class Repository
    {
        private readonly string _baseUrl;

        public Repository(string baseUrl)
        {
            _baseUrl = baseUrl;
        }
        
        public async Task<string> GetAsync<T>(string method, T data, string token)
        {
            try
            {
                var parameters = ToQueryString(data); 
                HttpWebRequest webRequest = WebRequest.CreateHttp($"{_baseUrl}{method}?{parameters}");
                webRequest.Method = "GET";
                webRequest.ContentType = ContentTypes.ApplicationJson;
                if(token != string.Empty)
                    webRequest.Headers.Add(HttpRequestHeader.Authorization, token);
                
                var mainResponse = await ReadResponseAsync(webRequest.GetResponse());
                return mainResponse;
            }
            catch (WebException webException)
            {
                var mainResponse = await ReadResponseAsync(webException.Response);
                return mainResponse;
            }
        }
        
        public async Task<string> GetAsync(string method, string token)
        {
            try
            {
                HttpWebRequest webRequest = WebRequest.CreateHttp($"{_baseUrl}{method}");
                webRequest.Method = "GET";
                webRequest.ContentType = ContentTypes.ApplicationJson;
                if(token != string.Empty)
                    webRequest.Headers.Add(HttpRequestHeader.Authorization, token);
                
                var mainResponse = await ReadResponseAsync(webRequest.GetResponse());
                return mainResponse;
            }
            catch (WebException webException)
            {
                var mainResponse = await ReadResponseAsync(webException.Response);
                return mainResponse;
            }
        }

        public async Task<string> PostAsync<T>(string method, T data, string contentType, string token)
        {
            try
            {
                var webRequest = WebRequest.CreateHttp(_baseUrl + method);
                webRequest.Method = "POST";
                webRequest.ContentType = contentType;
                if (token != string.Empty)
                    webRequest.Headers.Add(HttpRequestHeader.Authorization, token);
                
                if(contentType == ContentTypes.ApplicationJson)
                    await WriteJsonBodyAsync(webRequest, data);
                if (contentType == ContentTypes.FormData)
                    await WriteFormBodyAsync(webRequest, data);
                
                var mainResponse = await ReadResponseAsync(webRequest.GetResponse());
                return mainResponse;
            }
            catch (WebException webException)
            {
                var mainResponse = await ReadResponseAsync(webException.Response);
                Console.WriteLine(mainResponse);
                return mainResponse;
            }
        }

        private async Task<string> ReadResponseAsync(WebResponse webResponse)
        {
            var httpWebResponse = ((HttpWebResponse) webResponse);
            await using var responseStream = httpWebResponse.GetResponseStream();
            using var responseReader = new StreamReader(responseStream);
            return await responseReader.ReadToEndAsync();
        }

        private async Task WriteJsonBodyAsync<T>(HttpWebRequest webRequest, T writeBody)
        {
            await using var requestStream = webRequest.GetRequestStream();
            await using var requestWriter = new StreamWriter(requestStream);
            await requestWriter.WriteAsync(JsonConvert.SerializeObject(writeBody));
        }
        
        private async Task WriteFormBodyAsync<T>(HttpWebRequest webRequest, T writeBody)
        {
            await using var requestStream = webRequest.GetRequestStream();
            await using var requestWriter = new StreamWriter(requestStream);
            var data = ToQueryString(writeBody);
            await requestWriter.WriteAsync(data);
        }
        
        private string ToQueryString<T>(T writeBody)
        {
            string json = JsonConvert.SerializeObject(writeBody); 
            var jObj = (JObject)JsonConvert.DeserializeObject(json);

            var data = string.Join("&",
                jObj.Children()
                    .Cast<JProperty>()
                    .Select(jp=>jp.Name + "=" + HttpUtility.UrlEncode(jp.Value.ToString())));
            return data;
        }
    }
}