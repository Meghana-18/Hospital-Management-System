using System;
using Application.Entities.Base;
using Application.Entities;

namespace Application.Dal.Contract
{
    public interface IReportDataAccess<TReport, in TPk> where TReport : Report
    {

        decimal calcIpdBill(int id);
        IEnumerable<IpdPatient> GetbyDoctor(int DID);
        IEnumerable<IpdPatient> GetbyNurse(int NID);
        IEnumerable<IpdPatient> GetbyWard(string ward);
        decimal CalcTotalCollection();
        decimal calcOpdBill(int id);
        IEnumerable<Doctor> GetbySpec(string spec);
        IEnumerable<Doctor> GetEmpDoctors();
        IEnumerable<Doctor> GetVisitingDoctors();

    }
}

