using System;
using Application.Dal.Contract;
using Application.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Application.Data.DataAccess;

public class CanteenDataAccess : IEntityDataAccess<Canteen, int>
{
    SqlConnection connection;
    SqlCommand command;

    public CanteenDataAccess()
    {
        connection = new SqlConnection("Data Source = 127.0.0.1; Initial Catalog = HealthcareSystem_DB; User Id = sa; Password = Docker@SQL123");
    }

    IEnumerable<Canteen> IEntityDataAccess<Canteen, int>.Get()
    {
        List<Canteen> Entities = new List<Canteen>();
        try
        {
            connection.Open();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Canteen";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Entities.Add(
                      new Canteen()
                      {
                          ID = Convert.ToInt32(reader["CID"]),
                          Name = Convert.ToString(reader["CName"]),
                          Price = Convert.ToDecimal(reader["CPrice"])
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

    Canteen IEntityDataAccess<Canteen, int>.Get(int id)
    {
        Canteen Canteen = null;
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Canteen WHERE CID = {id}";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Canteen = new Canteen()
                {
                    ID = Convert.ToInt32(reader["CID"]),
                    Name = Convert.ToString(reader["CName"]),
                    Price = Convert.ToDecimal(reader["CPrice"])
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
        return Canteen;
    }

}

