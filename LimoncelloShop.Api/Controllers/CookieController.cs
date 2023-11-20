using LimoncelloShop.Domain.Objects;
using Microsoft.AspNetCore.Mvc;

namespace LimoncelloShop.Api.Controllers
{
    public class CookieController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CookieController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("CreateCookie")]
        public IActionResult Write()
        {

            try
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(30);
                string value = Guid.NewGuid().ToString();
                _httpContextAccessor.HttpContext.Response.Cookies.Append
                ("Lemonbros", value, options);
                CookieDTO cookie = new CookieDTO { Key = "Lemonbros", Value = value };
                return Ok(cookie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetCookie")]
        public IActionResult Read()
        {
            try
            {
                ViewBag.Data =
                    _httpContextAccessor.HttpContext.Request.Cookies["Lemonbros"];
                return Ok(ViewBag.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
