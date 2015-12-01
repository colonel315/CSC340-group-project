using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CSC340_ordering_sytem.DAL;
using CSC340_ordering_sytem.Models;

namespace CSC340_ordering_sytem.Controllers
{
    public class MenuItemsController : Controller
    {
        private readonly OrderingSystemDbContext _db = new OrderingSystemDbContext();

        // GET: MenuItems
        public ActionResult Index()
        {
            return View(_db.MenuItems.ToList());
        }

        // GET: MenuItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = _db.MenuItems.Find(id);
            if (menuItem == null)
            {
                return HttpNotFound();
            }
            return View(menuItem);
        }

        // GET: MenuItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price")] MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                _db.MenuItems.Add(menuItem);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menuItem);
        }

        // GET: MenuItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = _db.MenuItems.Find(id);
            if (menuItem == null)
            {
                return HttpNotFound();
            }
            return View(menuItem);
        }

        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price")] MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(menuItem).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menuItem);
        }

        // GET: MenuItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = _db.MenuItems.Find(id);
            if (menuItem == null)
            {
                return HttpNotFound();
            }
            return View(menuItem);
        }

        // POST: MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuItem menuItem = _db.MenuItems.Find(id);
            _db.MenuItems.Remove(menuItem);
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
