using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using CSC340_ordering_sytem.DAL;
using CSC340_ordering_sytem.Models;
using CSC340_ordering_sytem.Models.Helpers;
using CSC340_ordering_sytem.ViewModels;
using Microsoft.AspNet.Identity;
using MvcFlashMessages;

namespace CSC340_ordering_sytem.Controllers
{
    /// <summary>
    /// Handles the front end checkout process.
    /// </summary>
    public class FrontOrdersController : Controller
    {
        //Database access context
        private readonly OrderingSystemDbContext _db = new OrderingSystemDbContext();


        /// <summary>
        /// Displays the forms that allow the customer to select or add new addresses
        /// and credit cards.
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Checkout()
        {
            //Get logged in customer's Id
            var customerId = int.Parse(User.Identity.GetUserId());

            //Store customer in Viewbag to be accessed by the view
            ViewBag.Customer = _db.Users.OfType<Customer>().Where(x => x.Id == customerId)
                                    .Include("Addresses").Include("CreditCards").FirstOrDefault();

            //There is a chance that the order has already been created, but hasn't been submitted yet.
            //For instance,the customer may have returned to this page to change billing information.
            //This will check if the an existing order is in progress and display its information rather than
            //a blank slate.
            var order = _db.Order.FirstOrDefault(x => x.CustomerId == customerId && x.Status.Equals("In Progress"));
            FrontOrdersCheckoutViewModel checkoutOrder;
            if (order != null)
            {
                checkoutOrder = new FrontOrdersCheckoutViewModel
                {
                    ExistingCreditCardId = order.CreditCardId,
                    ExistingAddressId = order.DeliveryAddressId
                };
            }
            else
            {
                checkoutOrder = new FrontOrdersCheckoutViewModel();
            }

            return View(checkoutOrder);
        }


        /// <summary>
        /// Handles the form submition from the checkout page.
        /// </summary>
        /// <param name="checkoutOrder">The data submitted from the form.</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Checkout([Bind(Include = "ExistingCreditCardId, Number, ExpMonth, ExpYear, ExistingAddressId, Street, City, State, Zip")] FrontOrdersCheckoutViewModel checkoutOrder)
        {
            //If an existing credit card was selected, the data in
            //the "new card" fields should not be validated.
            if (checkoutOrder.ExistingCreditCardId != null)
            {
                //Clear errors from "new card" fields if any exist
                ModelState["Number"].Errors.Clear();
                ModelState["ExpMonth"].Errors.Clear();
                ModelState["ExpYear"].Errors.Clear();

                //Set the "new card" fields to empty
                checkoutOrder.Number = null;
                checkoutOrder.ExpMonth = null;
                checkoutOrder.ExpYear = null;
            }

            //If an existing address was selected, the data in
            //the "new address" fields should not be validated.
            if (checkoutOrder.ExistingAddressId != null)
            {
                //Clear errors from "new address" fields if any exist
                ModelState["Street"].Errors.Clear();
                ModelState["City"].Errors.Clear();
                ModelState["State"].Errors.Clear();
                ModelState["Zip"].Errors.Clear();

                //Set the "new address" fields to empty
                checkoutOrder.Street = null;
                checkoutOrder.City = null;
                checkoutOrder.State = null;
                checkoutOrder.Zip = null;
            }

            //Fetch the logged in customer from the DB
            var customerId = int.Parse(User.Identity.GetUserId());
            var customer = _db.Users.OfType<Customer>().Where(x => x.Id == customerId)
                                    .Include("Addresses").Include("CreditCards")
                                    .Include("Cart").First();

            //Make the customer's data available in the view
            ViewBag.Customer = customer;

            //If validation errors were found, display them to the customer
            if (!ModelState.IsValid)
            {
                this.Flash("danger", "Errors were found. Please correct them.");
                return View(checkoutOrder);
            }

            //Stores the ID of the existing or new credit card. This will be assigned
            //to the order object.
            int cardId;

            //If the customer didn't select an existing credit card,
            //create a new one from the "new card" form fields.
            if (checkoutOrder.ExistingCreditCardId == null)
            {
                var card = new CreditCard()
                {
                    Token = CreditCardHelper.GenerateToken(),
                    CardSuffix = CreditCardHelper.GetCardPrefix(checkoutOrder.Number),
                    ExpMonth = checkoutOrder.ExpMonth,
                    ExpYear = checkoutOrder.ExpYear,
                    CustomerId = customerId
                };

                _db.CreditCards.Add(card);
                _db.SaveChanges();

                //Store the ID of the new card so it may be assigned to the order
                cardId = card.Id;
            }
            else
            {
                //An existing card was selected. Use it's ID to assign to the order
                cardId = (int)checkoutOrder.ExistingCreditCardId;
            }

            //Stores the ID of the existing or new address. This will be assigned
            //to the order object.
            int addressId;

            //If the customer didn't select an existing address,
            //create a new one from the "new address" form fields.
            if (checkoutOrder.ExistingAddressId == null)
            {
                var address = new Address()
                {
                    Street = checkoutOrder.Street,
                    City = checkoutOrder.City,
                    State = checkoutOrder.State,
                    Zip = checkoutOrder.Zip,
                    CustomerId = customerId
                };

                _db.Addresses.Add(address);
                _db.SaveChanges();

                //Store the ID of the new address so it may be assigned to the order
                addressId = address.Id;
            }
            else
            {
                //An existing address was selected. Use it's ID to assign to the order
                addressId = (int)checkoutOrder.ExistingAddressId;
            }

            //Check if an order is already in progress. This will happen if the user returns to this page
            //to change order details such as billing and delivery info.
            var order = _db.Order.FirstOrDefault(x => x.CustomerId == customerId && x.Status.Equals("In Progress"));

            //If an existing order doesn't exist, create a new one
            if (order == null)
            {
                order = new Order()
                {
                    OrderNumber = OrderHelper.GenerateOrderNumber(),
                    Status = "In Progress",
                    CustomerId = customerId,
                    DeliveryAddressId = addressId,
                    CreditCardId = cardId,
                    CartId = (int) customer.CartId
                };
                _db.Order.Add(order);
            }
            else
            {
                //If an existing order already exists, updated the billing and address info
                order.DeliveryAddressId = addressId;
                order.CreditCardId = cardId;
            }

            //Save order updates
            _db.SaveChanges();

            //Redirect to order confirmation page
            return RedirectToAction("Confirm");
        }


