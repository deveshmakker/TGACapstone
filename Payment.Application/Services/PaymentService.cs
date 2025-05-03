
using AutoMapper;
using Payment.Application.DTO;
using Payment.Application.Interfaces;
using Payment.Common.Model;
using Payment.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public PaymentDTO GetPaymentDetails(int paymentid)
        {
            return _mapper.Map<PaymentDTO>(_paymentRepository.GetPaymentDetails);
        }

        public bool ProcessPayment(PaymentDTO payment)
        {
            return _paymentRepository.ProcessPayment(_mapper.Map<PaymentDetails>(payment));
        }

        public bool RefundPayment(int paymentid)
        {
            return _paymentRepository.RefundPayment(paymentid);
        }
    }
}
