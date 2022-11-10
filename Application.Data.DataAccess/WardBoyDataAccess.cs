using System;
using Application.Dal.Contract;
using Application.Entities;
using System.Data;
using System.Data.SqlClient;
using Application.Entities.Base;

namespace Application.Data.DataAccess;

public class WardBoyDataAccess : IStaffDataAccess<WardBoy, int>
{
    SqlConnection connection;
    SqlCommand command;

    public WardBoyDataAccess()
    {
        connection = new SqlConnection("Data Source = 127.0.0.1; Initial Catalog = HealthcareSystem_DB; User Id = sa; Password = Docker@SQL123");
    }

    WardBoy IStaffDataAccess<WardBoy, int>.Create(WardBoy wardBoy)
    {
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"INSERT INTO WardBoy VALUES ('{wardBoy.FirstName}', '{wardBoy.MiddleName}', '{wardBoy.LastName}', " +
                $"'{wardBoy.MobileNo}', '{wardBoy.EmailID}', '{wardBoy.HouseNo}', '{wardBoy.Society}', '{wardBoy.Area}', '{wardBoy.City}', " +
                $"'{wardBoy.State}', CONVERT(DATETIME,'{wardBoy.DOB}'), '{wardBoy.Gender}', {wardBoy.Salary}, {wardBoy.FeesPerday})";
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
        return wardBoy;
    }

    WardBoy IStaffDataAccess<WardBoy, int>.Delete(int id)
    {
        WardBoy WardBoy = null;
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"DELETE FROM WardBoy WHERE WID = {id}";
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
        return WardBoy;
    }

    IEnumerable<WardBoy> IStaffDataAccess<WardBoy, int>.Get()
    {
        List<WardBoy> WardBoys = new List<WardBoy>();
        try
        {
            connection.Open();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM WardBoy";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                WardBoys.Add(
                      new WardBoy()
                      {
                          StaffId = Convert.ToInt32(reader["WID"]),
                          FirstName = Convert.ToString(reader["WFirstName"]),
                          MiddleName = Convert.ToString(reader["WMiddleName"]),
                          LastName = Convert.ToString(reader["WLastName"]),
                          MobileNo = Convert.ToString(reader["WMobile"]),
                          EmailID = Convert.ToString(reader["WEmailID"]),
                          HouseNo = Convert.ToString(reader["WHouseNo"]),
                          Society = Convert.ToString(reader["WSociety"]),
                          Area = Convert.ToString(reader["WArea"]),
                          City = Convert.ToString(reader["WCity"]),
                          State = Convert.ToString(reader["WState"]),
                          DOB = Convert.ToString(reader["WDOB"]),
                          Gender = Convert.ToString(reader["WGender"]),
                          Salary = Convert.ToDecimal(reader["WSalary"]),
                          FeesPerday = Convert.ToDecimal(reader["WFees"])
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
        return WardBoys;
    }

    WardBoy IStaffDataAccess<WardBoy, int>.Get(int id)
    {
        WardBoy WardBoy = null;
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM WardBoy WHERE WID = {id}";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                WardBoy = new WardBoy()
                {
                    StaffId = Convert.ToInt32(reader["WID"]),
                    FirstName = Convert.ToString(reader["WFirstName"]),
                    MiddleName = Convert.ToString(reader["WMiddleName"]),
                    LastName = Convert.ToString(reader["WLastName"]),
                    MobileNo = Convert.ToString(reader["WMobile"]),
                    EmailID = Convert.ToString(reader["WEmailID"]),
                    HouseNo = Convert.ToString(reader["WHouseNo"]),
                    Society = Convert.ToString(reader["WSociety"]),
                    Area = Convert.ToString(reader["WArea"]),
                    City = Convert.ToString(reader["WCity"]),
                    State = Convert.ToString(reader["WState"]),
                    DOB = Convert.ToString(reader["WDOB"]),
                    Gender = Convert.ToString(reader["WGender"]),
                    Salary = Convert.ToDecimal(reader["WSalary"]),
                    FeesPerday = Convert.ToDecimal(reader["WFees"])
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
        return WardBoy;
    }

    WardBoy IStaffDataAccess<WardBoy, int>.Update(int id, WardBoy wardBoy)
    {
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"UPDATE WardBoy SET WFirstName='{wardBoy.FirstName}', WMiddleName='{wardBoy.MiddleName}', WLastName='{wardBoy.LastName}'," +
                $"WMobile='{wardBoy.MobileNo}', WEmailID='{wardBoy.EmailID}', WHouseNo='{wardBoy.HouseNo}', WSociety='{wardBoy.Society}', WArea='{wardBoy.Area}', " +
                $"WCity='{wardBoy.City}', WState='{wardBoy.State}', WDOB='{wardBoy.DOB}', WGender='{wardBoy.Gender}', WSalary={wardBoy.Salary}, WFees={wardBoy.FeesPerday}";
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
        return wardBoy;
    }
}

