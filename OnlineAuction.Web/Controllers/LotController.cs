using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Entities;
using OnlineAuction.Models;

namespace OnlineAuction.Controllers
{
    [Authorize(Roles = "admin")]
    public class LotController : Controller
    {
        private IManager<Lot> _manager;

        public LotController(IManager<Lot> manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var lots = _manager.Get();
                return View(lots);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                return View(new LotViewModel(time: 10, price: 10));
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        [HttpPost]
        public ActionResult Create(LotViewModel model, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        using (var reader = new BinaryReader(upload.InputStream))
                        {
                            model.Image = reader.ReadBytes(upload.ContentLength);
                        }
                    }
                    var lot = new Lot();
                    model.SetModel(lot);
                    _manager.Add(lot);
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                Lot lot = _manager.Get(id);
                if (lot == null)
                {
                    return HttpNotFound();
                }
                return View(lot);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _manager.Remove(id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                Lot lot = _manager.Get(id);
                if (lot == null)
                {
                    return HttpNotFound();
                }
                return View(lot);
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                Lot lot = _manager.Get(id);
                if (lot == null)
                {
                    return HttpNotFound();
                }
                return View(new LotViewModel(lot));
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LotViewModel model, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        using (var reader = new BinaryReader(upload.InputStream))
                        {
                            model.Image = reader.ReadBytes(upload.ContentLength);
                        }
                    }
                    var lot = new Lot();
                    model.SetModel(lot);
                    _manager.Update(lot);
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}