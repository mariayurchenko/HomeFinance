using Microsoft.AspNetCore.Components;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UI.Services
{
    public class IncomeService: BaseService
    {
        List<Income> list = new List<Income>();
        public IncomeService(HttpClient client) : base(client)
        {

        }
        public async Task AddIncome(Income income)
        {
            await _client.PostAsJsonAsync(_client.BaseAddress + "/income", income);
        }
        public async Task DeleteIncome(int id)
        {
            await _client.DeleteAsync(_client.BaseAddress + "/income/" + id);
        }
        public List<Income> GetIncomes()
        {
            var copy = new List<Income> { };

            foreach(Income i in list)
            {
                copy.Add(i);
            }

            return copy;
        }
        public async Task FillList()
        {
            list = await _client.GetJsonAsync<List<Income>>(_client.BaseAddress + "/income");
        }
    }
}
