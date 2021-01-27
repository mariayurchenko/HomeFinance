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
    public class AdmissionController : Controller
    {
        private IAdmissionsService _serviceAdmissions;
        private IIncomesService _serviceIncomes;
        public AdmissionController(IAdmissionsService _serviceAdmissions, IIncomesService _serviceIncomes)
        {
            this._serviceAdmissions = _serviceAdmissions;
            this._serviceIncomes = _serviceIncomes;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Admission admission)
        {
            try
            {
                if (admission == null)
                    return BadRequest();
                var income = _serviceIncomes.GetIncome(admission.IncomeID);
                if (income == null)
                    return NotFound();
                _serviceAdmissions.CreateAdmission(admission);
                return this.Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("{month}&{year}")]
        public ActionResult<List<Admission>> Get(int month, int year)
        {
            try
            {
                return _serviceAdmissions.GetMonthAdmissions(month, year).ToList();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("{date}")]
        public ActionResult<List<Admission>> Get(DateTime date)
        {
            try
            {
                return _serviceAdmissions.GetDayAdmissions(date).ToList();
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
