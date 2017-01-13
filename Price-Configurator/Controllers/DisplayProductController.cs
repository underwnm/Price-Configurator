using Microsoft.AspNet.Identity;
using Price_Configurator.Models;
using Price_Configurator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;

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
                    Checked = false,
                    ProductId = product.Id
                };

                checkboxList.Add(checkModel);
            }

            var radioList = new List<CheckModel>();
            foreach (var type in equipmentTypes)
            {
                var optional = new List<Equipment>();

                foreach (var equipment in equipmentList)
                {
                    var equipmentType = FindEquipmentType(equipment.EquipmentTypeId);
                    if (!equipmentType.Name.Contains("Optional") ||
                        equipmentType.EquipmentGroupId == null) continue;

                    optional.Add(equipment);
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
            decimal totalPrice = 0;
            var priceQuote = new PriceQuote
            {
                ApplicationUserId = User.Identity.GetUserId(),
                ProductId = selected[0].ProductId,
                SelectedEquipment = new List<CheckModel>(),
                DateCreated = DateTime.Now
            };

            var productId = selected[0].ProductId;
            var product = _context.Products
                .Where(it => it.Id == productId)
                .Select(it => it.ListPrice)
                .SingleOrDefault();

            totalPrice += product;
            foreach (var equipment in selected)
            {
                if (equipment.Checked == false) continue;

                priceQuote.SelectedEquipment.Add(equipment);
                totalPrice += equipment.Price;
            }

            priceQuote.TotalPrice = totalPrice;
            _context.PriceQuotes.Add(priceQuote);
            _context.SaveChanges();

            TempData["selectedEquipment"] = model.CheckModels;
            TempData["selectedProduct"] = productId;
            return RedirectToAction("PriceQuote");
        }

        [HttpGet]
        public ActionResult PriceQuote()
        {
            var equipments = (List<CheckModel>)TempData["selectedEquipment"];
            var productId = (int)TempData["selectedProduct"];
            var user = User.Identity.GetUserId();
            var priceQuote = _context.PriceQuotes
                .Where(x => x.ApplicationUserId == user)
                .OrderByDescending(x => x.DateCreated)
                .FirstOrDefault();
            var product = _context.Products
                .Where(it => it.Id == productId)
                .Select(it => it)
                .Single();

            var selectedEquipment = new List<CheckModel>();
            foreach (var equipment in equipments)
            {
                if (equipment.Checked == false) continue;

                selectedEquipment.Add(equipment);
            }

            var priceModel = new PriceQuoteViewModel
            {
                TotalPrice = priceQuote.TotalPrice,
                SelectedEquipment = selectedEquipment,
                Product = product
                
            };

            return View(priceModel);
        }

        [HttpPost]
        public ActionResult PriceQuote(PriceQuoteViewModel model)
        {
            return RedirectToAction("SubmitQuote");
        }

        public ActionResult SubmitQuote()
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