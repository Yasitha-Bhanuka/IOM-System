using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;
using IOMSystem.Contract.DTOs;

namespace IOMSystem.UI.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ApiService()
        {
            _apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"] ?? "https://localhost:7198/api/"; // Default or config
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_apiBaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void EnsureToken()
        {
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Session != null)
            {
                var token = System.Web.HttpContext.Current.Session["UserToken"] as string;
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                else
                {
                    _httpClient.DefaultRequestHeaders.Authorization = null;
                }
            }
        }

        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            EnsureToken();
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseData);
            }

            // Handle error (logging, throwing exception, etc.)
            throw new Exception($"API Error: {response.StatusCode}");
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            EnsureToken();
            var response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseData);
            }

            throw new Exception($"API Error: {response.StatusCode}");
        }

        // Specific methods using DTOs
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            return await PostAsync<UserDto>("users/login", loginDto);
        }

        public async Task<bool> CreateRegistrationRequestAsync(CreateRegistrationRequestDto dto)
        {
            try
            {
                // We don't expect a return object for creation, just 200 OK.
                // Re-using PostAsync might try to deserialize Void/String, so we use string return or void.
                // Let's make a generic non-returning Post or handle it here.
                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("registrations", content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
