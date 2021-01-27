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
    public class IncomeController : Controller
    {
        private IIncomesService _serviceIncomes;
        public IncomeController(IIncomesService _serviceIncomes)
        {
            this._serviceIncomes = _serviceIncomes;
        }
        
        [HttpGet]
        public ActionResult<List<Income>> Get()
        {
            try
            {
                return _serviceIncomes.AllIncomes.ToList();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Income> Get(int id)
        {
            try
            {
                var income = _serviceIncomes.GetIncome(id);
                if (income == null)
                    return NotFound();
                return income;
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Income income)
        {
            try
            {
                if (string.IsNullOrEmpty(income.Name))
                    return StatusCode(500);

                _serviceIncomes.CreateIncome(income);
                return this.Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var income = _serviceIncomes.GetIncome(id);
                if (income != null)
                {
                    _serviceIncomes.DeleteIncome(id);
                }
                return this.Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
