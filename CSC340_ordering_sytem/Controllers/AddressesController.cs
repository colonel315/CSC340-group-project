using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CSC340_ordering_sytem.DAL;
using CSC340_ordering_sytem.Models;
using Microsoft.AspNet.Identity;
using MvcFlashMessages;

namespace CSC340_ordering_sytem.Controllers
{
    [Authorize(Roles = "Customer")]
    public class AddressesController : Controller
    {
        private readonly OrderingSystemDbContext _db = new OrderingSystemDbContext();

        // GET: Addresses
        public ActionResult Index()
        {
            var activeUserId = int.Parse(User.Identity.GetUserId());

            var addresses = _db.Addresses.Include(a => a.Customer).Where(x => x.CustomerId == activeUserId);
            return View(addresses.ToList());
        }

        // GET: Addresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = _db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: Addresses/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = User.Identity.GetUserId();
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Street,State,Zip,CustomerId")] Address address)
        {
            if (ModelState.IsValid)
            {
                _db.Addresses.Add(address);
                _db.SaveChanges();
                this.Flash("success", "Address Added!");
                return RedirectToRoute("Addresses", new {action = "Index"});
            }

            ViewBag.CustomerId = User.Identity.GetUserId();
            return View(address);
        }

        // GET: Addresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var activeUserId = int.Parse(User.Identity.GetUserId());
            var address = _db.Addresses.Include(a => a.Customer).First(x => x.CustomerId == activeUserId);

            if (address == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = User.Identity.GetUserId();
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Street,State,Zip,CustomerId")] Address address)
        {
            if (ModelState.IsValid)
            {
                if (int.Parse(User.Identity.GetUserId()) != address.CustomerId)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                _db.Entry(address).State = EntityState.Modified;
                _db.SaveChanges();

                this.Flash("success", "Address Updated!");
                return RedirectToRoute("Addresses", new { action = "Index" });
            }

            ViewBag.CustomerId = User.Identity.GetUserId();
            return View(address);
        }

        // GET: Addresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var activeUserId = int.Parse(User.Identity.GetUserId());
            var address = _db.Addresses.Include(a => a.Customer)
                .First(x => x.CustomerId == activeUserId);

            if (address != null)
                return View(address);

            return HttpNotFound();
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var activeUserId = int.Parse(User.Identity.GetUserId());
            var address = _db.Addresses.Include(a => a.Customer)
                .First(x => x.CustomerId == activeUserId);

            _db.Addresses.Remove(address);
            _db.SaveChanges();
            this.Flash("success", "Address Removed!");
            return RedirectToRoute("Addresses", new { action = "Index" });
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
