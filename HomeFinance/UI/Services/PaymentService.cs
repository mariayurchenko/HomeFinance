using Microsoft.AspNetCore.Components;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UI.Services
{
    public class PaymentService: BaseService
    {
        List<Payment> list = new List<Payment>();
        public PaymentService(HttpClient client) : base(client)
        {

        }
        public async Task AddPayment(Payment payment)
        {
            await _client.PostAsJsonAsync(_client.BaseAddress + "/payment", payment);
        }
        public List<Payment> GetPayments()
        {
            var copy = new List<Payment> { };

            foreach (Payment i in list)
            {
                copy.Add(i);
            }

            return copy;
        }
        public async Task FillListDayPayments(string date)
        {
            list = await _client.GetJsonAsync<List<Payment>>(_client.BaseAddress + $"/payment/{date}");
            foreach (var l in list)
            {
                l.Expense = await _client.GetJsonAsync<Expense>(_client.BaseAddress + $"/expense/{l.ExpenseID}");
            }
        }
        public async Task FillListMonthPayments(int month, int year)
        {
            list = await _client.GetJsonAsync<List<Payment>>(_client.BaseAddress + $"/payment/{month}&{year}");
            foreach (var l in list)
            {
                l.Expense = await _client.GetJsonAsync<Expense>(_client.BaseAddress + $"/expense/{l.ExpenseID}");
            }
        }
    }
}
