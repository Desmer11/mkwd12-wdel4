using Lamazon.Domain.Constants;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using Lamazon.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.Web.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public OrdersController(IProductService productService, IOrderService orderService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        public IActionResult ShoppingCart()
        {
            var shoppingCart = GetShoppingCart();
            if (!shoppingCart.ShoppingCartItems.Any())
            {
                return RedirectToAction("Index", "Products");
            }

            var orderLineItems = new List<OrderLineItemViewModel>();

            foreach (var shoppingCartItem in shoppingCart.ShoppingCartItems)
            {
                var product = _productService.GetProductById(shoppingCartItem.Id);
                var orderLineItem = new OrderLineItemViewModel
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductPrice = product.DiscountedPrice,
                    ProductDescription = product.Description,
                    ProductImage = product.ImageUrl,
                    Quantity = 1,
                    DiscountPercentage = product.DiscountPercentage,
                };
                orderLineItem.TotalPrice = orderLineItem.Quantity * orderLineItem.ProductPrice;

                orderLineItems.Add(orderLineItem);
            }

            return View(orderLineItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(List<OrderLineItemViewModel> orderLineItems)
        {
            var orderViewModel = new OrderViewModel();
            orderViewModel.UserId = User.GetUserId();
            orderViewModel.TotalAmount = orderLineItems.Sum(x => x.TotalPrice);
            orderViewModel.OrderLineItems = orderLineItems;

            // TODO: Ip Address, CountryCode, CountryFlagUrl
            orderViewModel.IpAddress = "123";
            orderViewModel.CountryCode = "MK";
            orderViewModel.CountryFlagUrl = "MK";

            await _orderService.CreateOrder(orderViewModel);

            Response.Cookies.Delete(Cookies.ShoppingCart);
            AddNotificationMessage("Order was successfully created!");
            return RedirectToAction("Index", "Home");
        }
    }
}
