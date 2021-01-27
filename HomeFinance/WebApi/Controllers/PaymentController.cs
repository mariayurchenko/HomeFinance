using Infrastructure;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController: Controller
    {
        private IPaymentsService _servicePayments;
        private IExpensesService _serviceExpenses;
        public PaymentController(IPaymentsService _servicePayments, IExpensesService _serviceExpenses)
        {
            this._servicePayments = _servicePayments;
            this._serviceExpenses = _serviceExpenses;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Payment payment)
        {
            try
            {
                if (payment == null)
                    return BadRequest();
                var expense = _serviceExpenses.GetExpense(payment.ExpenseID);
                if (expense == null)
                    return NotFound();
                _servicePayments.CreatePayment(payment);
                return this.Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("{month}&{year}")]
        public ActionResult<List<Payment>> Get(int month, int year)
        {
            try
            {
                return _servicePayments.GetMonthPayments(month, year).ToList();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("{date}")]
        public ActionResult<List<Payment>> Get(DateTime date)
        {
            try
            {
                return _servicePayments.GetDayPayments(date).ToList();
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
