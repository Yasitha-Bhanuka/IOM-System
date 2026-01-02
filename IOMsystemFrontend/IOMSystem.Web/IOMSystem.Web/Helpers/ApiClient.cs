using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization; // Standard in .NET 4.x for JSON

namespace IOMSystem.Web.Helpers
{
    public class ApiClient
    {
        private static ApiClient _instance;
        private readonly HttpClient _client;
        private readonly string _baseUrl = "http://localhost:5102/api/";
        private readonly JavaScriptSerializer _serializer;

        private ApiClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _serializer = new JavaScriptSerializer();
        }

        public static ApiClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ApiClient();
                return _instance;
            }
        }

        public T Get<T>(string endpoint)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync(endpoint).Result;
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    return _serializer.Deserialize<T>(json);
                }
                return default(T);
            }
            catch (Exception)
            {
                // Log error
                return default(T);
            }
        }

        public TResponse Post<TResponse, TRequest>(string endpoint, TRequest data)
        {
            try
            {
                string json = _serializer.Serialize(data);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(endpoint, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseJson = response.Content.ReadAsStringAsync().Result;
                    return _serializer.Deserialize<TResponse>(responseJson);
                }
                return default(TResponse);
            }
            catch
            {
                return default(TResponse);
            }
        }

        public TResponse Post<TResponse, TRequest>(string endpoint, TRequest data)
        {
            try
            {
                string json = _serializer.Serialize(data);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(endpoint, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseJson = response.Content.ReadAsStringAsync().Result;
                    return _serializer.Deserialize<TResponse>(responseJson);
                }
                return default(TResponse);
            }
            catch
            {
                return default(TResponse);
            }
        }

        public bool Post<T>(string endpoint, T data)
        {
            try
            {
                string json = _serializer.Serialize(data);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(endpoint, content).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public bool Put<T>(string endpoint, T data)
        {
            try
            {
                string json = _serializer.Serialize(data);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(endpoint, content).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(string endpoint)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(endpoint).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
