using System;
namespace HealthcareApp.Repositories
{
    public class ReportRepository : IReportRepository<Report, int>
    {

        IReportDataAccess<Report, int> ReportDataAccess;

        public ReportRepository(IReportDataAccess<Report, int> dataAccess)
        {
            ReportDataAccess = dataAccess;

        }

        public ResponseStatus<decimal> IpdBillValue(int id)
        {
            ResponseStatus<decimal> response = new ResponseStatus<decimal>();
            try
            {
                response.Record = ReportDataAccess.calcIpdBill(id);
                response.Message = "Value retrieved successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<IpdPatient> GetByDoctorRecords(int id)
        {
            ResponseStatus<IpdPatient> response = new ResponseStatus<IpdPatient>();
            try
            {
                response.Records = ReportDataAccess.GetbyDoctor(id);
                response.Message = "Record is read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<IpdPatient> GetByNurseRecords(int id)
        {
            ResponseStatus<IpdPatient> response = new ResponseStatus<IpdPatient>();
            try
            {
                response.Records = ReportDataAccess.GetbyNurse(id);
                response.Message = "Record is read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<IpdPatient> GetByWardRecords(string ward)
        {
            ResponseStatus<IpdPatient> response = new ResponseStatus<IpdPatient>();
            try
            {
                response.Records = ReportDataAccess.GetbyWard(ward);
                response.Message = "Records are read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<decimal> TotalCollectionValue()
        {
            ResponseStatus<decimal> response = new ResponseStatus<decimal>();
            try
            {
                response.Record = ReportDataAccess.CalcTotalCollection();
                response.Message = "Value is read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<decimal> OpdBillValue(int id)
        {
            ResponseStatus<decimal> response = new ResponseStatus<decimal>();
            try
            {
                response.Record = ReportDataAccess.calcOpdBill(id);
                response.Message = "Value is read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Doctor> GetBySpecRecords(string spec)
        {
            ResponseStatus<Doctor> response = new ResponseStatus<Doctor>();
            try
            {
                response.Records = ReportDataAccess.GetbySpec(spec);
                response.Message = "Record is read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Doctor> GetEmpDocRecords()
        {
            ResponseStatus<Doctor> response = new ResponseStatus<Doctor>();
            try
            {
                response.Records = ReportDataAccess.GetEmpDoctors(); ;
                response.Message = "Record is read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Doctor> GetVisitingDocRecords()
        {
            ResponseStatus<Doctor> response = new ResponseStatus<Doctor>();
            try
            {
                response.Records = ReportDataAccess.GetVisitingDoctors();
                response.Message = "Record is read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}

