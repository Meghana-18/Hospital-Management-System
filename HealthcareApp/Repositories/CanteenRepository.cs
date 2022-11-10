using System;
namespace HealthcareApp.Repositories
{
    public class CanteenRepository : IEntityRepository<Canteen, int>
    {

        IEntityDataAccess<Canteen, int> CanteenDataAccess;

        public CanteenRepository(IEntityDataAccess<Canteen, int> dataAccess)
        {
            CanteenDataAccess = dataAccess;
        }

        public ResponseStatus<Canteen> GetRecord(int id)
        {
            ResponseStatus<Canteen> response = new ResponseStatus<Canteen>();
            try
            {
                response.Record = CanteenDataAccess.Get(id);
                response.Message = "Record is read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Canteen> GetRecords()
        {
            ResponseStatus<Canteen> response = new ResponseStatus<Canteen>();
            try
            {
                response.Records = CanteenDataAccess.Get();
                response.Message = "Records are read successfully";
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


