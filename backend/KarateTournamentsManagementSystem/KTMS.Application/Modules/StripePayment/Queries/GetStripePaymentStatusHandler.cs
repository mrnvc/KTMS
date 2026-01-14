using MediatR;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.StripePayment.Queries
{
    public class GetStripePaymentStatusHandler: IRequestHandler<GetStripePaymentStatusQuery, string>
    {
        public async Task<string> Handle(GetStripePaymentStatusQuery request, CancellationToken cancellationToken )
        {
            var service = new PaymentIntentService();
            var intent= await service.GetAsync(request.PaymentIntentId, cancellationToken:  cancellationToken);
            return intent.Status;
        }
    }
}
