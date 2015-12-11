using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CSC340_ordering_sytem.AuthorizeAttributes;
using CSC340_ordering_sytem.DAL;
using CSC340_ordering_sytem.Models;
using CSC340_ordering_sytem.Models.Helpers;
using CSC340_ordering_sytem.Utilities;
using CSC340_ordering_sytem.ViewModels;
using Microsoft.AspNet.Identity;
using MvcFlashMessages;

namespace CSC340_ordering_sytem.Controllers
{
    public class UsersController : Controller
    {
        private readonly OrderingSystemDbContext _db = new OrderingSystemDbContext();

        // GET: Users
        [RedirectCustomerToAccount(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(_db.Users.Where(x => x.Role == "Customer").ToList());
        }
        

        [Authorize(Roles = "Customer")]
        public ActionResult CustomerAccountOverview()
        {
            var userId = int.Parse(User.Identity.GetUserId());
            var user = _db.Users.OfType<Customer>()
                .Where(x => x.Id == userId)
                .Include(x => x.CreditCards)
                .Include(x => x.Addresses)
                .Include(i => i.Orders)
                .Include("Orders.CreditCard")
                .Include("Orders.DeliveryAddress")
                .FirstOrDefault();

            //The customer must have a valid session to access this page
            if (user == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View((Customer)user);
        }

        // GET: Users/Details/5
        /*public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }*/


        // GET: Users/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,FirstName,LastName,Email,Password,Role,ConfirmPassword")] UserCreateViewModel tempUser)
        {
            if (!ModelState.IsValid)
                return View(tempUser);

            var hashedPassword = SHA256Hasher.Create(tempUser.Password);
            var existingCustomer = _db.Users.FirstOrDefault(x => x.Email == tempUser.Email 
                    && x.Password.Equals(hashedPassword));

            if (existingCustomer != null)
            {
                ModelState.AddModelError(string.Empty, "That email is already being used with another account.");
                return View(tempUser);
            }

            var role = (!string.IsNullOrEmpty(tempUser.Role)) ? tempUser.Role : "Customer";

            if (role.Equals("Customer"))
            {
                _db.Users.Add(new Customer
                {
                    FirstName = tempUser.FirstName,
                    LastName = tempUser.LastName,
                    Email = tempUser.Email,
                    Password = SHA256Hasher.Create(tempUser.Password),
                    Role = (!string.IsNullOrEmpty(tempUser.Role)) ? tempUser.Role : "Customer"
                });
            }
            else
            {
                _db.Users.Add(new User
                {
                    FirstName = tempUser.FirstName,
                    LastName = tempUser.LastName,
                    Email = tempUser.Email,
                    Password = hashedPassword,
                    Role = (!string.IsNullOrEmpty(tempUser.Role)) ? tempUser.Role : "Customer"
                });
            }
                
            _db.SaveChanges();

            this.Flash("success", "Registration complete!");
            return RedirectToAction("Login", "Authentication");
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "Customer")]
        public ActionResult Edit()
        {
            var userId = int.Parse(User.Identity.GetUserId());
            var user = _db.Users.Where(x => x.Id == userId).FirstOrDefault();

            if(user == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(new UserEditViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminEditCustomer(int id)
        {
            var customer = UsersHelper.GetUserById(id);

            if (customer == null)
                return HttpNotFound();

            return View("Edit", new UserEditViewModel()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            });
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,Password,ConfirmPassword")] UserEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var user = _db.Users.AsNoTracking().FirstOrDefault(x => x.Id == viewModel.Id);

            if (user == null)
            {
                this.Flash("warning", "Oops! Something when wrong. Please logout, log back in, and then try again.");
                return View(viewModel);
            }

            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            user.Email = viewModel.Email;

            if (!string.IsNullOrEmpty(viewModel.Password))
                user.Password = SHA256Hasher.Create(viewModel.Password);

            if (!string.IsNullOrEmpty(viewModel.Role))
                user.Role = viewModel.Role;
            
            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();
            
            this.Flash("success", "Account Updated!");
            return RedirectToAction("Index");
        }

        // GET: Users/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            var activeUser = UsersHelper.GetUserById(int.Parse(User.Identity.GetUserId()));

            if(activeUser == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if ((id == null && activeUser.Role.Equals("Admin"))
                || (id != null && activeUser.Role.Equals("Customer")))
            {
                    return HttpNotFound();
            }

            if (id == null)
                return View(activeUser);

            var user = _db.Users.Find(id);

            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            var activeUser = UsersHelper.GetUserById(int.Parse(User.Identity.GetUserId()));

            if (activeUser == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = _db.Users.FirstOrDefault(x => x.Role.Equals("Customer"));
            _db.Users.Remove(user);
            _db.SaveChanges();

            this.Flash("success", "Account Deleted");

            if (activeUser.Role.Equals("Admin") && id != activeUser.Id)
            {
                return RedirectToRoute("AdminListCustomers");
            }
            
            return RedirectToRoute("Authentication", "Logout");
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
