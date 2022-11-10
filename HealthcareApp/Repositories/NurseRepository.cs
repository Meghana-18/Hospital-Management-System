using System;
namespace HealthcareApp.Repositories
{
    public class NurseRepository : IServiceRepository<Nurse, int>
    {

        IStaffDataAccess<Nurse, int> NurseDataAccess;

        public NurseRepository(IStaffDataAccess<Nurse, int> dataAccess)
        {
            NurseDataAccess = dataAccess;
        }

        public ResponseStatus<Nurse> CreateRecord(Nurse entity)
        {
            ResponseStatus<Nurse> response = new ResponseStatus<Nurse>();
            try
            {
                response.Record = NurseDataAccess.Create(entity);
                response.Message = "Record is created successfully";
                response.StatusCode = 201;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Nurse> DeleteRecord(int id)
        {
            ResponseStatus<Nurse> response = new ResponseStatus<Nurse>();
            try
            {
                response.Record = NurseDataAccess.Delete(id);
                response.Message = "Record is delete successfully";
                response.StatusCode = 203;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Nurse> GetRecord(int id)
        {
            ResponseStatus<Nurse> response = new ResponseStatus<Nurse>();
            try
            {
                response.Record = NurseDataAccess.Get(id);
                response.Message = "Record is read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Nurse> GetRecords()
        {
            ResponseStatus<Nurse> response = new ResponseStatus<Nurse>();
            try
            {
                response.Records = NurseDataAccess.Get();
                response.Message = "Records are read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Nurse> UpdateRecord(int id, Nurse entity)
        {
            ResponseStatus<Nurse> response = new ResponseStatus<Nurse>();
            try
            {
                response.Record = NurseDataAccess.Update(id, entity);
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


