using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using TravailPratique1.Models;

public class PaymentController : Controller
{
    private readonly StripeSettings _stripeSettings;

    public PaymentController(IOptions<StripeSettings> stripeSettings)
    {
        _stripeSettings = stripeSettings.Value;
        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
    }

    [HttpPost]
    public ActionResult Charge(string stripeToken)
    {
        try
        {
            var options = new ChargeCreateOptions
            {
                Amount = 5000, // $50.00 in cents
                Currency = "usd",
                Description = "Sample Charge",
                Source = stripeToken, // Token from frontend
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);

            return View("Success");
        }
        catch (Exception ex)
        {
            return View("Error");
        }
    }
}
