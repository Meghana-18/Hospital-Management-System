using System;
using Application.Dal.Contract;
using Application.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Application.Data.DataAccess;

public class MedicineDataAccess : IEntityDataAccess<Medicine, int>
{
    SqlConnection connection;
    SqlCommand command;

    public MedicineDataAccess()
    {
        connection = new SqlConnection("Data Source = 127.0.0.1; Initial Catalog = HealthcareSystem_DB; User Id = sa; Password = Docker@SQL123");
    }

    IEnumerable<Medicine> IEntityDataAccess<Medicine, int>.Get()
    {
        List<Medicine> Entities = new List<Medicine>();
        try
        {
            connection.Open();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Medicines";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Entities.Add(
                      new Medicine()
                      {
                          ID = Convert.ToInt32(reader["MID"]),
                          Name = Convert.ToString(reader["MName"]),
                          Price = Convert.ToDecimal(reader["MPrice"])
                      }
                    );
            }
            reader.Close();
        }
        catch (SqlException sqlEx)
        {
            throw sqlEx;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
        }
        return Entities;
    }

    Medicine IEntityDataAccess<Medicine, int>.Get(int id)
    {
        Medicine Medicine = null;
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Medicines WHERE MID = {id}";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Medicine = new Medicine()
                {
                    ID = Convert.ToInt32(reader["MID"]),
                    Name = Convert.ToString(reader["MName"]),
                    Price = Convert.ToDecimal(reader["MPrice"])
                };
            }
            reader.Close();
        }
        catch (SqlException sqlEx)
        {
            throw sqlEx;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
        }
        return Medicine;
    }

}

