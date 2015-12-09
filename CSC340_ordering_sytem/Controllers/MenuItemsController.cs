using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CSC340_ordering_sytem.Models;
using CSC340_ordering_sytem.Utilities;
using CSC340_ordering_sytem.ViewModels;

namespace CSC340_ordering_sytem.Controllers
{
    public class MenuItemsController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: MenuItems
        public ActionResult Index(string slug)
        {
            var category = _db.Categories.FirstOrDefault(x => x.Url == slug);

            if (category == null)
            {
                return HttpNotFound();
            }

            var menuItems = category.MenuItems; //_db.MenuItems.Include(m => m.Category);
            return View(menuItems.ToList());
        }

        // GET: MenuItems/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name");
            return View(new MenuItemSaveViewModel());
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CategoryId,Price,Image")] MenuItemSaveViewModel menuItem)
        {
            if (menuItem.Image == null || menuItem.Image.ContentLength == 0)
            {
                ModelState.AddModelError("Image", "Please select an image to upload.");
            }
            else if (!ImageUploader.IsValidImageType(menuItem.Image))
            {
                ModelState.AddModelError("Image", "The file you are attempting to upload is not an image.");
            }

            if (ModelState.IsValid)
            {
                var imageUrl = ImageUploader.Upload(menuItem.Image);
                _db.MenuItems.Add(new MenuItem()
                {
                    Name = menuItem.Name,
                    Price = menuItem.Price,
                    CategoryId = menuItem.CategoryId,
                    Image = imageUrl
                });
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name", menuItem.CategoryId);
            return View(menuItem);
        }

        // GET: MenuItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = _db.MenuItems.Where(x => x.Id == id).Include("Category").FirstOrDefault();
            if (menuItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name", menuItem.CategoryId);
            return View(new MenuItemSaveViewModel()
            {
                Name = menuItem.Name,
                Price = menuItem.Price,
                CategoryId = menuItem.CategoryId,
                Category = menuItem.Category,
                CurrentImageUrl = menuItem.Image
            });
        }

        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CategoryId,Price, Image")] MenuItemSaveViewModel menuItem)
        {
            if (ModelState.IsValid)
            {
                var item = _db.MenuItems.Find(menuItem.Id);

                if (item == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                item.Name = menuItem.Name;
                item.Price = menuItem.Price;
                item.CategoryId = menuItem.CategoryId;

                if (menuItem.Image != null && menuItem.Image.ContentLength > 0)
                {
                    item.Image = ImageUploader.Upload(menuItem.Image);
                }

                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name", menuItem.CategoryId);
            return View(menuItem);
        }

        // GET: MenuItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = _db.MenuItems.Where(x => x.Id == id).Include("Category").FirstOrDefault();
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
