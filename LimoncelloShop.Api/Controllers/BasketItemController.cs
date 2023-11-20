using LimoncelloShop.Domain.Interfaces;
using LimoncelloShop.Domain.Objects;
using Microsoft.AspNetCore.Mvc;

namespace LimoncelloShop.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("BasketItem")]
    public class BasketItemController : Controller
    {
        private readonly IBasketItemService _basketItemService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketItemController(IBasketItemService basketItemService, IHttpContextAccessor httpContextAccessor)
        {
            _basketItemService = basketItemService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult GetAllBasketItemsByCookieOrUser(string? cookieValue)
        {
            IEnumerable<BasketItem> basketItems;
            if (!User.IsInRole(UserRoles.User) && !User.IsInRole(UserRoles.Admin))
            {
                basketItems = _basketItemService.GetAllBasketItems(cookieValue, "");

            }
            else
            {
                string email = User.Identity!.Name!;
                basketItems = _basketItemService.GetAllBasketItems(null, email);
            }

            //List<BasketItemDTO> basketItemDTOs = new();
            //foreach (var basketItem in basketItems)
            //{
            //    basketItemDTOs.Add(BasketItemDTO.ToBasketItemDTO(basketItem));
            //}
            return Ok(basketItems);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, string? cookieValue)
        {
            bool hasRole = User.IsInRole(UserRoles.User) && User.IsInRole(UserRoles.Admin);
            if (hasRole)
            {
                string email = User.Identity.Name!;
                return Ok(_basketItemService.GetBasketItem(id, hasRole, cookieValue: null, email));
            }
            else
            {
                return Ok(_basketItemService.GetBasketItem(id, hasRole, cookieValue, ""));
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] BasketItemDTO basketItemDTO)
        {
            try
            {
                basketItemDTO.Id = 0;
                var basketItem = BasketItemDTO.ToBasketItem(basketItemDTO);

                //if (basketItemDTO.Cookie != "")
                //{
                //    basketItem.Basket.Cookie = _httpContextAccessor.HttpContext.Request.Cookies[basketItemDTO.Cookie];
                //}

                await _basketItemService.AddBasketItem(basketItem);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            try
            {
                bool isInRole = User.IsInRole(UserRoles.User) || User.IsInRole(UserRoles.Admin);
                BasketItem? existingBasketItem = _basketItemService.GetBasketItem(id, isInRole);
                if (existingBasketItem == null)
                    throw new ArgumentException($"The ticket to delete with id {id} does not exist.");
                await _basketItemService.DeleteBasketItem(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] BasketItemEditDTO basketItemEditDTO)
        {
            try
            {
                BasketItem? existingBasketItem = _basketItemService.GetBasketItem(basketItemEditDTO.Id, false);
                if (existingBasketItem == null)
                    throw new ArgumentException($"The ticket to edit with id {basketItemEditDTO.Id} does not exist.");
                await _basketItemService.EditBasketItem(existingBasketItem, basketItemEditDTO);
                return Ok(existingBasketItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
