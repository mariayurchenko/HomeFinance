using Infrastructure;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class SumController: Controller 
    {
        private IPaymentsService _servicePayments;
        private IAdmissionsService _serviceAdmissions;
        public SumController(IPaymentsService _servicePayments, IAdmissionsService _serviceAdmissions)
        {
            this._servicePayments = _servicePayments;
            this._serviceAdmissions = _serviceAdmissions;
        }
        [HttpGet("{month}&{year}")]
        public ActionResult<CashAccount> Get(int month, int year)
        {
            try
            {
                var admissions = (_serviceAdmissions.GetMonthAdmissions(month, year)).ToList();
                var payments = (_servicePayments.GetMonthPayments(month, year)).ToList();
                CalculateBalance calc = new CalculateBalance(admissions, payments);
                return calc.GetCashAccount();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("{date}")]
        public ActionResult<CashAccount> Get(DateTime date)
        {
            try
            {
                var admissions = (_serviceAdmissions.GetDayAdmissions(date)).ToList();
                var payments = (_servicePayments.GetDayPayments(date)).ToList();
                CalculateBalance calc = new CalculateBalance(admissions, payments);
                return calc.GetCashAccount();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
