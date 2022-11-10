using System;
namespace HealthcareApp.Repositories
{
    public class IpdPatientRepository : IServiceRepository<IpdPatient, int>
    {

        IPatientDataAccess<IpdPatient, int> IpdPatientDataAccess;

        public IpdPatientRepository(IPatientDataAccess<IpdPatient, int> dataAccess)
        {
            IpdPatientDataAccess = dataAccess;

        }

        public ResponseStatus<IpdPatient> CreateRecord(IpdPatient entity)
        {
            ResponseStatus<IpdPatient> response = new ResponseStatus<IpdPatient>();
            try
            {
                response.Record = IpdPatientDataAccess.Create(entity);
                response.Message = "Record is created successfully";
                response.StatusCode = 201;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<IpdPatient> DeleteRecord(int id)
        {
            ResponseStatus<IpdPatient> response = new ResponseStatus<IpdPatient>();
            try
            {
                response.Record = IpdPatientDataAccess.Delete(id);
                response.Message = "Record is delete successfully";
                response.StatusCode = 203;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<IpdPatient> GetRecord(int id)
        {
            ResponseStatus<IpdPatient> response = new ResponseStatus<IpdPatient>();
            try
            {
                response.Record = IpdPatientDataAccess.Get(id);
                response.Message = "Record is read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<IpdPatient> GetRecords()
        {
            ResponseStatus<IpdPatient> response = new ResponseStatus<IpdPatient>();
            try
            {
                response.Records = IpdPatientDataAccess.Get();
                response.Message = "Records are read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<IpdPatient> UpdateRecord(int id, IpdPatient entity)
        {
            ResponseStatus<IpdPatient> response = new ResponseStatus<IpdPatient>();
            try
            {
                response.Record = IpdPatientDataAccess.Update(id, entity);
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
