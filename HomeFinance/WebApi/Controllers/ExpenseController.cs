using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Infrastructure.Repository;
using Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ExpenseController: Controller
    {
        private IExpensesService _serviceExpenses;
        public ExpenseController(IExpensesService _serviceExpenses)
        {
            this._serviceExpenses = _serviceExpenses;
        }
        [HttpGet]
        public ActionResult<List<Expense>> Get()
        {
            try
            {
                return _serviceExpenses.AllExpenses.ToList();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Expense> Get(int id)
        {
            try
            {
                var expense = _serviceExpenses.GetExpense(id);
                if (expense == null)
                    return NotFound();
                return expense;
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Expense expense)
        {       
            try
            {
                if (expense == null || string.IsNullOrEmpty(expense.Name))
                    return StatusCode(500);

                _serviceExpenses.CreateExpense(expense);
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
                var expense = _serviceExpenses.GetExpense(id);
                if (expense != null)
                {
                    _serviceExpenses.DeleteExpense(id);
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
