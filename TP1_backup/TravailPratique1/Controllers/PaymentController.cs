
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using System.Diagnostics.Metrics;
using System.Net;
using System.Xml.Linq;
using TravailPratique1.Models;

namespace TravailPratique1.Controllers
{
    public class PaymentController : Controller
    {
        private readonly StripeSettings _stripeSettings;

        public PaymentController(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "Page de paiement";
            ViewBag.StripePublishableKey = _stripeSettings.PublishableKey;
            return View();
        }

        [HttpGet]
        public IActionResult Failure()
        {
            ViewBag.Title = "Erreur !";
            return View();
        }

        [HttpGet]
        public IActionResult Success()
        {
            ViewBag.Title = "Succès !";
            return View();
        }

        [HttpPost]
        public IActionResult Charge(string stripeToken)
        {
            var chargeOptions = new ChargeCreateOptions
            {
                Amount = 3500,
                Currency = "cad",
                Description = "Livre de programmation en Java",
                Source = stripeToken,

                ReceiptEmail = "john.doe@gmail.com",

                Shipping = new ChargeShippingOptions
                {
                    Name = "John Doe",
                    Address = new AddressOptions
                    {
                        Line1 = "1595 Boul Alphonse-Desjardins",
                        City = "Lévis",
                        State = "QC",
                        PostalCode = "G6V 0A6",
                        Country = "Canada"
                    }
                },

                Metadata = new Dictionary<string, string>
                {
                    { "customer_name", "John Doe" },
                    { "customer_email", "john.doe@gmail.com" },
                    { "customer_address", "1595 Boul Alphonse-Desjardins" },
                    { "customer_city", "Lévis" },
                    { "customer_state", "QC" },
                    { "customer_postal_code", "G6V 0A6" }
                }
            };

            var chargeService = new ChargeService();
            Charge charge = chargeService.Create(chargeOptions);

            if (charge.Status == "succeeded")
            {

                return RedirectToAction("Success");
            }
            else
            {

                return RedirectToAction("Failure");
            }
        }
    }
}
