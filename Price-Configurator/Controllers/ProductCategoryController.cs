using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Price_Configurator.Models;
using Price_Configurator.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Price_Configurator.Controllers
{
    public class ProductCategoryController : Controller
    {
        private ApplicationDbContext _context;

        public ProductCategoryController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: ProductCategory
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!IsAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new ProductCategoryViewModel
            {
                ProductCategories = _context.ProductCategories.ToList(),
            };
            return View(model);
        }

        public bool IsAdminUser()
        {
            if (!User.Identity.IsAuthenticated) return false;
            var user = User.Identity;
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            var roles = userManager.GetRoles(user.GetUserId());
            return roles[0] == "Admin";
        }
    }
}