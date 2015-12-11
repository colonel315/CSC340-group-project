using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CSC340_ordering_sytem.DAL;
using CSC340_ordering_sytem.Models;
using CSC340_ordering_sytem.ViewModels;
using Microsoft.AspNet.Identity;
using MvcFlashMessages;

namespace CSC340_ordering_sytem.Controllers
{
    public class CartsController : Controller
    {
        private readonly OrderingSystemDbContext _db = new OrderingSystemDbContext();

        // GET: Carts
        public ActionResult ShowCart()
        {
            var userId = int.Parse(User.Identity.GetUserId());
            
            var customer = _db.Users.OfType<Customer>().Where(c => c.Id == userId)
                .Include(c => c.Cart).Include(c => c.Cart.CartItems).First();

            if (customer.Cart?.CartItems != null)
            {
                foreach (var item in customer.Cart.CartItems)
                {
                    _db.Entry(item).Reference(x => x.MenuItem).Load();
                }
            }

            return View(customer);
        }

        // POST: AddItemToCart
        [HttpPost]
        public ActionResult AddItemToCart(int Id)
        {
            var menuItem = _db.MenuItems.Where(x => x.Id == Id).Include("Category").FirstOrDefault();

            if (menuItem != null)
            {
                var userId = int.Parse(User.Identity.GetUserId());
                var customer = _db.Users.OfType<Customer>().Where(c => c.Id == userId)
                .Include(c => c.Cart).Include(c => c.Cart.CartItems).First();

                if(customer.Cart == null)
                {
                    customer.Cart = new Cart();
                    _db.Entry(customer.Cart).State = EntityState.Added;
                    _db.Entry(customer).State = EntityState.Modified;
                    _db.SaveChanges();
                }

                var cartItem = new CartItem()
                {
                    MenuItemId = Id,
                    CartId = (int)customer.Cart.Id,
                    Quantity = 1
                };

                _db.CartItems.Add(cartItem);
                customer.CartId = customer.Cart.Id;
                _db.Entry(customer).State = EntityState.Modified;
                _db.SaveChanges();
                this.Flash("success", "Item Added to Cart");
                return RedirectToRoute("MenuCategoryProducts", new { slug = menuItem.Category.Url});
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult UpdateItemInCart([Bind(Include = "Id,Quantity")]  CartItem updateItem)
        {
            var item = _db.CartItems.Where(x => x.Id == updateItem.Id).Include("MenuItem").FirstOrDefault();
            if (item == null)
            {
                this.Flash("danger", "That item does not exist");
            }
            
            if (ModelState.IsValid)
            {
                if (item.Quantity < 1)
                {
                    this.Flash("danger", $"You must order at least 1 {item.MenuItem.Name}");
                    return RedirectToAction("ShowCart");
                }

                item.Quantity = updateItem.Quantity;
                _db.Entry(item).State = EntityState.Modified;
                _db.SaveChanges();
                this.Flash("success", "The item was successfully updated");
            }
            else
            {
                this.Flash("danger", "The item did not updated");
            }
            
            return RedirectToAction("ShowCart");
        }

        //  TODO
//        public ActionResult RemoveItemFromCart(int? id)
//        {
//            
//        }

        // GET: Carts
        //        public ActionResult Index()
        //        {
        //            return View(_db.Carts.ToList());
        //        }

        // GET: Carts/Details/5
        //        public ActionResult Details(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            Cart cart = _db.Carts.Find(id);
        //            if (cart == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(cart);
        //        }
        //
        //        // GET: Carts/Create
        //        public ActionResult Create()
        //        {
        //            return View();
        //        }
        //
        //        // POST: Carts/Create
        //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult Create([Bind(Include = "Id")] Cart cart)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                _db.Carts.Add(cart);
        //                _db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }
        //
        //            return View(cart);
        //        }
        //
        //        // GET: Carts/Edit/5
        //        public ActionResult Edit(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            Cart cart = _db.Carts.Find(id);
        //            if (cart == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(cart);
        //        }
        //
        //        // POST: Carts/Edit/5
        //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult Edit([Bind(Include = "Id")] Cart cart)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                _db.Entry(cart).State = EntityState.Modified;
        //                _db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }
        //            return View(cart);
        //        }
        //
        //        // GET: Carts/Delete/5
        //        public ActionResult Delete(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            Cart cart = _db.Carts.Find(id);
        //            if (cart == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(cart);
        //        }
        //
        //        // POST: Carts/Delete/5
        //        [HttpPost, ActionName("Delete")]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult DeleteConfirmed(int id)
        //        {
        //            Cart cart = _db.Carts.Find(id);
        //            _db.Carts.Remove(cart);
        //            _db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

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
