using System;
namespace HealthcareApp.Repositories
{
    public class ConstantRepository : IEntityRepository<Constant, int>
    {

        IEntityDataAccess<Constant, int> ConstantDataAccess;

        public ConstantRepository(IEntityDataAccess<Constant, int> dataAccess)
        {
            ConstantDataAccess = dataAccess;
        }

        public ResponseStatus<Constant> GetRecord(int id)
        {
            ResponseStatus<Constant> response = new ResponseStatus<Constant>();
            try
            {
                response.Record = ConstantDataAccess.Get(id);
                response.Message = "Record is read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Constant> GetRecords()
        {
            ResponseStatus<Constant> response = new ResponseStatus<Constant>();
            try
            {
                response.Records = ConstantDataAccess.Get();
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

