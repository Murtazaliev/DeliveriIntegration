using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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
        
        public async Task<string> GetAsync<T>(string method, T data)
        {
            try
            {
                var parameters = ToQueryString(data); 
                HttpWebRequest webRequest = WebRequest.CreateHttp($"{_baseUrl}{method}?{parameters}");
                webRequest.Method = "GET";
                webRequest.ContentType = "Application/json";
                var mainResponse = await ReadResponseAsync(webRequest.GetResponse());
                return mainResponse;
            }
            catch (WebException webException)
            {
                var mainResponse = await ReadResponseAsync(webException.Response);
                return mainResponse;
            }
        }

        public async Task<string> PostAsync<T>(string method, T bodyParameter, string contentType)
        {
            try
            {
                var webRequest = WebRequest.CreateHttp(_baseUrl + method);
                webRequest.Method = "POST";
                webRequest.ContentType = contentType;
                
                if(contentType == "Application/json")
                    await WriteJsonBodyAsync(webRequest, bodyParameter);
                if (contentType == "form-data")
                    await WriteFormBodyAsync(webRequest, bodyParameter);
                
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
            await using Stream responseStream = httpWebResponse.GetResponseStream();
            using StreamReader responseReader = new StreamReader(responseStream);
            return await responseReader.ReadToEndAsync();
        }

        private async Task WriteJsonBodyAsync<T>(HttpWebRequest webRequest, T writeBody)
        {
            await using Stream requestStream = webRequest.GetRequestStream();
            await using StreamWriter requestWriter = new StreamWriter(requestStream);
            await requestWriter.WriteAsync(JsonConvert.SerializeObject(writeBody));
        }
        
        private async Task WriteFormBodyAsync<T>(HttpWebRequest webRequest, T writeBody)
        {
            await using Stream requestStream = webRequest.GetRequestStream();
            await using StreamWriter requestWriter = new StreamWriter(requestStream);
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