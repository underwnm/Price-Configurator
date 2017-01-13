using Microsoft.AspNet.Identity;
using Price_Configurator.Models;
using Price_Configurator.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Price_Configurator.Controllers
{
    public class DisplayProductController : Controller
    {
        private ApplicationDbContext _context;

        public DisplayProductController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize()]
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

        public ActionResult SelectedProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _context.Products
                .Where(p => p.Id == id)
                .Select(p => p)
                .Single();

            var equipmentList = _context.ProductsEquipment
                .Where(p => p.ProductId == id)
                .Select(e => e.Equipment)
                .ToList();

            var equipmentTypes = _context.EquipmentTypes
                .Select(x => x)
                .ToList();

            var equipmentGroups = _context.EquipmentGroups
                .Select(x => x)
                .ToList();

            var checkboxList = new List<CheckModel>();
            for (var index = 0; index < equipmentList.Count; index++)
            {
                var equipmentType = FindEquipmentType(equipmentList[index].EquipmentTypeId);
                if (!equipmentType.Name.Contains("Optional") ||
                    equipmentType.EquipmentGroupId != null) continue;

                var checkModel = new CheckModel
                {
                    Id = equipmentList[index].Id,
                    Name = equipmentList[index].Name,
                    Price = equipmentList[index].ListPrice,
                    Checked = false
                };

                checkboxList.Add(checkModel);
            }

            var radioList = new List<CheckModel>();
            foreach (var type in equipmentTypes)
            {
                var optional = new List<Equipment>();

                for (var i = 0; i < equipmentList.Count; i++)
                {
                    var equipmentType = FindEquipmentType(equipmentList[i].EquipmentTypeId);
                    if (!equipmentType.Name.Contains("Optional") ||
                        equipmentType.EquipmentGroupId == null) continue;

                    optional.Add(equipmentList[i]);
                }

                if (optional.Count <= 0) continue;
                {
                    string groupName = null;
                    for (var i = 0; i < optional.Count; i++)
                    {
                        foreach (var group in equipmentGroups)
                        {
                            if (group.Id == type.EquipmentGroupId)
                            {
                                groupName = group.Description;
                            }
                        }

                        var checkModel = new CheckModel
                        {
                            Id = equipmentList[i].Id,
                            Description = groupName,
                            Name = equipmentList[i].Name,
                            Price = equipmentList[i].ListPrice,
                            Checked = false
                        };

                        radioList.Add(checkModel);
                        equipmentList.Remove(optional[i]);
                    }
                }
            }

            var model = new SelectionViewModel
            {
                Product = product,
                CheckModels = checkboxList,
                RadioList = radioList
            };

            return View(model);
        }

        private EquipmentType FindEquipmentType(int id)
        {
            return _context.EquipmentTypes
                .Where(t => t.Id == id)
                .Select(x => x)
                .SingleOrDefault();
        }

        [HttpPost]
        public ActionResult SelectedProduct(SelectionViewModel model)
        {
            var selected = model.CheckModels;
            for (int i = 0; i < selected.Count; i++)
            {
                if (selected[i].Checked == false) continue;

                var priceQuote = new PriceQuote
                {
                    ApplicationUserId = User.Identity.GetUserId(),
                    EquipmentId = selected[i].Id,
                    Name = selected[i].Name,
                    Description = selected[i].Description,
                    ListPrice = selected[i].Price
                };
            }
            return RedirectToAction("PriceQuote");
        }

        public ActionResult PriceQuote()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _context != null)
            {
                _context.Dispose();
                _context = null;
            }


            base.Dispose(disposing);
        }
    }
}