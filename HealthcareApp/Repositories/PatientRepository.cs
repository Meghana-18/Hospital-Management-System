using System;
namespace HealthcareApp.Repositories
{
    public class PatientRepository : IServiceRepository<Patient, int>
    {

        IPatientDataAccess<Patient, int> PatientDataAccess;

        public PatientRepository(IPatientDataAccess<Patient, int> dataAccess)
        {
            PatientDataAccess = dataAccess;
        }

        public ResponseStatus<Patient> CreateRecord(Patient entity)
        {
            ResponseStatus<Patient> response = new ResponseStatus<Patient>();
            try
            {
                response.Record = PatientDataAccess.Create(entity);
                response.Message = "Record is created successfully";
                response.StatusCode = 201;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Patient> DeleteRecord(int id)
        {
            ResponseStatus<Patient> response = new ResponseStatus<Patient>();
            try
            {
                response.Record = PatientDataAccess.Delete(id);
                response.Message = "Record is delete successfully";
                response.StatusCode = 203;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Patient> GetRecord(int id)
        {
            ResponseStatus<Patient> response = new ResponseStatus<Patient>();
            try
            {
                response.Record = PatientDataAccess.Get(id);
                response.Message = "Record is read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Patient> GetRecords()
        {
            ResponseStatus<Patient> response = new ResponseStatus<Patient>();
            try
            {
                response.Records = PatientDataAccess.Get();
                response.Message = "Records are read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Patient> UpdateRecord(int id, Patient entity)
        {
            ResponseStatus<Patient> response = new ResponseStatus<Patient>();
            try
            {
                response.Record = PatientDataAccess.Update(id, entity);
                response.Message = "Record is updated successfully";
                response.StatusCode = 204;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}


