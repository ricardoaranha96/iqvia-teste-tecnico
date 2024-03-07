using Microsoft.AspNetCore.Mvc;
using TesteTecnico.Data;
using Microsoft.EntityFrameworkCore;
using TesteTecnico.Models;
using Microsoft.AspNetCore.Identity;
using TesteTecnico.Areas.Identity.Data;
using System.Security.Claims;

namespace TesteTecnico.Controllers
{
    public class OrderController: Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly EcommerceContext _context;
        private readonly UserManager<TesteTecnicoUser> _userManager;
        private readonly SignInManager<TesteTecnicoUser> _signInManager;
        IHttpContextAccessor _httpContextAccessor;

        public OrderController(ILogger<OrderController> logger, 
            EcommerceContext context, UserManager<TesteTecnicoUser> userManager, IHttpContextAccessor httpContextAccessor,
            SignInManager<TesteTecnicoUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Cart()
        {
            Order order = this.getSessionOrder();
            if (order.OrdersProducts == null || !order.OrdersProducts.Any())
            {
                _logger.LogWarning("Trying to access empty cart");
                return RedirectToAction("Index", "Home");
            }

            return View(order);
        }

        public IActionResult AddProduct([Bind("ProductId")] OrderProduct orderProduct)
        {
            var product = _context.Products
                .Where(p => p.Id == orderProduct.ProductId)
                .FirstOrDefault();

            if (product == null)
            {
                _logger.LogError("Trying to add an inexistent product to the cart");
                return NotFound();
            }

            Order order = this.getSessionOrder();


            if(order.OrdersProducts != null && order.OrdersProducts.Any()) { 
                foreach (OrderProduct existentOrderProduct in order.OrdersProducts)
                {
                    if (existentOrderProduct.ProductId == orderProduct.ProductId)
                    {
                        _logger.LogWarning("Trying to add a product that is already in the cart");
                        return RedirectToAction("Cart");
                    }
                }
            }

            orderProduct.OrderId = order.Id;
            orderProduct.Quantity = 1;
            orderProduct.UnitPrice = product.Price;
            orderProduct.TotalPrice = product.Price;
            _context.OrdersProducts.Add(orderProduct);
            _context.SaveChanges();

            return RedirectToAction("Cart");
        }

        public IActionResult UpdateProductQuantity([Bind("Quantity", "ProductId")] OrderProduct orderProduct)
        {
            Order order = this.getSessionOrder();
            foreach (OrderProduct sessionOrderProduct in order.OrdersProducts)
            {
                if (sessionOrderProduct.ProductId == orderProduct.ProductId)
                {
                    sessionOrderProduct.Quantity = orderProduct.Quantity;
                    sessionOrderProduct.TotalPrice = sessionOrderProduct.Quantity * sessionOrderProduct.UnitPrice;
                    _context.OrdersProducts.Update(sessionOrderProduct);
                    _context.SaveChanges();
                    break;
                }
            }

            return RedirectToAction("Cart");
        }

        public IActionResult RemoveProduct(int? id)
        {
            if (id == null)
            {
                _logger.LogError("Missing id parameter on Order/RemoveProduct");
                return RedirectToAction("Cart");
            }

            Order order = this.getSessionOrder();
            foreach (OrderProduct orderProduct in order.OrdersProducts)
            {
                if (orderProduct.ProductId == id)
                {
                    _context.OrdersProducts.Remove(orderProduct);
                    _context.SaveChanges();
                    break;
                }
            }

            return RedirectToAction("Cart");
        }

        public IActionResult FinishOrder()
        {
            if (!_signInManager.IsSignedIn(_httpContextAccessor.HttpContext?.User))
            {                
                return Redirect("/Identity/Account/Login");                
            }

            Order order = this.getSessionOrder();
            order.Status = OrderStatuses.AWAITING_PAYMENT;
            order.FinishedAt = DateTime.Now;
            order.UserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Orders.Update(order);
            _context.SaveChanges();
            return RedirectToAction("Success", new { id = order.Id });
        }

        public IActionResult Success(int? id)
        {
            var order = _context.Orders
                .FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                _logger.LogWarning("Trying to access Order/Success without an ongoing order");
                return RedirectToAction("Index", "Home");
            }
            return View(order);
        }

        private Order getSessionOrder()
        {
            var order = _context.Orders
                .Include(o => o.OrdersProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefault(o => o.Status == OrderStatuses.CREATING);
            if (order != null)
            {
                return order;
            }

            order = new Order();
            order.CreatedAt = DateTime.Now;
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public IActionResult MyOrders()
        {
            if (!_signInManager.IsSignedIn(_httpContextAccessor.HttpContext?.User))
            {
                return Unauthorized();
            }
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Order[] orders = _context.Orders
                .Include(o => o.OrdersProducts)
                .ThenInclude(op => op.Product)
                .Where(o => o.UserId == userId)
                .ToArray();
            return View(orders);
        }
    }
}
