using Price_Configurator.Models;
using Price_Configurator.ViewModels.Manage;
using System.Linq;
using System.Web.Mvc;

namespace Price_Configurator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var model = new NavBarViewModel
            {
                ProductCategories = _context.ProductCategories.ToList(),
                ProductModels = _context.ProductModels.ToList()
            };
            return View(model);
        }
    }
}