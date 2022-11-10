using System;
namespace HealthcareApp.Repositories
{
    public class MedicineRepository : IEntityRepository<Medicine, int>
    {

        IEntityDataAccess<Medicine, int> MedicineDataAccess;

        public MedicineRepository(IEntityDataAccess<Medicine, int> dataAccess)
        {
            MedicineDataAccess = dataAccess;
        }

        public ResponseStatus<Medicine> GetRecord(int id)
        {
            ResponseStatus<Medicine> response = new ResponseStatus<Medicine>();
            try
            {
                response.Record = MedicineDataAccess.Get(id);
                response.Message = "Record is read successfully";
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public ResponseStatus<Medicine> GetRecords()
        {
            ResponseStatus<Medicine> response = new ResponseStatus<Medicine>();
            try
            {
                response.Records = MedicineDataAccess.Get();
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



