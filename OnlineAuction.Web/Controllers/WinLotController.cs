using System;
using System.Linq;
using System.Web.Mvc;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.Entities;
using OnlineAuction.Hubs;
using OnlineAuction.Models;

namespace OnlineAuction.Controllers
{
    [Authorize]
    public class WinLotController : Controller
    {
        private IWinLotManager _manager;
        private IManager<Lot> _lotManager;
        private IUserManager _userManager;

        public WinLotController(IWinLotManager manager, IManager<Lot> lotManager, IUserManager userManager)
        {
            _manager = manager;
            _lotManager = lotManager;
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var winLots = _manager.Get();
                var viewModels = winLots.Select(lot => new WinLotViewModel(lot)).ToList();

                return View(viewModels);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            try
            {
                if (id != null)
                {
                    WinLot winLot = _manager.Get((int)id);
                    var viewModel = new WinLotViewModel(winLot);
                    return View(viewModel);
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id != null)
                {
                    var winLot = _manager.Get((int)id);
                    if (winLot != null)
                    {
                        return View(winLot);
                    }
                }
                return RedirectToAction("Index");
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

        public ActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public void MakeBet(int id, decimal factor)
        {
            try
            {
                var userId = _userManager.GetByEmail(User.Identity.Name).UserId;
                var winLot = _manager.MakeBet(id, userId, factor);
                var lastBet = winLot.Bets.LastOrDefault();
                if (lastBet != null)
                    AuctionHub.NotifyAll(lastBet.Value, winLot.EndTime);               
            }
            catch (Exception)
            {
                //TODO: add error loger
            }

        }

        [HttpGet]
        public ActionResult Create(int? id)
        {
            try
            {
                if (id != null)
                {
                    Lot lot = _lotManager.Get((int)id);
                    if (lot != null)
                    {
                        var winLot = new WinLotViewModel { Lot = lot, LotId = lot.LotId };
                        return View(winLot);
                    }
                }
                return RedirectToAction("Index", "Lot");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WinLotViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.Lot = _lotManager.Get(viewModel.LotId);
                    WinLot winLot = new WinLot();
                    viewModel.SetModel(winLot);
                    _manager.Add(winLot);
                    return RedirectToAction("Index");
                }
                return View(viewModel);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}