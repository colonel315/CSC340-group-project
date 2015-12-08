using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CSC340_ordering_sytem.DAL;
using CSC340_ordering_sytem.Models;

namespace CSC340_ordering_sytem.Controllers
{
    [Authorize(Roles = "Customer")]
    public class FrontMenuController : Controller
    {
        private readonly OrderingSystemDbContext _db = new OrderingSystemDbContext();
       
        // GET: FrontMenu
        public ActionResult ListCategories()
        {
            return View(_db.Categories.ToList());
        }
        

        public ActionResult CategoryItems(string slug)
        {
            var category = _db.Categories.Where(x => x.Url.Equals(slug)).Include("MenuItems").FirstOrDefault();

            if (category == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(category);
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
