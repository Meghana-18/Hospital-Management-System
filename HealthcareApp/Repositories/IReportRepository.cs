using System;
namespace HealthcareApp.Repositories
{
    public interface IReportRepository<TEntity, TPk> where TEntity : class
    {
        ResponseStatus<decimal> IpdBillValue(TPk id);
        ResponseStatus<IpdPatient> GetByDoctorRecords(TPk id);
        ResponseStatus<IpdPatient> GetByNurseRecords(TPk id);
        ResponseStatus<IpdPatient> GetByWardRecords(string id);
        ResponseStatus<decimal> TotalCollectionValue();
        ResponseStatus<decimal> OpdBillValue(TPk id);
        ResponseStatus<Doctor> GetBySpecRecords(string id);
        ResponseStatus<Doctor> GetEmpDocRecords();
        ResponseStatus<Doctor> GetVisitingDocRecords();
    }
}

