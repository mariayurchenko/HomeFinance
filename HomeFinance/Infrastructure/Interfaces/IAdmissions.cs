using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Infrastructure.Interfaces
{
    public interface IAdmissions
    {
        IEnumerable<Admission> AllAdmissions { get; }
        IEnumerable<Admission> GetDayAdmissions(DateTime date);
        IEnumerable<Admission> GetMonthAdmissions(int month, int year);
        void CreateAdmission(Admission admission);
        void DeleteAdmission(int id);
    }
}
