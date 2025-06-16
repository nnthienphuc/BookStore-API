using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Text.Json;
using BookStoreWebAppFE.Components.Share;
using BookStoreWebAppFE.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BookStoreWebAppFE.Services
{
    public interface IAuthenticationService
    {
        Task<bool> Login(string username, string password);
        Task<string> Register(Staff staffModel);
        Task Logout();
    }

    public class AuthenticationService : IAuthenticationService
    {
        public UserDataBase User { get; set; }
        public StorageItem<string> tokenData { get; set; }

        private HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;
        private readonly IJSRuntime _js;
        public AuthenticationService(
            HttpClient httpClient,
            NavigationManager navigationManager,
             ILocalStorageService localStorageService,
            IJSRuntime js
        )
        {
            _js = js;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _httpClient = httpClient;

        }

        public async Task<bool> Login(string username, string password)
        {
            try
            {
                LoginModel login = new();
                {
                    login.Email = username;
                    login.Password = password;
                }
            ;
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", login);
                var errorContent = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var doc = JsonDocument.Parse(json);
                    var token = doc.RootElement.GetProperty("token").GetString();
                    _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    // Handle successbook
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public async Task Logout()
        {
            User = null;
            _httpClient.DefaultRequestHeaders.Authorization = null;
            _navigationManager.NavigateTo("");
        }

        public async Task<string> Register(Staff staffModel)
        {
            try
            {
                if (staffModel != null)
                {
                    var response = await _httpClient.PostAsJsonAsync("api/auth/register", staffModel);
                    var message = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return message;
                    }

                    return $"Error: {message}";
                }

                return "Error: Missing staff data.";
            }
            catch (Exception e)
            {
                return $"Error: {e.Message}";
            }
        }

        //public async Task<string> Register(Staff staffModel)
        //{
        //    string errorContent = string.Empty;
        //    try
        //    {
        //        if(staffModel != null)
        //        {
        //            var response = await _httpClient.PostAsJsonAsync("api/auth/register", staffModel);
        //            errorContent = await response.Content.ReadAsStringAsync();
        //            if (response.StatusCode == HttpStatusCode.OK)
        //            {
        //                return "Success";
        //            }
        //            return $"{errorContent}";
        //        }

        //        return $"{errorContent}";
        //    }
        //    catch (Exception e)
        //    {
        //        return $"{errorContent}";
        //    }
        //}
    }
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
