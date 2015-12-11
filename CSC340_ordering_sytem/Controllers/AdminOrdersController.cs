using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CSC340_ordering_sytem.DAL;
using CSC340_ordering_sytem.Models;

namespace CSC340_ordering_sytem.Controllers
{
    public class AdminOrdersController : Controller
    {
        private readonly OrderingSystemDbContext _db = new OrderingSystemDbContext();

        // GET: AdminOrders
        public ActionResult Index()
        {
            var order = _db.Order.Where(x => !x.Status.Equals("Complete")).Include(o => o.Cart).Include(o => o.Customer);
            return View(order.ToList());
        }


        public ActionResult PendingOrders()
        {
            var order = _db.Order.Where(x => x.Status.Equals("Pending")).Include(o => o.Cart).Include(o => o.Customer);
            return View(order.ToList());
        }

        [HttpPost]
        public ActionResult PendingOrders(string orderNumber)
        {
            var order = _db.Order.FirstOrDefault(x => x.OrderNumber.Equals(orderNumber));

            if (order != null)
            {
                order.Status = "Preparing";
                _db.Entry(order).State = EntityState.Modified;
                _db.SaveChanges();
            }

            var orders = _db.Order.Where(x => x.Status.Equals("Pending")).Include(o => o.Cart).Include(o => o.Customer);
            return View(orders.ToList());
        }

        public ActionResult PreparingOrders()
        {
            var order = _db.Order.Where(x => x.Status.Equals("Preparing")).Include(o => o.Cart).Include(o => o.Customer);
            return View(order.ToList());
        }

        [HttpPost]
        public ActionResult PreparingOrders(string orderNumber)
        {
            var order = _db.Order.FirstOrDefault(x => x.OrderNumber.Equals(orderNumber));

            if (order != null)
            {
                order.Status = "Delivering";
                _db.Entry(order).State = EntityState.Modified;
                _db.SaveChanges();
            }

            var orders = _db.Order.Where(x => x.Status.Equals("Preparing")).Include(o => o.Cart).Include(o => o.Customer);
            return View(orders.ToList());
        }

        public ActionResult DeliveryOrders()
        {
            var order = _db.Order.Where(x => x.Status.Equals("Delivering")).Include(o => o.Cart).Include(o => o.Customer);
            return View(order.ToList());
        }

        [HttpPost]
        public ActionResult DeliveryOrders(string orderNumber)
        {
            var order = _db.Order.FirstOrDefault(x => x.OrderNumber.Equals(orderNumber));

            if (order != null)
            {
                order.Status = "Complete";
                _db.Entry(order).State = EntityState.Modified;
                _db.SaveChanges();
            }

            var orders = _db.Order.Where(x => x.Status.Equals("Delivering")).Include(o => o.Cart).Include(o => o.Customer);
            return View(orders.ToList());
        }

        public ActionResult TodaysCompletedOrders()
        {
            var order = _db.Order.Where(x => x.Status.Equals("Complete")).Include(o => o.Cart).Include(o => o.Customer);
            return View(order.ToList());
        }



        // GET: AdminOrders/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: AdminOrders/Create
        public ActionResult Create()
        {
            ViewBag.CartId = new SelectList(_db.Carts, "Id", "Id");
            ViewBag.CustomerId = new SelectList(_db.Users, "Id", "FirstName");
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
                _db.Order.Add(order);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CartId = new SelectList(_db.Carts, "Id", "Id", order.CartId);
            ViewBag.CustomerId = new SelectList(_db.Users, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // GET: AdminOrders/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CartId = new SelectList(_db.Carts, "Id", "Id", order.CartId);
            ViewBag.CustomerId = new SelectList(_db.Users, "Id", "FirstName", order.CustomerId);
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
                _db.Entry(order).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CartId = new SelectList(_db.Carts, "Id", "Id", order.CartId);
            ViewBag.CustomerId = new SelectList(_db.Users, "Id", "FirstName", order.CustomerId);
            return View(order);
        }

        // GET: AdminOrders/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _db.Order.Find(id);
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
            Order order = _db.Order.Find(id);
            _db.Order.Remove(order);
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
