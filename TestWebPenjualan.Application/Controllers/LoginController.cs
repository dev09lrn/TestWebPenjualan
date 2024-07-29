using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TestWebPenjualan.Application.Auth;
using TestWebPenjualan.Domain.Helpers;
using TestWebPenjualan.Domain.Dtos.Login;
using TestWebPenjualan.Application.Interfaces;

namespace TestWebPenjualan.Application.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginHelper _loginHelper;
        private readonly IWebApiHelper _webApiHelper;

        public LoginController(ILogger<LoginController> logger,
            ILoginHelper loginHelper,
            IWebApiHelper webApiHelper)
        {
            _logger = logger;
            _loginHelper = loginHelper;
            _webApiHelper = webApiHelper;
        }

        [ServiceFilter(typeof(AuthorizeActionFilter))]
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost("/login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var url = _webApiHelper.GetLoginUrlEndpoint();

            try
            {
                using var client = new HttpClient();
                var response = await client.PostAsJsonAsync(url, loginRequest);
                var reponseObj = await response.Content.ReadFromJsonAsync<LoginResponseDto>();

                if (reponseObj != null)
                {
                    if (reponseObj.Success)
                    {
                        var signInAsync = await _loginHelper.ClaimsIdentitySignInAsync(loginRequest, reponseObj);

                        if (signInAsync)
                        {
                            return RedirectToAction("Index", "Home");
                        }                            
                        else
                        {
                            TempData["ErrorMessage"] = LoginMessageHelper.GetInfoLoginFailed();
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = reponseObj.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TempData["ErrorMessage"] = LoginMessageHelper.GetInfoLoginFailed();
            }

            return View();
        }

        [HttpGet("/forgotpassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
    }
}
