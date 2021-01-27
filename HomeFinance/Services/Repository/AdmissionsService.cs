using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Services.DAL;
using Services.Interfaces;

namespace Services.Repository
{
    public class AdmissionsService: IAdmissionsService
    {
        private readonly UnitOfWork unitOfWork;
        public AdmissionsService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;   
        }
        public IEnumerable<Admission> AllAdmissions => unitOfWork.AdmissionRepository.Get();
        public IEnumerable<Admission> GetDayAdmissions(DateTime date) => unitOfWork.AdmissionRepository.Get().Where(p => p.Date.Day == date.Day && p.Date.Month == date.Month && p.Date.Year == date.Year);
        public IEnumerable<Admission> GetMonthAdmissions(int month, int year) => unitOfWork.AdmissionRepository.Get().Where(p => p.Date.Month == month && p.Date.Year == year);

        public void CreateAdmission(Admission admission)
        {
            unitOfWork.AdmissionRepository.Insert(admission);
            unitOfWork.Save();
        }

        public void DeleteAdmission(int id)
        {
            if (unitOfWork.AdmissionRepository.GetByID(id) != null)
                unitOfWork.AdmissionRepository.Delete(id);
            unitOfWork.Save();
        }

    }
}
