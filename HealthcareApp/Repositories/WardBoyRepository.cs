using System;
namespace HealthcareApp.Repositories
{
    public class WardBoyRepository : IServiceRepository<WardBoy, int>
    {

        IStaffDataAccess<WardBoy, int> WardBoyDataAccess;

        public WardBoyRepository(IStaffDataAccess<WardBoy, int> dataAccess)
        {
            WardBoyDataAccess = dataAccess;
        }

        public ResponseStatus<WardBoy> CreateRecord(WardBoy entity)
        {
            ResponseStatus<WardBoy> response = new ResponseStatus<WardBoy>();
            try
            {
                response.Record = WardBoyDataAccess.Create(entity);
                response.Message = "Record is created successfully";
                response.StatusCode = 201;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<WardBoy> DeleteRecord(int id)
        {
            ResponseStatus<WardBoy> response = new ResponseStatus<WardBoy>();
            try
            {
                response.Record = WardBoyDataAccess.Delete(id);
                response.Message = "Record is delete successfully";
                response.StatusCode = 203;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<WardBoy> GetRecord(int id)
        {
            ResponseStatus<WardBoy> response = new ResponseStatus<WardBoy>();
            try
            {
                response.Record = WardBoyDataAccess.Get(id);
                response.Message = "Record is read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<WardBoy> GetRecords()
        {
            ResponseStatus<WardBoy> response = new ResponseStatus<WardBoy>();
            try
            {
                response.Records = WardBoyDataAccess.Get();
                response.Message = "Records are read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<WardBoy> UpdateRecord(int id, WardBoy entity)
        {
            ResponseStatus<WardBoy> response = new ResponseStatus<WardBoy>();
            try
            {
                response.Record = WardBoyDataAccess.Update(id, entity);
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
