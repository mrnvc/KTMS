using KTMS.Application.Modules.StripePayment.Dtos;
using MediatR;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KTMS.Application.Modules.StripePayment.Commands
{
    public class CreateStripePaymentHandler : IRequestHandler<CreateStripePaymentCommand, StripePaymentDto>
    {
        public async Task<StripePaymentDto>Handle(CreateStripePaymentCommand request, CancellationToken cancellationToken)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (int)(request.Amount*100),
                Currency = request.Currency,
                PaymentMethodTypes = new List<string> { "card" }
            };
            var service = new PaymentIntentService();
            var intent = await service.CreateAsync(options, cancellationToken: cancellationToken);
            return new StripePaymentDto
            {
                ClientSecret = intent.ClientSecret,
                Id = intent.Id,
                Status = intent.Status
            };
        }
    }
}
