using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Price_Configurator.Models;
using Price_Configurator.ViewModels;
using Price_Configurator.ViewModels.Manage;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Price_Configurator.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly ApplicationDbContext _context;

        public ManageController()
        {
            _context = new ApplicationDbContext();
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess
                    ? "Your password has been changed."
                    : message == ManageMessageId.SetPasswordSuccess
                        ? "Your password has been set."
                        : message == ManageMessageId.SetTwoFactorSuccess
                            ? "Your two-factor authentication provider has been set."
                            : message == ManageMessageId.Error
                                ? "An error has occurred."
                                : message == ManageMessageId.AddPhoneSuccess
                                    ? "Your phone number was added."
                                    : message == ManageMessageId.RemovePhoneSuccess
                                        ? "Your phone number was removed."
                                        : "";

            var userId = User.Identity.GetUserId();
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (IsAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
            }
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
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

        public ActionResult Users()
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

            var roles = new UserViewModel
            {
                ApplicationUsers = _context.Users.ToList(),
            };

            return View(roles);
        }

        public ActionResult DeleteUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Users");
        }

        public ActionResult EditUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = _context.Users.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser([Bind(Include = "Id,Username,PhoneNumber,Email")] ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Users");
            }
            return View(user);
        }

        public ActionResult Roles()
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

            var roles = new RoleViewModel
            {
                Roles = _context.Roles.ToList()
            };

            return View(roles);
        }

        public ActionResult DeleteRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = _context.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return RedirectToAction("Roles");
        }

        public ActionResult EditRole(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = _context.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole([Bind(Include =
                                          "Id,Name")] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(role).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Roles");
            }
            return View(role);
        }

        public ActionResult AddRole()
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

            var model = new RoleViewModel
            {
                Roles = _context.Roles.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddRole(RoleViewModel model)
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

            var role = new IdentityRole
            {
                Name = model.Name
            };

            _context.Roles.Add(role);
            _context.SaveChanges();
            return RedirectToAction("Roles");
        }

        public ActionResult ProductCategories()
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

        public ActionResult DeleteProductCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = _context.ProductCategories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            _context.ProductCategories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("ProductCategories");
        }

        public ActionResult EditProductCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = _context.ProductCategories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProductCategory([Bind(Include = "Id,Name")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(productCategory).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("ProductCategories");
            }
            return View(productCategory);
        }

        public ActionResult AddProductCategory()
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

        [HttpPost]
        public ActionResult AddProductCategory(ProductCategoryViewModel model)
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

            var category = new ProductCategory
            {
                Name = model.Name
            };
            _context.ProductCategories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("ProductCategories");
        }

        public ActionResult ProductModels()
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
            var model = new ProductModelViewModel
            {
                ProductModels = _context.ProductModels.ToList(),
                ProductCategories = _context.ProductCategories.ToList()
            };
            return View(model);
        }

        public ActionResult DeleteProductModel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productModel = _context.ProductModels.Find(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            _context.ProductModels.Remove(productModel);
            _context.SaveChanges();
            return RedirectToAction("ProductModels");
        }

        public ActionResult EditProductModel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productModels = _context.ProductModels.Find(id);
            if (productModels == null)
            {
                return HttpNotFound();
            }
            PopulateProductCategoriesDropDownList(productModels.ProductCategoryId);
            return View(productModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProductModel([Bind(Include = "Id,Name,ProductCategoryId,ProductCategory")] ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(productModel).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("ProductModels");
            }
            return View(productModel);
        }

        private void PopulateProductCategoriesDropDownList(object selectedProductCategory = null)
        {
            var query = from d in _context.ProductCategories
                                   orderby d.Name
                                   select d;
            ViewBag.ProductCategoryId = new SelectList(query, "Id", "Name", selectedProductCategory);
        }

        public ActionResult AddProductModel()
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

            var model = new ProductModelViewModel
            {
                ProductCategories = _context.ProductCategories.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddProductModel(ProductModelViewModel model)
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

            var productModel = new ProductModel
            {
                Name = model.Name,
                ProductCategoryId = model.ProductCategoryId
            };
            _context.ProductModels.Add(productModel);
            _context.SaveChanges();
            return RedirectToAction("ProductModels");
        }

        public ActionResult Products()
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
            var model = new ProductViewModel
            {
                Products = _context.Products.ToList(),
                ProductModels = _context.ProductModels.ToList()
            };
            return View(model);
        }

        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Products");
        }

        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            PopulateProductsDropDownList(product.ProductModelId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct([Bind(Include = "Id,Name,ProductModelId,ProductModel")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Products");
            }
            return View(product);
        }

        private void PopulateProductsDropDownList(object selectedProducts = null)
        {
            var query = from d in _context.ProductModels
                                  orderby d.Name
                                  select d;
            ViewBag.ProductId = new SelectList(query, "Id", "Name", selectedProducts);
        }

        public ActionResult AddProduct()
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

            var model = new ProductViewModel
            {
                ProductModels = _context.ProductModels.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModel model)
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

            var product = new Product
            {
                Name = model.Name,
                ProductModelId = model.ProductModelId
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Products");
        }

        public ActionResult ProductsEquipment()
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
            var model = new ProductEquipmentViewModel()
            {
                CurrentProductEquipments = _context.ProductsEquipment.ToList(),
                Products = _context.Products.ToList(),
                Equipments = _context.Equipments.ToList()
            };
            return View(model);
        }

        public ActionResult DeleteProductEquipment(int? productId, int? equipmentId)
        {
            if (equipmentId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productsEquipment = _context.ProductsEquipment.Find(productId, equipmentId);
            if (productsEquipment == null)
            {
                return HttpNotFound();
            }
            _context.ProductsEquipment.Remove(productsEquipment);
            _context.SaveChanges();
            return RedirectToAction("ProductsEquipment");
        }

        public ActionResult EditProductEquipment(int? productId, int? equipmentId)
        {
            if (equipmentId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productsEquipment = _context.ProductsEquipment.Find(productId, equipmentId);
            if (productsEquipment == null)
            {
                return HttpNotFound();
            }
            PopulateProductsFromDropDownList(productsEquipment.ProductId);
            PopulateEquipmentFromDropDownList(productsEquipment.EquipmentId);
            return View(productsEquipment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProductEquipment([Bind(Include = "ProductId, Product, EquipmentId, Equipment")] ProductEquipment productEquipment)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(productEquipment).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("ProductsEquipment");
            }
            return View(productEquipment);
        }

        private void PopulateProductsFromDropDownList(object selected = null)
        {
            var query = from d in _context.ProductsEquipment
                        orderby d.Product
                        select d;
            ViewBag.ProductId = new SelectList(query, "Id", "Name", selected);
        }

        private void PopulateEquipmentFromDropDownList(object selected = null)
        {
            var query = from d in _context.ProductsEquipment
                        orderby d.Equipment
                        select d;
            ViewBag.EquipmentId = new SelectList(query, "Id", "Name", selected);
        }

        public ActionResult AddProductEquipment()
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

            var model = new ProductEquipmentViewModel
            {
                Products = _context.Products.ToList(),
                Equipments = _context.Equipments.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddProductEquipment(ProductEquipmentViewModel model)
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

            var productEquipment = new ProductEquipment()
            {
                ProductId = model.ProductId,
                EquipmentId = model.EquipmentId
            };
            _context.ProductsEquipment.Add(productEquipment);
            _context.SaveChanges();
            return RedirectToAction("AddProductEquipment");
        }

        public ActionResult ProductEquipmentRules()
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
            var model = new ProductEquipmentRuleViewModel()
            {
                CurrentProductEquipmentRules = _context.ProductEquipmentRules.ToList(),
                Products = _context.Products.ToList(),
                EquipmentRules = _context.EquipmentRules.ToList()
            };
            return View(model);
        }

        public ActionResult AddProductEquipmentRule()
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

            var model = new ProductEquipmentRuleViewModel
            {
                Products = _context.Products.ToList(),
                EquipmentRules = _context.EquipmentRules.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddProductEquipmentRule(ProductEquipmentRuleViewModel model)
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

            var productEquipmentRule = new ProductEquipmentRule()
            {
                ProductId = model.ProductId,
                EquipmentRuleId = model.EquipmentRuleId
            };
            _context.ProductEquipmentRules.Add(productEquipmentRule);
            _context.SaveChanges();
            return RedirectToAction("ProductEquipmentRules");
        }

        public ActionResult EquipmentGroups()
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
            var model = new EquipmentGroupViewModel()
            {
                EquipmentGroups = _context.EquipmentGroups.ToList(),
            };
            return View(model);
        }

        public ActionResult DeleteEquipmentGroup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var group = _context.EquipmentGroups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            _context.EquipmentGroups.Remove(group);
            _context.SaveChanges();
            return RedirectToAction("EquipmentGroups");
        }

        public ActionResult EditEquipmentGroup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var group = _context.EquipmentGroups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEquipmentGroup([Bind(Include = "Id,Description")] EquipmentGroup equipmentGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(equipmentGroup).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("EquipmentGroups");
            }
            return View(equipmentGroup);
        }

        public ActionResult AddEquipmentGroup()
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

            var model = new EquipmentGroupViewModel
            {
                EquipmentGroups = _context.EquipmentGroups.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddEquipmentGroup(EquipmentGroupViewModel model)
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

            var equipmentGroup = new EquipmentGroup
            {
                Description = model.Description
            };
            _context.EquipmentGroups.Add(equipmentGroup);
            _context.SaveChanges();

            return RedirectToAction("EquipmentGroups");
        }

        public ActionResult EquipmentTypes()
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
            var model = new EquipmentTypeViewModel
            {
                EquipmentTypes = _context.EquipmentTypes.ToList(),
                EquipmentGroups = _context.EquipmentGroups.ToList()
            };
            return View(model);
        }

        public ActionResult DeleteEquipmentType(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var equipmentType = _context.EquipmentTypes.Find(id);
            if (equipmentType == null)
            {
                return HttpNotFound();
            }
            _context.EquipmentTypes.Remove(equipmentType);
            _context.SaveChanges();
            return RedirectToAction("EquipmentTypes");
        }

        public ActionResult EditEquipmentType(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var equipmentType = _context.EquipmentTypes.Find(id);
            if (equipmentType == null)
            {
                return HttpNotFound();
            }
            PopulateEquipmentTypesDropDownList(equipmentType.EquipmentGroupId);
            return View(equipmentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEquipmentType([Bind(Include = "Id,Name,EquipmentGroupId,EquipmentGroup")] EquipmentType equipmentType)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(equipmentType).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("EquipmentTypes");
            }
            return View(equipmentType);
        }

        private void PopulateEquipmentTypesDropDownList(object selectedProducts = null)
        {
            var query = from d in _context.EquipmentGroups
                        orderby d.Description
                        select d;
            ViewBag.EquipmentGroupId = new SelectList(query, "Id", "Description", selectedProducts);
        }

        public ActionResult AddEquipmentType()
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

            var model = new EquipmentTypeViewModel
            {
                EquipmentGroups = _context.EquipmentGroups.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddEquipmentType(EquipmentTypeViewModel model)
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

            var equipmentType = new EquipmentType
            {
                Name = model.Name,
                EquipmentGroupId = model.EquipmentGroupId,
            };

            _context.EquipmentTypes.Add(equipmentType);
            _context.SaveChanges();
            return RedirectToAction("EquipmentTypes");
        }

        public ActionResult Equipment()
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
            var model = new EquipmentViewModel
            {
                CurrentEquipment = _context.Equipments.ToList(),
                EquipmentTypes = _context.EquipmentTypes.ToList(),
                ListPrices = _context.ListPrices.ToList()
            };
            return View(model);
        }

        public ActionResult AddEquipment()
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

            var model = new EquipmentViewModel
            {
                EquipmentTypes = _context.EquipmentTypes.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddEquipment(EquipmentViewModel model)
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
            if (model.ListPrice != null)
            {
                var listPrice = new ListPrice
                {
                    Price = (decimal) model.ListPrice
                };

                _context.ListPrices.Add(listPrice);

                model.ListPriceId = listPrice.Id;
            }

            var equipment = new Equipment
            {
                Name = model.Name,
                Description = model.Description,
                ListPriceId = model.ListPriceId,
                EquipmentTypeId = model.EquipmentTypeId,
                PictureUrl = model.PictureUrl
            };

            _context.Equipments.Add(equipment);
            _context.SaveChanges();
            return RedirectToAction("Equipment");
        }

        public ActionResult EquipmentRules()
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
            var model = new EquipmentRuleViewModel
            {
                CurrentEquipmentRules = _context.EquipmentRules.ToList(),
                ParentEquipment = _context.Equipments.ToList(),
                ChildEquipment = _context.Equipments.ToList()
            };
            return View(model);
        }

        public ActionResult AddEquipmentRules()
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

            var model = new EquipmentRuleViewModel
            {
                ParentEquipment = _context.Equipments.ToList(),
                ChildEquipment = _context.Equipments.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddEquipmentRules(EquipmentRuleViewModel model)
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

            var equipmentRule = new EquipmentRule()
            {
                Name = model.Name,
                ParentEquipmentId = model.ParentEquipmentId,
                ChildEquipmentId = model.ChildEquipmentId
            };

            _context.EquipmentRules.Add(equipmentRule);
            _context.SaveChanges();
            return RedirectToAction("EquipmentRules");
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result =
                await
                    UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                        new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new {Message = message});
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new {PhoneNumber = model.Number});
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null
                ? View("Error")
                : View(new VerifyPhoneNumberViewModel {PhoneNumber = phoneNumber});
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result =
                await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new {Message = ManageMessageId.AddPhoneSuccess});
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Failed to verify phone");
            return View(model);
        }

        //
        // POST: /Manage/RemovePhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new {Message = ManageMessageId.Error});
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new {Message = ManageMessageId.RemovePhoneSuccess});
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result =
                await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new {Message = ManageMessageId.ChangePasswordSuccess});
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new {Message = ManageMessageId.SetPasswordSuccess});
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess
                    ? "The external login was removed."
                    : message == ManageMessageId.Error
                        ? "An error has occurred."
                        : "";
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins =
                AuthenticationManager.GetExternalAuthenticationTypes()
                    .Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider))
                    .ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"),
                User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new {Message = ManageMessageId.Error});
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded
                ? RedirectToAction("ManageLogins")
                : RedirectToAction("ManageLogins", new {Message = ManageMessageId.Error});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}