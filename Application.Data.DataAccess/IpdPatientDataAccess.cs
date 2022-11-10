using System;
using Application.Dal.Contract;
using Application.Entities;
using Application.Entities.Base;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Reflection;
using System.Security.AccessControl;

namespace Application.Data.DataAccess;

public class IpdPatientDataAccess : IPatientDataAccess<IpdPatient, int>
{
    SqlConnection connection;
    SqlCommand command;

    public IpdPatientDataAccess()
    {
        connection = new SqlConnection("Data Source = 127.0.0.1; Initial Catalog = HealthcareSystem_DB; User Id = sa; Password = Docker@SQL123");
    }

    IpdPatient IPatientDataAccess<IpdPatient, int>.Create(IpdPatient patient)
    {
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"INSERT INTO IPD_Patient VALUES ({patient.PatientId}, {patient.DoctorId}, {patient.NurseId}, {patient.RoomTypeId}, " +
                $"'{patient.Ward}', '{patient.RoomNo}', '{patient.AdvancePaid}', CONVERT(DATETIME,'{patient.AdmittedDate}'), CONVERT(DATETIME,'{patient.DischargeDate}')," +
                $"{patient.NoOfDoctorVisits}, {patient.MedicineCharges}, '{patient.BloodCheck}', '{patient.Radiology}', '{patient.Injection}'," +
                $"{patient.LaundryCharges} {patient.FoodCharges}, {patient.BillAmount}, '{patient.BillPaid}')";
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

    IpdPatient IPatientDataAccess<IpdPatient, int>.Delete(int id)
    {
        IpdPatient IpdPatient = null;
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"DELETE FROM IPD_Patient WHERE PID = {id}";
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
        return IpdPatient;
    }

    IEnumerable<IpdPatient> IPatientDataAccess<IpdPatient, int>.Get()
    {
        List<IpdPatient> IpdPatients = new List<IpdPatient>();
        try
        {
            connection.Open();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM IPD_Patient";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                IpdPatients.Add(
                      new IpdPatient()
                      {
                          PatientId = Convert.ToInt32(reader["PID"]),
                          DoctorId = Convert.ToInt32(reader["DID"]),
                          NurseId = Convert.ToInt32(reader["NID"]),
                          RoomTypeId = Convert.ToInt32(reader["RID"]),
                          Ward = Convert.ToString(reader["IWard"]),
                          RoomNo = Convert.ToInt32(reader["RoomNo"]),
                          AdvancePaid = Convert.ToBoolean(reader["IAdvance"]),
                          AdmittedDate = Convert.ToString(reader["IAdmitDate"]),
                          DischargeDate = Convert.ToString(reader["IDischargeDate"]),
                          NoOfDoctorVisits = Convert.ToInt32(reader["IDocVisitCount"]),
                          MedicineCharges = Convert.ToDecimal(reader["IMedicineCharges"]),
                          BloodCheck = Convert.ToBoolean(reader["IBloodCheck"]),
                          Radiology = Convert.ToBoolean(reader["IRadiology"]),
                          Injection = Convert.ToBoolean(reader["IInjection"]),
                          LaundryCharges = Convert.ToDecimal(reader["ILaundry"]),
                          FoodCharges = Convert.ToDecimal(reader["IFoodCharges"]),
                          BillAmount = Convert.ToDecimal(reader["IBillAmount"]),
                          BillPaid = Convert.ToBoolean(reader["IBillPaid"])
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
        return IpdPatients;
    }

    IpdPatient IPatientDataAccess<IpdPatient, int>.Get(int id)
    {
        IpdPatient IpdPatient = null;
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = $"SELECT PID, DID, NID, RID, IWard, IAdvance, IAdmitDate, IDischargeDate, IDocVisitCount, IMedicines" +
                $"IBloodCheck, IRadiology, IInjection, ILaundry, IFoodCharges, IBillAmount, IBillPaid FROM IPD_Patient WHERE PID = {id}";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                IpdPatient = new IpdPatient()
                {
                    PatientId = Convert.ToInt32(reader["PID"]),
                    DoctorId = Convert.ToInt32(reader["DID"]),
                    NurseId = Convert.ToInt32(reader["NID"]),
                    RoomTypeId = Convert.ToInt32(reader["RID"]),
                    Ward = Convert.ToString(reader["IWard"]),
                    RoomNo = Convert.ToInt32(reader["IRoomNo"]),
                    AdvancePaid = Convert.ToBoolean(reader["IAdvance"]),
                    AdmittedDate = Convert.ToString(reader["IAdmitDate"]),
                    DischargeDate = Convert.ToString(reader["IDischargeDate"]),
                    NoOfDoctorVisits = Convert.ToInt32(reader["IDocVisitCount"]),
                    MedicineCharges = Convert.ToDecimal(reader["IMedicineCharges"]),
                    BloodCheck = Convert.ToBoolean(reader["IBloodCheck"]),
                    Radiology = Convert.ToBoolean(reader["IRadiology"]),
                    Injection = Convert.ToBoolean(reader["IInjection"]),
                    LaundryCharges = Convert.ToDecimal(reader["ILaundry"]),
                    FoodCharges = Convert.ToDecimal(reader["IFoodCharges"]),
                    BillAmount = Convert.ToDecimal(reader["IBillAmount"]),
                    BillPaid = Convert.ToBoolean(reader["IBillPaid"])
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
        return IpdPatient;
    }

    IpdPatient IPatientDataAccess<IpdPatient, int>.Update(int id, IpdPatient patient)
    {
        try
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = $"UPDATE IPD_Patient SET DID={patient.DoctorId}, NID={patient.NurseId}, RID={patient.RoomTypeId}," +
                $"IWard='{patient.Ward}', IAdvance='{patient.AdvancePaid}', IAdmitDate='{patient.AdmittedDate}', IDischargeDate='{patient.DischargeDate}'," +
                $"IDocVisitCount={patient.NoOfDoctorVisits}, IMedicineCharges={patient.MedicineCharges}, IBloodCheck='{patient.BloodCheck}', " +
                $"IRadiology='{patient.Radiology}', IInjection='{patient.Injection}', ILaundry={patient.LaundryCharges}, IFoodCharges={patient.FoodCharges}, " +
                $"IBillAmount={patient.BillAmount}, IBillPaid='{patient.BillPaid}' where PID={patient.PatientId}";
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


