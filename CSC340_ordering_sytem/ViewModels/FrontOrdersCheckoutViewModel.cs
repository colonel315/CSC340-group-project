using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using CSC340_ordering_sytem.Models;
using CSC340_ordering_sytem.ValidationAttributes;

namespace CSC340_ordering_sytem.ViewModels
{
    public class FrontOrdersCheckoutViewModel
    {
        public int? ExistingCreditCardId { get; set; }

        [RequiredIfOtherFieldIsNull("ExistingCreditCardId")]
        [RegularExpression(@"[0-9]{16}", ErrorMessage = "Invalid credit card number.")]
        public string Number { get; set; }

        [RequiredIfOtherFieldIsNull("ExistingCreditCardId")]
        [RegularExpression(@"[0-9]{2}", ErrorMessage = "Invalid card expiration Month")]
        public string ExpMonth { get; set; }

        [RequiredIfOtherFieldIsNull("ExistingCreditCardId")]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Invalid card expiration Month")]
        public string ExpYear { get; set; }

        
        public int? ExistingAddressId { get; set; }

        [RequiredIfOtherFieldIsNull("ExistingAddressId")]
        [MinLength(5, ErrorMessage = "Street must contain at least 5 characters.")]
        [MaxLength(80, ErrorMessage = "Street may not contain more than 80 characters.")]
        public string Street { get; set; }

        [RequiredIfOtherFieldIsNull("ExistingAddressId")]
        public string City { get; set; }

        [RequiredIfOtherFieldIsNull("ExistingAddressId")]
        [StringLength(2, ErrorMessage = "Please enter the state's two letter abbreviation.")]
        public string State { get; set; }

        [RequiredIfOtherFieldIsNull("ExistingAddressId")]
        [StringLength(5, ErrorMessage = "Zip code is not valid.")]
        public string Zip { get; set; }

        public List<SelectListItem> GetMonths()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem { Text = "(1) January", Value = "01" },
                new SelectListItem { Text = "(2) February", Value = "02" },
                new SelectListItem { Text = "(3) March", Value = "03"},
                new SelectListItem { Text = "(4) April", Value = "04"},
                new SelectListItem { Text = "(5) May", Value = "05"},
                new SelectListItem { Text = "(6) June", Value = "06"},
                new SelectListItem { Text = "(7) July", Value = "07"},
                new SelectListItem { Text = "(8) August", Value = "08"},
                new SelectListItem { Text = "(9) September", Value = "09"},
                new SelectListItem { Text = "(10) October", Value = "10"},
                new SelectListItem { Text = "(11) November", Value = "11"},
                new SelectListItem { Text = "(12) December", Value = "12"}
            };
        }

        public List<SelectListItem> GetYears()
        {
            var currentYear = DateTime.Now.Year;
            var yearsList = new List<SelectListItem>();

            for (var x = currentYear; x < currentYear + 10; x++)
            {
                yearsList.Add(new SelectListItem { Text = $"{x}", Value = $"{x}" });
            }

            return yearsList;
        }

        public List<SelectListItem> ConvertCardsToSelectMenu(ICollection<CreditCard> cards)
        {
            return cards.Select(card => new SelectListItem {Text = $"************{card.CardSuffix}",
                        Value = $"{card.Id}"}).ToList();
        }

        public List<SelectListItem> ConvertAddressesToSelectMenu(ICollection<Address> addresses)
        {
            return addresses.Select(address => new SelectListItem {
                Text = $"{address.Street} {address.City}, {address.State} {address.Zip}",
                Value = $"{address.Id}"
            }).ToList();
        }
    }
}