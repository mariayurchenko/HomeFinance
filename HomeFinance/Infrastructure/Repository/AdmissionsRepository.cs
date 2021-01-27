using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Interfaces;
using Model;

namespace Infrastructure.Repository
{
    public class AdmissionsRepository: IAdmissions
    {
        private readonly AppDBContext appDBContext;

        public AdmissionsRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }
        public IEnumerable<Admission> AllAdmissions => appDBContext.Admission;
        public IEnumerable<Admission> GetDayAdmissions(DateTime date) => appDBContext.Admission.Where(p => p.Date.Day == date.Day && p.Date.Month == date.Month && p.Date.Year == date.Year);
        public IEnumerable<Admission> GetMonthAdmissions(int month, int year) => appDBContext.Admission.Where(p => p.Date.Month == month && p.Date.Year == year);

        public void CreateAdmission(Admission admission)
        {
            appDBContext.Admission.Add(admission);
            appDBContext.SaveChanges();
        }

        public void DeleteAdmission(int id)
        {
            var admission = (from s in appDBContext.Admission.ToList() where s.Id == id select s).First();
            if (admission!=null)
                appDBContext.Admission.Remove(admission);
            appDBContext.SaveChanges();
        }

    }
}
