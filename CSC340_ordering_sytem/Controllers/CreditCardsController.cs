using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CSC340_ordering_sytem.DAL;
using CSC340_ordering_sytem.Models;
using CSC340_ordering_sytem.Models.Helpers;
using CSC340_ordering_sytem.ViewModels;
using Microsoft.AspNet.Identity;

namespace CSC340_ordering_sytem.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CreditCardsController : Controller
    {
        private readonly OrderingSystemDbContext _db = new OrderingSystemDbContext();

        // GET: CreditCards
        public ActionResult Index()
        {
            var creditCards = _db.CreditCards.Include(c => c.Customer);
            return View(creditCards.ToList());
        }

        // GET: CreditCards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = _db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // GET: CreditCards/Create
        public ActionResult Create()
        {
            return View(new CreditCardCreateViewModel());
        }

        // POST: CreditCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Number,ExpMonth,ExpYear")] CreditCardCreateViewModel tempCard)
        {
            if (ModelState.IsValid)
            {
                var activeUserId = int.Parse(User.Identity.GetUserId());

                var creditCard = new CreditCard()
                {
                    Token = CreditCardHelper.GenerateToken(),
                    CustomerId = activeUserId,
                    CardSuffix = tempCard.Number.Substring(tempCard.Number.Length - 4),
                    ExpMonth = tempCard.ExpMonth,
                    ExpYear = tempCard.ExpYear
                };

                _db.CreditCards.Add(creditCard);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(tempCard);
        }


        // GET: CreditCards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditCard = _db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return HttpNotFound();
            }
            return View(creditCard);
        }

        // POST: CreditCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreditCard creditCard = _db.CreditCards.Find(id);
            _db.CreditCards.Remove(creditCard);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
