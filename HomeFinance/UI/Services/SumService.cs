using Microsoft.AspNetCore.Components;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UI.Services
{
    public class SumService: BaseService
    {
        public CashAccount CashAccount = new CashAccount();
        public SumService(HttpClient client) : base(client)
        {

        }
        public async Task AnalzeDay(string date)
        {
            CashAccount = await _client.GetJsonAsync<CashAccount>(_client.BaseAddress + $"/sum/{date}");
        }
        public async Task AnalzeMonth(DateTime date)
        {
            CashAccount = await _client.GetJsonAsync<CashAccount>(_client.BaseAddress + $"/sum/{date.Month}&{date.Year}");
        }
    }
}
