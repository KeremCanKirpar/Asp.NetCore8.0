using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

namespace StoreApp.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly Cart _cart;
        private readonly IOrderRepository _repository;

        public OrderController(Cart cart, IOrderRepository repository)
        {
            _cart = cart;
            _repository = repository;
        }
        public IActionResult Checkout()
        {

            return View(new OrderModel() { Cart = _cart });
        }
        [HttpPost]
        public IActionResult Checkout(OrderModel order)
        {
            if (_cart.Items.Count == 0)
            {
                ModelState.AddModelError("", "Sepetinizde Ürün Yok");
                return View(order);
            }

            if (!ModelState.IsValid)
            {
                order.Cart = _cart;
                return View(order);
            }

            var model = new Order
            {
                Name = order.Name,
                Email = order.Email,
                Phone = order.Phone,
                City = order.City,
                AddressLine = order.AddressLine,
                OrderDate = DateTime.Now,
                OrderItems = [.. _cart.Items.Select(c=> new StoreApp.Data.Concrete.OrderItem
                {
                    ProductId = c.Product.Id,
                    Price = c.Product.Price,
                    Quantity = c.Quantity
                })]
            };

            order.Cart = _cart;
            var payment = ProcessPayment(order);

            if (payment.Status == "success")
            {
                _repository.SaveOrder(model);
                _cart.Clear();
                return RedirectToPage("/Completed", new { OrderId = model.OrderId });
            }

            ModelState.AddModelError("", "Ödeme işlemi başarısız oldu. Lütfen bilgilerinizi kontrol edin.");
            return View(order);
        }

        public IActionResult Completed(int OrderId)
        {
            return View(OrderId);
        }

        private Payment ProcessPayment(OrderModel order)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-S1toIM1yGW06wYEPAKsuyoopMpJQgXPJ";
            options.SecretKey = "sandbox-mrWntrA9ZlM1NC2FfmOtFbYv80K7qaru";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = new Random().Next(111111111, 999999999).ToString();
            request.Price = order?.Cart?.CalculateTotal().ToString();
            request.PaidPrice = order?.Cart?.CalculateTotal().ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = order?.CardName;
            paymentCard.CardNumber = order?.Cardnumber;
            paymentCard.ExpireMonth = order?.ExpirationMonth;
            paymentCard.ExpireYear = order?.ExpirationYear;
            paymentCard.Cvc = order?.Cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = order?.Name;
            buyer.Surname = order?.Name;
            buyer.GsmNumber = order?.Phone;
            buyer.Email = order?.Email;
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            buyer.RegistrationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            buyer.RegistrationAddress = order?.AddressLine;
            buyer.Ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";
            buyer.City = order?.City;
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = order?.Name;
            shippingAddress.City = order?.City;
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = order?.AddressLine;
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = order?.Name;
            billingAddress.City = order?.City;
            billingAddress.Country = "Turkey";
            billingAddress.Description = order?.AddressLine;
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            foreach (var item in order?.Cart?.Items ?? Enumerable.Empty<CartItem>())
            {
                BasketItem firstBasketItem = new BasketItem();
                firstBasketItem.Id = item.Product.Id.ToString();
                firstBasketItem.Name = item.Product.ProductName;
                firstBasketItem.Category1 = "Telefon";
                firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                firstBasketItem.Price = item.Product.Price.ToString();
                basketItems.Add(firstBasketItem);
            }
            request.BasketItems = basketItems;
            Payment payment = Payment.Create(request, options).Result;
            return payment;
        }
    }
}