        /// <summary>
        /// Displays the customer's order information for them to review.
        /// </summary>
        /// <returns>ActionResults</returns>
        public ActionResult Confirm()
        {
            var customerId = int.Parse(User.Identity.GetUserId());

            //Fetch the customer's existing order
            var order = _db.Order.Where(x => x.CustomerId == customerId)
                            .Include(x => x.Cart.CartItems)
                            .Include(x => x.DeliveryAddress)
                            .Include(x => x.CreditCard)
                            .FirstOrDefault();

            //If the order doesn't exist, they should be accessing this page, so
            //they will be redirected to the the cart page.
            if (order == null)
            {
                return RedirectToAction("ShowCart", "Carts");
            }

            //Load all of the menu item details into memory
            foreach (var item in order.Cart.CartItems)
            {
                item.MenuItem = _db.MenuItems.Find(item.MenuItemId);
            }

            //Display the confirmation page
            return View(order);
        }


        /// <summary>
        /// Handles the order confirmation submission.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ConfirmSubmit()
        {
            var customerId = int.Parse(User.Identity.GetUserId());

            //Get the customer's info
            var customer = _db.Users.OfType<Customer>().First(x => x.Id == customerId);

            //Fetch the existing order
            var order = _db.Order.FirstOrDefault(x => x.CustomerId == customerId && x.Status.Equals("In Progress"));

            //If the order doesn't exist, they should be accessing this page, so
            //they will be redirected to the the cart page.
            if (order == null)
            {
                return RedirectToAction("ShowCart", "Carts");
            }

            //Remove the cart from the customer to reset it. This way, a new cart
            //will be generated when they attempt to make an order in the future.
            customer.CartId = null;
            _db.Entry(customer).State = EntityState.Modified;

            //Set the order status to "Pending"
            order.Status = "Pending";
            _db.Entry(order).State = EntityState.Modified;
            _db.SaveChanges();

            //Redirect the the "Thank You" page
            return RedirectToAction("ThankYou", new { orderNumber = order.OrderNumber });
        }


        /// <summary>
        /// Displays a "Thank You" message the the order number to the customer.
        /// </summary>
        /// <param name="orderNumber">The OrderNumber of an existing order.</param>
        /// <returns>ActionResult</returns>
        public ActionResult ThankYou(string orderNumber)
        {
            //Fetch the order from the DB
            var order = _db.Order.FirstOrDefault(x => x.OrderNumber.Equals(orderNumber));

            //If the order doesn't exist, throw a 404
            if (order == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.OrderNumber = orderNumber.ToUpper();
            return View();
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
