using Price_Configurator.Models;
using Price_Configurator.ViewModels;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Price_Configurator.Controllers
{
    public class DisplayProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DisplayProductController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Product
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var products = _context.Products
                .Where(p => p.ProductModelId == id)
                .Select(p => p)
                .ToList();
            
            var model = new DisplayProductViewModel
            {
                Products = products
            };
            return View(model);
        }

        public ActionResult SelectedProduct()
        {
            return View();
        }
    }
}