using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CSC340_ordering_sytem.Models;

namespace CSC340_ordering_sytem.Controllers
{
    public class AdminOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminOrders
        public ActionResult Index()
        {
            var order = db.Order.Include(o => o.Cart).Include(o => o.Customer);
            return View(order.ToList());
        }

        // GET: AdminOrders/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: AdminOrders/Create
        public ActionResult Create()
        {
            ViewBag.CartId = new SelectList(db.Carts, "Id", "Id");
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: AdminOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderNumber,Status,CustomerId,CartId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Order.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CartId = new SelectList(db.Carts, "Id", "Id", order.CartId);
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // GET: AdminOrders/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CartId = new SelectList(db.Carts, "Id", "Id", order.CartId);
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // POST: AdminOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderNumber,Status,CustomerId,CartId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CartId = new SelectList(db.Carts, "Id", "Id", order.CartId);
            ViewBag.CustomerId = new SelectList(db.Users, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // GET: AdminOrders/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: AdminOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
