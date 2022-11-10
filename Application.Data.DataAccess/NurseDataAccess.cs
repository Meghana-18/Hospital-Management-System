using System;
using Application.Dal.Contract;
using Application.Entities;
using System.Data;
using System.Data.SqlClient;
using Application.Entities.Base;

namespace Application.Data.DataAccess;

public class NurseDataAccess : IStaffDataAccess<Nurse, int>
{
    SqlConnection connection;
    SqlCommand command;

    public NurseDataAccess()
    {
        connection = new SqlConnection("Data Source = 127.0.0.1; Initial Catalog = HealthcareSystem_DB; User Id = sa; Password = Docker@SQL123");
    }

    Nurse IStaffDataAccess<Nurse, int>.Create(Nurse nurse)
    {
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"INSERT INTO Nurse VALUES ('{nurse.FirstName}', '{nurse.MiddleName}', '{nurse.LastName}', '{nurse.MobileNo}', " +
                $"'{nurse.EmailID}', '{nurse.HouseNo}', '{nurse.Society}', '{nurse.Area}', '{nurse.City}', '{nurse.State}', " +
                $"CONVERT(DATETIME,'{nurse.DOB}'), '{nurse.Gender}', {nurse.Salary}, {nurse.FeesPerday})";
            int result = command.ExecuteNonQuery();
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
        return nurse;
    }

    Nurse IStaffDataAccess<Nurse, int>.Delete(int id)
    {
        Nurse Nurse = null;
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"DELETE FROM Nurse WHERE NID = {id}";
            int result = command.ExecuteNonQuery();
        }
        catch (SqlException sqlEx)
        {
            Console.WriteLine($"Error occurred while processing the request - {sqlEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General error  - {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
        return Nurse;
    }

    IEnumerable<Nurse> IStaffDataAccess<Nurse, int>.Get()
    {
        List<Nurse> Nurses = new List<Nurse>();
        try
        {
            connection.Open();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Nurse";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Nurses.Add(
                      new Nurse()
                      {
                          StaffId = Convert.ToInt32(reader["NID"]),
                          FirstName = Convert.ToString(reader["NFirstName"]),
                          MiddleName = Convert.ToString(reader["NMiddleName"]),
                          LastName = Convert.ToString(reader["NLastName"]),
                          MobileNo = Convert.ToString(reader["NMobile"]),
                          EmailID = Convert.ToString(reader["NEmailID"]),
                          HouseNo = Convert.ToString(reader["NHouseNo"]),
                          Society = Convert.ToString(reader["NSociety"]),
                          Area = Convert.ToString(reader["NArea"]),
                          City = Convert.ToString(reader["NCity"]),
                          State = Convert.ToString(reader["NState"]),
                          DOB = Convert.ToString(reader["NDOB"]),
                          Gender = Convert.ToString(reader["NGender"]),
                          Salary = Convert.ToDecimal(reader["NSalary"]),
                          FeesPerday = Convert.ToDecimal(reader["NFees"])
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
        return Nurses;
    }

    Nurse IStaffDataAccess<Nurse, int>.Get(int id)
    {
        Nurse Nurse = null;
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Nurse WHERE DID = {id}";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Nurse = new Nurse()
                {
                    StaffId = Convert.ToInt32(reader["NID"]),
                    FirstName = Convert.ToString(reader["NFirstName"]),
                    MiddleName = Convert.ToString(reader["NMiddleName"]),
                    LastName = Convert.ToString(reader["NLastName"]),
                    MobileNo = Convert.ToString(reader["NMobile"]),
                    EmailID = Convert.ToString(reader["NEmailID"]),
                    HouseNo = Convert.ToString(reader["NHouseNo"]),
                    Society = Convert.ToString(reader["NSociety"]),
                    Area = Convert.ToString(reader["NArea"]),
                    City = Convert.ToString(reader["NCity"]),
                    State = Convert.ToString(reader["NState"]),
                    DOB = Convert.ToString(reader["NDOB"]),
                    Gender = Convert.ToString(reader["NGender"]),
                    Salary = Convert.ToDecimal(reader["NSalary"]),
                    FeesPerday = Convert.ToDecimal(reader["NFees"])
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
        return Nurse;
    }

    Nurse IStaffDataAccess<Nurse, int>.Update(int id, Nurse nurse)
    {
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"UPDATE Nurse SET NFirstName='{nurse.FirstName}', NMiddleName='{nurse.MiddleName}', NLastName='{nurse.LastName}'," +
                $"NMobile='{nurse.MobileNo}', NEmailID='{nurse.EmailID}', NHouseNo='{nurse.HouseNo}', NSociety='{nurse.Society}', NArea='{nurse.Area}', " +
                $"DCity='{nurse.City}', DState='{nurse.State}', DDOB='{nurse.DOB}', NGender='{nurse.Gender}, NSalary={nurse.Salary}, NFees={nurse.FeesPerday}";
            int result = command.ExecuteNonQuery();

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
        return nurse;
    }
}

