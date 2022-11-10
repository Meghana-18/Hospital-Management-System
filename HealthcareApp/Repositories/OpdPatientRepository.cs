using System;
namespace HealthcareApp.Repositories
{
    public class OpdPatientRepository : IServiceRepository<OpdPatient, int>
    {
        IPatientDataAccess<OpdPatient, int> OpdPatientDataAccess;

        public OpdPatientRepository(IPatientDataAccess<OpdPatient, int> dataAccess)
        {
            OpdPatientDataAccess = dataAccess;
        }

        public ResponseStatus<OpdPatient> CreateRecord(OpdPatient entity)
        {
            ResponseStatus<OpdPatient> response = new ResponseStatus<OpdPatient>();
            try
            {
                response.Record = OpdPatientDataAccess.Create(entity);
                response.Message = "Record is created successfully";
                response.StatusCode = 201;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<OpdPatient> DeleteRecord(int id)
        {
            ResponseStatus<OpdPatient> response = new ResponseStatus<OpdPatient>();
            try
            {
                response.Record = OpdPatientDataAccess.Delete(id);
                response.Message = "Record is delete successfully";
                response.StatusCode = 203;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<OpdPatient> GetRecord(int id)
        {
            ResponseStatus<OpdPatient> response = new ResponseStatus<OpdPatient>();
            try
            {
                response.Record = OpdPatientDataAccess.Get(id);
                response.Message = "Record is read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<OpdPatient> GetRecords()
        {
            ResponseStatus<OpdPatient> response = new ResponseStatus<OpdPatient>();
            try
            {
                response.Records = OpdPatientDataAccess.Get();
                response.Message = "Records are read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<OpdPatient> UpdateRecord(int id, OpdPatient entity)
        {
            ResponseStatus<OpdPatient> response = new ResponseStatus<OpdPatient>();
            try
            {
                response.Record = OpdPatientDataAccess.Update(id, entity);
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


