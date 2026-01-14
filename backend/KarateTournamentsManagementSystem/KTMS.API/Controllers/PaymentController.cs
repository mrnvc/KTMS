using KTMS.Application.Modules.StripePayment.Commands;
using KTMS.Application.Modules.StripePayment.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using KTMS.API.Configuration;
using Microsoft.Extensions.Options;


namespace KTMS.API.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly StripeSettings _stripeSettings;
        public PaymentController(IMediator mediator, IOptions<StripeSettings> stripeSettings)
        {
            _mediator = mediator;
            _stripeSettings = stripeSettings.Value;
        }
        [HttpPost("CreateStripePayment")]
        public async Task<IActionResult> CreateStripePayment([FromBody] CreateStripePaymentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet("status/{id}")]
        public async Task<IActionResult> GetStripePaymentStatus(string id)
        {
            var status = await _mediator.Send(new GetStripePaymentStatusQuery(id));
            return Ok(new { status });
        }
        [HttpPost("Webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var JSON = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    JSON,
                    Request.Headers["Stripe-Signature"],
                    _stripeSettings.Webhook
                    
                    );
                if (stripeEvent.Type == "payment_intent.succeeded")
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent; 
                    Console.WriteLine($"Payment succeeded: {paymentIntent.Id}");
                } 
                else if (stripeEvent.Type == "payment_intent.payment_failed") {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent; 
                    Console.WriteLine($"Payment failed: {paymentIntent.Id}");
                } 
                return Ok();
            }
            catch (StripeException e) { 
                Console.WriteLine($" Stripe error: {e.Message}");
                return BadRequest(); 
            }
                }
        }
    }

