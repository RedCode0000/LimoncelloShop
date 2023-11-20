using LimoncelloShop.Domain.Interfaces;
using LimoncelloShop.Domain.Objects;
using Microsoft.AspNetCore.Mvc;

namespace LimoncelloShop.Api.Controllers
{
    [ApiController]
    [Route("Basket")]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserService _userService;

        public BasketController(IBasketService basketService, IHttpContextAccessor contextAccessor, IUserService userService)
        {
            _basketService = basketService;
            _contextAccessor = contextAccessor;
            _userService = userService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateBasket(BasketDTO basketDTO)
        {
            try
            {
                Basket basket = BasketDTO.ToBasket(basketDTO);

                if (!User.IsInRole(UserRoles.User) && !User.IsInRole(UserRoles.Admin))
                {
                    basket.Cookie = _contextAccessor.HttpContext!.Request.Cookies[basketDTO.Cookie];
                }
                else
                {
                    basket.User = await _userService.GetUserByEmail(User.Identity!.Name!)!;
                }

                await _basketService.AddBasket(basket);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetBasket(int id)
        {
            try
            {
                _basketService.GetBasket(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            try
            {
                await _basketService.DeleteBasket(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
