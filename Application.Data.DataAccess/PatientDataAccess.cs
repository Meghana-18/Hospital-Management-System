using System;
using Application.Dal.Contract;
using Application.Entities.Base;
using System.Data;
using System.Data.SqlClient;

namespace Application.Data.DataAccess;

public class PatientDataAccess : IPatientDataAccess<Patient, int>
{
    SqlConnection connection;
    SqlCommand command;

    public PatientDataAccess()
    {
        connection = new SqlConnection("Data Source = 127.0.0.1; Initial Catalog = HealthcareSystem_DB; User Id = sa; Password = Docker@SQL123");
    }

    Patient IPatientDataAccess<Patient, int>.Create(Patient patient)
    {
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"INSERT INTO Patient VALUES ('{patient.FirstName}', '{patient.MiddleName}', '{patient.LastName}', " +
                $"'{patient.MobileNo}', '{patient.EmailID}', '{patient.HouseNo}', '{patient.Society}', '{patient.Area}', '{patient.City}', '{patient.State}', " +
                $"CONVERT(DATETIME,'{patient.DOB}'), '{patient.Gender}', '{patient.AgeType}', '{patient.Admit}')";
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
        return patient;
    }

    Patient IPatientDataAccess<Patient, int>.Delete(int id)
    {
        Patient Patient = null;
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"DELETE FROM Patient WHERE PID = {id}";
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
        return Patient;
    }

    IEnumerable<Patient> IPatientDataAccess<Patient, int>.Get()
    {
        List<Patient> Patients = new List<Patient>();
        try
        {
            connection.Open();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Patient";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Patients.Add(
                      new Patient()
                      {
                          PatientId = Convert.ToInt32(reader["PID"]),
                          FirstName = Convert.ToString(reader["PFirstName"]),
                          MiddleName = Convert.ToString(reader["PMiddleName"]),
                          LastName = Convert.ToString(reader["PLastName"]),
                          MobileNo = Convert.ToString(reader["PMobile"]),
                          EmailID = Convert.ToString(reader["PEmailID"]),
                          HouseNo = Convert.ToString(reader["PHouseNo"]),
                          Society = Convert.ToString(reader["PSociety"]),
                          Area = Convert.ToString(reader["PArea"]),
                          City = Convert.ToString(reader["PCity"]),
                          State = Convert.ToString(reader["PState"]),
                          DOB = Convert.ToString(reader["PDOB"]),
                          Gender = Convert.ToString(reader["PGender"]),
                          AgeType = Convert.ToString(reader["PAgeType"]),
                          Admit = Convert.ToBoolean(reader["PAdmit"])
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
        return Patients;
    }

    Patient IPatientDataAccess<Patient, int>.Get(int id)
    {
        Patient Patient = null;
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM Patient WHERE PID = {id}";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Patient = new Patient()
                {
                    PatientId = Convert.ToInt32(reader["PID"]),
                    FirstName = Convert.ToString(reader["PFirstName"]),
                    MiddleName = Convert.ToString(reader["PMiddleName"]),
                    LastName = Convert.ToString(reader["PLastName"]),
                    MobileNo = Convert.ToString(reader["PMobile"]),
                    EmailID = Convert.ToString(reader["PEmailID"]),
                    HouseNo = Convert.ToString(reader["PHouseNo"]),
                    Society = Convert.ToString(reader["PSociety"]),
                    Area = Convert.ToString(reader["PArea"]),
                    City = Convert.ToString(reader["PCity"]),
                    State = Convert.ToString(reader["PState"]),
                    DOB = Convert.ToString(reader["PDOB"]),
                    Gender = Convert.ToString(reader["PGender"]),
                    AgeType = Convert.ToString(reader["PAgeType"]),
                    Admit = Convert.ToBoolean(reader["PAdmit"])
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
        return Patient;
    }

    Patient IPatientDataAccess<Patient, int>.Update(int id, Patient patient)
    {
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"UPDATE Patient SET PFirstName='{patient.FirstName}', PMiddleName='{patient.MiddleName}', PLastName='{patient.LastName}'," +
                $"PMobile='{patient.MobileNo}', PEmailID='{patient.EmailID}', PHouseNo='{patient.HouseNo}', PSociety='{patient.Society}', PArea='{patient.Area}', " +
                $"PCity='{patient.City}', PState='{patient.State}', PDOB='{patient.DOB}', PGender='{patient.Gender}', PAgeType='{patient.AgeType}', " +
                $"PAdmit='{patient.Admit}' where PID={patient.PatientId}";
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
        return patient;
    }
}


