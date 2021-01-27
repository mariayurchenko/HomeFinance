using Microsoft.AspNetCore.Components;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UI.Services
{
    public class ExpenseService: BaseService
    {
        List<Expense> list = new List<Expense>();
        public ExpenseService(HttpClient client) : base(client)
        {

        }
        public async Task AddExpense(Expense expense)
        {
            await _client.PostAsJsonAsync(_client.BaseAddress + "/expense", expense);
        }
        public async Task DeleteExpense(int id)
        {
            await _client.DeleteAsync(_client.BaseAddress + "/expense/" + id);
        }
        public List<Expense> GetExpenses()
        {
            var copy = new List<Expense> { };

            foreach (Expense e in list)
            {
                copy.Add(e);
            }

            return copy;
        }
        public async Task FillList()
        {
            list = await _client.GetJsonAsync<List<Expense>>(_client.BaseAddress + "/expense");
        }
    }
}
