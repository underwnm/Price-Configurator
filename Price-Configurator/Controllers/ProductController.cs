using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Price_Configurator.Models;
using System.Web.Mvc;

namespace Price_Configurator.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _context;

        public ProductController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Product
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

            return View();
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