using Application.Dal.Contract;
using Application.Entities;
using Application.Entities.Base;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Application.Data.DataAccess;

public class OpdPatientDataAccess: IPatientDataAccess<OpdPatient, int>
{
    SqlConnection connection;
    SqlCommand command;

    public OpdPatientDataAccess()
    {
        connection = new SqlConnection("Data Source = 127.0.0.1; Initial Catalog = HealthcareSystem_DB; User Id = sa; Password = Docker@SQL123");
    }

    OpdPatient IPatientDataAccess<OpdPatient, int>.Create(OpdPatient patient)
    {
        decimal opdFees = 0;
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;

            //get OPD fees from Constants table and update patient object
            command.CommandText = $"SELECT CPrice FROM Constants WHERE CName='OPD FEES'";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                patient.OpdFees = Convert.ToDecimal(reader["CPrice"]);
            }
            reader.Close();
            int result = command.ExecuteNonQuery();

            command.CommandText = $"INSERT INTO OPD_Patient VALUES ({patient.PatientId}, {patient.DoctorId}, {patient.OpdFees}, {patient.DiagnoseFees}, " +
                $"'{patient.Dressing}', '{patient.Ecg}', '{patient.BloodTest}', {patient.MedicineCharges}, {patient.BillAmount}, " +
                $"'{patient.BillPaid}')";
            result = command.ExecuteNonQuery();

            
        }
        catch (SqlException sqlEx)
        {
            throw sqlEx;
        }
        catch(Exception ex)
        {
            throw ex;
        }
        finally
        {
            connection.Close();
        }
        return patient;
    }

    OpdPatient IPatientDataAccess<OpdPatient, int>.Delete(int id)
    {
        OpdPatient opdPatient = null;
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"DELETE FROM OPD_Patient WHERE PID = {id}";
            int result = command.ExecuteNonQuery();
        }
        catch(SqlException sqlEx)
        {
            Console.WriteLine($"Error occurred while processing the request - {sqlEx.Message}");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"General error  - {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
        return opdPatient;
    }

    IEnumerable<OpdPatient> IPatientDataAccess<OpdPatient, int>.Get()
    {
        List<OpdPatient> opdPatients = new List<OpdPatient>();
        try
        {
            connection.Open();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM OPD_Patient";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                opdPatients.Add(
                      new OpdPatient()
                      {
                          PatientId = Convert.ToInt32(reader["PID"]),
                          DoctorId = Convert.ToInt32(reader["DID"]),
                          OpdFees = Convert.ToDecimal(reader["OPDFees"]),
                          DiagnoseFees = Convert.ToDecimal(reader["ODiagnoseFees"]),
                          Dressing = Convert.ToBoolean(reader["ODressing"]),
                          BloodTest = Convert.ToBoolean(reader["OBloodTest"]),
                          Ecg = Convert.ToBoolean(reader["OECG"]),
                          MedicineCharges = Convert.ToDecimal(reader["OMedicineCharges"]),
                          BillAmount = Convert.ToDecimal(reader["OBillAmount"]),
                          BillPaid = Convert.ToBoolean(reader["OBillPaid"])
                      }
                    ) ;
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
        return opdPatients;
    }

    OpdPatient IPatientDataAccess<OpdPatient, int>.Get(int id)
    {
        OpdPatient opdPatient = null;
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM OPD_Patient WHERE PID = {id}";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                opdPatient = new OpdPatient()
                {
                    PatientId = Convert.ToInt32(reader["PID"]),
                    DoctorId = Convert.ToInt32(reader["DID"]),
                    OpdFees = Convert.ToDecimal(reader["OPDFees"]),
                    DiagnoseFees = Convert.ToDecimal(reader["ODiagnoseFees"]),
                    Dressing = Convert.ToBoolean(reader["ODressing"]),
                    BloodTest = Convert.ToBoolean(reader["OBloodTest"]),
                    Ecg = Convert.ToBoolean(reader["OECG"]),
                    MedicineCharges = Convert.ToDecimal(reader["OMedicineCharges"]),
                    BillAmount = Convert.ToDecimal(reader["OBillAmount"]),
                    BillPaid = Convert.ToBoolean(reader["OBillPaid"])
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
        return opdPatient;
    }

    OpdPatient IPatientDataAccess<OpdPatient, int>.Update(int id, OpdPatient patient)
    {
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"UPDATE OPD_Patient SET DID={patient.DoctorId}, OPDFees={patient.OpdFees}, ODiagnoseFees={patient.DiagnoseFees}," +
                $"ODressing='{patient.Dressing}', OBloodTest='{patient.BloodTest}', OECG='{patient.Ecg}', OMedicineCharges={patient.MedicineCharges}," +
                $"OBillAmount={patient.BillAmount}, OBillPaid='{patient.BillPaid}' where PID={patient.PatientId}";
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

