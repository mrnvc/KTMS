using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTMS.Application.Modules.StripePayment.Queries
{
    public class GetStripePaymentStatusQuery : IRequest<string>
    {
    public string PaymentIntentId { get; }
        public GetStripePaymentStatusQuery(string paymentIntentId)
        {
            PaymentIntentId=paymentIntentId;
        }
    }
}
