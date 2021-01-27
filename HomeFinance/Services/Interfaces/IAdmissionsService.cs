using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Services.Interfaces
{
    public interface IAdmissionsService
    {
        IEnumerable<Admission> AllAdmissions { get; }
        IEnumerable<Admission> GetDayAdmissions(DateTime date);
        IEnumerable<Admission> GetMonthAdmissions(int month, int year);
        void CreateAdmission(Admission admission);
        void DeleteAdmission(int id);
    }
}
