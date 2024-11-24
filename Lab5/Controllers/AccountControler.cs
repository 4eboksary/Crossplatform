using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Lab5.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace Lab5.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;

        public AccountController(IConfiguration config)
        {
            _config = config;
        }

        public async Task Login(string returnUrl = "/")
        {
            var authProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
            .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authProperties);
        }

        [Authorize]
        public async Task Logout()
        {
            var authProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "Home"))
            .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userProfile = new ViewLabsModel
            {
                Username = User.FindFirstValue("nickname"),
                Email = User.FindFirstValue("name")
            };

            if (!string.IsNullOrEmpty(userId))
            {
                var metadata = await GetUserMetadataAsync(userId);
                if (metadata != null)
                {
                    userProfile.FullName = metadata.FullName;
                    userProfile.PhoneNumber = metadata.PhoneNumber;
                }
            }

            return View(userProfile);
        }

        private async Task<UserMetadata> GetUserMetadataAsync(string userId)
        {
            using var client = new HttpClient();
            var token = await GetManagementApiTokenAsync();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var domain = _config["Auth0:Domain"];
            var response = await client.GetAsync($"https://{domain}/api/v2/users/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var userProfile = await response.Content.ReadFromJsonAsync<Auth0UserProfile>();
                return userProfile?.UserMetadata;
            }

            return null;
        }

        [HttpGet]
        public IActionResult Register() => View(new UsersLabModel());

        [HttpPost]
        public async Task<IActionResult> Register(UsersLabModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using var client = new HttpClient();
            var token = await GetManagementApiTokenAsync();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var user = new
            {
                email = model.Email,
                user_metadata = new
                {
                    full_name = model.FullName,
                    phone_number = model.PhoneNumber
                },
                connection = "Username-Password-Authentication",
                password = model.Password,
                username = model.Username
            };

            var response = await client.PostAsJsonAsync($"https://{_config["Auth0:Domain"]}/api/v2/users", user);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Login");

            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Registration failed: {errorContent}");

            return View(model);
        }

        private async Task<string> GetManagementApiTokenAsync()
        {
            using var client = new HttpClient();
            var domain = _config["Auth0:Domain"];

            var tokenRequest = new
            {
                client_id = _config["Auth0:ClientId"],
                client_secret = _config["Auth0:ClientSecret"],
                audience = $"https://{domain}/api/v2/",
                grant_type = "client_credentials"
            };

            var response = await client.PostAsJsonAsync($"https://{domain}/oauth/token", tokenRequest);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to get token: {response.StatusCode}, {errorContent}");
            }

            // Читаємо токен з відповіді
            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
            if (string.IsNullOrEmpty(tokenResponse?.AccessToken))
            {
                throw new Exception("Access token is null or empty.");
            }

            return tokenResponse.AccessToken;
        }

        private class TokenResponse
        {
            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; }
        }

        private class Auth0UserProfile
        {
            [JsonPropertyName("user_id")]
            public string UserId { get; set; }

            [JsonPropertyName("email")]
            public string Email { get; set; }

            [JsonPropertyName("username")]
            public string Username { get; set; }

            [JsonPropertyName("user_metadata")]
            public UserMetadata UserMetadata { get; set; }
        }

        private class UserMetadata
        {
            [JsonPropertyName("full_name")]
            public string FullName { get; set; }

            [JsonPropertyName("phone_number")]
            public string PhoneNumber { get; set; }
        }
    }
}