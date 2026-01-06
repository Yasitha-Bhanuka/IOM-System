using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebformsUI.Helpers
{
    public class UserDto
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string FullName { get; set; }
        public string BranchName { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Token { get; set; }
    }

    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string BranchName { get; set; }
        public string RoleName { get; set; }
    }

    public static class ApiClient
    {
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];

        public static async Task<UserDto> LoginAsync(string email, string password)
        {
            var loginData = new { Email = email, Password = password };
            var json = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try 
            {
                var response = await _client.PostAsync($"{_apiBaseUrl}/api/Auth/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UserDto>(responseString);
                }
            }
            catch (Exception)
            {
               // Log error or rethrow if needed
               throw;
            }

            return null;
        }

        public static async Task<string> RegisterAsync(RegisterUserDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync($"{_apiBaseUrl}/api/Auth/Register", content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }
    }
}
