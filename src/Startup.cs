using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using Stripe.Checkout;



public class StripeOptions
{
    public string option { get; set; }
}

namespace ToyChange
{
    public class Startup
    {





        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //StripeConfiguration.ApiKey = Configuration.GetValue<string>("Stripe:SecretKey");
            //StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

        }

        public IConfiguration Configuration { get; }



        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddMvc().AddNewtonsoftJson();
        //}
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // This is a public sample test API key.
            // Don’t submit any personally identifiable information in requests made with this key.
            // Sign in to see your own test API key embedded in code samples.
            StripeConfiguration.ApiKey = Configuration.GetValue<string>("Stripe:SecretKey");
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }

    [Route("create-checkout-session")]
    [ApiController]
    public class CheckoutApiController : Controller
    {
        [HttpPost]
        public ActionResult Create()
        {
            var domain = "https://localhost:7014";
            var options = new SessionCreateOptions {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = "prod_Mb2uEgXmd6kCV4",
                    Quantity = 1,
                  },
                },
                Mode = "payment",
                SuccessUrl = domain + "/success.html",
                CancelUrl = domain + "/cancel.html",
            };
            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }

}

