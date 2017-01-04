using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Price_Configurator.Models;
using System.Linq;
using System.Web.Mvc;

namespace Price_Configurator.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationDbContext _context;

        public RoleController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Role
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

            var roles = _context.Roles.ToList();
            return View(roles);
        }

        public bool IsAdminUser()
        {
            if (!User.Identity.IsAuthenticated) return false;
            var user = User.Identity;
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            var roles = userManager.GetRoles(user.GetUserId());
            return roles[0] == "Admin";
        }

        public ActionResult Create()
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

            var role = new IdentityRole();
            return View(role);
        }

        [HttpPost]
        public ActionResult Create(IdentityRole role)
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

            _context.Roles.Add(role);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}