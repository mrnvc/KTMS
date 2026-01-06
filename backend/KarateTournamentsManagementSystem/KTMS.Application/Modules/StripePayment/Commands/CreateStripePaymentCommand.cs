using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTMS.Application.Modules.StripePayment.Dtos;
using MediatR;

namespace KTMS.Application.Modules.StripePayment.Commands
{
    public class CreateStripePaymentCommand : IRequest<StripePaymentDto>
    {
        public required decimal Amount { get; set; }
        public required string Currency {  get; set; }
        public CreateStripePaymentCommand(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }


    }
}
