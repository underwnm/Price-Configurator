using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Price_Configurator.Models;

[assembly: OwinStartupAttribute(typeof(Price_Configurator.Startup))]
namespace Price_Configurator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            var context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole {Name = "Admin"};
                roleManager.Create(role);

                var user = new ApplicationUser
                {
                    UserName = "meyeradmin",
                    Email = "mark.underwood@meyermfg.com"
                };

                var userPwd = "foragebox";

                var checkUser = userManager.Create(user, userPwd);

                if (checkUser.Succeeded)
                {
                    var resultsOne = userManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("Dealer"))
            {
                var role = new IdentityRole {Name = "Dealer"};
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole {Name = "Employee"};
                roleManager.Create(role);
            }
        }

    }
}
