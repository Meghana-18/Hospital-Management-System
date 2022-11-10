using System;
using Application.Dal.Contract;
using Application.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Application.Data.DataAccess;

public class ConstantDataAccess : IEntityDataAccess<Constant, int>
{
    SqlConnection connection;
    SqlCommand command;

    public ConstantDataAccess()
    {
        connection = new SqlConnection("Data Source = 127.0.0.1; Initial Catalog = HealthcareSystem_DB; User Id = sa; Password = Docker@SQL123");
    }

    IEnumerable<Constant> IEntityDataAccess<Constant, int>.Get()
    {
        List<Constant> Entities = new List<Constant>();
        try
        {
            connection.Open();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Constants";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Entities.Add(
                      new Constant()
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

    Constant IEntityDataAccess<Constant, int>.Get(int id)
    {
        Constant Constant = null;
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Constants WHERE CID = {id}";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Constant = new Constant()
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
        return Constant;
    }

}

