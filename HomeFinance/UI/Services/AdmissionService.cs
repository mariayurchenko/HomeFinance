using Microsoft.AspNetCore.Components;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UI.Services
{
    public class AdmissionService : BaseService
    {
        List<Admission> list = new List<Admission>();
        public AdmissionService(HttpClient client) : base(client)
        {

        }
        public async Task AddAdmission(Admission admission)
        {
            await _client.PostAsJsonAsync(_client.BaseAddress + "/admission", admission);
        }
        public List<Admission> GetAdmissions()
        {
            var copy = new List<Admission> { };

            foreach (Admission i in list)
            {
                copy.Add(i);
            }

            return copy;
        }
        public async Task FillListDayAdmissions(string date)
        {
            list = await _client.GetJsonAsync<List<Admission>>(_client.BaseAddress + $"/admission/{date}");
            foreach(var l in list)
            {
                l.Income= await _client.GetJsonAsync<Income>(_client.BaseAddress + $"/income/{l.IncomeID}");
            }
        }
        public async Task FillListMonthAdmissions(int month, int year)
        {
            list = await _client.GetJsonAsync<List<Admission>>(_client.BaseAddress + $"/admission/{month}&{year}");
            foreach (var l in list)
            {
                l.Income = await _client.GetJsonAsync<Income>(_client.BaseAddress + $"/income/{l.IncomeID}");
            }
        }
    }
}
