using System;
using Application.Dal.Contract;
using Application.Entities;
using System.Data;
using System.Data.SqlClient;
using Application.Entities.Base;

namespace Application.Data.DataAccess;

public class DoctorDataAccess : IStaffDataAccess<Doctor, int>
{
    SqlConnection connection;
    SqlCommand command;

    public DoctorDataAccess()
    {
        connection = new SqlConnection("Data Source = 127.0.0.1; Initial Catalog = HealthcareSystem_DB; User Id = sa; Password = Docker@SQL123");
    }

    Doctor IStaffDataAccess<Doctor, int>.Create(Doctor doctor)
    {
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"INSERT INTO Doctor VALUES ('{doctor.FirstName}', '{doctor.MiddleName}', '{doctor.LastName}', '{doctor.MobileNo}', " +
                $"'{doctor.EmailID}', '{doctor.HouseNo}', '{doctor.Society}', '{doctor.Area}', '{doctor.City}', '{doctor.State}', " +
                $"CONVERT(DATETIME,'{doctor.DOB}'), '{doctor.Gender}', '{doctor.Specialization}', {doctor.Salary}, '{doctor.VisitingDoc}', " +
                $"{doctor.OpdFees}, {doctor.VisitCharges})";
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
        return doctor;
    }

    Doctor IStaffDataAccess<Doctor, int>.Delete(int id)
    {
        Doctor Doctor = null;
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"DELETE FROM Doctor WHERE DID = {id}";
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
        return Doctor;
    }

    IEnumerable<Doctor> IStaffDataAccess<Doctor, int>.Get()
    {
        List<Doctor> Doctors = new List<Doctor>();
        try
        {
            connection.Open();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Doctor";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Doctors.Add(
                      new Doctor()
                      {
                          StaffId = Convert.ToInt32(reader["DID"]),
                          FirstName = Convert.ToString(reader["DFirstName"]),
                          MiddleName = Convert.ToString(reader["DMiddleName"]),
                          LastName = Convert.ToString(reader["DLastName"]),
                          MobileNo = Convert.ToString(reader["DMobile"]),
                          EmailID = Convert.ToString(reader["DEmailID"]),
                          HouseNo = Convert.ToString(reader["DHouseNo"]),
                          Society = Convert.ToString(reader["DSociety"]),
                          Area = Convert.ToString(reader["DArea"]),
                          City = Convert.ToString(reader["DCity"]),
                          State = Convert.ToString(reader["DState"]),
                          DOB = Convert.ToString(reader["DDOB"]),
                          Gender = Convert.ToString(reader["DGender"]),
                          Specialization = Convert.ToString(reader["DSpecialization"]),
                          Salary = Convert.ToDecimal(reader["DSalary"]),
                          VisitingDoc = Convert.ToBoolean(reader["DVisiting"]),
                          OpdFees = Convert.ToDecimal(reader["DFees"]),
                          VisitCharges = Convert.ToDecimal(reader["DCharges"])
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
        return Doctors;
    }

    Doctor IStaffDataAccess<Doctor, int>.Get(int id)
    {
        Doctor Doctor = null;
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Doctor WHERE DID = {id}";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Doctor = new Doctor()
                {
                    StaffId = Convert.ToInt32(reader["DID"]),
                    FirstName = Convert.ToString(reader["DFirstName"]),
                    MiddleName = Convert.ToString(reader["DMiddleName"]),
                    LastName = Convert.ToString(reader["DLastName"]),
                    MobileNo = Convert.ToString(reader["DMobile"]),
                    EmailID = Convert.ToString(reader["DEmailID"]),
                    HouseNo = Convert.ToString(reader["DHouseNo"]),
                    Society = Convert.ToString(reader["DSociety"]),
                    Area = Convert.ToString(reader["DArea"]),
                    City = Convert.ToString(reader["DCity"]),
                    State = Convert.ToString(reader["DState"]),
                    DOB = Convert.ToString(reader["DDOB"]),
                    Gender = Convert.ToString(reader["DGender"]),
                    Specialization = Convert.ToString(reader["DSpecialization"]),
                    Salary = Convert.ToDecimal(reader["DSalary"]),
                    VisitingDoc = Convert.ToBoolean(reader["DVisiting"]),
                    OpdFees = Convert.ToDecimal(reader["DFees"]),
                    VisitCharges = Convert.ToDecimal(reader["DCharges"])
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
        return Doctor;
    }

    Doctor IStaffDataAccess<Doctor, int>.Update(int id, Doctor doctor)
    {
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"UPDATE Doctor SET DFirstName='{doctor.FirstName}', DMiddleName='{doctor.MiddleName}', DLastName='{doctor.LastName}'," +
                $"DMobile='{doctor.MobileNo}', DEmailID='{doctor.EmailID}', DHouseNo='{doctor.HouseNo}', DSociety='{doctor.Society}', DArea='{doctor.Area}', " +
                $"DCity='{doctor.City}', DState='{doctor.State}', DDOB='{doctor.DOB}', DGender='{doctor.Gender}', DSpecialization='{doctor.Specialization}', " +
                $"DSalary={doctor.Salary}, DVisiting='{doctor.VisitingDoc}', DFees={doctor.OpdFees}, DCharges={doctor.VisitCharges}";
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
        return doctor;
    }

    
}

